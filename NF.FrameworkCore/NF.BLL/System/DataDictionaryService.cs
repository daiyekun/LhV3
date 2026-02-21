using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NF.ViewModel.Extend.Enums;
using System.Linq.Expressions;
using NF.ViewModel.Models;
using Microsoft.EntityFrameworkCore;
using NF.ViewModel.Models.Utility;
using NF.ViewModel;
using NF.ViewModel.Models.Common;

namespace NF.BLL
{
    /// <summary>
    /// 数据字典
    /// </summary>
    public partial class DataDictionaryService: BaseService<DataDictionary>,IDataDictionaryService
    {
        /// <summary>
        /// 获取枚举项属性
        /// </summary>
        /// <returns>返回枚举项集合</returns>
        public IList<EnumItemAttribute> GetListTypes()
        {
           return EmunUtility.GetAttr(typeof(DataDictionaryEnum));

        }
        /// <summary>
        /// 返回枚举类型下所有类别
        /// </summary>
        /// <param name="dataEnum">当前枚举类型</param>
        /// <returns></returns>
        public IList<DataDictionary> GetListByDataEnumType(DataDictionaryEnum dataEnum)
        {
            var dataint = (int)dataEnum;
            var list = _DataDictionarySet.Where(a => a.DtypeNumber == dataint&&a.IsDelete!=1).ToList();
            return list;


        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids">删除Id:1,2,3,4...</param>
        /// <returns></returns>
        public int Delete(string Ids)
        {  
            
            string sqlstr = "update  DataDictionary set IsDelete=1 where Id in("+ Ids + ")";
             return ExecuteSqlCommand(sqlstr);
        }
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="pageInfo">分页</param>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        public LayPageInfo<DataDictionaryDTO> GetList(PageInfo<DataDictionary> pageInfo, Expression<Func<DataDictionary, bool>> whereLambda) {
            //  var tempquery = Db.Set<DataDictionary>().AsNoTracking().Where<DataDictionary>(whereLambda.Compile()).AsQueryable();
            var tempquery = _DataDictionarySet.AsNoTracking().Where<DataDictionary>(whereLambda.Compile()).AsQueryable();
            tempquery = tempquery.OrderByDescending(a => a.Id);
            if(!(pageInfo is NoPageInfo<DataDictionary>)) {
                pageInfo.TotalCount = tempquery.Count();
                tempquery = tempquery.Skip<DataDictionary>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<DataDictionary>(pageInfo.PageSize);
            }
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Dtype = a.Dtype,
                            Remark = a.Remark,
                            FundsNature = a.FundsNature,
                            DtypeNumber = a.DtypeNumber,
                            ShortName=a.ShortName,


                        };
            var local = from a in query.AsEnumerable()
                        select new DataDictionaryDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Dtype = a.Dtype,
                            Remark = a.Remark,
                            FundsNature = a.FundsNature,
                            DtypeNumber = a.DtypeNumber,
                            ShortName = a.ShortName,
                            FundDic = EmunUtility.GetDesc(typeof(FinceType), a.FundsNature??-1)
                            //IsMainDic = EmunUtility.GetDesc(typeof(OtherDataState), a.IsMain ?? 0)


                        };
            return new LayPageInfo<DataDictionaryDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }
        /// <summary>
        /// 查看修改
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns></returns>
        public  DataDictionaryDTO ShowView(int Id)
        {
            var tempInfo = _DataDictionarySet.Where(a => a.Id == Id).FirstOrDefault();
            var info = new DataDictionaryDTO
            {
                Id = tempInfo.Id,
                Name = tempInfo.Name,
                Dtype = tempInfo.Dtype,
                Remark = tempInfo.Remark,
                FundsNature = tempInfo.FundsNature,
                DtypeNumber = tempInfo.DtypeNumber,
                FundDic = EmunUtility.GetDesc(typeof(FinceType), tempInfo.FundsNature ?? -1),
                Pid = tempInfo.Pid,
                ShortName = tempInfo.ShortName,

            };
            return info;
        }
        /// <summary>
        /// 存储Redis
        /// </summary>
        public void SetRedis()
        {
            var pageInfo = new NoPageInfo<DataDictionary>();
            var list = GetList(pageInfo, a => a.IsDelete == 0);
            foreach (DataDictionaryDTO dc in list.data)
            {

                SysIniInfoUtility.SetDataDic(dc, StaticData.RedisDataKey, (a, b, c) => {
                    return $"{a}:{b}:{c}";
                });

            }
        }

        /// <summary>
        /// 根据类型和关键字查下计划对象
        /// </summary>
        /// <returns></returns>
        public IList<int> GetDicKes(string keyWord, DataDictionaryEnum dictionaryEnum)
        {
            if (!string.IsNullOrEmpty(keyWord))
            {
                var list = _DataDictionarySet.Where(a => a.Name.Contains(keyWord) && a.DtypeNumber == (int)dictionaryEnum)
                    .Select(a => a.Id).ToList();
                return list;

            }
            else
            {
                return new List<int>();
            }
        }
        /// <summary>
        /// 根据类型和关键字查部门
        /// </summary>
        /// <returns></returns>
        public IList<int> GetDepDicKes(string keyWord)
        {
            if (!string.IsNullOrEmpty(keyWord))
            {
                var list = this.Db.Set<Department>().Where(a => a.Name.Contains(keyWord))
                    .Select(a => a.Id).ToList();
                return list;

            }
            else
            {
                return new List<int>();
            }
        }

        #region treeselect需要数据
        /// <summary>
        /// 获取数据字典
        /// </summary>
        /// <returns></returns>
        public IList<DataDictionary> GetListAll(DataDictionaryEnum dictionaryEnum)
        {
            var list = _DataDictionarySet.Where(a =>a.DtypeNumber == (int)dictionaryEnum&&a.IsDelete==0)
                  .ToList();
            return list;

        }
        /// <summary>
        /// 返回LayUI Tree需要数据格式
        /// </summary>
        /// <returns></returns>
        public IList<TreeSelectInfo> GetTreeSelectData(DataDictionaryEnum dictionaryEnum)
        {
            IList<TreeSelectInfo> listTree = new List<TreeSelectInfo>();
            var listAll = GetListAll(dictionaryEnum);
            var list = listAll.Where(a => a.IsDelete == 0 ).ToList();
            foreach (var item in list.Where(a => (a.Pid??0) == 0))
            {
                TreeSelectInfo treeInfo = new TreeSelectInfo();
                treeInfo.id = item.Id;
                treeInfo.title = item.Name;
                treeInfo.name = item.Name;
                RecursionChrenNode(list, treeInfo, item);
                listTree.Add(treeInfo);

            }
            return listTree;
        }

        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="listDepts">数据列表</param>
        /// <param name="treeInfo">Tree对象</param>
        /// <param name="item">父类对象</param>
        public void RecursionChrenNode(IList<DataDictionary> listDepts, TreeSelectInfo treeInfo, DataDictionary item)
        {
            var listchren = listDepts.Where(a => a.Pid == item.Id);
            var listchrennode = new List<TreeSelectInfo>();
            if (listchren.Any())
            {
                foreach (var chrenItem in listchren.ToList())
                {
                    TreeSelectInfo treeInfotmp = new TreeSelectInfo();
                    treeInfotmp.id = chrenItem.Id;
                    treeInfotmp.title = chrenItem.Name;
                    treeInfotmp.name = chrenItem.Name;

                    RecursionChrenNode(listDepts, treeInfotmp, chrenItem);
                    listchrennode.Add(treeInfotmp);
                }
                treeInfo.children = listchrennode;

            }



        }
        #endregion

        public int GetDataDicID(string name)
        {
            int count = _DataDictionarySet.Where(p => p.Name == name).Count();
            if (count > 0)
            {
                return _DataDictionarySet.Where(p => p.Name == name).FirstOrDefault().Id;
            }
            else
            {
                return 0;
            }
        }




        public int BzSelect(string name)
        {
            int count = Db.Set<CurrencyManager>().Where(p => p.Name == name && p.IsDelete == 0).Count();
            if (count > 0)
            {
                return Db.Set<CurrencyManager>().Where(p => p.Name == name && p.IsDelete == 0).FirstOrDefault().Id;
            }
            else
            {
                return 0;
            }
        }
        public int GetZzjg(string name)
        {
            int count = Db.Set<Department>().Where(p => p.Name == name && p.IsDelete == 0).Count();
            if (count > 0)
            {
                return Db.Set<Department>().Where(p => p.Name == name && p.IsDelete == 0).FirstOrDefault().Id;
            }
            else
            {
                return 0;
            }
        }
        public int GetYh(string name)
        {
            int count = Db.Set<UserInfor>().Where(p => p.Name == name && p.IsDelete == 0).Count();
            if (count > 0)
            {
                return Db.Set<UserInfor>().Where(p => p.Name == name && p.IsDelete == 0).FirstOrDefault().Id;
            }
            else
            {
                return 0;
            }
        }
        public int GetQyzt(string name)
        {
            int count = Db.Set<Department>().Where(p => p.Name == name && p.IsMain == 1 && p.IsDelete == 0).Count();
            if (count > 0)
            {
                return Db.Set<Department>().Where(p => p.IsMain == 1 && p.Name == name && p.IsDelete == 0).FirstOrDefault().Id;
            }
            else
            {
                return 0;
            }
        }
        public int GetHtlyID(string name)
        {
            int count = _DataDictionarySet.Where(p => p.Name == name && p.DtypeNumber == 15 && p.IsDelete == 0).Count();
            if (count > 0)
            {
                return _DataDictionarySet.Where(p => p.Name == name && p.DtypeNumber == 15 && p.IsDelete == 0).FirstOrDefault().Id;
            }
            else
            {
                return 0;
            }
        }
    }
}
