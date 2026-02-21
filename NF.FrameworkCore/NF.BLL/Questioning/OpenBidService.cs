using Microsoft.EntityFrameworkCore;
using NF.AutoMapper;
using NF.BLL.Extend;
using NF.Common.Extend;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NF.BLL
{
    public partial class OpenBidService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<OpenBidDTO> GetKbqkList<s>(PageInfo<OpenBid> pageInfo, Expression<Func<OpenBid, bool>> whereLambda, Expression<Func<OpenBid, s>> orderbyLambda, bool isAsc)

        {
            var tempquery = _OpenBidSet.Include(a => a.UnitNavigation).AsTracking().Where<OpenBid>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();


            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<OpenBid>))
            { //分页
                tempquery = tempquery.Skip<OpenBid>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<OpenBid>(pageInfo.PageSize);
            }
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Unit = a.Unit,
                            UnitName = a.Unit == 0 ? "新建" : a.UnitNavigation.Name,
                            TotalPrices = a.TotalPrices,
                            UnitPrice = a.UnitPrice,
                            Personnel = a.Personnel,
                        };
            var local = from a in query.AsEnumerable()
                        select new OpenBidDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Unit = a.Unit ?? 0,
                            UnitName = a.UnitName,
                            TotalPrices = a.TotalPrices,
                            TotalPricesthis = a.TotalPrices.ThousandsSeparator(),
                            UnitPrice = a.UnitPrice,
                            UnitPricethis = a.UnitPrice.ThousandsSeparator(),
                            Personnel = a.Personnel,
                            PersonneName = this.Db.GetRedisUserFieldValue(a.Personnel ?? -1),
                            //RedisValueUtility.GetUserShowName(a.Personnel ?? 0), //创建人
                        };
            return new LayPageInfo<OpenBidDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };


        }

        /// <summary>
        /// 保存合同标的
        /// </summary>
        /// <param name="subjectMatter">合同标的</param>
        /// <returns>Id:->Hid:</returns>
        public bool AddSave(IList<OpenBidDTO> subs, int contId)
        {

            var arrayIds = subs.Select(a => a.Id).ToArray();
            var query = Db.Set<OpenBid>().AsNoTracking().Where(a => arrayIds.Contains(a.Id)).ToList();
            IList<OpenBid> subjectMatters = new List<OpenBid>();
            foreach (OpenBid item in query)
            {
                var submt = subs.Where(a => a.Id == item.Id).FirstOrDefault();
                // var submt = dtomel.ToModel<OpenBidDTO, OpenBid>();
                submt.TotalPrices = ParseThousandthString(submt.TotalPricesthis);
                submt.UnitPrice = ParseThousandthString(submt.UnitPricethis);
                submt.Name = submt.Name;
                submt.Unit = submt.Unit;
                submt.TotalPrices = submt.TotalPrices;
                submt.UnitPrice = submt.UnitPrice;
                submt.Personnel = submt.Personnel;
                submt.QuesId = (contId) <= 0 ? item.QuesId : contId;
                submt.IsDelete = item.IsDelete; //item.IsDelete;


                subjectMatters.Add(submt);

            }
            //添加历史

            this.Update(subjectMatters);

            return true;


        }

        /// <summary>
        ///千分位转数字
        /// </summary>
        /// <param name="thousandthStr"></param>
        /// <returns></returns>
        public int ParseThousandthString(string thousandthStr)
        {
            int _value = -1;
            if (!string.IsNullOrEmpty(thousandthStr))
            {
                try
                {
                    _value = int.Parse(thousandthStr, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                }
                catch (Exception ex)
                {
                    _value = -1;
                    // Debug.WriteLine(string.Format("将千分位字符串{0}转换成数字异常，原因:{0}", thousandthStr, ex.Message));
                }
            }
            return _value;
        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">需要删除的Ids集合</param>
        /// <returns>受影响行数</returns>
        public int Delete(string Ids)
        {
            string sqlstr = "update OpenBid set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }
    }
}
