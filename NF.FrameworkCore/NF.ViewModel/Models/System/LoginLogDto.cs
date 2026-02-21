using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 登录日志
    /// </summary>
    public  class LoginLogDTO
    {

        public int Id { get; set; }
        public int? LoginUserId { get; set; }
        public string RequestNetIp { get; set; }
        public string LoginIp { get; set; }
        public byte? Result { get; set; }
        public DateTime? CreateDatetime { get; set; }
        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginUserName { get; set; }
        /// <summary>
        /// 登录结果
        /// </summary>
        public string ResultDic { get; set; }


    }
}
