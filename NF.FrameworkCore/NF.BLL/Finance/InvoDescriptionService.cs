using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using NF.Common.Extend;

namespace NF.BLL
{
    /// <summary>
    /// 发票明细
    /// </summary>
    public partial class InvoDescriptionService
    {
        /// <summary>
        /// 查询发票大列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<InvoDescriptionViewDTO> GetList<s>(PageInfo<InvoDescription> pageInfo, Expression<Func<InvoDescription, bool>> whereLambda, Expression<Func<InvoDescription, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var query = from a in tempquery
                        select new
                        {
                            Id=a.Id,
                            ContInvoId=a.ContInvoId,
                            Name=a.Name,
                            Price=a.Price,
                            Count=a.Count,
                            Total=a.Total

                        };
            var local = from a in query.AsEnumerable()
                        select new InvoDescriptionViewDTO
                        {
                            Id = a.Id,
                            ContInvoId = a.ContInvoId,
                            Name = a.Name,
                            Price = a.Price,
                            Count = a.Count,
                            Total = a.Total,
                            TotalThod=a.Total.ThousandsSeparator()

                        };
            return new LayPageInfo<InvoDescriptionViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };


        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <param name="field">当前字段</param>
        /// <param name="fdv">当前值</param>
        /// <returns></returns>
        public bool UpdateDesc(int Id, string field, string fdv)
        {
           var info= Find(Id);

            switch (field)
            {
                case "Name":
                    info.Name = fdv;
                    break;
                case "Price":
                    info.Price = Convert.ToDecimal(fdv);
                    info.Total = info.Price * info.Count;
                    break;
                case "Count":
                    info.Count = Convert.ToInt32(fdv);
                    info.Total = info.Price * info.Count;
                    break;

            }

          return  Update(info);

        }

        /// <summary>
        /// 删除发票明细
        /// </summary>
        /// <param name="Ids">需要删除的Ids集合</param>
        /// <returns>受影响行数</returns>
        public int Delete(string Ids)
        {
            string sqlstr = "update InvoDescription set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
           
        }
    }
}
