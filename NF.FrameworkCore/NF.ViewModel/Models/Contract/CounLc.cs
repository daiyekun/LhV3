using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 合同新建修改页提交流程验证信息类
    /// </summary>
  public  class CounLc
    {
        /// <summary>
        /// 部门id
        /// </summary>
        public int? DeptId { get; set; }
        /// <summary>
        /// ContTypeId//类别
        /// </summary>
        public int? ContTypeId { get; set; }
        /// <summary>
        /// Name //合同名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Code // 合同编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///   AmountMoney//合同金额
        /// </summary>
        public decimal? AmountMoney { get; set; }
        /// <summary>
        /// 合同id
        /// </summary>
        public int Id { get; set; }
    }
}
