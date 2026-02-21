using Microsoft.EntityFrameworkCore;
using NF.BLL.Extend;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NF.BLL
{
  public partial class ContTextBorrowService
    {
        /// <summary>
        /// 保存借阅
        /// </summary>
        /// <param name="textBorrow">借阅对象</param>
        /// <returns></returns>
        public  ContTextBorrow AddSave(ContTextBorrow textBorrow)
        {
            var info= Add(textBorrow);
            string sqlstr = $"update ContTextArchive set BorrSumNumber=BorrSumNumber+{textBorrow.BorrNumber??0} where ContTextId={textBorrow.ContTextId}";
            var res=  ExecuteSqlCommand(sqlstr);
            return info;
        }
        /// <summary>
        /// 归还
        /// </summary>
        /// <param name="textRepay">归还对象</param>
        /// <returns></returns>
        public ContTextBorrow SaveRepay(ContTextBorrow textRepay)
        {
            var findinfo = _ContTextBorrowSet.Find(textRepay.Id);
            if (findinfo!=null)
            {
                findinfo.ModifyUserId = textRepay.ModifyUserId;
                findinfo.BorrHandlerUser = textRepay.BorrHandlerUser;
                findinfo.RepayHandlerUser = textRepay.RepayHandlerUser;
                findinfo.RepayNumber = textRepay.RepayNumber;
                findinfo.RepayUser = textRepay.RepayUser;
                findinfo.RepayDateTime = textRepay.RepayDateTime;
            }

            var info = Update(findinfo);
            string sqlstr = $"update ContTextArchive set BorrSumNumber=BorrSumNumber-{textRepay.RepayNumber ?? 0} where ContTextId={findinfo.ContTextId}";
            var res = ExecuteSqlCommand(sqlstr);
            return findinfo;
        }

       
        /// <summary>
        /// 查询借阅列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">Where条件表达式</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        public LayPageInfo<ContTextBorrowViewDTO> GetListBorrow<s>(PageInfo<ContTextBorrow> pageInfo,
            Expression<Func<ContTextBorrow, bool>> whereLambda,
            Expression<Func<ContTextBorrow, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = Db.Set<ContTextBorrow>().AsTracking().Where(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<ContTextBorrow>))
            { //分页
                tempquery = tempquery.Skip<ContTextBorrow>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take(pageInfo.PageSize);
            }

            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            ContTextId=a.ContTextId,
                            BorrUser=a.BorrUser,//借阅人
                            BorrDateTime=a.BorrDateTime,//借阅日期
                            BorrDeptName=a.BorrDeptName,//借阅部门
                            BorrRemark=a.BorrRemark,//借阅说明
                            BorrHandlerUser=a.BorrHandlerUser,//借阅处理人
                            BorrNumber=a.BorrNumber,//借阅数量
                            RepayNumber=a.RepayNumber,//归还数量
                            RepayHandlerUser=a.RepayHandlerUser,//归还处理人
                            RepayDateTime= a.RepayDateTime,//归还日期
                            RepayUser = a.RepayUser,//归还人
                        };
            var local = from a in query.AsEnumerable()
                        select new ContTextBorrowViewDTO
                        {
                            Id = a.Id,
                            ContTextId = a.ContTextId,
                            BorrUser = a.BorrUser,//借阅人
                            BorrDateTime = a.BorrDateTime,//借阅日期
                            BorrDeptName = a.BorrDeptName,//借阅部门
                            BorrRemark = a.BorrRemark,//借阅说明
                            BorrHandlerUser = a.BorrHandlerUser,//借阅处理人
                            BorrNumber = a.BorrNumber,//借阅数量
                            RepayNumber = a.RepayNumber,//归还数量
                            RepayDateTime = a.RepayDateTime,//归还日期
                            RepayUser = a.RepayUser,//归还人
                            RepayHandlerUser = a.RepayHandlerUser,//归还处理人
                            BorrowHandUName = this.Db.GetRedisUserFieldValue(a.BorrHandlerUser ?? -1),
                            //RedisValueUtility.GetUserShowName(a.BorrHandlerUser??0), //经办人
                            RepayHandUName = this.Db.GetRedisUserFieldValue(a.RepayHandlerUser ?? -1),
                            //RedisValueUtility.GetUserShowName(a.RepayHandlerUser ?? 0), //经办人

                        };
            return new LayPageInfo<ContTextBorrowViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }

    }
}
