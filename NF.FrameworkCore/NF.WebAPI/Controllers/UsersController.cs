using Microsoft.AspNetCore.Mvc;
using NF.IBLL;
using NF.WebAPI.Utility;
using NF.WebAPI.Utility.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NF.WebAPI.Extend;
using Microsoft.AspNetCore.Cors;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NF.WebAPI.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserInforService _IUserInforService;
        public UsersController(IUserInforService IUserInforService)
        {
            _IUserInforService= IUserInforService;

        }

        // GET: api/<UserController>
        
        /// <summary>
        /// 获取详情信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        // GET api/<UserController>/5
       
        [HttpGet]
        public string GetUser(string WxId)
        {
            var data = _IUserInforService.GetWxUserById(WxId);
            return new RequestData(data: data).ToJson();
            
        }
        [HttpGet("GetUserByWxId")]
        public string GetUserByWxId(string WxId)
        {
            base.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            base.HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
            base.HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "DNT,web-token,app-token,Authorization,Accept,Origin,Keep-Alive,User-Agent,X-Mx-ReqToken,X-Data-Type,X-Auth-Token,X-Requested-With,If-Modified-Since,Cache-Control,Content-Type,Range");
            base.HttpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            var data = _IUserInforService.GetWxUserById(WxId);
            return new RequestData(data: data).ToJson();

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
