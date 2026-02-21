using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NF.AutoMapper;
using NF.Common.Extend;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Finance.Enums;

using NF.ViewModel.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NF.BLL
{
    public partial class WinningInqService
    {
        private IMapper _IMapper;
        public LayPageInfo<WinningInqDTO> GetList<s>(PageInfo<WinningInq> pageInfo, Expression<Func<WinningInq, bool>> whereLambda, Expression<Func<WinningInq, s>> orderbyLambda, bool isAsc)
        {
            //var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var tempquery = Db.Set<WinningInq>().AsTracking().Where<WinningInq>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<WinningInq>))
                tempquery = tempquery.Skip<WinningInq>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<WinningInq>(pageInfo.PageSize);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            WinningName = a.WinningName,
                            WinningUntiId = a.WinningUntiId,
                            WinningModel = a.WinningModel,
                            WinningTotalPrice = a.WinningTotalPrice,
                            WinningUitprice = a.WinningUitprice,
                            WinningQuantity = a.WinningQuantity,
                            IsDelete = a.IsDelete
                        };
            var local = from a in query.AsEnumerable()
                        select new WinningInqDTO
                        {
                            Id = a.Id,
                            WinningName = a.WinningName,
                            WinningUntiId = a.WinningUntiId,
                            WinningModel = a.WinningModel,
                            WinningTotalPrice = a.WinningTotalPrice,
                            WinningUitprice = a.WinningUitprice,
                            WinningQuantity = a.WinningQuantity,
                            IsDelete = a.IsDelete
                        };
            return new LayPageInfo<WinningInqDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }

        public LayPageInfo<WinningInqDTO> GetListView<s>(PageInfo<WinningInq> pageInfo, Expression<Func<WinningInq, bool>> whereLambda, Expression<Func<WinningInq, s>> orderbyLambda, bool isAsc)
        {
            //var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var tempquery = Db.Set<WinningInq>().AsTracking().Where<WinningInq>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<WinningInq>))
                tempquery = tempquery.Skip<WinningInq>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<WinningInq>(pageInfo.PageSize);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            WinningName = a.WinningName,
                            WinningUntiId = a.WinningUntiId,
                            WinningModel = a.WinningModel,
                            WinningTotalPrice = a.WinningTotalPrice,
                            WinningUitprice = a.WinningUitprice,
                            WinningQuantity = a.WinningQuantity,
                            IsDelete = a.IsDelete
                        };
            var local = from a in query.AsEnumerable()
                        select new WinningInqDTO
                        {
                            Id = a.Id,
                            WinningName = a.WinningName,
                            WinningUntiId = a.WinningUntiId,
                            WinningModel = a.WinningModel,
                            WinningTotalPrice = a.WinningTotalPrice,
                            WinningUitprice = a.WinningUitprice,
                            WinningQuantity = a.WinningQuantity,
                            IsDelete = a.IsDelete
                        };
            return new LayPageInfo<WinningInqDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0
            };
        }

        public int Delete(string Ids)
        {
            string sqlstr = "update WinningInq set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }
        /// <summary>
        /// 保存合同标的
        /// </summary>
        /// <param name="subjectMatter">合同标的</param>
        /// <returns>Id:->Hid:</returns>
        public bool AddSave(IList<WinningInqDTO> subs, int contId)
        {

            var arrayIds = subs.Select(a => a.Id).ToArray();
            var query = Db.Set<WinningInq>().AsNoTracking().Where(a => arrayIds.Contains(a.Id)).ToList();
            IList<WinningInq> subjectMatters = new List<WinningInq>();
            foreach (WinningInq item in query)
            {
                var dtomel = subs.Where(a => a.Id == item.Id).FirstOrDefault();
                var submt = _IMapper.Map<WinningInqDTO, WinningInq>(dtomel);// dtomel.ToModel<WinningInqDTO, WinningInq>();
                submt.Inqid = item.Inqid;
                submt.WinningName = submt.WinningName;
                submt.WinningUntiId = submt.WinningUntiId;
                submt.WinningModel = submt.WinningModel;
                submt.WinningTotalPrice = submt.WinningTotalPrice;
                submt.WinningUitprice = submt.WinningUitprice;
                submt.WinningQuantity = submt.WinningQuantity;
                submt.IsDelete = item.IsDelete;
                submt.SessionCurrUserId = item.SessionCurrUserId;
                submt.ModifyUserId = item.ModifyUserId;
                submt.GuidFileName = item.GuidFileName;
                submt.SourceFileName = item.SourceFileName;
                //submt.IsDelete = item.IsDelete; //item.IsDelete;
                subjectMatters.Add(submt);
            }
            //添加历史
            this.Update(subjectMatters);
            return true;
        }

    }
}
