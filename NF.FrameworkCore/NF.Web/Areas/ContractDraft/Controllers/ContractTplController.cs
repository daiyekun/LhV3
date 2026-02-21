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
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.Web.Areas.ContractDraft.Models;
using NF.Web.Areas.NfCommon.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Common;
using NF.Web.Utility.Filters;

namespace NF.Web.Areas.ContractDraft.Controllers
{
    /// <summary>
    /// 合同模板
    /// </summary>
    [Area("ContractDraft")]
    [Route("ContractDraft/[controller]/[action]")]
    public class ContractTplController : Controller
    {
        /// <summary>
        /// 合同模板历史
        /// </summary>
        private IContTxtTemplateHistService _IContTxtTemplateHistService;
        /// <summary>
        /// 模板变量库
        /// </summary>
        private IContTxtTempVarStoreService _IContTxtTempVarStoreService;
        /// <summary>
        /// 合同模板
        /// </summary>
        private IContTxtTemplateService _IContTxtTemplateService;

        private IContTxtTempAndVarStoreRelaService _IContTxtTempAndVarStoreRelaService;
        private readonly ILogger _logger;
        /// <summary>
        /// 签约主体
        /// </summary>
        private IDeptMainService _IDeptMainService;

        public ContractTplController(IContTxtTemplateHistService IContTxtTemplateHistService
            , IContTxtTempVarStoreService IContTxtTempVarStoreService
            , IContTxtTemplateService IContTxtTemplateService
            , IContTxtTempAndVarStoreRelaService IContTxtTempAndVarStoreRelaService
            ,ILogger<ContractTplController> logger
            ,IDeptMainService IDeptMainService)
        {
            _IContTxtTemplateHistService = IContTxtTemplateHistService;
            _IContTxtTempVarStoreService = IContTxtTempVarStoreService;
            _IContTxtTemplateService = IContTxtTemplateService;
            _IContTxtTempAndVarStoreRelaService = IContTxtTempAndVarStoreRelaService;
            _logger = logger;
            _IDeptMainService = IDeptMainService;
        }


        /// <summary>
        /// 获取模板
        /// </summary>
        public IActionResult GetTemplateFile(int uid, int tplid, string locale)
        {
            try
            {

                var tplObj = _IContTxtTemplateHistService.Find(tplid);
                if (tplObj != null)
                {
                    if (!string.IsNullOrEmpty(tplObj.Path))
                    {
                        //var pathf = Path.Combine(Directory.GetCurrentDirectory(),
                        //            "wwwroot", tplObj.Path);
                        var pathf = FileUtility.GetMapPath(tplObj.Path);
                        if (FileUtility.Exists(pathf))
                        {
                            return Content(tplObj.Path.TrimStart('~'));
                        }
                        else
                        {

                            if ("en_us".Equals(locale))
                                return Content("/Uploads/ContractTemplates/NewContractTemplate_en_us.docx");
                            else
                                return Content("/Uploads/ContractTemplates/NewContractTemplate.docx");

                        }
                    }
                    else
                    {
                        if ("en_us".Equals(locale))
                            return Content("/Uploads/ContractTemplates/NewContractTemplate_en_us.docx");
                        else
                            return Content("/Uploads/ContractTemplates/NewContractTemplate.docx");
                    }
                }
                else
                {
                    return Content("NoFile");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Content("ERR");
            }


        }
        /// <summary>
        /// 获取模板相关的自定义变量
        /// </summary>
        /// <param name="uid">当前用户</param>
        /// <param name="tplId">模板历史ID</param>
        /// <returns></returns>
        public IActionResult GetTemplateCustVars(int uid, int? tplId)
        {

            if ((tplId ?? 0) <= 0)
            {
                return Content("ERR");
            }

            var predicateAnd = PredicateBuilder.True<ContTxtTempVarStore>();
            predicateAnd = predicateAnd.And(a => a.IsCustomer == 1 && a.TempHistId == tplId);
            IList<TplContractVariable> cuVars = _IContTxtTempVarStoreService.GetQueryable(predicateAnd)
             .OrderByDescending(p => p.Id)
                .Select(p => new TplContractVariable
                {
                    VarName = p.OriginalId == null ? p.Id.ToString() : p.OriginalId.ToString(),
                    VarLabel = p.Name
                }).ToList();
            return new CustomResultJson(cuVars);
        }
        /// <summary>
        /// 合同模板编辑 获取合同变量
        /// </summary>
        /// <param name="tplid">模板ID</param>        
        public IActionResult GetContractVariables(int tplid)
        {

            var ContTextTemp = _IContTxtTemplateHistService.Find(tplid);

            var TempValues = _IContTxtTempVarStoreService.GetQueryable(a => a.IsCustomer == 0&&a.Isdelete!=1)
                 .OrderBy(a => a.Id).ToList();


            var lst = new List<TplContractVariable>();
            foreach (var item in TempValues)
            {
                if (ContTextTemp != null && ContTextTemp.ShowType == (int)ShowTypeEnum.MingXi)
                {
                    if (item.Id == 38 || item.Name == "标的概要")
                    {
                        continue;
                    }
                }
                var info = new TplContractVariable();
                info.VarName = item.Id.ToString();
                info.VarLabel = item.Name;
                lst.Add(info);
            }
            return new CustomResultJson(lst);



        }
        /// <summary>
        /// 记录 在模板中添加的合同变量
        /// </summary>
        /// <param name="tplid">模板历史ID</param>
        /// <param name="uid">用户ID</param>
        /// <param name="varId">变量ID</param>
        public IActionResult RecordCtVarUsage(int varId, int uid, int tplid)
        {
            bool rest = _IContTxtTempAndVarStoreRelaService.GetQueryable(a => a.TempHistId == tplid && a.VarId == varId).Any();
            if (rest)
            {
                return Content("SUC");
            }
            var tplObj = _IContTxtTemplateHistService.GetQueryable(a => a.Id == tplid).Any();
            if (!tplObj)
            {
                return Content("ERR");

            }
            var relation = new ContTxtTempAndVarStoreRela();
            relation.TempHistId = tplid;
            relation.VarId = varId;
            _IContTxtTempAndVarStoreRelaService.Add(relation);
            return Content("ERR");

        }

        /// <summary>
        /// 保存自定义变量，返回自定变量的书签名称
        /// </summary>
        /// <param name="tplid">模板ID</param>
        /// <param name="varLabel">变量ID</param>
        /// <returns></returns>
        public IActionResult GetCustomContractVarName(string varLabel, int? tplid)
        {
            if ((tplid ?? 0) <= 0)
            {
                return Content("ERR");
            }
            ContTxtTempVarStore variable = new ContTxtTempVarStore
            {
                Name = varLabel,
                TempHistId = tplid,
                IsCustomer = 1,
                Isdelete = 0,
                StoreType = 0
            };
            _IContTxtTempVarStoreService.Add(variable);
            return new CustomResultJson(new { VarName = variable.Id.ToString(), VarLabel = varLabel ?? String.Empty });


        }
        /// <summary>
        /// 自定义变量重命名
        /// </summary>
        /// <param name="label"></param>
        /// <param name="varId">变量ID</param>
        /// <param name="varName">变量名称</param>
        /// <param name="tplid">模板ID</param>
        /// <returns></returns>
        public IActionResult RenameCustomContractVarName(int varId, string varName, int tplid)
        {
            ContTxtTempVarStore variable = _IContTxtTempVarStoreService.Find(varId);
            if (variable == null)
            {

                return Content("ERR");

            }
            variable.Name = varName;
            _IContTxtTempVarStoreService.Update(variable);
            return Content("SUC");


        }

        /// <summary>
        /// 删除自定义变量
        /// </summary>
        /// <returns></returns>
        public IActionResult DelCustomContractVarName(int? varId, int? tplid)
        {

            if ((varId ?? 0) <= 0 || (tplid ?? 0) <= 0)
            {
                return Content("ERR"); ;
            }
            var info = _IContTxtTempVarStoreService.GetQueryable(p => p.Id == varId && p.TempHistId == tplid).FirstOrDefault();
            _IContTxtTempVarStoreService.Delete(info);
            return Content("SUC");

        }

        /// <summary>
        /// 保存合同模板
        /// </summary>
        [HttpPost]
        [DisableFormValueModelBinding]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<IActionResult> SaveTemplate(int uid, int tplid)
        {
            
                var temp = _IContTxtTemplateHistService.GetQueryable(p => p.Id == tplid).FirstOrDefault();
                if (temp != null)
                {
                    var path = Path.Combine(
                                Directory.GetCurrentDirectory(),  "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), 10)
                                );
                    UploadFileInfo uploadFileInfo = new UploadFileInfo();
                    uploadFileInfo.RemGuidName = false;
                    uploadFileInfo.SourceFileName = FileUtility.GetFileName(temp.Path);
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
                    
                     return Content("SUC");
                    
                    
                }
                else
                {
                    return Content("ERR");
                }
            

        }
        /// <summary>
        /// 获取合同模板设置的自定义变量
        /// </summary>
        /// <param name="cttextid">文本ID</param>
        /// <param name="uid">用户ID</param>
        /// <returns></returns>
        public IActionResult GetCustomVariables(int uid,int cttextid)
        {
            try
            {
                if (cttextid <= 0)
                {
                    return Content("ERR");
                }
                IList<ContractVariable> cuVars = _IContTxtTemplateService.GetCustomVariables(cttextid);

                if (cuVars != null && cuVars.Count > 0)
                    return new CustomResultJson(cuVars);
                else
                    return Content("NULL");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Content("NULL");
            }
              

        }

        /// <summary>
        /// 获取模板变量库wpsjs 插件方式测试
        /// </summary>
        /// <returns></returns>
        public IActionResult GetHtTempVarList()
        {
            var TempValues = _IContTxtTempVarStoreService.GetQueryable(a => a.IsCustomer == 0 && a.Isdelete != 1)
               .OrderBy(a => a.Id).ToList();
            return new CustomResultJson(new ResultData()
            {
                msg = "",
                code = 0,
                data = TempValues 

            }); 
        }

       


    }
}