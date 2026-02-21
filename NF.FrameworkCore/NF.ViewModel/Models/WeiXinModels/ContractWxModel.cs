using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 列表/查看公用
    /// </summary>
    public class ContractWxModel
    {
        /// <summary>
        /// 合同ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 合同名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 合同对方
        /// </summary>
        public string ComName { get; set; }

    }
    /// <summary>
    /// 用于列表
    /// </summary>
    public class ContractListWxModel : ContractWxModel
    {


    }
    /// <summary>
    /// 用于查看页面
    /// </summary>
    public class ContractViewWxModel : ContractWxModel
    {
        /// <summary>
        /// 合同类别
        /// </summary>
        public string ContClssName { get; set; }
        /// <summary>
        /// 签约主体
        /// </summary>
        public string MdeptName { get; set; }
        /// <summary>
        /// 合同金额
        /// </summary>
        public string HtJe { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjName { get; set; }
        /// <summary>
        /// 对方编号
        /// </summary>
        public string OtherCode { get; set; }
        /// <summary>
        /// 执行部门
        /// </summary>
        public string Dname { get; set; }
        /// <summary>
        /// 签订日期
        /// </summary>
        public string Sdate { get; set; }
        /// <summary>
        /// 生效日期
        /// </summary>
        public string EfDate { get; set; }
        /// <summary>
        /// 计划完成日期
        /// </summary>
        public string Pdate { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string Fzr { get; set; }
        /// <summary>
        /// 合同属性
        /// </summary>
        public string ContPro { get; set; }
        /// <summary>
        /// 合同状态
        /// </summary>
        public string HtZt { get; set; }
        /// <summary>
        ///创建人
        /// </summary>
        public string Cjr { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string Cjsj { get; set; }
        /// <summary>
        /// 审批事项
        /// </summary>
        public int WfItem { get; set; }
        /// <summary>
        /// 类别ID
        /// </summary>
        public int HtLbId { get; set; }
        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal HtJeD { get; set; }
        /// <summary>
        /// 合同金额- 用于资金详情页查看
        /// </summary>
        public string ContAmThod { get; set; }
        /// <summary>
        /// 合同对方- 用于资金详情页查看
        /// </summary>
        public string CompName { get; set; }
        /// <summary>
        /// 合同完成金额- 用于资金详情页查看
        /// </summary>
        public string HtWcJeThod { get; set; }
        /// <summary>
        /// 完成比例- 用于资金详情页查看
        /// </summary>
        public string HtWcBl { get; set; }
        /// <summary>
        /// 票款差- 用于资金详情页查看
        /// </summary>
        public string PiaoKaunCha { get; set; }
        /// <summary>
        /// 发票金额- 用于资金详情页查看
        /// </summary>
        public string FaPiaoThod { get; set; }
        /// <summary>
        /// 合同金额- 用于资金详情页查看
        /// </summary>
        public decimal? AmountMoney { get; set; }
        /// <summary>
        /// 合同项目内容
        /// </summary>
        public string HtXmnr { get; set; }
    }

  
    
}
