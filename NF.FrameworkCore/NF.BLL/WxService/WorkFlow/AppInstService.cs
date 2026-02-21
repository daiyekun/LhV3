using Microsoft.EntityFrameworkCore;
using NF.BLL.Extend;
using NF.Common.Extend;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Finance.Enums;
using NF.ViewModel.Models.Utility;
using NF.ViewModel.Models.WeiXinModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NF.BLL
{
    /// <summary>
    /// 微信触发类
    /// </summary>
    public partial class AppInstService
    {
        /// <summary>
        /// 获取当前节点消息
        /// </summary>
        /// <param name="appInst">当前审批实例</param>
        public void WeiXinFlowNodeMsg(AppInst appInst)
        {
            //判断发送过的消息不在发送
            var currnodeinfo =
                this.Db.Set<AppInstNodeInfo>().Where(a => a.InstId == appInst.Id && a.NodeStrId == appInst.CurrentNodeStrId ).FirstOrDefault();
            if ((appInst.AppState == 1 || appInst.AppState == 0) && currnodeinfo != null)
            { //审批中/或者刚提交
                FlowWxMsgInfo wxMsgInfo = new FlowWxMsgInfo();

                //var currNode = this.Db.Set<AppInstNode>().Where(a => a.Id == appInst.CurrentNodeId).FirstOrDefault();

                var userCodes = "";
                IList<int> listUserId = null;
                if (currnodeinfo != null)
                {
                     listUserId = this.Db.Set<AppGroupUser>().Where(a => a.GroupId == currnodeinfo.GroupId&&a.InstId== appInst.Id).Select(a => a.UserId??0).ToArray();
                    var listCodes = this.Db.Set<UserInfor>().Where(a => listUserId.Contains(a.Id)).Select(a => a.WxCode).ToList();
                    userCodes = StringHelper.ArrayString2String2(listCodes);

                   
                }
                

                wxMsgInfo.ObjId = appInst.AppObjId;
                wxMsgInfo.ObjName = appInst.AppObjName;
                wxMsgInfo.ObjNo = appInst.AppObjNo;
                wxMsgInfo.ObjMoney = appInst.AppObjAmount.ThousandsSeparator();
                wxMsgInfo.FlowType = appInst.ObjType;//合同/客户/供应商/
                wxMsgInfo.WxCodes = userCodes;
                switch (wxMsgInfo.FlowType)
                {
                    case (int)FlowObjEnums.Contract:
                        {
                            var htinfo = this.Db.Set<ContractInfo>().Include(a => a.Comp).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();
                            if (htinfo != null)
                            {
                                wxMsgInfo.ObjType = this.Db.GetRedisDataDictionaryValue(htinfo.ContTypeId ?? 0, DataDictionaryEnum.contractType);

                                    //DataDicUtility.GetDicValueToRedis(htinfo.ContTypeId ?? 0, DataDictionaryEnum.contractType);
                                wxMsgInfo.FinceType = EmunUtility.GetDesc(typeof(FinanceTypeEnum), htinfo.FinanceType);
                                wxMsgInfo.HtDf = htinfo.Comp != null ? htinfo.Comp.Name : "";
                                wxMsgInfo.JbJg = this.Db.GetRedisDeptNameValue(htinfo.DeptId ?? -2);
                                    //RedisValueUtility.GetDeptName(htinfo.DeptId ?? -2);
                            }
                            else
                            {
                                wxMsgInfo.ObjType = "未知对象";
                            }

                        }
                        break;
                    case (int)FlowObjEnums.Customer:
                        {
                            var htinfo = this.Db.Set<Company>().Include(a => a.CompClass).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();
                            if (htinfo != null)
                            {
                                wxMsgInfo.ObjType = this.Db.GetRedisDataDictionaryValue(htinfo.CompClassId ?? 0, DataDictionaryEnum.customerType);
                                    //DataDicUtility.GetDicValueToRedis(htinfo.CompClassId ?? 0, DataDictionaryEnum.customerType);
                            }
                            else
                            {
                                wxMsgInfo.ObjType = "未知对象";
                            }
                        }
                        break;
                    case (int)FlowObjEnums.Supplier:
                        {
                            var htinfo = this.Db.Set<Company>().Include(a => a.CompClass).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();
                            if (htinfo != null)
                            {
                                wxMsgInfo.ObjType = this.Db.GetRedisDataDictionaryValue(htinfo.CompClassId ?? 0, DataDictionaryEnum.suppliersType);
                                   // DataDicUtility.GetDicValueToRedis(htinfo.CompClassId ?? 0, DataDictionaryEnum.suppliersType);
                            }
                            else
                            {
                                wxMsgInfo.ObjType = "未知对象";
                            }
                        }
                        break;
                    case (int)FlowObjEnums.Other:
                        {
                            var htinfo = this.Db.Set<Company>().Include(a => a.CompClass).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();
                            if (htinfo != null)
                            {
                                wxMsgInfo.ObjType = this.Db.GetRedisDataDictionaryValue(htinfo.CompClassId ?? 0, DataDictionaryEnum.otherType);
                                    //DataDicUtility.GetDicValueToRedis(htinfo.CompClassId ?? 0, DataDictionaryEnum.otherType);
                            }
                            else
                            {
                                wxMsgInfo.ObjType = "未知对象";
                            }
                        }
                        break;
                    case (int)FlowObjEnums.project://项目
                        {
                            var htinfo = this.Db.Set<ProjectManager>().Include(a => a.Category).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();
                            if (htinfo != null)
                            {
                                wxMsgInfo.ObjType = this.Db.GetRedisDataDictionaryValue(htinfo.CategoryId, DataDictionaryEnum.projectType);
                                    //DataDicUtility.GetDicValueToRedis(htinfo.CategoryId, DataDictionaryEnum.projectType);
                            }
                            else
                            {
                                wxMsgInfo.ObjType = "未知对象";
                            }
                        }
                        break;
                    case (int)FlowObjEnums.payment://付款
                        {
                            var htinfo = this.Db.Set<ContActualFinance>().Include(a => a.Cont).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();
                            if (htinfo != null)
                            {
                                wxMsgInfo.ObjName = htinfo.Cont.Name;
                                wxMsgInfo.ObjNo = htinfo.Cont.Code;
                                wxMsgInfo.ObjMoney = htinfo.AmountMoney.ThousandsSeparator();
                            }
                            else
                            {
                                wxMsgInfo.ObjType = "未知对象";
                            }
                        }
                        break;
                    case (int)FlowObjEnums.InvoiceOut://开票
                        {
                            var htinfo = this.Db.Set<ContInvoice>().Include(a => a.Cont).Include(a => a.Cont).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();

                            if (htinfo != null)
                            {
                                wxMsgInfo.ObjType = this.Db.Set<DataDictionary>().Where(a => a.DtypeNumber == 19 && a.Id == htinfo.InType).FirstOrDefault().Name;
                                wxMsgInfo.ObjName = htinfo.Cont.Name;
                                wxMsgInfo.ObjNo = htinfo.Cont.Code;
                                wxMsgInfo.ObjMoney = htinfo.AmountMoney.ThousandsSeparator();
                            }
                            else
                            {
                                wxMsgInfo.ObjType = "未知对象";
                            }
                        }
                        break;
                    case (int)FlowObjEnums.InvoiceIn://收票
                        {
                            var htinfo = this.Db.Set<ContInvoice>().Include(a => a.Cont).Include(a => a.Cont).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();

                            if (htinfo != null)
                            {
                                wxMsgInfo.ObjType = this.Db.Set<DataDictionary>().Where(a => a.DtypeNumber == 19 && a.Id == htinfo.InType).FirstOrDefault().Name;
                                wxMsgInfo.ObjName = htinfo.Cont.Name;
                                wxMsgInfo.ObjNo = htinfo.Cont.Code;
                                wxMsgInfo.ObjMoney = htinfo.AmountMoney.ThousandsSeparator();
                            }
                            else
                            {
                                wxMsgInfo.ObjType = "未知对象";
                            }
                        }
                        break;
                    default:
                        wxMsgInfo.ObjType = "未知流程类型";
                        break;

                }
                 ExecuteSqlCommand($"update AppInstNodeInfo set SubmitMsg=1 where Id={currnodeinfo.Id}");
                //将数据写入Redis
                RedisHelper.ListRightPush("WxMsgList", wxMsgInfo);
                //发送消息到队列以后添加消息表，未来如果超过2天再次发送
               // AddToAppMsg(appInst, wxMsgInfo, listUserId);
            }
        }
        /// <summary>
        /// 添加消息表
        /// </summary>
        /// <param name="appInst">实例对象</param>
        /// <param name="flowWxMsg">流程消息对象</param>
        /// <param name="listUserId">发送人列表</param>
        public void AddToAppMsg(AppInst appInst, FlowWxMsgInfo flowWxMsg,IList<int> listUserId)
        {
           
            var currnodId = appInst.CurrentNodeId??-1;
            if (currnodId < 0)
            {
                var tempnode = Db.Set<AppInstNode>().Where(a => a.InstId == appInst.Id && a.NodeStrId == appInst.CurrentNodeStrId).FirstOrDefault();
                currnodId = tempnode != null ? tempnode.Id : 0;
            }
            if (listUserId!=null&& listUserId.Count > 0)
            {
                
                AppMsg appMsg = new AppMsg();
                appMsg.InstId = appInst.Id;
                appMsg.MsgType = 0;
                appMsg.MsgRemark = flowWxMsg.ToJson();
                appMsg.NodeId = currnodId;
                appMsg.UserId = listUserId[0];
                appMsg.MsgDate = DateTime.Now;
                appMsg.MsgState = 0;
                appMsg.TxSum = 1;
                var info = Db.Set<AppMsg>()
                    .Where(a => a.InstId == appInst.Id && a.NodeId == currnodId && a.UserId == appMsg.UserId).FirstOrDefault();
                if (info != null)
                {
                    info.MsgDate = DateTime.Now;
                    info.MsgState = 0;
                    info.TxSum = info.TxSum+1;
                    Db.Entry<AppMsg>(info).State = EntityState.Modified;
                }
                else
                {
                    Db.Set<AppMsg>().Add(appMsg);
                }
               
                this.SaveChanges();
            }
            


        }

        /// <summary>
        /// 待处理审批超时提醒
        /// 目前2天。代码写死
        /// </summary>
        public void SearchAppMsg()
        {
            var appday = 2;
            var curryar = DateTime.Now.Year;
            var currm = DateTime.Now.Month;
            var currday = DateTime.Now.Day;
            var listinstIds = Db.Set<AppMsg>()
                .Where(a => a.MsgState == 0 && (a.MsgDate.HasValue 
                && a.MsgDate.Value.AddDays(appday).Year== curryar
                && a.MsgDate.Value.AddDays(appday).Month== currm
                && a.MsgDate.Value.AddDays(appday).Day == currday)).Select(a=>a.InstId??0).ToList();
            var listappinsts = Db.Set<AppInst>().Where(a => listinstIds.Contains(a.Id)).ToList();
            foreach (var inst in listappinsts)
            {
                WeiXinFlowNodeMsg(inst);
            }

        }

        /// <summary>
        /// 删除待处理消息
        /// </summary>
        /// <returns></returns>
        public int ClearMsg(int instId, int nodeId, int userId)
        {

            return ExecuteSqlCommand($"delete AppMsg where InstId={instId} and  NodeId={nodeId} and UserId={userId};");

        }


        /// <summary>
        /// 打回或者通过通知消息
        /// </summary>
        /// <param name="appInst">当前审批实例</param>
        /// <param name="MsgType">0:通过，1：打回</param>
        public void WxDhOrTgMsg(AppInst appInst,int MsgType)
        {
            //判断发送过的消息不在发送
            //var wxcodes = this.Db.Set<UserInfor>()
            //    .Where(a => a.Id == appInst.StartUserId).Select(a => a.WxCode).ToList();
            var option = this.Db.Set<AppInstOpin>().Where(a => a.InstId == appInst.Id)
                .OrderByDescending(a => a.Id).FirstOrDefault();
            var users = this.Db.Set<UserInfor>()
                .Where(a => a.Id == appInst.StartUserId || a.Id == option.CreateUserId).ToList();

            var wxcodes = users
                .Where(a => a.Id == appInst.StartUserId).Select(a => a.WxCode).ToList();
            var appuser = users.Where(a => a.Id == option.CreateUserId).FirstOrDefault();

            if (wxcodes.Count()>0)
            { 
                FlowWxMsgInfo wxMsgInfo = new FlowWxMsgInfo();
                wxMsgInfo.MsgType = MsgType;
                 var userCodes = "";
                userCodes = StringHelper.ArrayString2String2(wxcodes);
                wxMsgInfo.ObjId = appInst.AppObjId;
                wxMsgInfo.ObjName = appInst.AppObjName;
                wxMsgInfo.ObjNo = appInst.AppObjNo;
                wxMsgInfo.ObjMoney = appInst.AppObjAmount.ThousandsSeparator();
                wxMsgInfo.FlowType = appInst.ObjType;//合同/客户/供应商/
                wxMsgInfo.WxCodes = userCodes;
                wxMsgInfo.AppUser = appuser!=null? appuser.DisplyName:"";
                wxMsgInfo.Option = option.Opinion;
                switch (wxMsgInfo.FlowType)
                {
                    case (int)FlowObjEnums.Contract:
                        {
                            var htinfo = this.Db.Set<ContractInfo>().Include(a => a.Comp).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();
                            if (htinfo != null)
                            {
                                wxMsgInfo.ObjType = this.Db.GetRedisDataDictionaryValue(htinfo.ContTypeId ?? 0, DataDictionaryEnum.contractType);
                                   // DataDicUtility.GetDicValueToRedis(htinfo.ContTypeId ?? 0, DataDictionaryEnum.contractType);
                                wxMsgInfo.FinceType = EmunUtility.GetDesc(typeof(FinanceTypeEnum), htinfo.FinanceType);
                                wxMsgInfo.HtDf = htinfo.Comp != null ? htinfo.Comp.Name : "";
                                wxMsgInfo.JbJg = this.Db.GetRedisDeptNameValue(htinfo.DeptId ?? -1);
                                    //RedisValueUtility.GetDeptName(htinfo.DeptId ?? -2);
                            }
                            else
                            {
                                wxMsgInfo.ObjType = "未知对象";
                            }

                        }
                        break;
                    case (int)FlowObjEnums.Customer:
                        {
                            var htinfo = this.Db.Set<Company>().Include(a => a.CompClass).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();
                            if (htinfo != null)
                            {
                                wxMsgInfo.ObjType = this.Db.GetRedisDataDictionaryValue(htinfo.CompClassId ?? 0, DataDictionaryEnum.customerType); 
                                  //  DataDicUtility.GetDicValueToRedis(htinfo.CompClassId ?? 0, DataDictionaryEnum.customerType);
                            }
                            else
                            {
                                wxMsgInfo.ObjType = "未知对象";
                            }
                        }
                        break;
                    case (int)FlowObjEnums.Supplier:
                        {
                            var htinfo = this.Db.Set<Company>().Include(a => a.CompClass).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();
                            if (htinfo != null)
                            {
                                wxMsgInfo.ObjType = this.Db.GetRedisDataDictionaryValue(htinfo.CompClassId ?? 0, DataDictionaryEnum.suppliersType);
                            //    DataDicUtility.GetDicValueToRedis(htinfo.CompClassId ?? 0, DataDictionaryEnum.suppliersType);
                            }
                            else
                            {
                                wxMsgInfo.ObjType = "未知对象";
                            }
                        }
                        break;
                    case (int)FlowObjEnums.Other:
                        {
                            var htinfo = this.Db.Set<Company>().Include(a => a.CompClass).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();
                            if (htinfo != null)
                            {
                                wxMsgInfo.ObjType = this.Db.GetRedisDataDictionaryValue(htinfo.CompClassId ?? 0, DataDictionaryEnum.otherType);
                                //DataDicUtility.GetDicValueToRedis(htinfo.CompClassId ?? 0, DataDictionaryEnum.otherType);
                            }
                            else
                            {
                                wxMsgInfo.ObjType = "未知对象";
                            }
                        }
                        break;
                    case (int)FlowObjEnums.project://项目
                        {
                            var htinfo = this.Db.Set<ProjectManager>().Include(a => a.Category).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();
                            if (htinfo != null)
                            {
                                wxMsgInfo.ObjType = this.Db.GetRedisDataDictionaryValue(htinfo.CategoryId, DataDictionaryEnum.projectType);
                                //DataDicUtility.GetDicValueToRedis(htinfo.CategoryId, DataDictionaryEnum.projectType);
                            }
                            else
                            {
                                wxMsgInfo.ObjType = "未知对象";
                            }
                        }
                        break;
                    case (int)FlowObjEnums.payment://付款
                        {
                            var htinfo = this.Db.Set<ContActualFinance>().Include(a => a.Cont).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();
                            if (htinfo != null)
                            {
                                wxMsgInfo.ObjName = htinfo.Cont.Name;
                                wxMsgInfo.ObjNo = htinfo.Cont.Code;
                                wxMsgInfo.ObjMoney = htinfo.AmountMoney.ThousandsSeparator();
                            }
                            else
                            {
                                wxMsgInfo.ObjType = "未知对象";
                            }
                        }
                        break;
                    case (int)FlowObjEnums.InvoiceOut://开票
                        {
                            var htinfo = this.Db.Set<ContInvoice>().Include(a => a.Cont).Include(a => a.Cont).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();

                            if (htinfo != null)
                            {
                                wxMsgInfo.ObjType = this.Db.Set<DataDictionary>().Where(a => a.DtypeNumber == 19 && a.Id == htinfo.InType).FirstOrDefault().Name;
                                wxMsgInfo.ObjName = htinfo.Cont.Name;
                                wxMsgInfo.ObjNo = htinfo.Cont.Code;
                                wxMsgInfo.ObjMoney = htinfo.AmountMoney.ThousandsSeparator();
                            }
                            else
                            {
                                wxMsgInfo.ObjType = "未知对象";
                            }
                        }
                        break;
                    case (int)FlowObjEnums.InvoiceIn://收票
                        {
                            var htinfo = this.Db.Set<ContInvoice>().Include(a => a.Cont).Include(a => a.Cont).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();

                            if (htinfo != null)
                            {
                                wxMsgInfo.ObjType = this.Db.Set<DataDictionary>().Where(a => a.DtypeNumber == 19 && a.Id == htinfo.InType).FirstOrDefault().Name;
                                wxMsgInfo.ObjName = htinfo.Cont.Name;
                                wxMsgInfo.ObjNo = htinfo.Cont.Code;
                                wxMsgInfo.ObjMoney = htinfo.AmountMoney.ThousandsSeparator();
                            }
                            else
                            {
                                wxMsgInfo.ObjType = "未知对象";
                            }
                        }
                        break;
                    default:
                        wxMsgInfo.ObjType = "未知流程类型";
                        break;

                }
                //ExecuteSqlCommand($"update AppInstNodeInfo set SubmitMsg=1 where Id={currnodeinfo.Id}");
                //将数据写入Redis
                RedisHelper.ListRightPush("WxMsgList", wxMsgInfo);
            }
        }


        /// <summary>
        /// 提醒之前节点审批人员以及发起人员
        /// </summary>
        /// <param name="appInst">当前审批实例</param>
        /// <param name="spres">审批结果 0：同意，1：不同意</param>
        public void WxmsgPrvNode(AppInst appInst,int spres,string jdName)
        {
            var Dqcl = 0;
            //判断发送过的消息不在发送
            var listnodes = this.Db.Set<AppInstNode>().Where(a => a.InstId == appInst.Id).ToList();//查询实例节点
            var option = this.Db.Set<AppInstOpin>().Where(a => a.InstId == appInst.Id).OrderByDescending(a => a.Id).FirstOrDefault();//查询刚刚审批通过人员
            if (option!=null)
            {
                var userCodes = "";
                IList<int> userIds = new List<int>();
                var temlistnode = listnodes.Where(a => a.Id < option.NodeId && a.Type == 2).Select(a=>a.Id).ToList();
               if(temlistnode!=null&& temlistnode.Count > 0)
                {
                    var listUserId = this.Db.Set<AppGroupUser>().Where(a => temlistnode.Contains(a.NodeId??0) && a.InstId == appInst.Id)
                        .Select(a => a.UserId ?? 0).ToList();
                    if (listUserId != null)
                    {
                        userIds = listUserId;
                    }
                }
                userIds.Add(appInst.StartUserId??0);
                List<string> listCodes = null;
                if (spres==1)
                {
                   
                       var jdNames= listnodes.Where(a => a.NodeStrId == option.NodeStrId).FirstOrDefault().Name;

                        if (jdNames == jdName)
                        {
                            listCodes = this.Db.Set<UserInfor>().Where(a => a.Id == appInst.StartUserId).Select(a => a.WxCode).ToList();
                           
                        }
                        else
                        {
                        //select * from AppGroupUser  where NodeStrId=(select  NodeStrId from AppInstNode where id=1031) and InstId=173
                        var jd1id = listnodes.Where(a => a.Name == jdName).FirstOrDefault().NodeStrId;
                        var yhid = Db.Set<AppGroupUser>().Where(a => a.NodeStrId == jd1id && a.InstId == appInst.Id).FirstOrDefault().UserId;
                        listCodes = this.Db.Set<UserInfor>().Where(a => (a.Id == appInst.StartUserId||a.Id== yhid)).Select(a => a.WxCode).ToList();

                    }
                    userCodes = StringHelper.ArrayString2String2(listCodes);//发送消息的微信账号
                }
                else
                {
                    listCodes = this.Db.Set<UserInfor>().Where(a => a.Id == appInst.StartUserId).Select(a => a.WxCode).ToList();
                    userCodes = StringHelper.ArrayString2String2(listCodes);//发送消息的微信账号
                }
                foreach (var item in listCodes)
                {
                    var db = this.Db.Set<UserInfor>().Where(a => a.WxCode == item).FirstOrDefault().Id;
                    if (db== appInst.StartUserId)
                    {

                   
                var spnode = listnodes.Where(a => a.Id == option.NodeId).FirstOrDefault();//审批节点
                FlowWxMsgInfo wxMsgInfo = new FlowWxMsgInfo();
                        wxMsgInfo.StartUser = this.Db.GetRedisUserFieldValue(appInst.StartUserId ?? -1);
                            //RedisValueUtility.GetUserShowName(appInst.StartUserId??0);
                wxMsgInfo.AppUser = this.Db.GetRedisUserFieldValue(appInst.CreateUserId);
                        //RedisValueUtility.GetUserShowName(option.CreateUserId ?? 0);
                wxMsgInfo.Option = option.Opinion;
                wxMsgInfo.Node = spnode != null ? spnode.Name.Replace("（点选）","") : "";
                wxMsgInfo.MsgType = 6;//提醒之前审批人员
                wxMsgInfo.AppRest = spres;//处理结果
                wxMsgInfo.ObjId = appInst.AppObjId;
                wxMsgInfo.ObjName = appInst.AppObjName;
                wxMsgInfo.ObjNo = appInst.AppObjNo;
                wxMsgInfo.ObjMoney = appInst.AppObjAmount.ThousandsSeparator();
                wxMsgInfo.FlowType = appInst.ObjType;//合同/客户/供应商/
                wxMsgInfo.WxCodes = item;
                switch (wxMsgInfo.FlowType)
                {
                    case (int)FlowObjEnums.Contract:
                        {
                            var htinfo = this.Db.Set<ContractInfo>().Include(a => a.Comp).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();
                            if (htinfo != null)
                            {
                                wxMsgInfo.ObjType = this.Db.GetRedisDataDictionaryValue(htinfo.ContTypeId??0, DataDictionaryEnum.contractType);
                                        //DataDicUtility.GetDicValueToRedis(htinfo.ContTypeId ?? 0, DataDictionaryEnum.contractType);
                                wxMsgInfo.FinceType = EmunUtility.GetDesc(typeof(FinanceTypeEnum), htinfo.FinanceType);
                                wxMsgInfo.HtDf = htinfo.Comp != null ? htinfo.Comp.Name : "";
                                wxMsgInfo.JbJg = this.Db.GetRedisDeptNameValue(htinfo.DeptId ?? -2);
                                        //RedisValueUtility.GetDeptName(htinfo.DeptId ?? -2);
                            }
                            else
                            {
                                wxMsgInfo.ObjType = "未知对象";
                            }

                        }
                        break;
                    case (int)FlowObjEnums.Customer:
                        {
                            var htinfo = this.Db.Set<Company>().Include(a => a.CompClass).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();
                            if (htinfo != null)
                            {
                                wxMsgInfo.ObjType = this.Db.GetRedisDataDictionaryValue(htinfo.CompClassId ?? 0, DataDictionaryEnum.customerType);
                                        //DataDicUtility.GetDicValueToRedis(htinfo.CompClassId ?? 0, DataDictionaryEnum.customerType);
                            }
                            else
                            {
                                wxMsgInfo.ObjType = "未知对象";
                            }
                        }
                        break;
                    case (int)FlowObjEnums.Supplier:
                        {
                            var htinfo = this.Db.Set<Company>().Include(a => a.CompClass).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();
                            if (htinfo != null)
                            {
                                wxMsgInfo.ObjType = this.Db.GetRedisDataDictionaryValue(htinfo.CompClassId ?? 0, DataDictionaryEnum.suppliersType);
                                        //DataDicUtility.GetDicValueToRedis(htinfo.CompClassId ?? 0, DataDictionaryEnum.suppliersType);
                                    }
                            else
                            {
                                wxMsgInfo.ObjType = "未知对象";
                            }
                        }
                        break;
                    case (int)FlowObjEnums.Other:
                        {
                            var htinfo = this.Db.Set<Company>().Include(a => a.CompClass).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();
                            if (htinfo != null)
                            {
                                wxMsgInfo.ObjType = this.Db.GetRedisDataDictionaryValue(htinfo.CompClassId ?? 0, DataDictionaryEnum.otherType);
                                        //DataDicUtility.GetDicValueToRedis(htinfo.CompClassId ?? 0, DataDictionaryEnum.otherType);
                            }
                            else
                            {
                                wxMsgInfo.ObjType = "未知对象";
                            }
                        }
                        break;
                    case (int)FlowObjEnums.project://项目
                        {
                            var htinfo = this.Db.Set<ProjectManager>().Include(a => a.Category).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();
                            if (htinfo != null)
                            {
                                wxMsgInfo.ObjType = this.Db.GetRedisDataDictionaryValue(htinfo.CategoryId, DataDictionaryEnum.projectType);
                                        //DataDicUtility.GetDicValueToRedis(htinfo.CategoryId, DataDictionaryEnum.projectType);
                                    }
                            else
                            {
                                wxMsgInfo.ObjType = "未知对象";
                            }
                        }
                        break;
                    case (int)FlowObjEnums.payment://付款
                        {
                            var htinfo = this.Db.Set<ContActualFinance>().Include(a => a.Cont).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();
                            if (htinfo != null)
                            {
                                wxMsgInfo.ObjName = htinfo.Cont.Name;
                                wxMsgInfo.ObjNo = htinfo.Cont.Code;
                                wxMsgInfo.ObjMoney = htinfo.AmountMoney.ThousandsSeparator();
                            }
                            else
                            {
                                wxMsgInfo.ObjType = "未知对象";
                            }
                        }
                        break;
                    case (int)FlowObjEnums.InvoiceOut://开票
                        {
                            var htinfo = this.Db.Set<ContInvoice>().Include(a => a.Cont).Include(a => a.Cont).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();

                            if (htinfo != null)
                            {
                                wxMsgInfo.ObjType = this.Db.Set<DataDictionary>().Where(a => a.DtypeNumber == 19 && a.Id == htinfo.InType).FirstOrDefault().Name;
                                wxMsgInfo.ObjName = htinfo.Cont.Name;
                                wxMsgInfo.ObjNo = htinfo.Cont.Code;
                                wxMsgInfo.ObjMoney = htinfo.AmountMoney.ThousandsSeparator();
                            }
                            else
                            {
                                wxMsgInfo.ObjType = "未知对象";
                            }
                        }
                        break;
                    case (int)FlowObjEnums.InvoiceIn://收票
                        {
                            var htinfo = this.Db.Set<ContInvoice>().Include(a => a.Cont).Include(a => a.Cont).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();

                            if (htinfo != null)
                            {
                                wxMsgInfo.ObjType = this.Db.Set<DataDictionary>().Where(a => a.DtypeNumber == 19 && a.Id == htinfo.InType).FirstOrDefault().Name;
                                wxMsgInfo.ObjName = htinfo.Cont.Name;
                                wxMsgInfo.ObjNo = htinfo.Cont.Code;
                                wxMsgInfo.ObjMoney = htinfo.AmountMoney.ThousandsSeparator();
                            }
                            else
                            {
                                wxMsgInfo.ObjType = "未知对象";
                            }
                        }
                        break;
                    default:
                        wxMsgInfo.ObjType = "未知流程类型";
                        break;

                }
                
                
                //将数据写入Redis
                RedisHelper.ListRightPush("WxMsgList", wxMsgInfo);
                    }
                    else
                    {
                        var spnode = listnodes.Where(a => a.Id == option.NodeId).FirstOrDefault();//审批节点
                        FlowWxMsgInfo wxMsgInfo = new FlowWxMsgInfo();
                        wxMsgInfo.StartUser = this.Db.GetRedisUserFieldValue(appInst.StartUserId ?? -1);
                            //RedisValueUtility.GetUserShowName(appInst.StartUserId ?? 0);
                        wxMsgInfo.AppUser = this.Db.GetRedisUserFieldValue(appInst.StartUserId ?? -1);
                        //RedisValueUtility.GetUserShowName(option.CreateUserId ?? 0);
                        wxMsgInfo.Option = option.Opinion;
                        wxMsgInfo.Node = spnode != null ? spnode.Name.Replace("（点选）", "") : "";
                        wxMsgInfo.MsgType = 1;//提醒之前审批人员
                        wxMsgInfo.AppRest = spres;//处理结果
                        wxMsgInfo.ObjId = appInst.AppObjId;
                        wxMsgInfo.ObjName = appInst.AppObjName;
                        wxMsgInfo.ObjNo = appInst.AppObjNo;
                        wxMsgInfo.ObjMoney = appInst.AppObjAmount.ThousandsSeparator();
                        wxMsgInfo.FlowType = appInst.ObjType;//合同/客户/供应商/
                        wxMsgInfo.WxCodes = item;
                        switch (wxMsgInfo.FlowType)
                        {
                            case (int)FlowObjEnums.Contract:
                                {
                                    var htinfo = this.Db.Set<ContractInfo>().Include(a => a.Comp).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();
                                    if (htinfo != null)
                                    {
                                        wxMsgInfo.ObjType = this.Db.GetRedisDataDictionaryValue(htinfo.ContTypeId ?? 0, DataDictionaryEnum.contractType);

                                        //DataDicUtility.GetDicValueToRedis(htinfo.ContTypeId ?? 0, DataDictionaryEnum.contractType);
                                        wxMsgInfo.FinceType = EmunUtility.GetDesc(typeof(FinanceTypeEnum), htinfo.FinanceType);
                                        wxMsgInfo.HtDf = htinfo.Comp != null ? htinfo.Comp.Name : "";
                                        wxMsgInfo.JbJg = this.Db.GetRedisDeptNameValue(htinfo.DeptId ?? -2);

                                            //RedisValueUtility.GetDeptName(htinfo.DeptId ?? -2);
                                    }
                                    else
                                    {
                                        wxMsgInfo.ObjType = "未知对象";
                                    }

                                }
                                break;
                            case (int)FlowObjEnums.Customer:
                                {
                                    var htinfo = this.Db.Set<Company>().Include(a => a.CompClass).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();
                                    if (htinfo != null)
                                    {
                                        wxMsgInfo.ObjType = this.Db.GetRedisDataDictionaryValue(htinfo.CompClassId ?? 0, DataDictionaryEnum.customerType);
                                         //   DataDicUtility.GetDicValueToRedis(htinfo.CompClassId ?? 0, DataDictionaryEnum.customerType);
                                    }
                                    else
                                    {
                                        wxMsgInfo.ObjType = "未知对象";
                                    }
                                }
                                break;
                            case (int)FlowObjEnums.Supplier:
                                {
                                    var htinfo = this.Db.Set<Company>().Include(a => a.CompClass).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();
                                    if (htinfo != null)
                                    {
                                        wxMsgInfo.ObjType = this.Db.GetRedisDataDictionaryValue(htinfo.CompClassId ?? 0, DataDictionaryEnum.suppliersType);
                                       // DataDicUtility.GetDicValueToRedis(htinfo.CompClassId ?? 0, DataDictionaryEnum.suppliersType);
                                    }
                                    else
                                    {
                                        wxMsgInfo.ObjType = "未知对象";
                                    }
                                }
                                break;
                            case (int)FlowObjEnums.Other:
                                {
                                    var htinfo = this.Db.Set<Company>().Include(a => a.CompClass).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();
                                    if (htinfo != null)
                                    {
                                        wxMsgInfo.ObjType = this.Db.GetRedisDataDictionaryValue(htinfo.CompClassId ?? 0, DataDictionaryEnum.otherType);
                                        //DataDicUtility.GetDicValueToRedis(htinfo.CompClassId ?? 0, DataDictionaryEnum.otherType);
                                    }
                                    else
                                    {
                                        wxMsgInfo.ObjType = "未知对象";
                                    }
                                }
                                break;
                            case (int)FlowObjEnums.project://项目
                                {
                                    var htinfo = this.Db.Set<ProjectManager>().Include(a => a.Category).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();
                                    if (htinfo != null)
                                    {
                                        wxMsgInfo.ObjType = this.Db.GetRedisDataDictionaryValue(htinfo.CategoryId, DataDictionaryEnum.projectType);
                                        //DataDicUtility.GetDicValueToRedis(htinfo.CategoryId, DataDictionaryEnum.projectType);
                                    }
                                    else
                                    {
                                        wxMsgInfo.ObjType = "未知对象";
                                    }
                                }
                                break;
                            case (int)FlowObjEnums.payment://付款
                                {
                                    var htinfo = this.Db.Set<ContActualFinance>().Include(a => a.Cont).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();
                                    if (htinfo != null)
                                    {
                                        wxMsgInfo.ObjName = htinfo.Cont.Name;
                                        wxMsgInfo.ObjNo = htinfo.Cont.Code;
                                        wxMsgInfo.ObjMoney = htinfo.AmountMoney.ThousandsSeparator();
                                    }
                                    else
                                    {
                                        wxMsgInfo.ObjType = "未知对象";
                                    }
                                }
                                break;
                            case (int)FlowObjEnums.InvoiceOut://开票
                                {
                                    var htinfo = this.Db.Set<ContInvoice>().Include(a => a.Cont).Include(a => a.Cont).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();

                                    if (htinfo != null)
                                    {
                                        wxMsgInfo.ObjType = this.Db.Set<DataDictionary>().Where(a => a.DtypeNumber == 19 && a.Id == htinfo.InType).FirstOrDefault().Name;
                                        wxMsgInfo.ObjName = htinfo.Cont.Name;
                                        wxMsgInfo.ObjNo = htinfo.Cont.Code;
                                        wxMsgInfo.ObjMoney = htinfo.AmountMoney.ThousandsSeparator();
                                    }
                                    else
                                    {
                                        wxMsgInfo.ObjType = "未知对象";
                                    }
                                }
                                break;
                            case (int)FlowObjEnums.InvoiceIn://收票
                                {
                                    var htinfo = this.Db.Set<ContInvoice>().Include(a => a.Cont).Include(a => a.Cont).Where(a => a.Id == appInst.AppObjId).FirstOrDefault();

                                    if (htinfo != null)
                                    {
                                        wxMsgInfo.ObjType = this.Db.Set<DataDictionary>().Where(a => a.DtypeNumber == 19 && a.Id == htinfo.InType).FirstOrDefault().Name;
                                        wxMsgInfo.ObjName = htinfo.Cont.Name;
                                        wxMsgInfo.ObjNo = htinfo.Cont.Code;
                                        wxMsgInfo.ObjMoney = htinfo.AmountMoney.ThousandsSeparator();
                                    }
                                    else
                                    {
                                        wxMsgInfo.ObjType = "未知对象";
                                    }
                                }
                                break;
                            default:
                                wxMsgInfo.ObjType = "未知流程类型";
                                break;

                        }


                        //将数据写入Redis
                        RedisHelper.ListRightPush("WxMsgList", wxMsgInfo);
                    }
                }

            }
        }

        /// <summary>
        /// 判断当前人员流程权限
        /// </summary>
        /// <param name="flowPerm">审批对象
        ///</param>
        ///<returns>返回对象</returns>
        public WorFlowPerssion GetFlowPermission(FlowPerm flowPerm)
        {
            var IsSp = 0;//审批标识
            var spId = flowPerm.SpId;
            var spType = flowPerm.SpType;
            var wfItem = flowPerm.WfItem;
            var userId = flowPerm.UserId;
            WorFlowPerssion flowPerssion = new WorFlowPerssion();
            var appInst = this.Db.Set<AppInst>()
                .Where(a => a.AppObjId == spId && a.ObjType == spType && a.Mission == wfItem).OrderByDescending(a => a.Id).FirstOrDefault();
            if (appInst != null)
            {
                var instNode = this.Db.Set<AppInstNode>()
                    .Where(a => a.InstId == appInst.Id && a.NodeState == 1).FirstOrDefault();

                if (instNode != null)
                {
                    var myUser = this.Db.Set<UserInfor>().Where(a => a.WxCode == userId).FirstOrDefault();
                    var isspyj = this.Db.Set<AppInstOpin>().Any(a => a.InstId == appInst.Id && a.NodeId == instNode.Id && (myUser != null && (a.CreateUserId ?? 0) == myUser.Id));
                    if (!isspyj)
                    {//当前节点我没有审批过才判断

                        var groupIds = this.Db.Set<AppInstNodeInfo>()
                        .Where(a => a.NodeStrId == instNode.NodeStrId && a.InstId == appInst.Id).Select(a => a.GroupId).ToArray();
                        var wfUserIds = this.Db.Set<AppGroupUser>()
                            .Where(a => a.NodeStrId == instNode.NodeStrId && groupIds.Contains(a.GroupId)).Select(a => a.UserId).ToArray();

                        if (myUser != null && wfUserIds.Contains(myUser.Id))
                        {
                            IsSp = 1;
                        }
                    }
                    flowPerssion.InstId = appInst.Id;
                    flowPerssion.NodeStrId = instNode.NodeStrId;
                    flowPerssion.NodeId = instNode.Id;

                }



            }

            flowPerssion.Qx = IsSp;
            this.Db.Dispose();
            return flowPerssion;

        }
        /// <summary>
        /// 查询 审批意见情况
        /// </summary>
        /// <param name="flowPerm"></param>
        public IList<WfNodeView> GetFlowOptions(FlowPerm flowPerm)
        {
            IList<WfNodeView> wfNodeViews = new List<WfNodeView>();
            var appInst = this.Db.Set<AppInst>()
               .Where(a => a.AppObjId == flowPerm.SpId && a.ObjType == flowPerm.SpType && a.Mission == flowPerm.WfItem).FirstOrDefault();
            if (appInst != null)
            {
                var instNodes = this.Db.Set<AppInstNode>().Where(a => a.InstId == appInst.Id && a.Type == 2).ToList().OrderBy(a => a.Id);
                var options = this.Db.Set<AppInstOpin>().Where(a => a.InstId == appInst.Id).ToList();//当前实例所有审批意见
                foreach (var node in instNodes)
                {
                    WfNodeView wfNode = new WfNodeView();
                    wfNode.Nid = node.Id;
                    wfNode.Nc = node.Name;
                    wfNode.Spst = node.NodeState;
                    wfNode.Insst = appInst.AppState;//审批实例状态
                    var currnodeoptions = options.Where(a => a.NodeId == node.Id).ToList();
                    if (currnodeoptions.Count > 0)
                    {
                        IList<OptionMsg> optionMsgs = new List<OptionMsg>();
                        foreach (var opt in currnodeoptions)
                        {
                            OptionMsg msg = new OptionMsg();
                            msg.Uid = opt.CreateUserId ?? 0;
                            msg.Xm = this.Db.GetRedisUserFieldValue(opt.CreateUserId ?? -1);
                                //RedisValueUtility.GetUserShowName(opt.CreateUserId ?? 0);
                            msg.Yj = opt.Opinion;
                            msg.Sj = opt.CreateDatetime.ConvertDate();
                            optionMsgs.Add(msg);

                        }
                        wfNode.Options = optionMsgs;
                    }
                    else
                    {
                        wfNode.Options = new List<OptionMsg>();
                    }
                    wfNodeViews.Add(wfNode);

                }

            }
            return wfNodeViews;



        }

        #region 待处理
        /// <summary>
        /// 待处理-参数介绍见接口
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<WxAppSp> GetAppWxDclList<s>(PageInfo<AppInst> pageInfo, int sessionUserId, Expression<Func<AppInst, bool>> whereLambda, Expression<Func<AppInst, s>> orderbyLambda, bool isAsc)
        {

            var predicateAnd = PredicateBuilder.True<AppInst>();
            predicateAnd = predicateAnd.And(whereLambda);
            var groupIds = Db.Set<GroupUser>().Where(a => a.UserId == sessionUserId).AsNoTracking().Select(a => a.GroupId).ToList();

            var querynode = Db.Set<AppInstNodeInfo>().Where(a => groupIds.Contains(a.GroupId) && a.NodeState == 1);
            var queryoption = Db.Set<AppInstOpin>().AsNoTracking().Where(a => a.CreateUserId == sessionUserId);
            var query0 = from n in querynode
                         join g in queryoption
                         on n.InstId equals g.InstId

                         select new
                         {
                             g.InstId
                            ,
                             g.NodeStrId
                         };
            var apparr = query0.ToList();
            var arraynodes = querynode.ToList();
            IList<int> tempIds = new List<int>();
            foreach (var item in arraynodes)
            {
                if (!apparr.Any(a => a.InstId == item.InstId && a.NodeStrId == item.NodeStrId))
                {
                    tempIds.Add(item.InstId ?? 0);
                }

            }
            predicateAnd = predicateAnd.And(a => (tempIds.Any(c => c == a.Id)));
            var tempquery = _AppInstSet.AsTracking().Where(predicateAnd.Compile()).AsQueryable();
            tempquery = tempquery.OrderByDescending(a => a.Id);
            pageInfo.TotalCount = tempquery.Count();

            tempquery = tempquery.Skip<AppInst>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<AppInst>(pageInfo.PageSize);

            var query = from a in tempquery
                            // where a.Id==124
                        select new
                        {
                            Id = a.Id,
                            AppObjId = a.AppObjId,//
                            AppObjName = a.AppObjName,//
                            AppObjNo = a.AppObjNo,//
                            AppObjAmount = a.AppObjAmount,//
                            Mission = a.Mission,//
                            StartDateTime = a.StartDateTime,//
                            ObjType = a.ObjType
                        };
            var local = from a in query.AsEnumerable()
                        select new WxAppSp
                        {
                            Id = a.Id,
                            AppObjId = a.AppObjId,
                            AppObjName = a.AppObjName,
                            AppObjNo = a.AppObjNo,
                            Mission = a.Mission,
                            StartDateTime = a.StartDateTime,
                            ObjType = a.ObjType,
                            MissionDic = FlowUtility.GetMessionDic(a.Mission ?? -1, a.ObjType),//审批事项
                            AppObjAmount = a.AppObjAmount ?? 0,
                            AppObjAmountThod = a.AppObjAmount.ThousandsSeparator(),//


                        };
            var datalist = local.ToList();
            return new LayPageInfo<WxAppSp>()
            {
                data = datalist,
                count = pageInfo.TotalCount,
                code = 0


            };
        }
        #endregion

        #region 已处理
        /// <summary>
        /// 已处理-参数介绍见接口
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<WxAppSp> WxYcl<s>(PageInfo<AppInst> pageInfo, int sessionUserId, Expression<Func<AppInst, bool>> whereLambda, Expression<Func<AppInst, s>> orderbyLambda, bool isAsc)
        {
            try
            {
                var tempquery = _AppInstSet.AsTracking().Where<AppInst>(whereLambda.Compile()).AsQueryable();
                tempquery = tempquery.OrderByDescending(a => a.Id);
                pageInfo.TotalCount = tempquery.Count();
                tempquery = tempquery.Skip((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take(pageInfo.PageSize);
                var query = from a in tempquery
                            select new
                            {
                                Id = a.Id,
                                AppObjId = a.AppObjId,//
                                AppObjName = a.AppObjName,//
                                AppObjNo = a.AppObjNo,//
                                AppObjAmount = a.AppObjAmount,//
                                Mission = a.Mission,//
                                StartDateTime = a.StartDateTime,//
                                ObjType = a.ObjType
                            };
                var local = from a in query.AsEnumerable()
                            select new WxAppSp
                            {
                                Id = a.Id,
                                ObjType = a.ObjType,
                                ObjTypeDic = EmunUtility.GetDesc(typeof(FlowObjEnums), a.ObjType),
                                AppObjId = a.AppObjId,
                                AppObjName = a.AppObjName,
                                AppObjNo = a.AppObjNo,
                                Mission = a.Mission,
                                MissionDic = FlowUtility.GetMessionDic(a.Mission ?? -1, a.ObjType),//审批事项
                                StartDateTime = a.StartDateTime,
                                AppObjAmount = a.AppObjAmount ?? 0,
                                AppObjAmountThod = a.AppObjAmount.ThousandsSeparator(),
                            };
                return new LayPageInfo<WxAppSp>()
                {
                    data = local.ToList(),
                    count = pageInfo.TotalCount,
                    code = 0
                };
            }
            catch (Exception ex)
            {

                Log4netHelper.Error(ex.Message);
                return new LayPageInfo<WxAppSp>()
                {
                    data = null,
                    count = 0,
                    code = 0
                };
            }
        }
        #endregion
        #region 我审批列表

        /// <summary>
        /// 我审批数据（通过-打回）
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <param name="wosp">1：已通过，0：被打回</param>
        /// <returns></returns>
        public LayPageInfo<WxAppSp> GetWxWtgList<s>(PageInfo<AppInst> pageInfo, int sessionUserId, Expression<Func<AppInst, bool>> whereLambda, Expression<Func<AppInst, s>> orderbyLambda, bool isAsc, int wosp)
        {
            //var predicateAnd = PredicateBuilder.True<AppInst>();
            //predicateAnd = predicateAnd.And(whereLambda);

            var tempquery = _AppInstSet.AsTracking().Where(whereLambda.Compile());
            tempquery = tempquery.OrderByDescending(a => a.Id);
            var queryoption = Db.Set<AppInstOpin>().AsNoTracking().Where(a => a.CreateUserId == sessionUserId);
            if (wosp == 0)
            {//被打回查询
                queryoption = queryoption.Where(a => a.Result == 5);
            }
            else if (wosp == 1)
            {
                queryoption = queryoption.Where(a => a.Result == 2 || a.Result == 4);
            }

            var query = from a in tempquery
                        join
                        b in queryoption
                        on a.Id equals b.InstId
                        select new
                        {
                            Id = a.Id,
                            AppObjId = a.AppObjId,//
                            AppObjName = a.AppObjName,//
                            AppObjNo = a.AppObjNo,//
                            AppObjAmount = a.AppObjAmount,//
                            Mission = a.Mission,//
                            StartDateTime = a.StartDateTime,//
                            ObjType = a.ObjType




                        };
            //pageInfo.TotalCount = query.Count();
            pageInfo.TotalCount = query.Count();
            var query1 = query.OrderByDescending(a => a.Id).Skip((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take(pageInfo.PageSize);
            var local = from a in query1.AsEnumerable()
                        select new WxAppSp
                        {
                            Id = a.Id,
                            ObjType = a.ObjType,
                            ObjTypeDic = EmunUtility.GetDesc(typeof(FlowObjEnums), a.ObjType),
                            AppObjId = a.AppObjId,
                            AppObjName = a.AppObjName,
                            AppObjNo = a.AppObjNo,
                            Mission = a.Mission,
                            MissionDic = FlowUtility.GetMessionDic(a.Mission ?? -1, a.ObjType),//审批事项
                            StartDateTime = a.StartDateTime,
                            AppObjAmount = a.AppObjAmount ?? 0,
                            AppObjAmountThod = a.AppObjAmount.ThousandsSeparator(),
                        };
            var sd = local.ToList();
            return new LayPageInfo<WxAppSp>()
            {
                data = sd,
                count = pageInfo.TotalCount,
                code = 0


            };
        }
        #endregion
        #region 已发起
        /// <summary>
        /// 已发起-参数介绍见接口
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<WxAppSp> GetYfqList<s>(PageInfo<AppInst> pageInfo, Expression<Func<AppInst, bool>> whereLambda, Expression<Func<AppInst, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = _AppInstSet.AsTracking().Where(whereLambda.Compile());
            tempquery = tempquery.OrderByDescending(a => a.Id);
            pageInfo.TotalCount = tempquery.Count();
            tempquery = tempquery.Skip<AppInst>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<AppInst>(pageInfo.PageSize);

            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            AppObjId = a.AppObjId,//
                            AppObjName = a.AppObjName,//
                            AppObjNo = a.AppObjNo,//
                            AppObjAmount = a.AppObjAmount,//
                            Mission = a.Mission,//
                            StartDateTime = a.StartDateTime,//
                            ObjType = a.ObjType
                        };
            var local = from a in query.AsEnumerable()
                        select new WxAppSp
                        {
                            Id = a.Id,
                            AppObjId = a.AppObjId,
                            AppObjName = a.AppObjName,
                            AppObjNo = a.AppObjNo,
                            Mission = a.Mission,
                            StartDateTime = a.StartDateTime,
                            MissionDic = FlowUtility.GetMessionDic(a.Mission ?? -1, a.ObjType),//审批事项
                            AppObjAmount = a.AppObjAmount ?? 0,
                            AppObjAmountThod = a.AppObjAmount.ThousandsSeparator(),//
                        };
            return new LayPageInfo<WxAppSp>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0
            };
        }
        #endregion
        #region 被打回 

        /// <summary>
        /// 被打回-参数介绍见接口
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<WxAppSp> GetBdhList<s>(PageInfo<AppInst> pageInfo, Expression<Func<AppInst, bool>> whereLambda, Expression<Func<AppInst, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = _AppInstSet.AsTracking().Where(whereLambda.Compile());
            tempquery = tempquery.OrderByDescending(a => a.Id);
            tempquery = tempquery.Skip<AppInst>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<AppInst>(pageInfo.PageSize);
            pageInfo.TotalCount = tempquery.Count();
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            AppObjId = a.AppObjId,//
                            AppObjName = a.AppObjName,//
                            AppObjNo = a.AppObjNo,//
                            AppObjAmount = a.AppObjAmount,//
                            Mission = a.Mission,//
                            StartDateTime = a.StartDateTime,//
                            ObjType = a.ObjType
                        };
            var local = from a in query.AsEnumerable()
                        select new WxAppSp
                        {
                            Id = a.Id,
                            AppObjId = a.AppObjId,
                            AppObjName = a.AppObjName,
                            AppObjNo = a.AppObjNo,
                            Mission = a.Mission,
                            StartDateTime = a.StartDateTime,
                            MissionDic = FlowUtility.GetMessionDic(a.Mission ?? -1, a.ObjType),//审批事项
                            AppObjAmount = a.AppObjAmount ?? 0,
                            AppObjAmountThod = a.AppObjAmount.ThousandsSeparator(),//
                        };
            return new LayPageInfo<WxAppSp>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }
        #endregion

        #region 审批历史
        /// <summary>
        /// 审批历史
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="sessionUserId"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>

        public LayPageInfo<WxApproveHistList> GetWXAppHistList<s>(PageInfo<AppInst> pageInfo, int sessionUserId, Expression<Func<AppInst, bool>> whereLambda, Expression<Func<AppInst, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = _AppInstSet.AsTracking().Where(whereLambda.Compile()).AsQueryable();
            try
            {

                pageInfo.TotalCount = tempquery.Count();
            }
            catch (Exception ex)
            {


            }
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<AppInst>))
            { //分页
                tempquery = tempquery.Skip<AppInst>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<AppInst>(pageInfo.PageSize);
            }

            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Mission = a.Mission,
                            CurrentNodeName = a.CurrentNodeName,
                            StartDateTime = a.StartDateTime,
                            AppState = a.AppState,
                            StartUserId = a.StartUserId,
                            ObjType = a.ObjType,
                            CompleteDateTime = a.CompleteDateTime


                        };
            var local = from a in query.AsEnumerable()
                        select new WxApproveHistList
                        {
                            Id = a.Id,
                            Mission = a.Mission,
                            MissionDic = FlowUtility.GetMessionDic(a.Mission ?? -1, a.ObjType),//审批事项
                            CurrentNodeName = a.CurrentNodeName,
                            StartDateTime = a.StartDateTime,
                            AppState = a.AppState,
                            AppStateDic = EmunUtility.GetDesc(typeof(AppInstEnum), a.AppState),
                            StartUserName = this.Db.GetRedisUserFieldValue(a.StartUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.StartUserId ?? 0),
                            CompleteDateTime = a.CompleteDateTime
                        };
            return new LayPageInfo<WxApproveHistList>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }
        #endregion


        #region 查询时间轴
        public IList<WooFlowTime> GetFlowTime(int InstId)
        {
            IList<WooFlowTime> wooFlows = new List<WooFlowTime>();
            var instInfo = _AppInstSet.AsTracking().Where(a => a.Id == InstId).FirstOrDefault();
            if (instInfo!=null) {
             //审批节点
            var listnodes = Db.Set<AppInstNode>().Where(a => a.InstId == InstId).ToList();
            //var listappGruser = Db.Set<AppGroupUser>().Where(a => a.InstId == InstId).ToList();
            var listoptions= Db.Set<AppInstOpin>().Where(a => a.InstId == InstId).ToList();
            
           
           
                foreach (var node in listnodes)
                {
                    var flowtimeinfo = new WooFlowTime();
                    flowtimeinfo.Name = node.Name;
                    if (node.Type==0)
                    {
                        //IList<Option> options = new List<Option>();
                        var opiton = new Option() {
                            OpTime = instInfo.StartDateTime,
                            UserName = this.Db.GetRedisUserFieldValue(instInfo.StartUserId ?? -1),
                            //RedisValueUtility.GetUserShowName(instInfo.StartUserId ?? -1), //发起人
                            YuJian = "已发起"
                        };
                        

                        flowtimeinfo.Tstate = 1;
                        flowtimeinfo.Options = new List<Option>(){
                         opiton
                        };

                    }
                    else if (node.Type==1)
                    {
                        
                        var opiton = new Option()
                        {
                            OpTime = node.ReceDateTime,
                           
                            YuJian = "审批完毕"
                        };


                        flowtimeinfo.Tstate = (int)(node.Marked ?? 0);
                        flowtimeinfo.Options = new List<Option>(){
                         opiton
                        };

                        flowtimeinfo.Tstate = (int)(node.Marked ?? 0);
                       
                    }
                    else
                    {
                        var opts = listoptions.Where(a => a.NodeId == node.Id).ToList();
                        IList<Option> options = new List<Option>();
                        foreach (var opitem in opts)
                        {
                            var tmoption = new Option();
                            tmoption.OpTime = opitem.CreateDatetime;
                            tmoption.YuJian = opitem.Opinion;
                            tmoption.UserName = this.Db.GetRedisUserFieldValue(opitem.CreateUserId ?? -1);
                                //RedisValueUtility.GetUserShowName(opitem.CreateUserId ?? -1); //审批人
                            options.Add(tmoption);
                        }
                        
                        flowtimeinfo.Tstate =node.NodeState;
                        flowtimeinfo.Options = options;
                    }
                    wooFlows.Add(flowtimeinfo);


                }
            
            




           
             
            }



            return wooFlows;



        }


        /// <summary>
        /// 查询 审批意见情况
        /// 目前正在使用
        /// </summary>
        /// <param name="flowPerm"></param>
        public IList<WooFlowTime> GetFlowTime(FlowPerm flowPerm)
        {
           

            IList<WooFlowTime> wooFlows = new List<WooFlowTime>();
            var instInfo = this.Db.Set<AppInst>()
              .Where(a => a.AppObjId == flowPerm.SpId && a.ObjType == flowPerm.SpType).OrderByDescending(a=>a.Id).FirstOrDefault();
            if (instInfo != null)
            {
                //审批节点
                var listnodes = Db.Set<AppInstNode>().Where(a => a.InstId == instInfo.Id).OrderBy(a=>a.Top).ToList();
                var listappGruser = Db.Set<AppGroupUser>().Where(a => a.InstId == instInfo.Id).ToList();
                var listoptions = Db.Set<AppInstOpin>().Where(a => a.InstId == instInfo.Id).ToList();

                foreach (var node in listnodes)
                {
                    var flowtimeinfo = new WooFlowTime();
                    flowtimeinfo.Name = node.Name.Replace("（点选）","");
                    if (node.Type == 0)
                    {
                        //IList<Option> options = new List<Option>();
                        var opiton = new Option()
                        {
                            OpTime = instInfo.StartDateTime,
                            UserName = this.Db.GetRedisUserFieldValue(instInfo.StartUserId ?? -1),
                        //RedisValueUtility.GetUserShowName(instInfo.StartUserId ?? -1), //发起人
                        YuJian = "已发起"
                        };

                        flowtimeinfo.Sta = "(已发起)";
                        flowtimeinfo.Tstate = 2;
                        flowtimeinfo.Options = new List<Option>(){
                         opiton
                        };

                    }
                    else if (node.Type == 1)
                    {

                        var opiton = new Option()
                        {
                            OpTime = node.ReceDateTime,
                            UserName = "",
                            YuJian = "审批完毕"
                        };


                       
                        flowtimeinfo.Options = new List<Option>(){
                         opiton
                        };

                        flowtimeinfo.Tstate = (int)(node.Marked ?? 0);
                        flowtimeinfo.Sta = flowtimeinfo.Tstate>0?"(审批完毕)":"";

                    }
                    else
                    {
                       
                        
                        var opts = listoptions.Where(a => a.NodeId == node.Id).ToList();
                        IList<Option> options = new List<Option>();
                        if (opts.Count > 0)
                        {
                            
                            foreach (var opitem in opts)
                            {
                                var tmoption = new Option();
                                tmoption.OpTime = opitem.CreateDatetime;
                                tmoption.YuJian = opitem.Opinion;
                                tmoption.UserName = this.Db.GetRedisUserFieldValue(opitem.CreateUserId ?? -1);
                                  //  RedisValueUtility.GetUserShowName(opitem.CreateUserId ?? -1); //审批人
                                options.Add(tmoption);
                            }

                        }
                        else
                        {
                            var spusers = listappGruser
                                .Where(a => a.NodeId == node.Id).Select(a=>a.UserId).Distinct().ToList();
                            foreach (var uid in spusers)
                            {
                                var tmoption = new Option();
                                tmoption.UserName = this.Db.GetRedisUserFieldValue(uid ?? -1);
                                //RedisValueUtility.GetUserShowName(uid ?? -1); //审批人
                                options.Add(tmoption);
                            }

                        }
                        

                        flowtimeinfo.Tstate = node.NodeState;
                        flowtimeinfo.Options = options;
                        flowtimeinfo.Sta = GetSta(node.NodeState);
                    }
                    wooFlows.Add(flowtimeinfo);


                }








            }



            return wooFlows;



        }
        /// <summary>
        /// 获取状态描述
        /// </summary>
        /// <param name="state">节点状态</param>
        /// <returns></returns>
        private string GetSta(int state)
        {
            string sta = "(未审批)";
            switch (state)
            {
                case 1:
                    sta = "(审批中)";
                    break;
                case 2:
                    sta = "(审批通过)";
                    break;
                case 3:
                    sta = "(被打回)";
                    break;
                default:
                    break;

            }

            return sta;
        }
        #endregion

        #region 统一发送
        /// <summary>
        /// 查询当前待审批
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetAppSum()
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            var list = Db.Set<AppInst>().Where(a => a.AppState == 1).Select(a => a).ToList();
            var InstIds = list.Select(a => a.Id).ToList();
            var nodeIds = list.Select(a => a.CurrentNodeId??0).ToList();
            var appusergs = Db.Set<AppGroupUser>().Where(a =>InstIds.Contains((a.InstId??0)) && nodeIds.Contains((a.NodeId??-1))).ToList();
            var userdic = Db.Set<UserInfor>().Where(a => a.IsDelete != 1).Select(a => new { a.Id, a.WxCode }).ToDictionary(a=>a.Id,a=>a.WxCode);
            var query = from a in appusergs
                        group a by a.UserId into g
                        select new
                        {
                           UserId=g.Key,
                           Rows=g.Count()

                        };
            var listdata = query.ToList();
            foreach (var item in listdata)
            {
                var cuuserid = item.UserId ?? 0;
                if (userdic.ContainsKey(cuuserid))
                {
                    var wxcode = userdic[cuuserid];
                    if (!string.IsNullOrEmpty(wxcode))
                    {
                        dic.Add(wxcode,item.Rows);
                    }
                   
                }

                
            }


            return dic;
            



        }

        /// <summary>
        /// 提醒条数
        /// </summary>
        public void PubMsgRowsToList()
        {
            var week= DateTime.Today.DayOfWeek.ToString();//Friday=>星期5
            var dic = GetAppSum();
            foreach (var dc in dic)
            {
                var rowmsg = new WxTongZhiInfo();
                rowmsg.WxCode = dc.Key;
                rowmsg.Rows = dic[dc.Key];

                RedisHelper.ListRightPush("WxAppRowsMsg", rowmsg);

            }

        }

        /// <summary>
        /// 查询待处理审批信息
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <returns></returns>
        public DaiShenPiInfo GetDclInfo(int userId)
        {
            var pageInfo = new PageInfo<AppInst>(pageIndex: 0, pageSize: 5);
            var predicateAnd = PredicateBuilder.True<AppInst>();
            var predicateOr = PredicateBuilder.False<AppInst>();

           
           var res= GetAppPendingList(pageInfo, userId, predicateAnd, a => a.StartDateTime, true);
            DaiShenPiInfo daiShenPi = new DaiShenPiInfo();
            daiShenPi.RowCount = res.count;
            if (daiShenPi.RowCount>0)
            {
                daiShenPi.NextId = res.data[0].AppObjId;
            }

            return daiShenPi;



        }
         


        #endregion
    }
}
