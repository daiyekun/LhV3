using System;
using System.Collections.Generic;
using System.Text;

namespace NF.Common.Utility
{
   public class PublicMethod
    {
        /// <summary>
        /// 处理跨域 Jsonp
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string Jsonp(string str, string callback)
        {
            return string.Format("{0}({1});", callback, str);
        }
        /// <summary>
        /// 除数保留2位小数
        /// </summary>
        /// <param name="Cs">除数</param>
        /// <param name="Bcs">被除数</param>
        /// <returns></returns>
        public static decimal DivisionTWoDec(decimal Cs,decimal Bcs)
        {
             return Math.Round((Cs / Bcs), 2);

           

        }
    }
}
