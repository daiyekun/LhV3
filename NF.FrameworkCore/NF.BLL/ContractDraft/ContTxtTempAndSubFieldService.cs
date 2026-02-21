using NF.Common.Utility;
using NF.ViewModel.Models.ContTxtTemplate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using NF.Model.Models;
using Common.Utility;
using NF.ViewModel.Models.ContractDraft;
using NF.Common.Extend;

namespace NF.BLL
{
    /// <summary>
    /// 合同文本模板标的字段操作
    /// </summary>
   public partial class ContTxtTempAndSubFieldService
    {
        /// <summary>
        /// 根据模板历史ID，显示格式，单品类别ID查询字段信息
        /// </summary>
        /// <param name="tempHistId"></param>
        /// <param name="fieldType">显示格式0: 合同标的统一格式; 1: 按业务品类设置格式</param>
        /// <param name="bcId">单类别ID：0：非业务</param>
        /// <returns>字段信息</returns>
        public IList<SubChkField> GetSubChkFields(int tempHistId, int fieldType, int bcId)
        {
            var listall=GetListEumField(fieldType, bcId);
            var lisfield = GetList( tempHistId,  fieldType,  bcId);
            IList<SubChkField> subChks = new List<SubChkField>();

            foreach (var item in listall)
            {
                var fd = new SubChkField();
                fd.id = item.Value;
                fd.name = item.Desc;
                fd.on = lisfield.Where(a => a.SubFieldId == item.Value).Any() ? true : false;
                subChks.Add(fd);
            }

            return subChks;

        }
        /// <summary>
        /// 获取字段
        /// </summary>
        /// <param name="fieldType"></param>
        /// <param name="bcId"></param>
        /// <returns></returns>
        private IList<EnumItemAttribute>  GetListEumField(int fieldType, int? bcId)
        {
            //统一字段格式
            var lists = EmunUtility.GetAttr(typeof(ObjectFieldEnum));
            if (fieldType == 1)
            {
                if ((bcId??0)<= 0)
                {
                    lists = EmunUtility.GetAttr(typeof(ObjectField2Enum));
                }
                else
                {
                    lists = EmunUtility.GetAttr(typeof(ObjectField3Enum));
                }
            }

            return lists;
        }

        /// <summary>
        /// 查询字段
        /// </summary>
        /// <param name="tempHistId">模板ID</param>
        /// <param name="fieldType">显示格式0: 合同标的统一格式; 1: 按业务品类设置格式</param>
        /// <param name="bcId">单品类别ID。0：非业务</param>
        /// <returns></returns>
        public IList<ContTempSubFiled> GetList(int tempHistId, int fieldType, int bcId)
        {
            var listfields = GetListEumField(fieldType,bcId);
            var query = from a in _ContTxtTempAndSubFieldSet
                        where a.TempHistId == tempHistId
                                && a.FieldType == fieldType
                                && (a.BcId??0) == bcId
                        select new
                        {
                            SubFieldId=a.SubFieldId,
                            Id=a.Id,
                            Sval=a.Sval,
                            TempHistId=a.TempHistId,
                            SortFd=a.SortFd,



                        };

            var loacl = from a in query.AsEnumerable()
                        select new ContTempSubFiled
                        {
                            SubFieldId = a.SubFieldId??0,
                            Id = a.Id,
                            Sval = a.Sval,
                            TempHistId = a.TempHistId??0,
                            Name= listfields.Where(p=>p.Value==a.SubFieldId).Any()? listfields.Where(p => p.Value == a.SubFieldId).FirstOrDefault().Desc:"",
                        };
            return loacl.ToList();

        }
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <param name="fieldType"></param>
        /// <param name="bcId"></param>
        /// <returns></returns>
       public LayPageInfo<ContTempSubFiled> GetList<s>(PageInfo<ContTxtTempAndSubField> pageInfo, Expression<Func<ContTxtTempAndSubField, bool>> whereLambda, Expression<Func<ContTxtTempAndSubField, s>> orderbyLambda, bool isAsc,int fieldType=0, int bcId=0)
        {
            var listfields = GetListEumField(fieldType, bcId);
            var tempquery = _ContTxtTempAndSubFieldSet.AsTracking().Where<ContTxtTempAndSubField>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<ContTxtTempAndSubField>))
            { //分页
                tempquery = tempquery.Skip<ContTxtTempAndSubField>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<ContTxtTempAndSubField>(pageInfo.PageSize);
            }
            var query = from a in tempquery
                        select new
                        {
                            SubFieldId = a.SubFieldId,
                            Id = a.Id,
                            Sval = a.Sval,
                            TempHistId = a.TempHistId,
                            SortFd = a.SortFd,
                        };
            var local = from a in query.AsEnumerable()
                        select new ContTempSubFiled
                        {
                            SubFieldId = a.SubFieldId ?? 0,
                            Id = a.Id,
                            Sval = a.Sval,
                            TempHistId = a.TempHistId ?? 0,
                            Name = listfields.Where(p => p.Value == a.SubFieldId).Any() ? listfields.Where(p => p.Value == a.SubFieldId).FirstOrDefault().Desc : "",
                        };
            return new LayPageInfo<ContTempSubFiled>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };

        }

        #region Word插件标的数据绑定
        /// <summary>
        /// 获取合同标的数据
        /// </summary>
        /// <param name="cttextid">合同文本ID</param>
        /// <param name="bc_id">业务类实例ID</param>
        /// <param name="field_type">字段类别</param>
        /// <returns>合同标的数据</returns>
        public IList<String[]> GetContractObjectsDataTable(Int32 cttextid, Int32 bc_id, Int32 field_type)
        {
            if (bc_id == 0 && field_type == 0)
            {
                return GetUniformedTableData(cttextid);
            }
            else if (bc_id == 0 && field_type == 1)
            {
                return GetNonCategorizedTableData(cttextid);
            }
            else if (bc_id > 0 && field_type == 1)
            {
                return GetCategorizedTableData(cttextid, bc_id);
            }
            return null;
        }

        /// <summary>
        /// 获取统一格式的合同标的数据
        /// </summary>
        /// <param name="cttextid">合同文本ID</param>
        /// <returns>合同标的数据</returns>
        private IList<String[]> GetUniformedTableData(Int32 cttextid)
        {

            var ContText = Db.Set<ContText>().AsNoTracking().Where(a => a.Id == cttextid).FirstOrDefault();
                if (ContText == null)
                    return null;

                var lstFields = Db.Set<ContTxtTempAndSubField>().AsNoTracking()
                    .Where(a =>
                        a.TempHistId == ContText.TemplateId &&
                        (a.BcId == null || a.BcId == 0) &&
                        a.FieldType == 0)
                    .ToList();

                if (lstFields.Count == 0)
                    return null;

                var lstSubj =Db.Set< ContSubjectMatter>().AsNoTracking()
                    .Include(a=>a.BcInstance).AsNoTracking()
                    .Where(a =>
                        a.ContId == ContText.ContId &&
                        (a.IsFromCategory == 0 || a.IsFromCategory == 1))
                    .ToList();

                if (lstSubj.Count == 0)
                    return null;

                var lstBcInstance = Db.Set<ContSubjectMatter>().AsNoTracking()
                    .Where(a =>
                        a.ContId == ContText.ContId &&
                        a.IsFromCategory == 1 &&
                        a.BcInstance != null)
                    .Select(a => a.BcInstance)
                    .Distinct()
                    .ToList();

                var datatable = new List<String[]>(lstSubj.Count + 2);
                for (var i = 0; i < lstSubj.Count + 2; i++)
                    datatable.Add(null);

                for (int i = 0; i < lstSubj.Count; i++)
                {
                    var ctObject = new ContractObject();
                    ctObject.SetValues(lstSubj[i]);


                    String[] row = new String[lstFields.Count];
                    datatable[i + 1] = row;
                    for (int j = 0; j < lstFields.Count; j++)
                        row[j] = MapUniformedContractObjectFields(lstFields[j].SubFieldId ?? 0,
                            ctObject, lstBcInstance);
                }

                var sumRowIndex = lstSubj.Count + 1;

                datatable[0] = new String[lstFields.Count];
                //datatable[sumRowIndex] = new String[lstFields.Count];
                for (int i = 0; i < lstFields.Count; i++)
                {
                    var colDef = lstFields[i];
                    datatable[0][i] = colDef.Sval ?? "";
                  
                }
                var lastCount = lstFields.Count;
                if (lastCount < 3)
                {
                    lastCount = 3;
                }
                datatable[sumRowIndex] = new String[lastCount];
                datatable[sumRowIndex][0] = "合计:";
                var heji = lstSubj.Select(a => a.SubTotal ?? 0).Sum();
                ConvertCurrency  c= new ConvertCurrency();
                datatable[sumRowIndex][1] = "大写:" + c.ConvertMoney(Convert.ToDouble(heji));
                datatable[sumRowIndex][lastCount - 1] = "小写:" + heji.ThousandsSeparator();

                return datatable;
            }

        /// <summary>
        /// 获取非业务类的合同标的表格数据
        /// </summary>
        /// <param name="cttextid">合同文本ID</param>
        /// <returns>合同标的数据</returns>
        private IList<String[]> GetNonCategorizedTableData(int cttextid)
        {
            
                var ContText =Db.Set<ContText>().Where(a => a.Id == cttextid).FirstOrDefault();
                if (ContText == null)
                {
                    return null;
                }
                var lstField = Db.Set<ContTxtTempAndSubField>().AsNoTracking()
                    .Where(a =>
                        a.TempHistId == ContText.TemplateId &&
                        (a.BcId == null || a.BcId == 0) &&
                        a.FieldType == 1)
                    .ToList();
                if (lstField.Count == 0)
                {
                    return null;
                }

                var lstSubj =Db.Set<ContSubjectMatter>().AsNoTracking()
                    .Include(a=>a.BcInstance)
                    .Where(a =>
                        a.ContId == ContText.ContId &&
                        (a.IsFromCategory == null || a.IsFromCategory.Value == 0) &&
                        (a.BcInstanceId == null || a.BcInstanceId.Value == 0))
                     .ToList();
                if (lstSubj.Count == 0)
                {
                    return null;
                }

                var datatable = new List<String[]>(lstSubj.Count + 2);
                for (int i = 0; i < lstSubj.Count + 2; i++)
                {
                    datatable.Add(null);
                }

                for (int i = 0; i < lstSubj.Count; i++)
                {
                    var ctObject = new ContractObject();
                    ctObject.SetValues(lstSubj[i]);
                    var row = new String[lstField.Count];
                    datatable[i + 1] = row;
                    for (int j = 0; j < lstField.Count; j++)
                    {
                        row[j] = MapNonCategorizedContractObjectFields(
                            lstField[j].SubFieldId ?? 0,
                            ctObject);
                    }
                }

                var sumRowIndex = lstSubj.Count + 1;
                datatable[0] = new String[lstField.Count];
                //datatable[sumRowIndex] = new String[lstField.Count];

                for (int i = 0; i < lstField.Count; i++)
                {
                    var colDef = new ContractObjectTableHeader();
                    colDef.SetValues(lstField[i]);

                    datatable[0][i] = lstField[i].Sval;
                    //if (lstField[i].IsTotal)
                    //{
                    //    datatable[sumRowIndex][i] = "SumUp";
                    //}
                }
                var lastCount = lstField.Count;
                if (lastCount < 3)
                {
                    lastCount = 3;
                }
                datatable[sumRowIndex] = new String[lastCount];
                datatable[sumRowIndex][0] = "合计:";
                var heji = lstSubj.Select(a => a.SubTotal ?? 0).Sum();
            ConvertCurrency c = new ConvertCurrency();
            datatable[sumRowIndex][1] = "大写:" + c.ConvertMoney(Convert.ToDouble(heji));
                datatable[sumRowIndex][lastCount - 1] = "小写:" + heji.ThousandsSeparator();

                return datatable;
            
            
        }
        /// <summary>
        /// 获取由bc_id 参数指定类别的合同标的表格数据
        /// </summary>
        /// <param name="cttextid">合同文本ID</param>
        /// <param name="bc_id">业务品类实例</param>
        /// <returns></returns>
        private IList<String[]> GetCategorizedTableData(int cttextid, int bc_id)
        {
            if (bc_id <= 0)
                return null;
           
                var ContText =Db.Set<ContText>().AsNoTracking().FirstOrDefault();
                if (ContText == null)
                    return null;

                var lstField = Db.Set<ContTxtTempAndSubField>().AsNoTracking()
                    .Where(a =>
                        a.TempHistId == ContText.TemplateId &&
                        a.BcId == bc_id &&
                        a.FieldType == 1)
                    .ToList();
                if (lstField.Count == 0)
                {
                    return null;
                }
                var lstSubj = Db.Set<ContSubjectMatter>().AsNoTracking()
                    .Include(a=>a.BcInstance)
                    .Where(a =>
                        a.ContId == ContText.ContId &&
                        a.IsFromCategory == 1 &&
                        a.BcInstanceId == bc_id)
                    .ToList();
                if (lstSubj == null)
                {
                    return null;
                }

                var lstBcInstance = Db.Set<BcInstance>().AsNoTracking()
                    .Where(a => a.Id == bc_id)
                    .ToList();

               

                var datatable = new List<String[]>(lstSubj.Count + 2);
                for (int x = 0; x < lstSubj.Count + 2; x++)
                    datatable.Add(null);

                for (int i = 0; i < lstSubj.Count; i++)
                {
                    var ctObject = new AggregatedContractObject();
                    ctObject.CtObject = new ContractObject();
                    ctObject.CtObject.SetValues(lstSubj[i]);
                    ctObject.BcData = lstSubj[i].BcInstance;

                    var row = new String[lstField.Count];
                    datatable[i + 1] = row;
                    for (int j = 0; j < lstField.Count; j++)
                    {
                        row[j] = MapCategorizedContractObjectFields(
                            lstField[j].SubFieldId ?? 0,
                            ctObject
                            );
                    }
                }
                var sumRowIndex = lstSubj.Count + 1;
                datatable[0] = new String[lstField.Count];
                //datatable[sumRowIndex] = new String[lstField.Count];

                for (int i = 0; i < lstField.Count; i++)
                {
                    datatable[0][i] = lstField[i].Sval == null ? "" : lstField[i].Sval;
                    //if (lstField[i].IsTotal)
                    //{
                    //    datatable[sumRowIndex][i] = "SumUp";
                    //}                   
                }
                var lastCount = lstField.Count;
                if (lastCount < 3)
                {
                    lastCount = 3;
                }
                datatable[sumRowIndex] = new String[lastCount];
                datatable[sumRowIndex][0] = "合计:";
                var heji = lstSubj.Select(a => a.SubTotal ?? 0).Sum();
            ConvertCurrency c = new ConvertCurrency();
                datatable[sumRowIndex][1] = "大写:" + c.ConvertMoney(Convert.ToDouble(heji));
                datatable[sumRowIndex][lastCount - 1] = "小写:" + heji.ThousandsSeparator();

                return datatable;
            

            
        }
        /// <summary>
        /// 非业务类的标的字段映射
        /// </summary>
        /// <param name="dataIndex">字段索引</param>
        /// <param name="ctObject">合同标的对象</param>
        /// <returns></returns>
        private String MapNonCategorizedContractObjectFields(Int32 dataIndex, ContractObject ctObject)
        {
            if ((dataIndex <= 0 || dataIndex > 11) || ctObject == null)
                return null;
            String retval = null;
            Decimal decimalVal = 0;
            switch (dataIndex)
            {
                case 1:
                    retval = ctObject.Name;
                    break;
                case 2:
                    retval = ctObject.Spec;
                    break;
                case 3:
                    retval = ctObject.Stype;
                    break;
                case 4:
                    retval = ctObject.Unit;
                    break;
                case 5:
                    //retval = String.Format("{0:f6}", ctObject.Price);
                    //retval = FormatDecimal(retval);
                    retval =  ctObject.Price.ThousandsSeparator();
                    break;
                case 6:
                    //retval = String.Format("{0:f6}", ctObject.Amount);
                    //retval = FormatDecimal(retval);
                    retval = ctObject.Amount.ThousandsSeparator();
                    break;
                case 7:
                    retval = String.Format("{0:f2}", ctObject.SubTotal ?? 0);
                    retval = FormatDecimal(retval);
                    break;
                case 8:
                    //retval = String.Format("{0:f6}", ctObject.SalePrice ?? 0);
                    //retval = FormatDecimal(retval);
                    retval =  ctObject.SalePrice.ThousandsSeparator();
                    break;
                case 9:
                    decimalVal = ctObject.DiscountRate == null ? 0 : ctObject.DiscountRate.Value;
                    retval = FormatDecimal(String.Format("{0:f2}%", decimalVal > 1 ? decimalVal : decimalVal * 100));
                    break;
                case 10:
                    retval = ctObject.PlanDateTime == null ? null : ctObject.PlanDateTime.Value.ToString("yyyy年MM月dd日");
                    break;
                case 11:
                    retval = ctObject.Remark;
                    break;
            }
            return retval;
        }

        /// <summary>
        /// 统一格式的合同标的字段映射
        /// </summary>
        /// <param name="filedName">字段名</param>
        /// <param name="ctObject">合同标的</param>
        /// <param name="fieldsOfObjects">业务类合同标的属性列表</param>
        /// <returns>字段值</returns>
        private String MapUniformedContractObjectFields(Int32 dataIndex, ContractObject ctObject, IList<BcInstance> fieldsOfObjects)
        {
            if ((dataIndex <= 0 || dataIndex > 13) || ctObject == null)
                return String.Empty;

            BcInstance fieldsOfObject = null;
            if (ctObject.IsFromCategory == 1)
            {
                if (ctObject.BcInstanceId == null || fieldsOfObjects == null)
                    return String.Empty;
                fieldsOfObject = fieldsOfObjects.SingleOrDefault(fldset => fldset.Id == ctObject.BcInstanceId);
                if (fieldsOfObject == null)
                    return String.Empty;
            }
            String retval = null;
            Nullable<Decimal> decimalVal = 0;
            switch (dataIndex)
            {
                case 1://销售报价
                    //retval = fieldsOfObject == null ? String.Format("{0:f6}", ctObject.SalePrice) : String.Format("{0:f6}", fieldsOfObject.Price);
                    //retval = FormatDecimal(retval);
                    retval = fieldsOfObject == null ?  ctObject.SalePrice.ThousandsSeparator() :  fieldsOfObject.Price.ThousandsSeparator();
                    break;
                case 2:
                    retval = ctObject.Name;
                    break;
                case 3:
                    //retval = (fieldsOfObject == null || fieldsOfObject.Price == null) ? String.Format("{0:f6}", ctObject.Price) : String.Format("{0:f6}", fieldsOfObject.Price);
                    //retval = FormatDecimal(retval);
                    retval = (fieldsOfObject == null || fieldsOfObject.Price == null) ?  ctObject.Price.ThousandsSeparator() :fieldsOfObject.Price.ThousandsSeparator();
                    break;
                case 4:
                    retval = ctObject.BcInstance == null ? String.Empty : ctObject.BcInstance.Code;
                    break;
                case 5:
                    retval = ctObject.BcInstance == null ? String.Empty : ctObject.BcInstance.Name;
                    break;
                case 6:
                    retval = fieldsOfObject == null ? ctObject.Unit : fieldsOfObject.Unit;
                    break;
                case 7:
                    retval = ctObject.PlanDateTime == null ? null : ctObject.PlanDateTime.Value.ToString("yyyy年MM月dd日");
                    break;
                case 8:
                    //retval = (ctObject.MingYiPrice == null || ctObject.MingYiPrice.Value == 0) ? "0"
                    //    : String.Format("{0:f6}", ctObject.MingYiPrice);
                    //retval = FormatDecimal(retval);
                    retval = (ctObject.MingYiPrice == null || ctObject.MingYiPrice.Value == 0) ? "0"
                        :  ctObject.MingYiPrice.ThousandsSeparator();
                    break;
                case 9:
                    retval = String.Format("{0:f2}%", ctObject.MingYiRate == null ? 0 : (ctObject.MingYiRate.Value * 100));
                    break;
                case 10:
                    //retval = String.Format("{0:f6}", ctObject.Amount);
                    // retval = FormatDecimal(retval);
                    retval =  ctObject.Amount.ThousandsSeparator();
                    break;
                case 11:
                    retval = ctObject.SubTotal == null ? null : ctObject.SubTotal.ThousandsSeparator();
                    //retval = FormatDecimal(retval);
                    break;
                case 12:
                    decimalVal = (fieldsOfObject == null )
                        ? ctObject.DiscountRate: 0;
                    if (decimalVal != null)
                        retval = FormatDecimal(String.Format("{0:f2}%", decimalVal.Value > 1 ? decimalVal.Value : decimalVal.Value * 100));
                    else
                        retval = "0%";
                    break;
                case 13:
                    retval = fieldsOfObject == null ? ctObject.Remark : fieldsOfObject.Remark;
                    break;
                default:
                    retval = null;
                    break;
            }
            return retval;
        }
        /// <summary>
        /// 带业务品类的合同标的字段映射
        /// </summary>
        /// <param name="dataIndex">字段索引</param>
        /// <param name="ctObject">标的对象</param>
        /// <returns>字段值</returns>
        private String MapCategorizedContractObjectFields(Int32 dataIndex, AggregatedContractObject ctObject)
        {
            String retval = null;
            Nullable<Decimal> decimalVal = 0;
            switch (dataIndex)
            {
                case 1:
                    retval = ctObject.CtObject.Name;
                    break;
                case 2:
                    retval = ctObject.CtObject.BcInstance == null ? String.Empty : ctObject.CtObject.BcInstance.Name;
                    break;
                case 3:
                    retval = ctObject.CtObject.BcInstance == null ? String.Empty : ctObject.CtObject.BcInstance.Code;
                    break;
                case 4:
                    retval = ctObject.BcData == null ? ctObject.CtObject.Unit : ctObject.BcData.Unit;
                    break;
                case 5:
                    //retval = (ctObject.BcData == null || ctObject.BcData.Price == null)
                    //    ? String.Format("{0:f6}", ctObject.CtObject.Price) :
                    //    String.Format("{0:f6}", ctObject.BcData.Price ?? 0);
                    //retval = FormatDecimal(retval);
                    retval = (ctObject.BcData == null || ctObject.BcData.Price == null)
                        ?  ctObject.CtObject.Price.ThousandsSeparator() :
                         ctObject.BcData.Price.ThousandsSeparator();
                    break;
                case 6:
                    //retval = String.Format("{0:f6}", ctObject.CtObject.Amount);
                    //retval = FormatDecimal(retval);
                    retval =ctObject.CtObject.Amount.ThousandsSeparator();
                    break;
                case 7:
                    //retval = ctObject.BcData == null ? String.Format("{0:f6}", ctObject.CtObject.SalePrice) : String.Format("{0:f6}", ctObject.BcData.Price);
                    //retval = FormatDecimal(retval);
                    retval = ctObject.BcData == null ?  ctObject.CtObject.SalePrice.ThousandsSeparator() :  ctObject.BcData.Price.ThousandsSeparator();
                    break;
                case 8:
                    decimalVal = (ctObject.BcData == null )
                        ? ctObject.CtObject.DiscountRate : 0;
                    retval = FormatDecimal(String.Format("{0:f2}%", decimalVal.Value > 1 ? decimalVal.Value : (decimalVal.Value * 100)));
                    break;
                case 9:
                    //retval = (ctObject.CtObject.MingYiPrice == null || ctObject.CtObject.MingYiPrice == 0) ? "0" :
                    //    String.Format("{0:f6}", ctObject.CtObject.MingYiPrice.Value);
                    //retval = FormatDecimal(retval);
                    retval = (ctObject.CtObject.MingYiPrice == null || ctObject.CtObject.MingYiPrice == 0) ? "0" :
                         ctObject.CtObject.MingYiPrice.Value.ThousandsSeparator();
                    break;
                case 10:
                    retval = String.Format("{0:f2}%", ctObject.CtObject.MingYiRate == null ? 0 : ctObject.CtObject.MingYiRate.Value * 100);
                    retval = FormatDecimal(retval);
                    break;
                case 11:
                    //retval = ctObject.CtObject.SubTotal == null ? null : String.Format("{0:f2}", ctObject.CtObject.SubTotal);
                    //retval = FormatDecimal(retval);
                    retval = ctObject.CtObject.SubTotal == null ? null :  ctObject.CtObject.SubTotal.ThousandsSeparator();
                    break;
                case 12:
                    retval = ctObject.CtObject.PlanDateTime == null ? null : ctObject.CtObject.PlanDateTime.Value.ToString("yyyy年MM月dd日");
                    break;
                case 13:
                    retval = ctObject.BcData == null ? ctObject.CtObject.Remark : ctObject.BcData.Remark;
                    break;
                default:
                    
                    break;
            }
            return retval;
        }

        /// <summary>
        /// 去除小数点后多余的0
        /// </summary>
        /// <param name="decimalString">数字的字符串形式</param>
        /// <returns>格式化后的字符串</returns>
        private String FormatDecimal(String decimalString)
        {
            if (String.IsNullOrEmpty(decimalString))
                return decimalString;
            decimalString = decimalString.TrimEnd('0');
            if (decimalString.Length > 0 && '.'.Equals(decimalString.Last()))
                decimalString = decimalString.Remove(decimalString.Length - 1);
            return decimalString;
        }


        #endregion





    }

    /// <summary>
    /// 合同标的表头
    /// </summary>
    struct ContractObjectTableHeader
    {
        /// <summary>
        /// 标题
        /// =WOO_TEMPLATE_AND_OBJECT_FIELD.VALUE
        /// </summary>
        public String Label;
        /// <summary>
        /// =WOO_TEMPLATE_AND_OBJECT_FIELD.OBJECT_FIELD_ID
        /// </summary>
        public Int32 DataIndex;
        /// <summary>
        /// 是否合计?"1":"0"
        /// </summary>
        public String SumUp;
        public void SetValues(ContTxtTempAndSubField info)
        {
            this.Label = info.Sval;
            this.DataIndex = info.SubFieldId ?? 0;
            this.SumUp = info.IsTotal==1 ? "1" : "0";
        }
    }
}
