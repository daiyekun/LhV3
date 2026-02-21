using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NF.IBLL;
using NF.WeiXinApp.Extend;
using NF.WeiXinApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NF.WeiXinApp.Areas.APIData.Controllers
{

   
    [Route("api/[controller]")]
    [ApiController]
   
    public class UserController : ControllerBase
    {
        private IUserInforService _IUserInforService;
        public UserController(IUserInforService IUserInforService)
        {
            _IUserInforService = IUserInforService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        /// <summary>
        /// 根据微信账号获取用户信息
        /// </summary>
        /// <param name="WxId">微信账号</param>
        /// <returns></returns>
        [HttpGet("GetUserByWxId")]
        public string GetUserByWxId(string WxId)
        {
            //base.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            //base.HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
            //base.HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "DNT,web-token,app-token,Authorization,Accept,Origin,Keep-Alive,User-Agent,X-Mx-ReqToken,X-Data-Type,X-Auth-Token,X-Requested-With,If-Modified-Since,Cache-Control,Content-Type,Range");
            //base.HttpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            if (string.IsNullOrWhiteSpace(WxId))
            {
                //var data = _IUserInforService.GetWxUserById(WxId);
                return new RequestData(data: null).ToWxJson();
            }
            else
            {
                var data = _IUserInforService.GetWxUserById(WxId);
                return new RequestData(data: data).ToWxJson();
            }


        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
