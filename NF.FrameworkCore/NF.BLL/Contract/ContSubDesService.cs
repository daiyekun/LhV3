using NF.AutoMapper;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using NF.Common.Utility;
using NF.Common.Extend;
using NF.ViewModel.Models.Utility;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel;
using NF.BLL.Extend;
using AutoMapper;

namespace NF.BLL
{
    /// <summary>
    /// 交付描述
    /// </summary>
   public partial class ContSubDeService
    {
        private IMapper _IMapper;
        /// <summary>
        /// 标的交付
        /// </summary>
        /// <param name="info">交付描述信息</param>
        /// <param name="devDatas">交付行</param>
        /// <returns></returns>
        public bool BiaoDiJiaoFu(ContSubDesDTO info, IList<DevSubItem> devDatas, IMapper _IMapper)
        {
            
            
                IList<ContSubDe> contSubDes = new List<ContSubDe>();
                var savinfo = _IMapper.Map<ContSubDesDTO, ContSubDe>(info);// info.ToModel<ContSubDesDTO, ContSubDe>();
                foreach (var item in devDatas)
                {
                    savinfo.SubId = item.SubId;//标的ID
                    savinfo.CurrDevNumber = item.CurrNumber;//当前交付数量
                    savinfo.NotDevNumber = item.NotNumber;//档次交付以后剩余数量
                    contSubDes.Add(savinfo);
                }
                var subIds = devDatas.Select(a => a.SubId).ToList();
                var listsubs = Db.Set<ContSubjectMatter>().Where(a => subIds.Any(b => b == a.Id)).ToList();
                foreach (var sub in listsubs)
                {
                    var devInfo = contSubDes.Where(a => a.SubId == sub.Id).FirstOrDefault();
                    var currnum = devInfo == null ? 0 : devInfo.CurrDevNumber ?? 0;
                    sub.ComplateAmount = (sub.ComplateAmount ?? 0) + currnum;
                    sub.SjJfRq = devInfo == null ? null : devInfo.ActualDateTime;
                    sub.SubState =Convert.ToByte((devInfo.NotDevNumber??0)==0?2:1);//已经存在交付
                }
                this.Db.Set<ContSubDe>().AddRange(contSubDes);
                return SaveChanges() > 0;
            
           


        }

        /// <summary>
        /// 标的交付明细大列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<ContSubDesListDTO> GetMainList<s>(PageInfo<ContSubDe> pageInfo, Expression<Func<ContSubDe, bool>> whereLambda, Expression<Func<ContSubDe, s>> orderbyLambda, bool isAsc)
        {
            //var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var tempquery = Db.Set<ContSubDe>()
            .Include(a=>a.Sub).ThenInclude(a=>a.Cont).ThenInclude(a=>a.Comp)

                .AsTracking().Where<ContSubDe>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<ContSubDe>))
                tempquery = tempquery.Skip<ContSubDe>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<ContSubDe>(pageInfo.PageSize);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            SubId=a.SubId,
                            SubName= a.Sub==null?"":a.Sub.Name,//标的名称
                            Unit = a.Sub == null ? "" : a.Sub.Unit,
                            Amount = a.Sub == null ? 0 : a.Sub.Amount,
                            Price = a.Sub == null ? 0 : a.Sub.Price,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            ContName = (a.Sub != null && a.Sub.Cont != null) ? a.Sub.Cont.Name : "",
                            ContNo = (a.Sub != null && a.Sub.Cont != null) ? a.Sub.Cont.Code : "",
                            ContId = (a.Sub != null && a.Sub.Cont != null) ? a.Sub.Cont.Id : 0,
                            HtFzrId = (a.Sub != null && a.Sub.Cont != null) ? a.Sub.Cont.PrincipalUserId : 0,//合同负责人
                            HtJbjgId = (a.Sub != null && a.Sub.Cont != null) ? a.Sub.Cont.DeptId : -1,//经办机构
                            ContState = (a.Sub != null && a.Sub.Cont != null) ? a.Sub.Cont.ContState : 0,
                            CompName = (a.Sub != null && a.Sub.Cont != null&& a.Sub.Cont.Comp!=null) ? a.Sub.Cont.Comp.Name:"",//合同对方
                            CompId= (a.Sub != null && a.Sub.Cont != null && a.Sub.Cont.Comp != null)?a.Sub.Cont.Comp.Id:0,
                            ActDate=a.ActualDateTime,//实际交付日期
                            DevNumber=a.CurrDevNumber,//交付数量
                            DevDz=a.DeliverLocation,//地址
                            DevFsId=a.DeliverType,//交付方式
                            DevUId=a.DeliverUserId,//交付人
                            Bz1=a.Field1,//备注1
                            Bz2=a.Field2,//备注2
                            PlanDate=a.Sub!=null?a.Sub.PlanDateTime:null,//计划交付日期

                        };
            var list = query.ToList();
            var local = from a in query.AsEnumerable()
                        select new ContSubDesListDTO
                        {


                            Id = a.Id,
                            ContId = a.ContId,
                            CompId = a.CompId,
                            SubId = a.SubId,//标的ID
                            SubName = a.SubName,
                            ContName = a.ContName,
                            ContCode = a.ContNo,
                            ActDate = a.ActDate,
                            DevNumber = a.DevNumber,
                            DevMoneyThod = ((a.Price ?? 0) * (a.DevNumber ?? 0)).ThousandsSeparator(),
                            DevDz = a.DevDz,
                            DevFs =this.Db.GetRedisDataDictionaryValue(a.DevFsId ?? 0, DataDictionaryEnum.DevType),

                            //DataDicUtility.GetDicValueToRedis(a.DevFsId, DataDictionaryEnum.DevType),
                            DevUname = this.Db.GetRedisUserFieldValue(a.DevUId ?? -1),
                           // RedisValueUtility.GetUserShowName(a.DevUId ?? 0),
                            CompName=a.CompName,
                            ContStateDic = EmunUtility.GetDesc(typeof(ContractState), a.ContState),
                            HtFzr= this.Db.GetRedisUserFieldValue(a.HtFzrId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.HtFzrId ?? 0),
                            JbJg = this.Db.GetRedisDeptNameValue(a.HtJbjgId ?? -2),
                            //RedisValueUtility.GetDeptName(a.HtJbjgId ?? -2),
                            Unit=a.Unit,
                            DjThod=a.Price.ThousandsSeparator(),
                            Bz1=a.Bz1,
                            Bz2 = a.Bz2,
                            PlanDate=a.PlanDate,
                            ContState=a.ContState,


                        };
            return new LayPageInfo<ContSubDesListDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };

        }
    }
}
