using Microsoft.EntityFrameworkCore;
using NF.Common.Extend;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.ViewModel.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NF.AutoMapper;
using NF.ViewModel.Models.Finance.Enums;
using NF.BLL.Extend;
using AutoMapper;

namespace NF.BLL
{
    /// <summary>
    /// 合同操作
    /// </summary>
    public partial  class ContractInfoService
    {
        IMapper _IMappers;
        /// <summary>
        /// 校验某一字段值是否已经存在
        /// </summary>
        /// <param name="fieldInfo">字段相关信息</param>
        /// <returns>True:存在/False不存在</returns>
        public bool CheckInputValExist(UniqueFieldInfo fieldInfo)
        {
            var predicateAnd = PredicateBuilder.True<ContractInfo>();
            //不等于fieldInfo.CurrId是排除修改的时候
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0 && a.Id != fieldInfo.Id);
            switch (fieldInfo.FieldName)
            {
                case "Name":
                    predicateAnd = predicateAnd.And(a => a.Name == fieldInfo.FieldValue);
                    break;
                case "Code":
                    predicateAnd = predicateAnd.And(a => a.Code == fieldInfo.FieldValue);
                    break;

            }
            return GetQueryable(predicateAnd).AsNoTracking().Any();

        }
        /// <summary>
        /// 保存合同信息
        /// </summary>
        /// <param name="contractInfo">合同信息</param>
        /// <param name="contractInfoHistory">合同历史信息</param>
        /// <returns>Id:合同ID、Hid:历史ID</returns>
        public Dictionary<string, int> AddSave(ContractInfo contractInfo, ContractInfoHistory contractInfoHistory, IMapper _IMapper)
        {
            Test(_IMapper);
            contractInfo.ModificationTimes = 0;//变更次数
            contractInfoHistory.ModificationTimes = 0;//默认值
            var inof = Add(contractInfo);
            CreateHistroy(contractInfoHistory, inof);
            return ResultContIds(contractInfo, contractInfoHistory);
        }




       public List<CounLc> AddSave02(ContractInfo contractInfo, ContractInfoHistory contractInfoHistory, IMapper _IMapper)
        {
            Test(_IMapper);
            contractInfo.ModificationTimes = 0;//变更次数
            contractInfoHistory.ModificationTimes = 0;//默认值
            var inof = Add(contractInfo);
            CreateHistroy(contractInfoHistory, inof);
            List<ContractInfo> h = new List<ContractInfo>();
            CounLc r = new CounLc();
            h.Add(contractInfo);
            foreach (var item in h)
            {
                r.Id = item.Id;
                r.DeptId = item.DeptId;
                r.ContTypeId = item.ContTypeId;
                r.Name = item.Name;
                r.Code = item.Code;
                r.AmountMoney = item.AmountMoney;
            }
            List<CounLc> f = new List<CounLc>();
            f.Add(r);
            return f;
        }
        /// <summary>
        /// 创建合同历史数据及修改
        /// </summary>
        /// <param name="contractInfoHistory">历史合同对象</param>
        /// <param name="inof">当前合同对方</param>
        private void CreateHistroy(ContractInfoHistory contractInfoHistory, ContractInfo inof)
        {
            EventUtility eventUtility = new EventUtility();
            eventUtility.ContHistoryEvent += CreateContHistroy;
            eventUtility.ContHistoryEvent += UpdateItems;
            eventUtility.ContHistoryEvent += CreateContHistoryData;
            eventUtility.ExceContHistoryEvent(inof, contractInfoHistory);
        }

        /// <summary>
        /// 创建合同历史
        /// </summary>
        /// <param name="contractInfoHistory">合同历史</param>
        /// <param name="inof">合同对象</param>
        private void CreateContHistroy(ContractInfo inof, ContractInfoHistory contractInfoHistory)
        {
            contractInfoHistory.ContId = inof.Id;
            contractInfoHistory.ModifyDateTime = DateTime.Now;
            contractInfoHistory.ModifyUserId = inof.ModifyUserId;
            contractInfoHistory.ContId = inof.Id;
            Db.Set<ContractInfoHistory>().Add(contractInfoHistory);
            Db.SaveChanges();
        }
        #region 创建和修改标签历史数据
        /// <summary>
        /// 变更创建合同历史
        /// </summary>
        /// <param name="contId">合同ID</param>
        /// <param name="contHisId">合同历史ID</param>
        private void CreateContHistoryData(ContractInfo contractInfo, ContractInfoHistory contractInfoHistory)
        {
            //计划资金
            var listplanFinances = Db.Set<ContPlanFinance>().AsNoTracking().Where(a => a.ContId == contractInfo.Id).ToList();
            foreach (var plance in listplanFinances)
            {
                var infoHi = _IMappers.Map<ContPlanFinance, ContPlanFinanceHistory>(plance);// plance.ToModel<ContPlanFinance, ContPlanFinanceHistory>();
                infoHi.PlanFinanceId = plance.Id;
                infoHi.ContHisId = contractInfoHistory.Id;
                Db.Set<ContPlanFinanceHistory>().Add(infoHi);
            }
            //标的历史
            var listSubmatter = Db.Set<ContSubjectMatter>().AsNoTracking().Where(a => a.ContId == contractInfo.Id).ToList();
            foreach (var subjectMatter in listSubmatter)
            {
                var infoHi = _IMappers.Map<ContSubjectMatter, ContSubjectMatterHistory>(subjectMatter);// subjectMatter.ToModel<ContSubjectMatter, ContSubjectMatterHistory>();
                infoHi.SubjId = subjectMatter.Id;
                infoHi.ContHisId = contractInfoHistory.Id;
                Db.Set<ContSubjectMatterHistory>().Add(infoHi);
            }
            //合同文本
            var listconttext = Db.Set<ContText>().AsNoTracking().Where(a => a.ContId == contractInfo.Id).ToList();
            foreach (var contText in listconttext)
            {
                var infoHi = _IMappers.Map<ContText, ContTextHistory>(contText);// contText.ToModel<ContText, ContTextHistory>();
                infoHi.ContTxtId = contText.Id;
                infoHi.ContHisId = contractInfoHistory.Id;
                Db.Set<ContTextHistory>().Add(infoHi);
            }
            Db.SaveChanges();

        }
        /// <summary>
        /// 创建和修改历史数据
        /// </summary>
        /// <param name="contId">合同ID</param>
        /// <param name="contHisId">合同历史ID</param>
        private void UpdateContHistoryData(ContractInfo contractInfo, ContractInfoHistory contractInfoHistory)
        {
            
            var listplanFinances = Db.Set<ContPlanFinance>().AsNoTracking().Where(a => a.ContId == contractInfo.Id).ToList();
            var listhisplanfinances = Db.Set<ContPlanFinanceHistory>().AsNoTracking().Where(a => a.ContId == contractInfo.Id).ToList();
            foreach (var plance in listplanFinances)
            {
                var hiinfo = _IMappers.Map<ContPlanFinance, ContPlanFinanceHistory>(plance);//  plance.ToModel<ContPlanFinance, ContPlanFinanceHistory>();
                ContPlanFinanceHistory se=new ContPlanFinanceHistory();
                se.ActAmountMoney= plance.ActAmountMoney;
                se.AmountMoney = plance.AmountMoney;

                 var hisInfo = listhisplanfinances.Where(a => a.PlanFinanceId == plance.Id).OrderByDescending(a => a.Id).FirstOrDefault();
                if (hisInfo != null)
                {
                    hiinfo.Id = hisInfo.Id;
                    hiinfo.PlanFinanceId = plance.Id;
                    hiinfo.ContHisId = contractInfoHistory.Id;
                    Db.Entry<ContPlanFinanceHistory>(hiinfo).State = EntityState.Modified;
                }
                else
                {//如果没有就新建
                    hiinfo.PlanFinanceId = plance.Id;
                    hiinfo.ContHisId = contractInfoHistory.Id;
                    Db.Set<ContPlanFinanceHistory>().Add(hiinfo);

                }
            }

            Db.SaveChanges();
        }
        #endregion

        /// <summary>
        /// 合同历史相关信息
        /// </summary>
        private void CreateContHistoryRedis(int contId, int contHisId)
        {
            MappContToHistory contToHistory = new MappContToHistory
            {
                ContId = contId,
                ContHisId = contHisId

            };
            RedisHelper.ListRightPush(StaticData.AddContHistory, contToHistory);
        }
        /// <summary>
        /// 修改合同信息
        /// </summary>
        /// <param name="contractInfo">合同信息</param>
        /// <param name="contractInfoHistory">合同历史信息</param>
        /// <returns>Id:合同ID、Hid:历史ID</returns>
        public Dictionary<string, int> UpdateSave(ContractInfo contractInfo, ContractInfoHistory contractInfoHistory, IMapper _IMapper)
        {
            var d = _IMapper;
            Test(d);
            var inof = Update(contractInfo);
            //保存历史表
            EventUtility eventUtility = new EventUtility();
            eventUtility.ContHistoryEvent += UpdateContHisttory;
            eventUtility.ContHistoryEvent += UpdateItems;
            eventUtility.ContHistoryEvent += UpdateContHistoryData;
            eventUtility.ExceContHistoryEvent(contractInfo, contractInfoHistory);
            return ResultContIds(contractInfo, contractInfoHistory);
        }

        public void Test(IMapper _IMapper) 
        {
            _IMappers = _IMapper;
        } 
        /// <summary>
        /// 修该合同提交流程
        /// </summary>
        /// <param name="contractInfo"></param>
        /// <param name="contractInfoHistory"></param>
        /// <returns></returns>
        public List<CounLc> UpdateSave01(ContractInfo contractInfo, ContractInfoHistory contractInfoHistory,IMapper _IMapper)
        {
            Test(_IMapper);
            var inof = Update(contractInfo);
            //保存历史表
            EventUtility eventUtility = new EventUtility();
            eventUtility.ContHistoryEvent += UpdateContHisttory;
            eventUtility.ContHistoryEvent += UpdateItems;
            eventUtility.ContHistoryEvent += UpdateContHistoryData;
            eventUtility.ExceContHistoryEvent(contractInfo, contractInfoHistory);
            List<ContractInfo> h = new List<ContractInfo>();
            h.Add(contractInfo);
            CounLc r = new CounLc();
            foreach (var item in h)
            {
                r.Id = item.Id;
                r.DeptId = item.DeptId;
                r.ContTypeId = item.ContTypeId;
                r.Name = item.Name;
                r.Code = item.Code;
                r.AmountMoney = item.AmountMoney;
            }
            List<CounLc> f = new List<CounLc>();
            f.Add(r);
            return f;
           
        }
        /// <summary>
        /// 返回合同ID和历史合同ID
        /// </summary>
        /// <param name="contractInfo">合同</param>
        /// <param name="contractInfoHistory">历史合同</param>
        /// <returns></returns>
        private Dictionary<string, int> ResultContIds(ContractInfo contractInfo, ContractInfoHistory contractInfoHistory)
        {
            var dic = new Dictionary<string, int>();
            dic.Add("Id", contractInfo.Id);
            dic.Add("Hid", contractInfoHistory.Id);
            return dic;
        }
        /// <summary>
        /// 修改合同页提交流程返回值
        /// </summary>
        /// <param name="contractInfo"></param>
        /// <param name="contractInfoHistory"></param>
        /// <returns></returns>
        private List<ContractInfo> ResultContIds01(ContractInfo contractInfo, ContractInfoHistory contractInfoHistory)
        {
            List<ContractInfo> h = new List<ContractInfo>();
            h.Add(contractInfo);
            return h;
        }
        /// <summary>
        /// 变更
        /// </summary>
        /// <param name="contractInfo">合同信息</param>
        /// <param name="contractInfoHistory">合同历史信息</param>
        /// <returns>Id:合同ID、Hid:历史ID</returns>
        public Dictionary<string, int> ChangeSave(ContractInfo contractInfo, ContractInfoHistory contractInfoHistory, IMapper _IMapper)
        {
            contractInfo.ContState = 0; //未执行
            var inof = Update(contractInfo);
            //保存历史表
            contractInfoHistory.ModificationTimes = contractInfo.ModificationTimes;//变更次数
            CreateHistroy(contractInfoHistory, contractInfo);
            return ResultContIds(contractInfo, contractInfoHistory);
        }
        /// <summary>
        /// 修改历史
        /// </summary>
        /// <param name="infoHistory">修改历史合同</param>
        /// <returns></returns>
        private void UpdateContHisttory(ContractInfo contractInfo, ContractInfoHistory infoHistory)
        {
            infoHistory.ContId = contractInfo.Id;
            infoHistory.Id = contractInfo.ContHid ?? 0;
            Db.Entry<ContractInfoHistory>(infoHistory).State = EntityState.Modified;
            Db.SaveChanges();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">Where条件表达式</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        public LayPageInfo<ContractInfoListViewDTO> GetList<s>(PageInfo<ContractInfo> pageInfo, Expression<Func<ContractInfo, bool>> whereLambda,
             Expression<Func<ContractInfo, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = _ContractInfoSet
                .Include(a => a.Project)
                .Include(a => a.Comp)
                .Include(a => a.ContStatic)
                .Include(a => a.CreateUser)
                 .AsTracking()
                .Where<ContractInfo>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
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
                            FinanceType = a.FinanceType,
                            Dqjdspr = a.Dqjdspr

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
                            ContTypeName =this.Db.GetRedisDataDictionaryValue(a.ContTypeId ?? 0, DataDictionaryEnum.contractType) , //  DataDicUtility.GetDicValueToRedis(a.ContTypeId ?? 0, DataDictionaryEnum.contractType),//合同类别
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
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId), //RedisValueUtility.GetUserShowName(a.CreateUserId), //创建人
                            CreateDateTime = a.CreateDateTime,//创建时间
                            SngDate = a.SngnDateTime,//签订时间
                            EfDate = a.EffectiveDateTime,//生效日期
                            PlanDate = a.PlanCompleteDateTime,//计划完成时间
                            ActDate = a.ActualCompleteDateTime,//实际完成时间
                            DeptName =this.Db.GetRedisDeptNameValue(a.DeptId ?? -2),//RedisValueUtility.GetDeptName(a.DeptId ?? -2),//经办机构
                            DeptId = a.DeptId,//经办机构
                            MdeptName = this.Db.GetRedisDeptNameValue(a.MainDeptId ?? -2),// RedisValueUtility.GetDeptName(a.MainDeptId ?? -2), //签约主体
                            MainDeptId = a.MainDeptId,
                            ContState = a.ContState,
                            ContStateDic = EmunUtility.GetDesc(typeof(ContractState), a.ContState),
                            Reserve1 = a.Reserve1,//备注1
                            Reserve2 = a.Reserve2,//备注2
                            ModificationTimes = a.ModificationTimes ?? 0,//变更次数
                            PrincUserName = this.Db.GetRedisUserFieldValue(a.PrincipalUserId ?? -1),//RedisValueUtility.GetUserShowName(a.PrincipalUserId ?? -1), //负责人
                            WfState = a.WfState,
                            WfCurrNodeName = a.WfCurrNodeName,
                            WfItemDic = FlowUtility.GetMessionDic(a.WfItem ?? -1, 3),
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
                            Dqjdspr = a.Dqjdspr//当前节点审批人
                        };
            return new LayPageInfo<ContractInfoListViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0
            };
        }

        public string ZbName(int? id) {
            var zbname = "";
            try
            {
                zbname = this.Db.Set<TenderInfor>().Include(a => a.Project).AsEnumerable().Where(a => a.Id == id).FirstOrDefault().Project.Name;
            }
            catch (Exception)
            {

                return zbname;
            }


            return zbname;
        }
        public string XjName(int? id)
        { var zbname = "";
            try
            {
                zbname = this.Db.Set<Inquiry>().Include(a => a.ProjectNameNavigation).AsEnumerable().Where(a => a.Id == id).FirstOrDefault().ProjectNameNavigation.Name;
                //zbname = this.Db.Set<Inquiry>().AsEnumerable().Where(a => a.Id == id).FirstOrDefault().ProjectNameNavigation.Name;
            }
            catch (Exception)
            {

                return zbname;
            }

            return zbname;
        }
        public string YtName(int? id)
        {
            var zbname = "";
            try
            {
                zbname = this.Db.Set<Questioning>().Include(a => a.ProjectNameNavigation).AsEnumerable().Where(a => a.Id == id).FirstOrDefault().ProjectNameNavigation.Name;
                // zbname = this.Db.Set<Questioning>().AsEnumerable().Where(a => a.Id == id).FirstOrDefault().ProjectNameNavigation.Name;
            }
            catch (Exception)
            {

                return zbname;
            }

            return zbname;
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">需要删除的Ids集合</param>
        /// <returns>受影响行数</returns>
        public int Delete(string Ids, int i)
        {
            StringBuilder strsql = new StringBuilder();


            if (i == 0)
            {
                strsql.Append($"update ContractInfo set IsDelete = 1 where Id in (" + Ids + ")");
            }
            else if (i == 1)
            {
                strsql.Append($"update ContractInfo set IsDelete = 1 where Id in (" + Ids + ");");
                //strsql.Append($"update ContractInfoHistory set IsDelete = 1 where ContId in (" + Ids + ");");
                //strsql.Append($"update ContInvoice set IsDelete = 1 where ContId in (" + Ids + ");");//发票
                //strsql.Append($"update ContActualFinance  set IsDelete = 1 where ContId in (" + Ids + ");");//是实际资金
                //strsql.Append($"update ContractInfoHistory set IsDelete = 1 where ContId in (" + Ids + ");");
                //strsql.Append($"update ContDescription set IsDelete = 1  where ContId in (" + Ids + ");");//合同备忘
                //strsql.Append($"update ContPlanFinance set IsDelete = 1  where ContId in (" + Ids + ");");//计划资金
                //strsql.Append($"update ContPlanFinanceHistory set IsDelete = 1  where ContId in (" + Ids + ");");//计划资金历史
                //strsql.Append($"update ContAttachment set IsDelete = 1  where ContId in (" + Ids + ");");//附件
                //strsql.Append($"update ContText set IsDelete = 1 where  ContId in (" + Ids + ");");//合同文本
                //strsql.Append($"update ContTextHistory set IsDelete = 1  where ContId in (" + Ids + ");");//合同文本历史
                //strsql.Append($"update ContSubjectMatter set IsDelete = 1  where  ContId in (" + Ids + ");");
                //strsql.Append($"update ContSubjectMatterHistory set IsDelete = 1   where ContId in (" + Ids + ") or ContHisId in (" + Ids + ");");
                //    // strsql.Append($"delete ContConsult  where ContId={-currUserId};");//合同查阅人

            }

            return ExecuteSqlCommand(strsql.ToString());
        }


        public DELETElist GetIsFpt(string Ids)
        {
            DELETElist u = new DELETElist();

            var listIds = StringHelper.String2ArrayInt(Ids);
            var nums = Db.Set<ContInvoice>().Where(a => listIds.Contains(a.ContId ?? 0) && a.IsDelete == 0).Count();
            u.Num = nums;
            u.DateName = "发票";
            return u;
        }


        public DELETElist GetIsSjzj(string Ids)
        {
            DELETElist u = new DELETElist();

            var listIds = StringHelper.String2ArrayInt(Ids);
            var nums = Db.Set<ContActualFinance>().Where(a => listIds.Contains(a.ContId ?? 0) && a.IsDelete == 0).Count();
            u.Num = nums;
            u.DateName = "实际资金";
            return u;
        }


        /// <summary>
        /// 查看信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public ContractInfoViewDTO ShowView(int Id)
        {
            var query = from a in _ContractInfoSet.Include(a => a.Comp)
                        .Include(a => a.Project).Include(a => a.CompId3Navigation)
                        .Include(a => a.Zb).Include(a => a.Xj).Include(a => a.Yt)
                        .Include(a => a.CompId4Navigation).Include(a => a.SumCont)
                        .AsNoTracking()
                        where a.Id == Id
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
                            Ctype = a.Comp == null ? 0 : a.Comp.Ctype,
                            CompName = a.Comp == null ? "" : a.Comp.Name,
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
                            Comp3Name = a.CompId3Navigation == null ? "" : a.CompId3Navigation.Name,
                            Comp4Name = a.CompId4Navigation == null ? "" : a.CompId4Navigation.Name,
                            CompId3 = a.CompId3,
                            CompId4 = a.CompId4,
                            PrincipalUserId = a.PrincipalUserId,
                            FinanceTerms = a.FinanceTerms,
                            PerformanceDateTime = a.PerformanceDateTime,
                            SumContName = a.SumCont == null ? "" : a.SumCont.Name,//总包合同
                            SumContId = a.SumContId,
                            //htwcje = GetHtWcJe(a.Id),//实际资金已确认
                            //fpje=GetFpJe(a.Id),//发票已确认金额
                            // Zbid = a.Zbid,
                            zb = a.Zb.Project.Name,
                            // Xjid = a.Xjid,
                            xj = a.Xj.ProjectNameNavigation.Name,
                            //  Ytid = a.Ytid,
                            yt = a.Yt.ProjectNameNavigation.Name,
                            ContSingNo = a.ContSingNo,//签约人身份证号
                            FinanceType = a.FinanceType,
                            CountField = a.CountField
                        };
            var local = from a in query.AsEnumerable()
                        select new ContractInfoViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            OtherCode = a.OtherCode,
                            ContSourceId = a.ContSourceId,
                            ContTypeId = a.ContTypeId,
                            IsFramework = a.IsFramework,
                            //如果不是String.修改就得手动绑定Radio
                            ContDivision = (a.ContDivision ?? 0).ToString(),
                            CompId = a.CompId,
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
                            //合同类别
                            ContTypeName = this.Db.GetRedisDataDictionaryValue(a.ContTypeId ?? 0, DataDictionaryEnum.contractType),// DataDicUtility.GetDicValueToRedis(a.ContTypeId, DataDictionaryEnum.contractType),//合同类别
                            //合同来源
                            ContSName = this.Db.GetRedisDataDictionaryValue(a.ContSourceId ?? 0, DataDictionaryEnum.contSource),// DataDicUtility.GetDicValueToRedis(a.ContSourceId, DataDictionaryEnum.contSource),
                            CompName = a.CompName,//合同对方
                            ProjName = a.ProjName,//项目名称
                            ContPro = EmunUtility.GetDesc(typeof(ContractProperty), a.IsFramework ?? 0),//合同属性
                            ContSum = a.ContDivision > 0 ? "是" : "否",
                            ContAmThod = a.AmountMoney.ThousandsSeparator(),//合同金额千分位
                            CurrencyName =  RedisValueUtility.GetCurrencyName(a.CurrencyId, fileName: "Name"), ///币种
                            Rate = a.CurrencyRate ?? 1,//汇率
                            EsAmountThod = a.EstimateAmount.ThousandsSeparator(),//预估金额
                            AdvAmountThod = a.AdvanceAmount.ThousandsSeparator(),//预收预付
                            StampTaxThod = a.StampTax.ThousandsSeparator(),//千分位
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId),//创建人
                            PrincipalUserName = this.Db.GetRedisUserFieldValue(a.PrincipalUserId ?? -1),
                           // RedisValueUtility.GetUserShowName(a.PrincipalUserId ?? 0),//负责人
                            DeptName = this.Db.GetRedisDeptNameValue(a.DeptId ?? -2),// RedisValueUtility.GetDeptName(a.DeptId ?? -2),
                            MdeptName = this.Db.GetRedisDeptNameValue(a.MainDeptId ?? -2),// RedisValueUtility.GetDeptName(a.MainDeptId ?? -2),
                            StateDic = EmunUtility.GetDesc(typeof(ContractState), a.ContState),
                            Comp3Name = a.Comp3Name,
                            Comp4Name = a.Comp4Name,
                            CompId3 = a.CompId3,
                            CompId4 = a.CompId4,
                            FinanceTerms = a.FinanceTerms,//资金条款
                            PerformanceDateTime = a.PerformanceDateTime,
                            SumContName = a.SumContName,
                            SumContId = a.SumContId,//总包ID。修改时绑定
                                                    //HtWcJeThod = GetHtWcJe(a.Id).ThousandsSeparator(),//完成金额
                                                    //HtWcBl = GetWcBl(a.AmountMoney ?? 0, GetHtWcJe(a.Id)),//完成比例
                                                    //PiaoKaunCha = (GetFpJe(a.Id) - GetHtWcJe(a.Id)).ThousandsSeparator(),//票款差额
                                                    //FaPiaoThod = GetFpJe(a.Id).ThousandsSeparator()//发票金额
                            ZbName = a.zb,// ZbName(a.Zbid),
                            XjName = a.xj,// XjName(a.Xjid),
                            YtName = a.yt,// YtName(a.Ytid),
                            ContSingNo = a.ContSingNo,//签约人身份证号
                            FinanceType = a.FinanceType,
                            Ctype = a.Ctype,// 合同对方类型id
                         //  CustomFields = Zh(a.CountField)
                              CustomFields = Zhs (a.Id)

                        };

            var teminfo = local.FirstOrDefault();
            if (teminfo != null)
            {
                teminfo.HtWcJeThod = GetHtWcJe(teminfo.Id).ThousandsSeparator();//完成金额
                teminfo.HtWcBl = GetWcBl(teminfo.AmountMoney ?? 0, GetHtWcJe(teminfo.Id));//完成比例
                teminfo.PiaoKaunCha = (GetFpJe(teminfo.Id) - GetHtWcJe(teminfo.Id)).ThousandsSeparator();//票款差额
                teminfo.FaPiaoThod = GetFpJe(teminfo.Id).ThousandsSeparator();//发票金额
            }
            return teminfo;
        }

        /// <summary>
        /// 查看信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public ContractInfoViewDTO CopShowView(int Id)
        {
            var query = from a in _ContractInfoSet.Include(a => a.Comp)
                        .Include(a => a.Project).Include(a => a.CompId3Navigation)
                        .Include(a => a.Zb).Include(a => a.Xj).Include(a => a.Yt)
                        .Include(a => a.CompId4Navigation).Include(a => a.SumCont)
                        .AsNoTracking()
                        where a.Id == Id
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
                            Ctype = a.Comp == null ? 0 : a.Comp.Ctype,
                            CompName = a.Comp == null ? "" : a.Comp.Name,
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
                            Comp3Name = a.CompId3Navigation == null ? "" : a.CompId3Navigation.Name,
                            Comp4Name = a.CompId4Navigation == null ? "" : a.CompId4Navigation.Name,
                            CompId3 = a.CompId3,
                            CompId4 = a.CompId4,
                            PrincipalUserId = a.PrincipalUserId,
                            FinanceTerms = a.FinanceTerms,
                            PerformanceDateTime = a.PerformanceDateTime,
                            SumContName = a.SumCont == null ? "" : a.SumCont.Name,//总包合同
                            SumContId = a.SumContId,
                            //htwcje = GetHtWcJe(a.Id),//实际资金已确认
                            //fpje=GetFpJe(a.Id),//发票已确认金额
                            // Zbid = a.Zbid,
                            zb = a.Zb.Project.Name,
                            // Xjid = a.Xjid,
                            xj = a.Xj.ProjectNameNavigation.Name,
                            //  Ytid = a.Ytid,
                            yt = a.Yt.ProjectNameNavigation.Name,
                            ContSingNo = a.ContSingNo,//签约人身份证号
                            FinanceType = a.FinanceType,
                            CountField = a.CountField
                        };
            var local = from a in query.AsEnumerable()
                        select new ContractInfoViewDTO
                        {
                            //Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            OtherCode = a.OtherCode,
                            ContSourceId = a.ContSourceId,
                            ContTypeId = a.ContTypeId,
                            IsFramework = a.IsFramework,
                            //如果不是String.修改就得手动绑定Radio
                            ContDivision = (a.ContDivision ?? 0).ToString(),
                            CompId = a.CompId,
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
                            //合同类别
                            ContTypeName = this.Db.GetRedisDataDictionaryValue(a.ContTypeId ?? 0, DataDictionaryEnum.contractType),
                         //   DataDicUtility.GetDicValueToRedis(a.ContTypeId, DataDictionaryEnum.contractType),//合同类别
                            //合同来源
                            ContSName = this.Db.GetRedisDataDictionaryValue(a.ContSourceId ?? 0, DataDictionaryEnum.contractType),
                            //DataDicUtility.GetDicValueToRedis(a.ContSourceId, DataDictionaryEnum.contSource),
                            CompName = a.CompName,//合同对方
                            ProjName = a.ProjName,//项目名称
                            ContPro = EmunUtility.GetDesc(typeof(ContractProperty), a.IsFramework ?? 0),//合同属性
                            ContSum = a.ContDivision > 0 ? "是" : "否",
                            ContAmThod = a.AmountMoney.ThousandsSeparator(),//合同金额千分位
                            CurrencyName = RedisValueUtility.GetCurrencyName(a.CurrencyId, fileName: "Name"), ///币种
                            Rate = a.CurrencyRate ?? 1,//汇率
                            EsAmountThod = a.EstimateAmount.ThousandsSeparator(),//预估金额
                            AdvAmountThod = a.AdvanceAmount.ThousandsSeparator(),//预收预付
                            StampTaxThod = a.StampTax.ThousandsSeparator(),//千分位
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId),//创建人
                            PrincipalUserName = this.Db.GetRedisUserFieldValue(a.PrincipalUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.PrincipalUserId ?? 0),//负责人
                            DeptName = this.Db.GetRedisDeptNameValue(a.DeptId ?? -2),
                            //RedisValueUtility.GetDeptName(a.DeptId ?? -2),
                            MdeptName = this.Db.GetRedisDeptNameValue(a.MainDeptId ?? -2),
                            //RedisValueUtility.GetDeptName(a.MainDeptId ?? -2),
                            StateDic = EmunUtility.GetDesc(typeof(ContractState), a.ContState),
                            Comp3Name = a.Comp3Name,
                            Comp4Name = a.Comp4Name,
                            CompId3 = a.CompId3,
                            CompId4 = a.CompId4,
                            FinanceTerms = a.FinanceTerms,//资金条款
                            PerformanceDateTime = a.PerformanceDateTime,
                            SumContName = a.SumContName,
                            SumContId = a.SumContId,//总包ID。修改时绑定
                                                    //HtWcJeThod = GetHtWcJe(a.Id).ThousandsSeparator(),//完成金额
                                                    //HtWcBl = GetWcBl(a.AmountMoney ?? 0, GetHtWcJe(a.Id)),//完成比例
                                                    //PiaoKaunCha = (GetFpJe(a.Id) - GetHtWcJe(a.Id)).ThousandsSeparator(),//票款差额
                                                    //FaPiaoThod = GetFpJe(a.Id).ThousandsSeparator()//发票金额
                            ZbName = a.zb,// ZbName(a.Zbid),
                            XjName = a.xj,// XjName(a.Xjid),
                            YtName = a.yt,// YtName(a.Ytid),
                            ContSingNo = a.ContSingNo,//签约人身份证号
                            FinanceType = a.FinanceType,
                            Ctype = a.Ctype,// 合同对方类型id
                                            //  CustomFields = Zh(a.CountField)
                            CustomFields = Zhs(a.Id)

                        };

            var teminfo = local.FirstOrDefault();
            if (teminfo != null)
            {
                teminfo.HtWcJeThod = GetHtWcJe(teminfo.Id).ThousandsSeparator();//完成金额
                teminfo.HtWcBl = GetWcBl(teminfo.AmountMoney ?? 0, GetHtWcJe(teminfo.Id));//完成比例
                teminfo.PiaoKaunCha = (GetFpJe(teminfo.Id) - GetHtWcJe(teminfo.Id)).ThousandsSeparator();//票款差额
                teminfo.FaPiaoThod = GetFpJe(teminfo.Id).ThousandsSeparator();//发票金额
            }
            return teminfo;
        }
        public int CyzdAdd(int id, string vals) 
        {
            var rrt = vals.Split(',');

            int e = 0;
            StringBuilder strsql = new StringBuilder();

            for (int i = 0; i < rrt.Length; i++)
            {
                var er = rrt[i].Split('+');
                var rt = er[1].Split('-');
                strsql.Append($"insert into ContCustomField (FieldId, Fieldvale,ContId)Values({rt[1]},'{er[0]}',{id});");
            }
          
           
            ExecuteSqlCommand(strsql.ToString());



            return e;
        }

        public int CyzdUpd(int id, string vals)
        {
            if (!string.IsNullOrEmpty(vals)) { 
            var rrt = vals.Split(',');

            int e = 0;
            StringBuilder strsql = new StringBuilder();

            for (int i = 0; i < rrt.Length; i++)
            {
                var er = rrt[i].Split(';');
                var rt = er[1].Split('-');


                var rts = rt[1];
                var y = Db.Set<ContCustomField>().Where(a => a.ContId == id && a.FieldId == Convert.ToInt32(rts)).ToList();

                if (y.Count()>0)
                {
                    strsql.Append($"update ContCustomField set FieldVale='{er[0]}' where fieldid={rt[1]} and contid={id};");
                }
                else
                {
                    strsql.Append($"insert into ContCustomField (FieldId, Fieldvale,ContId)Values({rt[1]},'{er[0]}',{id});");
                }

               
            }


            ExecuteSqlCommand(strsql.ToString());



            return e;
            }

            return 0;
        }
        public  List<string>Zh (string tty)
        {
               List<string> s = new List<string>();
       
            try
            {
             
                string str = tty;
                string[] strArray = str.Split(','); //字符串转数组
                str = string.Empty;
                //str = string.Join(",", strArray);//数组转成字符串
                foreach (var item in strArray)
                {
                    string strs = item;
                    string[] strArrays = item.Split('-'); //字符串转数组
                    strs = string.Empty;
                    strs = string.Join(",", strArrays);//数组转成字符串
                    s.Add(strs);
                }
            }
            catch (Exception)
            {

                return s;
            }
           



            return s;
        
        }

        public List<string> Zhs(int id) 
        {
            var s1 = "";
            var tty = Db.Set<ContCustomField>().Where(a => a.ContId == id) ;
            foreach (var item in tty)
            {
                if (s1=="")
                {
                    s1 = item.FieldVale + ";" + item.FieldId;
                }
                else
                {
                    s1 = s1+","+ item.FieldVale + ";" + item.FieldId;
                }
            }
            List<string> s = new List<string>();

            try
            {

                string str = s1;
                string[] strArray = str.Split(','); //字符串转数组
                str = string.Empty;
                //str = string.Join(",", strArray);//数组转成字符串
                foreach (var item in strArray)
                {
                    string strs = item;
                    string[] strArrays = item.Split(';'); //字符串转数组
                    strs = string.Empty;
                    strs = string.Join(",", strArrays);//数组转成字符串
                    s.Add(strs);
                }
            }
            catch (Exception)
            {

                return s;
            }

            return s;
        }

        #region 计算字段方法
        /// <summary>
        /// 合同完成金额
        /// </summary>
        /// <param name="Id">当前合同ID</param>
        /// <returns></returns>
        private decimal GetHtWcJe(int Id)
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
        private decimal GetFpJe(int Id)
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
        private string GetWcBl(decimal htje, decimal wcje)
        {
            return ((htje == 0 || wcje == 0) ? 0 : (wcje / htje)).ConvertToPercent();
             

        }

       

        #endregion



        /// <summary>
        /// 修改当前对应标签下的-UserId数据
        /// </summary>
        /// <param name="Id">当前合同ID</param>
        /// <param name="HisId">合同历史ID</param>
        public void UpdateItems(ContractInfo contInfo, ContractInfoHistory infoHistory)
        {
            StringBuilder strsql = new StringBuilder();
            var currUserId = contInfo.ModifyUserId;
            strsql.Append($"update ContractInfo set ContHid={infoHistory.Id} where Id={contInfo.Id};");
            strsql.Append($"update ContPlanFinance set Ftype={contInfo.FinanceType},ContId={contInfo.Id},CurrencyId={contInfo.CurrencyId},CurrencyRate={contInfo.CurrencyRate} where ContId={contInfo.Id} or ContId={-currUserId};");
            strsql.Append($"update ContPlanFinanceHistory set ContId={contInfo.Id} where ContId={-currUserId};");
            strsql.Append($"update ContDescription set ContId={contInfo.Id} where ContId={-currUserId};");
            strsql.Append($"update ContAttachment set ContId={contInfo.Id} where ContId={-currUserId};");
            strsql.Append($"update ContText set ContId={contInfo.Id} where ContId={-currUserId};");
            strsql.Append($"update ContTextHistory set ContId={contInfo.Id},ContHisId={infoHistory.Id} where ContId={-currUserId};");
            strsql.Append($"update ContSubjectMatter set ContId={contInfo.Id} where ContId={-currUserId};");
            strsql.Append($"update ContSubjectMatterHistory set ContId={contInfo.Id},ContHisId={infoHistory.Id} where ContId={-currUserId} or ContHisId={-currUserId};");
             ExecuteSqlCommand(strsql.ToString());

        }
        /// <summary>
        /// 清除标签垃圾数据
        /// </summary>
        /// <param name="currUserId">当前用户ID</param>
        /// <returns></returns>
        public int ClearJunkItemData(int currUserId)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append($"delete ContractInfo  where ContState=100 and CreateUserId={currUserId};");//合同备忘
            strsql.Append($"delete ContDescription  where ContId={-currUserId};");//合同备忘
            strsql.Append($"delete ContPlanFinance  where ContId={-currUserId};");//计划资金
            strsql.Append($"delete ContPlanFinanceHistory  where ContId={-currUserId};");//计划资金历史
            strsql.Append($"delete ContAttachment  where ContId={-currUserId};");//附件
            //strsql.Append($"delete ContConsult  where ContId={-currUserId};");//合同查阅人
            strsql.Append($"delete ContText  where ContId={-currUserId};");//合同文本
            strsql.Append($"delete ContTextHistory  where ContId={-currUserId};");//合同文本历史
            strsql.Append($"delete ContSubjectMatter  where ContId={-currUserId};");
            strsql.Append($"delete ContSubjectMatterHistory  where ContId={-currUserId} or ContHisId={-currUserId};");
            //添加其他标签表
            return ExecuteSqlCommand(strsql.ToString());
        }
        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="info">修改的字段对象</param>
        /// <returns>返回受影响行数</returns>
        public int UpdateField(UpdateFieldInfo info)
        {
            string sqlstr = "";
            switch (info.FieldName)
            {

                case "OtherCode"://对方合同编号
                case "Reserve1":
                case "Reserve2":
                    sqlstr = $"update  ContractInfo set {info.FieldName}='{info.FieldValue}' where Id={info.Id}";
                    break;
                case "PrincipalUserName"://负责人
                    sqlstr = $"update  ContractInfo set PrincipalUserId={info.FieldValue} where Id={info.Id}";
                    break;
                case "PerformanceDateTime"://实际履行日期
                    sqlstr = $"update  ContractInfo set PerformanceDateTime='{info.FieldValue}' where Id={info.Id}";
                    break;
                case "ContSName"://合同来源
                    sqlstr = $"update  ContractInfo set ContSourceId={info.FieldValue} where Id={info.Id}";
                    break;
                case "ContTypeName"://合同类别
                    sqlstr = $"update  ContractInfo set ContTypeId={info.FieldValue} where Id={info.Id}";
                    break;
                case "DeptName"://经办机构
                    sqlstr = $"update  ContractInfo set DeptId={info.FieldValue} where Id={info.Id}";
                    break;
                case "ProjName"://项目
                    sqlstr = $"update  ContractInfo set ProjectId={info.FieldValue} where Id={info.Id}";
                    break;
               
                default:
                    break;
            }
            if (!string.IsNullOrEmpty(sqlstr))
                return ExecuteSqlCommand(sqlstr);
            return 0;

        }

        /// <summary>
        /// 修改多个字段
        /// </summary>
        /// <param name="fields">当前字段集合</param>
        /// <returns>返回受影响行数</returns>
        public int UpdateField(IList<UpdateFieldInfo> fields)
        {
            StringBuilder sqlstr = new StringBuilder($"update  ContractInfo set ModifyUserId={fields[0].CurrUserId},ModifyDateTime='{DateTime.Now}'");
            foreach (var fd in fields)
            {
                switch (fd.FieldType)
                {
                    case "int":
                        sqlstr.Append($" ,{fd.FieldName}={Convert.ToInt32(fd.FieldValue)} ");
                        break;
                    case "float":
                        sqlstr.Append($" ,{fd.FieldName}={Convert.ToDouble(fd.FieldValue)} ");
                        break;
                    default:
                        var time ="";
                        if (fd.FieldValue==null)
                        {
                            time= DateTime.Now.ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            time = fd.FieldValue;
                        }
                        sqlstr.Append($" ,{fd.FieldName}='{time}' ");
                        break;

                }
            }
            sqlstr.Append($"where Id={Convert.ToInt32(fields[0].Id)}");
            if (!string.IsNullOrEmpty(sqlstr.ToString()))
                return ExecuteSqlCommand(sqlstr.ToString());
            return 0;
        }
        /// <summary>
        /// 查询选择合同信息列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        public  LayPageInfo<SelectContractInfoDTO> GetSelectList<s>(PageInfo<ContractInfo> pageInfo, Expression<Func<ContractInfo, bool>> whereLambda, Expression<Func<ContractInfo, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = _ContractInfoSet.AsTracking().Where<ContractInfo>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
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
                            ContTypeId = a.ContTypeId,
                            CompId = a.CompId,
                            CompName = a.Comp==null?"": a.Comp.Name,
                            AmountMoney = a.AmountMoney,
                            CreateUserId = a.CreateUserId,
                            CreateDateTime = a.CreateDateTime,
                            ContState = a.ContState,
                            FinanceType=a.FinanceType,
                            ContSingNo = a.ContSingNo,//签约人身份证号

                        };
            var local = from a in query.AsEnumerable()
                        select new SelectContractInfoDTO
                        {
                            Id = a.Id,
                            Name = a.Name,//名称
                            Code = a.Code,//编号
                            ContTypeName = this.Db.GetRedisDataDictionaryValue(a.ContTypeId ?? 0, DataDictionaryEnum.contractType),
                            //DataDicUtility.GetDicValueToRedis(a.ContTypeId, DataDictionaryEnum.contractType),//合同类别
                            ContAmThod = a.AmountMoney.ThousandsSeparator(),//合同金额千分位
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId), //创建人
                            CreateDateTime = a.CreateDateTime,//创建时间
                            ContState = a.ContState,
                            ContStateDic = EmunUtility.GetDesc(typeof(ContractState), a.ContState),
                            FinanceTypeDesc= EmunUtility.GetDesc(typeof(FinceType), a.FinanceType),
                            ContSingNo = a.ContSingNo,//签约人身份证号


                        };
            return new LayPageInfo<SelectContractInfoDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };

        }
        
      public  LayPageInfo<CompanyContract> GetContsByCompId<s>(PageInfo<ContractInfo> pageInfo, Expression<Func<ContractInfo, bool>> whereLambda, Expression<Func<ContractInfo, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = _ContractInfoSet.AsTracking().Where<ContractInfo>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
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
                            ContTypeId = a.ContTypeId,
                            AmountMoney = a.AmountMoney,
                            ContState = a.ContState,
                            ContSingNo = a.ContSingNo,//签约人身份证号


                        };
            var local = from a in query.AsEnumerable()
                        select new CompanyContract
                        {
                            Id = a.Id,
                            Name = a.Name,//名称
                            Code = a.Code,//编号
                            ContTypeName = this.Db.GetRedisDataDictionaryValue(a.ContTypeId ?? 0, DataDictionaryEnum.contractType),
                            //DataDicUtility.GetDicValueToRedis(a.ContTypeId, DataDictionaryEnum.contractType),//合同类别
                            ContAmThod = a.AmountMoney.ThousandsSeparator(),//合同金额千分位
                            ContStateDic = EmunUtility.GetDesc(typeof(ContractState), a.ContState),
                           



                        };
            return new LayPageInfo<CompanyContract>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }
        /// <summary>
        ///资金统计
        /// </summary>
        public ContractStatic GetContractStatic(int ContId)
        {
            var info = Db.Set<ContStatistic>().AsNoTracking().FirstOrDefault(a=>a.ContId== ContId);
            ContractStatic contractStatic = new ContractStatic();
            if (info != null)
            {
                var continfo = Db.Set<ContractInfo>().AsNoTracking().FirstOrDefault(a=>a.Id== ContId);
                if (continfo.ContDivision==1)
                {//分包统计

                   
                    var Fbusinfo = Db.Set<ContractInfo>().Where(a => a.FinanceType== 1 && a.SumContId == continfo.Id && a.IsDelete == 0
                      && (a.ContState == 1 ||a.ContState==2|| a.ContState == 6)).ToList();
                    //以该合同为“总合同”的所有分包合同的“合同金额”之和
                    contractStatic.ZByqdje= Fbusinfo.Sum(a => a.AmountMoney??0).ThousandsSeparator();//已签订分包金额
                    var sjye = continfo.AmountMoney - Fbusinfo.Sum(a => a.AmountMoney);
                    //该合同的“合同金额” — 已签订分包金额
                    contractStatic.ZBsyje = sjye.ThousandsSeparator();//剩余合同金额
                    // ContActualFinance//实际资金
                    var Fbids = Fbusinfo.Select(a => a.Id).ToList();
                    var sjzz = Db.Set<ContActualFinance>().Where(a => Fbids.Contains(a.ContId??0) && a.Astate == 2&&a.IsDelete==0).Sum(s => s.AmountMoney??0);
                    //以该合同为“总合同”的所有分包合同下的,状态为“已确认”的实际付款
                    contractStatic.FbSjfk = sjzz.ThousandsSeparator(); //分包实际付款
                    //已签订分包金额 — 分包实际付款
                    var fbfk = Fbusinfo.Sum(a => a.AmountMoney??0) - sjzz;
                    contractStatic.Fbwfk = fbfk.ThousandsSeparator();//分包未付款
                    //以该合同为“总合同”的所有分包合同下的,状态为“已收到”的收票
                    var fbsp = Db.Set<ContInvoice>().Where(a => Fbids.Contains(a.ContId ?? 0) && a.InState == 3 && a.IsDelete == 0).Sum(s => s.AmountMoney??0);
                    contractStatic.Fbysp = fbsp.ThousandsSeparator();//分包已收票
                   //已签订分包金额 — 分包已收票
                    var Fbwsp = Fbusinfo.Sum(a => a.AmountMoney) - fbsp;
                    contractStatic.Fbwsp = Fbwsp.ThousandsSeparator();//分包未收票
                }
                contractStatic.ActMoneryThod = info.CompActAm>0? info.CompActAm.ThousandsSeparator():"0";
                contractStatic.InvoiceMoneryThod = info.CompInAm>0? info.CompInAm.ThousandsSeparator():"0";
                //应收=实际开票-实际资金 
                var Ys= ((info.CompInAm ?? 0) - (info.CompActAm ?? 0));
                contractStatic.ReceivableThod = Ys>0?Ys.ThousandsSeparator():"0";
                    
                 
                //预收=实际资金-实际开票
                var Ysk= ((info.CompActAm ?? 0)-(info.CompInAm ?? 0));
                contractStatic.ReceivableThod = Ysk > 0 ? Ysk.ThousandsSeparator() : "0";
                if (continfo != null)
                {var cs1=((continfo.AmountMoney ?? 0) - (info.CompActAm ?? 0));
                    contractStatic.ActNoMoneryThod = cs1 > 0 ? cs1.ThousandsSeparator() : "0";
                   var cs2= ((continfo.AmountMoney ?? 0) - (info.CompInAm ?? 0));
                    contractStatic.InvoiceNoMoneryThod = cs2 > 0 ? cs2.ThousandsSeparator() : "0";
                }
              //  contractStatic.AdvanceThod = continfo.AdvanceAmount.ThousandsSeparator();//预付预收
                decimal yf = 0;
                   yf= ((info.CompInAm ?? 0) - (info.CompActAm ?? 0));
                if (yf>0)
                {
                    contractStatic.AdvanceThod = yf.ThousandsSeparator();
                }
                else
                {
                    contractStatic.AdvanceThod = "0.00";
                }
               
            }
            else
            {
                contractStatic.ActMoneryThod = "0.00";
                contractStatic.InvoiceMoneryThod ="0.00";
                contractStatic.ReceivableThod = "0.00";
                contractStatic.ReceivableThod = "0.00";
                contractStatic.ActNoMoneryThod ="0.00";
                contractStatic.InvoiceNoMoneryThod = "0.00";
                contractStatic.AdvanceThod = "0.00";

                contractStatic.ZByqdje = "0.00";
                contractStatic.ZBsyje = "0.00";
                contractStatic.FbSjfk = "0.00";
                contractStatic.Fbwfk = "0.00";
                contractStatic.Fbysp = "0.00";
                contractStatic.Fbwsp = "0.00";
              


            }

            return contractStatic;

        }
        /// <summary>
        /// 首页合同列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">Where条件表达式</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns></returns>
        public LayPageInfo<ConsoleContractInfoDTO> GetListConsoleContracts<s>(PageInfo<ContractInfo> pageInfo, Expression<Func<ContractInfo, bool>> whereLambda, Expression<Func<ContractInfo, s>> orderbyLambda, bool isAsc)
        {


            var tempquery = _ContractInfoSet.Include(a=>a.ContStatic).AsTracking().Where<ContractInfo>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda.Compile()).AsQueryable();
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda.Compile()).AsQueryable();
            }
            if (!(pageInfo is NoPageInfo<ContractInfo>))
            { //分页
                tempquery = tempquery.Skip<ContractInfo>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<ContractInfo>(pageInfo.PageSize);
            }
            
            var query = from a in tempquery
                        //join b in Db.Set<ContStatistic>()
                        //on a.Id equals b.ContId into ht
                        //from dci in ht.DefaultIfEmpty()
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            AmountMoney = a.AmountMoney,
                            FpJe= a.ContStatic != null?a.ContStatic.CompInAm:0,
                            ActAmt= a.ContStatic!= null? a.ContStatic.CompActAm:0,
                            WcBl= a.ContStatic != null ? a.ContStatic.CompRatio : 0,
                            ContSingNo = a.ContSingNo,//签约人身份证号


                        };
            var local = from a in query.AsEnumerable()
                        select new ConsoleContractInfoDTO
                        {
                            Id = a.Id,
                            Name = a.Name,//名称
                            Code = a.Code,//编号
                            HtJeThond = a.AmountMoney.ThousandsSeparator(),//合同金额千分位
                            FpJeThod = a.FpJe.ThousandsSeparator(),//发票金额
                            HtWcBl = GetWcBl(a.AmountMoney??0,a.ActAmt??0), //a.WcBl.ConvertToPercent() ,
                            ContSingNo = a.ContSingNo,//签约人身份证号



                        };
            return new LayPageInfo<ConsoleContractInfoDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }
        /// <summary>
        /// 合同进度-用于首页
        /// </summary>
        /// <returns></returns>
        public ProgressInfoDTO GetProgress()
        {
            //实际资金
            var listacts = Db.Set<ContActualFinance>().Where(a => a.Astate == (int)ActFinanceStateEnum.Confirmed&&a.IsDelete!=1)
                        .Select(a => new { ftype = a.FinceType, je = a.AmountMoney ?? 0 }).ToList();
            //合同
            var lishts = _ContractInfoSet.Where(a => a.ContState != (int)ContractState.Dozee&& a.IsDelete != 1).Select(a => new {
                ftype = a.FinanceType, je = a.AmountMoney ?? 0
            }).ToList();
            //发票
            var lisfps = Db.Set<ContInvoice>().Where(a => (a.InState == (int)InvoiceStateEnum.ReceiptInvoice
              || a.InState == (int)InvoiceStateEnum.Invoicing) && a.IsDelete != 1).Select(a => new { a.InState, je = a.AmountMoney }).ToList() ;
            //收款实际资金
            var sksjzj = listacts.Where(a => a.ftype == 0).Sum(a => a.je);
            //收款合同
            var skhtje = lishts.Where(a => a.ftype == 0).Sum(a => a.je);
            //付款实际资金
            var fksjzj = listacts.Where(a => a.ftype == 1).Sum(a => a.je);
            //付款合同金额
            var fkhtje = lishts.Where(a => a.ftype == 1).Sum(a => a.je);

            //已收票金额
            var yspje = lisfps.Where(a => a.InState == (int)InvoiceStateEnum.ReceiptInvoice).Sum(a=>a.je);
            //已开票金额
            var ykpje = lisfps.Where(a => a.InState == (int)InvoiceStateEnum.Invoicing).Sum(a => a.je);

            ProgressInfoDTO infoDTO = new ProgressInfoDTO();
            infoDTO.SkHtWcBl = (skhtje ==0?0:sksjzj / skhtje).ConvertToPercent();
            infoDTO.FkHtWcBl = (fkhtje == 0 ? 0 : fksjzj / fkhtje).ConvertToPercent();
            infoDTO.SpWcBl = (fkhtje == 0 ? 0 : yspje / fkhtje).ConvertToPercent();
            infoDTO.KpWcBl = (skhtje == 0 ? 0 : ykpje / skhtje).ConvertToPercent();

            return infoDTO;


        }

        /// <summary>
        /// 查询分包合同
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<ProjContract> GetContsByZbContId<s>(PageInfo<ContractInfo> pageInfo, Expression<Func<ContractInfo, bool>> whereLambda, Expression<Func<ContractInfo, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = this.Db.Set<ContractInfo>().Include(a => a.Comp).AsTracking().Where<ContractInfo>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
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
                            ContTypeId = a.ContTypeId,
                            AmountMoney = a.AmountMoney,
                            ContState = a.ContState,
                            CompId = a.CompId,
                            CompName = a.Comp == null ? "" : a.Comp.Name,
                            CurrencyId = a.CurrencyId,
                            FinanceType = a.FinanceType,


                        };
            var local = from a in query.AsEnumerable()
                        select new ProjContract
                        {
                            Id = a.Id,
                            Name = a.Name,//名称
                            Code = a.Code,//编号
                            ContTypeName = this.Db.GetRedisDataDictionaryValue(a.ContTypeId ?? 0, DataDictionaryEnum.contractType),
                            //DataDicUtility.GetDicValueToRedis(a.ContTypeId, DataDictionaryEnum.contractType),//合同类别
                            ContAmThod = a.AmountMoney.ThousandsSeparator(),//合同金额千分位
                            ContStateDic = EmunUtility.GetDesc(typeof(ContractState), a.ContState),
                            CompId = a.CompId ?? 0,
                            CompName = a.CompName,
                            CurrName = RedisValueUtility.GetCurrencyName(a.CurrencyId, fileName: "Name"),//币种
                            FinceTypeName = EmunUtility.GetDesc(typeof(FinceType), a.FinanceType)//合同性质



                        };
            return new LayPageInfo<ProjContract>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }
        public LayPageInfo<SysFieldDTO> ContractXtZdyzd() 
        {
            var ds = this.Db.Set<SysField>().Where(a => a.Tag == 0&&a.Isqy==1);
            var query = from a in ds
                        select new
                        {
                            Id = a.Id,
                            Fname = a.Fname,
                            Lable = a.Lable,//字段标题
                            FieldType = a.FieldType,//字段类型
                            Required = a.Required,//必填
                            IsList = a.IsList,//显示列表
                            Tag = a.Tag,//用于
                            SelData = a.SelData,//选择框内容
                            Isqy = a.Isqy,
                            Zbpx = a.Zbpx
                        };
            var local = from a in query.AsEnumerable()
                        select new SysFieldDTO
                        {
                            Id = a.Id,
                            Fname = a.Fname,
                            Lable = a.Lable,//字段标题
                            FieldType =a.FieldType,//字段类型
                            RequiredName = (a.Required ?? 0) == 0 ? "否" : "是",//必填
                            IsListName = (a.IsList ?? 0) == 0 ? "否" : "是",//显示列表
                            Tag =a.Tag,//用于
                            SelData = a.SelData,//选择框内容
                            Isqy = a.Isqy,//自定义排序列
                            Zbpx = a.Zbpx//是否启用
                        };
            return new LayPageInfo<SysFieldDTO>()
            {
                data = local.ToList(),
                count = 33,//pageInfo.TotalCount,
                code = 0


            };
        }
        public string ZType(int t)
        {
            var name = "";
            if (t == 0)
            {
                name = "text";

            }
            else if (t == 1)
            {
                name = "select";
            }
            else if (t == 2)
            {
                name = "text";
            }

            return name;
        }



        public Thzt TjNewtimezt()
        {
            var itme = DateTime.Now.Year;
            var tj = Db.Set<ContractInfo>().Where(w => w.IsDelete == 0 && w.CreateDateTime.Year == itme);

            Thzt s = new Thzt();
            s.Wzx = tj.Where(a => a.ContState == 0).Count();
            s.Ywz = tj.Where(a => a.ContState == 6).Count();

            s.Yzz = tj.Where(a => a.ContState == 2).Count();
            s.Zxz = tj.Where(a => a.ContState == 1).Count();
            s.Sptg = tj.Where(a => a.ContState == 8).Count();
            return s;
        }
        public TjHtjenum TjNewHt()
        {
            var itme = DateTime.Now.Year;
            var tj = Db.Set<ContractInfo>().Where(w => w.IsDelete == 0 && w.EffectiveDateTime.Value.Year == itme);
            TjHtjenum s = new TjHtjenum();
            //收款合同金额
            s.Yiysk = Convert.ToInt32(tj.Where(a => a.EffectiveDateTime.Value.Month == 1 && a.FinanceType == 0).Sum(r => r.AmountMoney)) / 10000;
            s.Erysk = Convert.ToInt32(tj.Where(a => a.EffectiveDateTime.Value.Month == 2 && a.FinanceType == 0).Sum(r => r.AmountMoney)) / 10000;
            s.Sanysk = Convert.ToInt32(tj.Where(a => a.EffectiveDateTime.Value.Month == 3 && a.FinanceType == 0).Sum(r => r.AmountMoney)) / 10000;
            s.Siysk = Convert.ToInt32(tj.Where(a => a.EffectiveDateTime.Value.Month == 4 && a.FinanceType == 0).Sum(r => r.AmountMoney)) / 10000;
            s.Wuysk = Convert.ToInt32(tj.Where(a => a.EffectiveDateTime.Value.Month == 5 && a.FinanceType == 0).Sum(r => r.AmountMoney)) / 10000;
            s.Liuysk = Convert.ToInt32(tj.Where(a => a.EffectiveDateTime.Value.Month == 6 && a.FinanceType == 0).Sum(r => r.AmountMoney)) / 10000;
            s.Qiysk = Convert.ToInt32(tj.Where(a => a.EffectiveDateTime.Value.Month == 7 && a.FinanceType == 0).Sum(r => r.AmountMoney)) / 10000;
            s.Baysk = Convert.ToInt32(tj.Where(a => a.EffectiveDateTime.Value.Month == 8 && a.FinanceType == 0).Sum(r => r.AmountMoney)) / 10000;
            s.Jiuysk = Convert.ToInt32(tj.Where(a => a.EffectiveDateTime.Value.Month == 9 && a.FinanceType == 0).Sum(r => r.AmountMoney)) / 10000;
            s.Shiysk = Convert.ToInt32(tj.Where(a => a.EffectiveDateTime.Value.Month == 10 && a.FinanceType == 0).Sum(r => r.AmountMoney)) / 10000;
            s.ShiYiysk = Convert.ToInt32(tj.Where(a => a.EffectiveDateTime.Value.Month == 11 && a.FinanceType == 0).Sum(r => r.AmountMoney)) / 10000;
            s.ShiErysk = Convert.ToInt32(tj.Where(a => a.EffectiveDateTime.Value.Month == 12 && a.FinanceType == 0).Sum(r => r.AmountMoney)) / 10000;
            //付款合同金额
            s.Yiyfk = Convert.ToInt32(tj.Where(a => a.EffectiveDateTime.Value.Month == 1 && a.FinanceType == 1).Sum(r => r.AmountMoney)) / 10000;
            s.Eryfk = Convert.ToInt32(tj.Where(a => a.EffectiveDateTime.Value.Month == 2 && a.FinanceType == 1).Sum(r => r.AmountMoney)) / 10000;
            s.Sanyfk = Convert.ToInt32(tj.Where(a => a.EffectiveDateTime.Value.Month == 3 && a.FinanceType == 1).Sum(r => r.AmountMoney)) / 10000;
            s.Siyfk = Convert.ToInt32(tj.Where(a => a.EffectiveDateTime.Value.Month == 4 && a.FinanceType == 1).Sum(r => r.AmountMoney)) / 10000;
            s.Wuyfk = Convert.ToInt32(tj.Where(a => a.EffectiveDateTime.Value.Month == 5 && a.FinanceType == 1).Sum(r => r.AmountMoney)) / 10000;
            s.Liuyfk = Convert.ToInt32(tj.Where(a => a.EffectiveDateTime.Value.Month == 6 && a.FinanceType == 1).Sum(r => r.AmountMoney)) / 10000;
            s.Qiyfk = Convert.ToInt32(tj.Where(a => a.EffectiveDateTime.Value.Month == 7 && a.FinanceType == 1).Sum(r => r.AmountMoney)) / 10000;
            s.Bayfk = Convert.ToInt32(tj.Where(a => a.EffectiveDateTime.Value.Month == 8 && a.FinanceType == 1).Sum(r => r.AmountMoney)) / 10000;
            s.Jiuyfk = Convert.ToInt32(tj.Where(a => a.EffectiveDateTime.Value.Month == 9 && a.FinanceType == 1).Sum(r => r.AmountMoney)) / 10000;
            s.Shiyfk = Convert.ToInt32(tj.Where(a => a.EffectiveDateTime.Value.Month == 10 && a.FinanceType == 1).Sum(r => r.AmountMoney)) / 10000;
            s.ShiYiyfk = Convert.ToInt32(tj.Where(a => a.EffectiveDateTime.Value.Month == 11 && a.FinanceType == 1).Sum(r => r.AmountMoney)) / 10000;
            s.ShiEryfk = Convert.ToInt32(tj.Where(a => a.EffectiveDateTime.Value.Month == 12 && a.FinanceType == 1).Sum(r => r.AmountMoney)) / 10000;
            //收款合同份数
            s.Yiyskfs = tj.Where(a => a.EffectiveDateTime.Value.Month == 1 && a.FinanceType == 0).Count();
            s.Eryskfs = tj.Where(a => a.EffectiveDateTime.Value.Month == 2 && a.FinanceType == 0).Count();
            s.Sanyskfs = tj.Where(a => a.EffectiveDateTime.Value.Month == 3 && a.FinanceType == 0).Count();
            s.Siyskfs = tj.Where(a => a.EffectiveDateTime.Value.Month == 4 && a.FinanceType == 0).Count();
            s.Wuyskfs = tj.Where(a => a.EffectiveDateTime.Value.Month == 5 && a.FinanceType == 0).Count();
            s.Liuyskfs = tj.Where(a => a.EffectiveDateTime.Value.Month == 6 && a.FinanceType == 0).Count();
            s.Qiyskfs = tj.Where(a => a.EffectiveDateTime.Value.Month == 7 && a.FinanceType == 0).Count();
            s.Bayskfs = tj.Where(a => a.EffectiveDateTime.Value.Month == 8 && a.FinanceType == 0).Count();
            s.Jiuyskfs = tj.Where(a => a.EffectiveDateTime.Value.Month == 9 && a.FinanceType == 0).Count();
            s.Shiyskfs = tj.Where(a => a.EffectiveDateTime.Value.Month == 10 && a.FinanceType == 0).Count();
            s.ShiYiyskfs = tj.Where(a => a.EffectiveDateTime.Value.Month == 11 && a.FinanceType == 0).Count();
            s.ShiEryskfs = tj.Where(a => a.EffectiveDateTime.Value.Month == 12 && a.FinanceType == 0).Count();
            //付款合同份数
            s.Yiyfkfs = tj.Where(a => a.EffectiveDateTime.Value.Month == 1 && a.FinanceType == 1).Count();
            s.Eryfkfs = tj.Where(a => a.EffectiveDateTime.Value.Month == 2 && a.FinanceType == 1).Count();
            s.Sanyfkfs = tj.Where(a => a.EffectiveDateTime.Value.Month == 3 && a.FinanceType == 1).Count();
            s.Siyfkfs = tj.Where(a => a.EffectiveDateTime.Value.Month == 4 && a.FinanceType == 1).Count();
            s.Wuyfkfs = tj.Where(a => a.EffectiveDateTime.Value.Month == 5 && a.FinanceType == 1).Count();
            s.Liuyfkfs = tj.Where(a => a.EffectiveDateTime.Value.Month == 6 && a.FinanceType == 1).Count();
            s.Qiyfkfs = tj.Where(a => a.EffectiveDateTime.Value.Month == 7 && a.FinanceType == 1).Count();
            s.Bayfkfs = tj.Where(a => a.EffectiveDateTime.Value.Month == 8 && a.FinanceType == 1).Count();
            s.Jiuyfkfs = tj.Where(a => a.EffectiveDateTime.Value.Month == 9 && a.FinanceType == 1).Count();
            s.Shiyfkfs = tj.Where(a => a.EffectiveDateTime.Value.Month == 10 && a.FinanceType == 1).Count();
            s.ShiYiyfkfs = tj.Where(a => a.EffectiveDateTime.Value.Month == 11 && a.FinanceType == 1).Count();
            s.ShiEryfkfs = tj.Where(a => a.EffectiveDateTime.Value.Month == 12 && a.FinanceType == 1).Count();




            return s;
        }

        public HtTypeTj Htlbjt()
        {
            var itme = DateTime.Now.Year;
            var tj = Db.Set<ContractInfo>().Where(w => w.IsDelete == 0 && w.CreateDateTime.Year == itme);
            var htTpe = Db.Set<DataDictionary>().Where(a => a.IsDelete == 0);
            HtTypeTj s = new HtTypeTj();
            var zdgc = htTpe.Where(a => a.Name == "重点工程类").FirstOrDefault().Id;
            var wzgy = htTpe.Where(a => a.Name == "物资供应类").FirstOrDefault().Id;
            var dzxx = htTpe.Where(a => a.Name == "电子信息类").FirstOrDefault().Id;

            s.skzdgc = tj.Where(a => a.FinanceType == 0 && a.ContTypeId == zdgc).Count();//收-重点工程类
            s.skwzgy = tj.Where(a => a.FinanceType == 0 && a.ContTypeId == wzgy).Count();//收-物资供应类
            s.skdzxx = tj.Where(a => a.FinanceType == 0 && a.ContTypeId == dzxx).Count();// 收-电子信息类



            s.fkzdgc = tj.Where(a => a.FinanceType == 1 && a.ContTypeId == zdgc).Count();//  付-重点工程类
            s.fkwzgy = tj.Where(a => a.FinanceType == 1 && a.ContTypeId == wzgy).Count();// 付-物资供应类
            s.fkdzxx = tj.Where(a => a.FinanceType == 1 && a.ContTypeId == dzxx).Count();//  付-电子信息类
            return s;

        }

        public HtjeTj HtjeTj(int time)
        {
            var itme = 0;
            if (time == 0)
            {
                itme = DateTime.Now.Year;
            }
            else
            {
                itme = time;
            }

            var sj = itme - 4;
            var tj = Db.Set<ContractInfo>().Where(w => w.IsDelete == 0);

            //时间集合
            List<string> t = new List<string>();
            //签约金额
            List<int> q = new List<int>();
            ///应收金额
            List<int> y = new List<int>();
            ///实收金额
            List<int> ss = new List<int>();

            HtjeTj s = new HtjeTj();
            for (int i = sj; i <= itme; i++)
            {
                t.Add(i.ToString());

                //签约金额
                var qjje = tj.Where(w => w.EffectiveDateTime.Value.Year == i).Sum(a => a.AmountMoney);
                q.Add(Convert.ToInt32(qjje));
                //应收金额
                var ysje = Db.Set<ContPlanFinance>().Where(a => a.IsDelete == 0 && a.PlanCompleteDateTime.Value.Year == i).Sum(e => e.AmountMoney);
                y.Add(Convert.ToInt32(ysje));
                //实收金额
                var ssje = Db.Set<ContActualFinance>().Where(a => a.IsDelete == 0 && a.ConfirmDateTime.Value.Year == i).Sum(e => e.AmountMoney);
                ss.Add(Convert.ToInt32(ssje));
            }
            s.time = t;
            s.Qyje = q;
            s.Ysje = y;
            s.Ssje = ss;


            return s;

        }

        /// <summary>
        /// 合同资金往来
        /// </summary>
        /// <param name="time">年份</param>
        /// <returns></returns>
        public Htwlzjtj Htwlzjtj(int time)
        {
            var itme = 0;
            if (time == 0)
            {
                itme = DateTime.Now.Year;
            }
            else
            {
                itme = time;
            }
            var sj = itme - 4;
            Htwlzjtj s = new Htwlzjtj();
            //暂存年份集合
            List<int> tm = new List<int>();
            #region 收款合同暂存数据集合
            //暂存1月集合
            List<int> sk1 = new List<int>();
            //暂存2月集合
            List<int> sk2 = new List<int>();
            //暂存3月集合
            List<int> sk3 = new List<int>();
            //暂存4月集合
            List<int> sk4 = new List<int>();
            //暂存5月集合
            List<int> sk5 = new List<int>();
            //暂存6月集合
            List<int> sk6 = new List<int>();
            //暂存7月集合
            List<int> sk7 = new List<int>();
            //暂存8月集合
            List<int> sk8 = new List<int>();
            //暂存9月集合
            List<int> sk9 = new List<int>();
            //暂存10月集合
            List<int> sk10 = new List<int>();
            //暂存11月集合
            List<int> sk11 = new List<int>();
            //暂存12月集合
            List<int> sk12 = new List<int>();

            #endregion
            #region 付款合同暂存数据集合
            //暂存1月集合
            List<int> fk1 = new List<int>();
            //暂存2月集合
            List<int> fk2 = new List<int>();
            //暂存3月集合
            List<int> fk3 = new List<int>();
            //暂存4月集合
            List<int> fk4 = new List<int>();
            //暂存5月集合
            List<int> fk5 = new List<int>();
            //暂存6月集合
            List<int> fk6 = new List<int>();
            //暂存7月集合
            List<int> fk7 = new List<int>();
            //暂存8月集合
            List<int> fk8 = new List<int>();
            //暂存9月集合
            List<int> fk9 = new List<int>();
            //暂存10月集合
            List<int> fk10 = new List<int>();
            //暂存11月集合
            List<int> fk11 = new List<int>();
            //暂存12月集合
            List<int> fk12 = new List<int>();
            #endregion
            #region 实际收款暂存数据集合
            //实际收款一月
            List<int> sjsk1 = new List<int>();
            //实际收款二月
            List<int> sjsk2 = new List<int>();
            //实际收款三月
            List<int> sjsk3 = new List<int>();
            //实际收款四月
            List<int> sjsk4 = new List<int>();
            //实际收款五月
            List<int> sjsk5 = new List<int>();
            //实际收款六月
            List<int> sjsk6 = new List<int>();
            //实际收款七月
            List<int> sjsk7 = new List<int>();
            //实际收款八月
            List<int> sjsk8 = new List<int>();
            //实际收款九月
            List<int> sjsk9 = new List<int>();
            //实际收款十月
            List<int> sjsk10 = new List<int>();
            //实际收款11月
            List<int> sjsk11 = new List<int>();
            //实际收款十二月
            List<int> sjsk12 = new List<int>();
            #endregion
            #region 实际付款暂存数据集合
            //实际付款一月
            List<int> sjfk1 = new List<int>();
            //实际付款二月
            List<int> sjfk2 = new List<int>();
            //实际付款三月
            List<int> sjfk3 = new List<int>();
            //实际付款四月
            List<int> sjfk4 = new List<int>();
            //实际付款五月
            List<int> sjfk5 = new List<int>();
            //实际付款六月
            List<int> sjfk6 = new List<int>();
            //实际付款七月
            List<int> sjfk7 = new List<int>();
            //实际付款八月
            List<int> sjfk8 = new List<int>();
            //实际付款九月
            List<int> sjfk9 = new List<int>();
            //实际付款十月
            List<int> sjfk10 = new List<int>();
            //实际付款11月
            List<int> sjfk11 = new List<int>();
            //实际付款十二月
            List<int> sjfk12 = new List<int>();
            #endregion
            #region 开票暂存数据集合
            //开票一月数据
            List<int> kp1 = new List<int>();
            //开票二月数据
            List<int> kp2 = new List<int>();
            //开票三月数据
            List<int> kp3 = new List<int>();
            //开票四月数据
            List<int> kp4 = new List<int>();
            //开票五月数据
            List<int> kp5 = new List<int>();
            //开票六月数据
            List<int> kp6 = new List<int>();
            //开票七月数据
            List<int> kp7 = new List<int>();
            //开票八月数据
            List<int> kp8 = new List<int>();
            //开票九月数据
            List<int> kp9 = new List<int>();
            //开票十月数据
            List<int> kp10 = new List<int>();
            //开票十一月数据
            List<int> kp11 = new List<int>();
            //开票十二月数据
            List<int> kp12 = new List<int>();
            #endregion
            #region 收票暂存数据集合
            //收票一月数据
            List<int> sp1 = new List<int>();
            //收票二月数据
            List<int> sp2 = new List<int>();
            //收票三月数据
            List<int> sp3 = new List<int>();
            //收票四月数据
            List<int> sp4 = new List<int>();
            //收票五月数据
            List<int> sp5 = new List<int>();
            //收票六月数据
            List<int> sp6 = new List<int>();
            //收票七月数据
            List<int> sp7 = new List<int>();
            //收票八月数据
            List<int> sp8 = new List<int>();
            //收票九月数据
            List<int> sp9 = new List<int>();
            //收票十月数据
            List<int> sp10 = new List<int>();
            //收票十一月数据
            List<int> sp11 = new List<int>();
            //收票十二月数据
            List<int> sp12 = new List<int>();
            #endregion
            //合同数据集
            var ht = Db.Set<ContractInfo>().Where(a => a.IsDelete == 0);
            //实际资金数据集
            var sjzj = Db.Set<ContActualFinance>().Where(a => a.IsDelete == 0);
            //发票数据集
            var fp = Db.Set<ContInvoice>().Where(a => a.IsDelete == 0);
            //收款合同数据集
            for (int i = sj; i <= itme; i++)
            {
                tm.Add(i);
                #region 收款合同数据查询
                //收款合同
                var s1je = ht.Where(a => a.EffectiveDateTime.Value.Year == i && a.EffectiveDateTime.Value.Month == 1 && a.FinanceType == 0).Sum(e => e.AmountMoney);
                var s2je = ht.Where(a => a.EffectiveDateTime.Value.Year == i && a.EffectiveDateTime.Value.Month == 2 && a.FinanceType == 0).Sum(e => e.AmountMoney);
                var s3je = ht.Where(a => a.EffectiveDateTime.Value.Year == i && a.EffectiveDateTime.Value.Month == 3 && a.FinanceType == 0).Sum(e => e.AmountMoney);
                var s4je = ht.Where(a => a.EffectiveDateTime.Value.Year == i && a.EffectiveDateTime.Value.Month == 4 && a.FinanceType == 0).Sum(e => e.AmountMoney);
                var s5je = ht.Where(a => a.EffectiveDateTime.Value.Year == i && a.EffectiveDateTime.Value.Month == 5 && a.FinanceType == 0).Sum(e => e.AmountMoney);
                var s6je = ht.Where(a => a.EffectiveDateTime.Value.Year == i && a.EffectiveDateTime.Value.Month == 6 && a.FinanceType == 0).Sum(e => e.AmountMoney);
                var s7je = ht.Where(a => a.EffectiveDateTime.Value.Year == i && a.EffectiveDateTime.Value.Month == 7 && a.FinanceType == 0).Sum(e => e.AmountMoney);
                var s8je = ht.Where(a => a.EffectiveDateTime.Value.Year == i && a.EffectiveDateTime.Value.Month == 8 && a.FinanceType == 0).Sum(e => e.AmountMoney);
                var s9je = ht.Where(a => a.EffectiveDateTime.Value.Year == i && a.EffectiveDateTime.Value.Month == 9 && a.FinanceType == 0).Sum(e => e.AmountMoney);
                var s10je = ht.Where(a => a.EffectiveDateTime.Value.Year == i && a.EffectiveDateTime.Value.Month == 10 && a.FinanceType == 0).Sum(e => e.AmountMoney);
                var s11je = ht.Where(a => a.EffectiveDateTime.Value.Year == i && a.EffectiveDateTime.Value.Month == 11 && a.FinanceType == 0).Sum(e => e.AmountMoney);
                var s12je = ht.Where(a => a.EffectiveDateTime.Value.Year == i && a.EffectiveDateTime.Value.Month == 12 && a.FinanceType == 0).Sum(e => e.AmountMoney);
                #endregion
                #region 付款合同数据查询 
                //付款合同
                var f1je = ht.Where(a => a.EffectiveDateTime.Value.Year == i && a.EffectiveDateTime.Value.Month == 1 && a.FinanceType == 1).Sum(e => e.AmountMoney);
                var f2je = ht.Where(a => a.EffectiveDateTime.Value.Year == i && a.EffectiveDateTime.Value.Month == 2 && a.FinanceType == 1).Sum(e => e.AmountMoney);
                var f3je = ht.Where(a => a.EffectiveDateTime.Value.Year == i && a.EffectiveDateTime.Value.Month == 3 && a.FinanceType == 1).Sum(e => e.AmountMoney);
                var f4je = ht.Where(a => a.EffectiveDateTime.Value.Year == i && a.EffectiveDateTime.Value.Month == 4 && a.FinanceType == 1).Sum(e => e.AmountMoney);
                var f5je = ht.Where(a => a.EffectiveDateTime.Value.Year == i && a.EffectiveDateTime.Value.Month == 5 && a.FinanceType == 1).Sum(e => e.AmountMoney);
                var f6je = ht.Where(a => a.EffectiveDateTime.Value.Year == i && a.EffectiveDateTime.Value.Month == 6 && a.FinanceType == 1).Sum(e => e.AmountMoney);
                var f7je = ht.Where(a => a.EffectiveDateTime.Value.Year == i && a.EffectiveDateTime.Value.Month == 7 && a.FinanceType == 1).Sum(e => e.AmountMoney);
                var f8je = ht.Where(a => a.EffectiveDateTime.Value.Year == i && a.EffectiveDateTime.Value.Month == 8 && a.FinanceType == 1).Sum(e => e.AmountMoney);
                var f9je = ht.Where(a => a.EffectiveDateTime.Value.Year == i && a.EffectiveDateTime.Value.Month == 9 && a.FinanceType == 1).Sum(e => e.AmountMoney);
                var f10je = ht.Where(a => a.EffectiveDateTime.Value.Year == i && a.EffectiveDateTime.Value.Month == 10 && a.FinanceType == 1).Sum(e => e.AmountMoney);
                var f11je = ht.Where(a => a.EffectiveDateTime.Value.Year == i && a.EffectiveDateTime.Value.Month == 11 && a.FinanceType == 1).Sum(e => e.AmountMoney);
                var f12je = ht.Where(a => a.EffectiveDateTime.Value.Year == i && a.EffectiveDateTime.Value.Month == 12 && a.FinanceType == 1).Sum(e => e.AmountMoney);
                #endregion
                #region 实际收款
                var sjs1 = sjzj.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.FinceType == 0).Sum(a => a.AmountMoney);
                var sjs2 = sjzj.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.FinceType == 0).Sum(a => a.AmountMoney);
                var sjs3 = sjzj.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.FinceType == 0).Sum(a => a.AmountMoney);
                var sjs4 = sjzj.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.FinceType == 0).Sum(a => a.AmountMoney);
                var sjs5 = sjzj.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.FinceType == 0).Sum(a => a.AmountMoney);
                var sjs6 = sjzj.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.FinceType == 0).Sum(a => a.AmountMoney);
                var sjs7 = sjzj.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.FinceType == 0).Sum(a => a.AmountMoney);
                var sjs8 = sjzj.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.FinceType == 0).Sum(a => a.AmountMoney);
                var sjs9 = sjzj.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.FinceType == 0).Sum(a => a.AmountMoney);
                var sjs10 = sjzj.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.FinceType == 0).Sum(a => a.AmountMoney);
                var sjs11 = sjzj.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.FinceType == 0).Sum(a => a.AmountMoney);
                var sjs12 = sjzj.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.FinceType == 0).Sum(a => a.AmountMoney);
                #endregion
                #region 实际付款
                var sjf1 = sjzj.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.FinceType == 1).Sum(a => a.AmountMoney);
                var sjf2 = sjzj.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.FinceType == 1).Sum(a => a.AmountMoney);
                var sjf3 = sjzj.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.FinceType == 1).Sum(a => a.AmountMoney);
                var sjf4 = sjzj.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.FinceType == 1).Sum(a => a.AmountMoney);
                var sjf5 = sjzj.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.FinceType == 1).Sum(a => a.AmountMoney);
                var sjf6 = sjzj.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.FinceType == 1).Sum(a => a.AmountMoney);
                var sjf7 = sjzj.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.FinceType == 1).Sum(a => a.AmountMoney);
                var sjf8 = sjzj.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.FinceType == 1).Sum(a => a.AmountMoney);
                var sjf9 = sjzj.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.FinceType == 1).Sum(a => a.AmountMoney);
                var sjf10 = sjzj.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.FinceType == 1).Sum(a => a.AmountMoney);
                var sjf11 = sjzj.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.FinceType == 1).Sum(a => a.AmountMoney);
                var sjf12 = sjzj.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.FinceType == 1).Sum(a => a.AmountMoney);
                #endregion
                #region 开票
                var kpsj1 = fp.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.Cont.FinanceType == 0).Sum(a => a.AmountMoney);
                var kpsj2 = fp.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.Cont.FinanceType == 0).Sum(a => a.AmountMoney);
                var kpsj3 = fp.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.Cont.FinanceType == 0).Sum(a => a.AmountMoney);
                var kpsj4 = fp.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.Cont.FinanceType == 0).Sum(a => a.AmountMoney);
                var kpsj5 = fp.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.Cont.FinanceType == 0).Sum(a => a.AmountMoney);
                var kpsj6 = fp.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.Cont.FinanceType == 0).Sum(a => a.AmountMoney);
                var kpsj7 = fp.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.Cont.FinanceType == 0).Sum(a => a.AmountMoney);
                var kpsj8 = fp.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.Cont.FinanceType == 0).Sum(a => a.AmountMoney);
                var kpsj9 = fp.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.Cont.FinanceType == 0).Sum(a => a.AmountMoney);
                var kpsj10 = fp.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.Cont.FinanceType == 0).Sum(a => a.AmountMoney);
                var kpsj11 = fp.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.Cont.FinanceType == 0).Sum(a => a.AmountMoney);
                var kpsj12 = fp.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.Cont.FinanceType == 0).Sum(a => a.AmountMoney);
                #endregion
                #region 收票
                var spsj1 = fp.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.Cont.FinanceType == 1).Sum(a => a.AmountMoney);
                var spsj2 = fp.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.Cont.FinanceType == 1).Sum(a => a.AmountMoney);
                var spsj3 = fp.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.Cont.FinanceType == 1).Sum(a => a.AmountMoney);
                var spsj4 = fp.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.Cont.FinanceType == 1).Sum(a => a.AmountMoney);
                var spsj5 = fp.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.Cont.FinanceType == 1).Sum(a => a.AmountMoney);
                var spsj6 = fp.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.Cont.FinanceType == 1).Sum(a => a.AmountMoney);
                var spsj7 = fp.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.Cont.FinanceType == 1).Sum(a => a.AmountMoney);
                var spsj8 = fp.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.Cont.FinanceType == 1).Sum(a => a.AmountMoney);
                var spsj9 = fp.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.Cont.FinanceType == 1).Sum(a => a.AmountMoney);
                var spsj10 = fp.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.Cont.FinanceType == 1).Sum(a => a.AmountMoney);
                var spsj11 = fp.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.Cont.FinanceType == 1).Sum(a => a.AmountMoney);
                var spsj12 = fp.Where(a => a.ConfirmDateTime.Value.Year == i && a.ConfirmDateTime.Value.Month == 1 && a.Cont.FinanceType == 1).Sum(a => a.AmountMoney);
                #endregion

                #region 收款合同把查询到的收款数据添加到暂存集合
                //收款合同
                sk1.Add(Convert.ToInt32(s1je));
                sk2.Add(Convert.ToInt32(s2je));
                sk3.Add(Convert.ToInt32(s3je));
                sk4.Add(Convert.ToInt32(s4je));
                sk5.Add(Convert.ToInt32(s5je));
                sk6.Add(Convert.ToInt32(s6je));
                sk7.Add(Convert.ToInt32(s7je));
                sk8.Add(Convert.ToInt32(s8je));
                sk9.Add(Convert.ToInt32(s9je));
                sk10.Add(Convert.ToInt32(s10je));
                sk11.Add(Convert.ToInt32(s11je));
                sk12.Add(Convert.ToInt32(s12je));
                #endregion
                #region 付款合同把查询到的付款数据添加到暂存集合
                //付款合同
                fk1.Add(Convert.ToInt32(f1je));
                fk2.Add(Convert.ToInt32(f2je));
                fk3.Add(Convert.ToInt32(f3je));
                fk4.Add(Convert.ToInt32(f4je));
                fk5.Add(Convert.ToInt32(f5je));
                fk6.Add(Convert.ToInt32(f6je));
                fk7.Add(Convert.ToInt32(f7je));
                fk8.Add(Convert.ToInt32(f8je));
                fk9.Add(Convert.ToInt32(f9je));
                fk10.Add(Convert.ToInt32(f10je));
                fk11.Add(Convert.ToInt32(f11je));
                fk12.Add(Convert.ToInt32(f12je));
                #endregion
                #region 实际收款把查到的数据添加到暂存集合
                sjsk1.Add(Convert.ToInt32(sjs1));
                sjsk2.Add(Convert.ToInt32(sjs2));
                sjsk3.Add(Convert.ToInt32(sjs3));
                sjsk4.Add(Convert.ToInt32(sjs4));
                sjsk5.Add(Convert.ToInt32(sjs5));
                sjsk6.Add(Convert.ToInt32(sjs6));
                sjsk7.Add(Convert.ToInt32(sjs7));
                sjsk8.Add(Convert.ToInt32(sjs8));
                sjsk9.Add(Convert.ToInt32(sjs9));
                sjsk10.Add(Convert.ToInt32(sjs10));
                sjsk11.Add(Convert.ToInt32(sjs11));
                sjsk12.Add(Convert.ToInt32(sjs12));
                #endregion
                #region 实际付款把查到的数据添加到暂存集合
                sjfk1.Add(Convert.ToInt32(sjf1));
                sjfk2.Add(Convert.ToInt32(sjf2));
                sjfk3.Add(Convert.ToInt32(sjf3));
                sjfk4.Add(Convert.ToInt32(sjf4));
                sjfk5.Add(Convert.ToInt32(sjf5));
                sjfk6.Add(Convert.ToInt32(sjf6));
                sjfk7.Add(Convert.ToInt32(sjf7));
                sjfk8.Add(Convert.ToInt32(sjf8));
                sjfk9.Add(Convert.ToInt32(sjf9));
                sjfk10.Add(Convert.ToInt32(sjf10));
                sjfk11.Add(Convert.ToInt32(sjf11));
                sjfk12.Add(Convert.ToInt32(sjf12));
                #endregion
                #region 开票把把查询到的数据添加到暂存集合
                kp1.Add(Convert.ToInt32(kpsj1));
                kp2.Add(Convert.ToInt32(kpsj2));
                kp3.Add(Convert.ToInt32(kpsj3));
                kp4.Add(Convert.ToInt32(kpsj4));
                kp5.Add(Convert.ToInt32(kpsj5));
                kp6.Add(Convert.ToInt32(kpsj6));
                kp7.Add(Convert.ToInt32(kpsj7));
                kp8.Add(Convert.ToInt32(kpsj8));
                kp9.Add(Convert.ToInt32(kpsj9));
                kp10.Add(Convert.ToInt32(kpsj10));
                kp11.Add(Convert.ToInt32(kpsj11));
                kp12.Add(Convert.ToInt32(kpsj12));
                #endregion
                #region 收票把把查询到的数据添加到暂存集合
                sp1.Add(Convert.ToInt32(spsj1));
                sp2.Add(Convert.ToInt32(spsj2));
                sp3.Add(Convert.ToInt32(spsj3));
                sp4.Add(Convert.ToInt32(spsj4));
                sp5.Add(Convert.ToInt32(spsj5));
                sp6.Add(Convert.ToInt32(spsj6));
                sp7.Add(Convert.ToInt32(spsj7));
                sp8.Add(Convert.ToInt32(spsj8));
                sp9.Add(Convert.ToInt32(spsj9));
                sp10.Add(Convert.ToInt32(spsj10));
                sp11.Add(Convert.ToInt32(spsj11));
                sp12.Add(Convert.ToInt32(spsj12));
                #endregion
                #region 
                #endregion

            }
            s.Sknf = tm;
            #region 收款合同暂存集合添加到Htwlzjtj集合
            //收款合同
            s.Sk1y = sk1;
            s.Sk2y = sk2;
            s.Sk3y = sk3;
            s.Sk4y = sk4;
            s.Sk5y = sk5;
            s.Sk6y = sk6;
            s.Sk7y = sk7;
            s.Sk8y = sk8;
            s.Sk9y = sk9;
            s.Sk10y = sk10;
            s.Sk11y = sk11;
            s.Sk12y = sk12;
            #endregion
            #region 付款合同暂存集合添加到Htwlzjtj集合
            //付款合同
            s.Fk1y = fk1;
            s.Fk2y = fk2;
            s.Fk3y = fk3;
            s.Fk4y = fk4;
            s.Fk5y = fk5;
            s.Fk6y = fk6;
            s.Fk7y = fk7;
            s.Fk8y = fk8;
            s.Fk9y = fk9;
            s.Fk10y = fk10;
            s.Fk11y = fk11;
            s.Fk12y = fk12;
            #endregion
            #region 实际收款把暂存数据添加到Htwlzjtj集合
            s.sjsk1 = sjsk1;
            s.sjsk2 = sjsk2;
            s.sjsk3 = sjsk3;
            s.sjsk4 = sjsk4;
            s.sjsk5 = sjsk5;
            s.sjsk6 = sjsk6;
            s.sjsk7 = sjsk7;
            s.sjsk8 = sjsk8;
            s.sjsk9 = sjsk9;
            s.sjsk10 = sjsk10;
            s.sjsk11 = sjsk11;
            s.sjsk12 = sjsk12;
            #endregion
            #region 实际付款把暂存数据添加到Htwlzjtj集合
            s.sjfk1 = sjfk1;
            s.sjfk2 = sjfk2;
            s.sjfk3 = sjfk3;
            s.sjfk4 = sjfk4;
            s.sjfk5 = sjfk5;
            s.sjfk6 = sjfk6;
            s.sjfk7 = sjfk7;
            s.sjfk8 = sjfk8;
            s.sjfk9 = sjfk9;
            s.sjfk10 = sjfk10;
            s.sjfk11 = sjfk11;
            s.sjfk12 = sjfk12;
            #endregion
            #region 开票把暂存数据添加到Htwlzjtj集合
            s.kp1 = kp1;
            s.kp2 = kp2;
            s.kp3 = kp3;
            s.kp4 = kp4;
            s.kp5 = kp5;
            s.kp6 = kp6;
            s.kp7 = kp7;
            s.kp8 = kp8;
            s.kp9 = kp9;
            s.kp10 = kp10;
            s.kp11 = kp11;
            s.kp12 = kp12;
            #endregion
            #region 收票把暂存数据添加到Htwlzjtj集合
            s.sp1 = sp1;
            s.sp2 = sp2;
            s.sp3 = sp3;
            s.sp4 = sp4;
            s.sp5 = sp5;
            s.sp6 = sp6;
            s.sp7 = sp7;
            s.sp8 = sp8;
            s.sp9 = sp9;
            s.sp10 = sp10;
            s.sp11 = sp11;
            s.sp12 = sp12;
            #endregion
            return s;
        }

        /// <summary>
        /// 合同来源统计
        /// </summary>
        /// <returns></returns>
        public HtLyTj HtLyTj()
        {
            var tj = Db.Set<ContractInfo>().Where(w => w.IsDelete == 0);

            HtLyTj s = new HtLyTj();
            s.Szbht = tj.Where(a => a.FinanceType == 0 && a.ContType.Name == "招标合同").Count();
            s.Szdht = tj.Where(a => a.FinanceType == 0 && a.ContType.Name == "指定合同").Count();
            s.Sxjht = tj.Where(a => a.FinanceType == 0 && a.ContType.Name == "询价合同").Count();
            s.Fzbht = tj.Where(a => a.FinanceType == 1 && a.ContType.Name == "招标合同").Count();
            s.Fzdht = tj.Where(a => a.FinanceType == 1 && a.ContType.Name == "指定合同").Count();
            s.Fxjht = tj.Where(a => a.FinanceType == 1 && a.ContType.Name == "询价合同").Count();
            return s;
        }


        /// <summary>
        /// 不同意时清理缓存
        /// </summary>
        /// <param name="submitOption">意见</param>
        /// <returns></returns>
        public int Htch(SubmitOptionInfo submitOption)
        {
            var appinst = Db.Set<AppInst>().Where(a => a.Id == submitOption.InstId).FirstOrDefault();





            //var appinst = Db.Set<AppInst>().Where(a => a.Id == submitOption.InstId).FirstOrDefault();
            //appinst.AppState = 3;
            //appinst.CompleteDateTime = DateTime.Now;
            //var currNodeInfo = Db.Set<AppInstNodeInfo>().Where(a => a.NodeStrId == appinst.CurrentNodeStrId && a.InstId == submitOption.InstId).FirstOrDefault();
            //currNodeInfo.NodeState = 3;

            //var currNode = Db.Set<AppInstNode>().Where(a => a.NodeStrId == appinst.CurrentNodeStrId && a.InstId == submitOption.InstId).FirstOrDefault();
            //currNode.NodeState = 3;
            //currNode.CompDateTime = DateTime.Now;
            //var objdata = InitUpdateData(submitOption);
            //objdata.WfState = 3;
            //UpdateObjectState(objdata);
            //var opion = GetOtpionInfo(submitOption, appinst);
            //opion.Result = 5;
            //this.Db.Set<AppInstOpin>().Add(opion);
            //this.Db.SaveChanges();

            return 1;
        }




        /// <summary>
        /// 数据撤回
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int Datadh(int Id) 
        {
            var sinid = Db.Set<AppInst>().Where(a => a.AppObjId == Id).FirstOrDefault().Id;
            StringBuilder strsql = new StringBuilder();
            strsql.Append($"UPDATE ContractInfo SET WfState=3 WHERE id={Id};");//合同备忘
            strsql.Append($"UPDATE AppInst SET AppState=3 WHERE AppObjId={Id};");//合同备忘
            strsql.Append($"UPDATE AppInstNode SET NodeState=3 WHERE InstId ={sinid} AND Type=2;");//计划资金
            strsql.Append($"UPDATE AppInstNodeInfo SET NodeState=3   WHERE InstId ={sinid};");//计划资金历史
            //添加其他标签表
            return ExecuteSqlCommand(strsql.ToString());
        }
    }
}
