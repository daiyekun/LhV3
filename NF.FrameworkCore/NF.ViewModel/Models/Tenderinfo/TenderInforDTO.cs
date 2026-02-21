using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 招标添加
    /// </summary>
   public class TenderInforDTO
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
        public int CreateUserId { get; set; }
        public int IsDelete { get; set; }
        /// <summary>
        /// 招标类型
        /// </summary>
        public int? TenderType { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TenderTypeName { get; set; }
        public string RecorderName { get; set; }
        public int Zbdw { get; set; }
        public decimal Zje { get; set; }
    }

    /// <summary>
    /// 招标列表查看
    /// </summary>
    public class TenderInforListViewDTO : INfEntityHandle
    {
        public decimal Zje { get; set; }
        public string Zjethis { get; set; }
        public string ZbswName { get; set; }
        public int? Zbdw { get; set; }
        /// <summary>
        /// ID 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///招标人Id
        /// </summary>
        public int TenderUserId { get; set; }
        /// <summary>
        ///招标人Name 
        /// </summary>
        public string TenderUserNAME { get; set; }
        /// <summary>
        ///项目Id
        /// </summary>
        public int ProjectId { get; set; }
        /// <summary>
        ///项目名称 
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        ///项目编号 
        /// </summary>
        public string ProjectNO { get; set; }
        /// <summary>
        ///地点 
        /// </summary>
        public string Iocation { get; set; }
        /// <summary>
        ///时间 
        /// </summary>
        public DateTime TenderDate { get; set; }
        /// <summary>
        ///执行部门Id
        /// </summary>
        public int ContractEnforcementDepId { get; set; }
        /// <summary>
        ///合同执行部门 
        /// </summary>
        public string ContractEnforcementDepName { get; set; }
        /// <summary>
        ///中标条件 
        /// </summary>
        public string WinningConditions { get; set; }
        /// <summary>
        ///记录人ID 
        /// </summary>
        public int RecorderId { get; set; }
        /// <summary>
        ///记录人 
        /// </summary>
        public string RecorderName { get; set; }
        /// <summary>
        ///有效期 
        /// </summary>
        public DateTime TenderExpirationDate { get; set; }
        /// <summary>
        ///状态 
        /// </summary>
        public string TenderStatus { get; set; }
        /// <summary>
        ///状态ID
        /// </summary>
        public int TenderStatusId { get; set; }
        /// <summary>
        /// 招标类型
        /// </summary>
        public int? TenderType { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TenderTypeName { get; set; }

        public FieldInfo GetPropValue(string propName)
        {
            var obj = this.GetType().GetProperty(propName);
            return new FieldInfo
            {
                FileType = obj.PropertyType,
                FileValue = obj.GetValue(this, null)
            };

        }
    }
}
