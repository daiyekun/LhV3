using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.Web.Controllers;
using NF.Web.Utility;

namespace NF.Web.Areas.NfCommon.Controllers
{
    /// <summary>
    /// 权限
    /// </summary>
    [Area("NfCommon")]
    [Route("NfCommon/[controller]/[action]")]
    public class NfPermissionController : NfBaseController
    {
        private ISysPermissionModelService _ISysPermissionModelService;
        private IUserRoleService _IUserRoleService;
        private IRolePermissionService _IRolePermissionService;
        private IUserPermissionService _IUserPermissionService;
        public NfPermissionController(ISysPermissionModelService ISysPermissionModelService, IUserRoleService IUserRoleService
            , IRolePermissionService IRolePermissionService, IUserPermissionService IUserPermissionService)
        {
            _ISysPermissionModelService = ISysPermissionModelService;
            _IUserRoleService = IUserRoleService;
            _IRolePermissionService = IRolePermissionService;
            _IUserPermissionService = IUserPermissionService;

        }
        /// <summary>
        /// 新增权限/盖章归档
        /// </summary>
        /// <param name="requestPermissionInfo"></param>
        /// <returns></returns>
        public IActionResult AddPermission(RequestPermissionInfo requestPermissionInfo)
        {
            bool ispermiss = false;
            var res = new RequstResult() { Code = 0, };
            switch (requestPermissionInfo.FuncCode)
            {
                case "addcustomer"://新增客户
                case "addsupplier"://新建供应商
                case "addother"://其他对方
                    {
                        ispermiss = _ISysPermissionModelService.GetCmpAddPermission(requestPermissionInfo.FuncCode, this.SessionCurrUserId);
                        if (!ispermiss)
                        {
                            res.RetValue = 1;
                            res.Msg = "无权限";
                        }

                    }
                    break;
                case "addcollcont"://新建收款合同
                case "addpaycont"://新建付款合同
                
                    {
                        ispermiss = _ISysPermissionModelService.GetContractAddPermission(requestPermissionInfo.FuncCode, this.SessionCurrUserId);
                        if (!ispermiss)
                        {
                            res.RetValue = 1;
                            res.Msg = "无权限";
                        }

                    }
                    break;
                case "addproject"://新建项目
                    {
                        ispermiss = _ISysPermissionModelService.GetProjectAddPermission(requestPermissionInfo.FuncCode, this.SessionCurrUserId);
                        if (!ispermiss)
                        {
                            res.RetValue = 1;
                            res.Msg = "无权限";
                        }
                    }
                    break;
                case "collconttextseal"://盖章权限
                case "payconttextseal"://盖章权限
                    {
                        ispermiss = _ISysPermissionModelService.ContTextSealPermission(requestPermissionInfo.FuncCode, this.SessionCurrUserId);
                        if (!ispermiss)
                        {
                            res.RetValue = 1;
                            res.Msg = "无盖章权限";
                        }
                    }
                    break;
                case "collconttextarchive"://归档权限
                case "payconttextarchive"://归档权限
                    {
                        ispermiss = _ISysPermissionModelService.ContTextArchivePermission(requestPermissionInfo.FuncCode, this.SessionCurrUserId);
                        if (!ispermiss)
                        {
                            res.RetValue = 1;
                            res.Msg = "无归档权限";
                        }
                    }
                    break;
                case "addBcInstance"://单品
                    {
                        ispermiss = _ISysPermissionModelService.GetBcInstanceAddOrUpdatePermission(requestPermissionInfo.FuncCode, this.SessionCurrUserId);
                        if (!ispermiss)
                        {
                            res.RetValue = 1;
                            res.Msg = "无操作权限";
                        }
                    }
                    break;
                case "addzbcollcont"://新建招标
                    {
                        ispermiss = _ISysPermissionModelService.GetaddTenderAddPermission(requestPermissionInfo.FuncCode, this.SessionCurrUserId);
                        if (!ispermiss)
                        {
                            res.RetValue = 1;
                            res.Msg = "无权限";
                        }
                    }
                    break;
                case "addInquiry"://新建询价
                    {
                        ispermiss = _ISysPermissionModelService.GetInquiryAddPermission(requestPermissionInfo.FuncCode, this.SessionCurrUserId);
                        if (!ispermiss)
                        {
                            res.RetValue = 1;
                            res.Msg = "无权限";
                        }
                    }
                    break;
                case "addQuestioning"://新建约谈
                    {
                        ispermiss = _ISysPermissionModelService.GetQuestioningAddPermission(requestPermissionInfo.FuncCode, this.SessionCurrUserId);
                        if (!ispermiss)
                        {
                            res.RetValue = 1;
                            res.Msg = "无权限";
                        }
                    }
                    break;
                case "AddJfsb"://新建纠纷上报
                    ispermiss = _ISysPermissionModelService.GetJfsbAddPermission(requestPermissionInfo.FuncCode, this.SessionCurrUserId);
                    if (!ispermiss)
                    {
                        res.RetValue = 1;
                        res.Msg = "无权限";
                    }
                    break;
                case "AddAjg":
                    ispermiss = _ISysPermissionModelService.GetAjglAddPermission(requestPermissionInfo.FuncCode, this.SessionCurrUserId);
                    if (!ispermiss)
                    {
                        res.RetValue = 1;
                        res.Msg = "无权限";
                    }
                    break;
                default:
                    {
                        res.RetValue = 1;
                        res.Msg = "无权限(未知操作)";
                    }
                    break;

            }
            return new CustomResultJson(res);
        }
        /// <summary>
        /// 查看详情权限
        /// </summary>
        /// <param name="requestPermissionInfo"></param>
        /// <returns></returns>
        public IActionResult ViewPermission(RequestPermissionInfo requestPermissionInfo)
        {
            var res = new RequstResult() { Code = 0, };
            bool ispermiss = false;
            switch (requestPermissionInfo.FuncCode)
            {
                case "querycustomerview"://查看客户详情
                case "querysupplierview"://供应商
                case "queryotherview"://其他对方
                    {
                        ispermiss = _ISysPermissionModelService.GetCmpDetailPermission(requestPermissionInfo.FuncCode, this.SessionCurrUserId, this.SessionCurrUserDeptId, requestPermissionInfo.ObjId);
                        if (!ispermiss)
                        {
                            res.RetValue = 1;
                            res.Msg = "无权限";
                        }

                    }
                    break;
                case "queryInquirylist"://询价查看
                case "querypaycontview"://付款合同查看
                case "querycollcontview"://收款合同查看
                    {
                        ispermiss = _ISysPermissionModelService.GetContractDetailPermission(requestPermissionInfo.FuncCode, this.SessionCurrUserId, this.SessionCurrUserDeptId, requestPermissionInfo.ObjId);
                        if (!ispermiss)
                        {
                            res.RetValue = 1;
                            res.Msg = "无权限";
                        }

                    }
                    break;
                case "queryprojectview":
                    {
                        ispermiss = _ISysPermissionModelService.GetProjectDetailPermission(requestPermissionInfo.FuncCode, this.SessionCurrUserId, this.SessionCurrUserDeptId, requestPermissionInfo.ObjId);
                        if (!ispermiss)
                        {
                            res.RetValue = 1;
                            res.Msg = "无权限";
                        }

                    }
                    break;
                case "addOrUpdateInvoice"://建立和修改发票权限
                case "addOrUpdateInvoiceOut"://建立开票
                    ispermiss = _ISysPermissionModelService.GetAddUpdateInvoicePermission(requestPermissionInfo.FuncCode, this.SessionCurrUserId,this.SessionCurrUserDeptId, requestPermissionInfo.ObjHtId);
                    if (!ispermiss)
                    {
                        res.RetValue = 1;
                        res.Msg = "无权限";
                    }
                    break;
                case "addOrUpdateActFinanceColl"://建立实际收款
                case "addOrUpdateActFinancePay"://建立实际付款
                    ispermiss = _ISysPermissionModelService.GetAddUpdateActFinancePermission(requestPermissionInfo.FuncCode, this.SessionCurrUserId, this.SessionCurrUserDeptId, requestPermissionInfo.ObjHtId, requestPermissionInfo.ObjId);
                    if (!ispermiss)
                    {
                        res.RetValue = 1;
                        res.Msg = "无权限";
                    }
                    break;
                case "detailBcInstance"://单品管理
               
                    ispermiss = _ISysPermissionModelService.GetBcInstanceDetailPermission(requestPermissionInfo.FuncCode, this.SessionCurrUserId);
                    if (!ispermiss)
                    {
                        res.RetValue = 1;
                        res.Msg = "无权限";
                    }
                    break;


                default:
                    {
                        res.RetValue = 1;
                        res.Msg = "无权限(未知操作)";
                    }
                    break;
            }
            return new CustomResultJson(res);

        }
        /// <summary>
        /// 状态修改
        /// </summary>
        /// <param name="requestPermissionInfo"></param>
        /// <returns></returns>
        public IActionResult StateUpdate(RequestPermissionInfo requestPermissionInfo)
        {
            var res = new RequstResult() { Code = 0, };
            PermissionDicEnum pssemtmp = PermissionDicEnum.None;
            switch (requestPermissionInfo.FuncCode)
            {
                case "updatecustomerstate"://查看客户详情
                case "updatesupplierstate"://供应商
                case "updateotherstate"://其他对方
                    {
                        pssemtmp = _ISysPermissionModelService.UpdateCmp(requestPermissionInfo.FuncCode,
                             this.SessionCurrUserId, this.SessionCurrUserDeptId, requestPermissionInfo.ObjId);
                        if (pssemtmp != PermissionDicEnum.OK)
                        {
                            res.RetValue = 1;
                            res.Msg = EmunUtility.GetDesc(typeof(PermissionDicEnum), (int)pssemtmp);
                        }
                    }
                    break;
                case "updateprojectstate"://项目
                    {
                        pssemtmp = _ISysPermissionModelService.UpdateProj(requestPermissionInfo.FuncCode,
                            this.SessionCurrUserId, this.SessionCurrUserDeptId, requestPermissionInfo.ObjId);
                        if (pssemtmp != PermissionDicEnum.OK)
                        {
                            res.RetValue = 1;
                            res.Msg = EmunUtility.GetDesc(typeof(PermissionDicEnum), (int)pssemtmp);
                        }
                    }
                    break;
                case "updatecollcontstate"://收款合同
                case "updatepaycontstate"://付款合同
                    {
                        pssemtmp = _ISysPermissionModelService.UpdateContract(requestPermissionInfo.FuncCode,
                            this.SessionCurrUserId, this.SessionCurrUserDeptId, requestPermissionInfo.ObjId);
                        if (pssemtmp != PermissionDicEnum.OK)
                        {
                            res.RetValue = 1;
                            res.Msg = EmunUtility.GetDesc(typeof(PermissionDicEnum), (int)pssemtmp);
                        }
                    }
                    break;
                case "updatezbcollcontstate"://招标
                    {
                        pssemtmp = _ISysPermissionModelService.UpdateZhao(requestPermissionInfo.FuncCode,
                            this.SessionCurrUserId, this.SessionCurrUserDeptId, requestPermissionInfo.ObjId);
                        if (pssemtmp != PermissionDicEnum.OK)
                        {
                            res.RetValue = 1;
                            res.Msg = EmunUtility.GetDesc(typeof(PermissionDicEnum), (int)pssemtmp);
                        }
                    }
                    break;
                case "updateInquirystate"://询价
                    {
                        pssemtmp = _ISysPermissionModelService.UpdateXunjia(requestPermissionInfo.FuncCode,
                            this.SessionCurrUserId, this.SessionCurrUserDeptId, requestPermissionInfo.ObjId);
                        if (pssemtmp != PermissionDicEnum.OK)
                        {
                            res.RetValue = 1;
                            res.Msg = EmunUtility.GetDesc(typeof(PermissionDicEnum), (int)pssemtmp);
                        }
                    }
                    break;
                case "updateQuestioningstate"://洽谈
                    {
                        pssemtmp = _ISysPermissionModelService.UpdateQia(requestPermissionInfo.FuncCode,
                            this.SessionCurrUserId, this.SessionCurrUserDeptId, requestPermissionInfo.ObjId);
                        if (pssemtmp != PermissionDicEnum.OK)
                        {
                            res.RetValue = 1;
                            res.Msg = EmunUtility.GetDesc(typeof(PermissionDicEnum), (int)pssemtmp);
                        }
                    }
                    break;
                case "AjgUpdatedate":
                    pssemtmp = _ISysPermissionModelService.UpdateAjgl(requestPermissionInfo.FuncCode,
                          this.SessionCurrUserId, this.SessionCurrUserDeptId, requestPermissionInfo.ObjId);
                    if (pssemtmp != PermissionDicEnum.OK)
                    {
                        res.RetValue = 1;
                        res.Msg = EmunUtility.GetDesc(typeof(PermissionDicEnum), (int)pssemtmp);
                    }
                    break;
                default:
                    {
                        res.RetValue = 1;
                        res.Msg = "无权限(未知操作)";
                    }
                    break;
            }
            return new CustomResultJson(res);

        }
        public IActionResult UpdatePermission(RequestPermissionInfo requestPermissionInfo)
        {
            var res = new RequstResult() { Code = 0, };
            PermissionDicEnum pssemtmp = PermissionDicEnum.None;
            switch (requestPermissionInfo.FuncCode)
            {
                case "updatecustomer"://修改客户
                case "updatesupplier"://修改供应商
                case "updateother"://其他对方
               // case "updateInquiry"://修改询价
                    {
                        pssemtmp = _ISysPermissionModelService.GetCmpUpdatePermission(requestPermissionInfo.FuncCode,
                            this.SessionCurrUserId, this.SessionCurrUserDeptId, requestPermissionInfo.ObjId);
                        if (pssemtmp != PermissionDicEnum.OK)
                        {
                            res.RetValue = 1;
                            res.Msg = EmunUtility.GetDesc(typeof(PermissionDicEnum), (int)pssemtmp);
                        }

                    }
                    break;
                //GetInquiryUpdatePermission
                case "updateInquiry":// 修改询价
                    {
                        pssemtmp = _ISysPermissionModelService.GetInquiryUpdatePermission(requestPermissionInfo.FuncCode,
                            this.SessionCurrUserId, this.SessionCurrUserDeptId, requestPermissionInfo.ObjId);
                        if (pssemtmp != PermissionDicEnum.OK)
                        {
                            res.RetValue = 1;
                            res.Msg = EmunUtility.GetDesc(typeof(PermissionDicEnum), (int)pssemtmp);
                        }

                    }
                    break;
                case "updatepaycont"://修改付款合同
                case "updatecollcont"://修改收款合同
              //  case"updateInquiry":// 修改询价
                    {
                        pssemtmp = _ISysPermissionModelService.GetContractUpdatePermission(requestPermissionInfo.FuncCode, 
                            this.SessionCurrUserId, this.SessionCurrUserDeptId, requestPermissionInfo.ObjId);
                        if (pssemtmp != PermissionDicEnum.OK)
                        {
                            res.RetValue = 1;
                            res.Msg = EmunUtility.GetDesc(typeof(PermissionDicEnum), (int)pssemtmp);
                        }

                    }
                    break;
                case "updateproject"://修改项目
                    pssemtmp = _ISysPermissionModelService.GetProjectUpdatePermission(requestPermissionInfo.FuncCode,
                        this.SessionCurrUserId, this.SessionCurrUserDeptId, requestPermissionInfo.ObjId);
                    if (pssemtmp != PermissionDicEnum.OK)
                    {
                        res.RetValue = 1;
                        res.Msg = EmunUtility.GetDesc(typeof(PermissionDicEnum), (int)pssemtmp);
                    }
                    break;
                case "addOrUpdateInvoice"://建立和修改发票权限
                case "addOrUpdateInvoiceOut":
                    bool ispermiss = _ISysPermissionModelService.GetAddUpdateInvoicePermission(requestPermissionInfo.FuncCode, this.SessionCurrUserId,this.SessionCurrUserDeptId, requestPermissionInfo.ObjId);
                    if (!ispermiss)
                    {
                        res.RetValue = 1;
                        res.Msg = "无权限";
                    }
                    break;
                
                case "ConfirmOrBackInvoiceOut"://确认/打回发票权限
                case "ConfirmOrBackInvoice":
                    bool ispermiss2 = _ISysPermissionModelService.GetConfirmBackInvoicePermission(requestPermissionInfo.FuncCode, this.SessionCurrUserId, this.SessionCurrUserDeptId, requestPermissionInfo.ObjId);
                    if (!ispermiss2)
                    {
                        res.RetValue = 1;
                        res.Msg = "无权限";
                    }
                    break;
                case "addOrUpdateActFinanceColl"://实际收款
                case "addOrUpdateActFinancePay"://实际付款
                    bool ispermiss3 = _ISysPermissionModelService.GetAddUpdateActFinancePermission(requestPermissionInfo.FuncCode, this.SessionCurrUserId, this.SessionCurrUserDeptId, requestPermissionInfo.ObjHtId, requestPermissionInfo.ObjId);
                    if (!ispermiss3)
                    {
                        res.RetValue = 1;
                        res.Msg = "无权限";
                    }
                    break;
                case "confirmOrBackActFinanceColl"://确认、打回实际资金
                case "confirmOrBackActFinancePay":
                    bool ispermiss4 = _ISysPermissionModelService.GetConfirmBackActFinancePermission(requestPermissionInfo.FuncCode, this.SessionCurrUserId, this.SessionCurrUserDeptId, requestPermissionInfo.ObjId);
                    if (!ispermiss4)
                    {
                        res.RetValue = 1;
                        res.Msg = "无权限";
                    }
                    break;
                case "addBcInstance"://单品修改
                    bool ispermissbc = _ISysPermissionModelService.GetBcInstanceAddOrUpdatePermission(requestPermissionInfo.FuncCode, this.SessionCurrUserId);
                    if (!ispermissbc)
                    {
                        res.RetValue = 1;
                        res.Msg = "无权限";
                    }
                    break;
                case "updateQuestioning":// 洽谈
                    {
                        pssemtmp = _ISysPermissionModelService.GetQuestioningUpdatePermission(requestPermissionInfo.FuncCode,
                            this.SessionCurrUserId, this.SessionCurrUserDeptId, requestPermissionInfo.ObjId);
                        if (pssemtmp != PermissionDicEnum.OK)
                        {
                            res.RetValue = 1;
                            res.Msg = EmunUtility.GetDesc(typeof(PermissionDicEnum), (int)pssemtmp);
                        }

                    }
                    break;
                case "JfsbUpdate"://纠纷上报修改
                    pssemtmp = _ISysPermissionModelService.GetJfsbUpdatePermission(requestPermissionInfo.FuncCode,
                           this.SessionCurrUserId, this.SessionCurrUserDeptId, requestPermissionInfo.ObjId);
                    if (pssemtmp != PermissionDicEnum.OK)
                    {
                        res.RetValue = 1;
                        res.Msg = EmunUtility.GetDesc(typeof(PermissionDicEnum), (int)pssemtmp);
                    }
                    break;
                case "AjgUpdate"://案件管理修改
                    pssemtmp = _ISysPermissionModelService.GetAjglUpdatePermission(requestPermissionInfo.FuncCode,
                          this.SessionCurrUserId, this.SessionCurrUserDeptId, requestPermissionInfo.ObjId);
                    if (pssemtmp != PermissionDicEnum.OK)
                    {
                        res.RetValue = 1;
                        res.Msg = EmunUtility.GetDesc(typeof(PermissionDicEnum), (int)pssemtmp);
                    }
                    break;
                   
                default:
                    {
                        res.RetValue = 1;
                        res.Msg = "无权限(未知操作)";
                    }
                    break;
            }

            return new CustomResultJson(res);

        }
        /// <summary>
        /// 次要字段修改权限
        /// </summary>
        /// <returns></returns>
        public IActionResult SecFieldUpatePremission(RequestPermissionInfo requestPermissionInfo)
        {
            var res = new RequstResult() { Code = 0, };
            PermissionDicEnum permission = PermissionDicEnum.None;
            switch (requestPermissionInfo.FuncCode)
            {
                case "updatecustomerminor"://修改客户次要字段
                case "updatesupplierminor"://修改次要字段
                case "updateotherminor"://其他对方
                    permission = _ISysPermissionModelService.GetCmpSecFieldUpdatePermission(requestPermissionInfo.FuncCode,
                        this.SessionCurrUserId, this.SessionCurrUserDeptId, requestPermissionInfo.ObjId);
                    if (permission != PermissionDicEnum.OK)
                    {
                        res.RetValue = 1;
                        res.Msg = EmunUtility.GetDesc(typeof(PermissionDicEnum), (int)permission);
                    }
                    break;
                case "updateprojectminor"://修改项目次要字段
                    permission = _ISysPermissionModelService.GetProjSecFieldUpdatePermission(requestPermissionInfo.FuncCode, 
                        this.SessionCurrUserId, this.SessionCurrUserDeptId, requestPermissionInfo.ObjId);
                    if (permission != PermissionDicEnum.OK)
                    {
                        res.RetValue = 1;
                        res.Msg = EmunUtility.GetDesc(typeof(PermissionDicEnum), (int)permission);
                    }
                    break;
                case "updatecollcontminor"://收款合同次要字段
                case "updatepaycontminor"://付款合同次要字段
                    permission = _ISysPermissionModelService.GetContractSecFieldUpdatePermission(requestPermissionInfo.FuncCode,
                        this.SessionCurrUserId, this.SessionCurrUserDeptId, requestPermissionInfo.ObjId);
                    if (permission != PermissionDicEnum.OK)
                    {
                        res.RetValue = 1;
                        res.Msg = EmunUtility.GetDesc(typeof(PermissionDicEnum), (int)permission);
                    }
                    break;
                   
                default:
                    {
                        res.RetValue = 1;
                        res.Msg = "无权限(未知操作)";
                    }
                    break;


            }

            return new CustomResultJson(res);

        }

      
        /// <summary>
        /// 查看页面按钮权限
        /// </summary>
        /// <param name="Id">当前对象ID</param>
        /// <param name="perCode">当前操作对象类别=》contract：合同。。。。</param>
        /// <returns></returns>
        public IActionResult DetailBtnPermission(string perCode, int Id)
        {
            BtnPremissionInfo btnPremission = new BtnPremissionInfo();
            switch (perCode)
            {
                case "contract"://合同
                    btnPremission = _ISysPermissionModelService.GetContractBtnPermission(Id);
                    break;
                case "project"://项目
                    btnPremission = _ISysPermissionModelService.GetProjBtnPermission(Id);
                    break;
                case "company"://合同对方
                    btnPremission = _ISysPermissionModelService.GetCmpBtnPermission(Id);
                    break;
                case "sche"://进度管理
                    btnPremission = _ISysPermissionModelService.GetscheBtnPermission(Id);
                        break;

            }
            var res = new RequstResult() { Code = 0, Data = btnPremission };
            return new CustomResultJson(res);
        }

        /// <summary>
        /// 查询当前用户所拥有的权限列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetUserPermission()
        {
            var roleIds = _IUserRoleService.GetQueryable(a => a.UserId == this.SessionCurrUserId).AsNoTracking().Select(a => a.RoleId).ToList();
            var rolePermissions = _IRolePermissionService.GetQueryable(a => roleIds.Contains(a.RoleId)).AsNoTracking().ToList();
            var userPermissions = _IUserPermissionService.GetQueryable(a => a.UserId == this.SessionCurrUserId).AsNoTracking().Select(a => a).ToList();
            if (RedisHelper.KeyExists($"{StaticData.UserPermissions}:{this.SessionCurrUserId}"))
            {
                RedisHelper.KeyDelete($"{StaticData.UserPermissions}:{this.SessionCurrUserId}");
            }
            if (RedisHelper.KeyExists($"{StaticData.UserRolePermissions}:{this.SessionCurrUserId}"))
            {
                RedisHelper.KeyDelete($"{StaticData.UserRolePermissions}:{this.SessionCurrUserId}");
            }
            if (userPermissions.Count() > 0)
            {
                RedisHelper.StringSetAsync($"{StaticData.UserPermissions}:{this.SessionCurrUserId}", JsonUtility.SerializeObject(userPermissions));
            }
            if (rolePermissions.Count() > 0)
            {
                RedisHelper.StringSetAsync($"{StaticData.UserRolePermissions}:{this.SessionCurrUserId}", JsonUtility.SerializeObject(rolePermissions));
            }
            var res = new RequstResult() { Code = 0, Msg = "权限初始化完毕！" };
            return new CustomResultJson(res);

        }
        /// <summary>
        /// 按钮权限
        /// </summary>
        /// <returns></returns>
        public IActionResult GetBtnPermission(string opType,int Id)
        {
            switch (opType)
            {
                case "invoice"://发票

                    break;

            }
            var res = new RequstResult() { Code = 0, Msg = "" };
            return new CustomResultJson(res);
        }
    }
}