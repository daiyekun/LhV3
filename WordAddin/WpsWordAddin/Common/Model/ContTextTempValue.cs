using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Model
{
    ///// <summary>
    ///// 合同模板变量
    ///// </summary>
    //public class ContTextTempValue
    //{
    //    /// <summary>
    //    /// ID
    //    /// </summary>
    //    public int ID { get; set; }
    //    /// <summary>
    //    /// 名称
    //    /// </summary>
    //    public string NAME { get; set; }
    //    /// <summary>
    //    /// 编号
    //    /// </summary>
    //    public string NO { get; set; }
    //    /// <summary>
    //    /// 是否是系统变量0：系统变量，1：自定义变量
    //    /// </summary>
    //    public byte IS_CUSTOMER { get; set; }
    //    /// <summary>
    //    /// 是否可以删除0：位删除，1删除
    //    /// </summary>
    //    public byte IS_DELETE { get; set; }
    //    /// <summary>
    //    /// 模板ID，通常自定义变量的时候有值
    //    /// </summary>
    //    public int? TEMP_ID { get; set; }
    //    /// <summary>
    //    /// 系统变量的类别,0：合同,1：客户,2：供应商,目前0
    //    /// </summary>
    //    public int? TYPE { get; set; }
    //    /// <summary>
    //    /// 原始ID,版本=1时的ID
    //    /// </summary>
    //    public int? OriginalID { get; set; }
    //}

    /// <summary>
    /// 模板起草时系统变量数据承载
    /// </summary>
    public class ContractVariable
    {
        /// <summary>
        /// 系统变量ID
        /// </summary>
        public String VarName { get; set; }
        /// <summary>
        /// 系统变量名称
        /// </summary>
        public String VarLabel { get; set; }
        /// <summary>
        /// 系统变量内容
        /// </summary>
        public String VarValue { get; set; }

    }
}
  