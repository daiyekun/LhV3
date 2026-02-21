using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NF.BLL.Extend;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NF.BLL
{
    public partial class InterviewpeopleService
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
        public LayPageInfo<InterviewpeopleDTO> GetKbqkList<s>(PageInfo<Interviewpeople> pageInfo, Expression<Func<Interviewpeople, bool>> whereLambda, Expression<Func<Interviewpeople, s>> orderbyLambda, bool isAsc)

        {
            var tempquery = this.Db.Set<Interviewpeople>().Where<Interviewpeople>(whereLambda.Compile()).AsQueryable();
            //  var tempquery = _InterviewpeopleSet.Where<Interviewpeople>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();


            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<Interviewpeople>))
            { //分页
                tempquery = tempquery.Skip<Interviewpeople>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<Interviewpeople>(pageInfo.PageSize);
            }
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Inquirer = a.Inquirer,
                            Name = a.Name,
                            Position = a.Position,
                            Department = a.Department,
                        };
            var local = from a in query.AsEnumerable()
                        select new InterviewpeopleDTO
                        {
                            Id = a.Id,
                            Inquirer = a.Inquirer,
                            InquirerName = this.Db.GetRedisUserFieldValue(a.Inquirer ?? -1),
                            //RedisValueUtility.GetUserShowName(a.Inquirer ?? 0), //创建人
                            Name = a.Name,
                            Position = a.Position,
                            DepartmentName = this.Db.GetRedisDeptNameValue(a.Department ?? -2),
                            //RedisValueUtility.GetDeptName(a.Department ?? 0),
                            Department = a.Department ?? 0
                        };
            return new LayPageInfo<InterviewpeopleDTO>()
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
            string sqlstr = "update Interviewpeople set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }
        /// <summary>
        /// 保存合同标的
        /// </summary>
        /// <param name="subjectMatter">合同标的</param>
        /// <returns>Id:->Hid:</returns>
        public bool AddSave(IList<InterviewpeopleDTO> subs, int contId, IMapper _IMapper)
        {

            var arrayIds = subs.Select(a => a.Id).ToArray();
            var query = Db.Set<Interviewpeople>().AsNoTracking().Where(a => arrayIds.Contains(a.Id)).ToList();
            IList<Interviewpeople> subjectMatters = new List<Interviewpeople>();
            foreach (Interviewpeople item in query)
            {
                var dtomel = subs.Where(a => a.Id == item.Id).FirstOrDefault();
                var submt = _IMapper.Map<InterviewpeopleDTO, Interviewpeople>(dtomel); //dtomel.ToModel<InterviewpeopleDTO, Interviewpeople>();
                submt.Name = submt.Name;
                submt.Position = submt.Position;
                submt.Department = submt.Department;
                submt.Inquirer = submt.Inquirer;
                submt.QuesId = (contId) <= 0 ? item.QuesId : contId;
                submt.IsDelete = item.IsDelete; //item.IsDelete;
                subjectMatters.Add(submt);
            }
            //添加历史
            this.Update(subjectMatters);
            return true;
        }
    }
}
