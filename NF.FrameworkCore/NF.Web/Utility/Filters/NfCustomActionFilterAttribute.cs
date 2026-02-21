using Microsoft.AspNetCore.Mvc.Filters;
using NF.BLL;
using NF.Common.Utility;
using NF.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NF.Model.Models;
using NF.Common.SessionExtend;
using Microsoft.AspNetCore.Http;
using NF.ViewModel.Extend.Enums;
using NF.Common.Extend;
using NF.Web.Utility.DI;
using NF.Web.Controllers;
using Microsoft.AspNetCore.Http;
namespace NF.Web.Utility.Filters
{
    /// <summary>
    /// 方法执行过滤器,主要用于控制器方法,全局太浪费
    /// </summary>
    public class NfCustomActionFilterAttribute : Attribute, IActionFilter
    {
        /// <summary>
        /// 方法标题
        /// </summary>
        private string ActionTitle { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
       private OptionLogTypeEnum optionType { get; set; }
        /// <summary>
        /// 操作描述
        /// </summary>
       private string Remark { get; set; }
        /// <summary>
        /// 参数是不是对象传输
        /// </summary>
        private bool RequestObj { get; set; }
        /// <summary>
        /// 创建人id
        /// </summary>
        private int cjid { get; set; }
        //public NfCustomActionFilterAttribute() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_actionTitle">方法标题</param>
        /// <param name="_optionLogType">操作类型</param>
        public NfCustomActionFilterAttribute(string _actionTitle, OptionLogTypeEnum _optionLogType,string _Remark,bool _RequestObj)
        {
            ActionTitle = _actionTitle;
            optionType = _optionLogType;
            Remark = _Remark;
            RequestObj = _RequestObj;
         


        }

        /// <summary>
        /// 方法执行结束
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
           
        }
        /// <summary>
        /// 方法执行中
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                //context.ActionArguments
        
                    
                OptionLog OptionLog = GetOptionLogInfo(context);
             //   OptionLog.UserId = 1;
                var dbcontext = context.HttpContext.RequestServices.GetService(typeof(NFDbContext)) as NFDbContext;

                //IOptionLogService optionlogService = ServicesDIUtility.GetService<IOptionLogService, OptionLogService, OptionLog>();
                //optionlogService.Add(OptionLog);
                dbcontext.Add<OptionLog>(OptionLog);
                dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {

                Log4netHelper.Error($"添加操作日志错误:{ex.Message}");
            }
            //存储Redis队列
            //RedisHelper.ListRightPush(StaticData.OptionLogRedisKey, OptionLog);


        }
        /// <summary>
        /// 生成日志对象
        /// </summary>
        /// <param name="context"></param>
   //     /// <returns></returns>
   //     private OptionLog GetOptionLogInfo(ActionExecutingContext context)
   //     {
   //         OptionLog sd = new OptionLog();
   //sd.RequestUrl = context.HttpContext.Request.Path.ToString();
   //         sd.UserId = context.HttpContext.Session.GetInt32("NFUserId");// HttpContext.Session.GetInt32(StaticData.NFUserId) ?? 0;// context.HttpContext.Session.GetInt32("NFUserId");
         
   //         sd.ActionTitle = this.ActionTitle;
   //         sd.RequestData = RequestDataHpler.ExceListData(context.ActionArguments.ToList(), RequestObj);
   //         sd.ControllerName = context.Controller.ToString();
   //         sd.RequestIp = context.HttpContext.GetUserIp();
   //         sd.CreateDatetime = DateTime.Now;
   //         sd.Status = 0;
   //         sd.Remark = this.Remark;
   //         sd.OptionType = (byte)optionType;
   //         sd.RequestMethod = (byte)(context.HttpContext.Request.Method.ToString().ToUpper().Equals("POST") ? RequestMethodEnum.POST : RequestMethodEnum.GET);
   //         sd.ActionName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName;
   //         return sd;
   //         //return new OptionLog
   //         //{
         

   //         //};
   //     }

        /// <summary>
        /// 生成日志对象
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private OptionLog GetOptionLogInfo(ActionExecutingContext context)
        {
            return new OptionLog
            {
                UserId = context.HttpContext.Session.GetInt32("NFUserId"),
                RequestUrl = context.HttpContext.Request.Path.ToString(),
                ActionTitle = this.ActionTitle,
                RequestData = RequestDataHpler.ExceListData(context.ActionArguments.ToList(), RequestObj),
                ControllerName = context.Controller.ToString(),
                RequestIp = context.HttpContext.GetUserIp(),
                CreateDatetime = DateTime.Now,
                Status = 0,
                Remark = this.Remark,
                OptionType = (byte)optionType,
                RequestMethod = (byte)(context.HttpContext.Request.Method.ToString().ToUpper().Equals("POST") ? RequestMethodEnum.POST : RequestMethodEnum.GET),
                ActionName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName

            };
        }
    }
}
