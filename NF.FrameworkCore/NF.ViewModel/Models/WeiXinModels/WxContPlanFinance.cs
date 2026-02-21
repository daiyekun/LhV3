using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel
{
  public   class WxContPlanFinance
    {
        /// <summary>
        /// 计划资金ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal? AmountMoney { get; set; }
        /// <summary>
        /// 金额千分位
        /// </summary>
        public string AmountMoneyThod { get; set; }
        /// <summary>
        /// 已确认（已完成）
        /// </summary>
        public decimal? ConfirmedAmount { get; set; }
        /// <summary>
        /// 已完成金额千分位
        /// </summary>
        public string ConfirmedAmountThod { get; set; }
        /// <summary>
        /// 余额（未完成）
        /// </summary>
        public string BalanceThod { get; set; }
        /// <summary>
        /// 已提交未确认金额
        /// </summary>
        public decimal? SubAmount { get; set; }
        /// <summary>
        /// 已提交未确认金额千分位
        /// </summary>
        public string SubAmountThod { get; set; }
        /// <summary>
        /// 可核销
        /// </summary>
        public decimal? SurplusAmount { get; set; }
        /// <summary>
        /// 可核销=（金额-建立的实际资金金额）
        /// </summary>
        public string SurplusAmountThod { get; set; }
        /// <summary>
        /// 计划完成日期
        /// </summary>
        public DateTime? PlanCompleteDateTime { get; set; }
        /// <summary>
        /// 完成比例
        /// </summary>
        public string CompRate { get; set; }
        /// <summary>
        /// 结算方式
        /// </summary>
        public string SettlModelName { get; set; }
        /// <summary>
        /// 本次核销金额
        /// </summary>
        public decimal? CheckAmount { get; set; }
        public string CheckAmountThod { get; set; }
        /// <summary>
        /// 剩余计划资金
        /// </summary>
        public string SyPlanAmountThod { get; set; }


    }
}
