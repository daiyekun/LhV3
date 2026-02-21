using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NF.ViewModel.Models.Common
{
    public class SysFunctionHelper
    {
        public static IList<SysModelFunction> GetListMFunction(int ?modeId)
        {
            IList<SysModelFunction> listFunction= new List<SysModelFunction>();
           

            #region 客户
            listFunction.Add(new SysModelFunction
            {
                Id = 4,
                Name = "新增客户",
                FunIdentify = "addcustomer",
                ModelId = 1029,
                Remark = "新增客户",
                PrmssionType = 1,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 5,
                Name = "删除客户",
                FunIdentify = "deletecustomer",
                ModelId = 1029,
                Remark = "删除客户",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 6,
                Name = "修改客户",
                FunIdentify = "updatecustomer",
                ModelId = 1029,
                Remark = "修改客户",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 7,
                Name = "查看客户列表",
                FunIdentify = "querycustomerlist",
                ModelId = 1029,
                Remark = "查看客户列表",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 8,
                Name = "查看客户详情",
                FunIdentify = "querycustomerview",
                ModelId = 1029,
                Remark = "查看客户详情",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 9,
                Name = "修改客户次要字段",
                FunIdentify = "updatecustomerminor",
                ModelId = 1029,
                Remark = "修改客户次要字段",
                PrmssionType = 2,
            });
            listFunction.Add(new SysModelFunction
            {
                Id = 10,
                Name = "修改客户状态",
                FunIdentify = "updatecustomerstate",
                ModelId = 1029,
                Remark = "修改客户状态",
                PrmssionType = 2,
            });
            #endregion

            #region 供应商
            listFunction.Add(new SysModelFunction
            {
                Id = 11,
                Name = "新增供应商",
                FunIdentify = "addsupplier",
                ModelId = 1030,
                Remark = "新增供应商",
                PrmssionType = 1,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 12,
                Name = "删除供应商",
                FunIdentify = "deletesupplier",
                ModelId = 1030,
                Remark = "删除供应商",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 13,
                Name = "修改供应商",
                FunIdentify = "updatesupplier",
                ModelId = 1030,
                Remark = "修改供应商",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 14,
                Name = "查看供应商列表",
                FunIdentify = "querysupplierlist",
                ModelId = 1030,
                Remark = "查看供应商列表",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 15,
                Name = "查看供应商详情",
                FunIdentify = "querysupplierview",
                ModelId = 1030,
                Remark = "查看供应商详情",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 16,
                Name = "修改供应商次要字段",
                FunIdentify = "updatesupplierminor",
                ModelId = 1030,
                Remark = "修改供应商次要字段",
                PrmssionType = 2,
            });
            listFunction.Add(new SysModelFunction
            {
                Id = 17,
                Name = "修改供应商状态",
                FunIdentify = "updatesupplierstate",
                ModelId = 1030,
                Remark = "修改供应商状态",
                PrmssionType = 2,
            });
            #endregion

            #region 其他对方

            listFunction.Add(new SysModelFunction
            {
                Id = 18,
                Name = "新增其他对方",
                FunIdentify = "addother",
                ModelId = 1031,
                Remark = "新增其他对方",
                PrmssionType = 1,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 19,
                Name = "删除其他对方",
                FunIdentify = "deleteother",
                ModelId = 1031,
                Remark = "删除其他对方",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 20,
                Name = "修改其他对方",
                FunIdentify = "updateother",
                ModelId = 1031,
                Remark = "修改其他对方",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 21,
                Name = "查看其他对方列表",
                FunIdentify = "queryotherlist",
                ModelId = 1031,
                Remark = "查看其他对方列表",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 22,
                Name = "查看其他对方详情",
                FunIdentify = "queryotherview",
                ModelId = 1031,
                Remark = "查看其他对方详情",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 23,
                Name = "修改其他对方次要字段",
                FunIdentify = "updateotherminor",
                ModelId = 1031,
                Remark = "修改其他对方次要字段",
                PrmssionType = 2,
            });
            listFunction.Add(new SysModelFunction
            {
                Id = 24,
                Name = "修改其他对方状态",
                FunIdentify = "updateotherstate",
                ModelId = 1031,
                Remark = "修改其他对方状态",
                PrmssionType = 2,
            });
            #endregion

            #region 项目 3030
            listFunction.Add(new SysModelFunction
            {
                Id = 25,
                Name = "新增项目",
                FunIdentify = "addproject",
                ModelId = 3030,
                Remark = "新增项目",
                PrmssionType = 1,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 26,
                Name = "删除项目",
                FunIdentify = "deleteproject",
                ModelId = 3030,
                Remark = "删除项目",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 27,
                Name = "修改项目",
                FunIdentify = "updateproject",
                ModelId = 3030,
                Remark = "修改项目",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 28,
                Name = "查看项目列表",
                FunIdentify = "queryprojectlist",
                ModelId = 3030,
                Remark = "查看项目列表",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 29,
                Name = "查看项目详情",
                FunIdentify = "queryprojectview",
                ModelId = 3030,
                Remark = "查看项目详情",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 30,
                Name = "修改项目次要字段",
                FunIdentify = "updateprojectminor",
                ModelId = 3030,
                Remark = "修改项目次要字段",
                PrmssionType = 2,
            });
            listFunction.Add(new SysModelFunction
            {
                Id = 31,
                Name = "修改项目状态",
                FunIdentify = "updateprojectstate",
                ModelId = 3030,
                Remark = "修改项目状态",
                PrmssionType = 2,
            });
            #endregion

            #region 收款合同 3033

            listFunction.Add(new SysModelFunction
            {
                Id = 32,
                Name = "新建收款合同",
                FunIdentify = "addcollcont",
                ModelId = 3033,
                Remark = "新建收款合同",
                PrmssionType = 1,

            });
            listFunction.Add(new SysModelFunction
            {
                Id = 33,
                Name = "删除收款合同",
                FunIdentify = "delcollcont",
                ModelId = 3033,
                Remark = "删除收款合同",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 34,
                Name = "修改收款合同",
                FunIdentify = "updatecollcont",
                ModelId = 3033,
                Remark = "修改收款合同",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 35,
                Name = "查看收款合同列表",
                FunIdentify = "querycollcontlist",
                ModelId = 3033,
                Remark = "查看收款合同列表",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 36,
                Name = "查看收款合同详情",
                FunIdentify = "querycollcontview",
                ModelId = 3033,
                Remark = "查看收款合同详情",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 37,
                Name = "修改收款合同次要字段",
                FunIdentify = "updatecollcontminor",
                ModelId = 3033,
                Remark = "修改收款合同次要字段",
                PrmssionType = 2,
            });
            listFunction.Add(new SysModelFunction
            {
                Id = 38,
                Name = "修改收款合同状态",
                FunIdentify = "updatecollcontstate",
                ModelId = 3033,
                Remark = "修改收款合同状态",
                PrmssionType = 2,
            });
            #endregion

            #region 付款合同 3034

            listFunction.Add(new SysModelFunction
            {
                Id = 39,
                Name = "新建付款合同",
                FunIdentify = "addpaycont",
                ModelId = 3034,
                Remark = "新建付款合同",
                PrmssionType = 1,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 40,
                Name = "删除付款合同",
                FunIdentify = "delpaycont",
                ModelId = 3034,
                Remark = "删除付款合同",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 41,
                Name = "修改付款合同",
                FunIdentify = "updatepaycont",
                ModelId = 3034,
                Remark = "修改付款合同",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 42,
                Name = "查看付款合同列表",
                FunIdentify = "querypaycontlist",
                ModelId = 3034,
                Remark = "查看付款合同列表",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 43,
                Name = "查看付款合同详情",
                FunIdentify = "querypaycontview",
                ModelId = 3034,
                Remark = "查看付款合同详情",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 44,
                Name = "修改付款合同次要字段",
                FunIdentify = "updatepaycontminor",
                ModelId = 3034,
                Remark = "修改付款合同次要字段",
                PrmssionType = 2,
            });
            listFunction.Add(new SysModelFunction
            {
                Id = 45,
                Name = "修改付款合同状态",
                FunIdentify = "updatepaycontstate",
                ModelId = 3034,
                Remark = "修改付款合同状态",
                PrmssionType = 2,
            });
            #endregion

            #region 合同文本
            listFunction.Add(new SysModelFunction
            {
                Id = 46,
                Name = "盖章权限",
                FunIdentify = "collconttextseal",
                ModelId = 3039,
                Remark = "盖章权限",
                PrmssionType = 1

            });
            listFunction.Add(new SysModelFunction
            {
                Id = 47,
                Name = "归档权限",
                FunIdentify = "collconttextarchive",
                ModelId = 3039,
                Remark = "归档权限",
                PrmssionType = 1

            });
            listFunction.Add(new SysModelFunction
            {
                Id = 48,
                Name = "盖章权限",
                FunIdentify = "payconttextseal",
                ModelId = 3040,
                Remark = "盖章权限",
                PrmssionType = 1

            });
            listFunction.Add(new SysModelFunction
            {
                Id = 49,
                Name = "归档权限",
                FunIdentify = "payconttextarchive",
                ModelId = 3040,
                Remark = "归档权限",
                PrmssionType = 1

            });

            #endregion

            #region 收票
            listFunction.Add(new SysModelFunction
            {
                Id = 50,
                Name = "建立/修改发票(收票)",
                FunIdentify = "addOrUpdateInvoice",
                ModelId = 3041,
                Remark = "建立/修改发票(收票)",
                PrmssionType = 2,
            });
            
            listFunction.Add(new SysModelFunction
            {
                Id = 52,
                Name = "删除发票(收票)",
                FunIdentify = "deleteInvoice",
                ModelId = 3041,
                Remark = "删除发票(收票)",
                PrmssionType = 2,
            });
            listFunction.Add(new SysModelFunction
            {
                Id = 53,
                Name = "确认/打回发票(收票)",
                FunIdentify = "ConfirmOrBackInvoice",
                ModelId = 3041,
                Remark = "确认/打回发票(收票)",
                PrmssionType = 2,
            });
            #endregion

            #region 开票
            listFunction.Add(new SysModelFunction
            {
                Id = 54,
                Name = "建立/修改发票(开票)",
                FunIdentify = "addOrUpdateInvoiceOut",
                ModelId = 3042,
                Remark = "建立/修改发票(开票)",
                PrmssionType = 2,
            });
            
            listFunction.Add(new SysModelFunction
            {
                Id = 56,
                Name = "删除发票(开票)",
                FunIdentify = "deleteInvoiceOut",
                ModelId = 3042,
                Remark = "删除发票(开票)",
                PrmssionType = 2,
            });
            listFunction.Add(new SysModelFunction
            {
                Id = 57,
                Name = "确认/打回发票(开票)",
                FunIdentify = "ConfirmOrBackInvoiceOut",
                ModelId = 3042,
                Remark = "确认/打回发票(开票)",
                PrmssionType = 2,
            });
            #endregion

            #region 实际收款
            listFunction.Add(new SysModelFunction
            {
                Id = 58,
                Name = "建立/修改实际收款",
                FunIdentify = "addOrUpdateActFinanceColl",
                ModelId = 3044,
                Remark = "建立/修改实际收款",
                PrmssionType = 2,
            });

            listFunction.Add(new SysModelFunction
            {
                Id = 59,
                Name = "删除实际收款",
                FunIdentify = "deleteActFinanceColl",
                ModelId = 3044,
                Remark = "删除实际收款",
                PrmssionType = 2,
            });
            listFunction.Add(new SysModelFunction
            {
                Id = 60,
                Name = "确认/打回实际收款",
                FunIdentify = "confirmOrBackActFinanceColl",
                ModelId = 3044,
                Remark = "确认/打回实际收款",
                PrmssionType = 2,
            });
            #endregion

            #region 实际付款
            listFunction.Add(new SysModelFunction
            {
                Id = 61,
                Name = "建立/修改实际付款",
                FunIdentify = "addOrUpdateActFinancePay",
                ModelId = 3045,
                Remark = "建立/修改实际付款",
                PrmssionType = 2,
            });

            listFunction.Add(new SysModelFunction
            {
                Id = 62,
                Name = "删除实际付款",
                FunIdentify = "deleteActFinancePay",
                ModelId = 3045,
                Remark = "删除实际付款",
                PrmssionType = 2,
            });
            listFunction.Add(new SysModelFunction
            {
                Id = 63,
                Name = "确认/打回实际付款",
                FunIdentify = "confirmOrBackActFinancePay",
                ModelId = 3045,
                Remark = "确认/打回实际付款",
                PrmssionType = 2,
            });
            #endregion

            #region 单品管理
            listFunction.Add(new SysModelFunction
            {
                Id = 64,
                Name = "单品建立/修改",
                FunIdentify = "addBcInstance",
                ModelId = 4054,
                Remark = "单品建立/修改",
                PrmssionType = 1,
            });

            listFunction.Add(new SysModelFunction
            {
                Id = 65,
                Name = "单品列表查看",
                FunIdentify = "listBcInstance",
                ModelId = 4054,
                Remark = "单品列表查看",
                PrmssionType = 1,
            });
            listFunction.Add(new SysModelFunction
            {
                Id = 66,
                Name = "单品明细查看",
                FunIdentify = "detailBcInstance",
                ModelId = 4054,
                Remark = "单品明细查看",
                PrmssionType = 1,
            });
            #endregion
            #region 标的列表
            listFunction.Add(new SysModelFunction
            {
                Id = 67,
                Name = "收款合同标的列表",
                FunIdentify = "collSubList",
                ModelId = 4057,
                Remark = "收款合同标的列表",
                PrmssionType = 2,
            });
            listFunction.Add(new SysModelFunction
            {
                Id = 68,
                Name = "付款合同标的列表",
                FunIdentify = "paySubList",
                ModelId = 4058,
                Remark = "付款合同标的列表",
                PrmssionType = 2,
            });
            #endregion 
            #region 标的交付明细
            listFunction.Add(new SysModelFunction
            {
                Id = 69,
                Name = "收款合同标的交付明细",
                FunIdentify = "collSubDvDetailList",
                ModelId = 4060,
                Remark = "收款合同标的交付明细",
                PrmssionType = 2,
            });
            listFunction.Add(new SysModelFunction
            {
                Id = 70,
                Name = "付款合同标的交付明细",
                FunIdentify = "paySubDvDetailList",
                ModelId = 4061,
                Remark = "付款合同标的交付明细",
                PrmssionType = 2,
            });
            #endregion

            #region 询价管理 5057

            listFunction.Add(new SysModelFunction
            {
                Id = 71,
                Name = "新建询价",
                FunIdentify = "addInquiry",
                ModelId = 5057,
                Remark = "新建询价",
                PrmssionType = 1,

            });
            listFunction.Add(new SysModelFunction
            {
                Id = 72,
                Name = "删除询价",
                FunIdentify = "delInquiry",
                ModelId = 5057,
                Remark = "删除询价",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 73,
                Name = "修改询价",
                FunIdentify = "updateInquiry",
                ModelId = 5057,
                Remark = "修改询价",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 74,
                Name = "查看询价列表",
                FunIdentify = "queryInquirylist",
                ModelId = 5057,
                Remark = "查看询价列表",
                PrmssionType = 2,


            });
            //listFunction.Add(new SysModelFunction
            //{
            //    Id = 75,
            //    Name = "查看询价详情",
            //    FunIdentify = "queryInquiryview",
            //    ModelId = 5057,
            //    Remark = "查看询价详情",
            //    PrmssionType = 2,
            //});
            //listFunction.Add(new SysModelFunction
            //{
            //    Id = 76,
            //    Name = "修改询价次要字段",
            //    FunIdentify = "updateInquiryminor",
            //    ModelId = 5057,
            //    Remark = "修改询价次要字段",
            //    PrmssionType = 2,
            //});
            listFunction.Add(new SysModelFunction
            {
                Id = 77,
                Name = "修改询价状态",
                FunIdentify = "updateInquirystate",
                ModelId = 5057,
                Remark = "修改询价状态",
                PrmssionType = 2,
            });
            #endregion

            #region 招标 5058

            listFunction.Add(new SysModelFunction
            {
                Id = 78,
                Name = "新建招标",
                FunIdentify = "addzbcollcont",
                ModelId = 5058,
                Remark = "新建招标",
                PrmssionType = 1,

            });
            listFunction.Add(new SysModelFunction
            {
                Id = 79,
                Name = "删除招标",
                FunIdentify = "delzbcollcont",
                ModelId = 5058,
                Remark = "删除招标",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 80,
                Name = "修改招标",
                FunIdentify = "updatezbcollcont",
                ModelId = 5058,
                Remark = "修改招标",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 81,
                Name = "查看招标列表",
                FunIdentify = "querycollzbcontlist",
                ModelId = 5058,
                Remark = "查看招标列表",
                PrmssionType = 2,


            });
            //listFunction.Add(new SysModelFunction
            //{
            //    Id = 82,
            //    Name = "查看招标详情",
            //    FunIdentify = "queryzbview",
            //    ModelId = 5058,
            //    Remark = "查看招标详情",
            //    PrmssionType = 2,
            //});
            //listFunction.Add(new SysModelFunction
            //{
            //    Id = 83,
            //    Name = "修改招标次要字段",
            //    FunIdentify = "updatezbcollcontminor",
            //    ModelId = 5058,
            //    Remark = "修改招标次要字段",
            //    PrmssionType = 2,
            //});
            listFunction.Add(new SysModelFunction
            {
                Id = 84,
                Name = "修改招标状态",
                FunIdentify = "updatezbcollcontstate",
                ModelId = 5058,
                Remark = "修改招标状态",
                PrmssionType = 2,
            });
            #endregion

            #region 约谈管理 5059

            listFunction.Add(new SysModelFunction
            {
                Id = 85,
                Name = "新建洽谈",
                FunIdentify = "addQuestioning",
                ModelId = 5059,
                Remark = "新建洽谈",
                PrmssionType = 1,

            });
            listFunction.Add(new SysModelFunction
            {
                Id = 86,
                Name = "删除洽谈",
                FunIdentify = "delQuestioning",
                ModelId = 5059,
                Remark = "删除洽谈",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 87,
                Name = "修改洽谈",
                FunIdentify = "updateQuestioning",
                ModelId = 5059,
                Remark = "修改洽谈",
                PrmssionType = 2,


            });
            listFunction.Add(new SysModelFunction
            {
                Id = 88,
                Name = "查看约谈列表",
                FunIdentify = "queryQuestioning",
                ModelId = 5059,
                Remark = "查看约谈列表",
                PrmssionType = 2,


            });
            //listFunction.Add(new SysModelFunction
            //{
            //    Id = 89,
            //    Name = "查看洽谈详情",
            //    FunIdentify = "queryQuestioningview",
            //    ModelId = 5059,
            //    Remark = "查看洽谈详情",
            //    PrmssionType = 2,


            //});
            //listFunction.Add(new SysModelFunction
            //{
            //    Id = 90,
            //    Name = "修改洽谈次要字段",
            //    FunIdentify = "updateQuestioningminor",
            //    ModelId = 5059,
            //    Remark = "修改洽谈次要字段",
            //    PrmssionType = 2,
            //});
            listFunction.Add(new SysModelFunction
            {
                Id = 91,
                Name = "修改洽谈状态",
                FunIdentify = "updateQuestioningstate",
                ModelId = 5059,
                Remark = "修改洽谈状态",
                PrmssionType = 2,
            });
            #endregion

            #region 案件管理5074

            listFunction.Add(new SysModelFunction
            {
                Id = 92,
                Name = "添加案件管理",
                FunIdentify = "AddAjg",
                ModelId = 5074,
                Remark = "添加案件管理",
                PrmssionType = 1,
            });
            listFunction.Add(new SysModelFunction
            {
                Id = 93,
                Name = "案件管理查看",
                FunIdentify = "Ajgllist",
                ModelId = 5074,
                Remark = "案件管理查看",
                PrmssionType = 2,
            });
            listFunction.Add(new SysModelFunction
            {
                Id = 94,
                Name = "案件管理删除",
                FunIdentify = "AjgDelete",
                ModelId = 5074,
                Remark = "案件管理删除",
                PrmssionType = 2,
            });
            listFunction.Add(new SysModelFunction
            {
                Id = 95,
                Name = "案件管理修改状态",
                FunIdentify = "AjgUpdatedate",
                ModelId = 5074,
                Remark = "案件管理修改状态",
                PrmssionType = 2,
            });
            listFunction.Add(new SysModelFunction
            {
                Id = 96,
                Name = "案件管理修改",
                FunIdentify = "AjgUpdate",
                ModelId = 5074,
                Remark = "案件管理修改",
                PrmssionType = 2,
            });
            #endregion
            #region 纠纷上报5073

            listFunction.Add(new SysModelFunction
            {
                Id = 97,
                Name = "添加纠纷上报",
                FunIdentify = "AddJfsb",
                ModelId = 5073,
                Remark = "添加纠纷上报",
                PrmssionType = 1,
            });
            listFunction.Add(new SysModelFunction
            {
                Id = 98,
                Name = "纠纷上报查看",
                FunIdentify = "Jfsbllist",
                ModelId = 5073,
                Remark = "纠纷上报查看",
                PrmssionType = 2,
            });
            listFunction.Add(new SysModelFunction
            {
                Id = 99,
                Name = "纠纷上报删除",
                FunIdentify = "JfsbDelete",
                ModelId = 5073,
                Remark = "纠纷上报删除",
                PrmssionType = 2,
            });
            listFunction.Add(new SysModelFunction
            {
                Id = 100,
                Name = "纠纷上报修改状态",
                FunIdentify = "JfsbUpdatedate",
                ModelId = 5073,
                Remark = "纠纷上报修改状态",
                PrmssionType =2,
            });
            listFunction.Add(new SysModelFunction
            {
                Id = 101,
                Name = "纠纷上报修改",
                FunIdentify = "JfsbUpdate",
                ModelId = 5073,
                Remark = "纠纷上报修改",
                PrmssionType = 2,
            });
            #endregion
            return listFunction.Where(a => a.ModelId == modeId).ToList();

        }
    }
    /// <summary>
    /// 模块功能对象
    /// </summary>
    public class SysModelFunction
    {
        /// <summary>
        /// 功能ID-必须唯一
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 所属模块ID
        /// </summary>
        public int ModelId { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 模块标识字符串-必须唯一
        /// </summary>
        public string FunIdentify { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 权限类型：
        /// 1类：4是/5否
        /// 2类：1个人、2机构、3全部、6本机构、7本机构及子机构
        /// </summary>
        public int PrmssionType { get; set; }
        /// <summary>
        /// 数据范围-用来存储选择的部门
        /// </summary>
        public string PermssionRang { get; set; }

    }
}
