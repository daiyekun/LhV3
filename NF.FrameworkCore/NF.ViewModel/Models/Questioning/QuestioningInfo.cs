using System;
using System.Collections.Generic;
using System.Text;

 namespace NF.ViewModel.Models
{
  public  class QuestioningInfo : WfBase
    {
        public int Id { get; set; }
        public byte? Inquirer { get; set; }
        public int? ProjectName { get; set; }
        public string ProjectNumber { get; set; }
        public string Sites { get; set; }
        public DateTime? Times { get; set; }
        public int? ContractExecuteBranch { get; set; }
        public string TheWinningConditions { get; set; }
        public byte? Recorder { get; set; }
        public DateTime? UsefulLife { get; set; }
        public byte? IsDelete { get; set; }
        public byte? InState { get; set; }
        public byte? WfState { get; set; }
        public int? CreateUserId { get; set; }
        public int? ModifyUserId { get; set; }
        public int? QueType { get; set; }
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
