using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace SignalR.Client
{
    public class ClientManage
    {
        public void ClientSend(int id)
        {
            HubConnection connection = new HubConnectionBuilder()
                            .WithUrl("http://localhost:5000/chatHub")
                            .Build();
            //连接hub
            connection.StartAsync();
            Console.WriteLine("已连接");

            
           
            //发送消息
            connection.InvokeAsync("SendMessage", "", "ok");
        }
    }
}
