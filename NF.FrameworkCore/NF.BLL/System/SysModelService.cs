using Microsoft.EntityFrameworkCore;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NF.BLL
{
    /// <summary>
    /// 模块
    /// </summary>
   public partial class SysModelService
    {
        /// <summary>
        /// 查询模块列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
       public LayPageInfo<SysModelDTO> GetList(PageInfo<SysModel> pageInfo, Expression<Func<SysModel, bool>> whereLambda)
        {
            var tempquery =_SysModelSet.AsTracking().Where<SysModel>(whereLambda);
            pageInfo.TotalCount = tempquery.Count();
            tempquery = tempquery.OrderBy(a=>a.Sort);
            tempquery = tempquery.Skip<SysModel>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<SysModel>(pageInfo.PageSize);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Pid = a.Pid,
                            No = a.No,
                            ControllerName=a.ControllerName,
                            ActionName=a.ActionName,
                            RequestUrl=a.RequestUrl,
                            Remark=a.Remark,
                            IsShow=a.IsShow,
                            AreaName=a.AreaName,
                            CreateUserName=a.CreateUser.Name,
                            CreateDatetime = a.CreateDatetime,
                            ModifyUserName=a.ModifyUser.Name,
                            ModifyDatetime=a.ModifyDatetime,
                            Ico = a.Ico,
                            Sort=a.Sort

                        };
            var local = from a in query.AsEnumerable()
                        select new SysModelDTO
                        {
                            Id = a.Id,
                            Pid = a.Pid,
                            Name = a.Name,
                            No = a.No,
                            ControllerName = a.ControllerName,
                            ActionName = a.ActionName,
                            RequestUrl = a.RequestUrl,
                            Remark = a.Remark,
                            IsShow = a.IsShow,
                            AreaName = a.AreaName,
                            CreateUserName = a.CreateUserName,
                            CreateDatetime = a.CreateDatetime,
                            ModifyUserName = a.ModifyUserName,
                            ModifyDatetime = a.ModifyDatetime,
                            Ico = a.Ico,
                            Sort=a.Sort


                        };
            return new LayPageInfo<SysModelDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public IList<SysModelDTO> GetListAll()
        {
            IList<SysModelDTO> list = RedisHelper.StringGetToList<SysModelDTO>("Nf-SysModelListAll");
            if (list == null)
            {
                var tempquery = _SysModelSet.AsNoTracking().OrderBy(a=>a.Sort);
                var query = from a in tempquery
                            select new
                            {
                                Id = a.Id,
                                Pid = a.Pid,
                                Name = a.Name,
                                No = a.No,
                                ControllerName = a.ControllerName,
                                ActionName = a.ActionName,
                                RequestUrl = a.RequestUrl,
                                Remark = a.Remark,
                                IsShow = a.IsShow,
                                AreaName = a.AreaName,
                                CreateUserName = a.CreateUser.Name,
                                CreateDatetime = a.CreateDatetime,
                                ModifyUserName = a.ModifyUser.Name,
                                ModifyDatetime = a.ModifyDatetime,
                                IsDelete=a.IsDelete,
                                PName = Db.Set<SysModel>().AsNoTracking().Where(p=>p.Id==a.Pid).Any() ? Db.Set<SysModel>().AsNoTracking().Where(p => p.Id == a.Pid).FirstOrDefault().Name : "",
                                Ico=a.Ico,
                                Sort= a.Sort,
                                Mpath=a.Mpath,
                                Leaf=a.Leaf,



                            };
                var local = from a in query.AsEnumerable()
                            select new SysModelDTO
                            {
                                Id = a.Id,
                                Pid = a.Pid,
                                Name = a.Name,
                                No = a.No,
                                ControllerName = a.ControllerName,
                                ActionName = a.ActionName,
                                RequestUrl = a.RequestUrl,
                                Remark = a.Remark,
                                IsShow = a.IsShow,
                                AreaName = a.AreaName,
                                CreateUserName = a.CreateUserName,
                                CreateDatetime = a.CreateDatetime,
                                ModifyUserName = a.ModifyUserName,
                                ModifyDatetime = a.ModifyDatetime,
                                IsDelete = a.IsDelete,
                                IsShowDic = EmunUtility.GetDesc(typeof(OtherDataState), a.IsShow ?? 0),
                                Ico = a.Ico,
                                Sort = a.Sort,
                                PName=a.PName,
                                id = a.Id,
                                pid = a.Pid ?? 0,
                                Mpath = a.Mpath,
                                Leaf=a.Leaf,

                            };
                list = local.ToList();

                RedisHelper.ListObjToJsonStringSetAsync("Nf-SysModelListAll", list);
            }
            return list;

        }

        #region 递归 下拉选择树
        public IList<TreeInfo> GetTree()
        {
            IList<TreeInfo> listTree = new List<TreeInfo>();
            var listAll = GetListAll();
            var list = listAll.Where(a => a.IsDelete == 0 && a.IsShow == 1).ToList();
            foreach (var item in list.Where(a => a.Pid == -1))
            {
                TreeInfo treeInfo = new TreeInfo();
                treeInfo.id = item.Id;
                treeInfo.title = item.Name;
                treeInfo.spread = true;
                RecursionChrenNode(list, treeInfo, item);
                listTree.Add(treeInfo);

            }


            return listTree;
        }
        /// <summary>
        /// 返回LayUI Tree需要数据格式
        /// </summary>
        /// <returns></returns>
        public IList<TreeInfo> GetTree(int IsUser, int userIdorRoleId, bool fpQx)
        {
            IList<TreeInfo> listTree = new List<TreeInfo>();
            var listAll = GetListAll();
            if (fpQx)
            {
                if (IsUser == 0)
                {//用户权限分配
                    var listmodeIs = this.Db.Set<UserModule>().Where(a => a.UserId == userIdorRoleId).Select(a => a.ModuleId).ToList();
                    var list = listAll.Where(a => (a.IsDelete == 0 && a.IsShow == 1 && listmodeIs.Any(p => p == a.id)) || a.Pid == -1).ToList();
                    foreach (var item in list.Where(a => a.Pid == -1))
                    {
                        TreeInfo treeInfo = new TreeInfo();
                        treeInfo.id = item.Id;
                        treeInfo.title = item.Name;
                        treeInfo.spread = true;
                        RecursionChrenNode(list, treeInfo, item);
                        listTree.Add(treeInfo);

                    }


                }
                else
                {
                    var listmodeIs = this.Db.Set<RoleModule>().Where(a => a.RoleId == userIdorRoleId).Select(a => a.ModuleId).ToList();
                    var list = listAll.Where(a => (a.IsDelete == 0 && a.IsShow == 1 && listmodeIs.Any(p => p == a.id)) || a.Pid == -1).ToList();
                    foreach (var item in list.Where(a => a.Pid == -1))
                    {
                        TreeInfo treeInfo = new TreeInfo();
                        treeInfo.id = item.Id;
                        treeInfo.title = item.Name;
                        treeInfo.spread = true;
                        RecursionChrenNode(list, treeInfo, item);
                        listTree.Add(treeInfo);

                    }
                }

            }
            else
            {
                var list = listAll.Where(a => a.IsDelete == 0 && a.IsShow == 1).ToList();
                foreach (var item in list.Where(a => a.Pid == -1))
                {
                    TreeInfo treeInfo = new TreeInfo();
                    treeInfo.id = item.Id;
                    treeInfo.title = item.Name;
                    treeInfo.spread = true;
                    RecursionChrenNode(list, treeInfo, item);
                    listTree.Add(treeInfo);

                }
            }
            return listTree;
        }

        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="listDepts">数据列表</param>
        /// <param name="treeInfo">Tree对象</param>
        /// <param name="item">父类对象</param>
        public void RecursionChrenNode(IList<SysModelDTO> listDepts, TreeInfo treeInfo, SysModelDTO item)
        {
            var listchren = listDepts.Where(a => a.Pid == item.Id);
            var listchrennode = new List<TreeInfo>();
            if (listchren.Any())
            {
                foreach (var chrenItem in listchren.ToList())
                {
                    TreeInfo treeInfotmp = new TreeInfo();
                    treeInfotmp.id = chrenItem.Id;
                    treeInfotmp.title = chrenItem.Name;


                    RecursionChrenNode(listDepts, treeInfotmp, chrenItem);
                    listchrennode.Add(treeInfotmp);
                }

                treeInfo.children = listchrennode;

            }


        }

        #endregion

        #region 递归XTree-主要用于菜单权限分配

        /// <summary>
        /// 返回LayUI Tree需要数据格式
        /// </summary>
        /// <returns></returns>
        public IList<XTree> GetXtTree(IList<int>Ids)
        {
            
            IList<XTree> listTree = new List<XTree>();
            var listAll = GetListAll();
            var list = listAll.Where(a => a.IsDelete == 0 && a.IsShow == 1).ToList();
            foreach (var item in list.Where(a => a.Pid == -1))
            {
                XTree treeInfo = new XTree();
                treeInfo.value = item.Id.ToString();
                treeInfo.title = item.Name;
                treeInfo.Checked = (Ids!=null&& Ids.Contains(item.Id)) ? true:false;
              
                RecursionChrenNodeXt(list, treeInfo, item, Ids);
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
        public void RecursionChrenNodeXt(IList<SysModelDTO> listDepts, XTree treeInfo, SysModelDTO item, IList<int> Ids)
        {
            var listchren = listDepts.Where(a => a.Pid == item.Id);
            var listchrennode = new List<XTree>();
            if (listchren.Any())
            {
                foreach (var chrenItem in listchren.ToList())
                {
                    XTree treeInfotmp = new XTree();
                    treeInfotmp.value = chrenItem.Id.ToString();
                    treeInfotmp.title = chrenItem.Name;
                    treeInfotmp.Checked = (Ids!=null&& Ids.Contains(chrenItem.Id)) ? true : false;

                    RecursionChrenNodeXt(listDepts, treeInfotmp, chrenItem, Ids);
                    listchrennode.Add(treeInfotmp);
                }

                treeInfo.data = listchrennode;

            }


        }

        #region treeselect需要数据
        /// <summary>
        /// 返回LayUI Tree需要数据格式
        /// </summary>
        /// <returns></returns>
        public IList<TreeSelectInfo> GetModelTreeSelect()
        {
            IList<TreeSelectInfo> listTree = new List<TreeSelectInfo>();
            var listAll = GetListAll();
            var list = listAll.Where(a => a.IsDelete == 0 && a.IsShow == 1).ToList();
            foreach (var item in list.Where(a => a.Pid == -1))
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
        public void RecursionChrenNode(IList<SysModelDTO> listDepts, TreeSelectInfo treeInfo, SysModelDTO item)
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



        #endregion 


        /// <summary>
        /// 判断某一字段值是否唯一
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="inputValue">输入值</param>
        /// <param name="Id">修改时的ID</param>
        /// <returns>True表示存在，否则不存在</returns>
        public bool CheckFieldValExist(string fieldName, string inputValue, int? Id)
        {
            bool result = false;
            switch (fieldName)
            {
                case "Name":
                    result = _SysModelSet.AsNoTracking().Any(a => a.Name == inputValue && a.IsDelete != 1 && a.Id != Id);
                    break;

            }
            return result;

        }
        /// <summary>
        /// 保存信息
        /// </summary>
        /// <param name="userInfo">保存菜单信息</param>
        /// <returns>返回当前保存对象</returns>
        public SysModel SaveInfo(SysModel info)
        {
            SysModel resul = null;
            if (info.Id > 0)
            {//修改
               
                resul = UpdateSave(info);

            }
            else
            {
                resul = AddSave(info);
            }
            RedisHelper.KeyDeleteAsync("Nf-SysModelListAll");
            return resul;



        }
        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="info">修改对象</param>
        /// <returns></returns>
        private SysModel UpdateSave(SysModel info)
        {
            
            var tempinfo = _SysModelSet.FirstOrDefault(a => a.Id == info.Id);
            tempinfo.Name = info.Name;
           // tempinfo.No = info.No;
            tempinfo.Pid = info.Pid;
            tempinfo.RequestUrl = info.RequestUrl;
            tempinfo.Ico = info.Ico;
            tempinfo.ActionName=info.ActionName;
            tempinfo.AreaName = info.AreaName;
            tempinfo.ControllerName = info.ControllerName;
            tempinfo.Remark = info.Remark;
            tempinfo.Mpath = info.Mpath;
            tempinfo.Leaf = info.Leaf;
            //tempinfo.Sort = info.Sort;
            tempinfo.ModifyUserId = info.ModifyUserId;
            tempinfo.ModifyDatetime = DateTime.Now;
            
            Db.SaveChanges();
            return tempinfo;
        }
        /// <summary>
        /// 新增保存
        /// </summary>
        /// <param name="userInfo">新增对象</param>
        /// <returns>当前保存对象</returns>
        private SysModel AddSave(SysModel userInfo)
        {
            var list = GetListAll();
            userInfo.CreateDatetime = DateTime.Now;
            userInfo.CreateUserId = userInfo.CreateUserId;
            userInfo.IsDelete = 0;
            userInfo.IsShow = 1;
            userInfo.No = "M" + (list.Count()+1).ToString().PadLeft(3,'0');
            userInfo.Sort = (list.Count() + 1);
            return Add(userInfo);

        }
        /// <summary>
        /// 显示查看基本信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public SysModelDTO ShowView(int Id)
        {

            var tempInfo = _SysModelSet.Where(a => a.Id == Id).Include(a => a.CreateUser).FirstOrDefault();
            var info = new SysModelDTO
            {
                Id = tempInfo.Id,
                Name = tempInfo.Name,
                No = tempInfo.No,
                ControllerName = tempInfo.ControllerName,
                ActionName = tempInfo.ActionName,
                RequestUrl = tempInfo.RequestUrl,
                Remark = tempInfo.Remark,
                IsShow = tempInfo.IsShow,
                AreaName = tempInfo.AreaName,
                CreateUserName = tempInfo.CreateUser.Name,
                CreateDatetime = tempInfo.CreateDatetime,
                PName = Db.Set<SysModel>().AsNoTracking().Where(d => tempInfo.Pid == d.Id).Any() ? Db.Set<SysModel>().AsNoTracking().Where(d => tempInfo.Pid == d.Id).FirstOrDefault().Name : "",
                Ico = tempInfo.Ico,
                Sort = tempInfo.Sort,
                Pid=tempInfo.Pid,

            };
            info.IsShowDic = EmunUtility.GetDesc(typeof(OtherDataState), tempInfo.IsShow??0);
          
            return info;


        }
        /// <summary>
        /// 修改字段值
        /// </summary>
        /// <param name="info">修改字段新</param>
        /// <returns>返回受影响行数</returns>
        public int UpdateField(UpdateFieldInfo info)
        {
            string sqlstr = "";
            switch (info.FieldName)
            {
                case "IsShow"://状态
                    {
                        int state = 0;
                        int.TryParse(info.FieldValue, out state);
                        sqlstr = "update SysModel set IsShow=" + state + " where Id=" + info.Id;
                    }
                    break;
                case "Sort":
                    {
                        StringBuilder strb = new StringBuilder();
                        var listAll = GetListAll();
                        if (info.FieldValue == "0")
                        {//上移
                            var currinfo = listAll.Where(a => a.Id == info.Id).FirstOrDefault();
                            if (currinfo != null)
                            {
                                var upInfo = listAll.Where(a => a.Pid == currinfo.Pid && a.Sort < currinfo.Sort).OrderByDescending(a => a.Sort).FirstOrDefault();
                                if (upInfo != null)
                                {
                                    strb.Append("update SysModel set Sort=" + upInfo.Sort + " where Id=" + info.Id + ";");
                                    strb.Append("update SysModel set Sort=" + currinfo.Sort + " where Id=" + upInfo.Id + ";");
                                    sqlstr = strb.ToString();
                                }
                            }

                        }
                        else if (info.FieldValue == "1")
                        {
                            var currinfo = listAll.Where(a => a.Id == info.Id).FirstOrDefault();
                            if (currinfo != null)
                            {
                                var dowInfo = listAll.Where(a => a.Pid == currinfo.Pid && a.Sort > currinfo.Sort).OrderBy(a => a.Sort).FirstOrDefault();
                                if (dowInfo != null)
                                {
                                    strb.Append("update SysModel set Sort=" + dowInfo.Sort + " where Id=" + info.Id + ";");
                                    strb.Append("update SysModel set Sort=" + currinfo.Sort + " where Id=" + dowInfo.Id + ";");
                                    sqlstr = strb.ToString();
                                }
                            }

                        }
                    }
                    break;


            }
            RedisHelper.KeyDeleteAsync("Nf-SysModelListAll");
            if (!string.IsNullOrEmpty(sqlstr))
                return ExecuteSqlCommand(sqlstr);
           
            return 0;

        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">软删除ID集合</param>
        /// <returns></returns>
        public int Delete(string Ids)
        {
            string sqlstr = "update SysModel set IsDelete=1 where Id in(" + Ids + ")";
            RedisHelper.KeyDeleteAsync("Nf-SysModelListAll");
            return ExecuteSqlCommand(sqlstr);
        }

        #region 左侧菜单递归
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="mIds">菜单ID集合</param>
        /// <returns>返回菜单集合</returns>
        public IList<LeftTree> GetLeftTree(IList<int> mIds,int userId)
        {
            IList<LeftTree> listTree = new List<LeftTree>();
            var listAll = GetListAll();
            var rootInfo = listAll.Where(a => a.pid == -1).FirstOrDefault();
            IList<SysModelDTO> list = null;
            if (userId== -10000)
            {//超级管理员
                list = listAll.Where(a =>a.IsDelete == 0 && a.IsShow == 1).OrderBy(a => a.Sort).ToList();
            }
            else {
             list = listAll.Where(a => mIds.Contains(a.id)&&a.IsDelete == 0 && a.IsShow == 1).OrderBy(a=>a.Sort).ToList();
            }
            foreach (var item in list.Where(a => a.Pid == rootInfo.Id))
            {
                LeftTree treeInfo = new LeftTree();
                treeInfo.Id = item.Id;
                treeInfo.Name = item.Name;
                treeInfo.No = item.No;
                treeInfo.Href = item.RequestUrl;
                treeInfo.Ico = item.Ico;
                GetChrenTreeNode(list, treeInfo, item);
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
        public void GetChrenTreeNode(IList<SysModelDTO> listDepts, LeftTree treeInfo, SysModelDTO item)
        {
            var listchren = listDepts.Where(a => a.Pid == item.Id).OrderBy(a=>a.Sort);
            var listchrennode = new List<LeftTree>();
            if (listchren.Any())
            {
                foreach (var chrenItem in listchren.ToList())
                {
                    LeftTree treeInfotmp = new LeftTree();

                    treeInfotmp.Id = chrenItem.Id;
                    treeInfotmp.Name = chrenItem.Name;
                    treeInfotmp.No = chrenItem.No;
                    treeInfotmp.Href = chrenItem.RequestUrl;
                    treeInfotmp.Ico = chrenItem.Ico;
                    GetChrenTreeNode(listDepts, treeInfotmp, chrenItem);
                    listchrennode.Add(treeInfotmp);
                }

                treeInfo.ChildNode = listchrennode;

            }


        }
        #endregion


    }
}
