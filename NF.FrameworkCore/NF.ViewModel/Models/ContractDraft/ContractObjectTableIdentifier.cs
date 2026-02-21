using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 插件使用-合同标的表格的标识符
    /// </summary>
    public class ContractObjectTableIdentifier
    {
        public ContractObjectTableIdentifier()
        {
            this.CONFINE_NUM = -1;
        }
        /// <summary>
        /// 字段类型
        /// =WOO_TEMPLATE_AND_OBJECT_FIELD.FIELD_TYPE
        /// </summary>
        public Nullable<Int32> FIELD_TYPE { get; set; }
        /// <summary>
        /// 业务品类ID
        /// =WOO_TEMPLATE_AND_OBJECT_FIELD.BC_ID
        /// </summary>
        public Nullable<Int32> BC_ID { get; set; }
        /// <summary>
        /// 标的ID，仅Excel导入有效
        /// =WOO_CONT_SUBJECTMATTER.ID
        /// </summary>
        public Nullable<Int32> ExcelSubDocument { get; set; }
        /// <summary>
        /// 本类标的的描述
        /// =WOO_CONT_SUBJECTMATTER.BC_DESC
        /// </summary>
        public String BC_DESC { get; set; }
        /// <summary>
        /// Excel中Sheet名称
        /// =WOO_CONT_SUBJECTMATTER.EXCE_SHEET_NAME
        /// </summary>
        public String ExcelSubDocSheet { get; set; }
        /// <summary>
        /// 明细标题
        /// =WOO_CONT_TEXT_TEMPLATE.MING_XI_TITLE
        /// </summary>
        public string DETAIL_TITLE { get; set; }
        /// <summary>
        /// 限制行数，只有当显示方式选择(概要明细)时赋值，默认-1
        /// =WOO_CONT_TEXT_TEMPLATE.SHOW_TYPE_NUMBER
        /// </summary>
        public int CONFINE_NUM { get; set; }

        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="info">合同文本模板历史</param>
        public void SetValues(ContTxtTemplateHist info)
        {
            if (info == null)
            {
                this.DETAIL_TITLE = "";
                this.CONFINE_NUM = -1;
                return;
            }
            this.DETAIL_TITLE = info.MingXiTitle;
            if (info.ShowType == (int)ShowTypeEnum.MingXi)
            {
                this.CONFINE_NUM = -1;
            }
            if (info.ShowType == (int)ShowTypeEnum.GaiYao)
            {
                this.CONFINE_NUM = info.ShowTypeNumber ?? 0;
            }
        }
        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="info">合同模板标的字段</param>
        public void SetValues(ContTxtTempAndSubField info)
        {
            if (info == null)
            {
                this.FIELD_TYPE = null;
                this.BC_ID = null;

                return;
            }
            this.FIELD_TYPE = info.FieldType ?? 0;
            this.BC_ID = info.BcId ?? 0;
        }
        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="info">合同标的</param>
        public void SetValues(ContSubjectMatter info)
        {
            if (info == null)
            {
                this.ExcelSubDocument = null;
                this.BC_DESC = "";
                this.ExcelSubDocSheet = "";

                return;
            }
           
            this.BC_DESC = "";
            
        }
    }
}
