using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NF.Common.Extend;
using NF.ViewModel.Models.Utility;
using NF.ViewModel.Extend.Enums;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NF.BLL.Common;
using System.Globalization;
using System.Diagnostics;
using NF.BLL.Extend;

namespace NF.BLL
{
    /// <summary>
    /// 计划资金
    /// </summary>
   public partial  class ContPlanFinanceService
    {
        /// <summary>
        /// 保存计划资金
        /// </summary>
        /// <param name="planFinance">计划资金</param>
        /// <param name="planFinanceHistory">计划资金历史</param>
        /// <returns>Id:->Hid:</returns>
        public Dictionary<string, int> AddSave(ContPlanFinance planFinance, ContPlanFinanceHistory planFinanceHistory)
        {
            var inof = Add(planFinance);
            return SaveFinceHistory(inof, planFinanceHistory);
        }
        public ContPlanFinance AddSave(ContPlanFinance planFinance,bool IsFrameWorkCont=false)
        {
            var inof = Add(planFinance);
            if (IsFrameWorkCont) { 
            UpdateContAmount(inof.ContId??0);
            }

            return inof;

        }
        /// <summary>
        /// 修改合同金额
        /// </summary>
        public void UpdateContAmount(int ContId)
        {
             var contInfo = Db.Set<ContractInfo>().Find(ContId);
            if (contInfo.IsFramework == 1)
            {
             var sumamount = _ContPlanFinanceSet.AsNoTracking().Where(a => a.ContId == ContId&&a.IsDelete==0).Sum(a => a.AmountMoney ?? 0);
             string sqlstr = $"update ContractInfo set AmountMoney={sumamount} where Id={ContId}";
             ExecuteSqlCommand(sqlstr);
            }
        }

        /// <summary>
        /// 修改计划资金
        /// </summary>
        /// <param name="planFinance">计划资金</param>
        /// <param name="planFinanceHistory">计划资金历史</param>
        /// <returns>Id:计划资金ID、Hid:计划资金历史ID</returns>
        public Dictionary<string, int> UpdateSave(ContPlanFinance planFinance, ContPlanFinanceHistory planFinanceHistory)
        {
             Update(planFinance);
            //保存历史表
            planFinanceHistory.CreateDateTime = DateTime.Now;
            return SaveFinceHistory(planFinance, planFinanceHistory);

        }
        /// <summary>
        /// 修改合同
        /// </summary>
        /// <param name="planFinance">修改合同对象</param>
        /// <returns></returns>
        public ContPlanFinance UpdateSave(ContPlanFinance planFinance, bool IsFrameWorkCont = false)
        {
            Update(planFinance);
            var contInfo = Db.Set<ContractInfo>().Find(planFinance.ContId);
            if (contInfo == null)
            {
                return planFinance;
            }
            else { 
            if (contInfo.IsFramework == 1 && contInfo != null)
            {
                UpdateContAmount(planFinance.ContId ?? 0);
            } 
             return planFinance;
            }
           
        }
        #region 千分位转数子
        public decimal? ParseThousandthString(string strnum)
        {
            decimal? _value = 0;
            if (!string.IsNullOrEmpty(strnum))
            {
                try
                {
                 return   _value = int.Parse(strnum, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                }
                catch (Exception ex)
                {
                    _value = 0;
                    Debug.WriteLine(string.Format("将千分位字符串{0}转换成数字异常，原因:{0}", strnum, ex.Message));
                }
            }
            return _value;
        }
        #endregion
        #region 根据部门名称查询id
        public int DepartmentID(string name) {

            var id = 0;
            try
            {
                id = Db.Set<Department>().Where(a => a.Name == name).FirstOrDefault().Id;
                return id;
            }
            catch (Exception)
            {
                return id;
            }
        
        
        }

        #endregion

        /// <summary>
        /// 保存历史
        /// </summary>
        /// <param name="planFinance">计划资金</param>
        /// <param name="planFinanceHistory">计划资金历史</param>
        /// <returns></returns>
        private Dictionary<string, int> SaveFinceHistory(ContPlanFinance planFinance, ContPlanFinanceHistory planFinanceHistory)
        {
            planFinanceHistory.PlanFinanceId = planFinance.Id;
            planFinanceHistory.CreateDateTime = DateTime.Now;
            Db.Set<ContPlanFinanceHistory>().Add(planFinanceHistory);
            Db.SaveChanges();
            var dic = new Dictionary<string, int>();
            dic.Add("Id", planFinance.Id);
            dic.Add("Hid", planFinanceHistory.Id);
            return dic;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<ContPlanFinanceViewDTO> GetList<s>(PageInfo<ContPlanFinance> pageInfo, Expression<Func<ContPlanFinance, bool>> whereLambda, Expression<Func<ContPlanFinance, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Ftype= a.Ftype,
                            AmountMoney = a.AmountMoney,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            PlanCompleteDateTime= a.PlanCompleteDateTime,
                            Fstate= a.Fstate,
                            Remark=a.Remark,
                            SettlementModes=a.SettlementModes,//结算方式
                                                              //CreateUserName = a.CreateUser.DisplyName
                            ContId = a.ContId

                        };
            var local = from a in query.AsEnumerable()
                        select new ContPlanFinanceViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Ftype = a.Ftype,
                            AmountMoney = a.AmountMoney,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            PlanCompleteDateTime = a.PlanCompleteDateTime,
                            Fstate = a.Fstate,
                            Remark = a.Remark,
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                          //  RedisValueUtility.GetUserShowName(a.CreateUserId),
                            AmountMoneyThod=a.AmountMoney.ThousandsSeparator(),
                            SettlModelName= this.Db.GetRedisDataDictionaryValue(a.SettlementModes ?? 0, DataDictionaryEnum.SettlModes),
                            //DataDicUtility.GetDicValueToRedis(a.SettlementModes, DataDictionaryEnum.SettlModes),
                         SumHtje = Sumht(a.ContId)

                        };
            return new LayPageInfo<ContPlanFinanceViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0
            };
        }
        /// <summary>
        /// 求当前合同下计划资金之和
        /// </summary>
        /// <param name="id">合同id</param>
        /// <returns></returns>
        public string Sumht(int? id)
        {
            var sumje = "0";
            //var conid = id > 0 ? id :-id;
            //if (id>0)
            //{
            //    conid = id;
            //}
            //else
            //{
            //    conid = id;
            //}
            var je = Db.Set<ContPlanFinance>().Where(a =>a.ContId == id  && a.IsDelete == 0).Sum(q => q.AmountMoney);
            if (id > 0)
            {
                Xgje(je, id);
            }
            sumje = je.ThousandsSeparator();
            return sumje;
        }
        public void Xgje(decimal? je, int? id)
        {
            string sqlstr = $"update ContractInfo set AmountMoney='{je}' where Id={id}";
            ExecuteSqlCommand(sqlstr);
        }
        /// <summary>
        /// 查询计划资金大列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<ContPlanFinanceListViewDTO> GetMainList<s>(PageInfo<ContPlanFinance> pageInfo, Expression<Func<ContPlanFinance, bool>> whereLambda, Expression<Func<ContPlanFinance, s>> orderbyLambda, bool isAsc)
        {
            //var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var tempquery = Db.Set<ContPlanFinance>().Include(a=>a.Cont)
               .ThenInclude(a=>(a.Project)).Include(a=>a.Cont).ThenInclude(a=>a.Comp)
                .AsTracking().Where<ContPlanFinance>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<ContPlanFinance>))
                tempquery = tempquery.Skip<ContPlanFinance>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<ContPlanFinance>(pageInfo.PageSize);
            
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Ftype = a.Ftype,
                            AmountMoney = a.AmountMoney,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            PlanCompleteDateTime = a.PlanCompleteDateTime,
                            Fstate = a.Fstate,
                            Remark = a.Remark,
                            SettlementModes = a.SettlementModes,//结算方式
                            ContName= a.Cont==null?"":a.Cont.Name,
                            ContCode= a.Cont == null ? "" : a.Cont.Code,
                            ContCategoryId= a.Cont == null ? -1 : a.Cont.ContTypeId,
                            //ContCategoryName=a.Cont.ContType.Name,
                            // PrincipalUserName=a.Cont.PrincipalUser.Name,
                            PrincipalUserId = a.Cont == null ? -1 : a.Cont.PrincipalUserId,
                            CompName = (a.Cont != null && a.Cont.Comp != null)?a.Cont.Comp.Name:"",
                            CompClassId = (a.Cont != null && a.Cont.Comp != null) ? a.Cont.Comp.CompClassId:-1,
                            //DeptName=a.Cont.Dept.Name,
                            DeptId = a.Cont == null ? -1 : a.Cont.DeptId,
                            ProjectName =(a.Cont!=null&& a.Cont.Project!=null)?a.Cont.Project.Name:"",
                            CompId= a.Cont == null ? -1 : a.Cont.CompId,
                            ProjId= a.Cont == null ? -1 : a.Cont.ProjectId,
                            ContId=a.ContId,
                            ConfirmedAmount=a.ConfirmedAmount,//已确认金额
                            ActSettlementDate=a.ActSettlementDate,//完成时间
                            CurrencyId= a.Cont == null ? -1 : a.Cont.CurrencyId,//币种ID

                        };
         
            var local = from a in query.AsEnumerable()
                        select new ContPlanFinanceListViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Ftype = a.Ftype,
                            AmountMoney = a.AmountMoney,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            PlanCompleteDateTime = a.PlanCompleteDateTime,
                            Fstate = a.Fstate,
                            Remark = a.Remark,
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                         //   RedisValueUtility.GetUserShowName(a.CreateUserId),
                            AmountMoneyThod = a.AmountMoney.ThousandsSeparator(),
                            SettlModelName = this.Db.GetRedisDataDictionaryValue(a.SettlementModes ?? 0, DataDictionaryEnum.SettlModes),
                         //   DataDicUtility.GetDicValueToRedis(a.SettlementModes, DataDictionaryEnum.SettlModes),
                            ContName = a.ContName,
                            ContCode = a.ContCode,
                            ContCategoryName = this.Db.GetRedisDataDictionaryValue(a.ContCategoryId ?? 0, DataDictionaryEnum.contractType),
                            //DataDicUtility.GetDicValueToRedis(a.ContCategoryId, DataDictionaryEnum.contractType), //a.ContCategoryName,
                            PrincipalUserName = this.Db.GetRedisUserFieldValue(a.PrincipalUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.PrincipalUserId??0), //a.PrincipalUserName,
                            CompName =a.CompName,
                            CompTypeName = CompanyUtility.CompanyTypeClass(a.CompClassId, (a.Ftype==0?0:1)), //a.CompTypeName,
                            DeptName = this.Db.GetRedisDeptNameValue(a.DeptId ?? -2),
                            //RedisValueUtility.GetDeptName(a.DeptId??-1), //a.DeptName,
                            ProjectName = a.ProjectName,
                            CompAmountThod= a.ConfirmedAmount.ThousandsSeparator(),//完成金额
                            CompAmount= a.ConfirmedAmount??0,
                            BalanceThod =((a.AmountMoney??0)- (a.ConfirmedAmount??0)).ThousandsSeparator(),//余额
                            Balance= ((a.AmountMoney ?? 0) - (a.ConfirmedAmount ?? 0)),
                            ActSettlementDate =a.ActSettlementDate,//实际完成日期
                            ContActBl= GetWcBl(a.AmountMoney,a.ConfirmedAmount),//完成比例
                            CompId = a.CompId,
                            ProjId = a.ProjId,
                            ContId = a.ContId,
                            CurrencyName= RedisValueUtility.GetCurrencyName(a.CurrencyId),//币种
                        };
           
            return new LayPageInfo<ContPlanFinanceListViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };


        }
        /// <summary>
        /// 计算比例
        /// </summary>
        /// <param name="amount">计划资金金额</param>
        /// <param name="wcje">完成金额</param>
        /// <returns>完成比例</returns>
        private string GetWcBl(decimal? amount,decimal?wcje)
        {
            return (((amount ?? 0) == 0 || (wcje ?? 0) == 0) ? 0 : ((wcje ?? 0) / (amount ?? 0))).ConvertToPercent();
             
        }
       
        public LayPageInfo<ContPlanFinanceViewSecoundDTO> GetListSecod<s>(PageInfo<ContPlanFinance> pageInfo, Expression<Func<ContPlanFinance, bool>> whereLambda, Expression<Func<ContPlanFinance, s>> orderbyLambda, bool isAsc,int ActId)
        {
            //var plancklist = this.Db.Set<PlanFinnCheck>().AsNoTracking().Where(a => a.ActualFinanceId == ActId && a.IsDelete == 0).ToList();
            var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Ftype = a.Ftype,
                            AmountMoney = a.AmountMoney,
                            PlanCompleteDateTime = a.PlanCompleteDateTime,
                            SettlementModes = a.SettlementModes,//结算方式
                            ConfirmedAmount=a.ConfirmedAmount,//已完成
                            SubAmount=a.SubAmount,//已提交
                            SurplusAmount= a.SurplusAmount,//可核销
                            CheckAmount=a.CheckAmount,//本次核销

                        };
            var local = from a in query.AsEnumerable()
                        select new ContPlanFinanceViewSecoundDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            AmountMoney = a.AmountMoney,
                            PlanCompleteDateTime = a.PlanCompleteDateTime,
                            AmountMoneyThod = a.AmountMoney.ThousandsSeparator(),
                            SettlModelName = this.Db.GetRedisDataDictionaryValue(a.SettlementModes ?? 0, DataDictionaryEnum.SettlModes),

                            //DataDicUtility.GetDicValueToRedis(a.SettlementModes, DataDictionaryEnum.SettlModes),
                            ConfirmedAmountThod = a.ConfirmedAmount.ThousandsSeparator(),
                            SubAmountThod = a.SubAmount.ThousandsSeparator(),
                            CompRate = ((a.ConfirmedAmount ?? 0) / (a.AmountMoney ?? 0)).ConvertToPercent(),
                            BalanceThod = ((a.AmountMoney ?? 0) - (a.ConfirmedAmount ?? 0)).ThousandsSeparator(),
                            //计划资金-已核销金额（状态1（已确认））
                            //SurplusAmount =(a.AmountMoney??0)-(plancklist.Where(p => p.PlanFinanceId == a.Id&&p.ChkState==1).Sum(p=>(decimal?)p.AmountMoney)??0),
                            //SurplusAmountThod = ((a.AmountMoney??0) - (plancklist.Where(p => p.PlanFinanceId == a.Id && p.ChkState == 1).Sum(p => (decimal?)p.AmountMoney) ?? 0)).ThousandsSeparator(),
                            //CheckAmount= plancklist.Where(p=>p.PlanFinanceId==a.Id).Any()? plancklist.Where(p => p.PlanFinanceId == a.Id).FirstOrDefault().AmountMoney:0,
                            //CheckAmountThod = (plancklist.Where(p => p.PlanFinanceId == a.Id).Any() ? plancklist.Where(p => p.PlanFinanceId == a.Id).FirstOrDefault().AmountMoney : 0).ThousandsSeparator()
                        };
            return new LayPageInfo<ContPlanFinanceViewSecoundDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }
        public LayPageInfo<ContPlanFinanceViewSecoundDTO> GetPlanCheckList<s>(PageInfo<ContPlanFinance> pageInfo, Expression<Func<ContPlanFinance, bool>> whereLambda, Expression<Func<ContPlanFinance, s>> orderbyLambda, bool isAsc, int ActId)
        {
            var plancklist = this.Db.Set<PlanFinnCheck>().AsNoTracking().Where(a => a.ActualFinanceId == ActId && a.IsDelete == 0).ToList();
            var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Ftype = a.Ftype,
                            AmountMoney = a.AmountMoney,
                            //PlanCompleteDateTime = a.PlanCompleteDateTime,
                            //SettlementModes = a.SettlementModes,//结算方式
                            ConfirmedAmount = a.ConfirmedAmount,//已完成
                           

                        };
            var local = from a in query.AsEnumerable()
                        select new ContPlanFinanceViewSecoundDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            AmountMoney = a.AmountMoney,
                            AmountMoneyThod = a.AmountMoney.ThousandsSeparator(),
                            ConfirmedAmount = a.ConfirmedAmount,
                            ConfirmedAmountThod = a.ConfirmedAmount.ThousandsSeparator(),
                            SurplusAmount = (a.AmountMoney ?? 0) - (a.ConfirmedAmount ?? 0),
                            //   SurplusAmountThod = ((a.AmountMoney ?? 0) - (a.ConfirmedAmount ?? 0)).ThousandsSeparator(),
                            SurplusAmountThod = ( (((a.AmountMoney ?? 0) - (this.Db.Set<PlanFinnCheck>().Where(p => p.PlanFinanceId == a.Id && p.IsDelete != 1).Sum(p => (decimal?)p.AmountMoney) ?? 0)))).ThousandsSeparator(),
                            CheckAmount = plancklist.Where(p => p.PlanFinanceId == a.Id).Any() ? plancklist.Where(p => p.PlanFinanceId == a.Id).FirstOrDefault().AmountMoney : 0,
                            CheckAmountThod = (plancklist.Where(p => p.PlanFinanceId == a.Id).Any() ? plancklist.Where(p => p.PlanFinanceId == a.Id).FirstOrDefault().AmountMoney : 0).ThousandsSeparator(),
                            SyPlanAmountThod=((a.AmountMoney??0)- (this.Db.Set<PlanFinnCheck>().Where(p=>p.PlanFinanceId==a.Id&&p.IsDelete!=1).Sum(p=>(decimal?)p.AmountMoney)??0)).ThousandsSeparator()
                        };
            return new LayPageInfo<ContPlanFinanceViewSecoundDTO>()
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
            string sqlstr = "update ContPlanFinance set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids"></param>
        /// <param name="IsFrameWorkCont"></param>
        /// <returns></returns>
        public int Delete(string Ids, bool IsFrameWorkCont = false)
        {
            string sqlstr = "update ContPlanFinance set IsDelete=1 where Id in(" + Ids + ")";
            var res= ExecuteSqlCommand(sqlstr);
            deje(Ids);
            if (IsFrameWorkCont) { 
            var arrids = StringHelper.String2ArrayInt(Ids);
            var contInfo =_ContPlanFinanceSet.Find(arrids[0]);
                UpdateContAmount(contInfo.ContId??0);
            }
            return res;
        }

        public void deje(string id)
        {
            var ids = String2ArrayInt(id);
            var je = Db.Set<ContPlanFinance>().Where(a => ids.Contains(a.Id)).Sum(e => e.AmountMoney);

            var contid = Db.Set<ContPlanFinance>().Where(a => ids.Contains(a.Id)).FirstOrDefault().ContId;
            if (contid > 0)
            {
                var chje = Db.Set<ContractInfo>().Where(a => a.Id == contid).FirstOrDefault().AmountMoney;
                var jeR = chje - je;
                Xgje(jeR, contid);
            }

        }
        public static List<int> String2ArrayInt(string str, char sep = ',', bool AllowSame = false)
        {
            if (str == null || str.Trim() == "")
                return new List<int>();
            string[] arr = str.Split(sep);
            List<int> lst = new List<int>();
            int intTemp;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Trim() == "")
                    continue;
                if (!Int32.TryParse(arr[i], out intTemp))
                    continue;

                if (lst.Contains(intTemp))
                {
                    if (AllowSame == false)
                    {
                        continue;
                    }
                }
                lst.Add(intTemp);
            }
            return lst;
        }

        /// <summary>
        /// 查看或者修改
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public ContPlanFinanceViewDTO ShowView(int Id)
        {
            var query = from a in _ContPlanFinanceSet.AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Ftype = a.Ftype,
                            AmountMoney = a.AmountMoney,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            PlanCompleteDateTime = a.PlanCompleteDateTime,
                            Fstate = a.Fstate,
                            Remark = a.Remark,
                            SettlementModes = a.SettlementModes,//结算方式


                        };
            var local = from a in query.AsEnumerable()
                        select new ContPlanFinanceViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Ftype = a.Ftype,
                            AmountMoney = a.AmountMoney,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            PlanCompleteDateTime = a.PlanCompleteDateTime,
                            Fstate = a.Fstate,
                            Remark = a.Remark,
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                          //  RedisValueUtility.GetUserShowName(a.CreateUserId),
                            AmountMoneyThod = a.AmountMoney.ThousandsSeparator(),
                            SettlModelName = this.Db.GetRedisDataDictionaryValue(a.SettlementModes ?? 0, DataDictionaryEnum.SettlModes),
                            //DataDicUtility.GetDicValueToRedis(a.SettlementModes, DataDictionaryEnum.SettlModes),
                            SettlementModes =a.SettlementModes

                        };
            return local.FirstOrDefault();
        }

        

        


    }
}
