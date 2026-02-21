using Microsoft.AspNetCore.Http;
using NF.ViewModel.Models;
using NF.WeiXin.Lib.Common;
using NF.WeiXin.Lib.Module;
using NF.WeiXin.Lib.Module.MsgInfo;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.WeiXin.Lib.Utility
{
    /// <summary>
    /// 微信消息工具类
    /// </summary>
    public class WeiXinMsgUtility
    {
        /// <summary>
        /// 发送微信消息
        /// </summary>
        /// <returns></returns>
        public static string SubmitWxMsg(FlowWxMsgInfo wxMsgInfo)
        {
            return SubmitWxNewMsg(wxMsgInfo);
        }
        /// <summary>
        /// 发送文本卡片消息
        /// </summary>
        /// <param name="wxMsgInfo">消息对象</param>
        /// <returns></returns>
        public static string SubmitTextCardMsg(FlowWxMsgInfo wxMsgInfo)
        {
            try
            {
                string AuthUrl = $"{Constant.WxAppBaseURL}/WxRuKou/WxHttpRedirect";
                StringBuilder strb = new StringBuilder();
                string SpTitle = string.Empty;
                var WfType = wxMsgInfo.FlowType;//审批对象
                var OBJ_AMOUNT = wxMsgInfo.ObjMoney;//审批金额
                var OBJ_NAME = wxMsgInfo.ObjName;//审批对象名称
                var OBJ_NO = wxMsgInfo.ObjNo;//审批对象编号
                var OBJ_TYPE = wxMsgInfo.ObjType;//对象类别
                var OBJ_ID = wxMsgInfo.ObjId;//对象ID
                AuthUrl = AuthUrl + "?OBJID=" + OBJ_ID;
                //List<TextcardMain> _articles = new List<TextcardMain>();
                TextcardMain textcard = null;
                switch (WfType)
                {
                    case (int)FlowObjEnums.Contract:
                        strb.Append($"<div class=\"gray\">{DateTime.Now.ToString("yyyy年MM月dd日")}</div>");
                        strb.Append($"<div class=\"normal\">合同名称：{OBJ_NAME}</div>");
                        strb.Append($"<div class=\"normal\">合同编号：{OBJ_NO}</div>");
                        strb.Append($"<div class=\"normal\">合同对方：{wxMsgInfo.HtDf}</div>");
                        strb.Append($"<div class=\"normal\">合同类别：{OBJ_TYPE}</div>");
                        strb.Append($"<div class=\"normal\">合同金额：{OBJ_AMOUNT}</div>");
                        strb.Append($"<div class=\"normal\">执行部门：{wxMsgInfo.JbJg}</div>");
                        
                        if (wxMsgInfo.MsgType == 0)
                        {//审批通过
                            SpTitle = $"您有一份{wxMsgInfo.FinceType}合同审批通过";
                        }
                        else if(wxMsgInfo.MsgType == 1)
                        {
                            SpTitle = $"您有一份{wxMsgInfo.FinceType}合同被打回";
                            strb.Append($"<div class=\"highlight\">审批人：{wxMsgInfo.AppUser}</div>");
                            strb.Append($"<div class=\"highlight\">处理意见：{wxMsgInfo.Option}</div>");
                        }
                        else if (wxMsgInfo.MsgType == 6)
                        {//提醒之前节点
                            //if (wxMsgInfo.AppUser== wxMsgInfo.StartUser)
                            //{
                          
                                    SpTitle = $"{wxMsgInfo.AppUser}审批了一份{wxMsgInfo.FinceType}合同";
                                    strb = new StringBuilder();
                                    var resstr = wxMsgInfo.AppRest == 0 ? "同意" : "不同意";
                                    strb.Append($"<div class=\"normal\">合同名称：{OBJ_NAME}</div>");
                                    strb.Append($"<div class=\"normal\">合同编号：{OBJ_NO}</div>");
                                    strb.Append($"<div class=\"normal\">发起人：{wxMsgInfo.StartUser}</div>");
                                    strb.Append($"<div class=\"normal\">处理节点：{wxMsgInfo.Node}</div>");
                                    strb.Append($"<div class=\"normal\">处理意见：{wxMsgInfo.Option}</div>");
                                    strb.Append($"<div class=\"highlight\">处理结果：{resstr}</div>");
               
                        }
                        else
                        {
                        SpTitle = $"您有一份{wxMsgInfo.FinceType}合同需要审批";
                        }
                       // strb.Append($"<div class=\"highlight\">请您处理</div>");//测试
                        textcard = new TextcardMain()
                        {
                            title = SpTitle,
                            description = strb.ToString(),

                            url = WxQYHOAuth2Utility.GetAuthorizeURL(Constant.CorpId, AuthUrl, agentid: Constant.Agentid, state: "2020Contractdetail"), //"www.baidu.com"

                        };
                        strb.Append(textcard);

                        break;


                }
                var wxappmsg = new TextcardMsgInfo()
                {
                    touser = wxMsgInfo.WxCodes,
                    msgtype = "textcard",
                    agentid = Constant.Agentid,//企业应用ID：1
                    textcard = textcard

                };
                var PostData = JsonUtility.SerializeObject(wxappmsg);//.Replace("\\u0026", "&"); 
                //Log4netHelper.Info(PostData);
                WxQYHMsgManager.WxMsgSend(PostData);

                return "OK";
            }
            catch (Exception ex)
            {

                RedisHelper.ListRightPush("WxMsgList", wxMsgInfo);
                Log4netHelper.Error($"出现推送失败问题：{ex.Message}");
                return "NO";
            }


        }

        #region 发送新闻信息
        /// <summary>
        /// 发送新闻信息
        /// </summary>
        /// <param name="wxMsgInfo"></param>
        /// <returns></returns>
        public static string SubmitWxNewMsg(FlowWxMsgInfo wxMsgInfo)
        {

            var Wxz = "";
            try
            {

                StringBuilder strb = new StringBuilder();
                string SpTitle = string.Empty;

                var WfType = wxMsgInfo.FlowType;//审批对象
                var OBJ_AMOUNT = wxMsgInfo.ObjMoney;//审批金额
                var OBJ_NAME = wxMsgInfo.ObjName;//审批对象名称
                var OBJ_NO = wxMsgInfo.ObjNo;//审批对象编号
                var OBJ_TYPE = wxMsgInfo.ObjType;//对象类别
                var OBJ_ID = wxMsgInfo.ObjId;//对象ID
                //Log4netHelper.Info($"审批对象：" + WfType);
                //Log4netHelper.Info($"审批金额：" + OBJ_AMOUNT);
                //Log4netHelper.Info($"审批对象名称：" + OBJ_NAME);
                //Log4netHelper.Info($"审批对象编号：" + OBJ_NO);
                //Log4netHelper.Info($"对象类别：" + OBJ_TYPE);
                //Log4netHelper.Info($"对象ID：" + OBJ_ID);
                //Log4netHelper.Info($"提醒人：" + Wxz);
                var Utl = "";
                /// 客户  供应商  其他对方
                if (WfType == 0 || WfType == 1 || WfType == 2)
                {
                    Utl = "/Company/Detail?Id=" + OBJ_ID + "&FinanceType=" + WfType + "&T=6";
                }//合同
                else if (WfType == 3)
                {
                    Utl = "/Contract/Detail?Id=" + OBJ_ID + "&T=6";
                }//项目
                else if (WfType == 7)
                {
                    Utl = "/Project/Detail?Id=" + OBJ_ID + "&Wxz=" + Wxz + "&T=6";
                }//付款
                else if (WfType == 6)
                {
                    Utl = "/ActualFinance/Detail?Id=" + OBJ_ID + "&wxzh=" + Wxz + "&Ftype=1" + "&T=6";
                }//收票    开票
                else if (WfType == 4 || WfType == 5)
                {
                    var It = -1;
                    if (WfType == 4)
                    {
                        It = 1;
                    }
                    else if (WfType == 5)
                    {
                        It = 0;
                    }
                    Utl = "/ContInvoice/Detail?Id=" + OBJ_ID + "&Htid=" + 0 + "&wxzh=" + Wxz + "&Itype=" + It + "&T=6";
                }

                var urlId = Constant.WxAppBaseURL + Utl;
                // Constant.WxAppBaseURL + "?OBJID=" + OBJ_ID;
                article articleInfo = null;
                List<article> _articles = new List<article>();
                switch (WfType)
                {
                    case (int)FlowObjEnums.Customer:
                        strb.Append($"<div class=\"gray\">{DateTime.Now.ToString("yyyy年MM月dd日")}</div>");
                        strb.Append($"<div class=\"normal\">客户名称：{OBJ_NAME}</div>");
                        strb.Append($"<div class=\"normal\">客户编号：{OBJ_NO}</div>");
                        strb.Append($"<div class=\"normal\">客户类别：{OBJ_TYPE}</div>");
                       // strb.Append($"<div class=\"highlight\">请您处理！！！</div>");
                        if (wxMsgInfo.MsgType == 0)
                        {//审批通过
                            SpTitle = $"您有一个客户审批通过";
                        }
                        else if (wxMsgInfo.MsgType == 1)
                        {
                            SpTitle = $"您有一个客户被打回";
                        }
                        else if (wxMsgInfo.MsgType == 6)
                        {//提醒之前节点
                            SpTitle = $"{wxMsgInfo.AppUser}审批了{wxMsgInfo.StartUser}发起的客户";
                            strb = new StringBuilder();
                            var resstr = wxMsgInfo.AppRest == 0 ? "同意" : "不同意";
                            strb.Append($"<div class=\"normal\">客户名称：{OBJ_NAME}</div>");
                            strb.Append($"<div class=\"normal\">客户编号：{OBJ_NO}</div>");
                            strb.Append($"<div class=\"normal\">处理节点：{wxMsgInfo.Node}</div>");
                            strb.Append($"<div class=\"normal\">处理意见：{wxMsgInfo.Option}</div>");
                            strb.Append($"<div class=\"highlight\">处理结果：{resstr}</div>");

                        }
                        else
                        {
                            SpTitle = "您有一个客户需要审批";
                        }
                       
                        articleInfo = new article()
                        {

                            description = strb.ToString(),
                            title = SpTitle,
                            url = WxQYHOAuth2Utility.GetAuthorizeUrl(Constant.CorpId, urlId, state: "2017kfdetail"), //"www.baidu.com"

                        };
                        _articles.Add(articleInfo);
                        break;

                    case (int)FlowObjEnums.Supplier:
                        strb.Append($"<div class=\"gray\">{DateTime.Now.ToString("yyyy年MM月dd日")}</div>");
                        strb.Append($"<div class=\"normal\">供应商名称：{OBJ_NAME}</div>");
                        strb.Append($"<div class=\"normal\">供应商编号：{OBJ_NO}</div>");
                        strb.Append($"<div class=\"normal\">供应商类别：{OBJ_TYPE}</div>");
                       // strb.Append($"<div class=\"highlight\">请您处理！！！</div>");
                        if (wxMsgInfo.MsgType == 0)
                        {//审批通过
                            SpTitle = $"您有一个供应商审批通过";
                        }
                        else if (wxMsgInfo.MsgType == 1)
                        {
                            SpTitle = $"您有一个供应商被打回";
                        }
                        else if (wxMsgInfo.MsgType == 6)
                        {//提醒之前节点
                            SpTitle = $"{wxMsgInfo.AppUser}审批了{wxMsgInfo.StartUser}发起的供应商";
                            strb = new StringBuilder();
                            var resstr = wxMsgInfo.AppRest == 0 ? "同意" : "不同意";
                            strb.Append($"<div class=\"normal\">供应商名称：{OBJ_NAME}</div>");
                            strb.Append($"<div class=\"normal\">供应商编号：{OBJ_NO}</div>");
                            strb.Append($"<div class=\"normal\">处理节点：{wxMsgInfo.Node}</div>");
                            strb.Append($"<div class=\"normal\">处理意见：{wxMsgInfo.Option}</div>");
                            strb.Append($"<div class=\"highlight\">处理结果：{resstr}</div>");

                        }
                        else
                        {
                            SpTitle = "您有一个供应商需要审批";
                        }
                       
                        articleInfo = new article()
                        {

                            description = strb.ToString(),
                            title = SpTitle,
                            url = WxQYHOAuth2Utility.GetAuthorizeUrl(Constant.CorpId, urlId, state: "2017superlisdetail"), //"www.baidu.com"

                        };
                        _articles.Add(articleInfo);
                        break;
                    case (int)FlowObjEnums.Other:
                        strb.Append($"<div class=\"gray\">{DateTime.Now.ToString("yyyy年MM月dd日")}</div>");
                        strb.Append($"<div class=\"normal\">其他对方名称：{OBJ_NAME}</div>");
                        strb.Append($"<div class=\"normal\">其他对方编号：{OBJ_NO}</div>");
                        strb.Append($"<div class=\"normal\">其他对方类别：{OBJ_TYPE}</div>");
                       // strb.Append($"<div class=\"highlight\">请您处理！！！</div>");
                      
                        
                        if (wxMsgInfo.MsgType == 0)
                        {//审批通过
                            SpTitle = $"您有一个其他对方审批通过";
                        }
                        else if (wxMsgInfo.MsgType == 1)
                        {
                            SpTitle = $"您有一个其他对方被打回";
                        }
                        else if (wxMsgInfo.MsgType == 6)
                        {//提醒之前节点
                            SpTitle = $"{wxMsgInfo.AppUser}审批了{wxMsgInfo.StartUser}发起的其他对方";
                            strb = new StringBuilder();
                            var resstr = wxMsgInfo.AppRest == 0 ? "同意" : "不同意";
                            strb.Append($"<div class=\"normal\">其他对方名称：{OBJ_NAME}</div>");
                            strb.Append($"<div class=\"normal\">其他对方编号：{OBJ_NO}</div>");
                            strb.Append($"<div class=\"normal\">处理节点：{wxMsgInfo.Node}</div>");
                            strb.Append($"<div class=\"normal\">处理意见：{wxMsgInfo.Option}</div>");
                            strb.Append($"<div class=\"highlight\">处理结果：{resstr}</div>");

                        }
                        else
                        {
                            SpTitle = "您有一个其他对方需要审批";
                        }
                        articleInfo = new article()
                        {

                            description = strb.ToString(),
                            title = SpTitle,
                            url = WxQYHOAuth2Utility.GetAuthorizeUrl(Constant.CorpId, urlId, state: "2017OtherPartiesdetail"), //"www.baidu.com"

                        };
                        _articles.Add(articleInfo);
                        break;
                    case (int)FlowObjEnums.project:
                        strb.Append($"<div class=\"gray\">{DateTime.Now.ToString("yyyy年MM月dd日")}</div>");
                        strb.Append($"<div class=\"normal\">项目名称：{OBJ_NAME}</div>");
                        strb.Append($"<div class=\"normal\">项目编号：{OBJ_NO}</div>");
                        strb.Append($"<div class=\"normal\">项目类型：{OBJ_TYPE}</div>");
                       // strb.Append($"<div class=\"highlight\">请您处理！！！</div>");
                       
                        if (wxMsgInfo.MsgType == 0)
                        {//审批通过
                            SpTitle = $"您有一个项目审批通过";
                        }
                        else if (wxMsgInfo.MsgType == 1)
                        {
                            SpTitle = $"您有一个项目被打回";
                        }
                        else if (wxMsgInfo.MsgType == 6)
                        {//提醒之前节点
                            SpTitle = $"{wxMsgInfo.AppUser}审批了{wxMsgInfo.StartUser}发起的项目";
                            strb = new StringBuilder();
                            var resstr = wxMsgInfo.AppRest == 0 ? "同意" : "不同意";
                            strb.Append($"<div class=\"normal\">项目名称：{OBJ_NAME}</div>");
                            strb.Append($"<div class=\"normal\">项目编号：{OBJ_NO}</div>");
                            strb.Append($"<div class=\"normal\">处理节点：{wxMsgInfo.Node}</div>");
                            strb.Append($"<div class=\"normal\">处理意见：{wxMsgInfo.Option}</div>");
                            strb.Append($"<div class=\"highlight\">处理结果：{resstr}</div>");

                        }
                        else
                        {
                            SpTitle = "您有一个项目需要审批";
                        }
                        articleInfo = new article()
                        {
                            description = strb.ToString(),
                            title = SpTitle,
                            url = WxQYHOAuth2Utility.GetAuthorizeUrl(Constant.CorpId, urlId, state: "2017OtherPartiesdetail"), //"www.baidu.com"
                        };
                        _articles.Add(articleInfo);
                        break;
                    case (int)FlowObjEnums.payment://付款
                        strb.Append($"<div class=\"gray\">{DateTime.Now.ToString("yyyy年MM月dd日")}</div>");
                        strb.Append($"<div class=\"normal\">实际金额：{OBJ_AMOUNT}</div>");
                        strb.Append($"<div class=\"normal\">合同名称：{OBJ_NAME}</div>");
                        strb.Append($"<div class=\"normal\">合同编号：{OBJ_NO}</div>");
                       // strb.Append($"<div class=\"highlight\">请您处理！！！</div>");
                       
                        if (wxMsgInfo.MsgType == 0)
                        {//审批通过
                            SpTitle = $"您有一笔付款审批通过";
                        }
                        else if (wxMsgInfo.MsgType == 1)
                        {
                            SpTitle = $"您有一笔付款被打回";
                        }
                        else if (wxMsgInfo.MsgType == 6)
                        {//提醒之前节点
                            SpTitle = $"{wxMsgInfo.AppUser}审批了{wxMsgInfo.StartUser}发起的一笔付款";
                            strb = new StringBuilder();
                            var resstr = wxMsgInfo.AppRest == 0 ? "同意" : "不同意";
                            strb.Append($"<div class=\"normal\">实际金额：{OBJ_AMOUNT}</div>");
                            strb.Append($"<div class=\"normal\">合同名称：{OBJ_NAME}</div>");
                            strb.Append($"<div class=\"normal\">处理节点：{wxMsgInfo.Node}</div>");
                            strb.Append($"<div class=\"normal\">处理意见：{wxMsgInfo.Option}</div>");
                            strb.Append($"<div class=\"highlight\">处理结果：{resstr}</div>");

                        }
                        else
                        {
                            SpTitle = "您有一笔付款需要审批";
                        }
                        articleInfo = new article()
                        {
                            description = strb.ToString(),
                            title = SpTitle,
                            url = WxQYHOAuth2Utility.GetAuthorizeUrl(Constant.CorpId, urlId, state: "2017OtherPartiesdetail"), //"www.baidu.com"
                        };
                        _articles.Add(articleInfo);
                        break;
                    case (int)FlowObjEnums.InvoiceOut:

                        strb.Append($"<div class=\"gray\">{DateTime.Now.ToString("yyyy年MM月dd日")}</div>");
                        strb.Append($"<div class=\"normal\">类别：{OBJ_TYPE}</div>");
                        strb.Append($"<div class=\"normal\">合同名称：{OBJ_NAME}</div>");
                        strb.Append($"<div class=\"normal\">合同编号：{OBJ_NO}</div>");
                        strb.Append($"<div class=\"normal\">开票金额：{OBJ_AMOUNT}</div>");
                       // strb.Append($"<div class=\"highlight\">请您处理！！！</div>");
                      
                        
                        if (wxMsgInfo.MsgType == 0)
                        {//审批通过
                            SpTitle = $"您有一笔开票审批通过";
                        }
                        else if (wxMsgInfo.MsgType == 1)
                        {
                            SpTitle = $"您有一笔开票被打回";
                        }
                        else if (wxMsgInfo.MsgType == 6)
                        {//提醒之前节点
                            SpTitle = $"{wxMsgInfo.AppUser}审批了{wxMsgInfo.StartUser}发起的一笔开票";
                            strb = new StringBuilder();
                            var resstr = wxMsgInfo.AppRest == 0 ? "同意" : "不同意";
                            strb.Append($"<div class=\"normal\">开票金额：{OBJ_AMOUNT}</div>");
                            strb.Append($"<div class=\"normal\">合同名称：{OBJ_NAME}</div>");
                            strb.Append($"<div class=\"normal\">处理节点：{wxMsgInfo.Node}</div>");
                            strb.Append($"<div class=\"normal\">处理意见：{wxMsgInfo.Option}</div>");
                            strb.Append($"<div class=\"highlight\">处理结果：{resstr}</div>");

                        }
                        else
                        {
                            SpTitle = "您有一笔开票需要审批";
                        }
                        articleInfo = new article()
                        {

                            description = strb.ToString(),
                            title = SpTitle,
                            url = WxQYHOAuth2Utility.GetAuthorizeUrl(Constant.CorpId, urlId, state: "2017kaipiaodetail"), //"www.baidu.com"

                        };
                        _articles.Add(articleInfo);
                        break;
                    case (int)FlowObjEnums.InvoiceIn:

                        strb.Append($"<div class=\"gray\">{DateTime.Now.ToString("yyyy年MM月dd日")}</div>");
                        strb.Append($"<div class=\"normal\">类别：{OBJ_TYPE}</div>");
                        strb.Append($"<div class=\"normal\">合同名称：{OBJ_NAME}</div>");
                        strb.Append($"<div class=\"normal\">合同编号：{OBJ_NO}</div>");
                        strb.Append($"<div class=\"normal\">收票金额：{OBJ_AMOUNT}</div>");
                       // strb.Append($"<div class=\"highlight\">请您处理！！！</div>");

                       

                        if (wxMsgInfo.MsgType == 0)
                        {//审批通过
                            SpTitle = $"您有一笔收票审批通过";
                        }
                        else if (wxMsgInfo.MsgType == 1)
                        {
                            SpTitle = $"您有一笔收票被打回";
                        }
                        else if (wxMsgInfo.MsgType == 6)
                        {//提醒之前节点
                            SpTitle = $"{wxMsgInfo.AppUser}审批了{wxMsgInfo.StartUser}发起的一笔收票";
                            strb = new StringBuilder();
                            var resstr = wxMsgInfo.AppRest == 0 ? "同意" : "不同意";
                            strb.Append($"<div class=\"normal\">收票金额：{OBJ_AMOUNT}</div>");
                            strb.Append($"<div class=\"normal\">合同名称：{OBJ_NAME}</div>");
                            strb.Append($"<div class=\"normal\">处理节点：{wxMsgInfo.Node}</div>");
                            strb.Append($"<div class=\"normal\">处理意见：{wxMsgInfo.Option}</div>");
                            strb.Append($"<div class=\"highlight\">处理结果：{resstr}</div>");

                        }
                        else
                        {
                            SpTitle = "您有一笔收票需要审批";
                        }
                        articleInfo = new article()
                        {

                            description = strb.ToString(),
                            title = SpTitle,
                            url = WxQYHOAuth2Utility.GetAuthorizeUrl(Constant.CorpId, urlId, state: "2017kaipiaodetail"), //"www.baidu.com"

                        };
                        _articles.Add(articleInfo);
                        break;
                        //case (int)FlowObjEnums.Contract:
                        //    strb.Append("合同名称：" + OBJ_NAME + ",");
                        //    strb.Append("合同编号：" + OBJ_NO + ",");
                        //    strb.Append("合同类别：" + OBJ_TYPE);
                        //    strb.Append("合同金额：" + OBJ_AMOUNT);
                        //    SpTitle = "您有一个合同需要审批";
                        //    articleInfo = new article()
                        //    {

                        //        description = strb.ToString(),
                        //        title = SpTitle,
                        //        url = WxQYHOAuth2Utility.GetAuthorizeUrl(Constant.CorpId, urlId, state: "2017Contractdetail"), //"www.baidu.com"

                        //    };
                        //    _articles.Add(articleInfo);
                        //    break;



                        //Log4netHelper.Info($"消息卡片：" + strb.ToString());
                }
                var _news = new news();
                _news.articles = _articles;

                var MsgNewsInfo = new WxQYHMsgNewsInfo()
                {
                    touser = wxMsgInfo.WxCodes,
                    msgtype = "news",
                    agentid = Constant.Agentid,//企业应用ID：1
                    news = _news

                };
                var PostData = JsonUtility.SerializeObject(MsgNewsInfo).Replace("\\u0026", "&"); ;
                WxQYHMsgManager.WxMsgSend(PostData);

                return "OK";
            }
            catch (Exception ex)
            {
                RedisHelper.ListRightPush("WxMsgList", wxMsgInfo);
                Log4netHelper.Error(ex.Message);

                return "NO";
            }
        }
        #endregion

        #region 到期提醒
        /// <summary>
        /// 到期微信提醒
        /// </summary>
        /// <param name="wxMsgInfo">消息对象</param>
        /// <returns></returns>
        public static string SubmitWxDqMsg(DaoQiMsg wxMsgInfo)
        {
            try
            {
                string AuthUrl = $"{Constant.WxAppBaseURL}/WxRuKou/WxHttpRedirect";
                StringBuilder strb = new StringBuilder();
                string SpTitle = string.Empty;
                var wxappmsg = new TextcardMsgInfo()
                {
                    //touser = wxMsgInfo.WxCodes,
                    msgtype = "textcard",
                    agentid = Constant.Agentid,//企业应用ID：1
                    //textcard = textcard

                };
                switch (wxMsgInfo.MsgCode)
                {
                    case 0://到期合同
                        if (wxMsgInfo.DqHt != null)
                        {
                            AuthUrl = AuthUrl + "?OBJID=" + (wxMsgInfo.DqHt != null ? wxMsgInfo.DqHt.Id : 0);
                            strb.Append($"<div class=\"gray\">{DateTime.Now.ToString("yyyy年MM月dd日")}</div>");
                            strb.Append($"<div class=\"normal\">合同名称：{wxMsgInfo.DqHt.Name}</div>");
                            strb.Append($"<div class=\"normal\">合同编号：{wxMsgInfo.DqHt.No}</div>");
                            strb.Append($"<div class=\"normal\">建立人：{wxMsgInfo.DqHt.CreateUserName}</div>");
                            strb.Append($"<div class=\"normal\">建立时间：{wxMsgInfo.DqHt.CreateDate.ToString("yyyy年MM月dd日")}</div>");
                            strb.Append($"<div class=\"normal\">合同计划截止日期：{wxMsgInfo.DqHt.PlanDate.Value.ToString("yyyy年MM月dd日")}</div>");
                            var fincetype = wxMsgInfo.DqHt.FinType == 0 ? "收款" : "付款";
                            SpTitle = $"您有一份{fincetype}合同即将到达截止日期";

                            wxappmsg.touser = wxMsgInfo.DqHt.WxCode;
                        }
                        
                        break;
                    case 1://到期计划
                        if (wxMsgInfo.DqPlan!=null)
                        {
                            AuthUrl = AuthUrl + "?OBJID=" + (wxMsgInfo.DqPlan != null ? wxMsgInfo.DqPlan.ContId : 0);
                            strb.Append($"<div class=\"gray\">{DateTime.Now.ToString("yyyy年MM月dd日")}</div>");
                            strb.Append($"<div class=\"normal\">合同名称：{wxMsgInfo.DqPlan.ContName}</div>");
                            strb.Append($"<div class=\"normal\">合同编号：{wxMsgInfo.DqPlan.ContNo}</div>");
                            strb.Append($"<div class=\"normal\">计划资金名称：{wxMsgInfo.DqPlan.Name}</div>");
                            strb.Append($"<div class=\"normal\">计划资金金额：{wxMsgInfo.DqPlan.MoneryThond}</div>");
                            strb.Append($"<div class=\"normal\">计划完成日期：{wxMsgInfo.DqPlan.PlanDate.Value.ToString("yyyy/MM/dd")}</div>");
                            var fincetype = wxMsgInfo.DqPlan.FinType == 0 ? "收款" : "付款";
                            SpTitle = $"您有一笔计划{fincetype}即将到期";

                            wxappmsg.touser = wxMsgInfo.DqPlan.WxCode;
                        }
                        
                        break;

                }
               // strb.Append($"<div class=\"highlight\">请您处理！！！</div>");
                TextcardMain textcard = null;
                textcard = new TextcardMain()
                {
                    title = SpTitle,
                    description = strb.ToString(),

                    url = WxQYHOAuth2Utility.GetAuthorizeURL(Constant.CorpId, AuthUrl, agentid: Constant.Agentid, state: "2020Contractdetail"), //"www.baidu.com"

                };
                strb.Append(textcard);
                wxappmsg.textcard = textcard;
                

                var PostData = JsonUtility.SerializeObject(wxappmsg);//.Replace("\\u0026", "&"); 
                //Log4netHelper.Info(PostData);
                WxQYHMsgManager.WxMsgSend(PostData);

                return "OK";
            }
            catch (Exception ex)
            {

                RedisHelper.ListRightPush("DaoQiWxMsgList", wxMsgInfo);
                Log4netHelper.Error($"出现推送失败问题：{ex.Message}");
                return "NO";
            }


        }
        #endregion



        #region 到了指定时间发送待审批条数
        /// <summary>
        /// 到期微信提醒
        /// </summary>
        /// <param name="wxMsgInfo">消息对象</param>
        /// <returns></returns>
        public static string SubmitAppRowsMsg(WxTongZhiInfo wxMsgInfo)
        {
            try
            {
                string AuthUrl = $"{Constant.WxAppBaseURL}/WxRuKou/WxHttpRedirect";
                StringBuilder strb = new StringBuilder();
                string SpTitle = string.Empty;
                var wxappmsg = new TextcardMsgInfo()
                {
                    //touser = wxMsgInfo.WxCodes,
                    msgtype = "textcard",
                    agentid = Constant.Agentid,//企业应用ID：1
                    //textcard = textcard

                };
                wxappmsg.touser = wxMsgInfo.WxCode;
                strb.Append($"<div class=\"normal\">您最近一周有{wxMsgInfo.Rows}条待处理</div>");
                SpTitle = $"您最近一周有{wxMsgInfo.Rows}条待处理";

                //strb.Append($"<div class=\"highlight\">请您处理！！！</div>");
                TextcardMain textcard = null;
                textcard = new TextcardMain()
                {
                    title = SpTitle,
                    description = strb.ToString(),

                    url = WxQYHOAuth2Utility.GetAuthorizeURL(Constant.CorpId, AuthUrl, agentid: Constant.Agentid, state: "NFDaiChuLi")
                    //WxQYHOAuth2Utility.GetAuthorizeURL(Constant.CorpId, AuthUrl, agentid: Constant.Agentid, state: "2020Contractdetail"), //"www.baidu.com"

                };
                strb.Append(textcard);
                wxappmsg.textcard = textcard;


                var PostData = JsonUtility.SerializeObject(wxappmsg);//.Replace("\\u0026", "&"); 
                //Log4netHelper.Info(PostData);
                WxQYHMsgManager.WxMsgSend(PostData);

                return "OK";
            }
            catch (Exception ex)
            {

                RedisHelper.ListRightPush("DaoQiWxMsgList", wxMsgInfo);
                Log4netHelper.Error($"出现推送失败问题：{ex.Message}");
                return "NO";
            }


        }
        #endregion
    }
}
