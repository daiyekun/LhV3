using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
   public  class TeInfo : WfBase
    {

        public int Id { get; set; }
        public int TenderUserId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectNo { get; set; }
        public string Iocation { get; set; }
        public DateTime TenderDate { get; set; }
        public int ContractEnforcementDepId { get; set; }
        public string WinningConditions { get; set; }
        public int RecorderId { get; set; }
        public DateTime TenderExpirationDate { get; set; }
        public int TenderStatus { get; set; }
        public int IsDelete { get; set; }
        public int CreateUserId { get; set; }
        public int? TenderType { get; set; }
        public int? WfItem { get; set; }
        public string WfCurrNodeName { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateName { get; set; }
        /// <summary>
        /// 记录人
        /// </summary>
        public string RecorderName { get; set; }
    }
}
