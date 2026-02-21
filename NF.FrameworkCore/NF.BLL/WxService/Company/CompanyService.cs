using Microsoft.EntityFrameworkCore;
using NF.BLL.Common;
using NF.BLL.Extend;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NF.BLL
{
    public partial class CompanyService
    {/// <summary>
     /// 
     /// </summary>
     /// <typeparam name="s"></typeparam>
     /// <param name="pageInfo"></param>
     /// <param name="whereLambda"></param>
     /// <param name="orderbyLambda"></param>
     /// <param name="isAsc"></param>
     /// <returns></returns>
        public LayPageInfo<WxKhlist> GetWxCompList<s>(PageInfo<Company> pageInfo, Expression<Func<Company, bool>> whereLambda,
        Expression<Func<Company, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = _CompanySet//.Include(a=>a.CreateUser)//.AsTracking()
                .Where<Company>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<Company>))
            { //分页
                tempquery = tempquery.Skip<Company>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<Company>(pageInfo.PageSize);
            }

            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,//Wx
                            Name = a.Name,//Wx
                            Code = a.Code,//Wx
                            Cstate = a.Cstate,//Wx
                            Ctype = a.Ctype,//Wx
                            CompClassId = a.CompClassId,//Wx//公司类别
                            WfState = a.WfState,
                            WfItem = a.WfItem,
                            WfCurrNodeName = a.WfCurrNodeName,

                        };
            var local = from a in query.AsEnumerable()
                        select new WxKhlist
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            Cstate = a.Cstate,
                            CstateDic = EmunUtility.GetDesc(typeof(CompStateEnum), a.Cstate),
                            CompanyTypeClass = CompanyUtility.CompanyTypeClass(a.CompClassId, a.Ctype ?? -1), //tempInfo.CompClass.Name,//公司类别
                            CompClassId = a.CompClassId,
                            WfState = a.WfState,
                            WfCurrNodeName = a.WfCurrNodeName,
                            WfItem = a.WfItem ?? -2,
                            WfItemDic = FlowUtility.GetMessionDic(a.WfItem ?? -1, 0),
                            WfStateDic = EmunUtility.GetDesc(typeof(WfStateEnum), a.WfState ?? -1),
                        };
            return new LayPageInfo<WxKhlist>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }
        /// <summary>
        /// 查看信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public WxKhView KhView(int Id)
        {
            var query = from a in _CompanySet.AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,//Wx
                            Name = a.Name,//Wx
                            Code = a.Code,//Wx

                            CreateUserId = a.CreateUserId,//Wx
                            Cstate = a.Cstate,//Wx

                            FirstContact = a.FirstContact,//Wx//首要联系人
                            FirstContactMobile = a.FirstContactMobile,//Wx//首要联系人移动电话
                            FirstContactTel = a.FirstContactTel,//Wx//首要联系人办公电话
                            FirstContactPosition = a.FirstContactPosition,//Wx//职位
                            CareditId = a.CareditId,//Wx
                            LevelId = a.LevelId,//Wx
                            CompClassId = a.CompClassId,//Wx

                            PrincipalUserId = a.PrincipalUserId,//Wx


                            FirstContactEmail = a.FirstContactEmail,//Wx//Email

                            Ctype = a.Ctype,//Wx//标识是不是客户
                            WfState = a.WfState,
                            WfItem = a.WfItem,
                            WfCurrNodeName = a.WfCurrNodeName,

                        };
            var local = from tempInfo in query.AsEnumerable()

                        select new WxKhView
                        {
                            Id = tempInfo.Id,
                            Name = tempInfo.Name,
                            Code = tempInfo.Code,

                            CreateUserDisplayName = this.Db.GetRedisUserFieldValue(tempInfo.PrincipalUserId ?? -1),
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{tempInfo.CreateUserId}", "DisplyName"),//tempInfo.CreateUser.DisplyName,
                            Cstate = tempInfo.Cstate,

                            CstateDic = EmunUtility.GetDesc(typeof(CompStateEnum), tempInfo.Cstate),

                            FirstContact = tempInfo.FirstContact,//首要联系人
                            FirstContactMobile = tempInfo.FirstContactMobile,//首要联系人移动电话
                            FirstContactTel = tempInfo.FirstContactTel,//首要联系人办公电话
                            FirstContactPosition = tempInfo.FirstContactPosition,//职位
                            CareditName = GetCareditName(tempInfo.CareditId),//tempInfo.Caredit.Name,//信用等级
                            CareditId = tempInfo.CareditId,
                            LevelName = GetLevelName(tempInfo.LevelId, tempInfo.Ctype ?? -1),//Wx //tempInfo.Level.Name,//单位级别
                            LevelId = tempInfo.LevelId,
                            CompanyTypeClass = CompanyUtility.CompanyTypeClass(tempInfo.CompClassId, tempInfo.Ctype ?? -1), //tempInfo.CompClass.Name,//公司类别
                            CompClassId = tempInfo.CompClassId,

                            PrincipalUserDisplayName = (tempInfo.PrincipalUserId ?? 0) == 0 ? "" : RedisHelper.HashGet($"{StaticData.RedisUserKey}:{tempInfo.PrincipalUserId}", "DisplyName").ToString(),//负责人
                            PrincipalUserId = tempInfo.PrincipalUserId,



                            FirstContactEmail = tempInfo.FirstContactEmail,//Wx//Email

                            Ctype = tempInfo.Ctype,//标识是不是客户
                            WfState = tempInfo.WfState,
                            WfCurrNodeName = tempInfo.WfCurrNodeName,
                            WfItem = tempInfo.WfItem ?? -2,
                            WfItemDic = FlowUtility.GetMessionDic(tempInfo.WfItem ?? -1, 0),
                            WfStateDic = EmunUtility.GetDesc(typeof(WfStateEnum), tempInfo.WfState ?? -1),

                        };
            return local.FirstOrDefault();



        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<CompContact> WxQtlxr(int id)
        {
            try
            {
                var Rlist = Db.Set<CompContact>().Where(a => a.IsDelete == 0 && a.CompanyId == id).ToList();
                return Rlist;
            }
            catch (Exception)
            {

                return null;
            }

        }



    }
}
