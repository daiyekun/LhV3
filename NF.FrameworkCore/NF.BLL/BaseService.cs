using Microsoft.EntityFrameworkCore;
using NF.Common.Utility;
using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;


namespace NF.BLL
{
    public abstract class BaseService<T>
        where T : class, new()

    {

        protected DbContext Db { get; private set; }
        public BaseService(DbContext context)
        {
            this.Db = context;
        }
        /// <summary>
        /// 无参数构造
        /// </summary>
        public BaseService()
        {
        }


        public int SaveChanges()
        {

            return this.Db.SaveChanges();
        }

        public T Add(T Info)
        {

            Db.Set<T>().Add(Info);
            this.SaveChanges();
            return Info;
        }


        public IEnumerable<T> Add(IEnumerable<T> tList)
        {
            this.Db.Set<T>().AddRange(tList);
            this.SaveChanges();//一个链接  多个sql
            return tList;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Info">删除对象</param>
        /// <returns>true:成功/false:失败</returns>
        public bool Delete(T Info)
        {
            Db.Entry<T>(Info).State = EntityState.Deleted;

            return this.SaveChanges() > 0;
            
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns>true:成功,false:失败</returns>
        public bool Delete(Expression<Func<T, bool>> predicate)
        {
            var entitys = Db.Set<T>().Where(predicate).ToList();
            entitys.ForEach(m => Db.Entry<T>(m).State = EntityState.Deleted);
            return this.SaveChanges() > 0;
            

        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Info">修改对象</param>
        /// <returns>true:成功/false:失败</returns>
        public bool Update(T Info)
        {
            Db.Entry<T>(Info).State = EntityState.Modified;
             return this.SaveChanges() > 0;
           
        }
        
        public bool Update(IEnumerable<T> tList)
        {
            foreach (var t in tList)
            {
                //this.Db.Set<T>().Attach(t);
                this.Db.Entry<T>(t).State = EntityState.Modified;
            }
            return this.SaveChanges() > 0;
           
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">Where条件表达式</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">true:升序/false:降序</param>
        /// <returns>返回分页GetPageQueryable</returns>
        public PageInfo<T> GetPageQueryable<s>(PageInfo<T> pageInfo, Expression<Func<T, bool>> whereLambda, Expression<Func<T, s>> orderbyLambda, bool isAsc)
        {
            
            var tempquery = Db.Set<T>().Where<T>(whereLambda);
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
                tempquery = tempquery.OrderBy<T, s>(orderbyLambda);
            else
                tempquery = tempquery.OrderByDescending<T, s>(orderbyLambda);
            tempquery = tempquery.Skip<T>((pageInfo.PageIndex - 1) * pageInfo.PageSize)
                 .Take<T>(pageInfo.PageSize);
            pageInfo.PageList = tempquery.ToList();

            return pageInfo;
        }
        /// <summary>
        /// 普通查询
        /// </summary>
        /// <param name="whereLambda">Where条件表达式</param>
        /// <returns></returns>
        public IQueryable<T> GetQueryable(Expression<Func<T, bool>> whereLambda)
        {
            return Db.Set<T>().Where(whereLambda.Compile()).AsQueryable();
        }

       
        public int ExecuteSqlCommand(string strSql)
        {
            //return Db.Database.ExecuteSqlCommand(strSql);
            return Db.Database.ExecuteSqlRaw(strSql);
        }


        public int ExecuteSqlCommand(string strSql, DbParameter[] dbParameter)
        {
            return Db.Database.ExecuteSqlRaw(strSql, dbParameter);
        }
        /// <summary>
        /// 根据Ids批量删除
        /// </summary>
        /// <returns></returns>
        public int ExecuteDelSqlCommandByIds(string Ids)
        {
            string tablename = typeof(T).Name;
            string sqlstr =  $"delete {tablename} where Id in({Ids})";
            return Db.Database.ExecuteSqlRaw(sqlstr);
        }
        /// <summary>
        /// 根据ID返回实体对象
        /// </summary>
        /// <param name="Id">当前ID(主键)</param>
        /// <returns>实体对象</returns>
        public T Find(int Id) {
          return  Db.Set<T>().Find(Id);
        }

       public IQueryable<T> GetQueryToPage<s>(PageInfo<T> pageInfo, Expression<Func<T, bool>> whereLambda, Expression<Func<T, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = Db.Set<T>().AsTracking().Where<T>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if(!(pageInfo is NoPageInfo<T>))
            tempquery = tempquery.Skip<T>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<T>(pageInfo.PageSize);
            return tempquery;
        }
        /// <summary>
        /// 根据ID获取用户信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>返回用户显示信息</returns>
        public string GetUserName(int userId)
        {
            return userId == 0 ? "" : RedisHelper.HashGet($"{StaticData.RedisUserKey}:{userId}", "DisplyName").ToString();
           
        }
        /// <summary>
        /// 根据ID字符串删除数据(假删除)
        /// </summary>
        /// <param name="Ids">1,2,3...</param>
        /// <returns>受影响行数</returns>
        public virtual int UpdateIsDel(string Ids)
        {
            string sqlstr = $"update {typeof(T).Name} set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }







        ///// <summary>
        ///// 执行查询
        ///// </summary>
        ///// <typeparam name="TDto">DTO实体</typeparam>
        ///// <typeparam name="s">排序</typeparam>
        ///// <param name="tempQuery">数据库查询Iquery</param>
        ///// <param name="DtoQuery">DTO Query</param>
        ///// <param name="whereLambda">查询条件</param>
        ///// <param name="orderbyLambda">排序条件</param>
        ///// <param name="pageInfoDto">分页DTO</param>
        ///// <param name="isAsc">升序/降序</param>
        ///// <returns></returns>
        //public PageInfo<TDto> ExcetQueryToPage<TDto,s>(IQueryable<T> tempQuery,IQueryable<TDto> DtoQuery, Expression<Func<T, bool>> whereLambda, Expression<Func<T, s>> orderbyLambda, PageInfo<TDto> pageInfoDto,bool isAsc)
        //    where TDto: class, new()
        //{
        //     pageInfoDto.TotalCount = tempQuery.Count();
        //    if (isAsc)
        //        tempQuery = tempQuery.OrderBy<T, s>(orderbyLambda);
        //    else
        //        tempQuery = tempQuery.OrderByDescending<T, s>(orderbyLambda);
        //    tempQuery = tempQuery.Skip<T>((pageInfoDto.PageIndex - 1) * pageInfoDto.PageSize)
        //         .Take<T>(pageInfoDto.PageSize);
        //    pageInfoDto.PageList = DtoQuery.ToList();
        //    return pageInfoDto;
        //}
        ///// <summary>
        ///// 返回Iquery<T>
        ///// </summary>
        ///// <typeparam name="TDto">DTO模型，负责界面模型</typeparam>
        ///// <typeparam name="s">排序</typeparam>
        ///// <param name="tempQuery">临时Query</param>
        ///// <param name="DtoQuery">TDOQuery</param>
        ///// <param name="whereLambda"></param>
        ///// <param name="orderbyLambda"></param>
        ///// <param name="pageInfoDto"></param>
        ///// <param name="isAsc"></param>
        ///// <returns></returns>
        //public IQueryable<T> ExcetQuery<TDto, s>(IQueryable<T> tempQuery, IQueryable<TDto> DtoQuery, Expression<Func<T, bool>> whereLambda, Expression<Func<T, s>> orderbyLambda, PageInfo<TDto> pageInfoDto, bool isAsc)
        //    where TDto : class, new()
        //{
        //    pageInfoDto.TotalCount = tempQuery.Count();
        //    if (isAsc)
        //        tempQuery = tempQuery.OrderBy<T, s>(orderbyLambda);
        //    else
        //        tempQuery = tempQuery.OrderByDescending<T, s>(orderbyLambda);
        //    tempQuery = tempQuery.Skip<T>((pageInfoDto.PageIndex - 1) * pageInfoDto.PageSize)
        //         .Take<T>(pageInfoDto.PageSize);
        //    return tempQuery;
        //}






    }
}
