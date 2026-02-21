using Microsoft.EntityFrameworkCore;
using NF.AutoMapper;
using NF.BLL.Extend;
using NF.Common.Extend;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NF.BLL
{
    public partial class SuccessfulBidderLableService
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
        public LayPageInfo<SuccessfulBidderLableDTO> GetKbqkList<s>(PageInfo<SuccessfulBidderLable> pageInfo, Expression<Func<SuccessfulBidderLable, bool>> whereLambda, Expression<Func<SuccessfulBidderLable, s>> orderbyLambda, bool isAsc)

        {
            var tempquery = _SuccessfulBidderLableSet.AsNoTracking().Include(a => a.SuccessUnti.UnitNavigation).Where<SuccessfulBidderLable>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();


            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<SuccessfulBidderLable>))
            { //分页
                tempquery = tempquery.Skip<SuccessfulBidderLable>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<SuccessfulBidderLable>(pageInfo.PageSize);
            }
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            SuccessName=a.SuccessName,
                            SuccessUntiId = a.SuccessUntiId,
                            SuccessUntiName = a.SuccessUntiId==0?"新建":a.SuccessUnti.UnitNavigation.Name,
                            SuccessUntiIds= a.SuccessUnti.UnitNavigation.Id,
                            SuccTotalPrice = a.SuccTotalPrice,
                            SuccUitprice = a.SuccUitprice,
                            SuccId = a.SuccId,
                           //Zbdwid = a.Zbdwid
                        };
            var local = from a in query.AsEnumerable()
                        select new SuccessfulBidderLableDTO
                        {
                            Id = a.Id,
                            SuccessName= a.SuccessName,
                            SuccessUntiId=a.SuccessUntiId??0,
                            SuccessUntiName=a.SuccessUntiName,
                            SuccTotalPrice = a.SuccTotalPrice,
                            SuccTotalPricethis=a.SuccTotalPrice.ThousandsSeparator(),
                            SuccUitprice = a.SuccUitprice,
                            SuccUitpricethis=a.SuccUitprice.ThousandsSeparator(),
                            SuccId = a.SuccId??0,
                           // Zbdwid = a.Zbdwid,
                           // SuccName = this.Db.GetRedisUserFieldValue(a.SuccId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.SuccId??0), //创建人
                            SuccessUntiIds = a.SuccessUntiIds//客户id
                        };
            return new LayPageInfo<SuccessfulBidderLableDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };


        }
        public bool AddSaveInfro(SuccessfulBidderLableDTO subs)
        {
            this.Add(subs);
            return true;
        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">需要删除的Ids集合</param>
        /// <returns>受影响行数</returns>
        public int Delete(string Ids)
        {
            string sqlstr = "update SuccessfulBidderLable set IS_DELETE=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }
        /// <summary>
        /// 保存合同标的
        /// </summary>
        /// <param name="subjectMatter">合同标的</param>
        /// <returns>Id:->Hid:</returns>
        public bool AddSave(IList<SuccessfulBidderLableDTO> subs, int contId)
        {

            var arrayIds = subs.Select(a => a.Id).ToArray();
            var query = Db.Set<SuccessfulBidderLable>().AsNoTracking().Where(a => arrayIds.Contains(a.Id)).ToList();
            IList<SuccessfulBidderLable> subjectMatters = new List<SuccessfulBidderLable>();
            foreach (SuccessfulBidderLable item in query)
            {
                var submt = subs.Where(a => a.Id == item.Id).FirstOrDefault();
               // var submt = dtomel.ToModel<SuccessfulBidderLableDTO, SuccessfulBidderLable>();

                submt.SuccessName = submt.SuccessName;
                submt.SuccessUntiId = submt.SuccessUntiId;
                submt.SuccTotalPrice = ParseThousandthString(submt.SuccTotalPricethis);
                submt.SuccUitprice = ParseThousandthString(submt.SuccUitpricethis);
                submt.SuccId = submt.SuccId;
              // submt.Zbdwid=submt.SuccessUnti
                submt.TenderId = (contId) <= 0 ? item.TenderId : contId;
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
        public LayPageInfo<OpeningSituationInforDTO> GetList(PageInfo<OpeningSituationInfor> pageInfo, Expression<Func<OpeningSituationInfor, bool>> whereLambda)
        {
            var tempquery = Db.Set<OpeningSituationInfor>().AsNoTracking().Include(a => a.UnitNavigation).Where<OpeningSituationInfor>(whereLambda.Compile());
            pageInfo.TotalCount = tempquery.Count(); 
            tempquery = tempquery.OrderByDescending(a => a.Id);
            tempquery = tempquery.Skip<OpeningSituationInfor>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<OpeningSituationInfor>(pageInfo.PageSize);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Unit = a.Unit,
                            UnitName= a.Unit==0?"暂无单位":a.UnitNavigation.Name
                        };
            var local = from a in query.AsEnumerable()
                        select new OpeningSituationInforDTO
                        {
                            Id = a.Id,
                            Unit = a.Unit??0,
                            UnitName = a.UnitName
                            //IsMainDic = EmunUtility.GetDesc(typeof(OtherDataState), a.IsMain ?? 0)


                        };
            return new LayPageInfo<OpeningSituationInforDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };

        }
    }
}
