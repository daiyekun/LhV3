using Newtonsoft.Json;
using NF.Common.Utility;
using NF.WeiXinApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.WeiXinApp.Extend
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
        public static string ToWxJson(this RequestData requestData,bool istime=false)
        {
            try
            {
                
                return JsonUtility.SerializeObject(requestData,istime);

            }
            catch (Exception ex)
            {

                Log4netHelper.Error(ex.Message);
                return "";
            }

        }

       

        public static string ToWxJson<T>(this LayPageInfo<T> laypage)
             where T : class, new()
        {
            try
            {
                return JsonUtility.SerializeObject(laypage);
            }
            catch (Exception ex)
            {

                Log4netHelper.Error(ex.Message);
                return "";
            }

        }
    }
}
