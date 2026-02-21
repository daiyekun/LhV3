using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
    /// <summary>
    /// 币种管理
    /// </summary>
   public partial  interface ICurrencyManagerService
    {
        /// <summary>
        /// 校验某一字段值是否已经存在
        /// </summary>
        /// <param name="fieldInfo">字段相关信息</param>
        /// <returns>True:存在/False不存在</returns>
        bool CheckInputValExist(UniqueFieldInfo fieldInfo);
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<CurrencyManagerViewDTO> GetList<s>(PageInfo<CurrencyManager> pageInfo, Expression<Func<CurrencyManager, bool>> whereLambda, Expression<Func<CurrencyManager, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 查询选择列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<CurrencyManagerSelectViewDTO> GetSelectList<s>(PageInfo<CurrencyManager> pageInfo, Expression<Func<CurrencyManager, bool>> whereLambda, Expression<Func<CurrencyManager, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 删除信息-软删除
        /// </summary>
        /// <param name="Ids">删除数据Ids</param>
        /// <returns>受影响行数</returns>
        int Delete(string Ids);
        /// <summary>
        /// 显示查看基本信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        CurrencyManagerViewDTO ShowView(int Id);
        /// <summary>
        /// 查询币种返回Redis需要数据
        /// </summary>
        /// <returns>RedisCurrency集合</returns>
        IList<RedisCurrency> GetRedisCurrencies(Expression<Func<CurrencyManager, bool>> whereLambda);
        /// <summary>
        /// 存储Redis
        /// </summary>
        void SetRedis();
    }
}
