using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Utility;
using NF.Web.Utility;
using Quartz;
using Quartz.Impl;

namespace NF.Web.Areas.System.Controllers
{
    [Area("System")]
    [Route("System/[controller]/[action]")]
    public class InitDataController : Controller
    {
        /// <summary>
        /// 字典
        /// </summary>
        private IDataDictionaryService _IDataDictionaryService;
        /// <summary>
        /// 用户
        /// </summary>
        private IUserInforService _IUserInforService;
        /// <summary>
        /// 机构
        /// </summary>
        private IDepartmentService _IDepartmentService;
        /// <summary>
        /// 币种
        /// </summary>
        private ICurrencyManagerService _ICurrencyManagerService;
       /// <summary>
       /// 市
       /// </summary>
        private ICityService _ICityService;
        /// <summary>
        /// 省
        /// </summary>
        private IProvinceService _IProvinceService;
        /// <summary>
        /// 国家
        /// </summary>
        private ICountryService _ICountryService;
        /// <summary>
        /// 菜单
        /// </summary>
        private ISysModelService _ISysModelService;
        /// <summary>
        /// 合同统计
        /// </summary>
        private IContStatisticService _IContStatisticService;


        public InitDataController(IDataDictionaryService IDataDictionaryService, 
            IUserInforService IUserInforService, IDepartmentService IDepartmentService,
            ICurrencyManagerService ICurrencyManagerService, ICityService ICityService,
            IProvinceService IProvinceService, ICountryService ICountryService
            ,ISysModelService ISysModelService
            , IContStatisticService IContStatisticService
            )
        {
            _IDataDictionaryService = IDataDictionaryService;
            _IUserInforService = IUserInforService;
            _IDepartmentService = IDepartmentService;
            _ICurrencyManagerService = ICurrencyManagerService;
            _ICityService = ICityService;
            _IProvinceService = IProvinceService;
            _ICountryService = ICountryService;
            _ISysModelService = ISysModelService;
            _IContStatisticService = IContStatisticService;


        }
        #region 方法
      

        #endregion
        public IActionResult Index()
        {
            return View();
        }
       
        /// <summary>
        /// 初始化数据字典
        /// </summary>
        /// <returns></returns>
        public IActionResult SetCacheDataDic()
        {
            _IDataDictionaryService.SetRedis();
            return RequestMsg();
        }
        /// <summary>
        /// 初始化组织机构
        /// </summary>
        /// <returns></returns>
        public IActionResult SetCacheDept()
        {
            _IDepartmentService.SetRedis();
            return RequestMsg();
        }

      

        /// <summary>
        /// 初始化币种
        /// </summary>
        /// <returns></returns>
        public IActionResult SetCurrency()
        {
            _ICurrencyManagerService.SetRedis();
            return RequestMsg();
        }
        /// <summary>
        /// 初始化用户信息
        /// </summary>
        /// <returns></returns>
        public IActionResult SetCacheUserData()
        {
            _IUserInforService.SetRedis();
            return RequestMsg();
        }
        /// <summary>
        /// 地址信息初始化
        /// </summary>
        private void SetAddressInfo()
        {
            _ICountryService.SetRedis();
            _IProvinceService.SetRedis();
            _ICityService.SetRedis();

        }
        /// <summary>
        /// 初始化菜单
        /// </summary>
        private void SystreeIni()
        {
            if (RedisHelper.KeyExists("Nf-SysModelListAll"))
            {
                RedisHelper.KeyDelete("Nf-SysModelListAll");
            }
           
            _ISysModelService.GetListAll();
            

        }
        /// <summary>
        /// 初始化菜单
        /// </summary>
        public IActionResult SetSystreeReids()
        {
            SystreeIni();
            return RequestMsg();

        }


        public IActionResult SetAddress()
        {
            SetAddressInfo();
            return RequestMsg();
        }
        /// <summary>
        /// 初始化地址信息
        /// </summary>
        /// <returns></returns>
        private static IActionResult RequestMsg()
        {
            return new CustomResultJson(new RequstResult()
            {
                Msg = "初始化成功",
                Code = 0,


            });
        }
        /// <summary>
        /// 一键初始化Redis
        /// </summary>
        /// <returns></returns>
        public IActionResult SetCacheAll()
        {
            _IDataDictionaryService.SetRedis();
            _IDepartmentService.SetRedis();
            _IUserInforService.SetRedis();
            _ICurrencyManagerService.SetRedis();
            SetAddressInfo();
            SystreeIni();
            SetBusinessCate();
            return RequestMsg();
        }
        /// <summary>
        /// 业务品类
        /// </summary>
        private void SetBusinessCate()
        {
            RedisHelper.KeyDeleteAsync("NF-BcCateGoryListAll");
        }
        /// <summary>
        /// 统计初始化
        /// </summary>
        /// <returns></returns>
        public IActionResult SetStataicdata(string No)
        {
            _IContStatisticService.SetContractTongJi(No);
            return RequestMsg();
        }

        public async Task StartTestAsync()
        {
            try
            {                // 从工厂中获取调度程序实例
                NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                IScheduler scheduler = await factory.GetScheduler();
                // 开启调度器
                await scheduler.Start();                // 定义这个工作，并将其绑定到我们的IJob实现类
                IJobDetail job = JobBuilder.Create<QuartzNet.Job.OptionLogJob>()
                    .WithIdentity("job1", "group1")
                    .Build();                // 触发作业立即运行，然后每10秒重复一次，无限循环
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(10)
                        .RepeatForever())
                    .Build();
                var flag = scheduler.CheckExists(trigger.JobKey);
                if (!flag.Result)
                {
                    await scheduler.Start();
                    await scheduler.ScheduleJob(job, trigger);
                }
                else
                {
                    ITrigger trigger1 = TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(10)
                        .RepeatForever())
                    .Build();
                   await scheduler.Start();
                    await scheduler.ScheduleJob(job, trigger1);
                }
                // 告诉Quartz使用我们的触发器来安排作业
                await scheduler.ScheduleJob(job, trigger);                // 等待60秒
                //await Task.Delay(TimeSpan.FromSeconds(60));                // 关闭调度程序
                //await scheduler.Shutdown();
              
            }
            catch (SchedulerException se)
            {
                await Console.Error.WriteLineAsync(se.ToString());
            }
        }

        /// <summary>
        /// 更新日子
        /// </summary>
        /// <returns></returns>
        public IActionResult UpdateLog()
        {
            return View();

        }


        }
}