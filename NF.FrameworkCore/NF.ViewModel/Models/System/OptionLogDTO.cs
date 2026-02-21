using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 操作日志
    /// </summary>
   public class OptionLogDTO
    {  

        public int Id { get; set; }
        public int? UserId { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Remark { get; set; }
        public string RequestUrl { get; set; }
        public byte? RequestMethod { get; set; }
        public string RequestData { get; set; }
        public string RequestIp { get; set; }
        public string RequestNetIp { get; set; }
        public double? TotalTime { get; set; }
        public DateTime? CreateDatetime { get; set; }
        public byte? Status { get; set; }
        public string ActionTitle { get; set; }
        public string ExecResult { get; set; }
        public byte? OptionType { get; set; }
        /// <summary>
        /// 操作类型描述
        /// </summary>
        public string OptionTypeDic { get; set; }
        /// <summary>
        /// 登录名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 请求方式
        /// </summary>
        public string RequestMethodDic { get; set; }
    }
}
