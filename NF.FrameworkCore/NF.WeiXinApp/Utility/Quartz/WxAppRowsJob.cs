using NF.Common.Utility;
using NF.ViewModel.Models;
using NF.WeiXin.Lib.Utility;
using Quartz;
using System.Threading.Tasks;
using RedisHelper = NF.Common.Utility.RedisHelper;

namespace NF.WeiXinApp.Utility.Quartz
{
    public class WxAppRowsJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            GetWxAppRowsMsg();

            return Task.CompletedTask;
        }

        /// <summary>
        /// 获取微信消息
        /// 计划到周5发送待处理的身体条数
        /// </summary>
        public void GetWxAppRowsMsg()
        {
            if (RedisHelper.KeyExists("WxAppRowsMsg"))
            {
                //从redis获取消息
                var opObj = RedisHelper.ListLeftPopToObj<WxTongZhiInfo>("WxAppRowsMsg");
                if (opObj != null)
                {

                    WeiXinMsgUtility.SubmitAppRowsMsg(opObj);// 
                  
                }
            }
        }
    }
}
