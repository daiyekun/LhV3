using Microsoft.EntityFrameworkCore;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NF.Common.Extend;
using NF.ViewModel.Models.Common;
using NF.ViewModel.Models.Utility;

namespace NF.BLL
{
    /// <summary>
    /// 部门
    /// </summary>
   public partial  class DepartmentService:BaseService<Department>, IDepartmentService
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">Where条件</param>
        /// <returns></returns>
        public LayPageInfo<DepartmentDTO> GetList(PageInfo<Department> pageInfo, Expression<Func<Department, bool>> whereLambda)
        {
            var tempquery = Db.Set<Department>().Include(a=>a.Category).AsNoTracking().Where<Department>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            tempquery = tempquery.OrderByDescending(a => a.Id);
            tempquery = tempquery.Skip<Department>((pageInfo.PageIndex - 1) * pageInfo.PageSize)
                 .Take<Department>(pageInfo.PageSize);
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name=a.Name,
                            ShortName= a.ShortName,
                            No=a.No,
                            CategoryId=a.CategoryId,
                            Dsort=a.Dsort,
                            Remark=a.Remark,
                            IsMain=a.IsMain,
                            IsSubCompany=a.IsSubCompany,
                            Dstatus=a.Dstatus,
                            Dpath=a.Dpath,
                            Leaf=a.Leaf,
                            CategoryName=a.Category.Name,
                            PName= Db.Set<Department>().AsNoTracking().Where(d=>a.Pid==d.Id).Any()? Db.Set<Department>().AsNoTracking().Where(d => a.Pid == d.Id).FirstOrDefault().Name:"",


                        };
            var local = from a in query.AsEnumerable()
                        select new DepartmentDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            ShortName = a.ShortName,
                            No = a.No,
                            CategoryId = a.CategoryId,
                            Dsort = a.Dsort,
                            Remark = a.Remark,
                            IsMain = a.IsMain,
                            IsSubCompany = a.IsSubCompany,
                            Dstatus = a.Dstatus,
                            Dpath = a.Dpath,
                            Leaf = a.Leaf,
                            CategoryName = a.CategoryName,
                            PName=a.PName,
                            IsMainDic = EmunUtility.GetDesc(typeof(OtherDataState), a.IsMain ?? 0)


                        };


            return new LayPageInfo<DepartmentDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">修改集合</param>
        /// <returns></returns>
        public int Delete(string Ids)
        {
            string sqlstr = "update Department set IsDelete=1 where Id in(" + Ids + ")";
            RedisHelper.KeyDeleteAsync($"{StaticData.redisBaseKey}:Nf-DeptListAll");
            return ExecuteSqlCommand(sqlstr);
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
                case "DStatus"://状态
                     {
                        int state = 0;
                        int.TryParse(info.FieldValue, out state);
                        sqlstr = "update Department set DStatus="+ state +" where Id="+ info.Id;
                     }
                    break;
                case "IsSubCompany"://子公司
                    {
                        int issubcomp = 0;
                        int.TryParse(info.FieldValue, out issubcomp);
                        sqlstr = "update Department set IsSubCompany=" + issubcomp + " where Id=" + info.Id;

                    }
                    break;

            }
            RedisHelper.KeyDeleteAsync($"{StaticData.redisBaseKey}:Nf-DeptListAll");
            if (!string.IsNullOrEmpty(sqlstr))
                return ExecuteSqlCommand(sqlstr);
            return 0;

        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public IList<DepartmentDTO> GetListAll()
        {
            IList<DepartmentDTO> list = RedisHelper.StringGetToList<DepartmentDTO>($"{StaticData.redisBaseKey}:Nf-DeptListAll");
          
            if (list == null) { 
              var tempquery = Db.Set<Department>().AsNoTracking();
            var query = from a in tempquery
                        where a.IsDelete == 0
                        select new
                             {
                                 Id = a.Id,
                                 Pid = a.Pid,
                                 Name = a.Name,
                                 ShortName = a.ShortName,
                                 No = a.No,
                                 CategoryId = a.CategoryId,
                                 Dsort = a.Dsort,
                                 Remark = a.Remark,
                                 IsMain = a.IsMain,
                                 IsSubCompany = a.IsSubCompany,
                                 Dstatus = a.Dstatus,
                                 Dpath = a.Dpath,
                                 Leaf = a.Leaf,
                                 IsDelete=a.IsDelete,
                                 CategoryName = a.Category.Name,
                                 PName = Db.Set<Department>().AsNoTracking().Where(d => a.Pid == d.Id).Any() ? Db.Set<Department>().AsNoTracking().Where(d => a.Pid == d.Id).FirstOrDefault().Name : "",
                                 

                             };
            var local = from a in query.AsEnumerable()
                        select new DepartmentDTO
                        {
                            Id = a.Id,
                            Pid = a.Pid,
                            Name = a.Name,
                            ShortName = a.ShortName,
                            No = a.No,
                            CategoryId = a.CategoryId,
                            Dsort = a.Dsort,
                            Remark = a.Remark,
                            IsMain = a.IsMain,
                            IsSubCompany = a.IsSubCompany,
                            Dstatus = a.Dstatus,
                            Dpath = a.Dpath,
                            Leaf = a.Leaf,
                            CategoryName = a.CategoryName,
                            PName = a.PName,
                            IsDelete = a.IsDelete,
                            IsMainDic = EmunUtility.GetDesc(typeof(OtherDataState), a.IsMain ?? 0),
                            IsSubCompanyDic= EmunUtility.GetDesc(typeof(OtherDataState), a.IsSubCompany ?? 0)

                        };
                list= local.ToList();

                RedisHelper.ListObjToJsonStringSetAsync($"{StaticData.redisBaseKey}:Nf-DeptListAll", list);
            }
            return list;

        }
        /// <summary>
        /// 返回LayUI Tree需要数据格式
        /// </summary>
        /// <returns></returns>
      public  IList<TreeInfo> GetGetTreeListDept()
        {
            IList<TreeInfo> listTree = new List<TreeInfo>();
            var listAll =GetListAll();
            var list = listAll.Where(a => a.IsDelete == 0 && a.Dstatus == 1).ToList();
            foreach (var item in list.Where(a => a.Pid == 0))
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
        /// 递归
        /// </summary>
        /// <param name="listDepts">数据列表</param>
        /// <param name="treeInfo">Tree对象</param>
        /// <param name="item">父类对象</param>
        public void RecursionChrenNode(IList<DepartmentDTO> listDepts, TreeInfo treeInfo, DepartmentDTO item)
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

        #region treeselect需要数据
        /// <summary>
        /// 返回LayUI Tree需要数据格式
        /// </summary>
        /// <returns></returns>
        public IList<TreeSelectInfo> GetGetTreeselectListDept()
        {
            IList<TreeSelectInfo> listTree = new List<TreeSelectInfo>();
            var listAll = GetListAll();
            var list = listAll.Where(a => a.IsDelete == 0 && a.Dstatus == 1).ToList();
            foreach (var item in list.Where(a => a.Pid == 0))
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
        public void RecursionChrenNode(IList<DepartmentDTO> listDepts, TreeSelectInfo treeInfo, DepartmentDTO item)
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


        /// <summary>
        /// 保存部门信息
        /// </summary>
        /// <returns></returns>
        public Department SaveDeptInfo(Department deptInfo,DeptMain deptMain)
        {
            Department resul=null;
            if (deptInfo.Id > 0)
            {//修改
                resul = UpdateSave(deptInfo, deptMain);

            }
            else
            {
                resul = AddSave(deptInfo, deptMain);
            }
            RedisHelper.KeyDeleteAsync($"{StaticData.redisBaseKey}:Nf-DeptListAll");
            return resul;

            

        }

        private Department AddSave(Department deptInfo, DeptMain deptMain)
        {
            deptInfo.IsDelete = 0;
            deptInfo.Dstatus = 1;
            var tmpInfo = Add(deptInfo);
            if (deptMain != null && !string.IsNullOrEmpty(deptMain.LawPerson))
            {
                deptMain.IsDelete = 0;
                deptMain.DeptId = tmpInfo.Id;
                Db.Set<DeptMain>().Add(deptMain);
                Db.SaveChanges();
               
            }

            return deptInfo;
        }

        private Department UpdateSave(Department deptInfo, DeptMain deptMain)
        {
            Department resul;
            var tempinfo = _DepartmentSet.FirstOrDefault(a => a.Id == deptInfo.Id);
            tempinfo.Name = deptInfo.Name;
            tempinfo.Pid = deptInfo.Pid;
            tempinfo.No = deptInfo.No;
            tempinfo.CategoryId = deptInfo.CategoryId;
            tempinfo.Dsort = deptInfo.Dsort;
            tempinfo.Remark = deptInfo.Remark;
            tempinfo.IsMain = deptInfo.IsMain;
            tempinfo.ShortName = deptInfo.ShortName;
            tempinfo.IsSubCompany = deptInfo.IsSubCompany;
            tempinfo.Dpath = deptInfo.Dpath;
            tempinfo.Leaf = deptInfo.Leaf;
            resul = tempinfo;
            var tdeptMain = Db.Set<DeptMain>().FirstOrDefault(a => a.DeptId == deptInfo.Id);
            if (tdeptMain != null)
            {
                tdeptMain.LawPerson = deptMain.LawPerson;
                tdeptMain.TaxId = deptMain.TaxId;
                tdeptMain.BankName = deptMain.BankName;
                tdeptMain.Account = deptMain.Account;
                tdeptMain.Address = deptMain.Address;
                tdeptMain.ZipCode = deptMain.ZipCode;
                tdeptMain.Fax = deptMain.Fax;
                tdeptMain.TelePhone = deptMain.TelePhone;
                tdeptMain.InvoiceName = deptMain.InvoiceName;

            }
            else
            {
                deptMain.Id = 0;
                deptMain.IsDelete = 0;
                deptMain.DeptId = tempinfo.Id;
                Db.Set<DeptMain>().Add(deptMain);
            }

            Db.SaveChanges();
            return resul;
        }

        /// <summary>
        /// 显示查看基本信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public DepartmentViewDTO ShowView(int Id)
        {
            var listAll = GetListAll();
            var firstdept= listAll.FirstOrDefault(a => a.Id == Id);
            var deptMain = Db.Set<DeptMain>().AsNoTracking().Where(a => a.DeptId == firstdept.Id).FirstOrDefault();
            var info= new DepartmentViewDTO
            {
                Id = firstdept.Id,
                Pid = firstdept.Pid,
                Name = firstdept.Name,
                ShortName = firstdept.ShortName,
                No = firstdept.No,
                CategoryId = firstdept.CategoryId,
                Dsort = firstdept.Dsort,
                Remark = firstdept.Remark,
                IsMain = firstdept.IsMain,
                IsSubCompany = firstdept.IsSubCompany,
                Dstatus = firstdept.Dstatus,
                Dpath = firstdept.Dpath,
                Leaf = firstdept.Leaf,
                CategoryName = firstdept.CategoryName,
                PName = firstdept.PName,
                IsDelete = firstdept.IsDelete,
                IsMainDic = firstdept.IsMainDic,
                IsSubCompanyDic= firstdept.IsSubCompanyDic,


            };
            if (deptMain != null)
            {
                info.LawPerson = deptMain.LawPerson;
                info.TaxId = deptMain.TaxId;
                info.BankName = deptMain.BankName;
                info.Account = deptMain.Account;
                info.Address = deptMain.Address;
                info.ZipCode = deptMain.ZipCode;
                info.Fax = deptMain.Fax;
                info.TelePhone = deptMain.TelePhone;
                info.InvoiceName = deptMain.InvoiceName;
            }

            return info;


        }

        #region 递归XTree-主要用于功能权限分配

        /// <summary>
        /// 返回LayUI Tree需要数据格式
        /// </summary>
        /// <returns></returns>
        public IList<XTree> GetXtTree(IList<int> Ids)
        {

            IList<XTree> listTree = new List<XTree>();
            var listAll = GetListAll();
            var list = listAll.Where(a => a.IsDelete == 0).ToList();
            foreach (var item in list.Where(a => a.Pid == 0))
            {
                XTree treeInfo = new XTree();
                treeInfo.value = item.Id.ToString();
                treeInfo.title = item.Name;
                treeInfo.Checked = (Ids != null && Ids.Contains(item.Id)) ? true : false;

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
        public void RecursionChrenNodeXt(IList<DepartmentDTO> listDepts, XTree treeInfo, DepartmentDTO item, IList<int> Ids)
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
                    treeInfotmp.Checked = (Ids != null && Ids.Contains(chrenItem.Id)) ? true : false;

                    RecursionChrenNodeXt(listDepts, treeInfotmp, chrenItem, Ids);
                    listchrennode.Add(treeInfotmp);
                }

                treeInfo.data = listchrennode;

            }


        }

        /// <summary>
        /// 查询Redis部门
        /// </summary>
        /// <returns>Redis部门对象集合</returns>
       public IList<RedisDept> GetRedisDepts(Expression<Func<Department, bool>> whereLambda)
        {
           
            return GetQueryable(whereLambda).Select(a => new RedisDept
            {
                Id = a.Id,
                Pid = a.Pid,
                Name = a.Name,
                No = a.No,
                ShortName = a.ShortName
            }).ToList();

        }
        /// <summary>
        /// 存储Redis
        /// </summary>
        public void SetRedis()
        {
            RedisHelper.KeyDeleteAsync($"{StaticData.redisBaseKey}:Nf-DeptListAll");
            
            var list = GetRedisDepts(a => a.IsDelete == 0);
            foreach (var item in list)
            {
                SysIniInfoUtility.SetRedisHash(item, StaticData.RedisDeptKey, (a, c) =>
                {
                    return $"{a}:{c}";
                });


            }
        }

        #endregion
    }
}
