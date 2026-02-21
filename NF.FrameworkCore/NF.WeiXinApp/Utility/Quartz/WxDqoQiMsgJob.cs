using NF.Common.Utility;
using NF.ViewModel.Models;
using NF.WeiXin.Lib.Utility;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.WeiXinApp.Utility.Quartz
{
    public class WxDqoQiMsgJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            GetWxDqMsgToSubmit();

            return Task.CompletedTask;
        }
        /// <summary>
        /// 获取微信消息
        /// </summary>
        public void GetWxDqMsgToSubmit()
        {
            if (WeiXin.Lib.Utility.RedisHelper.KeyExists("DaoQiWxMsgList"))
            {
                //从redis获取消息
                var opObj = WeiXin.Lib.Utility.RedisHelper.ListLeftPopToObj<DaoQiMsg>("DaoQiWxMsgList");
                if (opObj != null)
                {

                    WeiXinMsgUtility.SubmitWxDqMsg(opObj);
                   
                }
            }
        }
    }
}
