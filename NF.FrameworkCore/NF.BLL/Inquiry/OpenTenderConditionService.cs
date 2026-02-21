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
    public partial class OpenTenderConditionService
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
        public LayPageInfo<OpenTenderConditionDTO> GetKbqkList<s>(PageInfo<OpenTenderCondition> pageInfo, Expression<Func<OpenTenderCondition, bool>> whereLambda, Expression<Func<OpenTenderCondition, s>> orderbyLambda, bool isAsc)

        {
            var tempquery = _OpenTenderConditionSet.Include(a => a.UnitNavigation).AsTracking().Where<OpenTenderCondition>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();


            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<OpenTenderCondition>))
            { //分页
                tempquery = tempquery.Skip<OpenTenderCondition>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<OpenTenderCondition>(pageInfo.PageSize);
            }
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Unit = a.Unit,
                            UnitName = a.Unit == 0 ? "新建" : a.UnitNavigation.Name,
                            UnitId =  a.UnitNavigation.Id,
                            TotalPrices = a.TotalPrices,
                            UnitPrice = a.UnitPrice,
                            Personnel = a.Personnel,
                            Lxr = a.Lxr,
                            Lxfs = a.Lxfs
                        };
            var local = from a in query.AsEnumerable()
                        select new OpenTenderConditionDTO
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
                           // RedisValueUtility.GetUserShowName(a.Personnel ?? 0), //创建人
                            Lxr = a.Lxr,
                            UnitId=a.UnitId,
                            Lxfs = a.Lxfs
                        };
            return new LayPageInfo<OpenTenderConditionDTO>()
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
        public bool AddSave(IList<OpenTenderConditionDTO> subs, int contId)
        {

            var arrayIds = subs.Select(a => a.Id).ToArray();
            var query = Db.Set<OpenTenderCondition>().AsNoTracking().Where(a => arrayIds.Contains(a.Id)).ToList();
            IList<OpenTenderCondition> subjectMatters = new List<OpenTenderCondition>();
            foreach (OpenTenderCondition item in query)
            {
                var submt = subs.Where(a => a.Id == item.Id).FirstOrDefault();
                // var submt = dtomel.ToModel<OpenTenderConditionDTO, OpenTenderCondition>();
                submt.TotalPrices = ParseThousandthString(submt.TotalPricesthis);
                submt.UnitPrice = ParseThousandthString(submt.UnitPricethis);
                submt.Name = submt.Name;
                submt.Unit = submt.Unit;
                submt.TotalPrices = submt.TotalPrices;
                submt.UnitPrice = submt.UnitPrice;
                submt.Lxr = submt.Lxr;
                submt.Lxfs = submt.Lxfs;
                submt.Personnel = submt.Personnel;
                submt.LnquiryId = (contId) <= 0 ? item.LnquiryId : contId;
                submt.IsDelete = item.IsDelete; //item.IsDelete;

          


                subjectMatters.Add(submt);

            }
            //添加历史

            this.Update(subjectMatters);

            return true;


        }


        public bool AddSaves(IList<OpenTenderConditionDTO> subs, int contId)
        {

            var arrayIds = subs.Select(a => a.Id).ToArray();
            var query = Db.Set<OpenTenderCondition>().AsNoTracking().Where(a => arrayIds.Contains(a.Id)).ToList();
            IList<OpenTenderCondition> subjectMatters = new List<OpenTenderCondition>();
            foreach (OpenTenderCondition item in query)
            {
                var submt = subs.Where(a => a.Id == item.Id).FirstOrDefault();
                // var submt = dtomel.ToModel<OpenTenderConditionDTO, OpenTenderCondition>();
                submt.TotalPrices = ParseThousandthString(submt.TotalPricesthis);
                submt.UnitPrice = ParseThousandthString(submt.UnitPricethis);
                submt.Name = submt.Name;
                submt.Unit = submt.Unit;
                submt.TotalPrices = submt.TotalPrices;
                submt.UnitPrice = submt.UnitPrice;
                submt.Lxr = submt.Lxr;
                submt.Lxfs = submt.Lxfs;
                submt.Personnel = submt.Personnel;
                submt.LnquiryId = (contId) <= 0 ? item.LnquiryId : contId;
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
            string sqlstr = "update OpenTenderCondition set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }
    }
}
