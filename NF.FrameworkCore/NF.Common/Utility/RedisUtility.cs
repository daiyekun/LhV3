using NF.Common.Extend;
using NF.Common.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.Common.Utility
{

    /// <summary>
    /// 直接访问Redis工具类-正常使用
    /// </summary>
    public class RedisUtility
    {
        public static IDatabase _redisData;
        public static IDatabase _redisSessionData;
        static object locobj = new object();
         static RedisUtility()
        {

            if (_redisSessionData == null|| _redisData == null)
            {
                lock (locobj)
                {
                    if (_redisSessionData == null || _redisData == null)
                   {
                        var connection = ConfigurationManager.GetAppSettings<ConnectionStrings>("ConnectionStrings");
                        ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(connection.Redis.RedisSessionConnection);
                        ConnectionMultiplexer connectionMultiplexerDefault = ConnectionMultiplexer.Connect(connection.Redis.RedisDefaultConnection);
                        _redisSessionData = connectionMultiplexer.GetDatabase();
                        _redisData = connectionMultiplexerDefault.GetDatabase();
                    }
                }
            }

        }
        


    }


}
