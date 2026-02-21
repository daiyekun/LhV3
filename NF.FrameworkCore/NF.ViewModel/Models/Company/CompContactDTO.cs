using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 其他联系人
    /// </summary>
    public class CompContactDTO
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public string Name { get; set; }
        public string DeptName { get; set; }
        public string Position { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Im { get; set; }
        public string Remark { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }
    }
    /// <summary>
    /// 联系人显示字段
    /// </summary>
    public class CompContactViewDTO: CompContactDTO
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserDisplyName { get; set; }

    }
    /// <summary>
    /// 合同对方资金统计
    /// </summary>
    public class FundCalcul
    {
        /// <summary>
        /// 合同金额
        /// </summary>
        public string HtJeThod { get; set; }
        /// <summary>
        /// 已收/已付款金额
        /// </summary>
        public string CompAtmThod { get; set; }
        /// <summary>
        /// 未收未付金额
        /// </summary>
        public string NoCompAtmThod { get; set; }
        /// <summary>
        /// 已开/收票金额
        /// </summary>
        public string CompInThod { get; set; }
        /// <summary>
        /// 未开/收票金额
        /// </summary>
        public string NoCompInThod { get; set; }
        /// <summary>
        /// 财务应收/付
        /// </summary>
        public string CaiYsThod { get; set; }
        /// <summary>
        /// 财务预收/付
        /// </summary>
        public string CaiYjThod { get; set; }

    }


}
