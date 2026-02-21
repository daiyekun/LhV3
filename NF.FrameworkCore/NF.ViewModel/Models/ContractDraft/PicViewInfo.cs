using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 图片预览类
    /// </summary>
   public class PicViewInfo
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 相册ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 开始
        /// </summary>
        public int start { get; set; } = 0;
        /// <summary>
        /// 数据
        /// </summary>
        public IList<PicData> data { get; set; }
             
    }
    /// <summary>
    /// 图片数据
    /// </summary>
    public class PicData
    {
        /// <summary>
        /// 图片名
        /// </summary>
        public string alt { get; set; }
        /// <summary>
        /// 图片ID
        /// </summary>
        public int pid { get; set; }
        /// <summary>
        /// 图片源地址
        /// </summary>
        public string src { get; set; }
        /// <summary>
        /// 缩略图地址
        /// </summary>
        public string thumb { get; set; }


    }
}
