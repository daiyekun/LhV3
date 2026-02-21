using System;
using System.Collections.Generic;
using System.Text;

namespace NF.WeiXin.Lib.Module
{
    /// <summary>
    /// Reis缓存使用实体-实体名称必须和配置文件名称一致
    /// </summary>
    public class RedisInfo
    {
        /// <summary>
        /// Reis连接
        /// </summary>
        public string RedisDefaultConnection { get; set; }
        /// <summary>
        /// 实例名称
        /// </summary>
        public string RedisDefaultInstanceName { get; set; }
        /// <summary>
        /// Session的Redis连接
        /// </summary>
        public string RedisSessionConnection { get; set; }
        /// <summary>
        /// Session的Redis实例名称
        /// </summary>
        public string RedisSessionInstanceName { get; set; }
        /// <summary>
        /// Session过期时间30分钟
        /// </summary>
        public int SessionTimeOut { get; set; }
    }
   

    /// <summary>
    /// ConnectionStrings配置文件实体，此类属性一定需要和配置文件名称一致
    /// </summary>
    public class ConnectionStrings
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        public string SqlConnection { set; get; }
        /// <summary>
        /// Redis配置
        /// </summary>
        public RedisInfo Redis { get; set; }
        


    }
}
