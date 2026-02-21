using Microsoft.AspNetCore.SignalR;
using NF.Common.Utility;
using NF.QuartzNet;
using NF.Web.Models.ContText;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.Web.Models
{
    public class LhHub: Hub
    {
        public async Task SendMessage(DownloadInfo datainfo)
        {
            if (RedisHelper.KeyExists($"pdf:{datainfo.Id}:{datainfo.UserId}"))
            {
                datainfo.IsYes = true;
            }
             await Clients.All.SendAsync("ReceiveMessage", datainfo);
        }
    }
}
