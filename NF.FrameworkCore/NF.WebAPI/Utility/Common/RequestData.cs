using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.WebAPI.Utility.Common
{
    public class RequestData
    {
        public RequestData()
        {

        }
        /// <summary>
        /// 消息标识码，默认0:成功
        /// </summary>
        public int Code { get; set; } = 0;
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; } = "ok";
        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        public RequestData(int code=0,string msg="ok",object data=null)
        {
            Code = code;
            Msg = msg;
            Data = data;


        }
    }
}
