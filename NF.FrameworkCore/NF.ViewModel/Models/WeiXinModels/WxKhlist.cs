using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel
{
    public class WxKhlist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Cstate { get; set; }
        public int Ctype { get; set; }
        public int? CompClassId { get; set; }
        public string CstateDic { get; set; }

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

        public string   CompanyTypeClass {get;set;}

    }
}
