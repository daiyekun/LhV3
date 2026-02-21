using ImportData.Models;
using NF.Model.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportData.Utility.Common
{
    /// <summary>
    /// Excel导入
    /// </summary>
    public class ImportDataHelper
    {
        /// <summary>
        /// 导入合同对方数据
        /// </summary>
        /// <param name="fullfileName">文件名称</param>
        /// <param name="datas">合同对方类别集合</param>
        /// <returns></returns>
        public static IList<Company> ImportCompany(string fullfileName, IList<DataDictionary> datas,
            IList<UserInfor> users, IList<Country> Gj, IList<Province> Sf, IList<City> Xd)
        {

            FileInfo file = new FileInfo(fullfileName);
            try
            {
                IList<Company> listcomp = new List<Company>();
                //EPPlus 是商业版本，需要再次指定非商业化，如果有兴趣可以手动手改组件
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage package = new ExcelPackage(file))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;
                    int ColCount = worksheet.Dimension.Columns;
                    //bool bHeaderRow = true;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        Company company = new Company();
                        var dftype = worksheet.Cells[$"A{row}"].Text.ToString();
                      

                        switch (dftype)
                        {
                            case "客户 ":
                            case "客户":
                                company.Ctype = 0;
                                break;
                            case "供应商":
                                company.Ctype = 1;
                                break;
                            case "其他对方":
                                company.Ctype = 2;
                                break;
                        }
                        company.Name = worksheet.Cells[$"B{row}"].Text.ToString();//客户 供应商 其他对方 name
                        company.Code = worksheet.Cells[$"C{row }"].Text.ToString();//编号
                        var jb = worksheet.Cells[$"D{row}"].Text.ToString();//级别
                        var jbid = datas.Where(a => a.Name == jb && a.DtypeNumber == 5).FirstOrDefault();
                        company.LevelId = jbid == null ? 0 : jbid.Id;
                        var Xy = worksheet.Cells[$"E{row}"].Text.ToString();//信用等级
                        var Xyid = datas.Where(a => a.Name == Xy && a.DtypeNumber == 6).FirstOrDefault();
                        company.CareditId = Xyid == null ? 0 : Xyid.Id;

                        var lb = worksheet.Cells[$"F{row}"].Text.ToString();
                       
                        switch (dftype)
                        {
                            case "客户 ":
                            case "客户":
                                var lbinfo = datas.Where(a => a.Name == lb && a.DtypeNumber == 3).FirstOrDefault();
                                company.CompClassId = lbinfo == null ? 0 : lbinfo.Id; //客户类别
                                break;
                            case "供应商":
                                var lbinfos = datas.Where(a => a.Name == lb && a.DtypeNumber == 2).FirstOrDefault();
                                company.CompClassId = lbinfos == null ? 0 : lbinfos.Id; //客户类别
                                break;
                            case "其他对方":
                                var lbinfow = datas.Where(a => a.Name == lb && a.DtypeNumber == 4).FirstOrDefault();
                                company.CompClassId = lbinfow == null ? 0 : lbinfow.Id; //客户类别
                                break;

                        }
                       
                        var fistconnt = worksheet.Cells[$"G{row}"].Text.ToString();
                        var ids = Gj.Where(a => a.Name == fistconnt).FirstOrDefault();
                        company.CountryId = ids == null ? 0 : ids.Id;// 国家
                        var S = worksheet.Cells[$"H{row}"].Text.ToString();
                        var Ss = Sf.Where(a => a.Name == fistconnt).FirstOrDefault();
                        company.CountryId = ids == null ? 0 : ids.Id;// 省

                        var Xs = worksheet.Cells[$"I{row}"].Text.ToString();
                        var Xslist = Xd.Where(a => a.Name == fistconnt).FirstOrDefault();
                        company.CountryId = Xslist == null ? 0 : Xslist.Id;// 市县


                        company.Trade = worksheet.Cells[$"J{row}"].Text.ToString(); //行业
                        company.Address = worksheet.Cells[$"K{row}"].Text.ToString(); //地址
                        company.PostCode = worksheet.Cells[$"L{row}"].Text.ToString(); //邮编
                        company.Tel = worksheet.Cells[$"M{row}"].Text.ToString(); //电话
                        company.Fax = worksheet.Cells[$"N{row}"].Text.ToString(); //传真
                        company.RegisterCapital = worksheet.Cells[$"O{row}"].Text.ToString(); //注册资金
                        company.RegisterAddress = worksheet.Cells[$"P{row}"].Text.ToString(); //住所（注册地址）
                        try
                        {
                            company.FoundDateTime = Convert.ToDateTime(worksheet.Cells[$"Q{row}"].Text); //成立日期
                        }
                        catch (Exception)
                        {

                            company.FoundDateTime = null;
                        }

                        company.BusinessTerm = worksheet.Cells[$"R{row}"].Text.ToString(); //营业期限
                        try
                        {
                            company.ExpirationDateTime = Convert.ToDateTime(worksheet.Cells[$"S{row}"].Text); //营业执照截至日期
                        }
                        catch (Exception)
                        {

                            company.ExpirationDateTime = null;
                        }
                        company.InvoiceTitle = worksheet.Cells[$"T{row}"].Text.ToString(); //开票名称
                        company.TaxIdentification = worksheet.Cells[$"U{row}"].Text.ToString(); //纳税人识别号
                        company.InvoiceAddress = worksheet.Cells[$"V{row}"].Text.ToString(); //发票地址
                        company.InvoiceTel = worksheet.Cells[$"W{row}"].Text.ToString(); //发票电话
                        company.BankName = worksheet.Cells[$"X{row}"].Text.ToString(); //开户银行
                        company.BankAccount = worksheet.Cells[$"Y{row}"].Text.ToString(); //账号
                        company.PaidUpCapital = worksheet.Cells[$"Z{row}"].Text.ToString(); //实收资本
                        company.LegalPerson = worksheet.Cells[$"AA{row}"].Text.ToString(); //法人
                        company.WebSite = worksheet.Cells[$"AB{row}"].Text.ToString(); //网址
                        company.FirstContact = worksheet.Cells[$"AC{row}"].Text.ToString(); //首要联系人
                        company.FirstContactDept = worksheet.Cells[$"AD{row}"].Text.ToString(); //联系人部门
                        company.FirstContactPosition = worksheet.Cells[$"AE{row}"].Text.ToString(); //联系人职位
                        company.FirstContactTel = worksheet.Cells[$"AF{row}"].Text.ToString(); //联系人办公电话
                        company.FirstContactMobile = worksheet.Cells[$"AG{row}"].Text.ToString(); //联系人移动电话
                        company.FirstContactQq = worksheet.Cells[$"AH{row}"].Text.ToString(); //QQ/MSN
                        company.FirstContactEmail = worksheet.Cells[$"AI{row}"].Text.ToString(); //联系人邮箱
                        company.Remark = worksheet.Cells[$"AJ{row}"].Text.ToString(); //备注
                        var usName = worksheet.Cells[$"AK{row}"].Text.ToString();
                        var user = users.Where(a => a.DisplyName == (usName == null ? "" : usName).ToString()).FirstOrDefault();
                        try
                        {
                            company.CreateUserId = user == null ? 1 : user.Id;//建立人
                            var createDate = worksheet.Cells[$"AL{row}"].Text; //建立时间
                            company.CreateDateTime = Convert.ToDateTime(createDate == null ? System.DateTime.Now : createDate);
                        }
                        catch (Exception)
                        {

                            company.CreateDateTime = System.DateTime.Now;
                        }
                        var Fzr = worksheet.Cells[$"Am{row}"].Text; //负责人
                        var us = users.Where(a => a.DisplyName == (Fzr == null ? "" : Fzr).ToString()).FirstOrDefault();
                        company.PrincipalUserId = us == null ? 1 : us.Id;//建立人


                        company.BusinessScope = worksheet.Cells[$"AN{row}"].Text.ToString(); //经营范围

                        var username = worksheet.Cells[$"K{row}"].Text;

                        //默认值
                        company.IsDelete = 0;
                        company.Cstate = 1; //状态0：0:未审核1：审核通过 2：已终止
                        company.FoundDateTime = null;
                        company.ExpirationDateTime = null;
                        company.ModifyDateTime = DateTime.Now;
                        listcomp.Add(company);


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
        /// 导入合同数据
        /// </summary>
        /// <param name="fullfileName">文件名称</param>
        /// <param name="datas">合同对方类别集合</param>
        /// <returns></returns>
        public static IList<ContractInfo> ImportContract(string fullfileName,
            IList<DataDictionary> datas,
            IList<UserInfor> users
            , IList<Department> deptes
            , IList<CompanyInfo> compani
            , IList<CurrencyManager> currencies
           , IList<ProjectManager> Xminfo
           )
        {
            //EPPlus 是商业版本，需要再次指定非商业化，如果有兴趣可以手动手改组件
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileInfo file = new FileInfo(fullfileName);
            try
            {
                IList<ContractInfo> listcont = new List<ContractInfo>();
                using (ExcelPackage package = new ExcelPackage(file))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;
                    int ColCount = worksheet.Dimension.Columns;
                    //bool bHeaderRow = true;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        ContractInfo contract = new ContractInfo();
                        //合同编号
                        contract.Code = worksheet.Cells[$"A{row}"].Text.ToString();
                        if (string.IsNullOrWhiteSpace(contract.Code))
                        {
                            continue;
                        }
                        //合同对方编号
                        contract.OtherCode = worksheet.Cells[$"B{row}"].Text.ToString();
                        //合同名称
                        contract.Name = worksheet.Cells[$"C{row}"].Text.ToString();
                        if (string.IsNullOrWhiteSpace(contract.Name))
                        {
                            continue;
                        }
                        var lb = worksheet.Cells[$"D{row}"].Text.ToString();
                        var lbinfo = datas.Where(a => a.Name == lb).FirstOrDefault();
                        contract.ContTypeId = lbinfo == null ? 0 : lbinfo.Id;//合同类别
                        //收付款
                        var sfk = worksheet.Cells[$"E{row}"].Text.ToString();
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
                        //合同金额
                        var ftje = worksheet.Cells[$"F{row}"].Text.ToString();
                        //默认值
                        //contract.AmountMoney = Convert.ToDecimal(ftje);
                        if (!string.IsNullOrEmpty(ftje))
                        {
                            var iszh1 = decimal.TryParse(ftje, out decimal dtt);
                            if (iszh1)
                            {
                                contract.AmountMoney = dtt;

                            }
                            else
                            {
                                contract.AmountMoney = 0;

                            }

                        }
                        //币种
                        var bz = worksheet.Cells[$"G{row}"].Text.ToString();
                        var bzinfo = currencies.Where(a => a.ShortName == bz).FirstOrDefault();
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
                        //合同状态
                        var Httype = worksheet.Cells[$"H{row}"].Text.ToString();
                        byte state = 0;
                        switch (Httype)
                        {
                            case "未执行":
                                state = 0;
                                break;
                            case "执行中":
                                state = 1;
                                break;
                            case "已终止":
                                state = 2;
                                break;
                            case "已作废":
                                state = 3;
                                break;
                            case "审批中":
                                state = 4;
                                break;
                            case "被打回":
                                state = 5;
                                break;
                            case "已完成":
                                state = 6;
                                break;
                            case "审批通过":
                                state = 8;
                                break;
                          

                        }
                        contract.ContState = state;
                        //0:未执行，1：执行中
                        //2：已作废，3已终止 4：已完成
                        //经办机构 合同所属部门
                        var jbjg = worksheet.Cells[$"I{row}"].Text.ToString();
                        var jbjginfo = deptes.Where(a => a.Name == jbjg).FirstOrDefault();
                        contract.DeptId = jbjginfo == null ? 1 : jbjginfo.Id;
                        //合同对方
                        var htdf = worksheet.Cells[$"J{row}"].Text.ToString();
                        if (string.IsNullOrWhiteSpace(htdf))
                        {
                            continue;
                        }
                        var dfinfo = compani.Where(a => a.Name == htdf).FirstOrDefault();
                        if (dfinfo == null)
                        {
                            continue;
                        }
                        else
                        {
                            contract.CompId = dfinfo.Id;
                        }
                        // 合同所属项目
                        var XmName = worksheet.Cells[$"K{row}"].Text.ToString();
                        var Xm = Xminfo.Where(a => a.Code == XmName).FirstOrDefault();
                        contract.ProjectId = Xm == null ? 0 : Xm.Id;
                        //合同签订日期
                        try
                        {

                            contract.SngnDateTime = Convert.ToDateTime(worksheet.Cells[$"L{row}"].Text);
                        }
                        catch (Exception)
                        {
                            contract.SngnDateTime = null;
                        }
                        //合同生效日期
                        try
                        {

                            contract.EffectiveDateTime = Convert.ToDateTime(worksheet.Cells[$"M{row}"].Text);
                        }
                        catch (Exception)
                        {
                            contract.EffectiveDateTime = null;
                        }
                        //合同计划完成日期
                        try
                        {

                            contract.PlanCompleteDateTime = Convert.ToDateTime(worksheet.Cells[$"N{row}"].Text);
                        }
                        catch (Exception)
                        {
                            contract.PlanCompleteDateTime = null;
                        }
                        //合同实际完成日期
                        try
                        {

                            contract.ActualCompleteDateTime = Convert.ToDateTime(worksheet.Cells[$"O{row}"].Text);
                        }
                        catch (Exception)
                        {
                            contract.ActualCompleteDateTime = null;
                        }
                        //合同 负责人
                        var cjr = worksheet.Cells[$"P{row}"].Text;
                        if (cjr != null)
                        {
                            var jlr = users.Where(a => a.Name == cjr.ToString()).FirstOrDefault();
                            contract.PrincipalUserId = jlr == null ? 1 : jlr.Id;
                        }
                        else
                        {
                            contract.CreateUserId = 1;

                        }
                        //签约主体
                        var qyzt = worksheet.Cells[$"Q{row}"].Text.ToString();
                        var qyztinfo = deptes.Where(a => a.Name == qyzt).FirstOrDefault();
                        contract.MainDeptId = qyztinfo == null ? 1 : qyztinfo.Id;
                        ////总 / 分包
                        //try
                        //{
                        //    var zfb = worksheet.Cells[$"R{row}"].Text;
                        //    if (zfb == null)
                        //    {
                        //        contract.ContDivision = 0;

                        //    }
                        //    else
                        //    {
                        //        if (sfkId == 0)
                        //        {
                        //            contract.ContDivision = 1;
                        //        }
                        //        else
                        //        {
                        //            contract.ContDivision = 2;
                        //        }

                        //    }
                        //}
                        //catch (Exception)
                        //{

                        //    contract.ContDivision = 0;
                        //}
                        //总包合同
                       
                        contract.SumContId =  null;
                        //合同属性
                        var htsx = worksheet.Cells[$"T{row}"].Text;
                        if (htsx == null)
                        {
                            contract.IsFramework = 0;
                        }
                        else
                        {
                            contract.IsFramework = 1;
                        }
                        //创建人
                        var cjrs = worksheet.Cells[$"U{row}"].Text;
                        if (cjrs != null)
                        {
                            var jlrs = users.Where(a => a.Name == cjrs.ToString()).FirstOrDefault();
                            contract.CreateUserId = jlrs == null ? 1 : jlrs.Id;
                        }
                        else
                        {
                            contract.CreateUserId = 1;
                        }
                        //创建时间
                        try
                        {
                            var cjsj = worksheet.Cells[$"V{row}"].Text;
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


        #region 导入项目
        /// <summary>
        /// 导入项目
        /// </summary>
        /// <param name="fullfileName">文件名称</param>
        /// <param name="datas">合同对方类别集合</param>
        /// <returns></returns>
        public static IList<ProjectManager> ImportProj(string fullfileName,
            IList<DataDictionary> datas,
            IList<UserInfor> users
            , IList<CurrencyManager> currencies)
        {
            //EPPlus 是商业版本，需要再次指定非商业化，如果有兴趣可以手动手改组件
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileInfo file = new FileInfo(fullfileName);
            try
            {
                IList<ProjectManager> listcont = new List<ProjectManager>();
                using (ExcelPackage package = new ExcelPackage(file))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;
                    int ColCount = worksheet.Dimension.Columns;
                    //bool bHeaderRow = true;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        ProjectManager proj = new ProjectManager();
                        //项目名称
                        proj.Name = worksheet.Cells[$"A{row}"].Text.ToString();
                        //项目编号
                        proj.Code = worksheet.Cells[$"B{row}"].Text.ToString();
                        var lb = worksheet.Cells[$"C{row}"].Text.ToString();
                        var lbinfo = datas.Where(a => a.Name == lb).FirstOrDefault();
                        proj.CategoryId = lbinfo == null ? 0 : lbinfo.Id;//类别

                        //计划开始日期
                        var jhks = worksheet.Cells[$"D{row}"].Text.ToString();
                        if (!string.IsNullOrEmpty(jhks))
                        {
                            var iszh1=DateTime.TryParse(jhks, out DateTime dtt);
                            if (iszh1)
                            {
                                proj.PlanBeginDateTime = dtt;
                            }

                        }
                        //计划结束
                        var jhjs = worksheet.Cells[$"E{row}"].Text.ToString();
                        if (!string.IsNullOrEmpty(jhjs))
                        {
                            var iszh1 = DateTime.TryParse(jhjs, out DateTime dtt);
                            if (iszh1)
                            {
                                proj.PlanCompleteDateTime = dtt;
                            }

                        }
                        //实际开始
                        var sjks = worksheet.Cells[$"F{row}"].Text.ToString();
                        if (!string.IsNullOrEmpty(sjks))
                        {
                            var iszh1 = DateTime.TryParse(sjks, out DateTime dtt);
                            if (iszh1)
                            {
                                proj.ActualBeginDateTime = dtt;
                            }

                        }
                        //实际接受
                        var sjjs = worksheet.Cells[$"G{row}"].Text.ToString();
                        if (!string.IsNullOrEmpty(sjjs))
                        {
                            var iszh1 = DateTime.TryParse(sjjs, out DateTime dtt);
                            if (iszh1)
                            {
                                proj.ActualCompleteDateTime = dtt;
                            }

                        }
                        //项目预算收款
                        var xmyssr = worksheet.Cells[$"H{row}"].Text.ToString();
                        if (!string.IsNullOrEmpty(sjjs))
                        {
                            var iszh1 = decimal.TryParse(sjjs, out decimal dtt);
                            if (iszh1)
                            {
                                proj.BugetCollectAmountMoney = dtt;
                            }
                            else
                            {
                                proj.BugetCollectAmountMoney = 0;
                            }

                        }
                        //项目预算付款款
                        var xmysfk = worksheet.Cells[$"J{row}"].Text.ToString();
                        if (!string.IsNullOrEmpty(sjjs))
                        {
                            var iszh1 = decimal.TryParse(sjjs, out decimal dtt);
                            if (iszh1)
                            {
                                proj.BudgetPayAmountMoney = dtt;
                            }
                            else
                            {
                                proj.BudgetPayAmountMoney = 0;
                            }

                        }
                        //币种
                        var bz = worksheet.Cells[$"I{row}"].Text.ToString();
                        var bzinfo = currencies.Where(a => a.ShortName == bz).FirstOrDefault();
                        if (bzinfo == null)
                        {
                            proj.BudgetCollectCurrencyId = 1;
                           

                        }
                        else
                        {
                            proj.BudgetCollectCurrencyId = bzinfo.Id;
                           
                        }
                        //付款币种
                        var bz1 = worksheet.Cells[$"K{row}"].Text.ToString();
                        var bzinfo1 = currencies.Where(a => a.ShortName == bz1).FirstOrDefault();
                        if (bzinfo == null)
                        {
                            proj.BudgetPayCurrencyId = 1;


                        }
                        else
                        {
                            proj.BudgetPayCurrencyId = bzinfo1.Id;

                        }
                        //PrincipalUserId 负责人
                        //
                        var fzr = worksheet.Cells[$"L{row}"].Text;
                        if (fzr != null)
                        {
                            var jlr = users.Where(a => a.Name == fzr.ToString()).FirstOrDefault();
                            proj.PrincipalUserId = jlr == null ? 1 : jlr.Id;
                        }
                        else
                        {
                            proj.PrincipalUserId = 1;

                        }
                        proj.Pstate = 1;//默认执行中
                        //创建人
                        var cjr = worksheet.Cells[$"N{row}"].Text;
                        if (cjr != null)
                        {
                            var jlr = users.Where(a => a.Name == cjr.ToString()).FirstOrDefault();
                            proj.CreateUserId = jlr == null ? 1 : jlr.Id;
                            proj.ModifyUserId= jlr == null ? 1 : jlr.Id;
                        }
                        else
                        {
                            proj.CreateUserId = 1;
                            proj.ModifyUserId = 1;

                        }
                        //创建时间
                        try
                        {
                            var cjsj = worksheet.Cells[$"P{row}"].Text;
                            if (cjsj == null)
                            {
                                proj.CreateDateTime = DateTime.Now;
                                proj.ModifyDateTime = DateTime.Now;

                            }
                            else
                            {
                                proj.CreateDateTime = Convert.ToDateTime(cjsj);
                                proj.ModifyDateTime = Convert.ToDateTime(cjsj);
                            }
                        }
                        catch (Exception)
                        {

                            proj.CreateDateTime = DateTime.Now;
                            proj.ModifyDateTime = DateTime.Now;
                        }

                        listcont.Add(proj);


                    }

                }

                return listcont;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 计划资金
        /// <summary>
        /// 计划资金
        /// </summary>
        /// <param name="fullfileName">文件名称</param>
        /// <param name="datas">合同对方类别集合</param>
        /// <returns></returns>
        public static IList<ContPlanFinance> ImportPlanFince(string fullfileName,
            IList<DataDictionary> datas,
            IList<UserInfor> users
            , IList<ContractInfo> conts
            , IList<CurrencyManager> currencies)
        {
            //EPPlus 是商业版本，需要再次指定非商业化，如果有兴趣可以手动手改组件
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileInfo file = new FileInfo(fullfileName);
            try
            {
                IList<ContPlanFinance> listcont = new List<ContPlanFinance>();
                using (ExcelPackage package = new ExcelPackage(file))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;
                    int ColCount = worksheet.Dimension.Columns;
                    //bool bHeaderRow = true;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        ContPlanFinance planFinance = new ContPlanFinance();
                        //合同编号
                        var htno= worksheet.Cells[$"A{row}"].Text.ToString();
                        var htinfo = conts.Where(a => a.Code == htno).FirstOrDefault();
                        if (htinfo != null)
                        {
                            planFinance.ContId = htinfo.Id;
                            planFinance.Ftype = htinfo.FinanceType;//资金性质
                        }
                        else
                        {
                            continue;//跳出当前循环
                        }
                        //资金名称
                        planFinance.Name = worksheet.Cells[$"B{row}"].Text.ToString();
                        //币种
                        var bz = worksheet.Cells[$"E{row}"].Text.ToString();
                        var bzinfo = currencies.Where(a => a.ShortName == bz).FirstOrDefault();
                        if (bzinfo == null)
                        {
                            planFinance.CurrencyRate = 1;
                            planFinance.CurrencyId = 1;

                        }
                        else
                        {
                            planFinance.CurrencyRate = bzinfo.Rate;
                            planFinance.CurrencyId = bzinfo.Id;

                        }
                        //金额
                        var je = worksheet.Cells[$"F{row}"].Text.ToString();
                        if (!string.IsNullOrEmpty(je))
                        {
                            var iszh1 = decimal.TryParse(je, out decimal dtt);
                            if (iszh1)
                            {
                                planFinance.AmountMoney = dtt;
                                planFinance.SurplusAmount = dtt;//可核销
                            }
                            else
                            {
                                planFinance.AmountMoney = 0;
                                planFinance.SurplusAmount = 0;
                            }

                        }
                        //结算方式
                        var lb = worksheet.Cells[$"G{row}"].Text.ToString();
                        var lbinfo = datas.Where(a => a.Name == lb).FirstOrDefault();
                        planFinance.SettlementModes = lbinfo == null ? 0 : lbinfo.Id;//类别
                                                                                     //计划完成时间
                        var jhks = worksheet.Cells[$"H{row}"].Text.ToString();
                        if (!string.IsNullOrEmpty(jhks))
                        {
                            var iszh1 = DateTime.TryParse(jhks, out DateTime dtt);
                            if (iszh1)
                            {
                                planFinance.PlanCompleteDateTime = dtt;
                            }

                        }
                        //备注
                        planFinance.Remark = worksheet.Cells[$"I{row}"].Text.ToString();
                        planFinance.Fstate = 0;//默认未完成 1：已完成
                        //创建人
                        var cjr = worksheet.Cells[$"J{row}"].Value;
                        if (cjr != null)
                        {
                            var jlr = users.Where(a => a.Name == cjr.ToString()).FirstOrDefault();
                            planFinance.CreateUserId = jlr == null ? 1 : jlr.Id;
                            planFinance.ModifyUserId = jlr == null ? 1 : jlr.Id;
                        }
                        else
                        {
                            planFinance.CreateUserId = 1;
                            planFinance.ModifyUserId = 1;

                        }
                        //创建时间
                        try
                        {
                            var cjsj = worksheet.Cells[$"K{row}"].Text;
                            if (cjsj == null)
                            {
                                planFinance.CreateDateTime = DateTime.Now;
                                planFinance.ModifyDateTime = DateTime.Now;

                            }
                            else
                            {
                                planFinance.CreateDateTime = Convert.ToDateTime(cjsj);
                                planFinance.ModifyDateTime = Convert.ToDateTime(cjsj);
                            }
                        }
                        catch (Exception)
                        {

                            planFinance.CreateDateTime = DateTime.Now;
                            planFinance.ModifyDateTime = DateTime.Now;
                        }
                        planFinance.IsDelete = 0;
                        planFinance.SubAmount = 0;//提交金额
                        planFinance.ConfirmedAmount = 0;//已确认金额
                        planFinance.ActSettlementDate = null;//完成时间
                        planFinance.ActAmountMoney = 0;//建立实际资金金额
                        planFinance.CheckAmount = 0;//本次核销金额


                        listcont.Add(planFinance);


                    }

                }

                return listcont;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 发票
        /// <summary>
        /// 发票
        /// </summary>
        /// <param name="fullfileName">文件名称</param>
        /// <param name="datas">合同对方类别集合</param>
        /// <returns></returns>
        public static IList<ContInvoice> ImportInvoice(string fullfileName,
            IList<DataDictionary> datas,
            IList<UserInfor> users
            , IList<ContractInfo> conts
            , IList<CurrencyManager> currencies)
        {
            //EPPlus 是商业版本，需要再次指定非商业化，如果有兴趣可以手动手改组件
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileInfo file = new FileInfo(fullfileName);
            try
            {
                IList<ContInvoice> listcont = new List<ContInvoice>();
                using (ExcelPackage package = new ExcelPackage(file))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;
                    int ColCount = worksheet.Dimension.Columns;
                    //bool bHeaderRow = true;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        ContInvoice invoice = new ContInvoice();
                        //发票类型
                        var lb = worksheet.Cells[$"A{row}"].Text.ToString();
                        var lbinfo = datas.Where(a => a.Name == lb).FirstOrDefault();
                        invoice.InType = lbinfo == null ? 0 : lbinfo.Id;//发票类型

                        var htno = worksheet.Cells[$"B{row}"].Text.ToString();
                        var htinfo = conts.Where(a => a.Code == htno).FirstOrDefault();
                        if (htinfo != null)
                        {
                            invoice.ContId = htinfo.Id;
                            //发票状态
                            if (htinfo.FinanceType==0)
                            {
                                invoice.InState = (int)NF.ViewModel.Models.InvoiceStateEnum.Invoicing;//已开出

                            }
                            else
                            {
                                invoice.InState = (int)NF.ViewModel.Models.InvoiceStateEnum.ReceiptInvoice;//已收到
                            }
                        }
                        else
                        {
                            continue;//跳出当前循环
                        }


                        //发票单位
                        invoice.InTitle = worksheet.Cells[$"C{row}"].Text.ToString();
                        //纳税人识别号
                        invoice.TaxpayerIdentification = worksheet.Cells[$"D{row}"].Text.ToString();
                        //地址
                        invoice.InAddress = worksheet.Cells[$"E{row}"].Text.ToString();
                        //电话
                        invoice.InTel = worksheet.Cells[$"F{row}"].Text.ToString();
                        //银行
                        invoice.BankName = worksheet.Cells[$"G{row}"].Text.ToString();
                        //账号
                        invoice.BankAccount = worksheet.Cells[$"H{row}"].Text.ToString();
                        //金额
                        var je = worksheet.Cells[$"I{row}"].Text.ToString();
                        if (!string.IsNullOrEmpty(je))
                        {
                            var iszh1 = decimal.TryParse(je, out decimal dtt);
                            if (iszh1)
                            {
                                invoice.AmountMoney = dtt;
                               
                            }
                            else
                            {
                                invoice.AmountMoney = 0;
                               
                            }

                        }
                        //开票日期
                        var jhks = worksheet.Cells[$"J{row}"].Text.ToString();
                        if (!string.IsNullOrEmpty(jhks))
                        {
                            var iszh1 = DateTime.TryParse(jhks, out DateTime dtt);
                            if (iszh1)
                            {
                                invoice.MakeOutDateTime = dtt;
                            }
                            else
                            {
                                invoice.MakeOutDateTime = null;
                            }

                        }
                        //发票号
                        invoice.InCode = worksheet.Cells[$"K{row}"].Text.ToString();
                        //备注
                        invoice.Remark= worksheet.Cells[$"L{row}"].Text.ToString();
                        invoice.Reserve1 = worksheet.Cells[$"M{row}"].Text.ToString();
                        invoice.Reserve2 = worksheet.Cells[$"N{row}"].Text.ToString();

                        //创建人
                        var cjr = worksheet.Cells[$"O{row}"].Text;
                        if (cjr != null)
                        {
                            var jlr = users.Where(a => a.Name == cjr.ToString()).FirstOrDefault();
                            invoice.CreateUserId = jlr == null ? 1 : jlr.Id;
                            invoice.ModifyUserId = jlr == null ? 1 : jlr.Id;
                            invoice.ConfirmUserId= jlr == null ? 1 : jlr.Id;
                        }
                        else
                        {
                            invoice.CreateUserId = 1;
                            invoice.ModifyUserId = 1;
                            invoice.ConfirmUserId = 1;

                        }
                        invoice.CreateDateTime = DateTime.Now;
                        invoice.ModifyDateTime = DateTime.Now;
                        try
                        {
                            var qrsj = worksheet.Cells[$"P{row}"].Text;
                            if (qrsj == null)
                            {
                                invoice.CreateDateTime = DateTime.Now;
                                invoice.ModifyDateTime = DateTime.Now;

                            }
                            else
                            {
                                invoice.CreateDateTime = Convert.ToDateTime(qrsj);
                                invoice.ModifyDateTime = Convert.ToDateTime(qrsj);
                            }
                        }
                        catch (Exception)
                        {

                            invoice.CreateDateTime = DateTime.Now;
                            invoice.ModifyDateTime = DateTime.Now;
                        }
                        listcont.Add(invoice);


                    }

                }

                return listcont;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 标的
        /// <summary>
        /// 标的
        /// </summary>
        /// <param name="fullfileName">文件名称</param>
        /// <param name="datas">合同对方类别集合</param>
        /// <returns></returns>
        public static IList<ContSubjectMatter> ImportSubMit(string fullfileName,
            IList<DataDictionary> datas,
            IList<UserInfor> users
            , IList<ContractInfo> conts
            , IList<CurrencyManager> currencies)
        {
            //EPPlus 是商业版本，需要再次指定非商业化，如果有兴趣可以手动手改组件
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileInfo file = new FileInfo(fullfileName);
            try
            {
                IList<ContSubjectMatter> listcont = new List<ContSubjectMatter>();
                using (ExcelPackage package = new ExcelPackage(file))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;
                    int ColCount = worksheet.Dimension.Columns;
                    //bool bHeaderRow = true;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        ContSubjectMatter submit = new ContSubjectMatter();
                        var htno = worksheet.Cells[$"A{row}"].Text.ToString();
                        var htinfo = conts.Where(a => a.Code == htno).FirstOrDefault();
                        if (htinfo != null)
                        {
                            submit.ContId = htinfo.Id;
                            submit.CreateUserId = htinfo.CreateUserId;
                            submit.CreateDateTime = htinfo.CreateDateTime;
                            submit.ModifyUserId = htinfo.ModifyUserId;
                            submit.ModifyDateTime = htinfo.ModifyDateTime;
                        }
                        else
                        {
                            continue;//跳出当前循环
                        }


                        //发票单位
                        submit.Name = worksheet.Cells[$"B{row}"].Text.ToString();
                        //规格
                        submit.Spec = worksheet.Cells[$"C{row}"].Text.ToString();
                        //型号
                        submit.Stype = worksheet.Cells[$"D{row}"].Text.ToString();
                        //单位
                        submit.Unit = worksheet.Cells[$"E{row}"].Text.ToString();
                        //数量
                        var sl = worksheet.Cells[$"F{row}"].Text.ToString();
                        if (!string.IsNullOrEmpty(sl))
                        {
                            var iszh1 = decimal.TryParse(sl, out decimal dtt);
                            if (iszh1)
                            {
                                submit.Amount = dtt;

                            }
                            else
                            {
                                submit.Amount = 0;

                            }

                        }
                        //单价
                        var dj = worksheet.Cells[$"G{row}"].Text.ToString();
                        if (!string.IsNullOrEmpty(dj))
                        {
                            var iszh1 = decimal.TryParse(dj, out decimal dtt);
                            if (iszh1)
                            {
                                submit.Price = dtt;

                            }
                            else
                            {
                                submit.Price = 0;

                            }

                        }
                        //小计
                        submit.SubTotal = (submit.Price) * (submit.Amount ?? 0);
                        //备注
                        submit.Remark = worksheet.Cells[$"G{row}"].Text.ToString();
                        submit.IsFromCategory = 0;
                        submit.BcInstanceId = null;
                        submit.IsDelete = 0;
                        
                        listcont.Add(submit);


                    }

                }

                return listcont;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 实际资金
        /// <summary>
        /// 实际资金导入
        /// </summary>
        /// <param name="fullfileName">文件名称</param>
        /// <param name="datas">合同对方类别集合</param>
        /// <returns></returns>
        public static IList<ContActualFinance> ImportContAcfince(string fullfileName,
            IList<DataDictionary> datas,
            IList<UserInfor> users
            , IList<ContractInfo> conts
            , IList<CurrencyManager> currencies)
        {
            //EPPlus 是商业版本，需要再次指定非商业化，如果有兴趣可以手动手改组件
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileInfo file = new FileInfo(fullfileName);
            try
            {
                IList<ContActualFinance> listcont = new List<ContActualFinance>();
                using (ExcelPackage package = new ExcelPackage(file))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;
                    int ColCount = worksheet.Dimension.Columns;
                    //bool bHeaderRow = true;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        ContActualFinance acfince = new ContActualFinance();
                        var htno = worksheet.Cells[$"A{row}"].Text.ToString();
                        var htinfo = conts.Where(a => a.Code == htno).FirstOrDefault();
                        if (htinfo != null)
                        {
                            acfince.ContId = htinfo.Id;
                            acfince.FinceType = htinfo.FinanceType;
                            acfince.CreateUserId = htinfo.CreateUserId;
                            acfince.CreateDateTime = htinfo.CreateDateTime;
                            acfince.ModifyUserId = htinfo.ModifyUserId;
                            acfince.ModifyDateTime = htinfo.ModifyDateTime;
                        }
                        else
                        {
                            continue;//跳出当前循环
                        }
                        //结算方式
                        var lb = worksheet.Cells[$"G{row}"].Text.ToString();
                        var lbinfo = datas.Where(a => a.Name == lb).FirstOrDefault();
                        acfince.SettlementMethod = lbinfo == null ? 0 : lbinfo.Id;
                        //金额
                        var sl = worksheet.Cells[$"D{row}"].Text.ToString();
                        if (!string.IsNullOrEmpty(sl))
                        {
                            var iszh1 = decimal.TryParse(sl, out decimal dtt);
                            if (iszh1)
                            {
                                acfince.AmountMoney = dtt;

                            }
                            else
                            {
                                acfince.AmountMoney = 0;

                            }

                        }
                        //币种
                        var bz = worksheet.Cells[$"E{row}"].Text.ToString();
                        var bzinfo = currencies.Where(a => a.ShortName == bz).FirstOrDefault();
                        if (bzinfo == null)
                        {
                            acfince.CurrencyRate = 1;
                            acfince.CurrencyId = 1;

                        }
                        else
                        {
                            acfince.CurrencyRate = bzinfo.Rate;
                            acfince.CurrencyId = bzinfo.Id;

                        }
                        //结算时间
                        //开票日期
                        var jhks = worksheet.Cells[$"F{row}"].Text.ToString();
                        if (!string.IsNullOrEmpty(jhks))
                        {
                            var iszh1 = DateTime.TryParse(jhks, out DateTime dtt);
                            if (iszh1)
                            {
                                acfince.ActualSettlementDate = dtt;
                            }
                            else
                            {
                                acfince.ActualSettlementDate = null;
                            }

                        }
                        //凭证号
                        acfince.VoucherNo= worksheet.Cells[$"G{row}"].Text.ToString();
                        //状态
                        acfince.Astate = (byte)NF.ViewModel.Models.Finance.Enums.ActFinanceStateEnum.Confirmed;
                        //确认人
                        var qrr = worksheet.Cells[$"L{row}"].Text;
                        if (qrr != null)
                        {
                            var jlr = users.Where(a => a.Name == qrr.ToString()).FirstOrDefault();
                            
                            acfince.ConfirmUserId = jlr == null ? 1 : jlr.Id;
                        }
                        else
                        {
                            
                            acfince.ConfirmUserId = 1;

                        }

                        //开票日期
                        var qrsj = worksheet.Cells[$"M{row}"].Text.ToString();
                        if (!string.IsNullOrEmpty(qrsj))
                        {
                            var iszh1 = DateTime.TryParse(qrsj, out DateTime dtt);
                            if (iszh1)
                            {
                                acfince.ConfirmDateTime = dtt;
                            }
                            else
                            {
                                acfince.ConfirmDateTime = null;
                            }

                        }
                        //备注1
                        acfince.Reserve1 = worksheet.Cells[$"N{row}"].Text.ToString();
                        //备注2
                        acfince.Reserve1 = worksheet.Cells[$"O{row}"].Text.ToString();
                        acfince.IsDelete = 0;

                        listcont.Add(acfince);


                    }

                }

                return listcont;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 用户
        /// <summary>
        /// 用户
        /// </summary>
        /// <param name="fullfileName">文件名称</param>
        /// <param name="datas">合同对方类别集合</param>
        /// <returns></returns>
        public static IList<UserInfor> ImportUserInfo(string fullfileName,
            IList<Department> departments
           )
        {
            //EPPlus 是商业版本，需要再次指定非商业化，如果有兴趣可以手动手改组件
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileInfo file = new FileInfo(fullfileName);
            try
            {
                IList<UserInfor> listcont = new List<UserInfor>();
                using (ExcelPackage package = new ExcelPackage(file))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;
                    int ColCount = worksheet.Dimension.Columns;
                    //bool bHeaderRow = true;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        UserInfor userinfo = new UserInfor();
                        userinfo.Name= worksheet.Cells[$"A{row}"].Text.ToString();
                        userinfo.DisplyName= worksheet.Cells[$"B{row}"].Text.ToString();
                        var bmname= worksheet.Cells[$"C{row}"].Text.ToString();
                        var bminfo = departments.Where(a => a.Name == bmname).FirstOrDefault();
                       
                        if (bminfo != null)
                        {
                            userinfo.DepartmentId = bminfo.Id;
                        }
                        else
                        {
                            continue;//跳出当前循环
                        }


                        //性别
                        var sexstr = worksheet.Cells[$"D{row}"].Text.ToString();
                        userinfo.Sex = sexstr.Trim() == "女" ? 0 :1;
                        var nl = worksheet.Cells[$"E{row}"].Text.ToString();
                        if (!string.IsNullOrEmpty(nl))
                        {
                            var iszh1 = int.TryParse(nl, out int dtt);
                            if (iszh1)
                            {
                                userinfo.Age = dtt;

                            }
                            else
                            {
                                userinfo.Age = null;

                            }

                        }
                        //电话
                        userinfo.Tel= worksheet.Cells[$"F{row}"].Text.ToString();
                        userinfo.Mobile = worksheet.Cells[$"G{row}"].Text.ToString();
                        userinfo.Email = worksheet.Cells[$"H{row}"].Text.ToString();
                        var rzrq = worksheet.Cells[$"I{row}"].Text.ToString();
                        if (!string.IsNullOrEmpty(rzrq))
                        {
                            var iszh1 = DateTime.TryParse(rzrq, out DateTime dtt);
                            if (iszh1)
                            {
                                userinfo.EntryDatetime = dtt;

                            }
                            else
                            {
                                userinfo.EntryDatetime = null;

                            }

                        }
                        //身份证
                        userinfo.IdNo = worksheet.Cells[$"J{row}"].Text.ToString();
                        //家庭地址
                        userinfo.Address = worksheet.Cells[$"K{row}"].Text.ToString();
                        //单位
                        var sx = worksheet.Cells[$"L{row}"].Text.ToString();
                        if (!string.IsNullOrEmpty(sx))
                        {
                            var iszh1 = int.TryParse(sx, out int dtt);
                            if (iszh1)
                            {
                                userinfo.Sort = dtt;

                            }
                            else
                            {
                                userinfo.Sort = 0;

                            }

                        }
                        

                        //备注
                        userinfo.Remark = worksheet.Cells[$"M{row}"].Text.ToString();
                        userinfo.IsDelete = 0;
                        userinfo.CreateUserId = 1;
                        userinfo.CreateDatetime = DateTime.Now;
                        userinfo.ModifyUserId = 1;
                        userinfo.ModifyDatetime = DateTime.Now;
                        userinfo.Ustart = 0;
                        userinfo.Pwd = "28c8edde3d61a0411511d3b1866f0636";//默认1
                        listcont.Add(userinfo);
                    }

                }

                return listcont;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion


    }
}
