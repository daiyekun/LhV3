using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NF.Model;
using NF.ViewModel.Models.LayUI;
using NF.Model.Models;
using NF.ViewModel.Models.Common;
using NF.Common.Utility;

namespace NF.BLL
{
    /// <summary>
    /// 业务品类
    /// </summary>
    public partial class BusinessCategoryService
    {
        /// <summary>
        /// 查询所有树
        /// </summary>
        /// <returns></returns>
        public IList<BusinessCategoryDTO> GetListAll()
        {
            IList<BusinessCategoryDTO> list = RedisHelper.StringGetToList<BusinessCategoryDTO>("NF-BcCateGoryListAll");
            if (list == null)
            {
                var query = from a in _BusinessCategorySet
                            where a.IsDelete == 0
                            select new
                            {
                                Id = a.Id,
                                Pid = a.Pid,
                                Name = a.Name,
                                Code = a.Code

                            };
                var local = from a in query.AsEnumerable()
                            select new BusinessCategoryDTO
                            {
                                Id = a.Id,
                                Pid = a.Pid,
                                Name = a.Name,
                                Code = a.Code
                            };
                 list = local.ToList();
                RedisHelper.ListObjToJsonStringSetAsync("NF-BcCateGoryListAll", list);

            }
            return list;
        }
        /// <summary>
        /// 转换树
        /// </summary>
        /// <returns></returns>
        public IList<LayTree> GetLayUITreeData()
        {


            var list = GetListAll();
            return GetLayTree(list);


        }

        #region 递归树

        /// <summary>
        /// 返回LayUI Tree需要数据格式
        /// </summary>
        /// <returns></returns>
        public IList<LayTree> GetLayTree(IList<BusinessCategoryDTO> list)
        {
            IList<LayTree> listTree = new List<LayTree>();

            foreach (var item in list.Where(a => a.Pid == 0))
            {
                LayTree treeInfo = new LayTree();
                treeInfo.id = item.Id;
                treeInfo.title = item.Name;

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
        public void RecursionChrenNode(IList<BusinessCategoryDTO> listDepts, LayTree treeInfo, BusinessCategoryDTO item)
        {
            var listchren = listDepts.Where(a => a.Pid == item.Id);
            var listchrennode = new List<LayTree>();
            if (listchren.Any())
            {
                foreach (var chrenItem in listchren.ToList())
                {
                    LayTree treeInfotmp = new LayTree();
                    treeInfotmp.id = chrenItem.Id;
                    treeInfotmp.title = chrenItem.Name;
                    RecursionChrenNode(listDepts, treeInfotmp, chrenItem);
                    listchrennode.Add(treeInfotmp);
                }

                treeInfo.children = listchrennode;

            }


        }
        #endregion
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="business">业务品类对象</param>
        /// <returns></returns>
        public BusinessCategory AddSave(BusinessCategory business)
        {
            RedisHelper.KeyDelete("NF-BcCateGoryListAll");
            return Add(business);

        }
        /// <summary>
        /// 根据ID查询当前对象
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public BusinessCategoryViewDTO GetTreeDataById(int Id)
        {
            var list = GetListAll();
            var query = from a in list
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Pid = a.Pid,
                            Code = a.Code

                        };
            var locl = from a in query.AsEnumerable()
                       select new BusinessCategoryViewDTO
                       {
                           Id = a.Id,
                           Name = a.Name,
                           Code = a.Code,
                           PName = list.Where(p => p.Id == a.Pid).Any() ? list.Where(p => p.Id == a.Pid).FirstOrDefault().Name : "",
                           Pid = a.Pid
                       };


            return locl.FirstOrDefault();


        }
        /// <summary>
        /// 删除当前对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int DeleteInfo(int Id)
        {
            RedisHelper.KeyDelete("NF-BcCateGoryListAll");
            var sqlstr = $"update BusinessCategory set IsDelete=1 where Id={Id}";
           return ExecuteSqlCommand(sqlstr);
        }


        #region treeselect需要数据
        /// <summary>
        /// 返回LayUI Tree需要数据格式
        /// </summary>
        /// <returns></returns>
        public IList<TreeSelectInfo> GetTreeselect()
        {
            IList<TreeSelectInfo> listTree = new List<TreeSelectInfo>();
            var listAll = GetListAll();
            //var list = listAll.ToList();
            foreach (var item in listAll.Where(a => a.Pid == 0))
            {
                TreeSelectInfo treeInfo = new TreeSelectInfo();
                treeInfo.id = item.Id;
                treeInfo.title = item.Name;
                treeInfo.name= item.Name;
                RecursionChrenNode(listAll, treeInfo, item);
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
        public void RecursionChrenNode(IList<BusinessCategoryDTO> listDepts, TreeSelectInfo treeInfo, BusinessCategoryDTO item)
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
                    treeInfotmp.name= chrenItem.Name;
                    RecursionChrenNode(listDepts, treeInfotmp, chrenItem);
                    listchrennode.Add(treeInfotmp);
                }
                treeInfo.children = listchrennode;

            }



        }
        #endregion

        /// <summary>
        /// 校验某一字段值是否已经存在
        /// </summary>
        /// <param name="fieldInfo">字段相关信息</param>
        /// <returns>True:存在/False不存在</returns>
        public bool CheckInputValExist(UniqueFieldInfo fieldInfo)
        {
            var predicateAnd = PredicateBuilder.True<BusinessCategory>();
            //不等于fieldInfo.CurrId是排除修改的时候
            predicateAnd = predicateAnd.And(a => a.IsDelete == 0 && a.Id != fieldInfo.Id);
            switch (fieldInfo.FieldName)
            {
                case "Name":
                    predicateAnd = predicateAnd.And(a => a.Name == fieldInfo.FieldValue);
                    break;
                case "Code":
                    predicateAnd = predicateAnd.And(a => a.Code == fieldInfo.FieldValue);
                    break;

            }
            return GetQueryable(predicateAnd).AsNoTracking().Any();

        }
        /// <summary>
        /// 查询子类
        /// </summary>
        /// <returns></returns>
        public IList<int> GetCateIds(int Id)
        {
            var query = from a in _BusinessCategorySet
                        where a.IsDelete == 0
                        select a;
            var list = query.ToList();
            IList<int> listIds = new List<int>();
            listIds.Add(Id);

            var chidIds = list.Where(a => a.Pid == Id).Select(a => a.Id).ToList();
            foreach (var tid in chidIds)
            {
                listIds.Add(tid);
            }
            GetChidIds(listIds, list, chidIds);

            return listIds;

        }

        public void GetChidIds(IList<int> Ids, IList<BusinessCategory> cates,IList<int>PidIds)
        {
            var lists = cates.Where(a => PidIds.Contains(a.Pid ?? -100)).Select(a=>a.Id).ToList();
            if (lists!=null&&lists.Count>0)
            {
                foreach (var tid in lists)
                {
                    Ids.Add(tid);
                }

                GetChidIds(Ids, cates, lists);
            }
        }

    }
}
