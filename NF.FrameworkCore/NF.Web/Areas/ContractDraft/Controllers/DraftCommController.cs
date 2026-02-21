using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NF.IBLL;
using NF.Web.Utility;

namespace NF.Web.Areas.ContractDraft.Controllers
{
    /// <summary>
    /// 模板起草公共部分
    /// </summary>
    [Area("ContractDraft")]
    [Route("ContractDraft/[controller]/[action]")]
    public class DraftCommController : Controller
    {
        private IUserInforService _IUserInforService;
        public DraftCommController(IUserInforService IUserInforService)
        {
            _IUserInforService = IUserInforService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public IActionResult SignInFromWord(int uid)
        {
            return new CustomResultJson(_IUserInforService.SignInFromWord(uid));
        }
    }
}