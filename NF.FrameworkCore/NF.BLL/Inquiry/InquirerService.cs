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
    public partial class InquirerService
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
        public LayPageInfo<InquirerDTO> GetKbqkList<s>(PageInfo<Inquirer> pageInfo, Expression<Func<Inquirer, bool>> whereLambda, Expression<Func<Inquirer, s>> orderbyLambda, bool isAsc)

        {
            var tempquery = _InquirerSet.Where<Inquirer>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();


            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<Inquirer>))
            { //分页
                tempquery = tempquery.Skip<Inquirer>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<Inquirer>(pageInfo.PageSize);
            }
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            InqId = a.InqId,
                            Name = a.Name,
                            Position = a.Position,
                            Department = a.Department,
                        };
            var local = from a in query.AsEnumerable()
                        select new InquirerDTO
                        {
                            Id = a.Id,
                            InqId = a.InqId,
                            InqName = this.Db.GetRedisUserFieldValue(a.InqId ?? -1),
                            //RedisValueUtility.GetUserShowName(a.InqId ?? 0), //创建人
                            Name = a.Name,
                            Position = a.Position,
                            DepartmentName = this.Db.GetRedisDeptNameValue(a.Department ?? -2),
                            //RedisValueUtility.GetDeptName(a.Department ?? 0),
                            Department = a.Department ?? 0
                        };
            return new LayPageInfo<InquirerDTO>()
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
            string sqlstr = "update Inquirer set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }
        /// <summary>
        /// 保存合同标的
        /// </summary>
        /// <param name="subjectMatter">合同标的</param>
        /// <returns>Id:->Hid:</returns>
        public bool AddSave(IList<InquirerDTO> subs, int contId)
        {

            var arrayIds = subs.Select(a => a.Id).ToArray();
            var query = Db.Set<Inquirer>().AsNoTracking().Where(a => arrayIds.Contains(a.Id)).ToList();
            IList<Inquirer> subjectMatters = new List<Inquirer>();
            foreach (Inquirer item in query)
            {
                var dtomel = subs.Where(a => a.Id == item.Id).FirstOrDefault();
                var submt = _IMapper.Map<InquirerDTO, Inquirer>(dtomel); //dtomel.ToModel<InquirerDTO, Inquirer>();
                submt.Name = submt.Name;
                submt.Position = submt.Position;
                submt.Department = submt.Department;
                submt.InqId = submt.InqId;
                submt.InquiryId = (contId) <= 0 ? item.InquiryId : contId;
                submt.IsDelete = item.IsDelete; //item.IsDelete;
                subjectMatters.Add(submt);
            }
            //添加历史
            this.Update(subjectMatters);
            return true;
        }
    }
}
