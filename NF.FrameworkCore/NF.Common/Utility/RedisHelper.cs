using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NF.Common.Extend;
using StackExchange.Redis;

namespace NF.Common.Utility
{
    /// <summary>
    /// 使用到帮助类
    /// </summary>
    public class RedisHelper
    {
        /// <summary>
        /// 异步设置String值
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="Value">Value</param>
        /// <returns>bool True/False</returns>
        public static Task<bool> StringSetAsync(string key, string Value)
        {
            return RedisUtility._redisData.StringSetAsync(key, Value);
        }
        /// <summary>
        /// 设置String值
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="Value">值</param>
        /// <returns>bool True/False</returns>
        public static bool StringSet(string key, string Value)
        {
            return RedisUtility._redisData.StringSet(key, Value);
        }
        /// <summary>
        /// 设置string 过期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="Value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static bool StringSet(string key, string Value, TimeSpan expiry)
        {
            return RedisUtility._redisData.StringSet(key, Value, expiry);
        }
        /// <summary>
        /// 将list转化成Json保存到数据库
        /// </summary>
        /// <typeparam name="T">当前对象类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="listT">List集合</param>
        /// <returns>True/False</returns>
        public static Task<bool> ListObjToJsonStringSetAsync<T>(string key, IList<T> listt)
            where T : class
        {
            return RedisUtility._redisData.StringSetAsync(key, listt.ToJson());

        }
        /// <summary>
        /// 将list转化成Json保存到数据库
        /// </summary>
        /// <typeparam name="T">当前对象类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="listT">List集合</param>
        /// <returns>True/False</returns>
        public static Task<bool> ListObjToJsonStringSetAsync<T>(string key, T t)
            where T : class
        {
            return RedisUtility._redisData.StringSetAsync(key, t.ToJson());

        }
        /// <summary>
        /// 判断Key是否存在
        /// </summary>
        /// <param name="key">当前key</param>
        /// <returns>bool True:存在，False：不存在</returns>
        public static bool KeyExists(string key)
        {
            return RedisUtility._redisData.KeyExists(key);
        }
        /// <summary>
        /// 根据字符串获取Key
        /// </summary>
        /// <param name="key">当前Key</param>
        /// <returns>返回String字符串值</returns>
        public static string StringGet(string key)
        {
            return RedisUtility._redisData.StringGet(key);
        }
        /// <summary>
        /// 直接获取字符串凡系列化成对象集合
        /// </summary>
        /// <typeparam name="T">当前对象类型</typeparam>
        /// <param name="key">key</param>
        /// <returns>返回对象集合</returns>
        public static IList<T> StringGetToList<T>(string key)
            where T : class

        {

            if (RedisUtility._redisData.KeyExists(key))
            {
                var strJson = RedisUtility._redisData.StringGet(key);
                return strJson.ToString().JsonToList<T>();
            }
            return null;

        }

        /// <summary>
        /// 直接获取字符串凡系列化成对象
        /// </summary>
        /// <typeparam name="T">当前对象类型</typeparam>
        /// <param name="key">key</param>
        /// <returns>返回对象</returns>
        public static T StringGetToObject<T>(string key)
            where T : class

        {

            if (RedisUtility._redisData.KeyExists(key))
            {
                var strJson = RedisUtility._redisData.StringGet(key);
                return strJson.ToString().JsonToObje<T>();
            }
            return null;

        }
        /// <summary>
        /// 异步删除Key
        /// </summary>
        /// <param name="key">当前key</param>
        /// <returns>返回True/False</returns>
        public static Task<bool> KeyDeleteAsync(string key)
        {
            return RedisUtility._redisData.KeyDeleteAsync(key);
        }
        /// <summary>
        /// 删除Key
        /// </summary>
        /// <param name="key">当前key</param>
        /// <returns>返回True/False</returns>
        public static bool KeyDelete(string key)
        {
            return RedisUtility._redisData.KeyDelete(key);
        }

        #region  队列List
        /// <summary>
        /// 从右边开始加队列
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">Value</param>
        /// <returns></returns>
        public static long ListRightPush(string key, string value)
        {
            return RedisUtility._redisData.ListRightPush(key, value);
        }

        /// <summary>
        /// 从右边开始加队列
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="t">当前对象</param>
        /// <returns></returns>
        public static long ListRightPush<T>(string key, T t)
            where T : class
        {
            return RedisUtility._redisData.ListRightPush(key, JsonUtility.SerializeObject(t, true));
        }
        /// <summary>
        /// 从左边开始出队
        /// </summary>
        /// <returns></returns>
        public static string ListLeftPop(string key)
        {
            return RedisUtility._redisData.ListLeftPop(key);
        }
        /// <summary>
        /// 获取队列然后反系列化对象
        /// </summary>
        /// <typeparam name="T">当前对象类型</typeparam>
        /// <param name="key">key</param>
        /// <returns>返回对象</returns>
        public static T ListLeftPopToObj<T>(string key)
            where T : class
        {
            var strList = RedisUtility._redisData.ListLeftPop(key);
            if (string.IsNullOrEmpty(strList))
            {
                return default(T);
            }
            else
            {
                return strList.ToString().JsonToObje<T>();
            }

        }
        /// <summary>
        /// 存储List到队列
        /// </summary>
        /// <typeparam name="T">当前对象</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">对象集合值</param>
        public void ListSet<T>(string key, List<T> value)
        {

            //下面的database 是redis的数据库对象.
            foreach (var single in value)
            {
                var s = single.ToJson(); //序列化
                RedisUtility._redisData.ListRightPush(key, s); //要一个个的插入
            }
        }
        /// <summary>
        /// 取出队列值直接为List
        /// </summary>
        /// <typeparam name="T">当前对象类型</typeparam>
        /// <param name="key">当前Key</param>
        /// <returns>对象集合</returns>
        //封装的ListGet
        public List<T> ListGet<T>(string key)
            where T : class
        {

            //ListRange返回的是一组字符串对象
            //需要逐个反序列化成实体
            var vList = RedisUtility._redisData.ListRange(key);
            List<T> result = new List<T>();
            foreach (var item in vList)
            {
                T model = item.ToString().JsonToObje<T>(); //反序列化
                result.Add(model);
            }
            return result;
        }
        #endregion

        #region Hash操作
        /// <summary>
        /// 删除Hash
        /// </summary>
        /// <param name="key">Hash</param>
        /// <param name="hashField">Hash 字段</param>
        /// <returns>true/false</returns>
        public static bool HashDelete(RedisKey key, RedisValue hashField)
        {
            return RedisUtility._redisData.HashDelete(key, hashField);

        }
        /// <summary>
        /// 删除hash异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">字段</param>
        /// <returns>true/false</returns>
        public static Task<bool> HashDeleteAsync(RedisKey key, RedisValue hashField)
        {
            return RedisUtility._redisData.HashDeleteAsync(key, hashField);

        }
        /// <summary>
        /// 根据hash key和批量字段删除批量类型
        /// </summary>
        /// <param name="key">Hash</param>
        /// <param name="hashField">Hash 字段</param>
        /// <returns>true/false</returns>
        public static long HashDelete(RedisKey key, RedisValue[] hashField)
        {
            return RedisUtility._redisData.HashDelete(key, hashField);

        }
        /// <summary>
        /// 根据hash key和批量字段删除批量类型(Async)
        /// </summary>
        /// <param name="key">Hash</param>
        /// <param name="hashField">Hash 字段</param>
        /// <returns>true/false</returns>
        public static Task<long> HashDeleteAsync(RedisKey key, RedisValue[] hashField)
        {
            return RedisUtility._redisData.HashDeleteAsync(key, hashField);

        }
        /// <summary>
        /// 判断Hash下是否存在某一字段
        /// </summary>
        /// <param name="key">Hash </param>
        /// <param name="hashField">字段</param>
        /// <returns></returns>
        public static bool HashHasKey(RedisKey key, RedisValue hashField)
        {
            return RedisUtility._redisData.HashExists(key, hashField);
        }
        /// 判断Hash下是否存在某一字段(Asnyc)
        /// </summary>
        /// <param name="key">Hash </param>
        /// <param name="hashField">字段</param>
        /// <returns></returns>
        public static Task<bool> HashHasKeyAsync(RedisKey key, RedisValue hashField)
        {
            return RedisUtility._redisData.HashExistsAsync(key, hashField);
        }
        /// <summary>
        /// 根据Hash key 和Field获取字段值
        /// </summary>
        /// <param name="key">Hash Key</param>
        /// <param name="hashField">字段值</param>
        /// <returns>hash值</returns>
        public static RedisValue HashGet(RedisKey key, RedisValue hashField)
        {
            return RedisUtility._redisData.HashGet(key, hashField);
        }
        /// <summary>
        /// 根据Hash key 和Field获取字段值
        /// </summary>
        /// <param name="key">Hash Key</param>
        /// <param name="hashField">字段值</param>
        /// <returns>hash值</returns>
        public static Task<RedisValue> HashGetAsync(RedisKey key, RedisValue hashField)
        {
            return RedisUtility._redisData.HashGetAsync(key, hashField);
        }
        /// <summary>
        /// 获取存储在指定键的哈希中的所有字段和值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Dictionary<string, object> HashGetAll(string key)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            var collection = RedisUtility._redisData.HashGetAll(key);
            foreach (var item in collection)
            {
                dic.Add(item.Name, item.Value);
            }
            return dic;
        }
        /// <summary>
        /// 获取哈希中的所有字段
        /// </summary>
        /// <param name="key">Hash Key</param>
        /// <returns></returns>
        public static string[] HashKeys(string key)
        {
            return RedisUtility._redisData.HashKeys(key).ToStringArray();
        }
        /// <summary>
        /// 获取散列中的字段数量
        /// </summary>
        /// <param name="key">Hash key</param>
        /// <returns>long</returns>
        public static long HashSize(string key)
        {
            return RedisUtility._redisData.HashLength(key);
        }
        /// <summary>
        /// 存储Hash数据
        /// </summary>
        /// <param name="key">hash key</param>
        /// <param name="hashField">Hash 字段</param>
        /// <param name="value">字段值</param>
        public static void HashSet(RedisKey key, RedisValue hashField, RedisValue value)
        {
            RedisUtility._redisData.HashSet(key, hashField, value);
        }
        /// <summary>
        /// 更新Hash
        /// </summary>
        /// <param name="key">hash key</param>
        /// <param name="hashField">字段名称</param>
        /// <param name="value">值</param>
        public static void HashUpdate(RedisKey key, RedisValue hashField, RedisValue value)
        {
            if(HashHasKey(key, hashField))
            {
                HashDelete(key, hashField);
            }
            HashSet(key,hashField,value);
        }
       
        /// <summary>
        /// 为多个哈希字段分别设置它们的值
        /// </summary>
        /// <param name="key">Hash key </param>
        /// <param name="dic">字段：值 dic</param>
        /// <returns></returns>
        public static void HashPutAll(string key, Dictionary<string, string> dic)
        {
            List<HashEntry> list = new List<HashEntry>();
            for (int i = 0; i < dic.Count; i++)
            {
                KeyValuePair<string, string> param = dic.ElementAt(i);
                list.Add(new HashEntry(param.Key, param.Value));
            }
            RedisUtility._redisData.HashSet(key, list.ToArray());
        }
        /// <summary>
        /// redis中获取指定键的值并返回对象，
        /// *尽量避免使用，因为其中会涉及系列化和反系列化*
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetHashValue<T>(string key)
           where T : class
        {
            HashEntry[] array = RedisUtility._redisData.HashGetAll(key);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            for (int i = 0; i < array.Length; i++)
            {
                dic.Add(array[i].Name, array[i].Value);
            }
            string strJson = JsonUtility.SerializeObject(dic);
            return JsonUtility.DeserializeJsonToObject<T>(strJson);
        }

        /// <summary>
        /// 把指定对象存储在键值为key的redis中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="key"></param>
        public static void SetHashValue<T>(T t, string key)
        {
            string strJson = JsonUtility.SerializeObject(t);
            Dictionary<string, string> param = JsonUtility.DeserializeObject<Dictionary<string, string>>(strJson);
            HashPutAll(key, param);
        }

        #endregion Hash

        #region redis 集合（Set）操作
         /// <summary>
        /// 集合添加元素
        /// </summary>
        /// <param name="key">key</param>
         /// <param name="value">Value</param>
         public static void SetAdd(RedisKey key, RedisValue value)
         {
            RedisUtility._redisData.SetAdd(key, value);
        }
        /// <summary>
         /// 集合组合操作
         /// </summary>
         /// <param name="point">操作标示：0--并集；1--交集；2--差集</param>
         /// <param name="firstKey">第一个集合的键值</param>
         /// <param name="secondKey">第二个集合的键值</param>
         public static string[] SetCombine(int point, RedisKey firstKey, RedisKey secondKey)
         {
             RedisValue[] array;
             switch (point)
             {
                 case 0:
                     array = RedisUtility._redisData.SetCombine(SetOperation.Union, firstKey, secondKey);
                     break;
                 case 1:
                    array = RedisUtility._redisData.SetCombine(SetOperation.Intersect, firstKey, secondKey);
                     break;
                 case 2:
                     array = RedisUtility._redisData.SetCombine(SetOperation.Difference, firstKey, secondKey);
                     break;
                 default:
                     array = new RedisValue[0];
                     break;
             }
             return array.ToStringArray();
        }

         /// <summary>
         /// 
         /// </summary>
         /// <param name="key"></param>
         /// <param name="value"></param>
         /// <returns></returns>
         public static bool SetContains(string key, string value)
         {
             return RedisUtility._redisData.SetContains(key, value);
        }
         /// <summary>
         /// 返回对应键值集合的长度
         /// </summary>
         /// <param name="key"></param>
         /// <returns></returns>
         public static long SetLength(string key)
         {
            return RedisUtility._redisData.SetLength(key);
        }
         /// <summary>
         /// 根据键值返回集合中所有的value
         /// </summary>
         /// <param name="key"></param>
         /// <returns></returns>
         public static string[] SetMembers(string key)
         {
             return RedisUtility._redisData.SetMembers(key).ToStringArray();
        }
        /// <summary>
         /// 将成员从源集移动到目标集
         /// </summary>
         /// <param name="sourceKey">源集key</param>
         /// <param name="destinationKey">目标集key</param>
         /// <param name="value"></param>
         public static bool SetMove(string sourceKey, string destinationKey, string value)
         {
            return RedisUtility._redisData.SetMove(sourceKey, destinationKey, value);
         }
 
         /// <summary>
        /// 移除集合中指定键值随机元素
         /// </summary>
         /// <param name="key"></param>
         public static string SetPop(string key)
         {
            return RedisUtility._redisData.SetPop(key);
         }
 
         /// <summary>
         /// 返回集合中指定键值随机元素
         /// </summary>
         /// <param name="key"></param>
         /// <returns></returns>
         public static string SetRandomMember(string key)
         {
            return RedisUtility._redisData.SetRandomMember(key);
         }

         /// <summary>
         /// 
         /// </summary>
         /// <param name="key"></param>
         /// <param name="count"></param>
         public static string[] SetRandomMembers(string key, long count)
         {
            return RedisUtility._redisData.SetRandomMembers(key, count).ToStringArray();
        }
        

         /// <summary>
         /// 移除集合中指定key值和value
         /// </summary>
         /// <param name="key"></param>
         /// <param name="value"></param>
         public static void SetRemove(string key, string value)
        {
            RedisUtility._redisData.SetRemove(key, value);
         }
 
         /// <summary>
         /// 
         /// </summary>
         /// <param name="key"></param>
         public static void SetScan(string key)
         {
            RedisUtility._redisData.SetScan(key);
        }


       #endregion

         #region redis 有序集合（sorted set）操作
 
         public static void Method(string key, string value, double score)
         {
            RedisUtility._redisData.SortedSetAdd(key, new SortedSetEntry[] { new SortedSetEntry(value, score) });
         }
 
         #endregion








}

}