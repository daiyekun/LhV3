using NF.Common.Utility;
using NF.ViewModel.Extend.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 显示类-比如大列表-查看页面
    /// </summary>
    public class CompanyViewDTO : CompanyDTO, INfEntityHandle
    {
        /// <summary>
        /// 创建人显示名称
        /// </summary>
        public string CreateUserDisplayName { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public string CompTypeName { get; set; }
        /// <summary>
        /// 信用等级
        /// </summary>
        public string CareditName { get; set; }
        /// <summary>
        /// 单位级别
        /// </summary>
        public string LevelName { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public string CompanyTypeClass { get; set; }
        /// <summary>
        /// 赋值人显示名称
        /// </summary>
        public string PrincipalUserDisplayName { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string CountryName { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 合同对方状态
        /// </summary>
        public string CstateDic { get; set; }
        /// <summary>
        /// 建立人部门ID
        /// </summary>
        public int UserDeptId { get; set; }
        /// <summary>
        /// 流程状态
        /// </summary>
        public int? WfState{get;set;}
        /// <summary>
        /// 流程状态
        /// </summary>
        public string WfStateDic { get; set; }
        /// <summary>
        /// 当前节点
        /// </summary>
        public string WfCurrNodeName { get; set; }
        /// <summary>
        /// 审批事项
        /// </summary>
        public string WfItemDic { get; set; }

        /// <summary>
        /// 根据属性名称获取属性值
        /// </summary>
        /// <param name="propName">属性名称</param>
        /// <returns></returns>
        public FieldInfo GetPropValue(string propName)
        {
            var fieldinfo = new FieldInfo();
            if (propName == "Cstate")
            {
                try
                {
                    fieldinfo.FileValue= EmunUtility.GetDesc(typeof(SysDataSateEnum), Convert.ToInt32(this.GetType().GetProperty(propName).GetValue(this, null)));
                }
                catch (Exception)
                {

                    return fieldinfo;
                }

            }
            else { 
            var obj = this.GetType().GetProperty(propName);
                fieldinfo.FileType = obj.PropertyType;
                fieldinfo.FileValue = obj.GetValue(this, null);
            }
            return fieldinfo;


        }
    }
}
