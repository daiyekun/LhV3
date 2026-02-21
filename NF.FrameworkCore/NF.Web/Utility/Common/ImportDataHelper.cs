using AutoMapper;
using NF.Model.Models;
using NF.ViewModel.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NF.Web.Utility.Common
{
    public class ImportDataHelper
    {
        private IMapper _IMapper;
        /// <summary>
        /// 导入询价数据
        /// </summary>
        /// <param name="fullfileName">文件名称</param>
        /// <param name="datas">合同对方类别集合</param>
        /// <returns></returns>
        public static IList<WinningInq> ImportCompany(string fullfileName, IList<DataDictionary> datas, IList<UserInfor> users)
        {

            FileInfo file = new FileInfo(fullfileName);
            try
            {
                IList<WinningInq> listcomp = new List<WinningInq>();
                using (ExcelPackage package = new ExcelPackage(file))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                    int rowCount = worksheet.Dimension.Rows;
                    int ColCount = worksheet.Dimension.Columns;
                    //bool bHeaderRow = true;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        WinningInq Winning = new WinningInq();
                        Winning.WinningName = worksheet.Cells[$"A{row}"].Value.ToString();
                        Winning.WinningUntiId = worksheet.Cells[$"B{row }"].Value.ToString();
                        Winning.WinningModel = worksheet.Cells[$"C{row}"].Value.ToString();
                      
                        var sl= worksheet.Cells[$"D{row}"].Value.ToString();
                        Winning.WinningQuantity = Convert.ToDecimal(sl);
                     
                        var dj= worksheet.Cells[$"E{row}"].Value.ToString();
                        Winning.WinningTotalPrice = Convert.ToDecimal(dj);
                    
                        var zj = worksheet.Cells[$"F{row}"].Value.ToString();
                        Winning.WinningUitprice = Convert.ToDecimal(zj);
                        //默认值
                      //  Winning.WinningQuantity = Convert.ToDecimal(sl);
                    //  Winning.Inqid = this.SessionCurrUserId;
                        Winning.IsDelete = 0;
                        listcomp.Add(Winning);
                    }
                }
                return listcomp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 导入约谈数据
        /// </summary>
        /// <param name="fullfileName"></param>
        /// <param name="datas"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        public static IList<WinningQue> ImportQue(string fullfileName, IList<DataDictionary> datas, IList<UserInfor> users)
        {

            FileInfo file = new FileInfo(fullfileName);
            try
            {
                IList<WinningQue> listcomp = new List<WinningQue>();
                using (ExcelPackage package = new ExcelPackage(file))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                    int rowCount = worksheet.Dimension.Rows;
                    int ColCount = worksheet.Dimension.Columns;
                    //bool bHeaderRow = true;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        WinningQue Winning = new WinningQue();
                        Winning.WinningName = worksheet.Cells[$"A{row}"].Value.ToString();
                        Winning.WinningUntiId = worksheet.Cells[$"B{row }"].Value.ToString();
                        Winning.WinningModel = worksheet.Cells[$"C{row}"].Value.ToString();
                        var sl = worksheet.Cells[$"D{row}"].Value.ToString();
                        Winning.WinningQuantity = Convert.ToDecimal(sl);

                        var dj = worksheet.Cells[$"E{row}"].Value.ToString();
                        Winning.WinningTotalPrice = Convert.ToDecimal(dj);

                        var zj = worksheet.Cells[$"F{row}"].Value.ToString();
                        Winning.WinningUitprice = Convert.ToDecimal(zj);
                        //  Winning.Inqid = this.SessionCurrUserId;
                        Winning.IsDelete = 0;
                        listcomp.Add(Winning);
                    }
                }
                return listcomp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 导入招标数据
        /// </summary>
        /// <param name="fullfileName"></param>
        /// <param name="datas"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        public static IList<WinningItem>   ImportItems(string fullfileName, IList<DataDictionary> datas, IList<UserInfor> users)
        {

            FileInfo file = new FileInfo(fullfileName);
            try
            {
                IList<WinningItem> listcomp = new List<WinningItem>();
                using (ExcelPackage package = new ExcelPackage(file))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                    int rowCount = worksheet.Dimension.Rows;
                    int ColCount = worksheet.Dimension.Columns;
                    //bool bHeaderRow = true;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        WinningItem Winning = new WinningItem();
                        Winning.WinningName = worksheet.Cells[$"A{row}"].Value.ToString();
                        Winning.WinningUntiId = worksheet.Cells[$"B{row }"].Value.ToString();
                        Winning.WinningModel = worksheet.Cells[$"C{row}"].Value.ToString();
                        var sl = worksheet.Cells[$"D{row}"].Value.ToString();
                        Winning.WinningQuantity = Convert.ToDecimal(sl);

                        var dj = worksheet.Cells[$"E{row}"].Value.ToString();
                        Winning.WinningTotalPrice = Convert.ToDecimal(dj);

                        var zj = worksheet.Cells[$"F{row}"].Value.ToString();
                        Winning.WinningUitprice = Convert.ToDecimal(zj);
                        //  Winning.Inqid = this.SessionCurrUserId;
                        Winning.IsDelete = 0;
                        listcomp.Add(Winning);
                    }
                }
                return listcomp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="fullfileName">文件名称</param>
        /// <param name="datas">合同对方类别集合</param>
        /// <returns></returns>
        public static IList<ContractInfo> ImportContract(string fullfileName,
            IList<DataDictionary> datas,
            IList<UserInfor> users
            , IList<Department> deptes
            , IList<CompanyInfo> compani
            , IList<CurrencyManager> currencies)
        {

            FileInfo file = new FileInfo(fullfileName);
            try
            {
                IList<ContractInfo> listcont = new List<ContractInfo>();
                using (ExcelPackage package = new ExcelPackage(file))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                    int rowCount = worksheet.Dimension.Rows;
                    int ColCount = worksheet.Dimension.Columns;
                    //bool bHeaderRow = true;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        ContractInfo contract = new ContractInfo();
                        //合同名称
                        contract.Name = worksheet.Cells[$"A{row}"].Value.ToString();
                        //合同编号
                        contract.Code = worksheet.Cells[$"B{row}"].Value.ToString();
                        //收付款
                        var sfk = worksheet.Cells[$"C{row}"].Value.ToString();
                        byte sfkId = 0;
                        switch (sfk)
                        {
                            case "收款":
                                sfkId = 0;
                                break;
                            case "付款":
                                sfkId = 1;
                                break;
                            default:
                                sfkId = 0;
                                break;

                        }
                        contract.FinanceType = sfkId;
                        var lb = worksheet.Cells[$"D{row}"].Value.ToString();
                        var lbinfo = datas.Where(a => a.Name == lb).FirstOrDefault();
                        contract.ContTypeId = lbinfo == null ? 0 : lbinfo.Id;//合同类别
                        //经办机构
                        var jbjg = worksheet.Cells[$"E{row}"].Value.ToString();
                        var jbjginfo = deptes.Where(a => a.Name == jbjg).FirstOrDefault();
                        contract.DeptId = jbjginfo == null ? 1 : jbjginfo.Id;
                        //合同对方
                        var htdf = worksheet.Cells[$"F{row}"].Value.ToString();
                        var dfinfo = compani.Where(a => a.Name == htdf).FirstOrDefault();
                        if (dfinfo == null)
                        {
                            continue;
                        }
                        else
                        {
                            contract.CompId = dfinfo.Id;
                        }

                        //签约主体
                        var qyzt = worksheet.Cells[$"G{row}"].Value.ToString();
                        var qyztinfo = deptes.Where(a => a.Name == qyzt).FirstOrDefault();
                        contract.MainDeptId = qyztinfo == null ? 1 : qyztinfo.Id;
                        //合同金额
                        var ftje = worksheet.Cells[$"H{row}"].Value.ToString();
                        //默认值
                        contract.AmountMoney = Convert.ToDecimal(ftje);
                        //币种
                        var bz = worksheet.Cells[$"I{row}"].Value.ToString();
                        var bzinfo = currencies.Where(a => a.Name == bz).FirstOrDefault();
                        if (bzinfo == null)
                        {
                            contract.CurrencyId = 1;
                            contract.CurrencyRate = 1;

                        }
                        else
                        {
                            contract.CurrencyId = bzinfo.Id;
                            contract.CurrencyRate = bzinfo.Rate;
                        }
                        //印花税
                        try
                        {
                            var yhs = worksheet.Cells[$"J{row}"].Value;
                            if (yhs == null)
                            {
                                contract.StampTax = 0;


                            }
                            else
                            {
                                contract.StampTax = Convert.ToDecimal(yhs);
                            }
                        }
                        catch (Exception)
                        {

                            contract.StampTax = 0;
                        }
                        //总 / 分包
                        try
                        {
                            var zfb = worksheet.Cells[$"K{row}"].Value;
                            if (zfb == null)
                            {
                                contract.ContDivision = 0;

                            }
                            else
                            {
                                if (sfkId == 0)
                                {
                                    contract.ContDivision = 1;
                                }
                                else
                                {
                                    contract.ContDivision = 2;
                                }

                            }
                        }
                        catch (Exception)
                        {

                            contract.ContDivision = 0;
                        }
                        //签订日期
                        var qdrq = worksheet.Cells[$"L{row}"].Value;
                        if (qdrq != null)
                        {
                            contract.SngnDateTime = Convert.ToDateTime(qdrq);
                        }
                        else
                        {
                            contract.SngnDateTime = null;
                        }
                        //生效日期
                        var sxrq = worksheet.Cells[$"M{row}"].Value;
                        if (sxrq != null)
                        {
                            contract.EffectiveDateTime = Convert.ToDateTime(sxrq);
                        }
                        else
                        {
                            contract.EffectiveDateTime = null;
                        }
                        //合同属性
                        var htsx = worksheet.Cells[$"N{row}"].Value;
                        if (htsx == null)
                        {
                            contract.IsFramework = 0;
                        }
                        else
                        {
                            contract.IsFramework = 1;
                        }
                        //创建人
                        var cjr = worksheet.Cells[$"O{row}"].Value;
                        if (cjr != null)
                        {
                            var jlr = users.Where(a => a.Name == cjr.ToString()).FirstOrDefault();
                            contract.CreateUserId = jlr == null ? 1 : jlr.Id;
                        }
                        else
                        {
                            contract.CreateUserId = 1;

                        }
                        //创建时间
                        try
                        {
                            var cjsj = worksheet.Cells[$"P{row}"].Value;
                            if (cjsj == null)
                            {
                                contract.CreateDateTime = DateTime.Now;


                            }
                            else
                            {
                                contract.CreateDateTime = Convert.ToDateTime(cjsj);
                            }
                        }
                        catch (Exception)
                        {

                            contract.CreateDateTime = DateTime.Now;
                        }
                        contract.PlanCompleteDateTime = null;
                        contract.ActualCompleteDateTime = null;
                        contract.ModifyDateTime = DateTime.Now;
                        contract.PerformanceDateTime = null;
                        contract.ModifyUserId = 1;
                        contract.IsDelete = 0;
                        contract.ContState = 0;
                        //0:未执行，1：执行中
                        //2：已作废，3已终止 4：已完成




                        listcont.Add(contract);


                    }

                }

                return listcont;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
