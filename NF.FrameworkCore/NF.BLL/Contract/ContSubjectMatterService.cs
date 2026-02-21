using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using NF.Common.Extend;
using NF.AutoMapper;
using NF.ViewModel;
using NF.BLL.Common;
using NF.ViewModel.Models.ContTxtTemplate;
using NF.BLL.Extend;
using AutoMapper;

namespace NF.BLL
{
    /// <summary>
    /// 合同标的
    /// </summary>
    public partial class ContSubjectMatterService
    {
        private IMapper _IMapper;
        /// <summary>
        /// 保存合同标的
        /// </summary>
        /// <param name="subjectMatter">合同标的</param>
        /// <returns>Id:->Hid:</returns>
        public Dictionary<string, int> AddSave(ContSubjectMatter subjectMatter, IMapper _IMapper)
        {
            var inof = Add(subjectMatter);

            return SaveSubMatterHistory(inof, _IMapper);
        }

        /// <summary>
        /// 保存合同标的
        /// </summary>
        /// <param name="subjectMatter">合同标的</param>
        /// <returns>Id:->Hid:</returns>
        public bool AddSave(IList<ContSubjectMatterDTO> subs, IMapper _IMapper)
        {

            var arrayIds = subs.Select(a => a.Id).ToArray();
            var query = Db.Set<ContSubjectMatter>().AsNoTracking().Where(a => arrayIds.Contains(a.Id)).ToList();
            IList<ContSubjectMatter> subjectMatters = new List<ContSubjectMatter>();
            foreach (ContSubjectMatter item in query)
            {
                var dtomel = subs.Where(a => a.Id == item.Id).FirstOrDefault();
                var submt = _IMapper.Map<ContSubjectMatterDTO, ContSubjectMatter>(dtomel); //dtomel.ToModel<ContSubjectMatterDTO, ContSubjectMatter>();
                submt.CreateDateTime = item.CreateDateTime;
                submt.CreateUserId = item.CreateUserId;
                submt.ModifyDateTime = DateTime.Now;
                submt.ModifyUserId = item.ModifyUserId;
                submt.IsFromCategory = item.IsFromCategory;
                submt.IsDelete = 0; //item.IsDelete;
                submt.SubState = 0;//未交付
                submt.ContId = item.ContId;
                submt.BcInstanceId = item.BcInstanceId;
                subjectMatters.Add(submt);

            }
            //添加历史

            this.Update(subjectMatters);
            SaveSubMatterHistory(subjectMatters, _IMapper);

            return true;





        }
        /// <summary>
        /// 创建历史
        /// </summary>
        /// <param name="subjectMatters"></param>
        /// <returns></returns>
        private void SaveSubMatterHistory(IList<ContSubjectMatter> subjectMatters, IMapper _IMapper)
        {
            IList<ContSubjectMatterHistory> listsubhist = new List<ContSubjectMatterHistory>();
            foreach (var item in subjectMatters)
            {
                var subjectMatterHistory = _IMapper.Map<ContSubjectMatter, ContSubjectMatterHistory>(item); // item.ToModel<ContSubjectMatter, ContSubjectMatterHistory>();
                subjectMatterHistory.SubjId = item.Id;
                subjectMatterHistory.CreateDateTime = DateTime.Now;
                listsubhist.Add(subjectMatterHistory);
            }


            this.Db.Set<ContSubjectMatterHistory>().AddRange(listsubhist);
            this.SaveChanges();//一个链接  多个sql



        }

        /// <summary>
        /// 修改合同标的
        /// </summary>
        /// <param name="subjectMatter">合同标的</param>
        /// <param name="subjectMatterHistory">合同标的历史</param>
        /// <returns>Id:合同标的ID、Hid:合同标的历史ID</returns>
        public Dictionary<string, int> UpdateSave(ContSubjectMatter subjectMatter, IMapper _IMapper)
        {
            Update(subjectMatter);
            return UpdateSubMatterHistory(subjectMatter, _IMapper);

        }
        /// <summary>
        /// 修改标的历史
        /// </summary>
        /// <param name="subjectMatter">合同标的</param>
        /// <returns></returns>
        private Dictionary<string, int> UpdateSubMatterHistory(ContSubjectMatter subjectMatter, IMapper _IMapper)
        {

            var subHis = Db.Set<ContSubjectMatterHistory>().AsNoTracking().Where(a => a.SubjId == subjectMatter.Id).OrderByDescending(a => a.Id).FirstOrDefault();
            if (subHis != null)
            {
                var subjectMatterHistory = _IMapper.Map<ContSubjectMatter, ContSubjectMatterHistory>(subjectMatter); //  subjectMatter.ToModel<ContSubjectMatter, ContSubjectMatterHistory>();
                subjectMatterHistory.Id = subHis.Id;
                subjectMatterHistory.ContHisId = subHis.ContHisId;
                Db.Entry<ContSubjectMatterHistory>(subjectMatterHistory).State = EntityState.Modified;
            }
            Db.SaveChanges();
            var dic = new Dictionary<string, int>();
            dic.Add("Id", subjectMatter.Id);
            dic.Add("Hid", subHis.Id);
            return dic;
        }
        /// <summary>
        /// 保存标的历史
        /// </summary>
        /// <param name="subjectMatter">合同标的</param>
        /// <returns></returns>
        private Dictionary<string, int> SaveSubMatterHistory(ContSubjectMatter subjectMatter, IMapper _IMapper)
        {
            var subjectMatterHistory = _IMapper.Map<ContSubjectMatter, ContSubjectMatterHistory>(subjectMatter); //  subjectMatter.ToModel<ContSubjectMatter, ContSubjectMatterHistory>();
            subjectMatterHistory.SubjId = subjectMatter.Id;
            subjectMatterHistory.CreateDateTime = DateTime.Now;
            Db.Set<ContSubjectMatterHistory>().Add(subjectMatterHistory);
            Db.SaveChanges();
            var dic = new Dictionary<string, int>();
            dic.Add("Id", subjectMatter.Id);
            dic.Add("Hid", subjectMatterHistory.Id);
            return dic;
        }
        /// <summary>
        /// 标的大列表查询
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<ContSubjectMatterListDTO> GetMainList<s>(PageInfo<ContSubjectMatter> pageInfo, Expression<Func<ContSubjectMatter, bool>> whereLambda, Expression<Func<ContSubjectMatter, s>> orderbyLambda, bool isAsc)
        {
            IList<BusinessCategoryDTO> listcates = RedisHelper.StringGetToList<BusinessCategoryDTO>("NF-BcCateGoryListAll");
            //var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var tempquery = Db.Set<ContSubjectMatter>()
                .Include(a => a.Cont)//.ThenInclude(a=>a.Comp)
                .Include(a=>a.BcInstance)
                // .Include(a => a.Cont).ThenInclude(a => a.CreateUser)
                .AsTracking().Where<ContSubjectMatter>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<ContSubjectMatter>))
            {
                tempquery = tempquery.Skip<ContSubjectMatter>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<ContSubjectMatter>(pageInfo.PageSize);
            }
            var query = from a in tempquery.AsQueryable()
                        //join b in Db.Set<BcInstance>()
                        //on a.BcInstanceId equals b.Id into g
                        //from c in g.DefaultIfEmpty()
                        select new
                        {
                            Id = a.Id,
                            ContId = a.ContId,
                            Name = a.Name,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            Unit = a.Unit,
                            Amount = a.Amount,
                            Price = a.Price,
                            Remark = a.Remark,
                            DiscountRate = a.DiscountRate,
                            SubTotalRate = a.SubTotalRate,
                            SubTotal = a.SubTotal,
                            SalePrice = a.SalePrice,
                            AmountMoney = a.AmountMoney,
                            PlanDateTime = a.PlanDateTime,
                            Lb = a.BcInstance==null?-1: a.BcInstance.LbId,//c==null?-1:c.LbId,
                            ContName = a.Cont==null?"": a.Cont.Name,
                            ContNo = a.Cont==null?"":a.Cont.Code,
                            CompName = (a.Cont!=null&& a.Cont.Comp!=null)?a.Cont.Comp.Name:"",//合同对方
                            CompId= (a.Cont != null && a.Cont.Comp != null) ? a.Cont.Comp.Id :0,//合同对方id
                            ComplateAmount = a.ComplateAmount,
                            SjJfRq = a.SjJfRq,
                            ContState = a.Cont==null?-1: a.Cont.ContState,
                            IsFromCategory = a.IsFromCategory,//标的类型
                            SubState= a.SubState


                        };

            var list = query.ToList();
            var local = from a in query.AsEnumerable()
                        select new ContSubjectMatterListDTO
                        {
                            Id = a.Id,
                            ContId = a.ContId,
                            ContName = a.ContName,
                            ContNo = a.ContNo,
                            SubName = a.Name,//名称
                            Unit = a.Unit,//单位
                            PriceThod = a.Price.ThousandsSeparator(),
                            Amountstr = a.Amount.ThousandsSeparator(),//数量
                            TotalThod = ((a.Price ?? 0) * (a.Amount ?? 0)).ThousandsSeparator(),//小计
                            SalePriceThod = a.SalePrice.ThousandsSeparator(),//报价
                            Zkl = (((a.SalePrice ?? 0)==0|| (a.Price ?? 0)==0)?0:(a.SalePrice ?? 0) / (a.Price ?? 0)).ConvertToPercent(),//折扣率
                            Remark = a.Remark,//备注
                            CompName = a.CompName,//合同对方
                            CompId = a.CompId,
                            CreateDateTime = a.CreateDateTime,//创建时间
                            PlanDateTime = a.PlanDateTime,//计划交付时间
                            ComplateAmount = a.ComplateAmount ?? 0,//已交付数量
                            NotDelNum = ((a.Amount ?? 0) - (a.ComplateAmount ?? 0)),//未交付数量
                            JfBl = GetJfBl(a.ComplateAmount ?? 0, a.Amount ?? 0),
                            SjJfRq = a.SjJfRq,//实际交付日期
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                           // RedisValueUtility.GetUserShowName(a.CreateUserId),//建立人
                            ContStateDic = EmunUtility.GetDesc(typeof(ContractState), a.ContState),//合同状态
                            ContState = a.ContState,//合同状态
                            CatePath = GetBcLb(a.IsFromCategory, a.Lb, listcates),//所属类别
                            SubState=a.SubState??0,//交付状态

                        };
            return new LayPageInfo<ContSubjectMatterListDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }
        /// <summary>
        /// 类别
        /// </summary>
        /// <param name="IsFromCategory">
        /// 标的类型（是否关联品类等存值）
        ///是否为品类标的
        ///0：普通标的，非业务类(NULL也是)
        ///1：品类关联标的，业务类
        ///2：导入的Excel
        //</param>
        /// <param name="lb"></param>
        /// <param name="listcates">品类类别</param>
        /// <returns></returns>
        private string GetBcLb(int? IsFromCategory, int? lb, IList<BusinessCategoryDTO> listcates)
        {
            if ((IsFromCategory ?? -1) <= 0)
            {
                return "非业务品类";
            }
            else
            {
                return BcCateUtility.GetCatePath((lb ?? -1), listcates);
            }

        }
        /// <summary>
        /// 交付比例
        /// </summary>
        /// <param name="DelNum">交付数量</param>
        /// <param name="Amount">数量</param>
        /// <returns></returns>
        private string GetJfBl(decimal DelNum, decimal Amount)
        {
            if (DelNum <= 0 || Amount <= 0)
            {
                return DelNum.ConvertToPercent();
            }
            else
            {
                return (DelNum / Amount).ConvertToPercent();
            }

        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<ContSubjectMatterViewDTO> GetList<s>(PageInfo<ContSubjectMatter> pageInfo, Expression<Func<ContSubjectMatter, bool>> whereLambda, Expression<Func<ContSubjectMatter, s>> orderbyLambda, bool isAsc)
        {
            //var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var tempquery = Db.Set<ContSubjectMatter>().Include(a=>a.BcInstance).AsTracking().Where<ContSubjectMatter>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<ContSubjectMatter>)) {
                tempquery = tempquery.Skip<ContSubjectMatter>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<ContSubjectMatter>(pageInfo.PageSize);
            }
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Spec = a.Spec,
                            Stype = a.Stype,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            Unit = a.Unit,
                            Amount = a.Amount,
                            Price = a.Price,
                            Remark = a.Remark,
                            DiscountRate = a.DiscountRate,
                            SubTotalRate = a.SubTotalRate,
                            SubTotal = a.SubTotal,
                            SalePrice = a.SalePrice,
                            AmountMoney = a.AmountMoney,
                            NominalQuote = a.NominalQuote,
                            NominalRate = a.NominalRate,
                            PlanDateTime = a.PlanDateTime


                        };
            var local = from a in query.AsEnumerable()
                        select new ContSubjectMatterViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,//名称
                            Spec = a.Spec,//规格
                            Stype = a.Stype,//型号
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            Unit = a.Unit,//单位
                            Amount = a.Amount,
                            Price = a.Price,
                            Remark = a.Remark,
                            DiscountRate = a.DiscountRate,
                            SubTotalRate = a.SubTotalRate,
                            SubTotal = a.SubTotal,
                            SalePrice = a.SalePrice,
                            AmountMoney = a.AmountMoney,
                            NominalQuote = a.NominalQuote,
                            PlanDateTime = a.PlanDateTime,
                            NominalRate = a.NominalRate,
                            PriceThod = a.Price.ThousandsSeparator(),
                            SubTotalThod = a.SubTotal.ThousandsSeparator(),
                            SalePriceThod = a.SalePrice.ThousandsSeparator(),


                        };
            return new LayPageInfo<ContSubjectMatterViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };


        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">需要删除的Ids集合</param>
        /// <returns>受影响行数</returns>
        public int Delete(string Ids)
        {
            string sqlstr = "update ContSubjectMatter set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }
        /// <summary>
        /// 查看或者修改
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public ContSubjectMatterViewDTO ShowView(int Id)
        {
            var query = from a in _ContSubjectMatterSet.AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Spec = a.Spec,
                            Stype = a.Stype,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            Unit = a.Unit,
                            Amount = a.Amount,
                            Price = a.Price,
                            Remark = a.Remark,
                            DiscountRate = a.DiscountRate,
                            SubTotalRate = a.SubTotalRate,
                            SubTotal = a.SubTotal,
                            SalePrice = a.SalePrice,
                            AmountMoney = a.AmountMoney,
                            NominalQuote = a.NominalQuote,
                            NominalRate = a.NominalRate,
                            PlanDateTime = a.PlanDateTime,
                        };
            var local = from a in query.AsEnumerable()
                        select new ContSubjectMatterViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Spec = a.Spec,
                            Stype = a.Stype,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            Unit = a.Unit,
                            Amount = a.Amount,
                            Price = a.Price,
                            Remark = a.Remark,
                            DiscountRate = a.DiscountRate,
                            SubTotalRate = a.SubTotalRate,
                            SubTotal = a.SubTotal,
                            SalePrice = a.SalePrice,
                            AmountMoney = a.AmountMoney,
                            NominalQuote = a.NominalQuote,
                            NominalRate = a.NominalRate,
                            PriceThod = a.Price.ThousandsSeparator(),
                            SubTotalThod = a.SubTotal.ThousandsSeparator(),
                            SalePriceThod = a.SalePrice.ThousandsSeparator(),
                            PlanDateTime = a.PlanDateTime,
                        };
            return local.FirstOrDefault();
        }
        /// <summary>
        /// 交付列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <param name="search">查询类型：1：完全交付</param>
        /// 
        /// <returns></returns>
        public LayPageInfo<JiaoFuListInfo> GetJiaoFuList<s>(PageInfo<ContSubjectMatter> pageInfo, Expression<Func<ContSubjectMatter, bool>> whereLambda, Expression<Func<ContSubjectMatter, s>> orderbyLambda, bool isAsc, int? search)
        {
            //var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var tempquery = Db.Set<ContSubjectMatter>()
                .Include(a=>a.Cont)
                .ThenInclude(a=>a.Comp)
                .AsTracking().Where<ContSubjectMatter>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<ContSubjectMatter>))
                tempquery = tempquery.Skip<ContSubjectMatter>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<ContSubjectMatter>(pageInfo.PageSize);
            
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Unit = a.Unit,//单位
                            Price = a.Price,//单价
                            Amount = a.Amount,//数量
                            SubTotal = a.SubTotal,//小计
                            ComplateAmount = a.ComplateAmount,//已完成已交付
                            PlanDateTime = a.PlanDateTime,//计划交付时间
                            ContId = a.ContId,
                            ContName = a.Cont==null?"": a.Cont.Name,//合同名称
                            CompName =(a.Cont!=null&& a.Cont.Comp != null)? a.Cont.Comp.Name:"",//合同对方名称
                            CompId = (a.Cont != null && a.Cont.Comp != null) ? a.Cont.Comp.Id : 0,//合同对方id
                        };
            var local = from a in query.AsEnumerable()
                        select new JiaoFuListInfo
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Unit = a.Unit,//单位
                            Price = a.Price ?? 0,//单价
                            PriceThod = (a.Price ?? 0).ThousandsSeparator(),
                            Amount = a.Amount ?? 0,//数量
                            TotalThod = a.SubTotal.ThousandsSeparator(),//小计
                            ComplateAmount = a.ComplateAmount ?? 0,//已完成已交付
                            PlanDateTime = a.PlanDateTime,//计划交付时间
                            ContId = a.ContId,
                            ContName = a.ContName,//合同名称
                            CompName = a.CompName,//合同对方名称
                            CompId=a.CompId,
                            CurrDelNum = search == 1 ? ((a.Amount ?? 0) - (a.ComplateAmount ?? 0)) : 0,//本次交付数量
                            NotDelNum = search == 1 ? 0 : (a.Amount ?? 0) - (a.ComplateAmount ?? 0),//未交付数量
                            NotOldDelNum= (a.Amount ?? 0) - (a.ComplateAmount ?? 0),//原始未交付数量
                            JfBl = GetJfBl(a.Amount, a.ComplateAmount, search),//比例
                            CurrDelmoney = search == 1 ? (((a.Amount ?? 0) - (a.ComplateAmount ?? 0)) * a.Price):0,//本次交付金额
                            CurrDelmoneyThod = search == 1 ? (((a.Amount ?? 0) - (a.ComplateAmount ?? 0)) * a.Price).ThousandsSeparator() : "0"
                        };
            var listdata = local.ToList();
            var summoney = listdata.Sum(a => a.CurrDelmoney ?? 0);//交付总额
            return new LayPageInfo<JiaoFuListInfo>()
            {
                data = listdata,
                count = pageInfo.TotalCount,
                code = 0,
                msg= summoney.ThousandsSeparator()


            };

        }
        /// <summary>
        /// 交付比例
        /// </summary>
        /// <param name="sl">数量</param>
        /// <param name="yjfSl">已交付数量</param>
        /// <param name="search">1:完全交付</param>
        /// <returns></returns>
        private string GetJfBl(decimal? sl, decimal? yjfSl, int? search)
        {
            

                decimal jfbl = 0;
                if ((sl ?? 0) != 0 || (yjfSl ?? 0) != 0)
                {
                    jfbl = (yjfSl ?? 0) / (sl ?? 0);
                }

                return jfbl.ConvertToPercent();
            


        }
        /// <summary>
        /// 用于 Distinct WOO_TEMPLATE_AND_OBJECT_FIELD：FIELD_TYPE,BC_ID
        /// </summary>
        private class TemplateAndObjectFieldComparer : IEqualityComparer<ContTxtTempAndSubField>
        {
            public bool Equals(ContTxtTempAndSubField x, ContTxtTempAndSubField y)
            {
                return x.FieldType == y.FieldType && x.BcId == y.BcId;
            }

            public int GetHashCode(ContTxtTempAndSubField obj)
            {
                return ((obj.FieldType ?? -999).ToString() + " " + (obj.BcId ?? -999).ToString()).GetHashCode();
            }

           
        }

        #region 插件使用信息
        /// <summary>
        /// 获取 业务类/非业务类 合同标的表格的标识符列表
        /// </summary>
        /// <param name="cttextid">合同文本ID</param>
        /// <returns>合同标的表格的标识符列表</returns>
        public List<ContractObjectTableIdentifier> GetContractObjectIdentifiers(int cttextid)
        {

            //var ContText =Db.Set<ContText>()
            //    .Include("ContractInfo")
            //    .Include("ContractInfo.ContSubjectMatter")
            //    .Include("ContractInfo.ContSubjectMatter.BcInstance")
            //    .Include("ContTxtTemplateHist")
            //    .Where(a => a.Id == cttextid).FirstOrDefault();
            var ContText = Db.Set<ContText>()
                .Include(a=>a.Cont)
                .Include(a=>a.Cont.ContSubjectMatters)
                .Include(a=>a.Template)
                .Where(a => a.Id == cttextid).FirstOrDefault();
            if (ContText == null)
                    return null;

                var Cont = ContText.Cont;
                var ContTextTempHist = ContText.Template;// Db.Set<ContTxtTemplateHist>().Where(a=>a.Id== ContText.TemplateId)//ContText.WOO_CONT_TEXT_TEMP_HIST;
                var lstSubjectMatter = new List<ContSubjectMatter>();
                if (Cont != null)
                {
                    lstSubjectMatter = Cont.ContSubjectMatters.ToList();
                }

                var Fields =Db.Set<ContTxtTempAndSubField>() 
                    .Where(a => a.TempHistId == ContText.TemplateId)
                    .ToList()
                    .Distinct(new TemplateAndObjectFieldComparer())
                    .ToList();

                var lst = new List<ContractObjectTableIdentifier>();

                foreach (var item in Fields)
                {
                    var info = new ContractObjectTableIdentifier();

                ContSubjectMatter subj = null;

                    switch (item.FieldType)
                    {
                        case (int)FieldTypeEnum.BusinessCategory:
                            {
                                if (item.BcId.HasValue && item.BcId.Value > 0)
                                {
                                    //业务类

                                    subj = lstSubjectMatter
                                        .Where(a => a.BcInstance != null && a.BcInstance.LbId == item.BcId.Value)
                                        .FirstOrDefault();

                                }
                                else
                                {
                                    //非业务类

                                    subj = lstSubjectMatter
                                        .Where(a => a.IsFromCategory == (int)IsFromCategoryEnum.NoClass)
                                        .FirstOrDefault();

                                }
                                break;
                            }
                        case (int)FieldTypeEnum.Unity:
                            {
                                subj = lstSubjectMatter
                                    .Where(a =>
                                        a.IsFromCategory == (int)IsFromCategoryEnum.NoClass ||
                                        a.IsFromCategory == (int)IsFromCategoryEnum.Class)
                                    .FirstOrDefault();

                                break;
                            }

                    }
                    info.SetValues(item);
                    info.SetValues(ContTextTempHist);
                    info.SetValues(subj);

                    lst.Add(info);
                }
                return lst;
            
            
        }
        #endregion

        /// <summary>
        /// 根据合同ID查询当前的标的总金额
        /// </summary>
        /// <param name="contId">负数时标识当前登录用户。正数合同ID</param>
        /// <returns></returns>
        public ContSubTotalInfo GetSubTotal(int contId, int userId)
        {
            ContSubTotalInfo s = new ContSubTotalInfo();
            var list = Db.Set<ContSubjectMatter>().Where(a => (a.ContId == contId||a.ContId==-userId) && a.IsDelete != 1).ToList();
            var subtol= list.Sum(a => (a.SubTotal ?? 0));
            s.TotalAm = subtol;
            s.TotalAmThod = subtol.ThousandsSeparator();
            return s;

        }
    }
}
