using Microsoft.EntityFrameworkCore;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Finance.Enums;
using NF.ViewModel.Models.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NF.BLL
{
    /// <summary>
    /// 提醒
    /// </summary>
    public partial class RemindService
    {
        public LayPageInfo<RemindDTO> GetList<s>(PageInfo<Remind> pageInfo, Expression<Func<Remind, bool>> whereLambda, Expression<Func<Remind, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = _RemindSet.AsTracking().Where<Remind>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<Remind>))
            { //分页
                tempquery = tempquery.Skip<Remind>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<Remind>(pageInfo.PageSize);
            }

            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Item = a.Item,
                            CustomName = a.CustomName,
                            AheadDays = a.AheadDays,
                            DelayDays = a.DelayDays,
                            IsDelete = a.IsDelete,
                        };
            var local = from a in query.AsEnumerable()
                        select new RemindDTO
                        {
                            Id = a.Id,
                            Item = a.Item,
                            Name = a.Name,
                            CustomName = a.CustomName,
                            AheadDays = a.AheadDays ?? 0,
                            DelayDays = a.DelayDays ?? 0,
                            IsDelete = a.IsDelete,

                        };
            return new LayPageInfo<RemindDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };


        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids">删除ID</param>
        /// <returns>受影响行数</returns>
        public int Delete(string Ids)
        {
            string sqlstr = "update Remind set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }

        #region 提醒信息
        /// <summary>
        /// 当前登录人提醒信息实体
        /// </summary>
        /// <param name="mesgInfo">提醒对象</param>
        /// <param name="currUserId">当前登录人ID</param>
        /// <param name="currUserDeptId">当前登录人所在部门ID</param>
        /// <returns>提醒实体</returns>
        public ConMesgInfo GetConsoleReminder(ConMesgInfo mesgInfo, int currUserId, int currUserDeptId)
        {

            if (mesgInfo == null)
            {
                mesgInfo = new ConMesgInfo();
            }

            try
            {
                mesgInfo.DqjhskNum = GetDqjhNum("到期的计划收款", currUserId, currUserDeptId).ToString();
            }
            catch (Exception ex)
            {

                mesgInfo.DqjhskNum = "0";
                Log4netHelper.Error("Err-->mesgInfo.DqjhskNum:" + ex.Message);
            }
            try
            {
                mesgInfo.DqjhfkNum = GetDqjhNum("到期的计划付款", currUserId, currUserDeptId).ToString();
            }
            catch (Exception ex)
            {

                mesgInfo.DqjhfkNum = "0";
                Log4netHelper.Error("Err-->mesgInfo.DqjhfkNum:" + ex.Message);
            }
            try
            {
                mesgInfo.DclsjskNum = GetActualFinanceReceive("待处理的实际收款", currUserId, currUserDeptId).ToString();
            }
            catch (Exception ex)
            {

                mesgInfo.DclsjskNum = "0";
                Log4netHelper.Error("Err-->mesgInfo.DclsjskNum:" + ex.Message);
            }
            try
            {
                mesgInfo.DclsjfkNum = GetActualFinanceReceive("待处理的实际付款", currUserId, currUserDeptId).ToString();
            }
            catch (Exception ex)
            {

                mesgInfo.DclsjfkNum = "0";
                Log4netHelper.Error("Err-->mesgInfo.DclsjfkNum:" + ex.Message);
            }
            try
            {
                mesgInfo.DclspNum = GetInvoice("待处理的收票", currUserId, currUserDeptId).ToString();
            }
            catch (Exception ex)
            {

                mesgInfo.DclspNum = "0";
                Log4netHelper.Error("Err-->mesgInfo.DclspNum:" + ex.Message);
            }
            try
            {
                mesgInfo.DclkpNum = GetInvoice("待处理的开票", currUserId, currUserDeptId).ToString();
            }
            catch (Exception ex)
            {

                mesgInfo.DclkpNum = "0";
                Log4netHelper.Error("Err-->mesgInfo.DclkpNum:" + ex.Message);
            }
            try
            {
                mesgInfo.DqSkHtNum = GetDqhtNum("到期收款合同", currUserId, currUserDeptId).ToString();
            }
            catch (Exception ex)
            {
                mesgInfo.DqSkHtNum = "0";
                Log4netHelper.Error("Err-->mesgInfo.DqSkHtNum:" + ex.Message);
            }
            try
            {
                mesgInfo.DqFkHtNum = GetDqhtNum("到期付款合同", currUserId, currUserDeptId).ToString();
            }
            catch (Exception ex)
            {
                mesgInfo.DqFkHtNum = "0";
                Log4netHelper.Error("Err-->mesgInfo.DqFkHtNum:" + ex.Message);
            }
            try
            {
                mesgInfo.YiTongGuo = GetYiTongGuo("已通过的审批", currUserId, currUserDeptId).ToString();
            }
            catch (Exception ex)
            {
                mesgInfo.YiTongGuo = "0";
                Log4netHelper.Error("Err-->mesgInfo.YiTongGuo:" + ex.Message);
            }
            try
            {
                mesgInfo.Mydesc = GetMydesc("我的工作台", currUserId, currUserDeptId).ToString();
            }
            catch (Exception ex)
            {
                mesgInfo.Mydesc = "0";
                Log4netHelper.Error("Err-->mesgInfo.Mydesc:" + ex.Message);
            }
            try
            {
                mesgInfo.Gzdesc = GetGzdesc("跟踪工作台", currUserId, currUserDeptId).ToString();
            }
            catch (Exception ex)
            {
                mesgInfo.Gzdesc = "0";
                Log4netHelper.Error("Err-->mesgInfo.Gzdesc:" + ex.Message);
            }
            try
            {
                mesgInfo.Jdpdtx = GetJindu("进度评定", currUserId, currUserDeptId).ToString();
            }
            catch (Exception ex)
            {

                mesgInfo.Jdpdtx = "0";
                Log4netHelper.Error("Err-->mesgInfo.Jdpdtx:" + ex.Message);
            }
            return mesgInfo;

        }
        /// <summary>
        /// 我的工作台
        /// </summary>
        /// <param name="content"></param>
        /// <param name="currUserId"></param>
        /// <param name="currUserDeptId"></param>
        /// <returns></returns>
        public int GetMydesc(string content, int currUserId, int currUserDeptId)
        {
            var ids = Db.Set<ScheduleManagement>().Where(a => a.State == 1 && a.IsDelete == 0).Select(s => s.Id).ToList();
            var num = Db.Set<ScheduleList>().Where(a => ids.Contains(a.ScheduleId ?? 0)
            && ((a.Tixing == currUserId && a.Mystate == (int)DescEnum.State0)
            || (a.Designee == currUserId && (a.Mystate == (int)DescEnum.State2 || a.Mystate == (int)DescEnum.State1)))
            ).Count();
            //var num = Db.Set<ScheduleList>().Where(a => (id.Contains(a.ScheduleId ?? 0) || a.Mystate == 3) && a.IsDelete == 0 
            //&& (a.Designee == currUserId|| a.Tixing == currUserId) && a.Mystate != 4).Count();
            return num <= 0 ? 0 : num;
        }
        /// <summary>
        /// 跟踪工作台
        /// </summary>
        /// <param name="content"></param>
        /// <param name="currUserId"></param>
        /// <param name="currUserDeptId"></param>
        /// <returns></returns>
        public int GetGzdesc(string content, int currUserId, int currUserDeptId)
        {
            var id = Db.Set<ScheduleManagement>().Where(a => a.State == 1 && a.IsDelete == 0).Select(s => s.Id).ToList();
            // var num = Db.Set<ScheduleList>().Where(a => (id.Contains(a.ScheduleId ?? 0)||a.Mystate==3) && a.IsDelete == 0 &&
            //(a.Designee == currUserId || a.Stalker == currUserId || a.Tixing == currUserId) && a.Mystate != 4).Count();
            var num = Db.Set<ScheduleList>().Where(a => id.Contains(a.ScheduleId ?? 0) && a.IsDelete == 0 && a.Stalker == currUserId).Count();
            return num <= 0 ? 0 : num;
        }
        /// <summary>
        /// 已通过审批
        /// </summary>
        /// <returns></returns>
        private int GetYiTongGuo(string content, int currUserId, int currUserDeptId)
        {
            var predicate = GetWfIntanceExpression(content, currUserId);
            var filter = Db.Set<AppInst>().Where(predicate.Compile());
            var num = filter.Count();
            return num <= 0 ? 0 : num;
        }
        /// <summary>
        /// 到期合同
        /// </summary>
        /// <returns></returns>
        private int GetDqhtNum(string content, int currUserId, int currUserDeptId)
        {
            try
            {
                var predicate2 = PredicateBuilder.True<ContractInfo>();
                ISysPermissionModelService sysPermission = new SysPermissionModelService();
                var funcode = content == "到期收款合同" ? "querycollcontlist" : "querypaycontlist";
                predicate2 = predicate2.And(sysPermission.GetContractListPermissionExpression(funcode, currUserId, currUserDeptId));
                predicate2 = predicate2.And(GetContractExpression(content));
                var filter = Db.Set<ContractInfo>().Where(predicate2.Compile());
                var num = filter.Select(p => p.Id).Count();


                return num;
            }
            catch (Exception)
            {

                return 0;
            }


        }

        /// <summary>
        /// 到期计划收款
        /// </summary>
        /// <returns></returns>
        private int GetDqjhNum(string content, int currUserId, int currUserDeptId)
        {
            var predicate2 = PredicateBuilder.True<ContPlanFinance>();
            ISysPermissionModelService sysPermission = new SysPermissionModelService();
            var funcode = content == "到期的计划收款" ? "querycollcontview" : "querypaycontview";
            predicate2 = predicate2.And(sysPermission.GetFinanceListPermissionExpression(funcode, currUserId, currUserDeptId));
            predicate2 = predicate2.And(GetPlanFinanceExpression(content));
            var filter = Db.Set<ContPlanFinance>().Include(a => a.Cont)
               .ThenInclude(a => (a.Project)).Include(a => a.Cont).ThenInclude(a => a.Comp)
                .AsTracking().Where(a=>a.IsDelete==0);

            var sd = filter.Where(predicate2.Compile()).ToList();


                var count = sd.Count();

            //(from s in filter
            //         join m in Db.Set<PlanFinnCheck>() 
            //         on s.Id equals m.PlanFinanceId into G
            //         where (s.AmountMoney == 0 ? 100 : (((G.Count() > 0 ? G.Sum(p => p.ChkState == (byte)ActFinanceStateEnum.Confirmed ? p.AmountMoney : 0) : 0) / s.AmountMoney) * 100)) < 100
            //         select s.Id).Count();



            return count;


        }
        /// <summary>
        /// 待处理实际资金
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public int GetActualFinanceReceive(string content, int currUserId, int currUserDeptId)
        {

            try
            {
                var predicate2 = PredicateBuilder.True<ContActualFinance>();
                ISysPermissionModelService sysPermission = new SysPermissionModelService();
                var funcode = content == "待处理的实际收款" ? "querycollcontview" : "querypaycontview";
                predicate2 = predicate2.And(sysPermission.GetActFinanceListPermissionExpression(funcode, currUserId, currUserDeptId));  //权限
                predicate2 = predicate2.And(GetActualFinanceExpression(content));    //判断条件
                try
                {
                    return this.Db.Set<ContActualFinance>().Include(a => a.Cont).Where(predicate2.Compile()).AsQueryable().Select(a => a.Id).Count();

                }
                catch (Exception)
                {

                    return 0;
                }
               
            }
            catch (Exception)
            {

                return 0;
            }
        }

        /// <summary>
        /// 待处理的发票
        /// </summary>
        ///  <param name="content">设置项</param>
        /// <returns>数量，金额</returns>
        public int GetInvoice(string content, int currUserId, int currUserDeptId)
        {
            try
            {
                var predicate2 = PredicateBuilder.True<ContInvoice>();
                ISysPermissionModelService sysPermission = new SysPermissionModelService();
                var funcode = content == "待处理的收票" ? "querycollcontview" : "querypaycontview";
                predicate2 = predicate2.And(sysPermission.GetInvoiceListPermissionExpression(funcode, currUserId, currUserDeptId));  //权限
                predicate2 = predicate2.And(GetInvoiceExpression(content));    //判断条件



                var contlist = Db.Set<ContInvoice>().Include(a => a.Cont);
                var intlist = contlist.Where(predicate2.Compile()).Select(a => a.Id).Count();
                return intlist;
            }
            catch (Exception)
            {

                return 0;
            }
            //return dal.GetInvoice(predicate2);
        }
        /// <summary>
        /// 审批列表条件表达式
        /// </summary>
        /// <param name="content">设置项</param>
        /// <param name="userId">当前登录人</param>
        /// <returns>审批实例条件表达式</returns>
        public Expression<Func<AppInst, bool>> GetWfIntanceExpression(string content, int userId)
        {


            var reminfo = GetRemindByItem(content);
            var postponeDay = reminfo == null ? 0 : reminfo.DelayDays ?? 0;  //延后天数
            //var aherdDay = reminfo == null ? 0 : reminfo.AheadDays ?? 0;    //提前天数
            var predicate = PredicateBuilder.True<AppInst>();
            var predicate2 = PredicateBuilder.True<AppInst>();
            switch (content)
            {
                case "已通过的审批"://提醒：已通过的审批
                    {
                        //int[] idList =
                        //    Db.Set<AppInst>().Where(
                        //        p =>
                        //        p.StartUserId == userId && p.AppState == (int)AppInstEnum.AppState1 &&
                        //        p.CompleteDateTime.HasValue&&
                        //        Convert.ToDateTime(p.CompleteDateTime).AddDays(postponeDay) >= DateTime.Now).Select(
                        //            p => p.Id).ToArray();
                        predicate2 = predicate2.And(p => p.StartUserId == userId
                        && p.AppState == (int)AppInstEnum.AppState1
                        && (p.CompleteDateTime.HasValue
                        && Convert.ToDateTime(p.CompleteDateTime).AddDays(postponeDay) >= DateTime.Now));
                        int[] idList = Db.Set<AppInst>().Where(predicate2.Compile()).Select(a => a.Id).ToArray();
                        predicate = predicate.And(p => idList.Contains(p.Id));

                    }
                    break;
            }
            return predicate;

        }

        /// <summary>
        /// 取得查看合同的条件表达式
        /// </summary>
        /// <param name="content">设置项</param>
        /// <returns>合同表达式</returns>
        public Expression<Func<ContractInfo, bool>> GetContractExpression(string content)
        {


            var reminfo = GetRemindByItem(content);
            var postponeDay = reminfo == null ? 0 : reminfo.DelayDays ?? 0;  //延后天数
            var aherdDay = reminfo == null ? 0 : reminfo.AheadDays ?? 0;    //提前天数
            var predicate = PredicateBuilder.True<ContractInfo>();
            switch (content)
            {
                case "新签的收款合同":
                    {
                        var predicate1 = PredicateBuilder.True<ContractInfo>();


                        predicate1 = predicate1.And(p => p.FinanceType == 0);
                        predicate1 = predicate1.And(p => p.SngnDateTime.HasValue && p.SngnDateTime.Value.AddDays(postponeDay) >= DateTime.Now);
                        predicate1 = predicate1.And(p => p.ContState == (int)ContractState.Execution);
                        predicate = predicate.And(predicate1);
                    }
                    break;
                case "新签的付款合同":
                    {
                        var predicate1 = PredicateBuilder.True<ContractInfo>();
                        predicate1 = predicate1.And(p => p.FinanceType == 1);
                        predicate1 = predicate1.And(p => p.SngnDateTime.HasValue && p.SngnDateTime.Value.AddDays(postponeDay) >= DateTime.Now);

                        predicate1 = predicate1.And(p => p.ContState == (int)ContractState.Execution);
                        predicate = predicate.And(predicate1);
                    }
                    break;
                case "到期收款合同":
                    {
                        var predicate5 = PredicateBuilder.True<ContractInfo>();
                        predicate5 = predicate5.And(p => p.FinanceType == (int)FinceType.Put);
                        predicate5 = predicate5.And(p => p.ContState == (int)ContractState.Execution);
                        try
                        {
                            if (aherdDay > 0 && postponeDay <= 0)
                            {
                                predicate5 = predicate5.And(p => p.PlanCompleteDateTime.HasValue && DateTime.Now.AddDays(aherdDay) >= p.PlanCompleteDateTime.Value);
                                predicate5 = predicate5.And(p => p.PlanCompleteDateTime.HasValue && DateTime.Now.AddDays(0) <= p.PlanCompleteDateTime.Value);
                            }
                            else if (postponeDay > 0 && aherdDay <= 0)
                            {
                                predicate5 = predicate5.And(p => p.PlanCompleteDateTime.HasValue && p.PlanCompleteDateTime.Value.AddDays(postponeDay) >= DateTime.Now);
                            }
                            else
                            {
                                predicate5 = predicate5.And(p => p.PlanCompleteDateTime.HasValue && DateTime.Now.AddDays(aherdDay) >= p.PlanCompleteDateTime.Value);
                                predicate5 = predicate5.And(p => p.PlanCompleteDateTime.HasValue && p.PlanCompleteDateTime.Value.AddDays(postponeDay) >= DateTime.Now);
                            }
                        }
                        catch
                        {
                            predicate5 = predicate5.And(h => false);
                        }
                        predicate = predicate.And(predicate5);
                    }
                    break;

                case "到期付款合同":
                    {
                        var predicate5 = PredicateBuilder.True<ContractInfo>();
                        predicate5 = predicate5.And(p => p.FinanceType == (int)FinceType.pay);
                        predicate5 = predicate5.And(p => p.ContState == (int)ContractState.Execution);
                        try
                        {
                            if (aherdDay > 0 && postponeDay <= 0)
                            {
                                predicate5 = predicate5.And(p => p.PlanCompleteDateTime.HasValue && DateTime.Now.AddDays(aherdDay) >= p.PlanCompleteDateTime.Value);
                                predicate5 = predicate5.And(p => p.PlanCompleteDateTime.HasValue && DateTime.Now.AddDays(0) <= p.PlanCompleteDateTime.Value);
                            }
                            else if (postponeDay > 0 && aherdDay <= 0)
                            {
                                predicate5 = predicate5.And(p => p.PlanCompleteDateTime.HasValue && p.PlanCompleteDateTime.Value.AddDays(postponeDay) >= DateTime.Now);
                            }
                            else
                            {
                                predicate5 = predicate5.And(p => p.PlanCompleteDateTime.HasValue && DateTime.Now.AddDays(aherdDay) >= p.PlanCompleteDateTime.Value);
                                predicate5 = predicate5.And(p => p.PlanCompleteDateTime.HasValue && p.PlanCompleteDateTime.Value.AddDays(postponeDay) >= DateTime.Now);
                            }
                        }
                        catch
                        {
                            predicate5 = predicate5.And(h => false);
                        }
                        predicate = predicate.And(predicate5);
                    }
                    break;
            }
            return predicate;


        }

        /// <summary>
        /// 取得查看发票的条件表达式
        /// </summary>
        /// <param name="content">设置项</param>
        /// <returns>发票表达式</returns>
        public Expression<Func<ContInvoice, bool>> GetInvoiceExpression(string content)
        {


            var reminfo = GetRemindByItem(content);
            var postponeDay = reminfo == null ? 0 : reminfo.DelayDays ?? 0;  //延后天数
            var aherdDay = reminfo == null ? 0 : reminfo.AheadDays ?? 0;    //提前天数
            var predicate = PredicateBuilder.True<ContInvoice>();


            switch (content)
            {
                case "待处理的收票":
                case "待处理的开票":
                    {
                        var predicate2 = PredicateBuilder.True<ContInvoice>();
                        predicate2 = predicate2.And(p => p.Cont.ContState == (int)ContractState.Execution);
                        if (content == "待处理的收票")
                        {
                            predicate2 = predicate2.And(p => p.Cont.FinanceType == (byte)FinceType.pay);
                            predicate2 = predicate2.And(p => p.InState!=3);
                        }
                        else if (content == "待处理的开票")
                        {
                            predicate2 = predicate2.And(p => p.Cont.FinanceType == (byte)FinceType.Put);
                            predicate2 = predicate2.And(p => p.InState != 2);
                        }

                        if (aherdDay > 0 && postponeDay <= 0)
                        {
                            predicate2 = predicate2.And(p => DateTime.Now.AddDays(aherdDay) >= p.MakeOutDateTime);
                            predicate2 = predicate2.And(p => DateTime.Now.AddDays(0) <= p.MakeOutDateTime);
                        }
                        else if (postponeDay > 0 && aherdDay <= 0)
                        {
                            predicate2 = predicate2.And(p => p.MakeOutDateTime.Value.AddDays(postponeDay) >= DateTime.Now);
                        }
                        else
                        {
                            predicate2 = predicate2.And(p => DateTime.Now.AddDays(aherdDay) >= p.MakeOutDateTime);
                            predicate2 = predicate2.And(p => p.MakeOutDateTime.Value.AddDays(postponeDay) >= DateTime.Now);
                        }
                        predicate = predicate.And(predicate2);
                    }
                    break;
            }
            return predicate;

        }



        /// <summary>
        /// 获取计划资金提醒表达式
        /// </summary>
        /// <param name="content">提醒项</param>
        /// <returns>计划资金表达</returns>
        public Expression<Func<ContPlanFinance, bool>> GetPlanFinanceExpression(string content)
        {

            var reminfo = GetRemindByItem(content);
            var postponeDay = reminfo == null ? 0 : reminfo.DelayDays ?? 0;  //延后天数
            var aherdDay = reminfo == null ? 0 : reminfo.AheadDays ?? 0;    //提前天数
            var predicate = PredicateBuilder.True<ContPlanFinance>();
            switch (content)
            {
                case "到期的计划收款":
                case "到期的计划付款":
                    {
                        var predicate1 = PredicateBuilder.True<ContPlanFinance>();
                        predicate1 = predicate1.And(p => p.Cont!=null&&( p.Cont.ContState == (int)ContractState.Execution || p.Cont.ContState == (int)ContractState.Approve));
                        if (content == "到期的计划收款")
                        {
                            predicate1 = predicate1.And(p => p.Ftype == (byte)FinceType.Put);
                        }
                        else if (content == "到期的计划付款")
                        {
                            predicate1 = predicate1.And(p => p.Ftype == (byte)FinceType.pay);
                        }
                        if (aherdDay > 0 && postponeDay <= 0)
                        {
                            predicate1 = predicate1.And(p => DateTime.Now.AddDays(aherdDay) >= p.PlanCompleteDateTime.Value);
                            predicate1 = predicate1.And(p => DateTime.Now.AddDays(0) <= p.PlanCompleteDateTime.Value);
                        }
                        else if (postponeDay > 0 && aherdDay <= 0)
                        {
                            predicate1 = predicate1.And(p => p.PlanCompleteDateTime.Value.AddDays(postponeDay) >= DateTime.Now);
                        }
                        else
                        {

                            DateTime? dateTime;
                            dateTime = null;

                            predicate1 = predicate1.And(p => DateTime.Now.AddDays(aherdDay) >= (p.PlanCompleteDateTime == null ? dateTime : p.PlanCompleteDateTime.Value));
                            predicate1 = predicate1.And(p => (p.PlanCompleteDateTime == null ? dateTime : p.PlanCompleteDateTime.Value.AddDays(postponeDay)) >= DateTime.Now);
                        }
                        predicate = predicate.And(predicate1);
                    }
                    break;



            }
            return predicate;
        }

        /// <summary>
        /// 取得查看实际资金的条件表达式
        /// </summary>
        /// <param name="content">设置项</param>
        /// <returns>实际资金表达式</returns>
        public Expression<Func<ContActualFinance, bool>> GetActualFinanceExpression(string content)
        {
            var reminfo = GetRemindByItem(content);
            var postponeDay = reminfo == null ? 0 : reminfo.DelayDays ?? 0;  //延后天数
            var aherdDay = reminfo == null ? 0 : reminfo.AheadDays ?? 0;    //提前天数
            var predicate = PredicateBuilder.True<ContActualFinance>();
            switch (content)
            {
                case "待处理的实际收款":
                case "待处理的实际付款":
                    {
                        var predicate2 = PredicateBuilder.True<ContActualFinance>();
                        predicate2 = predicate2.And(p => p.Cont.ContState == (int)ContractState.Execution);
                        if (content == "待处理的实际收款")
                        {
                            predicate2 = predicate2.And(p => p.FinceType == (byte)FinceType.Put);
                        }
                        else if (content == "待处理的实际付款")
                        {
                            predicate2 = predicate2.And(p => p.FinceType == (byte)FinceType.pay);
                        }

                        predicate2 = predicate2.And(p => p.Astate != (byte)ActFinanceStateEnum.Confirmed);
                        predicate2 = predicate2.And(p => p.Astate != (byte)ActFinanceStateEnum.Uncommitted);
                        if (aherdDay > 0 && postponeDay <= 0)
                        {
                            predicate2 = predicate2.And(p => DateTime.Now.AddDays(aherdDay) >= p.ActualSettlementDate.Value);
                            predicate2 = predicate2.And(p => DateTime.Now.AddDays(0) <= p.ActualSettlementDate.Value);
                        }
                        else if (postponeDay > 0 && aherdDay <= 0)
                        {
                            predicate2 = predicate2.And(p => p.ActualSettlementDate.Value.AddDays(postponeDay) >= DateTime.Now);
                        }
                        else
                        {
                            predicate2 = predicate2.And(p => DateTime.Now.AddDays(aherdDay) >= p.ActualSettlementDate.Value);
                            predicate2 = predicate2.And(p => p.ActualSettlementDate.Value.AddDays(postponeDay) >= DateTime.Now);
                        }
                        predicate = predicate.And(predicate2);
                    }
                    break;

            }
            return predicate;


        }
        /// <summary>
        /// 根据提醒事项获取当前提示设置对象
        /// </summary>
        /// <param name="content">提醒事项内容</param>
        /// <returns>提醒设置对象</returns>
        private Remind GetRemindByItem(string content)
        {
            return _RemindSet.Where(a => a.Item == content).FirstOrDefault();
        }

        /// <summary>
        /// 已通过审批StalkerName
        /// </summary>
        /// <returns></returns>
        private int GetJindu(string content, int currUserId, int currUserDeptId)
        {
            var filter = Db.Set<ScheduleDetail>()
                .Where(p => p.ScheduleSerNavigation.Stalker == currUserId
                && p.State != 2 && p.IsDelete == 0);
            var num = filter.Count();
            return num <= 0 ? 0 : num;
        }
        #endregion
    }
}
