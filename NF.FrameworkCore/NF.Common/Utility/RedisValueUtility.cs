using System;
using System.Collections.Generic;
using System.Text;

namespace NF.Common.Utility
{
    /// <summary>
    /// Redis取值--数据字典到：DataDicUtility
    /// </summary>
    public class RedisValueUtility
    {
        /// <summary>
        /// 获取用户显示名称
        /// </summary>
        /// <param name="UserId">用户ID</param>
        /// <returns>用户显示名称</returns>
        public static string GetUserShowName(int UserId,string fieldName= "DisplyName")
        {
            return UserId<=0?"":RedisHelper.HashGet($"{StaticData.RedisUserKey}:{UserId}", fieldName).ToString();
        }
        
        /// <summary>
        /// 国家名称
        /// </summary>
        /// <returns></returns>
        public static string GetCountryName(int? CountryId)
        {
            return (CountryId??-1)<0?"": RedisHelper.HashGet($"{StaticData.RedisCountryKey}:{CountryId}", "Name").ToString();
        }
        /// <summary>
        /// 省
        /// </summary>
        /// <returns></returns>
        public static string GetProvinceNameName(int? ProvinceId)
        {
            return (ProvinceId ?? -1) < 0 ? "" : RedisHelper.HashGet($"{StaticData.RedisProvinceKey}:{ProvinceId}", "Name").ToString();
        }
        /// <summary>
        /// 市
        /// </summary>
        /// <returns></returns>
        public static string GetCityName(int? CityId)
        {
            return (CityId ?? -1) < 0 ? "" : RedisHelper.HashGet($"{StaticData.RedisCityKey}:{CityId}", "Name").ToString();
        }
        /// <summary>
        /// 币种
        /// </summary>
        /// <param name="CurrencyId"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetCurrencyName(int? CurrencyId,string fileName= "ShortName")
        {
            return (CurrencyId??-1)<0?"": RedisHelper.HashGet($"{StaticData.RedisCurrencyKey}:{CurrencyId}", fileName).ToString();
        }
        /// <summary>
        /// 获取部门名称
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <returns>返回部门名称</returns>
        public static string GetDeptName(int deptId,string fieldName="Name")
        {
            return deptId <= 0 ? "" : RedisHelper.HashGet($"{StaticData.RedisDeptKey}:{deptId}", fieldName).ToString();
        }
        /// <summary>
        /// 获取创建人部门ID
        /// </summary>
        /// <param name="createUserId">创建人ID</param>
        /// <returns></returns>
        public static int GetRedisUserDeptId(int createUserId)
        {
            var deptId = RedisHelper.HashGet($"{StaticData.RedisUserKey}:{createUserId}", "DepartmentId");
            if (deptId.IsNullOrEmpty)
                return 0;
            return Convert.ToInt32(deptId);

        }
    }
}
