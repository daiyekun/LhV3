using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel
{
  public   class WxContActualFinance
    {

        /// <summary>
        /// ID 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 合同ID
        /// </summary>
        public int? ContId { get; set; }
        /// <summary>
        /// 结算方式
        /// </summary>
        public int? SettlementMethod { get; set; }

        /// <summary>
        /// 资金类型
        /// </summary>
        public byte? FinceType { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal? AmountMoney { get; set; }
        /// <summary>
        /// 币种ID
        /// </summary>
        public int? CurrencyId { get; set; }
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal? CurrencyRate { get; set; }
        /// <summary>
        /// 结算时间
        /// </summary>
        public DateTime? ActualSettlementDate { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string VoucherNo { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public byte? Astate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public int CreateUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }
        /// <summary>
        /// 确认人ID
        /// </summary>
        public int? ConfirmUserId { get; set; }
        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime? ConfirmDateTime { get; set; }
        /// <summary>
        /// 备用1
        /// </summary>
        public string Reserve1 { get; set; }
        /// <summary>
        /// 备用2
        /// </summary>
        public string Reserve2 { get; set; }
        /// <summary>
        /// 银行
        /// </summary>
        public string Bank { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 建立人
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 确认人
        /// </summary>
        public string ConfirmUserName { get; set; }
        /// <summary>
        /// 结算方式
        /// </summary>
        public string SettlementMethodDic { get; set; }
        /// <summary>
        /// 金额千分位
        /// </summary>
        public string AmountMoneyThod { get; set; }
        /// <summary>
        ///实际资金状态
        /// </summary>
        public string AstateDic { get; set; }
        /// <summary>
        /// 流程状态
        /// </summary>
        public byte WfState { get; set; }
        public int? WfItem { get; set; }

    }
}
