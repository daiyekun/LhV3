using NF.Common.Utility;
using NF.WebAPI.Utility.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.WebAPI.Extend
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class RequestDataExtend
    {
        /// <summary>
        /// 将对象转换成JSON
        /// </summary>
        /// <param name="requestData">数据</param>
        /// <returns>返回JSON字符串</returns>
        public static string ToJson(this RequestData requestData)
        {
            try
            {
                return JsonUtility.SerializeObject(requestData);
            }
            catch (Exception ex)
            {

                Log4netHelper.Error(ex.Message);
                return "";
            }

        }
    }
}
