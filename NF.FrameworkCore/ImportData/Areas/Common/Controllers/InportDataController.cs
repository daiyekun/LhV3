using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImportData.Models;
using ImportData.Utility.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models.Common;
using NF.Web.Utility;
using NF.Web.Utility.Common;
using NF.Web.Utility.Filters;

namespace ImportData.Areas.Common.Controllers
{
    [Area("Common")]
    [Route("Common/[controller]/[action]")]
    [EnableCors("AllowSpecificOrigin")]
    public class InportDataController : Controller
    {
        private ICompanyService _ICompanyService;
        private IDataDictionaryService _IDataDictionaryService;
        private IUserInforService _IUserInforService;

        
        private IDepartmentService _IDepartmentService;
        private ICurrencyManagerService _ICurrencyManagerService;
        private IContractInfoService _IContractInfoService;
        private IProjectManagerService _IProjectManagerService;
        private IContPlanFinanceService _IContPlanFinanceService;
        private IContInvoiceService _IContInvoiceService;
        private IContSubjectMatterService _IContSubjectMatterService;
        private IContActualFinanceService _IContActualFinanceService;
        
        public InportDataController(ICompanyService ICompanyService
            , IDataDictionaryService IDataDictionaryService
            , IUserInforService IUserInforService
            , IDepartmentService IDepartmentService
            , ICurrencyManagerService ICurrencyManagerService
            , IContractInfoService IContractInfoService
            , IProjectManagerService IProjectManagerService
            , IContPlanFinanceService IContPlanFinanceService
            , IContInvoiceService IContInvoiceService
            , IContSubjectMatterService IContSubjectMatterService
            , IContActualFinanceService IContActualFinanceService
            )
        {
            _ICompanyService = ICompanyService;
            _IDataDictionaryService = IDataDictionaryService;
            _IUserInforService = IUserInforService;
            _IDepartmentService = IDepartmentService;
            _ICurrencyManagerService = ICurrencyManagerService;
            _IContractInfoService = IContractInfoService;
            _IProjectManagerService = IProjectManagerService;
            _IContPlanFinanceService = IContPlanFinanceService;
            _IContInvoiceService = IContInvoiceService;
            _IContSubjectMatterService = IContSubjectMatterService;
            _IContActualFinanceService = IContActualFinanceService;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [DisableFormValueModelBinding]

        //[EnableCors("AllowSpecificOrigin1")]
        public async Task<IActionResult> UploadAsync(DownLoadAndUploadRequestInfo downLoadAndUploadRequestInfo)
        {
            var path = Path.Combine(
                         Directory.GetCurrentDirectory(), "wwwroot", "Uploads"
                         );
            FormValueProvider formModel;
            UploadFileInfo uploadFileInfo = new UploadFileInfo();
            uploadFileInfo.RemGuidName = true;
            //uploadFileInfo.SourceFileName =$"{System.DateTime.Now.Ticks.ToString()}.xlsx" ;
            formModel = await Request.StreamFiles(path, uploadFileInfo);
            var viewModel = new MyViewModel();
            var bindingSuccessful = await TryUpdateModelAsync(viewModel, prefix: "",
                valueProvider: formModel);
            if (!bindingSuccessful)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
            }
            return new CustomResultJson(new RequstResult()
            {
                Msg = "上传成功",
                Code = 0,
                Data = uploadFileInfo


            });
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="dataInfo">保存对象</param>
        /// <returns></returns>
        public IActionResult SaveFile(InportDataInfo dataInfo)
        {
            if (dataInfo.SelType == 1)
            {
                dataInfo.SelTypeDic = "合同对方";
            }
            else if (dataInfo.SelType == 2)
            {
                dataInfo.SelTypeDic = "合同";
            }
            else if (dataInfo.SelType == 3)
            {
                dataInfo.SelTypeDic = "项目";
            }
            else
            {
                dataInfo.SelTypeDic = dataInfo.SelType.ToString();
            }
            IList<InportDataInfo> list = null;
            if (RedisHelper.KeyExists("NF-InportData"))
            {
                list = RedisHelper.StringGetToList<InportDataInfo>("NF-InportData");
                list.Add(dataInfo);
                RedisHelper.KeyDelete("NF-InportData");

            }
            else
            {
                list = new List<InportDataInfo>();
                list.Add(dataInfo);

            }
            RedisHelper.ListObjToJsonStringSetAsync("NF-InportData", list);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "操作成功",
                Code = 0
            });


        }
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetFiles(PageparamInfo pageParam)
        {
            LayPageInfo<InportDataInfo> layPage = null;
            if (RedisHelper.KeyExists("NF-InportData"))
            {
                var list = RedisHelper.StringGetToList<InportDataInfo>("NF-InportData");
                layPage = new LayPageInfo<InportDataInfo>()
                {
                    data = list,
                    count = list.Count,
                    code = 0


                };

            }
            else
            {
                layPage = new LayPageInfo<InportDataInfo>()
                {
                    data = new List<InportDataInfo>(),
                    count = 0,
                    code = 0


                };

            }
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="GfName"></param>
        /// <returns></returns>
        public IActionResult DelFile(string GfName)
        {
            if (RedisHelper.KeyExists("NF-InportData"))
            {
                var list = RedisHelper.StringGetToList<InportDataInfo>("NF-InportData");
                foreach (var item in list)
                {
                    if (item.GfName == GfName)
                    {
                        list.Remove(item);
                        break;
                    }

                }
                RedisHelper.ListObjToJsonStringSetAsync("NF-InportData", list);
                return new CustomResultJson(new RequstResult()
                {
                    Msg = "操作成功",
                    Code = 0
                });
            }
            else
            {
                return new CustomResultJson(new RequstResult()
                {
                    Msg = "没有数据",
                    Code = 0
                });

            }
        }

        public IActionResult ShowView()
        {
            return View();
        }
        /// <summary>
        /// 导入文件名称
        /// </summary>
        /// <param name="GfName">guid文件名称</param>
        /// <param name="InportType">1:合同对方，2：合同</param>
        /// <returns></returns>
        public IActionResult InportData(string GfName, int InportType)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", GfName);
                var users = _IUserInforService.GetQueryable(a => (a.IsDelete ?? 0) == 0).ToList();
              
                switch (InportType)
                {
                    case 1://合同对方
                        {
                            var gj = _IUserInforService.Gj();//国家
                            var Sf = _IUserInforService.Sf();//省
                            var sj = _IUserInforService.sj();//县市
                            var listdics = _IDataDictionaryService.GetQueryable(a =>
                              a.DtypeNumber == (int)DataDictionaryEnum.suppliersType
                              || a.DtypeNumber == (int)DataDictionaryEnum.customerType
                              || a.DtypeNumber == (int)DataDictionaryEnum.otherType).ToList();
                            var list = ImportDataHelper.ImportCompany(path, listdics, users, gj, Sf, sj);
                            IList<Company> addlist = new List<Company>();
                            foreach (var item in list)
                            {
                                //var exist= _ICompanyService.GetQueryable(a => a.Name == item.Name).Any();
                                // if (!exist)
                                // {
                                addlist.Add(item);
                                // }

                            }
                            _ICompanyService.Add(addlist);
                        }
                        break;
                    case 2://合同{
                        {
                            var Xminfo = _IProjectManagerService.GetQueryable(a => a.IsDelete != 1).ToList();
                            var listdics = _IDataDictionaryService.GetQueryable(a =>
                                 a.DtypeNumber == (int)DataDictionaryEnum.contractType).ToList();
                            var depts = _IDepartmentService.GetQueryable(a => a.IsDelete != 1).ToList();
                            var listcmp = _ICompanyService.GetQueryable(a => a.IsDelete != 1).Select(a => new CompanyInfo { Id = a.Id, Name = a.Name }).ToList();
                            var listbz = _ICurrencyManagerService.GetQueryable(a => a.IsDelete != 1).ToList();
                            var list = ImportDataHelper.ImportContract(path, listdics, users, depts, listcmp, listbz, Xminfo);
                            IList<ContractInfo> addlist = new List<ContractInfo>();
                            foreach (var item in list)
                            {
                                //var exist = _IContractInfoService.GetQueryable(a => a.Name == item.Name).Any();
                                //if (!exist)
                                //{
                                addlist.Add(item);
                                // }

                            }
                            _IContractInfoService.Add(addlist);

                        }
                        break;
                    case 3://项目
                        {
                            var listdics = _IDataDictionaryService.GetQueryable(a =>
                             a.DtypeNumber == (int)DataDictionaryEnum.projectType).ToList();
                            //var depts = _IDepartmentService.GetQueryable(a => a.IsDelete != 1).ToList();
                            var listbz = _ICurrencyManagerService.GetQueryable(a => a.IsDelete != 1).ToList();
                            var list = ImportDataHelper.ImportProj(path, listdics, users, listbz);
                            IList<ProjectManager> addlist = new List<ProjectManager>();
                            foreach (var item in list)
                            {

                                addlist.Add(item);


                            }
                            _IProjectManagerService.Add(addlist);

                        }
                        break;
                    case 4://计划资金
                        {
                            var listdics = _IDataDictionaryService.GetQueryable(a =>
                             a.DtypeNumber == (int)DataDictionaryEnum.SettlModes).ToList();
                            //var depts = _IDepartmentService.GetQueryable(a => a.IsDelete != 1).ToList();
                            var listbz = _ICurrencyManagerService.GetQueryable(a => a.IsDelete != 1).ToList();
                            var listhts = _IContractInfoService.GetQueryable(a => a.IsDelete != 1).ToList();
                            var list = ImportDataHelper.ImportPlanFince(path, listdics, users, listhts, listbz);
                            IList<ContPlanFinance> addlist = new List<ContPlanFinance>();
                            foreach (var item in list)
                            {

                                addlist.Add(item);


                            }
                            _IContPlanFinanceService.Add(addlist);


                        }
                        break;
                    case 5://实际资金导入
                        {
                            var listdics = _IDataDictionaryService.GetQueryable(a =>
                                                        a.DtypeNumber == (int)DataDictionaryEnum.InvoiceType).ToList();
                            //var depts = _IDepartmentService.GetQueryable(a => a.IsDelete != 1).ToList();
                            var listbz = _ICurrencyManagerService.GetQueryable(a => a.IsDelete != 1).ToList();
                            var listhts = _IContractInfoService.GetQueryable(a => a.IsDelete != 1).ToList();
                            var list = ImportDataHelper.ImportContAcfince(path, listdics, users, listhts, listbz);
                            IList<ContActualFinance> addlist = new List<ContActualFinance>();
                            foreach (var item in list)
                            {

                                addlist.Add(item);


                            }
                            _IContActualFinanceService.Add(addlist);
                        }
                        break;
                    case 6://发票
                        {
                            var listdics = _IDataDictionaryService.GetQueryable(a =>
                            a.DtypeNumber == (int)DataDictionaryEnum.InvoiceType).ToList();
                            //var depts = _IDepartmentService.GetQueryable(a => a.IsDelete != 1).ToList();
                            var listbz = _ICurrencyManagerService.GetQueryable(a => a.IsDelete != 1).ToList();
                            var listhts = _IContractInfoService.GetQueryable(a => a.IsDelete != 1).ToList();
                            var list = ImportDataHelper.ImportInvoice(path, listdics, users, listhts, listbz);
                            IList<ContInvoice> addlist = new List<ContInvoice>();
                            foreach (var item in list)
                            {

                                addlist.Add(item);


                            }
                            _IContInvoiceService.Add(addlist);
                        }
                        break;
                    case 7://标的
                        { 
                        var listdics = _IDataDictionaryService.GetQueryable(a =>
                           a.DtypeNumber == (int)DataDictionaryEnum.InvoiceType).ToList();
                        //var depts = _IDepartmentService.GetQueryable(a => a.IsDelete != 1).ToList();
                        var listbz = _ICurrencyManagerService.GetQueryable(a => a.IsDelete != 1).ToList();
                        var listhts = _IContractInfoService.GetQueryable(a => a.IsDelete != 1).ToList();
                        var list = ImportDataHelper.ImportSubMit(path, listdics, users, listhts, listbz);
                        IList<ContSubjectMatter> addlist = new List<ContSubjectMatter>();
                        foreach (var item in list)
                        {

                            addlist.Add(item);


                        }
                            _IContSubjectMatterService.Add(addlist);
                        }
                        break;
                    case 8://用户
                        {
                            var depts = _IDepartmentService.GetQueryable(a => a.IsDelete != 1).ToList();
                            var list = ImportDataHelper.ImportUserInfo(path, depts);
                            IList<UserInfor> addlist = new List<UserInfor>();
                            foreach (var item in list)
                            {

                                addlist.Add(item);


                            }
                            _IUserInforService.Add(addlist);
                        }
                
                        break;



                }
                return new CustomResultJson(new RequstResult()
                {
                    Msg = "success",
                    Code = 0
                });
            }
            catch (Exception ex)
            {

                return new CustomResultJson(new RequstResult()
                {
                    Msg = "系统错误",
                    Code = 0
                });
            }
        }


    }
    /// <summary>
    /// 目前没有意义
    /// </summary>
    public class MyViewModel
    {
        public string Username { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; }
    }

}
