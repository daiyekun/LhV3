using Microsoft.EntityFrameworkCore;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.ViewModel.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NF.BLL
{
    /// <summary>
    /// 币种管理
    /// </summary>
   public partial  class CurrencyManagerService
    {
        /// <summary>
        /// 校验某一字段值是否已经存在
        /// </summary>
        /// <param name="fieldInfo">字段相关信息</param>
        /// <returns>True:存在/False不存在</returns>
        public bool CheckInputValExist(UniqueFieldInfo fieldInfo)
        {
            var predicateAnd = PredicateBuilder.True<CurrencyManager>();
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
        /// 查询币种列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">where条件表达式</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns>返回列表对象</returns>
        public LayPageInfo<CurrencyManagerViewDTO> GetList<s>(PageInfo<CurrencyManager> pageInfo, Expression<Func<CurrencyManager, bool>> whereLambda,
            Expression<Func<CurrencyManager, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = _CurrencyManagerSet.AsTracking().Where<CurrencyManager>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<CurrencyManager>))
            { //分页
                tempquery = tempquery.Skip<CurrencyManager>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<CurrencyManager>(pageInfo.PageSize);
            }
           
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            Abbreviation=a.Abbreviation,//英文缩写
                            Rate =a.Rate,//汇率
                            Remark=a.Remark,//备注
                            ShortName= a.ShortName

                        };
            var local = from a in query.AsEnumerable()
                        select new CurrencyManagerViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            Abbreviation = a.Abbreviation,//英文缩写
                            Rate = a.Rate,//汇率
                            Remark = a.Remark,//备注
                            ShortName = a.ShortName
                        };
            return new LayPageInfo<CurrencyManagerViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }
        /// <summary>
        /// 查询币种列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">where条件表达式</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns>返回列表对象</returns>
        public LayPageInfo<CurrencyManagerSelectViewDTO> GetSelectList<s>(PageInfo<CurrencyManager> pageInfo, Expression<Func<CurrencyManager, bool>> whereLambda,
            Expression<Func<CurrencyManager, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = _CurrencyManagerSet.AsTracking().Where<CurrencyManager>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<CurrencyManager>))
            { //分页
                tempquery = tempquery.Skip<CurrencyManager>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<CurrencyManager>(pageInfo.PageSize);
            }

            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            Abbreviation = a.Abbreviation,//英文缩写
                            Rate = a.Rate,//汇率
                            //Remark = a.Remark,//备注
                            ShortName = a.ShortName

                        };
            var local = from a in query.AsEnumerable()
                        select new CurrencyManagerSelectViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            Abbreviation = a.Abbreviation,//英文缩写
                            Rate = a.Rate,//汇率
                            //Remark = a.Remark,//备注
                            ShortName = a.ShortName
                        };
            return new LayPageInfo<CurrencyManagerSelectViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">需要删除的Ids集合</param>
        /// <returns>受影响行数</returns>
        public int Delete(string Ids)
        {
          
            string sqlstr = "update CurrencyManager set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }
        /// <summary>
        /// 查看信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public CurrencyManagerViewDTO ShowView(int Id)
        {
            var query = from a in _CurrencyManagerSet
                        where a.Id == Id
                        select new CurrencyManagerViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            Abbreviation = a.Abbreviation,//缩写英文
                            Rate = a.Rate,//汇率
                            Remark = a.Remark,//备注
                            ShortName = a.ShortName
                        };
            return query.FirstOrDefault();



        }
        /// <summary>
        /// 查询币种返回Redis需要数据
        /// </summary>
        /// <returns>RedisCurrency集合</returns>
      public  IList<RedisCurrency> GetRedisCurrencies(Expression<Func<CurrencyManager, bool>> whereLambda)
        {
            var query = from a in GetQueryable(whereLambda)
                        select new {
                            Id = a.Id,
                            Name = a.Name,
                            Abbreviation = a.Abbreviation,//英文缩写
                            Rate = a.Rate,//汇率
                            ShortName = a.ShortName
                        };
            var local = from a in query
                        select new RedisCurrency
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Abbreviation = a.Abbreviation,//英文缩写
                            Rate = a.Rate,//汇率
                            ShortName = a.ShortName

                        };
            return local.ToList();
        }
        /// <summary>
        /// 存储Redis
        /// </summary>
        public void SetRedis()
        {
            var list = GetRedisCurrencies(a => a.IsDelete == 0);
            foreach (var item in list)
            {
                SysIniInfoUtility.SetRedisHash(item, StaticData.RedisCurrencyKey, (a, c) =>
                {
                    return $"{a}:{c}";
                });


            }
        }

    }
}
