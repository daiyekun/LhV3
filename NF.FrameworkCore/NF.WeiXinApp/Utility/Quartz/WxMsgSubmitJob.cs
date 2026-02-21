using Microsoft.AspNetCore.Http;
using NF.ViewModel.Models;
using NF.WeiXin.Lib.Utility;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.WeiXinApp.Utility.Quartz
{
    /// <summary>
    /// 微信消息发送
    /// </summary>
    public class WxMsgSubmitJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            GetWxMsgToSubmit();

            return Task.CompletedTask;
        }
        /// <summary>
        /// 获取微信消息
        /// </summary>
        public void GetWxMsgToSubmit()
        {
            if (RedisHelper.KeyExists("WxMsgList")) { 
             //从redis获取消息
              var opObj = RedisHelper.ListLeftPopToObj<FlowWxMsgInfo>("WxMsgList");
                if (opObj != null)
                {
                   
                    WeiXinMsgUtility.SubmitTextCardMsg(opObj);// 合同
                    WeiXinMsgUtility.SubmitWxNewMsg(opObj);//客户 消息标记1
                }
            }
        }
       
    }
}
