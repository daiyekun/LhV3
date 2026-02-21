using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NF.Common.Utility;
using NF.IBLL;

namespace NF.Web.Areas.ContractDraft.Controllers
{
    /// <summary>
    /// 文本比较
    /// </summary>
    [Area("ContractDraft")]
    [Route("ContractDraft/[controller]/[action]")]
    public class ContractTextCmpController : Controller
    {
        private IContTextService _IContTextService;
        private IContTextHistoryService _IContTextHistoryService;
        private IContTxtTemplateHistService _IContTxtTemplateHistService;
        private readonly ILogger _logger;
        /// <summary>
        /// 合同模板变量
        /// </summary>
        private IContTxtTempVarStoreService _IContTxtTempVarStoreService;

        public ContractTextCmpController(
            ILogger<ContractTextCmpController> logger
            , IContTextService IContTextService
            , IContTextHistoryService IContTextHistoryService
            , IContTxtTemplateHistService IContTxtTemplateHistService
            , IContTxtTempVarStoreService IContTxtTempVarStoreService)
        {
            _IContTextService = IContTextService;
            _IContTextHistoryService = IContTextHistoryService;
            _IContTxtTemplateHistService = IContTxtTemplateHistService;
            _IContTxtTempVarStoreService = IContTxtTempVarStoreService;
            _logger = logger;
        }
        /// <summary>
        /// 比较时获取对应文件路径
        /// </summary>
        /// <returns></returns>
        public IActionResult GetContractTextVersionedDoc(int? cttextid, int? uid)
        {
            try
            {

                if ((cttextid ?? -1) < 0 || (uid ?? -1) < 0)
                {
                    return Content("ERR");
                }

                var contractText = _IContTextHistoryService.Find(cttextid ?? -1);

                if (contractText == null || String.IsNullOrEmpty(contractText.Path))
                {
                    return Content("ERR");
                }


                if (!FileUtility.Exists(contractText.Path, true))
                {
                    return Content("ERR");
                }


                string fileurl = contractText.Path.TrimStart('~');
                return Content(fileurl);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                return Content("ERR");
            }
        }

    }
}
