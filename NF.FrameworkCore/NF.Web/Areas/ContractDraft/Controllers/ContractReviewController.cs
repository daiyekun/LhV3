using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.Web.Areas.NfCommon.Controllers;
using NF.Web.Utility.Common;
using NF.Web.Utility.Filters;

namespace NF.Web.Areas.ContractDraft.Controllers
{
    [Area("ContractDraft")]
    [Route("ContractDraft/[controller]/[action]")]
    public class ContractReviewController : Controller
    {

        #region 构造函数注入
        private IContTextService _IContTextService;
        private IContTextHistoryService _IContTextHistoryService;
        private IContTxtTemplateHistService _IContTxtTemplateHistService;
        private readonly ILogger _logger;
        

        public ContractReviewController(
            ILogger<ContractTplController> logger
            , IContTextService IContTextService
            , IContTextHistoryService IContTextHistoryService
            , IContTxtTemplateHistService IContTxtTemplateHistService
            )
        {
            _IContTextService = IContTextService;
            _IContTextHistoryService = IContTextHistoryService;
            _IContTxtTemplateHistService = IContTxtTemplateHistService;
            
            _logger = logger;
        }
        #endregion
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [DisableFormValueModelBinding]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<IActionResult>  SaveReviewedContractText(int uid,int cttextid)
        {
            try
            {

                ContText info = null;
                if (cttextid > 0)
                {
                    info = _IContTextService.Find(cttextid);
                }
                if (uid <= 0 || info == null)
                {


                    return Content("ERR");

                }
                _IContTextService.UpdateQiCaoSave(info, AddHistory: true);

                //保存原始版本
                var lstHist = _IContTextHistoryService.GetQueryable(a => a.ContTxtId == info.Id).Select(a => a).ToList();
                if (lstHist.Count == 1)
                {
                    //只有一条历史纪录，不能覆盖原始路径,用新路径替代
                    var SavePath = info.Path;
                    var Ext = Path.GetExtension(SavePath);
                    SavePath = SavePath.Replace(Ext, "_1" + Ext);
                    info.Path = SavePath;
                    SavePath = FileUtility.TransFilePath(SavePath, true);

                    //Files[0].SaveAs(SavePath);
                    await SaveFile(info, SavePath);
                    _IContTextService.UpdateQiCaoSave(info, AddHistory: false);
                }
                else
                {
                   
                    var SavePath = FileUtility.TransFilePath(info.Path, true);
                    if (SavePath.IndexOf(".docx") > 0)
                    {
                        SavePath= Path.GetDirectoryName(SavePath);
                    }
                    await SaveFile(info, SavePath);
                }




                return Content("SUC");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Content("ERR");
            }
           
        }
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="info">合同文本对象</param>
        /// <param name="SavePath">保存路径</param>
        /// <returns></returns>
        private async Task SaveFile(ContText info, string SavePath)
        {
            UploadFileInfo uploadFileInfo = new UploadFileInfo();
            uploadFileInfo.RemGuidName = false;
            uploadFileInfo.SourceFileName = FileUtility.GetFileName(info.Path);
            FormValueProvider formModel = await Request.StreamFiles(SavePath, uploadFileInfo);

            var viewModel = new MyViewModel();

            var bindingSuccessful = await TryUpdateModelAsync(viewModel, prefix: "",
                valueProvider: formModel);

            if (!bindingSuccessful)
            {
                if (!ModelState.IsValid)
                {
                    BadRequest(ModelState);
                }

            }
        }
        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <returns></returns>
        public IActionResult GetContractText(int? uid,int? cttextid)
        {
            //String uid = Request.Form["uid"];
            //String contractTextId = Request.Form["cttextid"];
            //Int32 intContractTextId = -1;
            try
            {
                if ((uid ?? -1) < 0 || (cttextid ?? -1) < 0)
                {
                    return Content("ERR");
                }
                ContText contractTextObj = _IContTextService.Find(cttextid ?? -1);
                if (contractTextObj == null || String.IsNullOrEmpty(contractTextObj.Path))
                {
                    return Content("ERR");
                }

                String contractTextDocPath = FileUtility.TransFilePath(contractTextObj.Path);
                if (String.IsNullOrEmpty(contractTextDocPath) == false && FileUtility.Exists(contractTextDocPath))
                {
                    if (contractTextDocPath.EndsWith(".docx"))
                    {
                        return Content(contractTextObj.Path.TrimStart('~'));
                    }else if (contractTextDocPath.EndsWith(".doc"))
                    {
                        return Content($"/{contractTextObj.Path.TrimStart('/')}");
                    }
                }
                else
                {
                    return Content("ERR");
                }

                return Content("ERR");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Content("ERR");
            }

        }
    }
}