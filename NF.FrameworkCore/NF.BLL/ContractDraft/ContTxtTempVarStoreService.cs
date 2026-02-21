using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NF.Common.Utility;
using Microsoft.EntityFrameworkCore;

namespace NF.BLL
{
  public partial  class ContTxtTempVarStoreService
    {
        /// <summary>
        /// 获取所有系统变量
        /// </summary>
        /// <param name="cttextid">文本ID</param>
        /// <param name="locale">语言版本</param>
        /// <returns></returns>
        public IList<ContractVariable> GetAllContractVariables(int cttextid, String locale)
        {
            var _contractTextObj = Db.Set<ContText>().FirstOrDefault(p => p.Id == cttextid);
            if (_contractTextObj == null || _contractTextObj.ContId == null)
                return null;

            ContractInfo contractObj = Db.Set<ContractInfo>().Include(a => a.Comp).Include(a => a.MainDept).AsNoTracking().SingleOrDefault(p => p.Id == _contractTextObj.ContId.Value);
            if (contractObj == null) { return null; }

            IList<ContTxtTempVarStore> variables =Db.Set<ContTxtTempVarStore>().OrderByDescending(ent => ent.Id)
                .Where(ent => ent.IsCustomer == 0).ToList();
            if (variables.Count <= 0)
                return null;
            //if ("en_us".Equals(locale))
            //    return variables.Select(@var => new ContractVariable
            //    {
            //        VarLabel = TemplateAndObjectField.GetFildNameByKey(@var.NAME, I18NTypeEnum.enUS),
            //        VarName = @var.ID.ToString(),
            //        VarValue = mapContractVariable(contractObj, @var.NAME)
            //    }).ToList();

            return variables.Select(@var => new ContractVariable
            {
                VarLabel = @var.Name,
                VarName = @var.Id.ToString(),
                VarValue = MapContractVariable(contractObj, @var.Name)
            }).ToList();
        }
        /// <summary>
        /// 获取合同文本变量
        /// </summary>
        /// <param name="cttextid">合同文本ID</param>
        /// <param name="locale">语言版本</param>
        /// <returns></returns>
        public IList<ContractVariable> GetContractVariables(Int32 cttextid, String locale)
        {
          
               var  _contractTextObj = Db.Set<ContText>().FirstOrDefault(p => p.Id == cttextid);
            if (_contractTextObj == null || _contractTextObj.ContId == null)
                return null;

            ContractInfo contractObj = Db.Set<ContractInfo>().Include(a => a.Comp).Include(a=>a.MainDept).AsNoTracking().SingleOrDefault(p => p.Id == _contractTextObj.ContId.Value);
            if (contractObj == null) { return null; }

            IQueryable<ContTxtTempVarStore> variablesQuery = Db.Set<ContTxtTempAndVarStoreRela>()
                 .Where(rel => rel.TempHistId == _contractTextObj.TemplateId)
                 .Join(Db.Set<ContTxtTempVarStore>(), varRel => varRel.VarId, varDef => varDef.Id, (rel, def) => def)
                 .Where(def => (def.IsCustomer == 0 && (def.Isdelete??0) == 0));

            IList<ContTxtTempVarStore> variables = variablesQuery.ToList();
            if (variables.Count <= 0)
                return null;

            //if ("en_us".Equals(locale))
            //    return variables.Select(@var => new ContractVariable
            //    {
            //        VarLabel = TemplateAndObjectField.GetFildNameByKey(@var.NAME, I18NTypeEnum.enUS),
            //        VarName = @var.ID.ToString(),
            //        VarValue = mapContractVariable(contractObj, @var.NAME)
            //    }).ToList();
            return variables.Select(@var => new ContractVariable
            {
                VarLabel = @var.Name,
                VarName = @var.Id.ToString(),
                VarValue = MapContractVariable(contractObj, @var.Name)
            }).ToList();
        }
        /// <summary>
        /// 获取合同文本自定义变量
        /// </summary>
        /// <param name="cttextid">合同文本id</param>
        /// <returns>合同文本自定义变量列表</returns>
        public IList<ContractVariable> GetCustomVariables(Int32 cttextid)
        {

            var _contractTextObj = Db.Set<ContText>().Where(p => p.Id == cttextid).FirstOrDefault();
            if (_contractTextObj == null)
                return null;
            IList<ContTxtTempVarStore> varsDef = Db.Set<ContTxtTempVarStore>()
                .Where(p => p.TempHistId == _contractTextObj.TemplateId && p.IsCustomer == 1 && p.Isdelete != 1)
                .ToList();

            ContTxtTempVarStore @var = null;
            if (varsDef != null && varsDef.Count > 0)
            {
                IList<ContractVariable> cuVars = new List<ContractVariable>();
                for (UInt16 i = 0; i < varsDef.Count; i++)
                {
                    ContractVariable cuvar = new ContractVariable();
                    @var = varsDef[i];
                    cuvar.VarLabel = @var.Name == null ? String.Empty : @var.Name;
                    cuvar.VarName = @var.OriginalId == null ? @var.Id.ToString() : @var.OriginalId.Value.ToString();
                    cuvar.VarValue = null;
                    cuVars.Add(cuvar);
                }
                return cuVars;
            }
            return null;

        }

        /// <summary>
        /// 系统变量映射
        /// </summary>
        /// <param name="contractObj">合同对象</param>
        /// <param name="varName">变量名称</param>
        /// <returns>变量值</returns>
        public string MapContractVariable( ContractInfo contractObj, String varName)
        {
            var mainInfo = Db.Set<DeptMain>().Where(a => a.DeptId == contractObj.MainDeptId).AsNoTracking().FirstOrDefault();
            const String dateTimeFormat = "yyyy年MM月dd日";
            if (String.IsNullOrEmpty(varName) || contractObj == null)
                return String.Empty;
            String retval = null;
            switch (varName)
            {
                case "Contract Name":
                case "合同名称":
                    retval = contractObj.Name;
                    break;
                case "Contract Number":
                case "合同编号":
                    retval = contractObj.Code;
                    break;
                case "Contract Amount In Figures":
                case "合同金额小写":
                    retval = String.Format("{0:f2}", contractObj.AmountMoney);
                    break;
                case "Contract Amount In Words":
                case "合同金额大写":
                    retval = DecimalToCHTString.Convert(String.Format("{0:f2}", contractObj.AmountMoney), true);
                    break;
                case "Currency":
                case "币种":
                    retval = RedisValueUtility.GetCurrencyName(contractObj.CurrencyId); //contractObj.Currency != null ? contractObj.Currency.Name : String.Empty;
                    break;
                //case "Contract Delivery Address":
                //case "合同收货地址":
                //    retval = contractObj.WOO_DELIVERY_INFO != null ? contractObj.WOO_DELIVERY_INFO.ADDRESS : String.Empty;
                //    break;
                //case "Contract Delivery Zip Code":
                //case "合同收货邮编":
                //    retval = contractObj.WOO_DELIVERY_INFO != null ? contractObj.WOO_DELIVERY_INFO.ZIP_CODE : String.Empty;
                //    break;
                //case "Contract Receiver（Consignee）":
                //case "合同收货人":
                //    retval = contractObj.WOO_DELIVERY_INFO != null ? contractObj.WOO_DELIVERY_INFO.CONSIGNEE : String.Empty;
                //    break;
                //case "Contract Receiver Telephone":
                //case "合同收货人联系电话":
                //    retval = contractObj.WOO_DELIVERY_INFO != null ? contractObj.WOO_DELIVERY_INFO.TEL : String.Empty;
                //    break;
                //case "Contract Delivery Mode":
                //case "合同收货方式":
                //    retval = contractObj.DELIVERY_REMARK == null ? String.Empty : contractObj.DELIVERY_REMARK;
                //    break;
                case "Contract Sighed Date":
                case "合同签订日期":
                    retval = !contractObj.SngnDateTime.HasValue ? null : contractObj.SngnDateTime.Value.ToString(dateTimeFormat);
                    break;
                case "Contract Effect Date":
                case "合同生效日期":
                    retval = contractObj.EffectiveDateTime == null ? null : contractObj.EffectiveDateTime.Value.ToString(dateTimeFormat);
                    break;
                case "Contract Planned Finish Date":
                case "合同计划完成日期":
                    retval = contractObj.PlanCompleteDateTime == null ? null : contractObj.PlanCompleteDateTime.Value.ToString(dateTimeFormat);
                    break;
                case "Contract Signatory":
                case "签约主体":
                    retval = contractObj.MainDept != null ? contractObj.MainDept.Name : String.Empty;
                    break;
                case "Contract Signatory Legal Person":
                case "签约主体法定代表人":
                    retval = mainInfo != null ? mainInfo.LawPerson : String.Empty;
                    break;
                case "Contract Signatory Tax Number":
                case "签约主体税务登记号":
                    retval = mainInfo != null ? mainInfo.TaxId : String.Empty;
                    break;
                case "Contract Signatory Deposit Bank":
                case "签约主体开户银行":
                    retval = mainInfo != null ? mainInfo.BankName : String.Empty;
                    break;
                case "Contract Signatory Account":
                case "签约主体账号":
                    retval = mainInfo != null ? mainInfo.Account : String.Empty;
                    break;
                case "Contract Signatory Address":
                case "签约主体地址":
                    retval = mainInfo != null ? mainInfo.Address : String.Empty;
                    break;
                case "Contract Signatory Zip Code":
                case "签约主体邮编":
                    retval = mainInfo != null ? mainInfo.ZipCode : String.Empty;
                    break;
                case "Contract Signatory Tel.No":
                case "签约主体电话":
                    retval = mainInfo != null ? mainInfo.TelePhone : String.Empty;
                    break;
                case "Contract Signatory Invoice Name":
                case "签约主体开票名称":
                    retval = mainInfo != null ? mainInfo.InvoiceName : String.Empty;
                    break;
                case "Contract Signatory Fax":
                case "签约主体传真":
                    retval = mainInfo != null ? mainInfo.Fax : String.Empty;
                    break;
                case "Contract Party":
                case "合同对方":
                    retval = contractObj.Comp != null ? contractObj.Comp.Name : String.Empty;
                    break;
                case "Contract Party Legal Person":
                case "对方法定代表人":
                    retval = contractObj.Comp != null ? contractObj.Comp.LegalPerson : String.Empty;
                    break;
                case "Contract Party Tel.No":
                case "对方联系电话":
                    retval = contractObj.Comp != null ? contractObj.Comp.FirstContactTel : String.Empty;
                    break;
                case "Contract Party Address":
                case "对方联系地址":
                    retval = contractObj.Comp != null ? contractObj.Comp.Address : String.Empty;
                    break;
                case "Contract Party Zip Code":
                case "对方邮编":
                    retval = contractObj.Comp != null ? contractObj.Comp.PostCode : String.Empty;
                    break;
                case "Contract Party Fax":
                case "对方传真":
                    retval = contractObj.Comp != null ? contractObj.Comp.Fax : String.Empty;
                    break;
                case "Customer Invoice Name":
                case "客户开票名称":
                    retval = contractObj.Comp != null ? contractObj.Comp.InvoiceTitle : String.Empty;
                    break;
                case "Customer Invoice Add":
                case "客户开票地址电话":
                    retval = contractObj.Comp != null ? contractObj.Comp.InvoiceAddress : String.Empty;
                    break;
                case "Customer Invoice Tel":
                case "客户开票电话":
                    retval = contractObj.Comp != null ? contractObj.Comp.InvoiceTel : String.Empty;
                    break;
                case "Customer Invoice Deposit Bank":
                case "客户开户银行":
                    retval = contractObj.Comp != null ? contractObj.Comp.BankName : String.Empty;
                    break;
                case "Customer Invoice Deposit Account Number":
                case "客户开户账号":
                    retval = contractObj.Comp != null ? contractObj.Comp.BankAccount : String.Empty;
                    break;
                case "Customer Tax Number":
                case "客户税务登记号":
                    retval = contractObj.Comp != null ? contractObj.Comp.TaxIdentification : String.Empty;
                    break;
                case "Supplier Deposit Bank":
                case "供应商开户银行":
                    retval = contractObj.Comp != null ? contractObj.Comp.BankName : String.Empty;
                    break;
                case "Supplier Account":
                case "供应商账号":
                    retval = contractObj.Comp != null ? contractObj.Comp.BankAccount : String.Empty;
                    break;
                case "Current Date":
                case "当前日期":
                    retval = DateTime.Now.ToString(dateTimeFormat);
                    break;
                case "Subjectmatter Details":
                case "标的明细":
                case "Subjectmatter Summary":
                case "标的概要":
                case "Contract Subjectmatter":
                case "合同标的":
                    retval = String.Empty;
                    break;
                default:
                    retval = String.Empty;
                    break;
            };
            return retval;
        }
    }
}
