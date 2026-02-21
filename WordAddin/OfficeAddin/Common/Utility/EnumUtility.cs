using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Common.Utility
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public class EnumUtility
    {

        /// <summary>
        /// 得到所有的值
        /// </summary>
        /// <param name="EnumType"></param>
        /// <returns></returns>
        public static IList<int> GetValues(Type EnumType)
        {
            Attribute[] atts = Attribute.GetCustomAttributes(EnumType, typeof(EnumClassAttribute));

            if (atts.Length == 0)
                throw new Exception("没有找到EnumClassAttribute");

            EnumClassAttribute classAtt = (EnumClassAttribute)atts[0];

            IList<int> lst = new List<int>();
            foreach (int intValue in Enum.GetValues(EnumType))
            {
                if (intValue >= classAtt.Min && intValue <= classAtt.Max)
                    lst.Add(intValue);
            }

            return lst;
        }

        /// <summary>
        /// 得到自定义类型
        /// </summary>
        /// <param name="EnumType"></param>
        /// <returns></returns>
        public static IList<EnumItemAttribute> GetAttr(Type EnumType)
        {
            IList<EnumItemAttribute> lst = new List<EnumItemAttribute>();

            EnumClassAttribute classAtt = GetClassAttribute(EnumType);

            MemberInfo[] members = EnumType.GetMembers();
            foreach (MemberInfo member in members)
            {
                EnumItemAttribute itemAtt = (EnumItemAttribute)Attribute.GetCustomAttribute(member, typeof(EnumItemAttribute));
                if (itemAtt == null)
                    continue;

                lst.Add(itemAtt);
            }
            return lst;
        }

        /// <summary>
        /// 得到类属性
        /// </summary>
        /// <param name="EnumType"></param>
        /// <returns></returns>
        public static EnumClassAttribute GetClassAttribute(Type EnumType)
        {
            EnumClassAttribute classAtt = (EnumClassAttribute)Attribute.GetCustomAttribute(EnumType, typeof(EnumClassAttribute));

            if (classAtt == null)
                throw new Exception("没有找到EnumClassAttribute");
            return classAtt;
        }

        /// <summary>
        /// 得到描述
        /// </summary>
        /// <param name="EnumType"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string GetDesc(Type EnumType, int Value)
        {
            var clsAttr = GetClassAttribute(EnumType);
            MemberInfo[] members = EnumType.GetMembers();
            foreach (MemberInfo member in members)
            {
                EnumItemAttribute itemAtt = (EnumItemAttribute)Attribute.GetCustomAttribute(member, typeof(EnumItemAttribute));
                if (itemAtt == null)
                    continue;
                if (itemAtt.Value == Value)
                {
                    if (clsAttr.IsLocal)
                    {
                        return "是";
                    }
                    return itemAtt.Desc;
                }
            }
            return "";
        }

        /// <summary>
        /// 得到请求根路径
        /// </summary>
        /// <param name="EnumType"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string GetRequestType(Type EnumType, int Value)
        {
            var clsAttr = GetClassAttribute(EnumType);
            MemberInfo[] members = EnumType.GetMembers();
            foreach (MemberInfo member in members)
            {
                EnumContTextOptionAttribute itemAtt = (EnumContTextOptionAttribute)Attribute.GetCustomAttribute(member, typeof(EnumContTextOptionAttribute));
                if (itemAtt == null)
                    continue;
                if (itemAtt.Value == Value)
                {
                    if (clsAttr.IsLocal)
                    {
                        return "是";
                    }
                    return itemAtt.RequestType;
                }
            }
            return "";
        }
        /// <summary>
        /// 得到默认描述
        /// </summary>
        /// <param name="EnumType"></param>
        /// <returns></returns>
        public static string GetDefaultDesc(Type EnumType)
        {
            return GetDesc(EnumType, GetDefaultValue(EnumType));
        }
        /// <summary>
        /// 得到默认值
        /// </summary>
        /// <param name="EnumType"></param>
        /// <returns></returns>
        public static int GetDefaultValue(Type EnumType)
        {
            EnumClassAttribute classAtt = GetClassAttribute(EnumType);
            if (classAtt.HasDefault)
            {
                return classAtt.Default;
            }
            return classAtt.Min;
        }
        /// <summary>
        /// 位或字段Get
        /// </summary>
        /// <param name="State">数据库状态值</param>
        /// <param name="Type">位或掩码</param>
        /// <returns></returns>
        public static bool StateGet(int State, int Type)
        {
            if (State < 0)
                return false;
            return (State & Type) == Type;
        }


        /// <summary>
        /// 返回迭代器
        /// </summary>
        /// <param name="EnumType"></param>
        /// <returns></returns>
        public static IEnumerable<EnumItemAttribute> GetEnumerator(Type EnumType)
        {
            EnumClassAttribute classAtt = GetClassAttribute(EnumType);

            MemberInfo[] members = EnumType.GetMembers();
            foreach (MemberInfo member in members)
            {
                EnumItemAttribute itemAtt = (EnumItemAttribute)Attribute.GetCustomAttribute(member, typeof(EnumItemAttribute));
                if (itemAtt == null)
                    continue;

                yield return itemAtt;
            }
        }

          /// <summary>
        /// 返回迭代器
        /// </summary>
        /// <param name="EnumType"></param>
        /// <returns></returns>
        public static IEnumerable<EnumContTextOptionAttribute> GetOptionEnumerator(Type EnumType)
        {
            EnumClassAttribute classAtt = GetClassAttribute(EnumType);

            MemberInfo[] members = EnumType.GetMembers();
            foreach (MemberInfo member in members)
            {
                EnumContTextOptionAttribute itemAtt = (EnumContTextOptionAttribute)Attribute.GetCustomAttribute(member, typeof(EnumContTextOptionAttribute));
                if (itemAtt == null)
                    continue;

                yield return itemAtt;
            }
        }

        /// <summary>
        /// 根据请求类型得到值
        /// </summary>
        /// <param name="EnumType">Enum类型</param>
        /// <param name="requestType">请求类型</param>
        /// <returns></returns>
        public static int GetValueByRequestType(Type EnumType, string requestType)
        {
           // var clsAttr = GetClassAttribute(EnumType);

            foreach (var item in GetOptionEnumerator(EnumType))
            {

                if (item.RequestType.Trim() == requestType.Trim())
                    return item.Value;
                
            }
            return GetDefaultValue(EnumType);
        }

        /// <summary>
        /// 根据描述得到值
        /// </summary>
        /// <param name="EnumType"></param>
        /// <param name="Desc"></param>
        /// <returns></returns>
        public static int GetValueByDesc(Type EnumType, string Desc)
        {
            var clsAttr = GetClassAttribute(EnumType);

            foreach (var item in GetEnumerator(EnumType))
            {

                if (item.Desc.Trim() == Desc.Trim())
                    return item.Value;
               
            }
            return GetDefaultValue(EnumType);
        }
        /// <summary>
        /// 根据描述得到值
        /// </summary>
        /// <param name="EnumType"></param>
        /// <param name="Desc"></param>
        /// <returns></returns>
        public static int? GetValueByDescNull(Type EnumType, string Desc)
        {
            var clsAttr = GetClassAttribute(EnumType);

            foreach (var item in GetEnumerator(EnumType))
            {

                if (item.Desc.Trim() == Desc.Trim())
                    return item.Value;
                else
                    return -1;
            }
            return null;
        }

    }
}
