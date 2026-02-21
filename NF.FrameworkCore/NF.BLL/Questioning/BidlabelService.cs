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
    public partial class BidlabelService
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
        public LayPageInfo<BidlabelDTO> GetKbqkList<s>(PageInfo<Bidlabel> pageInfo, Expression<Func<Bidlabel, bool>> whereLambda, Expression<Func<Bidlabel, s>> orderbyLambda, bool isAsc)

        {
            
            var tempquery = _BidlabelSet.AsNoTracking().Include(a => a.WinningUnitNavigation.UnitNavigation).Where<Bidlabel>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();


            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<Bidlabel>))
            { //分页
                tempquery = tempquery.Skip<Bidlabel>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<Bidlabel>(pageInfo.PageSize);
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
                            Zbdwid = a.Zbdwid
                        };
            var local = from a in query.AsEnumerable()
                        select new BidlabelDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            WinningUnit = a.WinningUnit,
                            WinningUnitName = a.WinningUnitName,
                            BidPrices = a.BidPrices,
                            BidPrice = a.BidPrice,
                            BidPricesthis=a.BidPrices.ThousandsSeparator(),
                            BidPricethis = a.BidPrice.ThousandsSeparator(),
                            BidUser = a.BidUser ?? 0,
                            BidUserName = this.Db.GetRedisUserFieldValue(a.BidUser ?? -1),
                            //RedisValueUtility.GetUserShowName(a.BidUser ?? 0), //创建人
                            Zbdwid = a.Zbdwid
                        };
            return new LayPageInfo<BidlabelDTO>()
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
            string sqlstr = "update Bidlabel set Bidlabel=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }
        /// <summary>
        /// 保存合同标的
        /// </summary>
        /// <param name="subjectMatter">合同标的</param>
        /// <returns>Id:->Hid:</returns>
        public bool AddSave(IList<BidlabelDTO> subs, int contId)
        {

            var arrayIds = subs.Select(a => a.Id).ToArray();
            var query = Db.Set<Bidlabel>().AsNoTracking().Where(a => arrayIds.Contains(a.Id)).ToList();
            IList<Bidlabel> subjectMatters = new List<Bidlabel>();
            foreach (Bidlabel item in query)
            {
                var submt = subs.Where(a => a.Id == item.Id).FirstOrDefault();
              //  var submt = dtomel.ToModel<BidlabelDTO, Bidlabel>();
                submt.Name = submt.Name;
                submt.WinningUnit = submt.WinningUnit;
                submt.BidPrices = ParseThousandthString(submt.BidPricesthis);
                submt.BidPrice = ParseThousandthString(submt.BidPricethis);
                submt.BidUser = submt.BidUser;
                submt.QuesId = (contId) <= 0 ? item.QuesId : contId;
                submt.Bidlabel1 = item.Bidlabel1; //item.IsDelete;
                subjectMatters.Add(submt);
            }
            //添加历史
            this.Update(subjectMatters);
            return true;
        }
        public bool AddSaveInfro(BidlabelDTO subs)
        {
            this.Add(subs);
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
        public LayPageInfo<OpenBidDTO> GetList(PageInfo<OpenBid> pageInfo, Expression<Func<OpenBid, bool>> whereLambda)
        {
            var tempquery = Db.Set<OpenBid>().AsNoTracking().Include(a => a.UnitNavigation).Where<OpenBid>(whereLambda.Compile());
            pageInfo.TotalCount = tempquery.Count();
            tempquery = tempquery.OrderByDescending(a => a.Id);
            tempquery = tempquery.Skip<OpenBid>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<OpenBid>(pageInfo.PageSize);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Unit = a.Unit,
                            UnitName = a.Unit == 0 ? "暂无单位" : a.UnitNavigation.Name
                        };
            var local = from a in query.AsEnumerable()
                        select new OpenBidDTO
                        {
                            Id = a.Id,
                            Unit = a.Unit ?? 0,
                            UnitName = a.UnitName
                            //IsMainDic = EmunUtility.GetDesc(typeof(OtherDataState), a.IsMain ?? 0)


                        };
            return new LayPageInfo<OpenBidDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };

        }
    }
}
