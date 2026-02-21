using Microsoft.EntityFrameworkCore;
using NF.AutoMapper;
using NF.BLL.Extend;
using NF.Common.Extend;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NF.BLL
{
    partial class OpeningSituationInforService
    {
        /// <summary>
        /// 保存招标情况
        /// </summary>
        /// <param name="OpeningSituationInfor">招标情况</param>
         public Dictionary<string, int> AddSave(OpeningSituationInfor OpeningSituationInfor)
        {
            var inof = Add(OpeningSituationInfor);
            var dic = new Dictionary<string, int>();
            dic.Add("Id", OpeningSituationInfor.Id);
            return dic;
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
        public LayPageInfo<OpeningSituationInforDTO> GetKbqkList<s>(PageInfo<OpeningSituationInfor> pageInfo, Expression<Func<OpeningSituationInfor, bool>> whereLambda, Expression<Func<OpeningSituationInfor, s>> orderbyLambda, bool isAsc)

        {
            var tempquery = _OpeningSituationInforSet.Include(a => a.UnitNavigation).AsTracking().Where<OpeningSituationInfor>(whereLambda.Compile()).AsQueryable();
                pageInfo.TotalCount = tempquery.Count();


            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<OpeningSituationInfor>))
            { //分页
                tempquery = tempquery.Skip<OpeningSituationInfor>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<OpeningSituationInfor>(pageInfo.PageSize);
            }
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            OpenSituationName = a.OpenSituationName,
                            Unit = a.Unit,
                            UnitName = a.Unit==0?"新建":a.UnitNavigation.Name,
                            TotalPrice = a.TotalPrice,
                            Uitprice = a.Uitprice,
                            UserId = a.UserId,
                        };
            var local = from a in query.AsEnumerable()
                        select new OpeningSituationInforDTO
                        {
                            Id = a.Id,
                            OpenSituationName = a.OpenSituationName,
                            Unit=a.Unit??0,
                            UnitName = a.UnitName,
                            TotalPrice = a.TotalPrice,
                           // TotalPrice = a.TotalPrice,
                            TotalPricethis=a.TotalPrice.ThousandsSeparator(),

                            Uitprice = a.Uitprice,
                            Uitpricethis=a.Uitprice.ThousandsSeparator(),
                            UserId = a.UserId,
                            UserName  = this.Db.GetRedisUserFieldValue(a.UserId),
                            //RedisValueUtility.GetUserShowName(a.UserId), //创建人
                        };
            return new LayPageInfo<OpeningSituationInforDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };


        }

        /// <summary>
        /// 保存合同标的
        /// </summary>
        /// <param name="subjectMatter">合同标的</param>
        /// <returns>Id:->Hid:</returns>
        public bool AddSave(IList<OpeningSituationInforDTO> subs,int contId)
        {

            var arrayIds = subs.Select(a => a.Id).ToArray();
            var query = Db.Set<OpeningSituationInfor>().AsNoTracking().Where(a => arrayIds.Contains(a.Id)).ToList();
            IList<OpeningSituationInfor> subjectMatters = new List<OpeningSituationInfor>();
           
            foreach (OpeningSituationInfor item in query)
            {
               
                var submt = subs.Where(a => a.Id == item.Id).FirstOrDefault();
              //  var submt = dtomel.ToModel<OpeningSituationInfor, OpeningSituationInforDTO>();
                submt.TotalPrice = ParseThousandthString(submt.TotalPricethis);
                submt.Uitprice = ParseThousandthString(submt.Uitpricethis);
                submt.OpenSituationName = submt.OpenSituationName;
                submt.Unit = submt.Unit;
                //submt.TotalPrice = submt.TotalPrice;
                //submt.Uitprice = submt.Uitprice;
                submt.UserId = submt.UserId;
                submt.TenderId = (contId) <= 0 ? item.TenderId : contId;
                submt.IsDelete = item.IsDelete; //item.IsDelete;


                subjectMatters.Add(submt);

            }
            //添加历史

            this.Update(subjectMatters);

            return true;


        }
        /// <summary>
        ///千分位转数字
        /// </summary>
        /// <param name="thousandthStr"></param>
        /// <returns></returns>
        public int ParseThousandthString(string thousandthStr)
        {
            int _value = -1;
            if (!string.IsNullOrEmpty(thousandthStr))
            {
                try
                {
                    _value = int.Parse(thousandthStr, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                }
                catch (Exception ex)
                {
                    _value = -1;
                    // Debug.WriteLine(string.Format("将千分位字符串{0}转换成数字异常，原因:{0}", thousandthStr, ex.Message));
                }
            }
            return _value;
        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">需要删除的Ids集合</param>
        /// <returns>受影响行数</returns>
        public int Delete(string Ids)
        {
            string sqlstr = "update OpeningSituationInfor set IS_DELETE=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }
    }
}
