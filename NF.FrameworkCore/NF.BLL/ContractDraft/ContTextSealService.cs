using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NF.Common.Utility;
using NF.ViewModel;

namespace NF.BLL
{
    /// <summary>
    /// 盖章
    /// </summary>
   public partial class ContTextSealService
    {
        /// <summary>
        /// 当前合同文本ID
        /// </summary>
        /// <param name="build">是否新建</param>
        /// <param name="Id">合同文本ID</param>
        /// <returns></returns>
       public ContTextSealViewDTO ShowView(int userId,int Id)
        {
            var query = from a in Db.Set<ContText>().AsNoTracking()
                        join b in Db.Set<ContTextSeal>().AsNoTracking()
                        on a.Id equals b.ContTextId  into s
                        from sel in s.DefaultIfEmpty()
                        where a.Id == Id
                        select new
                        {
                          
                          ContTextId=a.Id,
                          MainDeptId = a.Cont.MainDeptId,
                          DeptId=a.Cont.DeptId,
                          ContState=a.Cont.ContState,
                          CreateUserId= sel==null? userId : sel.CreateUserId,
                          CreateDateTime= sel==null?DateTime.Now:sel.CreateDateTime,
                          SealUser=sel==null?"":sel.SealUser,//印章申请人
                          SealState=sel==null?-1: sel.SealState,//印章状态
                          SealNumber=sel==null?0: sel.SealNumber,//盖章数
                          EachNumber=sel==null?0:sel.EachNumber,//每份盖章数
                          SealTotal=sel==null?0: sel.SealTotal,//盖章总数
                          SealName=sel==null?"": sel.Seal.SealName,//印章名称
                          SealId= sel == null ? -1 : sel.SealId,//印章ID
                          Id= sel == null ? 0 : sel.Id,//盖章ID
                        };
            var local = from a in query.AsEnumerable()
                        select new ContTextSealViewDTO
                        {
                            ContTextId = a.ContTextId,
                            MainDeptName = RedisValueUtility.GetDeptName(a.MainDeptId ?? -2),
                            DeptName = RedisValueUtility.GetDeptName(a.DeptId ?? -2),
                            ContStateDic = EmunUtility.GetDesc(typeof(ContractState), a.ContState),
                            CreateDateTime = a.CreateDateTime,
                            CreateUserName = RedisValueUtility.GetUserShowName(a.CreateUserId),
                            SealUser = a.SealUser,//印章申请人
                            SealState = a.SealState,//印章状态
                            SealNumber = a.SealNumber,//盖章数
                            EachNumber = a.EachNumber,//每份盖章数
                            SealTotal = a.SealTotal,//盖章总数
                            SealName =a.SealName,//印章名称
                            SealId = a.SealId,//印章ID,
                            Id = a.Id,//盖章ID
                            SealStateDic= EmunUtility.GetDesc(typeof(SealStateEnum), a.SealState)
                        };
            return local.FirstOrDefault();


        }
    }
}
