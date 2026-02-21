using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 导出请求对象
    /// </summary>
   public class ExportRequestInfo
    {
        /// <summary>
        /// 选择行时选择对象ID集合
        /// </summary>
        public string Ids { get; set; }
        /// <summary>
        /// 选择的标题
        /// </summary>
        public string SelTitle { get; set; }
        /// <summary>
        /// 选择字段
        /// </summary>
        public string SelField { get; set; }
        /// <summary>
        /// true表示是导出选择列
        /// </summary>
        public bool SelCell { get; set; }
        /// <summary>
        /// True表示是导出选择行
        /// </summary>
        public bool SelRow { get; set; }
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string KeyWord { get; set; }
        /// <summary>
        /// 导出（收款、付款）
        /// </summary>
        public int FType { get; set; }
        /// <summary>
        /// 查询类型
        /// </summary>
        public int? search { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? beginData { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? endData { get; set; }
        /// <summary>
        /// 高级查询字符串
        /// </summary>
        public string jsonStr { get; set; }

        /// <summary>
        /// 返回选择的列头
        /// </summary>
        /// <returns></returns>
        public IList<string> GetCellsTitleList()
        {
            return StringHelper.Strint2ArrayString(this.SelTitle);
        }
        /// <summary>
        /// 返回选择的ID集合
        /// </summary>
        /// <returns></returns>
        public IList<int> GetSelectListIds()
        {
            return StringHelper.String2ArrayInt(this.Ids);
        }
        /// <summary>
        /// 返回选择的字段集合
        /// </summary>
        /// <returns></returns>
        public IList<string> GetCellsFieldList()
        {
            return StringHelper.Strint2ArrayString(this.SelField);
        }




    }
}
