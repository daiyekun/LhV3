using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NF.IBLL;

namespace NF.Web.Areas.ContractDraft.Controllers
{
    /// <summary>
    /// 文本锁
    /// </summary>
    [Area("ContractDraft")]
    [Route("ContractDraft/[controller]/[action]")]
    public class ContTextLockController : Controller
    {
         /// <summary>
        /// 合同文本
        /// </summary>
        private IContTextService _IContTextService;
        private readonly ILogger _logger;
        public ContTextLockController(IContTextService IContTextService
            , ILogger<ContTextLockController> logger)
        {
            _IContTextService = IContTextService;
            _logger = logger;
        }
        /// <summary>
        /// 锁文本
        /// </summary>
        /// <param name="txtId">文本ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public IActionResult Locktext(int txtId,int userId)
        {

            try
            {
                var cont_text = _IContTextService.Find(txtId);
                Int32 lockStatus = cont_text.TextLock ?? -1;

                bool ret = false;
                if (lockStatus < 0)
                {
                    cont_text.LockTime = DateTime.Now;
                    cont_text.TextLock = userId;
                    ret = true;
                }
                else if (lockStatus == userId)
                {
                    cont_text.LockTime = DateTime.Now;
                    ret = true;
                }
                else
                {
                    TimeSpan intervalTime = DateTime.Now - (DateTime)cont_text.LockTime;
                    if (intervalTime.TotalMinutes > 5)
                    {
                        cont_text.LockTime = DateTime.Now;
                        cont_text.TextLock = userId;
                        ret = true;
                    }
                }
                _IContTextService.SaveChanges();
                if (ret)
                {
                    return Content("SUC");
                }
                else
                {
                    return Content("LOCKED");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Content("ERR");
            }

        }
        /// <summary>
        /// 重新设置锁定时间
        /// </summary>
        /// <param name="txtId">当前文本</param>
        /// <returns></returns>
        public IActionResult Keeplock(int txtId)
        {

            try
            {
                var cont_text = _IContTextService.Find(txtId);
                cont_text.LockTime = DateTime.Now;
                _IContTextService.SaveChanges();
                return Content("SUC");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Content("ERR");
            }
        }
        /// <summary>
        /// 解锁
        /// </summary>
        public IActionResult UnLockText(int? txtId)
        {
            try
            {
                if ((txtId ?? -1) < 0)
                {
                    return Content("ERR");
                }
                var cont_text = _IContTextService.Find(txtId??-1);
                if (cont_text != null) { 
                cont_text.LockTime = null;
                cont_text.TextLock = -1;
                _IContTextService.SaveChanges();
                return Content("SUC");
                }
                else
                {
                    return Content("ERR");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Content("ERR");
            }
        }
        /// <summary>
        /// 判断锁
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IActionResult CheckLock(string ids)
        {

            try
            {
                string result = "UNLOCKED";
                string[] textId = ids.Split(',');
                Int32 lockStatus;
                foreach (var item in textId)
                {
                    var i = Convert.ToInt32(item);
                    var cont_text = _IContTextService.Find(i);
                    lockStatus = cont_text.TextLock ?? -1;
                    if (cont_text.LockTime == null)
                        break;

                    TimeSpan intervalTime = DateTime.Now - (DateTime)cont_text.LockTime;
                    if (lockStatus > 0 && intervalTime.TotalMinutes < 5)
                        result = "LOCKED";
                }


                return Content(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Content("ERR");
            }
        }

    }
}