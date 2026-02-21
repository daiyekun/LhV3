using Common.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common.Utility
{
    /// <summary>
    /// word下载帮助类
    /// </summary>
    public class WordDownloadUtility
    {
        /// <summary>
        /// 创建零时存储Word路径
        /// </summary>
        public static string CreateWordTempPath(string arg)
        {
            var tnk = System.DateTime.Now.Ticks.ToString();
            string tempPath = System.Environment.GetEnvironmentVariable("TEMP");
            string tempfile = string.Format("{0}_{1}.docx", arg, tnk);
            return Path.Combine(tempPath, tempfile);

        }
        /// <summary>
        /// 创建默认的零时存储Word路径
        /// </summary>
        public static string CreateWordTempPathDefault()
        {
            return CreateWordTempPath("LhWordAddin");

        }

        #region 创建请求路径

        /// <summary>
        /// 获取模板
        /// </summary>
        /// <returns></returns>
        public string GetContTextTemp(int TempId, int UserId)
        {
            var requestUrl = UrlDataUtility.GetOptionUrl(CreateUrlType.Temp)+ "GetTemplateFile";
            StringBuilder strb = new StringBuilder();
            strb.Append("cmd=gettemplatefile");
            strb.Append("&uid=" + UserId);
            strb.Append("&tplid=" + TempId);
            strb.Append("&locale=zh_cn");
            var postdata = strb.ToString();
            return HttpRequestUtility.HttpPost(requestUrl, postdata);
            //return strfile;
        }
        /// <summary>
        /// 获取合同文本对应模板路径及文本路径
        /// </summary>
        /// <param name="cttextid">合同文本ID</param>
        /// <param name="UserId">用户ID</param>
        /// <returns>返回文件路径</returns>
        public string GetContTextTempDefarUrl(int cttextid, int UserId)
        {
            var requestUrl = UrlDataUtility.GetOptionUrl(CreateUrlType.TextDraft)+ "GetContTextTempVsto"; 
            StringBuilder strb = new StringBuilder();
            strb.Append("cmd=getcontracttexttemplate_vsto");
            strb.Append("&uid=" + UserId);
            if(WordShare.requestWordType== requestAddinWordType.history_readonly)
            {
                strb.Append("&h_cttextid=" + cttextid);//查看其他版本时预览
            }
            else
            {
                strb.Append("&cttextid=" + cttextid);
            }
            strb.Append("&locale=zh_cn");
            var postdata = strb.ToString();
            return HttpRequestUtility.HttpPost(requestUrl, postdata);
        }
        /// <summary>
        /// 合同文本审阅时获取合同文本路径
        /// </summary>
        /// <param name="cttextid">合同文本ID</param>
        /// <param name="UserId">用户ID</param>
        /// <returns>合同文本路径</returns>
        public string GetContractReviewUrl(int cttextid, int UserId)
        {
            var requestUrl = UrlDataUtility.GetOptionUrl(CreateUrlType.TextReview)+ "GetContractText"; 
            StringBuilder strb = new StringBuilder();
            //strb.Append("cmd=/ContractReview/getContractText");
            strb.Append("uid=" + UserId);
            strb.Append("&cttextid=" + cttextid);
            var postdata = strb.ToString();
            return HttpRequestUtility.HttpPost(requestUrl, postdata);
            

        }
        /// <summary>
        /// 合同文本下载时-获取文本路径
        /// </summary>
        /// <param name="cttextid">合同文本ID</param>
        /// <param name="UserId">用户ID</param>
        /// <returns>合同文本路径</returns>
        public string GetContractRawUrl(int cttextid, int UserId)
        {
            var requestUrl = UrlDataUtility.GetOptionUrl(CreateUrlType.TextDownload)+ "GetContTextTempVsto"; 
            StringBuilder strb = new StringBuilder();
            strb.Append("uid=" + UserId);
            strb.Append("&cttextid=" + cttextid);
            var postdata = strb.ToString();
            return HttpRequestUtility.HttpPost(requestUrl, postdata);

        }
        /// <summary>
        /// 获取合同水印文件
        /// </summary>
        /// <param name="cttextid">合同文本ID</param>
        /// <param name="UserId">用户ID</param>
        /// <returns>合同文本路径</returns>
        public string GetContractWordWaterMark()
        {
            var requestUrl = UrlDataUtility.GetOptionUrl(CreateUrlType.WaterMark); 
            StringBuilder strb = new StringBuilder();
            strb.Append("&Vsto=true");
            //strb.Append("&cttextid=" + cttextid);
            var postdata = strb.ToString();
            return HttpRequestUtility.HttpPost(requestUrl, postdata);

        }

        /// <summary>
        /// 合同对比时下载文件
        /// </summary>
        /// <param name="cttextid">合同文本ID</param>
        /// <param name="UserId">用户ID</param>
        /// <returns>合同文本路径</returns>
        public string GetContractTextCmpUrl(int cttextid, int UserId)
        {
            var requestUrl = UrlDataUtility.GetOptionUrl(CreateUrlType.TextCmp)+ "GetContractTextVersionedDoc"; 
            StringBuilder strb = new StringBuilder();
            //strb.Append("cmd=/ContractTextCmp/GetContractTextVersionedDoc");
            strb.Append("uid=" + UserId);
            strb.Append("&cttextid=" + cttextid);
            var postdata = strb.ToString();
            return HttpRequestUtility.HttpPost(requestUrl, postdata);

        }
        /// <summary>
        /// 获取当前文件路径
        /// </summary>
        /// <param name="fileId">文件ID，如果是模板就是模板文件ID，如果是正文就是正文文件ID</param>
        /// <param name="userId">当前登录用户</param>
        /// <param name="acType">访问类型</param>
        /// <returns>返回文件路径</returns>
        public string GetCurrentWordUrl(int fileId, int userId, ContTextOption acType)
        {

            string Result = string.Empty;
            switch (acType)
            {
                case ContTextOption.contractTpl: //"lhaddin://contractTpl/"://模板相关
                    Result =GetContTextTemp(fileId, userId);
                    break;
                case ContTextOption.contractText://"lhaddin://contractText/"://模板起草
                    Result = GetContTextTempDefarUrl(fileId, userId);
                    break;
                case ContTextOption.contractReview:// "lhaddin://contractReview/"://审阅文本
                    Result = this.GetContractReviewUrl(fileId, userId);
                    break;
                case ContTextOption.contractFinalProcess:// "lhaddin://contractFinalProcess/"://合同文本下载-或者PDF下载
                    Result = this.GetContractRawUrl(fileId, userId);
                    break;
                case ContTextOption.contractTextCmp:// "lhaddin://contractTextCmp/"://合同文本对比
                    Result = this.GetContractTextCmpUrl(fileId, userId);
                    break;

                default:
                    Result = "NO Access Type";
                    break;

            }
            return Result;



        }

        #endregion

        ///// <summary>
        ///// 创建当前下载word的路径
        ///// </summary>
        ///// <param name="baseUrl">当前请求base路径</param>
        ///// <returns>当前下载路径</returns>
        //public static string CreateCurrentDownloadWordUrl(ContTextOption contTextOption, string baseUrl,int userId,int conttextId,  string requstType = "")
        //{
        //    string httpRequestUrl = baseUrl + GetHttpBaseUrl(contTextOption, requstType);
        //    string requestData = GetRequestData(contTextOption, userId, conttextId, requstType);
        //    return HttpRequestUtility.HttpPost(httpRequestUrl, requestData);
        //}

        ///// <summary>
        ///// 创建当前下载word的路径
        ///// </summary>
        ///// <param name="baseUrl">当前请求base路径</param>
        ///// <returns>当前下载路径</returns>
        //public static string CreateCurrentDownloadWordUrlDefault(ContTextOption contTextOption,string requstType = "")
        //{
        //    //string httpRequestUrl = baseUrl + GetHttpBaseUrl(contTextOption, requstType);
        //    //string requestData = GetRequestData(contTextOption, userId, conttextId, requstType);
        //    //return HttpRequestUtility.HttpPost(httpRequestUrl, requestData);
        //    return CreateCurrentDownloadWordUrl(contTextOption, StoreData.RequestBaseUrl, StoreData.requestUserId, StoreData.requestTempId, requstType);
        //}
        ///// <summary>
        ///// 获取http请求路径
        ///// </summary>
        //private static string GetHttpBaseUrl(ContTextOption contTextOption, string requstType = "")
        //{
        //    string requestUrl = string.Empty;
        //    switch (contTextOption)
        //    {
        //        case ContTextOption.contractTpl:
        //            requestUrl = "/AjaxPage/WooContractText/ContTextTempValue.aspx";
        //            break;
        //        case ContTextOption.contractText:
        //            requestUrl = "/AjaxPage/WooContractText/ContTextDraft.aspx";
        //            break;
        //        case ContTextOption.contractReview:
        //            requestUrl = "/BLL/ModuleBusiness/Draft/Operation.aspx?cmd=/ContractReview/getContractText";
        //            break;
        //        case ContTextOption.contractFinalProcess:
        //            if (requstType.Equals("WordWaterMark"))
        //            {
        //                requestUrl = "/BLL/ModuleBusiness/Draft/Operation.aspx?cmd=/ContractAuthoring/getWordWaterMark";
        //            }
        //            else
        //            {
        //                requestUrl = "/BLL/ModuleBusiness/Draft/Operation.aspx?cmd=/ContractAuthoring/getContractTextTemplate";
        //            }
        //            break;
        //        case ContTextOption.contractTextCmp:
        //            requestUrl = "/BLL/ModuleBusiness/Draft/Operation.aspx?cmd=/ContractTextCmp/GetContractTextVersionedDoc";
        //            break;

        //    }
        //    return requestUrl;

        //}

        //#region 获取参数
        ///// <summary>
        ///// 获取模板
        ///// </summary>
        ///// <returns></returns>
        //public static string GetRequestData(ContTextOption contTextOption, int userId, int cttextid, string requstType = "")
        //{
        //    //var requestUrl = ConnData.GetOptionTempUrl();
        //    //StringBuilder strb = new StringBuilder();
        //    //strb.Append("cmd=gettemplatefile");
        //    //strb.Append("&uid=" + UserId);
        //    //strb.Append("&tplid=" + TempId);
        //    //strb.Append("&locale=zh_cn");
        //    //var postdata = strb.ToString();
        //    StringBuilder strb = new StringBuilder();
        //    switch (contTextOption)
        //    {
        //        case ContTextOption.contractTpl:
        //            {
        //                strb.Append("cmd=gettemplatefile");
        //                strb.Append("&uid=" + userId);
        //                strb.Append("&tplid=" + cttextid);
        //                strb.Append("&locale=zh_cn");
        //            }
        //            break;
        //        case ContTextOption.contractText:
        //            {
        //                strb.Append("cmd=getcontracttexttemplate_vsto");
        //                CreateParamData(userId, cttextid, strb);
        //                strb.Append("&locale=zh_cn");
        //            }
                   
        //            break;
        //        case ContTextOption.contractReview:
        //            {
        //                CreateParamData(userId, cttextid, strb);
        //            }

        //            break;
        //        case ContTextOption.contractFinalProcess:
        //            if (requstType.Equals("WordWaterMark"))
        //            {
        //                strb.Append("&Vsto=true");
        //            }
        //            else
        //            {
        //                CreateParamData(userId, cttextid, strb);
        //            }
        //            break;
        //        case ContTextOption.contractTextCmp:
        //            {
        //                CreateParamData(userId, cttextid, strb);
        //                strb.Append("&vsto=true");
        //            }
        //            break;

        //    }
        //    return strb.ToString();
           
        //}

        //private static void CreateParamData(int userId, int cttextid, StringBuilder strb)
        //{
        //    strb.Append("&uid=" + userId);
        //    strb.Append("&cttextid=" + cttextid);
        //}
        //#endregion

       


    }
}
