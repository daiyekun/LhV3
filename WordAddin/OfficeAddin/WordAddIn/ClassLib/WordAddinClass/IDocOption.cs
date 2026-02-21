using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordAddIn.ClassLib.WordAddinClass
{
    /// <summary>
    /// 文档操作接口
    /// </summary>
   public interface IDocOption
    {
        /// <summary>
        /// 保持Word文件到服务器
        /// </summary>
        /// <param name="wordFullName">本地Word文件全路径名称</param>
        /// <returns>返回服务器响应数据</returns>
         string WordSave(string wordFullName = "");
    }
}
