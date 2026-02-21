using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NF.AutoMapper;
using NF.Common.Extend;
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
  public  partial class WinningQueService
    {
        private IMapper _IMapper;
        public LayPageInfo<WinningQueDTO> GetList<s>(PageInfo<WinningQue> pageInfo, Expression<Func<WinningQue, bool>> whereLambda, Expression<Func<WinningQue, s>> orderbyLambda, bool isAsc)
        {
            //var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var tempquery = Db.Set<WinningQue>().AsTracking().Where<WinningQue>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<WinningQue>))
                tempquery = tempquery.Skip<WinningQue>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<WinningQue>(pageInfo.PageSize);
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
                        select new WinningQueDTO
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
            return new LayPageInfo<WinningQueDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }

        public LayPageInfo<WinningQueDTO> GetListView<s>(PageInfo<WinningQue> pageInfo, Expression<Func<WinningQue, bool>> whereLambda, Expression<Func<WinningQue, s>> orderbyLambda, bool isAsc)
        {
            //var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var tempquery = Db.Set<WinningQue>().AsTracking().Where<WinningQue>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<WinningQue>))
                tempquery = tempquery.Skip<WinningQue>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<WinningQue>(pageInfo.PageSize);
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
                        select new WinningQueDTO
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
            return new LayPageInfo<WinningQueDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0
            };
        }

        public int Delete(string Ids)
        {
            string sqlstr = "update WinningQue set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }
        /// <summary>
        /// 保存合同标的
        /// </summary>
        /// <param name="subjectMatter">合同标的</param>
        /// <returns>Id:->Hid:</returns>
        public bool AddSave(IList<WinningQueDTO> subs, int contId)
        {

            var arrayIds = subs.Select(a => a.Id).ToArray();
            var query = Db.Set<WinningQue>().AsNoTracking().Where(a => arrayIds.Contains(a.Id)).ToList();
            IList<WinningQue> subjectMatters = new List<WinningQue>();
            foreach (WinningQue item in query)
            {
                var dtomel = subs.Where(a => a.Id == item.Id).FirstOrDefault();
                var submt = _IMapper.Map<WinningQueDTO, WinningQue>(dtomel);// dtomel.ToModel<WinningQueDTO, WinningQue>();
                submt.QueId = contId;//item.QueId;
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
