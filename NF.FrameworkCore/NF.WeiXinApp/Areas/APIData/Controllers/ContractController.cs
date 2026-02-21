using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.ViewModel.Models.WeiXinModels;
using NF.WeiXin.Lib.Common;
using NF.WeiXin.Lib.Utility;
using NF.WeiXinApp.Extend;
using NF.WeiXinApp.Utility;
using NF.WeiXinApp.Utility.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using Log4netHelper = NF.WeiXin.Lib.Utility.Log4netHelper;

namespace NF.WeiXinApp.Areas.APIData.Controllers
{
    //[EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    [ApiController]

    public class ContractController : ControllerBase
    {
        private IContractInfoService _IContractInfoService;
        private IUserInforService _IUserInforService;
        private IDataDictionaryService _IDataDictionaryService;
        private ISysPermissionModelService _ISysPermissionModelService;
        private IContTextService _IContTextService;
        /// <summary>
        /// 提醒
        /// </summary>
        private IRemindService _IRemindService;
        public ContractController(
            IContTextService IContTextService,
            IContractInfoService IContractInfoService,
            IUserInforService IUserInforService,
            IDataDictionaryService IDataDictionaryService,
            ISysPermissionModelService ISysPermissionModelService,
            IRemindService IRemindService
            )
        {
            _IContTextService = IContTextService;
            _ISysPermissionModelService = ISysPermissionModelService;
            _IRemindService = IRemindService;
            _IContractInfoService = IContractInfoService;
            _IUserInforService = IUserInforService;
            _IDataDictionaryService = IDataDictionaryService;
        }
        // GET: api/<UserController>
        [HttpGet("Htlist")]
        public string Get(int page, int limit, string keyWord, string wxzh, int FinanceType)
        {
            PageparamInfo pageParam = new PageparamInfo();
            var usinfo = _IContractInfoService.Yhinfo(wxzh);
            if (usinfo != null)
            {
                var UsId = usinfo.Id;
                var deptId = usinfo.DepartmentId;
                var pageInfo = new PageInfo<ContractInfo>(pageIndex: page, pageSize: limit);
                var predicateAnd = PredicateBuilder.True<ContractInfo>();
                predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, keyWord, pageParam.search, UsId, deptId ?? 0, FinanceType));
                Expression<Func<ContractInfo, object>> orderbyLambda = null;
                bool IsAsc = false;
                switch (pageParam.orderField)
                {

                    default:
                        orderbyLambda = a => a.Id;
                        break;

                }
                if (pageParam.orderType == "asc")
                    IsAsc = true;
                var layPage = _IContractInfoService.WXCountGetList(pageInfo, predicateAnd, orderbyLambda, IsAsc);
                return layPage.ToWxJson();
            }
            else
            {
                var layPage = new LayPageInfo<ContractInfoListViewDTO>();
                return layPage.ToWxJson();
            }




        }
        /// <summary>
        /// 获取查询条件表达式
        /// </summary>
        /// <param name="pageInfo">查询分页器，传NoPageInfo对象不分页</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        private Expression<Func<ContractInfo, bool>> GetQueryExpression(PageInfo<ContractInfo> pageInfo, string keyWord, int? search, int usid, int depId, int FinanceType)
        {
            var predicateAnd = PredicateBuilder.True<ContractInfo>();
            var predicateOr = PredicateBuilder.False<ContractInfo>();
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0 && a.FinanceType == FinanceType);
            var sd = "";
            if (FinanceType==0)
            {
                sd = "querycollcontlist";
            }
            else if (FinanceType == 1)
            {
                sd = "querypaycontlist";
            }
            predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetContractListPermissionExpression(sd, usid, depId));
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => !string.IsNullOrEmpty(a.Name) && a.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => !string.IsNullOrEmpty(a.Code) && a.Code.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            return predicateAnd;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        [HttpGet("GetCountViwe")]
        public string GetCountViwe(int id)
        {
            var info = _IContractInfoService.ShowWxViewMode(id);

            return new RequestData(data: info).ToWxJson();
        }
        [HttpGet("GetContTextViwe")]
        public IList<WxCountText> GetContTextViwe(int id)
        {
            var info = _IContTextService.WxShowViews(id);

            return info;
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

        /// <summary>
        /// pdf 文件下载
        /// </summary>
        /// <param name="Id">文件ID</param>
        /// <returns></returns>
        [HttpGet("GetPdf")]
        public IActionResult GetFileBytes(int Id)
        {
            //Log4netHelper.Info("请求GetFileBytes");
           //string guidFileName = string.Empty;
           var contText = _IContTextService.Find(Id);
           
            DownLoadAndUploadRequestInfo downLoad = new DownLoadAndUploadRequestInfo();
            downLoad.Id = Id;
            downLoad.folderIndex = 6;
            var txturl = $"{Constant.WxDownloadurl}/{contText.Path}";
            //Log4netHelper.Info($"文件url:{txturl}");
            //本地保存路径
            var pathf = Path.Combine(
                            Directory.GetCurrentDirectory(), "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), downLoad.folderIndex)
                            , contText.FileName);
            RequestUtility.Download(txturl, pathf);//下载到wxapp
            var downInfo = FileStreamingHelper.Download(pathf);//下载到微信客户端

            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);
        }

        ///// <summary>
        ///// pdf 文件下载
        ///// </summary>
        ///// <param name="Id">文件ID</param>
        ///// <returns></returns>
        //[HttpGet("GetJpg")]
        //public IActionResult GetJpg(int Id)
        //{
        //    //string guidFileName = string.Empty;
        //    var contText = _IContTextService.Find(Id);

        //    DownLoadAndUploadRequestInfo downLoad = new DownLoadAndUploadRequestInfo();
        //    downLoad.Id = Id;
        //    downLoad.folderIndex = 6;
        //    var txturl = $"{Constant.WxDownloadurl}/{contText.Path}";
        //    //Log4netHelper.Info($"图片文件url:{txturl}");
        //    ////本地保存路径
        //    //var pathf = Path.Combine(
        //    //                Directory.GetCurrentDirectory(), "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), downLoad.folderIndex)
        //    //                , contText.FileName);
        //    //RequestUtility.Download(txturl, pathf);//下载到wxapp
        //    //var downInfo = FileStreamingHelper.Download(pathf);//下载到微信客户端

        //    //return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);
           
        //}
        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="txtId">合同文本ID</param>
        /// <returns></returns>
        [HttpGet("DownLoadTxt")]
        public IActionResult DownLoadTxt(int txtId)
        {
            var txtur3 = $"{Constant.WxAPPRequestUrl}";
            var txtur2 = $"{Constant.WxAppBaseURL}";
            //本地保存路径
            var txturl = $"http://localhost:8096/WeiXin/HtFile/WxFileDownLoad";


            var httxinfo = _IContTextService.Find(txtId);
            Uri url = new Uri(txturl);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            using (Stream stream = request.GetResponse().GetResponseStream())
            {
                //文件流，流信息读到文件流中，读完关闭//@"download.jpg"
                //using (FileStream fs = File.Create(loadpath))
                //{
                //    //建立字节组，并设置它的大小是多少字节
                //    int length = 1024;//缓冲，1kb，如果设置的过大，而要下载的文件大小小于这个值，就会出现乱码。
                //    byte[] bytes = new byte[length];
                //    int n = 1;
                //    while (n > 0)
                //    {
                //        //一次从流中读多少字节，并把值赋给Ｎ，当读完后，Ｎ为０,并退出循环
                //        n = stream.Read(bytes, 0, length);
                //        fs.Write(bytes, 0, n); //将指定字节的流信息写入文件流中
                //    }
                //}
            }
            return Ok();
            //return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);
        }
    }
}
