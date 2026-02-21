using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 合同标的交付明细列表
    /// </summary>
   public  class ContSubDesListDTO: INfEntityHandle
    {
        /// <summary>
        /// 交付描述ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 标的ID
        /// </summary>
        public int? SubId { get; set; }
        /// <summary>
        /// 合同ID
        /// </summary>
        public int ContId { get; set; }
        /// <summary>
        /// 合同对方ID
        /// </summary>
        public int CompId { get; set; }
        /// <summary>
        /// 标的名称
        /// </summary>
        public string SubName { get; set; }
        /// <summary>
        /// 合同名称
        /// </summary>
        public string ContName { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContCode { get; set; }
        /// <summary>
        /// 交付日期
        /// </summary>
        public DateTime? ActDate { get; set; }
        /// <summary>
        /// 交付数量
        /// </summary>
        public decimal? DevNumber { get; set; }
        /// <summary>
        /// 交付金额
        /// </summary>
        public string DevMoneyThod { get; set; }
        /// <summary>
        /// 交付地址
        /// </summary>
        public string DevDz { get; set; }
        /// <summary>
        /// 交付方式
        /// </summary>
        public string DevFs { get; set; }
        /// <summary>
        /// 交付人
        /// </summary>
        public string DevUname { get; set; }
       
        /// <summary>
        /// 合同对方
        /// </summary>
        public string CompName { get; set; }
        /// <summary>
        /// 合同状态
        /// </summary>
        public string ContStateDic { get; set; }
        /// <summary>
        /// 合同负责人
        /// </summary>
        public string HtFzr { get; set; }
        /// <summary>
        /// 经办机构
        /// </summary>
        public string JbJg { get; set; }
        /// <summary>
        /// 标的单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public string DjThod { get; set; }
        /// <summary>
        /// 备注1
        /// </summary>
        public string Bz1 { get; set; }
        /// <summary>
        /// 备注2
        /// </summary>
        public string Bz2 { get; set; }
        /// <summary>
        /// 计划交付日期
        /// </summary>
        public DateTime? PlanDate { get; set; }
        /// <summary>
        /// 合同状态
        /// </summary>
        public int? ContState { get; set; }
        /// <summary>
        /// 导出时使用
        /// </summary>
        /// <param name="propName"></param>
        /// <returns></returns>
        public FieldInfo GetPropValue(string propName)
        {
            var fieldinfo = new FieldInfo();
            var obj = this.GetType().GetProperty(propName);
            fieldinfo.FileType = obj.PropertyType;
            fieldinfo.FileValue = obj.GetValue(this, null);
            //if (propName == "Pro")
            //{
            //    try
            //    {
            //        fieldinfo.FileValue = EmunUtility.GetDesc(typeof(BcPropertyEnums), Convert.ToInt32(this.GetType().GetProperty(propName).GetValue(this, null)));
            //    }
            //    catch (Exception)
            //    {

            //        return fieldinfo;
            //    }

            //}
            //else
            //{
            //    var obj = this.GetType().GetProperty(propName);
            //    fieldinfo.FileType = obj.PropertyType;
            //    fieldinfo.FileValue = obj.GetValue(this, null);
            //}
            return fieldinfo;
        }
    }
}
