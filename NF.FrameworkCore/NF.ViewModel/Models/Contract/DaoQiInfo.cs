using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models
{

    public class DaoQi
    {
        /// <summary>
        /// 实体ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 微信账号
        /// </summary>
        public string WxCode { get; set; }
    }

    /// <summary>
    /// 到期合同
    /// </summary>
    public class DqoQiHt: DaoQi
    {
       
        /// <summary>
        /// 合同名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string No { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public int CreateUserId { get; set; }
        /// <summary>
        /// 合同计划截止日期
        /// </summary>
        public DateTime? PlanDate { get; set; }
        /// <summary>
        /// 0:收款，1：付款
        /// </summary>
        public int FinType { get; set; }
    }
    /// <summary>
    /// 到期计划资金
    /// </summary>
    public class DqPlanFinceInfo: DaoQi
    {
       
        /// <summary>
        /// 合同编号
        /// </summary>
        public int ContId { get; set; }
        /// <summary>
        /// 合同名称
        /// </summary>
        public string ContName { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContNo { get; set; }
        /// <summary>
        /// 计划资金名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 计划完成日期
        /// </summary>
        public DateTime? PlanDate { get; set; }
        /// <summary>
        /// 0:收款，1：付款
        /// </summary>
        public int FinType { get; set; }
        /// <summary>
        /// 创建人-合同创建人
        /// </summary>
        public int CreateUserId { get; set; }
        /// <summary>
        /// 计划资金金额
        /// </summary>
        public string MoneryThond { get; set; }



    }

    /// <summary>
    /// 到期提醒实体
    /// </summary>
    public class DaoQiMsg
    {  
        /// <summary>
        /// 0：到期合同
        /// 1：到期计划
        /// </summary>
        public int MsgCode { get; set; }
        /// <summary>
        /// 到期计划
        /// </summary>
        public DqPlanFinceInfo DqPlan { get; set; }
        /// <summary>
        /// 到期合同
        /// </summary>
        public DqoQiHt DqHt { get; set; }


    }

}
