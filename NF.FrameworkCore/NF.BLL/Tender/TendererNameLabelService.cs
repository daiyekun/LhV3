using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NF.AutoMapper;
using NF.BLL.Extend;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NF.BLL
{
    public partial class TendererNameLabelService
    {
        private IMapper _IMapper;
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<TendererNameLabelDTO> GetKbqkList<s>(PageInfo<TendererNameLabel> pageInfo, Expression<Func<TendererNameLabel, bool>> whereLambda, Expression<Func<TendererNameLabel, s>> orderbyLambda, bool isAsc)

        {
            var tempquery = _TendererNameLabelSet.Where<TendererNameLabel>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();


            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<TendererNameLabel>))
            { //分页
                tempquery = tempquery.Skip<TendererNameLabel>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<TendererNameLabel>(pageInfo.PageSize);
            }
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            UserId = a.UserId,
                            TeNameLabel=a.TeNameLabel,
                            Psition = a.Psition,
                            TeDartment = a.TeDartment,
                        };
            var local = from a in query.AsEnumerable()
                        select new TendererNameLabelDTO
                        {
                            Id = a.Id,
                            UserId = a.UserId??0,
                            UserName = this.Db.GetRedisUserFieldValue(a.UserId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.UserId??0), //创建人
                            TeNameLabel = a.TeNameLabel,
                            Psition = a.Psition,
                            TeDartmentName = this.Db.GetRedisDeptNameValue(a.TeDartment ?? -2),
                            //RedisValueUtility.GetDeptName(a.TeDartment??0),
                            TeDartment= a.TeDartment??0
                        };
            return new LayPageInfo<TendererNameLabelDTO>()
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
            string sqlstr = "update TendererNameLabel set IS_DELETE=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }
        /// <summary>
        /// 保存合同标的
        /// </summary>
        /// <param name="subjectMatter">合同标的</param>
        /// <returns>Id:->Hid:</returns>
        public bool AddSave(IList<TendererNameLabelDTO> subs, int contId, IMapper _IMapper)
        {

            var arrayIds = subs.Select(a => a.Id).ToArray();
            var query = Db.Set<TendererNameLabel>().AsNoTracking().Where(a => arrayIds.Contains(a.Id)).ToList();
            IList<TendererNameLabel> subjectMatters = new List<TendererNameLabel>();
            foreach (TendererNameLabel item in query)
            {
                var dtomel = subs.Where(a => a.Id == item.Id).FirstOrDefault();
                var submt = _IMapper.Map<TendererNameLabelDTO, TendererNameLabel>(dtomel);// dtomel.ToModel<TendererNameLabelDTO, TendererNameLabel>();
                submt.TeNameLabel = submt.TeNameLabel;
                submt.Psition = submt.Psition;
                submt.TeDartment = submt.TeDartment;
                submt.UserId = submt.UserId;
                submt.TenderId = (contId) <= 0 ? item.TenderId : contId;
                submt.IsDelete = item.IsDelete; //item.IsDelete;
                subjectMatters.Add(submt);
            }
            //添加历史
            this.Update(subjectMatters);
            return true;
        }

    }
}
