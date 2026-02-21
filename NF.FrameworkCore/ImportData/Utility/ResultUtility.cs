using NF.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.Web.Utility
{
    /// <summary>
    /// 提示输出消息
    /// </summary>
    public class ResultUtility
    {
        /// <summary>
        /// 操作成功消息
        /// </summary>
        /// <returns></returns>
        public static CustomResultJson ResultMsg()
        {
            return new CustomResultJson(new RequstResult()
            {
                Msg = "操作成功",
                Code = 0,


            });

        }
        /// <summary>
        /// 扩展。传递消息参数输出
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static CustomResultJson ResultMsg(string msg)
        {
            return new CustomResultJson(new RequstResult()
            {
                Msg = msg,
                Code = 0,


            });

        }
    }
}
