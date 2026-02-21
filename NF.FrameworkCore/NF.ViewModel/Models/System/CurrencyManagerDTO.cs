using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 币种
    /// </summary>
   public  class CurrencyManagerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Code { get; set; }
        public decimal? Rate { get; set; }
        public string Remark { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }
        public string ShortName { get; set; }
    }

    public class CurrencyManagerViewDTO: CurrencyManagerDTO
    {

    }
    /// <summary>
    /// 下拉框选择时数据源
    /// </summary>
    public class CurrencyManagerSelectViewDTO 
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 英文缩写
        /// </summary>
        public string Abbreviation { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
            /// <summary>
            /// 汇率
            /// </summary>
        public decimal? Rate { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        public string ShortName { get; set; }

    }
    /// <summary>
    /// Redis存储币种
    /// </summary>
    public class RedisCurrency: IEntityDTO
    {
        /// <summary>
        /// 币种ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 币种名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 因为缩写
        /// </summary>
        public string Abbreviation { get; set; }
       /// <summary>
       /// 简写
       /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        ///汇率
        /// </summary>
        public decimal? Rate { get; set; }

    }
}
