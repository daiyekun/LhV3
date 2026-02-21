using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.Web.Areas.NfCommon.Controllers;
using NF.Web.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Common;
using NF.Web.Utility.Filters;

namespace NF.Web.Areas.Company.Controllers
{
    [Area("Company")]
    [Route("Company/[controller]/[action]")]
    [EnableCors("AllowSpecificOrigin")]
    public class InportDataController : NfBaseController
    {
     //   public int SessionCurrUserId { get; set; }


        private IWinningQueService _IWinningQueService;
        private IWinningInqService _IWinningInqService;
        private IWinningItemService _IWinningItemService;
        private ICompanyService _ICompanyService;
        private IDataDictionaryService _IDataDictionaryService;
        private IUserInforService _IUserInforService;
        private IDepartmentService _IDepartmentService;
        private ICurrencyManagerService _ICurrencyManagerService;
        private IContractInfoService _IContractInfoService;
        public InportDataController(ICompanyService ICompanyService
            , IWinningQueService IWinningQueService
            , IWinningInqService IWinningInqService
            , IWinningItemService IWinningItemService
            , IDataDictionaryService IDataDictionaryService
            , IUserInforService IUserInforService
            , IDepartmentService IDepartmentService
            , ICurrencyManagerService ICurrencyManagerService
            , IContractInfoService IContractInfoService)
        {
            _IWinningQueService = IWinningQueService;
            _IWinningInqService = IWinningInqService;
            _IWinningItemService = IWinningItemService;
            _ICompanyService = ICompanyService;
            _IDataDictionaryService = IDataDictionaryService;
            _IUserInforService = IUserInforService;
            _IDepartmentService = IDepartmentService;
            _ICurrencyManagerService = ICurrencyManagerService;
            _IContractInfoService = IContractInfoService;
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

        //[EnableCors("AllowSpecificOrigin")]
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
            if (dataInfo.SelType == 0)
            {
                dataInfo.SelTypeDic = "招标";
            }
            else if (dataInfo.SelType == 1)
            {
                dataInfo.SelTypeDic = "询价";
            }
            else if (dataInfo.SelType == 2)
            {
                dataInfo.SelTypeDic = "洽谈";
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
       //   public IActionResult InportData(WinningInqDTO WinningInqDTO)
          public IActionResult InportData(string GfName, int InportType)
        {
            try
            {//
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", GfName);
                var users = _IUserInforService.GetQueryable(a => (a.IsDelete ?? 0) == 0).ToList();
                switch (InportType)
                {
                    case 0://招标
                        {
                            var listdics = _IDataDictionaryService.GetQueryable(a =>
                              a.DtypeNumber == (int)DataDictionaryEnum.suppliersType
                              || a.DtypeNumber == (int)DataDictionaryEnum.customerType
                              || a.DtypeNumber == (int)DataDictionaryEnum.otherType).ToList();
                            var list = ImportDataHelper.ImportItems(path, listdics, users);
                            IList<WinningItem> addlist = new List<WinningItem>();
                            WinningItemsDTO idsd = new WinningItemsDTO();
                            foreach (var item in list)
                            {
                                item.SourceFileName = GfName;
                                item.SessionCurrUserId = this.SessionCurrUserId;
                                item.ModifyUserId = this.SessionCurrUserId;
                                item.TenderId = (idsd.TenderId ) <= 0 ? -SessionCurrUserId : idsd.TenderId;
                                addlist.Add(item);

                            }
                            _IWinningItemService.Add(addlist);
                        }
                        break;
                    case 1://询价
                        {
                            var listdics = _IDataDictionaryService.GetQueryable(a =>
                              a.DtypeNumber == (int)DataDictionaryEnum.suppliersType
                              || a.DtypeNumber == (int)DataDictionaryEnum.customerType
                              || a.DtypeNumber == (int)DataDictionaryEnum.otherType).ToList();
                            var list = ImportDataHelper.ImportCompany(path, listdics, users);
                            IList<WinningInq> addlist = new List<WinningInq>();
                            WinningInqDTO idsd = new WinningInqDTO();
                            foreach (var item in list)
                            {
                                item.SourceFileName = GfName;
                                item.SessionCurrUserId = this.SessionCurrUserId;
                                item.ModifyUserId = this.SessionCurrUserId;
                                item.Inqid = (idsd.Inqid ?? 0) <= 0 ? -SessionCurrUserId : idsd.Inqid;
                                addlist.Add(item);

                            }
                            _IWinningInqService.Add(addlist);
                        }
                        break;
                    case 2://约谈
                        {
                            var listdics = _IDataDictionaryService.GetQueryable(a =>
                              a.DtypeNumber == (int)DataDictionaryEnum.suppliersType
                              || a.DtypeNumber == (int)DataDictionaryEnum.customerType
                              || a.DtypeNumber == (int)DataDictionaryEnum.otherType).ToList();
                            var list = ImportDataHelper.ImportQue(path, listdics, users);
                            IList<WinningQue> addlist = new List<WinningQue>();
                       WinningQueDTO idsd = new WinningQueDTO();
                            foreach (var item in list)
                            {
                                item.SourceFileName = GfName;
                                item.SessionCurrUserId = this.SessionCurrUserId;
                                item.ModifyUserId = this.SessionCurrUserId;
                                item.QueId = idsd.QueId <= 0 ? -SessionCurrUserId : idsd.QueId;
                                addlist.Add(item);

                            }
                            _IWinningQueService.Add(addlist);
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
    public class MyViewModel
    {
        public string Username { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; }
    }
}