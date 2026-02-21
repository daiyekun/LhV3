using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models
{
   public class jFsbDTO: INfEntityHandle
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Urgent { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int CompId { get; set; }
        public int HandlerId { get; set; }
        public DateTime HandDate { get; set; }
        public int ContId { get; set; }
        public int DisputeType { get; set; }
        public decimal Amount { get; set; }
        public string CompInfo { get; set; }
        public string Remark { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public int? ModifyUserId { get; set; }
        public DateTime? ModifyDateTime { get; set; }
        public int? IsDelete { get; set; }
        public int? Dstate { get; set; }
        /// <summary>
        /// 经办单位Name
        /// </summary>
        public string CompName { get; set; }
        /// <summary>
        /// 经办人Name
        /// </summary>
        public string HandlerIdName { get; set; }
        /// <summary>
        /// 合同Name
        /// </summary>
        public string ContIdName { get; set; }
        /// <summary>
        /// 纠纷类别Name
        /// </summary>

        public string DisputeName { get; set; }
        /// <summary>
        /// 紧急程度Name
        /// </summary>
        public string UrgentName { get; set; }
        /// <summary>
        /// 创建人Name
        /// </summary>
        public string CName { get; set; }
        /// <summary>
        /// 根据属性名称获取属性值
        /// </summary>
        /// <param name="propName">属性名称</param>
        /// <returns></returns>
        public FieldInfo GetPropValue(string propName)
        {
            var obj = this.GetType().GetProperty(propName);
            return new FieldInfo
            {
                FileType = obj.PropertyType,
                FileValue = obj.GetValue(this, null)
            };

        }




    }


}
