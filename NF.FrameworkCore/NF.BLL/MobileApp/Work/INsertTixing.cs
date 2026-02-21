using Microsoft.EntityFrameworkCore;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.APPModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NF.BLL.MobileApp.Work
{
   public class INsertTixing
    {
        public void newSelectTixing(string NodeStrId, int InstId, DbContext db)
        {
            //获取下个节点审批用户或组员ID
            //查询AppInstNodeInfo审批实例节点信息表
            var nextnodeInfos = db.Set<AppInstNodeInfo>()
                   .Where(a => a.InstId == InstId && a.NodeStrId == NodeStrId).FirstOrDefault();
            List<AppGroupUser> username = db.Set<AppGroupUser>()
                    .Where(a => a.GroupId == nextnodeInfos.GroupId && a.InstId == nextnodeInfos.InstId &&a.NodeStrId== NodeStrId).ToList();
            var info = "";
            foreach (var item in username)
            {
                int userid = item.UserId ?? 0;
                var insr = db.Set<AppInst>().Where(a => a.Id == InstId).FirstOrDefault();
                switch (insr.ObjType)
                {
                    case 0:
                        var Company = db.Set<Company>().Where(a => a.Id == insr.AppObjId).FirstOrDefault();
                        info = "您有一条客户信息：" + Company.Name + ",需要审批请。请尽快登录系统处理。";
                        break;
                    case 1:
                        var Company1 = db.Set<Company>().Where(a => a.Id == insr.AppObjId).FirstOrDefault();
                        info = "您有一条客户供应商：" + Company1.Name + ",需要审批请。请尽快登录系统处理。";
                        break;
                    case 2:
                        var Company2 = db.Set<Company>().Where(a => a.Id == insr.AppObjId).FirstOrDefault();
                        info = "您有一条客户其他对方：" + Company2.Name + ",需要审批请。请尽快登录系统处理。";
                        break;
                    case 3:
                        var ContractInfo = db.Set<ContractInfo>().Where(a => a.Id == insr.AppObjId).FirstOrDefault();
                        info = "您有一条合同：" + ContractInfo.Name + ",需要审批请。请尽快登录系统处理。";
                        break;
                    case 4:
                        var ContInvoice = db.Set<ContInvoice>().Where(a => a.Id == insr.AppObjId).FirstOrDefault();
                        info = "您有一条收票金额：" + ContInvoice.AmountMoney + ",需要审批请。请尽快登录系统处理。";
                        break;

                    case 5:
                        var ContInvoice1 = db.Set<ContInvoice>().Where(a => a.Id == insr.AppObjId).FirstOrDefault();
                        info = "您有一条开票金额：" + ContInvoice1.AmountMoney + ",需要审批请。请尽快登录系统处理。";
                        break;
                    case 6:
                        var ContActualFinance = db.Set<ContActualFinance>().Where(a => a.Id == insr.AppObjId).FirstOrDefault();
                        info = "您有一条付款金额：" + ContActualFinance.AmountMoney + ",需要审批请。请尽快登录系统处理。";
                        break;
                    case 7:
                        var ProjectManager = db.Set<ProjectManager>().Where(a => a.Id == insr.AppObjId).FirstOrDefault();
                        info = "您有一条项目：" + ProjectManager.Name + ",需要审批请。请尽快登录系统处理。";
                        break;
                    default:
                        break;
                }
                SetRedis(userid, info);

            }
        }

        /// <summary>
        /// 存储Redis
        /// </summary>
        public void SetRedis(int UserID, string info)
        {
            //TIxing
            var Uuid = Guid.NewGuid().ToString();
            int state = 0;
            DateTime date = DateTime.Now;
            TIxing ab = new TIxing { Uuid = Uuid, date = date, info = info, UserID = UserID, state = state };
            //SetRedisHash();
            TIxing result = new TIxing() { Uuid = Uuid, date = date, info = info, UserID = UserID, state = state };
            string json = JsonUtility.SerializeObject(result);
            //SetRedisHash(json, UserID,StaticData.InsertTExing, (a, c) =>
            //{
            //    return $"{a}:{c}";
            //}); 
            var keys = "InsertTExing:" + UserID;
            RedisHelper.HashUpdate(keys, Uuid, json);

        }
    }
}
