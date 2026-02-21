using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Server
{
    /// <summary>
    /// 客户端调用方法
    /// </summary>
    public class ChatHub: Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage",message);
        }
        public Task SendMessageToCaller(string message)
        {
            return Clients.Caller.SendAsync("SendMessageToCaller", message);
        }

    }
}
