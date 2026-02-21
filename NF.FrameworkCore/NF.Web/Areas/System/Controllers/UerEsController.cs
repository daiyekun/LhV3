using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NF.IBLL;
using NF.Web.Controllers;

namespace NF.Web.Areas.System.Controllers
{
    [Area("System")]
    [Route("System/[controller]/[action]")]
    public class UerEsController : NfBaseController
    {
        public IUserInforService _IUserInforService;
        public IUserRoleService _IUserRoleService;
        public IUserModuleService _IUserModuleService;
        public IUserPermissionService _IUserPermissionService;
        private IMapper _IMapper;
        public UerEsController(IUserInforService IUserInforService, IUserRoleService IUserRoleService, IUserModuleService IUserModuleService, IUserPermissionService IUserPermissionService, IMapper IMapper)
        {

            _IUserInforService = IUserInforService;
            _IUserRoleService = IUserRoleService;
            _IUserModuleService = IUserModuleService;
            _IUserPermissionService = IUserPermissionService;
            _IMapper = IMapper;
        }
        public IActionResult Index()
        {
            ViewData["ids"] = this.SessionCurrUserId;
            ViewData["UserE"] = _IUserInforService.DZQZ(this.SessionCurrUserId);
            return View();
        }
    }
}
