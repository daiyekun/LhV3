using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 交付描述
    /// </summary>
   public class ContSubDesDTO
    {
        /// <summary>
        /// 交付描述ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 交付方式
        /// </summary>
        public int? DeliverType { get; set; }
        /// <summary>
        /// 交付地点
        /// </summary>
        public string DeliverLocation { get; set; }
        /// <summary>
        /// 交付人ID
        /// </summary>
        public int? DeliverUserId { get; set; }
        /// <summary>
        /// 实际交付日期
        /// </summary>
        public DateTime? ActualDateTime { get; set; }
        /// <summary>
        /// Guid附件名称
        /// </summary>
        public string GuidFileName { get; set; }
        /// <summary>
        /// 文件夹名称
        /// </summary>
        public string FolderName { get; set; }
        /// <summary>
        /// 文件名称，不带后缀
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 文件名称，带后缀
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 附件路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 备用1
        /// </summary>
        public string Field1 { get; set; }
        /// <summary>
        /// 备用2
        /// </summary>
        public string Field2 { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
         public string Remark { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public int CreateUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }
        /// <summary>
        /// 修改人ID
        /// </summary>
        public int ModifyUserId { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyDateTime { get; set; }
        /// <summary>
        /// 状态，默认0
        /// </summary>
        public byte? Dstate { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public byte? IsDelete { get; set; }

    }
    /// <summary>
    /// 交付项
    /// </summary>
    public class DevSubItem
    {
        /// <summary>
        /// 标的ID
        /// </summary>
        public int SubId { get; set; }
        /// <summary>
        /// 当前交付数量
        /// </summary>
        public decimal? CurrNumber { get; set; }
        /// <summary>
        /// 交付以后剩余交付数量
        /// </summary>
        public decimal? NotNumber { get; set; }
        /// <summary>
        /// 交付之前剩余交付数量
        /// </summary>
        public decimal? YanShiNum { get; set; }



    }
}
