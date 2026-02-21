using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.EntityFrameworkCore;
using NF.BLL.Extend;
using NF.Common.Extend;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NF.BLL
{
   public partial  class ContractInfoService
    {

        /// <summary>
        /// 查看微信信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public ContractViewWxModel ShowWxViewMode(int Id)
        {
            var query = from a in _ContractInfoSet.AsNoTracking().Include(a => a.Comp).AsNoTracking()
                        .Include(a => a.Project).AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            OtherCode = a.OtherCode,
                            ContTypeId = a.ContTypeId,
                            IsFramework = a.IsFramework,
                            Ctype = a.Comp == null ? 0 : a.Comp.Ctype,

                            CompName = a.Comp == null ? "" : a.Comp.Name,
                            ProjName = a.Project == null ? "" : a.Project.Name,
                            AmountMoney = a.AmountMoney,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            SngnDateTime = a.SngnDateTime,
                            EffectiveDateTime = a.EffectiveDateTime,
                            PlanCompleteDateTime = a.PlanCompleteDateTime,
                            ActualCompleteDateTime = a.ActualCompleteDateTime,
                            DeptId = a.DeptId,
                            ContState = a.ContState,
                            MainDeptId = a.MainDeptId,
                            PrincipalUserId = a.PrincipalUserId,
                            WfItem = a.WfItem,//审批事项
                            HtXmnr=a.HtXmnr
                        };
            var local = from a in query.AsEnumerable()
                        select new ContractViewWxModel
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            OtherCode = a.OtherCode,
                            ComName = a.CompName,
                            ContClssName = this.Db.GetRedisDataDictionaryValue(a.ContTypeId ?? 0, DataDictionaryEnum.contractType),
                            //DataDicUtility.GetDicValueToRedis(a.ContTypeId, DataDictionaryEnum.contractType),//合同类别
                            MdeptName = this.Db.GetRedisDeptNameValue(a.MainDeptId ?? -2),
                            //RedisValueUtility.GetDeptName(a.MainDeptId ?? -2),
                            HtJe = a.AmountMoney.ThousandsSeparator(),
                            HtJeD = a.AmountMoney ?? 0,//合同金额
                            ProjName = a.ProjName,
                            Dname = this.Db.GetRedisDeptNameValue(a.DeptId ?? -2),
                            //RedisValueUtility.GetDeptName(a.DeptId ?? -2),
                            HtZt = EmunUtility.GetDesc(typeof(ContractState), a.ContState),
                            EfDate = a.EffectiveDateTime.ConvertToString(),
                            Sdate = a.SngnDateTime.ConvertToString(),
                            Pdate = a.PlanCompleteDateTime.ConvertToString(),
                            Fzr = this.Db.GetRedisUserFieldValue(a.PrincipalUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.PrincipalUserId ?? 0),
                            ContPro = EmunUtility.GetDesc(typeof(ContractProperty), a.IsFramework ?? 0),//合同属性
                            Cjr =this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId),//创建人
                            Cjsj = a.CreateDateTime.ConvertToString(),
                            WfItem = a.WfItem ?? -2,
                            ContAmThod = a.AmountMoney.ThousandsSeparator(),//合同金额千分位
                            CompName = a.CompName,
                            HtXmnr = a.HtXmnr

                        };
            var teminfo = local.FirstOrDefault();
            if (teminfo != null)
            {
                teminfo.HtWcJeThod = GetWxHtWcJe(teminfo.Id).ThousandsSeparator();//完成金额
                teminfo.HtWcBl = GetWxWcBl(teminfo.AmountMoney ?? 0, GetHtWcJe(teminfo.Id));//完成比例
                teminfo.PiaoKaunCha = (GetFpJe(teminfo.Id) - GetHtWcJe(teminfo.Id)).ThousandsSeparator();//票款差额
                teminfo.FaPiaoThod = GetWxFpJe(teminfo.Id).ThousandsSeparator();//发票金额
            }
            return teminfo;
            //var teminfo = local.FirstOrDefault();
            //return teminfo;





        }
        #region 计算字段方法
        /// <summary>
        /// 合同完成金额
        /// </summary>
        /// <param name="Id">当前合同ID</param>
        /// <returns></returns>
        private decimal GetWxHtWcJe(int Id)
        {
            var info = Db.Set<ContStatistic>().Where(a => a.ContId == Id).FirstOrDefault();
            if (info != null)
                return info.CompActAm ?? 0;
            return 0;

        }
        /// <summary>
        /// 合同完成金额
        /// </summary>
        /// <param name="Id">当前合同ID</param>
        /// <returns></returns>
        private decimal GetWxFpJe(int Id)
        {
            var info = Db.Set<ContStatistic>().Where(a => a.ContId == Id).FirstOrDefault();
            if (info != null)
                return info.CompInAm ?? 0;
            return 0;

        }
        /// <summary>
        /// 完成比例
        /// </summary>
        /// <returns></returns>
        private string GetWxWcBl(decimal htje, decimal wcje)
        {
            return ((htje == 0 || wcje == 0) ? 0 : (wcje / htje)).ConvertToPercent();


        }



        #endregion


        /// <summary>
        /// 查询列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">Where条件表达式</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        public LayPageInfo<ContractInfoListViewDTO> WXCountGetList<s>(PageInfo<ContractInfo> pageInfo, Expression<Func<ContractInfo, bool>> whereLambda,
             Expression<Func<ContractInfo, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = _ContractInfoSet
                .Include(a => a.Project)
                .Include(a => a.Comp)
                .Include(a => a.ContStatic)
                .Include(a => a.CreateUser)
                 .AsTracking()
                .Where<ContractInfo>(whereLambda.Compile()).AsQueryable();

            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            pageInfo.TotalCount = tempquery.Count();
            if (!(pageInfo is NoPageInfo<ContractInfo>))
            { //分页
                tempquery = tempquery.Skip<ContractInfo>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<ContractInfo>(pageInfo.PageSize);
            }

            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            OtherCode = a.OtherCode,
                            ContSourceId = a.ContSourceId,
                            ContTypeId = a.ContTypeId,
                            IsFramework = a.IsFramework,
                            ContDivision = a.ContDivision,
                            CompId = a.CompId,
                            CompName = a.Comp == null ? "" : a.Comp.Name,
                            BankName = a.Comp == null ? "" : a.Comp.BankName,
                            BankAccount = a.Comp == null ? "" : a.Comp.BankAccount,
                            ProjName = a.Project == null ? "" : a.Project.Name,
                            AmountMoney = a.AmountMoney,
                            CurrencyId = a.CurrencyId,
                            CurrencyRate = a.CurrencyRate,
                            EstimateAmount = a.EstimateAmount,
                            AdvanceAmount = a.AdvanceAmount,
                            StampTax = a.StampTax,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            SngnDateTime = a.SngnDateTime,
                            EffectiveDateTime = a.EffectiveDateTime,
                            PlanCompleteDateTime = a.PlanCompleteDateTime,
                            ActualCompleteDateTime = a.ActualCompleteDateTime,
                            DeptId = a.DeptId,
                            ProjectId = a.ProjectId,
                            ContState = a.ContState,
                            MainDeptId = a.MainDeptId,
                            Reserve1 = a.Reserve1,
                            Reserve2 = a.Reserve2,
                            PrincipalUserId = a.PrincipalUserId,
                            SumContId = a.SumContId,//总包合同ID
                            ModificationTimes = a.ModificationTimes,//变更次数
                            WfState = a.WfState,
                            WfCurrNodeName = a.WfCurrNodeName,
                            WfItem = a.WfItem,
                            CompInAm = a.ContStatic == null ? 0 : a.ContStatic.CompInAm,//已确认发票
                            CompActAm = a.ContStatic == null ? 0 : a.ContStatic.CompActAm,//已确实际资金
                            CompRatio = a.ContStatic == null ? 0 : a.ContStatic.CompRatio,//完成比例
                            BalaTick = a.ContStatic == null ? 0 : a.ContStatic.BalaTick,//票款差
                            // Zbid = a.Zbid,
                            //zb = a.Zb == null ? "" : a.Zb.Project.Name,
                            ////zb = a.Zb.Project.Name,
                            //// Xjid = a.Xjid,
                            //xj = a.Xj == null ? "" : a.Xj.ProjectNameNavigation.Name,
                            ////  Ytid = a.Ytid,
                            //yt = a.Yt == null ? "" : a.Yt.ProjectNameNavigation.Name,
                            // yt =a.Yt.ProjectNameNavigation.Name,
                            ContSingNo = a.ContSingNo,//签约人身份证号
                            FinanceType = a.FinanceType

                            // SumContName=a.SumCont.Name,

                        };
            var local = from a in query.AsEnumerable()
                        select new ContractInfoListViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,//名称
                            Code = a.Code,//编号
                            OtherCode = a.OtherCode,//合同对方编号
                            ContTypeId = a.ContTypeId,//合同类别ID
                            ContTypeName = this.Db.GetRedisDataDictionaryValue(a.ContTypeId ?? 0, DataDictionaryEnum.contractType),
                            //DataDicUtility.GetDicValueToRedis(a.ContTypeId ?? 0, DataDictionaryEnum.contractType),//合同类别
                            //合同来源
                            ContSName = this.Db.GetRedisDataDictionaryValue(a.ContSourceId ?? 0, DataDictionaryEnum.contSource),
                            //DataDicUtility.GetDicValueToRedis(a.ContSourceId ?? 0, DataDictionaryEnum.contSource),
                            CompId = a.CompId,
                            CompName = a.CompName,//合同对方
                            BankName = a.BankName,
                            BankAccount = a.BankAccount,
                            ProjName = a.ProjName,//项目名称
                            ContPro = EmunUtility.GetDesc(typeof(ContractProperty), (a.IsFramework ?? 0)),//合同属性
                            ContSum = (a.ContDivision ?? 0) > 0 ? "是" : "否",
                            AmountMoney = a.AmountMoney ?? 0,//合同金额
                            ContAmThod = a.AmountMoney.ThousandsSeparator(),//合同金额千分位
                            ContAmRmbThod = ((a.AmountMoney ?? 0) * (a.CurrencyRate ?? 1)).ThousandsSeparator(),//折合本币
                            ContAmRmb = (a.AmountMoney ?? 0) * (a.CurrencyRate ?? 1),//折合本币
                            CurrName = RedisValueUtility.GetCurrencyName(a.CurrencyId, fileName: "Name"),//币种
                            Rate = a.CurrencyRate ?? 1,//汇率
                            CurrencyId = a.CurrencyId,
                            EsAmountThod = (a.EstimateAmount ?? 0).ThousandsSeparator(),//预估金额
                            AdvAmountThod = (a.AdvanceAmount ?? 0).ThousandsSeparator(),//预收预付
                            StampTax = (a.StampTax ?? 0).ThousandsSeparator(),//千分位
                            CreateUserName = RedisValueUtility.GetUserShowName(a.CreateUserId), //创建人
                            CreateDateTime = a.CreateDateTime,//创建时间
                            SngDate = a.SngnDateTime,//签订时间
                            EfDate = a.EffectiveDateTime,//生效日期
                            PlanDate = a.PlanCompleteDateTime,//计划完成时间
                            ActDate = a.ActualCompleteDateTime,//实际完成时间
                            DeptName = this.Db.GetRedisDeptNameValue(a.DeptId ?? -2),
                            //RedisValueUtility.GetDeptName(a.DeptId ?? -2),//经办机构
                            DeptId = a.DeptId,//经办机构
                            MdeptName = this.Db.GetRedisDeptNameValue(a.MainDeptId ?? -2),
                            //RedisValueUtility.GetDeptName(a.MainDeptId ?? -2), //签约主体
                            MainDeptId = a.MainDeptId,
                            ContState = a.ContState,
                            ContStateDic = EmunUtility.GetDesc(typeof(ContractState), a.ContState),
                            Reserve1 = a.Reserve1,//备注1
                            Reserve2 = a.Reserve2,//备注2
                            ModificationTimes = a.ModificationTimes ?? 0,//变更次数
                            PrincUserName = this.Db.GetRedisUserFieldValue(a.PrincipalUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.PrincipalUserId ?? -1), //负责人
                            WfState = a.WfState,
                            WfCurrNodeName = a.WfCurrNodeName,
                            WfItemDic = FlowUtility.GetMessionDic(a.WfItem ?? -1, 0),
                            WfStateDic = EmunUtility.GetDesc(typeof(WfStateEnum), a.WfState ?? -1),
                            //ZbName =a.zb,// ZbName(a.Zbid),
                            // XjName =a.xj,// XjName(a.Xjid),
                            // YtName =a.yt,// YtName(a.Ytid),
                            ContSingNo = a.ContSingNo,//签约人身份证号
                            FinanceType = a.FinanceType,
                            CompInAmThod = a.CompInAm.ThousandsSeparator(),//已确认发票
                            CompActAmThod = a.CompActAm.ThousandsSeparator(),//已确实际资金
                            CompRatioStr = a.CompRatio.ConvertToPercent(),//完成比例
                            BalaTickThod = a.BalaTick.ThousandsSeparator(),//票款差
                            CompInAm = a.CompInAm ?? 0,//发票已确认
                            CompActAm = a.CompActAm ?? 0,//实际资金已确认
                        };
            return new LayPageInfo<ContractInfoListViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0
            };
        }
        /// <summary>
        ///根据微信账号查询用户信息
        /// </summary>
        /// <param name="Wxzh">微信账号</param>
        /// <returns></returns>
        public UserInfor Yhinfo(string Wxzh)
        {
            return Db.Set<UserInfor>().Where(a => a.IsDelete == 0 && a.WxCode == Wxzh).FirstOrDefault();

        }

        /// <summary>
        /// 到期合同到redis
        /// </summary>
        /// <returns></returns>
        public int DaoQqhtToRedisList()
        {
            var aherdMoth = 3;    //提取3个月
            var predicate = PredicateBuilder.True<ContractInfo>();
            predicate = predicate.And(p => p.PlanCompleteDateTime.HasValue&&DateTime.Now.AddMonths(aherdMoth) >= p.PlanCompleteDateTime.Value
            && p.ContState == (int)ContractState.Execution&&p.IsWxMsg!=1);
            var query0 = Db.Set<ContractInfo>().AsNoTracking()
                .Where(predicate.Compile());
            var query = from a in query0
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            No = a.Code,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            PlanCompleteDateTime = a.PlanCompleteDateTime,
                            FinanceType=a.FinanceType,
                           

                        };
            var local = from a in query.AsEnumerable()
                        select new DqoQiHt
                        {
                            Id=a.Id,
                            Name=a.Name,
                            No=a.No,
                            CreateDate=a.CreateDateTime,
                            CreateUserName= this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId),
                            PlanDate =a.PlanCompleteDateTime,
                            FinType=a.FinanceType,
                            CreateUserId=a.CreateUserId
                        };
            var list = local.ToList();
            var htIds = list.Select(a => a.Id).ToList();
            var listinsts = Db.Set<AppInst>().Where(a => a.ObjType == 3 && htIds.Contains(a.AppObjId)).ToList();
            var instIds = listinsts.Select(a => a.Id).ToList();
            var listNodes = Db.Set<AppInstNode>().Where(a => instIds.Contains(a.InstId ?? -1)).ToList();
            var listLines = Db.Set<AppInstNodeLine>().Where(a => instIds.Contains(a.InstId??-1)).ToList();
            var listuser = Db.Set<UserInfor>().Where(a => a.IsDelete != 1).Select(a => new { a.Id, a.WxCode }).ToList();
            StringBuilder strb = new StringBuilder();
            foreach (var ht in list)
            {
                var dqmsg = new DaoQiMsg();
                dqmsg.MsgCode = 0;//到期合同
                var listids = GetFirstNodeUser(ht.Id, listinsts, listNodes, listLines);
                listids.Add(ht.CreateUserId);
                var wxcodes= listuser.Where(a => listids.Contains(a.Id)).Select(a => a.WxCode).ToList();
                ht.WxCode = StringHelper.ArrayString2String2(wxcodes);
                dqmsg.DqHt = ht;
                RedisHelper.ListRightPush("DaoQiWxMsgList", dqmsg);
                strb.Append($"update ContractInfo set IsWxMsg=1,IsWXMsgDate='{DateTime.Now}' where Id={ht.Id};");






            }

            if (!string.IsNullOrEmpty(strb.ToString()))
            {
                ExecuteSqlCommand(strb.ToString());
            }

            return list.Count();




        }
        /// <summary>
        /// 获取合同第一个节点审批人员
        /// </summary>
        /// <param name="contId">合同ID</param>
        /// <param name="instIds">审批实例</param>
        /// <param name="instNodeLines">连线集合</param>
        /// <param name="instNodes">节点集合</param>
        /// <returns></returns>
        private IList<int> GetFirstNodeUser(int contId,IList<AppInst> instIds, IList<AppInstNode> instNodes, IList<AppInstNodeLine> instNodeLines)
        {
            IList<int> userIds = new List<int>();
            var instInfo = instIds.Where(a => a.AppObjId == contId).OrderByDescending(a => a.Id).FirstOrDefault();
            if (instInfo != null)
            {
                var node0 = instNodes.Where(a => a.InstId == instInfo.Id && a.Type == 0).FirstOrDefault();
                if (node0!=null)
                {
                    var line = instNodeLines
                        .Where(a => a.InstId == instInfo.Id && a.From == node0.NodeStrId).FirstOrDefault();
                    if (line != null)
                    {
                      var node1=  instNodes.Where(a => a.InstId == instInfo.Id && a.NodeStrId == line.To).FirstOrDefault();

                        if (node1 != null)
                        {
                            //var appgroupuser= Db.Set<AppGroupUser>().Where(a=>a.)
                            var option = Db.Set<AppInstOpin>().Where(a => a.InstId == instInfo.Id
                            && a.NodeId == node1.Id).FirstOrDefault();
                            userIds.Add(option.CreateUserId??0);

                        }

                    }

                }


            }

            return userIds;
        }

        /// <summary>
        /// 到期计划到redis
        /// </summary>
        /// <returns></returns>
        public int DaoQqJhToRedisList()
        {
            var aherdMoth = 2;    //提取3个月
            var predicate = PredicateBuilder.True<ContPlanFinance>();
            predicate = predicate.And(p =>(p.Cont!=null&&p.Cont.ContState == (int)ContractState.Execution)&& p.PlanCompleteDateTime!=null && DateTime.Now.AddMonths(aherdMoth) >= p.PlanCompleteDateTime.Value
            && p.IsWxMsg != 1);
            var query0 = Db.Set<ContPlanFinance>().Include(a=>a.Cont).AsNoTracking()
                .Where(predicate.Compile());
            var query = from a in query0
                        select new
                        {
                            Id = a.Id,
                            ContId=a.ContId,
                            Name = a.Name,
                            ContName = a.Cont != null ? a.Cont.Name : "",
                            ContNo = a.Cont != null ? a.Cont.Code : "",
                           
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            PlanDate = a.PlanCompleteDateTime,
                            FinanceType = a.Ftype,
                            AmountMoney=a.AmountMoney
                            

                        };
            var local = from a in query.AsEnumerable()
                        select new DqPlanFinceInfo
                        {
                            Id = a.Id,
                            Name = a.Name,
                            ContNo = a.ContNo,
                            ContName=a.ContName,
                            ContId = a.ContId??0,
                            // CreateDate = a.CreateDateTime,
                            // CreateUserName = RedisValueUtility.GetUserShowName(a.CreateUserId),
                            PlanDate = a.PlanDate,
                            FinType = a.FinanceType??0,
                            CreateUserId = a.CreateUserId,
                            MoneryThond=a.AmountMoney.ThousandsSeparator()
                        };
            var list = local.ToList();
            var htIds = list.Select(a => a.ContId).ToList();
            var listinsts = Db.Set<AppInst>().Where(a => a.ObjType == 3 && htIds.Contains(a.AppObjId)).ToList();
            var instIds = listinsts.Select(a => a.Id).ToList();
            var listNodes = Db.Set<AppInstNode>().Where(a => instIds.Contains(a.InstId ?? -1)).ToList();
            var listLines = Db.Set<AppInstNodeLine>().Where(a => instIds.Contains(a.InstId ?? -1)).ToList();
            var listuser = Db.Set<UserInfor>().Where(a => a.IsDelete != 1).Select(a => new { a.Id, a.WxCode }).ToList();
            StringBuilder strb = new StringBuilder();
            foreach (var ht in list)
            {
                var dqmsg = new DaoQiMsg();
                dqmsg.MsgCode = 1;//到期计划资金
                var listids = GetFirstNodeUser(ht.ContId, listinsts, listNodes, listLines);
                listids.Add(ht.CreateUserId);
                var wxcodes = listuser.Where(a => listids.Contains(a.Id)).Select(a => a.WxCode).ToList();
                ht.WxCode = StringHelper.ArrayString2String2(wxcodes);
                dqmsg.DqPlan = ht;
                RedisHelper.ListRightPush("DaoQiWxMsgList", dqmsg);
                strb.Append($"update ContPlanFinance set IsWxMsg=1,IsWXMsgDate='{DateTime.Now}' where Id={ht.Id};");

            }

            if (!string.IsNullOrEmpty(strb.ToString()))
            {
                ExecuteSqlCommand(strb.ToString());
            }

            return list.Count();

        }



        }
}
