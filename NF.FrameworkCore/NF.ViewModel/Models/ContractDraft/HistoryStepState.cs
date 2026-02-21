using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 历史纪录 合同文本保存时的状态
    /// </summary>
    public class HistoryStepState
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HistoryStepState()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Exist">已经存在的字符串</param>
        public HistoryStepState(string Exist)
        {
            var lst = StringHelper.String2ArrayInt(Exist, '-', AllowSame: true);
            if (lst.Count > 0)
            {
                this.WfInstID = lst[0];
            }
            if (lst.Count > 1)
            {
                this.WfInstState = lst[1];
            }
            if (lst.Count > 2)
            {
                this.WfInstNodeID = lst[2];
            }
            if (lst.Count > 3)
            {
                this.WfInstVersions = lst[3];
            }
            if (lst.Count > 4)
            {
                this.UserID = lst[4];
            }
        }
        /// <summary>
        /// 审批ID
        /// </summary>
        public int? WfInstID { get; set; }
        /// <summary>
        /// 审批状态
        /// </summary>
        public int? WfInstState { get; set; }
        /// <summary>
        /// 审批节点ID
        /// </summary>
        public int? WfInstNodeID { get; set; }
        /// <summary>
        /// 审批版本
        /// </summary>
        public int? WfInstVersions { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int? UserID { get; set; }

        #region override
        /// <summary>
        /// 到字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}-{1}-{2}-{3}-{4}",
                this.WfInstID ?? 0,
                this.WfInstState ?? 0,
                this.WfInstNodeID ?? 0,
                this.WfInstVersions ?? 0,
                this.UserID ?? 0);
        }
        /// <summary>
        /// 返回该字符串的哈希代码
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        /// <summary>
        /// 是否相等
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var exist = obj as HistoryStepState;
            if (exist == null)
                return false;

            return this.GetHashCode() == exist.GetHashCode();
        }
        #endregion
        /// <summary>
        /// 审批中
        /// </summary>
        public bool IsInWorkflow { get; set; }
        /// <summary>
        /// 可修改
        /// </summary>
        public bool IsWordEdit { get; set; }
    }
}
