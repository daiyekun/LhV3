using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 单品管理
    /// </summary>
    public class BcInstanceViewDTO: INfEntityHandle
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 类别Id
        /// </summary>
        public int LbId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 所属类别路径
        /// </summary>
        public string CatePath { get; set; }
        /// <summary>
        /// 属性
        /// </summary>
        public string ProDic { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 报价千分位
        /// </summary>
        public string PriceThod { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }
        /// <summary>
        /// 建立人
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public string CateName { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal? Price { get; set; }
        /// <summary>
        /// 属性
        /// </summary>
        public int? Pro { get; set; }

        public FieldInfo GetPropValue(string propName)
        {
            var fieldinfo = new FieldInfo();
            if (propName == "Pro")
            {
                try
                {
                    fieldinfo.FileValue = EmunUtility.GetDesc(typeof(BcPropertyEnums), Convert.ToInt32(this.GetType().GetProperty(propName).GetValue(this, null)));
                }
                catch (Exception)
                {

                    return fieldinfo;
                }

            }
            else
            {
                var obj = this.GetType().GetProperty(propName);
                fieldinfo.FileType = obj.PropertyType;
                fieldinfo.FileValue = obj.GetValue(this, null);
            }
            return fieldinfo;
        }
    }
}
