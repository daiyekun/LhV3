
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models
{
   public  class ProjectWxModel
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 项目类被
        /// </summary>
        public string ProjTypeName { get; set; }
        /// <summary>
        /// 项目类别id
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 项目状态
        /// </summary>
        public string XmState { get; set; }
        /// <summary>
        /// 项目状态 id
        /// </summary>
        public int? Pstate { get; set; }

    }
}
public class ProjectViewWxModel : ProjectWxModel
{
    // 负责人
    /// <summary>
    /// 项目负责人Name
    /// </summary>
    public string XmFzrName { get; set; }
    /// <summary>
    /// 项目负责人Id
    /// </summary>
    public int? PrincipalUserId { get; set; }
    //预算收款、
    /// <summary>
    /// 项目预算收款
    /// </summary>
    public string XmYsSk { get; set; }
    //收款币种
    public int? XmSkBz { get; set; }
    /// <summary>
    /// 项目收款币种Name
    /// </summary>
    public string XmSkBzName { get; set; }
    //预算付款
    /// <summary>
    /// 项目预算付款
    /// </summary>
    public string XmYsFk { get; set; }
    //付款币种
    /// <summary>
    /// 项目付款币种
    /// </summary>
    public int? XmFkBz { get; set; }
    /// <summary>
    /// 项目付款币种Name
    /// </summary>
    public string XmFkBzName { get; set; }
    //计划开始时间
    /// <summary>
    /// 项目计划开始时间
    /// </summary>
    public DateTime? XmJhKsSj { get; set; }
    //计划完成时间
    /// <summary>
    /// 项目计划完成时间
    /// </summary>
    public DateTime? XmJhWcsSj { get; set; }
    // 实际开始时间
    /// <summary>
    /// 项目实际开始时间
    /// </summary>
    public DateTime? XmSjKsSj { get; set; }
    //实际完成时间
    /// <summary>
    /// 项目实际完成时间
    /// </summary>
    public DateTime? XmSjWcSj { get; set; }
    //建立人
    /// <summary>
    /// 项目建立人Name
    /// </summary>
    public string XmJlrName { get; set; }
    //建立日期
    /// <summary>
    /// 项目建立日期
    /// </summary>
    public DateTime XmJlRq { get; set; }
    //项目来源
    /// <summary>
    /// 项目来源
    /// </summary>
    public string XmLy { get; set; }
    //备用1
    /// <summary>
    /// 项目备用1
    /// </summary>
    public string Reserve1 { get; set; }
    //备用2
    /// <summary>
    /// 项目备用2
    /// </summary>
    public string Reserve2 { get; set; }
    /// <summary>
    /// 项目预算收款
    /// </summary>
    public decimal? BugetCollectAmountMoney { get; set; }
    /// <summary>
    /// 项目预算付款
    /// </summary>
    public decimal? BudgetPayAmountMoney { get; set; }

    public int WfItem { get; set; }
    public byte? WfState { get; set; }
    public int? ProjectSource { get; set; }
    public string ProjectSourceName { get; set; }

}