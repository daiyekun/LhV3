using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.Common.Models
{
    /// <summary>
    /// Ajax 返回信息
    /// </summary>
    public class AjaxResult
    {
        
        /// <summary>
        /// 调试信息
        /// </summary>
        public string DebugMessage { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 返回结果
        /// </summary>
        public DoResult Result { get; set; }
        /// <summary>
        /// 结果值
        /// </summary>
        public object RetValue { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public object Tag { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; } = 0;
    }

    public enum DoResult
    {  
        /// <summary>
        /// 错误
        /// </summary>
        Failed = 0,
        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,
        /// <summary>
        /// 超时
        /// </summary>
        OverTime = 2,
        /// <summary>
        /// 未授权
        /// </summary>
        NoAuthorization = 3,
        /// <summary>
        /// 其他
        /// </summary>
        Other = 255
    }

    /// <summary>
    /// 请求返回值-其中命名不规范是由于前段限制必须
    /// </summary>
    public class RequstResult
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; } = 0;
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 数据信息
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 结果值
        /// </summary>
        public object RetValue { get; set; } = 0;
        /// <summary>
        /// 标签
        /// </summary>
        public object Tag { get; set; }
        /// <summary>
        /// Access_Token
        /// </summary>
        public string access_token { get; set; }

        public int User_id { get; set; }


    }
    /// <summary>
    /// 请求返回值-其中命名不规范是由于前段限制必须
    /// </summary>
    public class ResultData
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int code { get; set; } = 0;
        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 数据信息
        /// </summary>
        public object data { get; set; }
        /// <summary>
        /// 结果值
        /// </summary>
        public object retvalue { get; set; } = 0;
       


    }
    /// <summary>
    /// 标准返回值
    /// </summary>
    public class Result
    {
        public bool success { get; set; }
        public string msg { get; set; }
    }
    /// <summary>
    /// 合同返回返回值
    /// </summary>
    public class ResultContract
    {
        public string totalCount { get; set; }
        public string items { get; set; }
    }
    public class ResultContractss
    {
        public string Uuid { get; set; }
        public int state { get; set; }
        public DateTime date { get; set; }
        public int UserID { get; set; }
        public string info { get; set; }
    }
    public class TOjson<T> where T: class
    {
        public string totalCount { get; set; }
        public IList<T> items { get; set; }
    }
}
