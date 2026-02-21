using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NF.ViewModel.Models.Common;
using NF.Common.Extend;
using NF.BLL.Common;
using NF.BLL.Extend;

namespace NF.BLL
{
    /// <summary>
    /// 单品管理
    /// </summary>
    public partial class BcInstanceService
    {
        /// <summary>
        /// 单品管理列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">Where条件表达式</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns>单品管理列表</returns>
        public LayPageInfo<BcInstanceViewDTO> GetList<s>(PageInfo<BcInstance> pageInfo, Expression<Func<BcInstance, bool>> whereLambda, Expression<Func<BcInstance, s>> orderbyLambda, bool isAsc)
        {
            IList<BusinessCategoryDTO> listcates = RedisHelper.StringGetToList<BusinessCategoryDTO>("NF-BcCateGoryListAll");
            var tempquery = _BcInstanceSet.AsTracking().Include(a=>a.CreateUser).Where(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (!(pageInfo is NoPageInfo<BcInstance>))
            {
                tempquery = tempquery.Skip<BcInstance>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<BcInstance>(pageInfo.PageSize);
            }
             
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            Remark = a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            LbId = a.LbId,
                            Unit=a.Unit,//单位
                            Price=a.Price,//报价
                            Pro=a.Pro,//属性

                        };
            var local = from a in query.AsEnumerable()
                        select new BcInstanceViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            Remark = a.Remark,
                            Unit = a.Unit,//单位
                            PriceThod = a.Price.ThousandsSeparator(),//报价
                            CreateDateTime = a.CreateDateTime,
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                         //   RedisValueUtility.GetUserShowName(a.CreateUserId),
                            CatePath = BcCateUtility.GetCatePath(a.LbId??-1,listcates),
                            ProDic = EmunUtility.GetDesc(typeof(BcPropertyEnums), (a.Pro ?? -1))
                        };
            return new LayPageInfo<BcInstanceViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0
            };
        }
        
        /// <summary>
        /// 校验某一字段值是否已经存在
        /// </summary>
        /// <param name="fieldInfo">字段相关信息</param>
        /// <returns>True:存在/False不存在</returns>
        public bool CheckInputValExist(UniqueFieldInfo fieldInfo)
        {
            var predicateAnd = PredicateBuilder.True<BcInstance>();
            //不等于fieldInfo.CurrId是排除修改的时候
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0 && a.Id != fieldInfo.Id);
            switch (fieldInfo.FieldName)
            {
                case "Name":
                    predicateAnd = predicateAnd.And(a => a.Name == fieldInfo.FieldValue);
                    break;
                case "Code":
                    predicateAnd = predicateAnd.And(a => a.Code == fieldInfo.FieldValue);
                    break;

            }
            return GetQueryable(predicateAnd).AsNoTracking().Any();

        }
        /// <summary>
        /// 单品管理
        /// </summary>
        /// <param name="bcInstance">单品对象</param>
        /// <returns></returns>
        public BcInstance Save(BcInstance bcInstance)
        {
            BcInstance reltinfo = null;
            if (bcInstance.Id <= 0)
            {
                bcInstance.CreateDateTime = DateTime.Now;
                reltinfo = Add(bcInstance);
                UpdateItems(reltinfo.Id,reltinfo.CreateUserId);
            }
            else
            {
                Update(bcInstance);
                reltinfo = bcInstance;
            }
           
            return reltinfo;
        }
        /// <summary>
        /// 查看信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public BcInstanceViewDTO ShowView(int Id)
        {
            IList<BusinessCategoryDTO> listcates = RedisHelper.StringGetToList<BusinessCategoryDTO>("NF-BcCateGoryListAll");
            var query = from a in _BcInstanceSet.AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            Unit=a.Unit,//单位
                            Price=a.Price,//单价
                            Pro = a.Pro,//属性
                            Remark=a.Remark,//备注
                            LbId=a.LbId,


                        };
            var local = from a in query.AsEnumerable()

                        select new BcInstanceViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserName = RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"),//tempInfo.CreateUser.DisplyName,
                            Unit = a.Unit,//单位
                            ProDic = EmunUtility.GetDesc(typeof(BcPropertyEnums),(a.Pro??0)),//属性
                            Remark = a.Remark,//备注
                            PriceThod=a.Price.ThousandsSeparator(),
                            LbId=a.LbId??0,
                            Price=a.Price,
                            Pro=a.Pro,
                            CatePath = BcCateUtility.GetCatePath(a.LbId ?? -1, listcates),


                        };
            return local.FirstOrDefault();



        }
        /// <summary>
        /// 清除标签垃圾数据
        /// </summary>
        /// <param name="currUserId">当前用户ID</param>
        /// <returns></returns>
        public int ClearJunkItemData(int currUserId)
        {
            StringBuilder strsql = new StringBuilder();
           
            strsql.Append($"delete BcAttachment  where BcId={-currUserId};");
            //添加其他标签表
            return ExecuteSqlCommand(strsql.ToString());
        }
        /// <summary>
        /// 修改当前对应标签下的-UserId数据
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <param name="currUserId">当前用户ID</param>
        public int UpdateItems(int Id, int currUserId)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append($"update BcAttachment set BcId={Id} where BcId={-currUserId};");
          
            //添加其他标签表
            return ExecuteSqlCommand(strsql.ToString());

        }
       


    }
}
