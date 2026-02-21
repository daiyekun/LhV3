using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using NF.BLL;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.Web.Areas.NfCommon.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Common;
using NF.Web.Utility.Filters;

namespace NF.Web.Areas.ContractDraft.Controllers
{
    /// <summary>
    /// 模板起草
    /// </summary>
    [Area("ContractDraft")]
    [Route("ContractDraft/[controller]/[action]")]
    [EnableCors("AllowSpecificOrigin")]
    public class ContTextDraftController : Controller
    {
        private IContTextService _IContTextService;
        private IContTextHistoryService _IContTextHistoryService;
        private IContTxtTemplateHistService _IContTxtTemplateHistService;
        private readonly ILogger _logger;
        /// <summary>
        /// 标的模板字段
        /// </summary>
        private IContTxtTempAndSubFieldService _IContTxtTempAndSubFieldService;

        /// <summary>
        /// 合同模板变量
        /// </summary>
        private IContTxtTempVarStoreService _IContTxtTempVarStoreService;
        /// <summary>
        /// 合同标的
        /// </summary>
        private IContSubjectMatterService _IContSubjectMatterService;
        public ContTextDraftController(
            ILogger<ContractTplController> logger
            ,IContTextService IContTextService
            , IContTextHistoryService IContTextHistoryService
            , IContTxtTemplateHistService IContTxtTemplateHistService
            , IContTxtTempVarStoreService IContTxtTempVarStoreService
            , IContTxtTempAndSubFieldService IContTxtTempAndSubFieldService
            , IContSubjectMatterService IContSubjectMatterService)
        {
            _IContTextService = IContTextService;
            _IContTextHistoryService = IContTextHistoryService;
            _IContTxtTemplateHistService = IContTxtTemplateHistService;
            _IContTxtTempVarStoreService = IContTxtTempVarStoreService;
            _logger = logger;
            _IContTxtTempAndSubFieldService = IContTxtTempAndSubFieldService;
            _IContSubjectMatterService = IContSubjectMatterService;
        }
        /// <summary>
        /// 获取合同模板，如果有合同文本获取合同文本
        /// </summary>
        /// <param name="cttextid"></param>
        /// <param name="h_cttextid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
       public IActionResult GetContTextTempVsto(int? cttextid,int? h_cttextid,int uid)
        {

            try
            {

                if ((cttextid ?? -1) <= 0 && (h_cttextid ?? -1) <= 0)
                {
                    return Content("ERR");
                }
                var Path = "";
                if ((cttextid ?? -1) <= 0 && (h_cttextid ?? -1) > 0)
                {
                    var hist = _IContTextHistoryService.Find(h_cttextid ?? -1);
                    if (hist != null)
                    {
                        Path = hist.Path;
                    }
                }
                else
                {
                    var ContText = _IContTextService.Find(cttextid ?? -1);
                    if (ContText != null)
                    {
                        Path = ContText.Path;

                        if (!FileUtility.Exists(Path, true))
                        {
                            var tempHist = _IContTxtTemplateHistService.Find(ContText.TemplateId ?? 0);
                            if (tempHist != null)
                            {
                                Path = tempHist.Path;
                            }
                        }
                    }
                }
                if (string.IsNullOrEmpty(Path))
                {

                    return Content("ERR");
                }
                if (Path.EndsWith(".pdf"))
                {
                    Path = Path.Replace(".pdf", ".docx");
                }
                string localPath = FileUtility.TransFilePath(Path);

                Path = Path.TrimStart('~');
                var tpath = Models.DraftHtTxtUtility.WriteEmptyWord(localPath, Path);
                return Content(tpath);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                return Content("ERR");
            }
           
        }
        
        ///// <summary>
        ///// 如果文件长度为0，则写入一个空文件（长度不为0）        
        ///// </summary>
        ///// <param name="FileName"></param>
        //private void WriteEmptyWord(string FileName, string urlfilename)
        //{
        //    var file = new System.IO.FileInfo(FileName);
        //    if (!file.Exists || file.Length == 0)
        //    {
        //        //FileName = HttpContext.Current.Server.MapPath("~/Module/WooContract/ContTextEmpty.docx");
        //        urlfilename = "/Module/WooContract/ContTextEmpty.docx";
        //    }
        //    else
        //    {
        //        urlfilename = urlfilename;
        //    }
        //    Response.Write(urlfilename);
        //    Response.End();
        //}
        /// <summary>
        /// 文本起草系统变量及赋值
        /// </summary>
        /// <returns></returns>
        public IActionResult GetContractVariables(int uid,int? cttextid,string locale,string getAll)
        {

            try
            {
                if ((cttextid ?? -1) < 0)
                {
                    return Content("ERR");
                }
                IList<ContractVariable> varsWithValue = null;
                if (getAll != null && "true".Equals(getAll.ToLower()))
                    varsWithValue = _IContTxtTempVarStoreService.GetAllContractVariables(cttextid ?? 0, locale);
                else
                    varsWithValue = _IContTxtTempVarStoreService.GetContractVariables(cttextid ?? 0, locale);

                if (varsWithValue == null || varsWithValue.Count <= 0)
                {

                    return Content("NULL");
                }
                else
                {

                    //String JsonContent = varsWithValue.ToJSON();
                    //JsonContent = JsonContent.Replace("\\u0027", "'");
                    //Response.ClearContent();
                    //Response.Write(JsonContent);
                    return new CustomResultJson(varsWithValue);
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                return Content("ERR");
            }
        }
        /// <summary>
        /// 合同文本自定义变量
        /// </summary>
        /// <returns></returns>
        public IActionResult GetCustomVariables(int uid,int? cttextid)
        {

            try
            {
                if ((cttextid ?? -1) < 0)
                {
                    return Content("ERR");
                }


                IList<ContractVariable> cuVars = _IContTxtTempVarStoreService.GetCustomVariables(cttextid ?? -1);

                if (cuVars != null && cuVars.Count > 0)
                    return new CustomResultJson(cuVars);
                else
                    return Content("NULL");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                return Content("ERR");
            }
           
        }
        /// <summary>
        /// 保存word
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [DisableFormValueModelBinding]
        //[EnableCors("AllowSpecificOrigin")]
        public async Task<IActionResult> SaveContractTextDoc(int uid,int? cttextid)
        {
            //var UserID = Woo.Utility.PageUtility.GetRequestInt("uid");
            //var ContTextID = Woo.Utility.PageUtility.GetRequestInt("cttextid");
            //var Files = Request.Files;
            try
            {
                ContText info = null;
                if ((cttextid ?? -1) > 0)
                {
                    info = _IContTextService.Find(cttextid ?? -1);
                }
                if (uid <= 0 || info == null)
                {

                    return Content("ERR");
                }
                var SavePath = FileUtility.TransFilePath(info.Path, true);
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


                //Files[0].SaveAs(SavePath);

                //保存原始版本
                //    var lstHist = _IContTextHistoryService.GetQueryable(a=>a.ContTxtId==info.Id).OrderByDescending(a=>a.Id).ToList();
                //    if (lstHist.Count > 0)
                //    {
                //        var hist = lstHist[0];
                //        SavePath = FileUtility.TransFilePath(hist.path);
                //        Files[1].SaveAs(SavePath);
                //    }

                //Woo.BLL.WooContractDraft.ContText.Update(info, AddHistory: true);

                return Content("SUC");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                return Content("ERR");
            }

        }

        /// <summary>
        /// 开始下载word
        /// </summary>
        public IActionResult BeginDownWord(int Id)
        {
            try
            {

                //Dictionary<string, string> dic = new Dictionary<string, string>();
                var ret = new RequstResult()
                {

                    Code = 1,


                };

                var info = _IContTextService.Find(Id);
                if (info == null)
                {
                    return new CustomResultJson(ret);

                }
                info.WordPath = "";
                _IContTextService.UpdateQiCaoSave(info, AddHistory: false);
                //dic.Add("id",info.Id.ToString());
                ret.Code = 0;
                ret.Data = info;
                return new CustomResultJson(ret);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                return Content("ERR");
            }
           
        }

        /// <summary>
        /// 插件保存文件
        /// </summary>
        [HttpPost]
        [DisableFormValueModelBinding]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<IActionResult> SaveWordRaw(int? uid,int? cttextid)
        {
            try
            {

               
                var info = _IContTextService.Find(cttextid ?? 0);
                if (info == null)
                {
                    return Content("ERR");
                }
                var wordPath = info.Path;
                if (wordPath.EndsWith(".pdf"))
                {
                    wordPath = wordPath.Replace(".pdf", ".docx");
                }
                var ran = new Random();
                wordPath = wordPath.Replace(".docx",
                    string.Format("_raw{0}.docx", ran.Next()));

                info.WordPath = wordPath;
                //var SaveWordPath = FileUtility.TransFilePath(wordPath);
                var SavePath = FileUtility.TransFilePath(wordPath, true);
                UploadFileInfo uploadFileInfo = new UploadFileInfo();
                uploadFileInfo.RemGuidName = false;
                uploadFileInfo.SourceFileName = FileUtility.GetFileName(wordPath);
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

                _IContTextService.UpdateQiCaoSave(info, AddHistory: false);
                return Content("SUC");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                return Content("ERR");
            }
           
        }

        /// <summary>
        /// 获取合同标的表数据
        /// </summary>
        /// <param name="bc_id">业务品类ID</param>
        /// <param name="cttextid">合同文本ID</param>
        /// <param name="field_type">显示格式。0：统一格式，1：按业务品类显示</param>
        public IActionResult GetContractObjectsTableData(int uid, int? cttextid, int? bc_id, int? field_type)
        {
            try
            {
                if ((cttextid ?? 0) <= 0 || (bc_id ?? 0) < 0 || field_type < 0)
                {
                    return Content("ERR");
                }


                var datatable = _IContTxtTempAndSubFieldService.GetContractObjectsDataTable(cttextid ?? 0, bc_id ?? 0, field_type ?? 0);

                if (datatable == null)
                    return Content("NULL");

                else
                    return new CustomResultJson(datatable);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                return Content("ERR");
            }

        }
        /// <summary>
        /// 获取合同标的数据表格的标的列表
        /// </summary>
        /// <param name="cttextid">合同文本ID</param>
        /// <param name="uid">用户ID</param>
        public IActionResult GetContractObjectTableIDList(int uid,int? cttextid)
        {
            try
            {
                //用户ID

                if ((cttextid ?? -1) <= 0)
                {
                    return Content("ERR");
                }

                //ContractTextDataSource dao = new ContractTextDataSource();
                var IdentifierList = _IContSubjectMatterService.GetContractObjectIdentifiers(cttextid ?? -1);
                //var IdentifierList2 = dao.GetImportedContractObjectDcumentIndentifier(cttextid);

                var lst = new List<ContractObjectTableIdentifier>();
                if (IdentifierList != null)
                {
                    lst.AddRange(IdentifierList);
                }

                if (lst.Count == 0)
                {

                    return Content("NULL");
                }
                //String jsonResult = new JavaScriptSerializer().Serialize(lst);
                //Response.ClearContent();
                //Response.Write(ReplaceUnicodeEscapeSeq(jsonResult));
                return new CustomResultJson(lst);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Content("ERR");

            }



        }

        /// <summary>
        /// 获取水印模板路径
        /// </summary>
       public IActionResult GetWordWaterMark()
        {
            try
            {
                var path = FileUtility.TransFilePath(@"~/Uploads/WaterMarkTemplate/ContractTextWordWaterMark.dotx");
                if (!FileUtility.Exists(path))
                {
                    return Content("ERR");
                }
                else
                {
                    return Content("/Uploads/WaterMarkTemplate/ContractTextWordWaterMark.dotx");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Content("ERR");
            }
            
        }
        /// <summary>
        /// word保存为pdf
        /// </summary>
        [HttpPost]
        [DisableFormValueModelBinding]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<IActionResult> SaveWordPdf(int? cttextid,int? uid)
        {

            try
            {
                var info = _IContTextService.Find(cttextid ?? 0);
                if (info == null)
                {
                    return Content("ERR");

                }
                var padfpath = info.Path;
                if (padfpath.EndsWith(".docx"))
                {
                    padfpath = padfpath.Replace(".docx", ".pdf");
                }
                var path = Path.Combine(
                                    Directory.GetCurrentDirectory(),  "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), 6)
                                    );
                UploadFileInfo uploadFileInfo = new UploadFileInfo();
                uploadFileInfo.RemGuidName = false;
                uploadFileInfo.SourceFileName = FileUtility.GetFileName(padfpath);
                FormValueProvider formModel = await Request.StreamFiles(path, uploadFileInfo);

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

                if (info.Path.EndsWith(".docx"))
                {
                    info.Path = info.Path.Replace(".docx", ".pdf");
                    info.FileName = info.Path.Replace(".docx", ".pdf");
                }
                _IContTextService.UpdateQiCaoSave(info, AddHistory: false);

                if (RedisHelper.KeyExists($"pdf:{cttextid}:{uid}"))
                {
                    RedisHelper.KeyDelete($"pdf:{cttextid}:{uid}");
                }
                RedisHelper.StringSet($"pdf:{cttextid}:{uid}", info.Path);

                return Content("SUC");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                return Content("ERR");
            }
        }


    }
}