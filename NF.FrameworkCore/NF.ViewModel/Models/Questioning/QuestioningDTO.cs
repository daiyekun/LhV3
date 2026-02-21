using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    public class QuestioningDTO
    {
        /// <summary>
        /// id 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 询价人
        /// </summary>
        public byte? Inquirer { get; set; }
        public string InquirerNameS { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public int? ProjectName { get; set; }

        public string  ProjectNames { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectNumber { get; set; }
        /// <summary>
        /// 地点
        /// </summary>
        public string Sites { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? Times { get; set; }
        /// <summary>
        /// 合同执行部门
        /// </summary>
        public int? ContractExecuteBranch { get; set; }

        public string  ContractExecuteBranchName { get; set; }
        /// <summary>
        /// 中标条件
        /// </summary>
        public string TheWinningConditions { get; set; }
        /// <summary>
        /// 记录人
        /// </summary>
        public string RecorderS  { get; set; }
        public byte? Recorder { get; set; }
        /// <summary>
        /// 约谈类别
        /// </summary>
        public int? QueType { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime? UsefulLife { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>

        public byte? IsDelete { get; set; }
        /// <summary>
        /// 询价状态
        /// </summary>
        public byte? InState { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int? CreateUserId { get; set; }
        public byte? WfState { get; set; }

        public string  QueTypeName { get; set; }
        public int Zbdw { get; set; }
        public decimal Zje { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        ///
        //public int? ModifyUserId { get; set; }
        //public string RecorderName { get; set; }
        //public string DeptName { get; set; }
        //public byte? DeptId { get; set; }
        //public string MdeptName { get; set; }
        //public string InquirerName { get; set; }
    }
    public class QuestioningListDTO : INfEntityHandle
    {
        public decimal Zje { get; set; }
        public string Zjethis { get; set; }
        public int? Zbdw { get; set; }
        public string ZbdwName { get; set; }
        /// <summary>
        /// id 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 询价人
        /// </summary>
        public byte? Inquirer { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectNumber { get; set; }
        /// <summary>
        /// 地点
        /// </summary>
        public string Sites { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? Times { get; set; }
        /// <summary>
        /// 合同执行部门
        /// </summary>
        public byte? ContractExecuteBranch { get; set; }
        /// <summary>
        /// 中标条件
        /// </summary>
        public string TheWinningConditions { get; set; }
        /// <summary>
        /// 记录人
        /// </summary>
        public byte? Recorder { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime? UsefulLife { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>

        public byte? IsDelete { get; set; }
        /// <summary>
        /// 询价状态
        /// </summary>
        public byte? InState { get; set; }
        public string InStateDic { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int? CreateUserId { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public int? ModifyUserId { get; set; }
        public string RecorderName { get; set; }
        public string DeptName { get; set; }
        public byte? DeptId { get; set; }
        public string MdeptName { get; set; }
        public int? QueType { get; set; }
        public string QueTypeName { get; set; }
        public byte? WfStae { get; set; }
        public int ProjectId { get; set; }

        //public FieldInfo GetPropValue(string propName)
        //{
        //    throw new NotImplementedException();
        //}

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
