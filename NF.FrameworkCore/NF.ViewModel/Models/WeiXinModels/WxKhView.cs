using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel
{
  public partial  class WxKhView
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public string  Code { get; set; }
        public int? CreateUserId { get; set; }
        public int? Cstate { get; set; }
        public string FirstContact { get; set; }
        public string  FirstContactMobile { get; set; }
        public string FirstContactTel { get; set; }
        public string  FirstContactPosition { get; set; }
        public int? CareditId { get; set; }
        public int? LevelId { get; set; }
        public int? CompClassId { get; set; }
        public int? PrincipalUserId { get; set; }
        public string  FirstContactEmail { get; set; }
        public int? Ctype { get; set; }
        public string  CreateUserDisplayName { get; set; }
        public string  CstateDic { get; set; }
        public string CareditName { get; set; }
        public string  LevelName { get; set; }
        public string CompanyTypeClass { get; set; }
        public string  PrincipalUserDisplayName { get; set; }
        /// <summary>
        /// 流程状态
        /// </summary>
        public int? WfState { get; set; }
        /// <summary>
        /// 流程状态
        /// </summary>
        public string WfStateDic { get; set; }
        /// <summary>
        /// 当前节点
        /// </summary>
        public string WfCurrNodeName { get; set; }
        /// <summary>
        /// 审批事项
        /// </summary>
        public string WfItemDic { get; set; }
        public int? WfItem { get; set; }


    }
}
