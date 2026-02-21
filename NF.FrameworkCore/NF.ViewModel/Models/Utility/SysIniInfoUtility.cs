using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NF.ViewModel.Models.Utility
{
   public class SysIniInfoUtility
    {
        /// <summary>
        /// 初始化数据字典
        /// </summary>
        /// <param name="dictionaryDTO"></param>
        /// <param name="hashkey"></param>
        /// <param name="func"></param>
        public static void SetDataDic(DataDictionaryDTO dictionaryDTO, string hashkey, Func<string, int,int?, string> func)
        {
            Type t = dictionaryDTO.GetType();
            PropertyInfo[] properties = t.GetProperties();
            foreach (var p in properties)
            {
                var v = PropertyUtility.GetObjectPropertyValue(dictionaryDTO, p.Name);
                var key = func.Invoke(hashkey, dictionaryDTO.Id, dictionaryDTO.DtypeNumber); //$"{StaticData.RedisDataKey}:{dictionaryDTO.Id}:{dictionaryDTO.DtypeNumber}";
               
                RedisHelper.HashUpdate(key, p.Name, v);
            }
        }
        /// <summary>
        /// 删除Hash表指定缓存数据字典
        /// </summary>
        /// <param name="dictionaryDTO"></param>
        /// <param name="hashkey"></param>
        /// <param name="func"></param>
        public static void DelDataDic(DataDictionaryDTO dictionaryDTO, string hashkey, Func<string, int, int?, string> func)
        {
            Type t = dictionaryDTO.GetType();
            PropertyInfo[] properties = t.GetProperties();
            foreach (var p in properties)
            {
                var key = func.Invoke(hashkey, dictionaryDTO.Id, dictionaryDTO.DtypeNumber); 
                RedisHelper.HashDelete(key, p.Name);
            }
        }

        /// <summary>
        /// 初始化一些Hash
        /// </summary>
        /// <typeparam name="T">当前实体类型</typeparam>
        /// <param name="t1">实体对象</param>
        /// <param name="hashkey">hashKey</param>
        /// <param name="func"></param>
        public static void SetRedisHash<T>(T t1,string hashkey, Func<string, int, string> func)
            where T: IEntityDTO
        {
            Type t = t1.GetType();
            PropertyInfo[] properties = t.GetProperties();
            foreach (var p in properties)
            {
                var v = PropertyUtility.GetObjectPropertyValue(t1, p.Name);
                var key = func.Invoke(hashkey, t1.Id);
                //if (RedisHelper.HashHasKey(key, p.Name))
                //{
                //    RedisHelper.HashDelete(key, p.Name);
                //}
                //RedisHelper.HashSet(key, p.Name, v);
                RedisHelper.HashUpdate(key, p.Name, v);
            }
        }
        /// <summary>
        /// 删除Hash
        /// </summary>
        /// <typeparam name="T">当前实体类型</typeparam>
        /// <param name="t1">实体对象</param>
        /// <param name="hashkey">hashKey</param>
        /// <param name="func"></param>
        public static void DelRedisHash<T>(T t1, string hashkey, Func<string, int, string> func)
            where T : IEntityDTO
        {
            Type t = t1.GetType();
            PropertyInfo[] properties = t.GetProperties();
            foreach (var p in properties)
            {
                var key = func.Invoke(hashkey, t1.Id);
               
                RedisHelper.HashDelete(key, p.Name);
            }
        }
    }
}
