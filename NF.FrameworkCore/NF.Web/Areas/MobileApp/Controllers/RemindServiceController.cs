using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NF.Common.Models;
using NF.Common.Utility;
using NF.ViewModel.APPModels;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NF.Web.Areas.MobileApp.Controllers
{
    [Area("MobileApp")]
    [Route("MobileApp/[controller]/[action]")]
    public class RemindServiceController : Controller
    {
        /// <summary>
        /// 查询提醒    
        /// </summary>
        /// <returns></returns>
        public IActionResult GetList(int userId,string callback)
        {
            var key = "InsertTExing:"+ userId;
            var layPage = HashGetAll(key);
            var result = new TOjson<TIxing> { totalCount = "", items = layPage };
            string json = JsonUtility.SerializeObject(result);
            NF.Common.Utility.PublicMethod O = new PublicMethod();
            return Content(O.Jsonp(json, callback));
        } 
        /// <summary>
        /// 获取存储在指定键的哈希中的所有字段和值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IList<TIxing> HashGetAll(string key)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            var collection = RedisUtility._redisData.HashGetAll(key);
            IList<TIxing> listdata = new List<TIxing>();
            foreach (var item in collection)
            {
                listdata.Add(JsonUtility.DeserializeJsonToObject<TIxing>(item.Value));
               // dic.Add(item.Name, );
            }
            return listdata;
         }

        public static TIxing HashGetAsyncall(string key, string remindId)
        {
            var collection1 = RedisUtility._redisData.HashGet(key, remindId);
            TIxing tex=JsonUtility.DeserializeJsonToObject<TIxing>(collection1);
            return tex;
        }

        public static string DeleteHashGetAllss(string key)
        {
            var collection = RedisUtility._redisData.HashGetAll(key);
            IList<TIUuid> listdata = new List<TIUuid>();
            foreach (var item in collection)
            {
                var Name = item.Name;
                RedisHelper.HashDelete(key, Name);
            }
            return "ok";
        }
        public static string updateHashGetAllss(string key)
        {
            var collection = RedisUtility._redisData.HashGetAll(key);
            IList<TIUuid> listdata = new List<TIUuid>();
            foreach (var item in collection)
            {
                var Name = item.Name;
                var layPage = HashGetAsyncall(key, Name);
                TIxing result = new TIxing() { Uuid = Name, date = layPage.date, info = layPage.info, UserID = layPage.UserID, state = 1 };
                string json = JsonUtility.SerializeObject(result);
                RedisHelper.HashUpdate(key, Name, json);
            }
            return "ok";
        }
        /// <summary>
        /// 添加提醒
        /// </summary>
        /// <returns></returns>
        public IActionResult Insert()
        {
            return View();
        }
        /// <summary>
        /// 删除提醒
        /// </summary>
        /// <returns></returns>
        public string DeeleteRemin(string remindId, int userId)
        {
            //Redis KEY值
            var key = "InsertTExing:" + userId;
            var layPage = HashGetAll(key);
            RedisHelper.HashDelete(key, remindId);
            return "ok";
        }
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <returns></returns>
        public string UpdateDeeleteAll(string remindId, int userId)
        {
            var key = "InsertTExing:" + userId;
            //var layPage = HashGetAsync(key, remindId);
            var layPage = HashGetAsyncall(key, remindId);
            TIxing result = new TIxing() { Uuid = remindId, date = layPage.date, info = layPage.info, UserID = userId, state = 1 };
            string json = JsonUtility.SerializeObject(result);
            RedisHelper.HashUpdate(key, remindId, json);
            return "ok";
        }
        /// <summary>
        /// 删除全部
        /// </summary>
        /// <returns></returns>
        public IActionResult DeeleteAll(int userId)
        {
            var key = "InsertTExing:" + userId;
            DeleteHashGetAllss(key);
            return View();
        }

        /// <summary>
        /// 全部标记为已读
        /// </summary>
        /// <returns></returns>
        public IActionResult UpdatedesAll(int userId)
        {
            var key = "InsertTExing:" + userId;
            updateHashGetAllss(key);
            return View();
        }
    }
}
