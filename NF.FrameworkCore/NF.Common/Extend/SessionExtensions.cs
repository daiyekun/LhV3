using Microsoft.AspNetCore.Http;
using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.Common.SessionExtend
{
    public static class SessionExtensions
    {
        /// <summary>
        /// 设置Session
        /// 存储实体对象信息
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key">Session Key</param>
        /// <param name="value">Session Value</param>
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonUtility.SerializeObject(value));
        }
        /// <summary>
        /// 获取Session
        /// 主要用于存储是实体对象的信息
        /// </summary>
        /// <typeparam name="T">Session实体对象</typeparam>
        /// <param name="session"></param>
        /// <param name="key">Session Key</param>
        /// <returns>返回实体</returns>
        public static T GetObjectFromJson<T>(this ISession session, string key) where T : class
        {
            var value = session.GetString(key);

            return value == null ? default(T) : JsonUtility.DeserializeJsonToObject<T>(value);
        }

        /// <summary>
        /// 设置Session字符串
        /// </summary>
        /// <param name="session">Session</param>
        /// <param name="key">Session Key</param>
        /// <param name="value">Session Value</param>
        public static void SetSessionString(this ISession session, string key, string value)
        {
            session.SetString(key, value);
        }

        /// <summary>
        /// 获取Session 字符串
        /// 主要用于单词字符串存储
        /// </summary>
        /// <param name="session">Session对象</param>
        /// <param name="key">Session Key</param>
        /// <returns>返回字符串</returns>
        public static string GetSessionString(this ISession session, string key)
        {
            return session.GetString(key);


        }
        /// <summary>
        /// 设置一个对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="session">Session 对象</param>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonUtility.SerializeObject(value));
        }
        /// <summary>
        /// Session 获取一个对象
        /// </summary>
        /// <typeparam name="T">当前Session对象类型</typeparam>
        /// <param name="session">Session对象</param>
        /// <param name="key">Session Key</param>
        /// <returns></returns>
        public static T Get<T>(this ISession session, string key)
            where T:class
        {
            var value = session.GetString(key);

            return value == null ? default(T): JsonUtility.DeserializeJsonToObject<T>(value);

        }

    }
}
