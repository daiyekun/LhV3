using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models
{
   public class AjjfDTO: INfEntityHandle
    //INfEntityHandle
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Urgent { get; set; }
        public string DisName { get; set; }
        public string DisCode { get; set; }
        public int DisId { get; set; }
        public string Name { get; set; }
        public string  Code { get; set; }
        public DateTime HandDate { get; set; }
        public int ContId { get; set; }
        public int? HandlerId { get; set; }
        public string WoDw { get; set; }
        public string Remark { get; set; }
        public int? DisputeType { get; set; }
        public string Pawn { get; set; }
        public decimal? Amount { get; set; }
        public string AmoutDisc { get; set; }
        public string DisDic { get; set; }
        public string CaseRemark { get; set; }
        public string CaseBrief { get; set; }
        public int? CaseId { get; set; }
        public string CaseFileName { get; set; }
        public string CaseFilePath { get; set; }
        public string CourtName { get; set; }
        public DateTime? BeginDate { get; set; }
        public string SpResult { get; set; }
        public string SpFileName { get; set; }
        public string SpFilePath { get; set; }
        public string LawyerName { get; set; }
        public string HandRemark { get; set; }
        public int? PreType { get; set; }
        public string PreFileName { get; set; }
        public string PreFilePath { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public int? ModifyUserId { get; set; }
        public DateTime? ModifyDateTime { get; set; }
        public int? IsDelete { get; set; }
        public int? Dstate { get; set; }
        public string ZtName { get; set; }
        /// <summary>
        /// 负责人Name
        /// </summary>
        public string ContIdName { get; set; }
        /// <summary>
        /// 记录人Name
        /// </summary>
        public string HandlerIdName { get; set; }
        /// <summary>
        /// 纠纷类别Name
        /// </summary>
        public string DisputeName { get; set; }
        /// <summary>
        /// 紧急程度Name
        /// </summary>
        public string UrgentName { get; set; }
        /// <summary>
        /// 保全类型Name
        /// </summary>
        public string PreTypeName { get; set; }

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
