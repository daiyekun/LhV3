using Microsoft.EntityFrameworkCore;
using NF.Common.Extend;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using NF.BLL.Common;
using NF.BLL.Extend;

namespace NF.BLL
{

    public partial class APPSompanylist : BaseService<Company>, IAPPSompanylist
    {
        private DbSet<Company> _CompanyInfoSet = null;
        public APPSompanylist(DbContext dbContext)
           : base(dbContext)
        {
            _CompanyInfoSet = Db.Set<Company>();
        }
        public APPSompanylist() { }


        /// <summary>
        /// 客户/供应商大列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public APPLayPageInfo<CompanyViewDTO> GetList<s>(PageInfo<Company> pageInfo, Expression<Func<Company, bool>> whereLambda,
    Expression<Func<Company, s>> orderbyLambda, bool isAsc, int type, int start, int limit)
        {
            var tempquery = _CompanyInfoSet.AsTracking().Where<Company>(whereLambda.Compile()).AsQueryable(); 
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            var query = from a in tempquery
                        where a.Ctype == type
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,//名称
                            Code = a.Code,//编号
                            FirstContact = a.FirstContact,    //联系人
                            FirstContactTel = a.FirstContactTel  //联系方式
                        };
            var local = from a in query.AsEnumerable()
                        select new CompanyViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,//名称
                            Code = a.Code,//编号
                            FirstContact = a.FirstContact==null?"": a.FirstContact,    //联系人
                            FirstContactTel = a.FirstContactTel == null ? "" : a.FirstContactTel  //联系方式
                        };
            int totalCount = local.Count(); //共有记录数
            int pageNum = start / limit;//共有页数 
            var result = local.Skip(limit * pageNum).Take(limit).ToList();
            return new APPLayPageInfo<CompanyViewDTO>()
            {
                totalCount = totalCount,
                items = result,
            };
        }
        /// <summary>
        /// 客户-联系人
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<APPCompContact> companyDetailContact(int Id)
        {
            var query = from a in Db.Set<CompContact>().AsNoTracking()
                        where a.CompanyId == Id
                        select new
                        {
                            Id = a.Id, // 客户id
                            Name = a.Name,// 首要联系人
                            Position = a.Position,// 职位
                            Tel = a.Tel,// 首要联系人办公电话
                            Mobile = a.Mobile,// 首要联系人移动电话
                            Email = a.Email,// Email

                        };
            var local = from a in query.AsEnumerable()
                        select new APPCompContact
                        {
                            Id = a.Id, // 客户id
                            Name = a.Name,// 首要联系人
                            Position = a.Position == null ? "" : a.Position,// 职位
                            Tel = a.Tel == null ? "" : a.Tel,// 首要联系人办公电话
                            Mobile = a.Mobile == null ? "" : a.Mobile,// 首要联系人移动电话
                            Email = a.Email == null ? "" : a.Email,// Email
                        };
            return local.ToList();
        }
        /// <summary>
        /// APP客户-详细信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public CompanyViewDTO GetIDView(int Id)
        {
            var query = from a in _CompanyInfoSet.AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id, // 客户id
                            Name = a.Name,// 客户名称
                            Code = a.Code,// 编号
                            CompClassId = a.CompClassId,// 类别id
                            LevelId = a.LevelId,// 单位级别
                            CareditId = a.CareditId,// 信用等级
                            WfState = a.WfState,// 流程状态
                            PrincipalUserId = a.PrincipalUserId,// 负责人
                            CreateUserId = a.CreateUserId,// 建立人
                            FirstContact = a.FirstContact,// 首要联系人
                            FirstContactPosition = a.FirstContactPosition,// 职位
                            FirstContactTel = a.FirstContactTel,// 首要联系人办公电话
                            FirstContactMobile = a.FirstContactMobile,// 首要联系人移动电话
                            FirstContactEmail = a.FirstContactEmail,// Email
                            Ctype = a.Ctype,
                        };
            var local = from a in query.AsEnumerable()
                        select new CompanyViewDTO
                        {
                            Id = a.Id,// id
                            Name = a.Name,// 客户名称
                            Code = a.Code,// 编号
                            CompanyTypeClass = CompanyUtility.CompanyTypeClass(a.CompClassId, a.Ctype ?? -1), //tempInfo.CompClass.Name,//公司类别
                            LevelName = GetLevelName(a.LevelId, a.Ctype ?? -1), //tempInfo.Level.Name,//单位级别
                            LevelId = a.LevelId,
                            CareditName = GetCareditName(a.CareditId),//tempInfo.Caredit.Name,//信用等级
                            CareditId = a.CareditId,
                            WfStateDic = EmunUtility.GetDesc(typeof(WfStateEnum), a.WfState ?? -1),
                            PrincipalUserDisplayName = (a.PrincipalUserId ?? 0) == 0 ? "" : RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.PrincipalUserId}", "DisplyName").ToString(),//负责人
                            PrincipalUserId = a.PrincipalUserId,
                            CreateUserDisplayName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                          //  RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"),//建立人
                            FirstContact = a.FirstContact,// 首要联系人
                            FirstContactPosition = a.FirstContactPosition,// 职位
                            FirstContactTel = a.FirstContactTel,//首要联系人办公电话
                            FirstContactMobile = a.FirstContactMobile,//首要联系人移动电话
                            FirstContactEmail = a.FirstContactEmail,//Email
                        };
            return local.FirstOrDefault();
        }

        /// <summary>
        /// 信用等级名称
        /// </summary>
        /// <returns>返回信息等级名称</returns>
        private string GetCareditName(int? CareditId)
        {
            return DataDicUtility.GetDicValueToRedis(CareditId, DataDictionaryEnum.customerCaredit);
        }
        /// <summary>
        /// 级别
        /// </summary>
        /// <param name="LevelId">级别ID</param>
        /// <param name="comptype">0：客户，1：供应商，2：其他对方</param>
        /// <returns></returns>
        private string GetLevelName(int? LevelId, int comptype)
        {
            if (LevelId == null)
            {
                return "";
            }
            else
            {
                DataDictionaryEnum customerLevel = DataDictionaryEnum.customerLevel;
                switch (comptype)
                {
                    case 0:
                        customerLevel = DataDictionaryEnum.customerLevel;
                        break;
                    case 1:
                        customerLevel = DataDictionaryEnum.supplierLevel;
                        break;
                    case 2:
                        customerLevel = DataDictionaryEnum.otherLevel;
                        break;
                }

                return DataDicUtility.GetDicValueToRedis(LevelId, customerLevel);
            }
        }
        /// <summary>
        /// APP根据合同id查看客户附件
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<APPCompAttachmentDTO> Company_accessory(int Id)
        {
            var query = from a in Db.Set<CompAttachment>().AsNoTracking()
                            //    var query = from a in _CompanyInfoSet.AsNoTracking()
                        where a.CompanyId == Id
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Remark = a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            //CreateUserDisplyName =  //a.CreateUser.DisplyName,
                            CategoryName = a.Category.Name,
                            Path = a.Path,
                            FileName = a.FileName,
                            GuidFileName = a.GuidFileName,

                        };
            var local = from a in query.AsEnumerable()
                        select new APPCompAttachmentDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Remark = a.Remark == null ? "": a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserDisplyName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                        //    RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"),//a.CreateUserDisplyName,
                            CategoryName = a.CategoryName == null ? "": a.CategoryName,
                            Path = a.Path == null ? "": a.Path,
                            FileName = a.FileName == null ? "": a.FileName,
                            GuidFileName = a.GuidFileName == null ? "": a.GuidFileName
                        };
            return local.ToList();

        }
    }
}
