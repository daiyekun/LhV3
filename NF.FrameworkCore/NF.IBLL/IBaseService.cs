using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
   public interface IBaseService<T> where T : class, new()
    {
        /// <summary>
        /// 不分页查询
        /// </summary>
        /// <param name="whereLambda">where查询条件</param>
        /// <returns>IQueryable<T> where T:Model</returns>
        IQueryable<T> GetQueryable(Expression<Func<T, bool>> whereLambda);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="s">排序字段类型</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">where查询条件</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">True:表示正序、False:表示倒序</param>
        /// <returns>IQueryable<T> where T:Model</returns>
        PageInfo<T> GetPageQueryable<s>(PageInfo<T> pageInfo, Expression<Func<T, bool>> whereLambda, Expression<Func<T, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="info">实体对象</param>
        /// <returns>返回新增实体</returns>
        T Add(T info);
        /// <summary>
        /// 新增数据，即时Commit
        /// 多条sql 一个连接，事务插入
        /// </summary>
        /// <param name="tList"></param>
        /// <returns>返回带主键的集合</returns>
        IEnumerable<T> Add(IEnumerable<T> tList);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="info">删除的实体对象</param>
        /// <returns>true:成功，false：失败</returns>
        bool Delete(T info);
        /// <summary>
        /// 删除满足条件的数据
        /// </summary>
        /// <param name="whereLambda">where条件</param>
        /// <returns>删除成功</returns>
        bool Delete(Expression<Func<T, bool>> whereLambda);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="info">更新实体</param>
        /// <returns>true:成功,false失败</returns>
        bool Update(T info);
        /// <summary>
        /// 更新数据，即时Commit
        /// </summary>
        /// <param name="tList"></param>
        bool Update(IEnumerable<T> tList);
        ///// <summary>
        ///// 执行带参数SQL查询语句
        ///// </summary>
        ///// <param name="strSql">SQL语句</param>
        ///// <param name="dbParameter">参数</param>
        ///// <returns>IQueryable<T></returns>
        //IQueryable<T> GetQueryableBySqlQuery(string strSql, DbParameter[] dbParameter);
        ///// <summary>
        ///// 执行不带参数的SQL查询语句
        ///// </summary>
        ///// <param name="strSql">SQL语句</param>
        ///// <returns>IQueryable<T></returns>
        //IQueryable<T> GetQueryableBySqlQuery(string strSql);
        /// <summary>
        /// 执行不带参数新增，修改，删除SQL语句
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>返回受影响的行数</returns>
        int ExecuteSqlCommand(string strSql);
        /// <summary>
        /// 执行不带参数新增，修改，删除SQL语句
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <returns>返回受影响的行数</returns>
        int ExecuteSqlCommand(string strSql, DbParameter[] dbParameter);
        /// <summary>
        /// 提交
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        /// <summary>
        /// 根据Ids批量删除
        /// </summary>
        /// <param name="Ids">Ids</param>
        /// <returns>受影响行数</returns>
        int ExecuteDelSqlCommandByIds(string Ids);
        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="Id">当前ID(主键)</param>
        /// <returns>实体对象</returns>
        T Find(int Id);
        /// <summary>
        /// 根据ID字符串删除数据(假删除)
        /// </summary>
        /// <param name="Ids">1,2,3...</param>
        /// <returns>受影响行数</returns>
        int UpdateIsDel(string Ids);
    }
}
