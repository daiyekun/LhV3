using System;
using System.Collections.Generic;
using System.Text;

namespace LhCode
{
    /// <summary>
    /// 授权
    /// </summary>
    public static class LhLicense
    {
        /// <summary>
        /// 用户数
        /// </summary>
        public static readonly  int  UserNumber= 15;
        /// <summary>
        /// 是否存在流程
        /// </summary>
        public static readonly bool IsFlow = false;
        /// <summary>
        /// 是否存在模板
        /// </summary>
        public static readonly bool IsTxtTemp = false;
        /// <summary>
        /// 是否存在单品
        /// </summary>
        public static readonly bool IsBices = false;
        /// <summary>
        /// 移动用户数
        /// </summary>
        public static readonly int MobileUserNumber = 15;
        /// <summary>
        /// 是否是并发
        /// </summary>
        public static readonly bool IsBinFa = true;
        /// <summary>
        /// 是否自动生成编号 0关 1开
        /// </summary>
        public static readonly int NewCoreStyle = 0;
        /// <summary>
        /// 项目编号自动生成1：自动。0：不自动
        /// </summary>
        public static readonly int ProjCodeZd = 0;
        /// <summary>
        /// 供应商编号自动生成  0关 1开
        /// </summary>
        public static readonly int SupplierCoreStyle = 0;
        /// <summary>
        /// 客户编号自动生成  0关 1开
        /// </summary>
        public static readonly int CustomerCoreStyle = 0;
        /// <summary>
        /// 其他对方编号自动生成  0关 1开
        /// </summary>
        public static readonly int OtherCoreStyle = 0;
        /// <summary>
        /// 是否开启微信应用(0:关闭，1：开启)
        /// </summary>
        public static readonly int WxKaiQi = 1;
    }
}
