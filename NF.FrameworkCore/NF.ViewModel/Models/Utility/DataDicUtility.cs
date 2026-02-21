using NF.Common.Utility;
using NF.ViewModel.Extend.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models.Utility
{
   public class DataDicUtility
    {
        /// <summary>
        /// 从Redis获取数据字典
        /// </summary>
        /// <param name="dicId">字典ID</param>
        /// <param name="dictionaryEnum">字典类型枚举</param>
        /// <param name="FieldName">字典字段名称</param>
        /// <returns></returns>
        public static string GetDicValueToRedis(int? dicId, DataDictionaryEnum dictionaryEnum,string FieldName="Name")
        {
            
          return dicId==null?"": RedisHelper.HashGet($"{StaticData.RedisDataKey}:{dicId}:{(int)dictionaryEnum}", FieldName).ToString();
           

        }
        

    }
}
