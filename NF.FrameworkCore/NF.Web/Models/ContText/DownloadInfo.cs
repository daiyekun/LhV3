using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.Web.Models.ContText
{
    public class DownloadInfo
    {
        /// <summary>
        /// 下载url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 文本ID
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 文件夹索引
        /// </summary>
        public int? Folder { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public int? Dtype { get; set; }

        public int? DownType { get; set; }
        /// <summary>
        /// 是否生成成功
        /// </summary>
        public bool IsYes { get; set; } = false;
        /// <summary>
        /// 当前用户ID
        /// </summary>
        public int UserId { get; set; }

    }
}
