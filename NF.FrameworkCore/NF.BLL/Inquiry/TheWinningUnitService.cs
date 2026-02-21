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
    public partial class TheWinningUnitService
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
        public LayPageInfo<TheWinningUnitDTO> GetKbqkList<s>(PageInfo<TheWinningUnit> pageInfo, Expression<Func<TheWinningUnit, bool>> whereLambda, Expression<Func<TheWinningUnit, s>> orderbyLambda, bool isAsc)

        {
            var tempquery = _TheWinningUnitSet.AsNoTracking().Include(a => a.WinningUnitNavigation.UnitNavigation).Where<TheWinningUnit>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();


            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<TheWinningUnit>))
            { //分页
                tempquery = tempquery.Skip<TheWinningUnit>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<TheWinningUnit>(pageInfo.PageSize);
            }
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            WinningUnit = a.WinningUnit,
                            WinningUnitName = a.WinningUnit == 0 ? "新建" : a.WinningUnitNavigation.UnitNavigation.Name,
                            BidPrices = a.BidPrices,
                            BidPrice = a.BidPrice,
                            BidUser = a.BidUser,
                            Zbdwid=a.Zbdwid,
                            Lxr = a.Lxr,
                            Lxfs = a.Lxfs
                        };
            var local = from a in query.AsEnumerable()
                        select new TheWinningUnitDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            WinningUnit = a.WinningUnit,
                            WinningUnitName = a.WinningUnitName,
                            BidPrices = a.BidPrices,
                            BidPricesthis=a.BidPrices.ThousandsSeparator(),
                            BidPrice = a.BidPrice,
                            BidPricethis = a.BidPrice.ThousandsSeparator(),
                            BidUser = a.BidUser ?? 0,
                            Zbdwid = a.Zbdwid,
                            BidUserName = this.Db.GetRedisUserFieldValue(a.BidUser ?? -1),
                            //RedisValueUtility.GetUserShowName(a.BidUser ?? 0), //创建人
                            Lxr = a.Lxr,
                            Lxfs = a.Lxfs
                        };
            return new LayPageInfo<TheWinningUnitDTO>()
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
            string sqlstr = "update TheWinningUnit set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }
        /// <summary>
        /// 保存合同标的
        /// </summary>
        /// <param name="subjectMatter">合同标的</param>
        /// <returns>Id:->Hid:</returns>
        public bool AddSave(IList<TheWinningUnitDTO> subs, int contId)
        {

            var arrayIds = subs.Select(a => a.Id).ToArray();
            var query = Db.Set<TheWinningUnit>().AsNoTracking().Where(a => arrayIds.Contains(a.Id)).ToList();
            IList<TheWinningUnit> subjectMatters = new List<TheWinningUnit>();
            foreach (TheWinningUnit item in query)
            {
                var submt = subs.Where(a => a.Id == item.Id).FirstOrDefault();
               // var submt = dtomel.ToModel<TheWinningUnitDTO, TheWinningUnit>();
                submt.Name = submt.Name;
                submt.WinningUnit = submt.WinningUnit;
                submt.BidPrices = ParseThousandthString(submt.BidPricesthis);
                submt.BidPrice = ParseThousandthString(submt.BidPricethis);
                submt.BidUser = submt.BidUser;
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
        /// 查询用户列表
        /// </summary>
        /// <param name="pageInfo">用户对象</param>
        /// <param name="whereLambda">Where条件</param>
        /// <returns>用户列表</returns>
        public LayPageInfo<OpenTenderConditionDTO> GetList(PageInfo<OpenTenderCondition> pageInfo, Expression<Func<OpenTenderCondition, bool>> whereLambda)
        {
            var tempquery = Db.Set<OpenTenderCondition>().AsNoTracking().Include(a => a.UnitNavigation).Where<OpenTenderCondition>(whereLambda.Compile());
            pageInfo.TotalCount = tempquery.Count();
            tempquery = tempquery.OrderByDescending(a => a.Id);
            tempquery = tempquery.Skip<OpenTenderCondition>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<OpenTenderCondition>(pageInfo.PageSize);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Unit = a.Unit,
                            UnitName = a.Unit == 0 ? "暂无单位" : a.UnitNavigation.Name
                        };
            var local = from a in query.AsEnumerable()
                        select new OpenTenderConditionDTO
                        {
                            Id = a.Id,
                            Unit = a.Unit ?? 0,
                            UnitName = a.UnitName
                            //IsMainDic = EmunUtility.GetDesc(typeof(OtherDataState), a.IsMain ?? 0)


                        };
            return new LayPageInfo<OpenTenderConditionDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };

        }

        public bool SaveBZData(TheWinningUnitDTO subs)
        {
            this.Add(subs);
            return true;
        }
    }
}
