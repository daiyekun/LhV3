using AutoMapper;
using NF.BLL;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Utility;
using NF.Web.Utility.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.Web.Utility.TimeJob
{
   
    /// <summary>
    /// 定时任务操作工具类
    /// </summary>
    public class TimeJobOptionUtility
    {
       
        /// <summary>
        /// 从队列获取信息写入数据库
        ///  //感觉有点不是特别好，但是目前只能想到的最好办法
        ///  //解决视图就是创建新服务器对象然后新注册，然后获取，这么做主要是为了解耦
        /// </summary>
        public static void GetQueueWriteDb()
        {
            //获取队列数据，添加到日志
            OpLogRedisListToDb();
            //删除缓存在Redis数据字典
            DelRedisHashDic();
            //删除用户Redis
            DelRedisHashUser();
            //删除币种
            DelRedisHashCurrency();
        }
        /// <summary>
        /// 出队删除Hash数据字典值
        /// </summary>
        private static void DelRedisHashDic()
        {
            if (RedisHelper.KeyExists(StaticData.RedisDataDelKey))
            {   //数据字典删除队列Key
                var service = ServicesDIUtility.GetService<IDataDictionaryService, DataDictionaryService, DataDictionary>();
                var listIds = StringHelper.String2ArrayInt(RedisHelper.ListLeftPop(StaticData.RedisDataDelKey));
                var pageInfo = new NoPageInfo<DataDictionary>();
                var list = service.GetList(pageInfo, a => true);
                foreach (DataDictionaryDTO dc in list.data)
                {

                    SysIniInfoUtility.DelDataDic(dc, StaticData.RedisDataKey, (a, b, c) =>
                    {
                        return $"{a}:{b}:{c}";
                    });

                }


            }
        }

        /// <summary>
        /// 后台定时执行出队操作日志到数据库
        /// </summary>
        private static void OpLogRedisListToDb()
        {
            if (RedisUtility._redisData.KeyExists(StaticData.OptionLogRedisKey))
            {
                IOptionLogService optionlogService = ServicesDIUtility.GetService<IOptionLogService, OptionLogService, OptionLog>(); //GetService();
                var opObj = RedisHelper.ListLeftPopToObj<OptionLog>(StaticData.OptionLogRedisKey);
                if (opObj != null)
                {
                    optionlogService.Add(opObj);
                }

            }
        }
        
        /// <summary>
        /// 删除用户Redis
        /// </summary>
        private static void DelRedisHashUser()
        {
            if (RedisHelper.KeyExists(StaticData.RedisUserDelKey))
            {   //数据字典删除队列Key
                var service = ServicesDIUtility.GetService<IUserInforService, UserInforService, UserInfor>();
                var listIds = StringHelper.String2ArrayInt(RedisHelper.ListLeftPop(StaticData.RedisUserDelKey));
                var list = service.GetRedisUsers(a=> listIds.Contains(a.Id));
                foreach (RedisUser user in list)
                {

                    SysIniInfoUtility.DelRedisHash(user, StaticData.RedisUserKey, (a, b) =>
                    {
                        return $"{a}:{b}";
                    });

                }


            }
        }
        /// <summary>
        /// 删除组织机构Redis
        /// </summary>
        private static void DelRedisHashDept()
        {
            if (RedisHelper.KeyExists(StaticData.RedisDelDeptKey))
            {   //数据字典删除队列Key
                var service = ServicesDIUtility.GetService<IDepartmentService, DepartmentService, Department>();
                var listIds = StringHelper.String2ArrayInt(RedisHelper.ListLeftPop(StaticData.RedisDelDeptKey));
                var list = service.GetRedisDepts(a => listIds.Contains(a.Id));
                foreach (RedisDept dept in list)
                {

                    SysIniInfoUtility.DelRedisHash(dept, StaticData.RedisDeptKey, (a, b) =>
                    {
                        return $"{a}:{b}";
                    });

                }


            }
        }
        /// <summary>
        /// 删除币种Redis
        /// </summary>
        private static void DelRedisHashCurrency()
        {
            if (RedisHelper.KeyExists(StaticData.RedisDelDeptKey))
            {   //数据字典删除队列Key
                var service = ServicesDIUtility.GetService<ICurrencyManagerService, CurrencyManagerService, CurrencyManager>();
                var listIds = StringHelper.String2ArrayInt(RedisHelper.ListLeftPop(StaticData.RedisDelCurrencyKey));
                var list = service.GetRedisCurrencies(a => listIds.Contains(a.Id));
                foreach (var  item in list)
                {

                    SysIniInfoUtility.DelRedisHash(item, StaticData.RedisCurrencyKey, (a, b) =>
                    {
                        return $"{a}:{b}";
                    });

                }


            }
        }
    }
}
