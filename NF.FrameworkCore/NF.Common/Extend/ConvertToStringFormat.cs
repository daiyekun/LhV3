using System;
using System.Collections.Generic;
using System.Text;

namespace NF.Common.Extend
{
    /// <summary>
    /// 特定类型转换成字符串格式
    /// </summary>
    public static class ConvertToStringFormat
    {
        /// <summary>
        /// 转换日期:将格式转换成yyyy-MM-dd
        /// </summary>
        /// <param name="date">linq读出的datetime</param>
        /// <returns>yyyy-MM-dd格式的字符串，为空或异常返回string.Empty</returns>
        public static string ConvertDate(this DateTime? date)
        {
            try
            {
                if (date == null)
                {
                    return string.Empty;
                }
                string dd = date.Value.ToString("yyyy-MM-dd");
                return dd;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 转换日期:将格式转换成yyyy-MM-dd
        /// </summary>
        /// <param name="date">linq读出的datetime</param>
        /// <returns>yyyy-MM-dd格式的字符串，为空或异常返回string.Empty</returns>
        public static string ConvertDate(this DateTime date)
        {
            try
            {
              
                string dd = date.ToString("yyyy-MM-dd");
                return dd;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 将字符串转换成时间格式
        /// </summary>
        /// <param name="stringDate">时间字符串</param>
        /// <returns>yyyy-MM-dd格式的字符串，为空或异常返回string.Empty</returns>
        public static DateTime? StringToDate(this string stringDate)
        {
            try
            {

                return Convert.ToDateTime(stringDate);
            }
            catch
            {
                return null;
            }
        }
    }
}
