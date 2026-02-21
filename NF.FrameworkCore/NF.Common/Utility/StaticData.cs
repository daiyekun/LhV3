using System;
using System.Collections.Generic;
using System.Text;

namespace NF.Common.Utility
{
   public class StaticData
    {
        /// <summary>
        /// redis 前缀，所有redis必须在key前加入这个
        /// 需要修改两个地方。还有一个地方也需要同样修改
        /// </summary>
        public static readonly string redisBaseKey = "WOO_TS";
        /// <summary>
        /// 存储操作日志的RedisKey
        /// </summary>
        public static readonly string OptionLogRedisKey = $"{redisBaseKey}:OptionLogRedis";
        /// <summary>
        /// Session UserId 存储key
        /// </summary>
        public static readonly string NFUserId = $"NFUserId";
        /// <summary>
        /// Session User对象Key
        /// </summary>
        public static readonly string NFUser = $"{redisBaseKey}:NFUser";
        /// <summary>
        /// 验证码Session Key
        /// </summary>
        public static readonly string NFVerifyCode = $"{redisBaseKey}:NFVerifyCode";
        /// <summary>
        /// 所在部门
        /// </summary>
        public static readonly string NFUserDeptId = $"{redisBaseKey}:NFUserDeptId";
        /// <summary>
        /// 数据字典Hashkey
        /// </summary>
        public static readonly string RedisDataKey = $"{redisBaseKey}:data";
        /// <summary>
        /// 用户hashkey
        /// </summary>
        public static readonly string RedisUserKey = $"{redisBaseKey}:user";
        /// <summary>
        /// 删除数据字典队列key
        /// </summary>
        public static readonly string RedisDataDelKey = $"{redisBaseKey}:datadel";
        /// <summary>
        /// 删除用户队列Key
        /// </summary>
        public static readonly string RedisUserDelKey = $"{redisBaseKey}:userdel";
        /// <summary>
        /// 组织机构Hashkey
        /// </summary>
        public static readonly string RedisDeptKey = $"{redisBaseKey}:dept";
        /// <summary>
        /// 删除组织机构Hashkey
        /// </summary>
        public static readonly string RedisDelDeptKey = $"{redisBaseKey}:deptdel";
        /// <summary>
        /// 存储币种key
        /// </summary>
        public static readonly string RedisCurrencyKey = $"{redisBaseKey}:currency";
        /// <summary>
        /// 删除币种
        /// </summary>
        public static readonly string RedisDelCurrencyKey = $"{redisBaseKey}:currencydel";
        /// <summary>
        /// 市KEy
        /// </summary>
        public static readonly string RedisCityKey = $"{redisBaseKey}:city";
        /// <summary>
        /// 省
        /// </summary>
        public static readonly string RedisProvinceKey = $"{redisBaseKey}:province";
        /// <summary>
        /// 国家
        /// </summary>
        public static readonly string RedisCountryKey = $"{redisBaseKey}:country";
        /// <summary>
        /// 当前用户权限
        /// </summary>
        public static readonly string UserPermissions = $"{redisBaseKey}:userpiss";
        /// <summary>
        /// 当前用户角色权限
        /// </summary>
        public static readonly string UserRolePermissions = $"{redisBaseKey}:urolepiss";
        /// <summary>
        /// TreeSelect需要的部门数据
        /// </summary>
        public static readonly string RedisTreeSelDeptKey = $"{redisBaseKey}:TreeSelDeptKey";
        /// <summary>
        /// 创建合同历史信息（主要是标签信息）
        /// </summary>
        public static readonly string AddContHistory = $"{redisBaseKey}:AddContHistory";
        /// <summary>
        /// 合同统计
        /// </summary>
        public static readonly string ContStat = $"{redisBaseKey}:ContStat";
        /// <summary>
        /// 添加APP提醒
        /// </summary>
        public static readonly string InsertTExing = $"{redisBaseKey}:InsertTExing";



    }
}
