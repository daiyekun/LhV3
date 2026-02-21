using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
    public partial interface IDepartmentService
    {
        /// <summary>
        /// 查询部门信息列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<DepartmentDTO> GetList(PageInfo<Department> pageInfo, Expression<Func<Department, bool>> whereLambda);

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="Ids">修改数据Ids</param>
        /// <returns>受影响行数</returns>
        int Delete(string Ids);
        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="info">修改字段信息</param>
        /// <returns>受影响行数</returns>
        int UpdateField(UpdateFieldInfo info);
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        IList<DepartmentDTO> GetListAll();
        /// <summary>
        /// 获取部门树结构
        /// </summary>
        /// <returns></returns>
        IList<TreeInfo> GetGetTreeListDept();
        /// <summary>
        /// 保存部门信息
        /// </summary>
        /// <param name="deptInfo">部门信息</param>
        /// <param name="deptMain">签约主体信息</param>
        /// <returns>返回主信息</returns>
        Department SaveDeptInfo(Department deptInfo, DeptMain deptMain);
        /// <summary>
        /// 显示查看基本信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        DepartmentViewDTO ShowView(int Id);
        /// <summary>
        /// 组织机构多选
        /// </summary>
        /// <param name="Ids">已经选中的ID</param>
        /// <returns></returns>
        IList<XTree> GetXtTree(IList<int> Ids);
        /// <summary>
        /// 查询Redis部门
        /// </summary>
        /// <returns>Redis部门对象集合</returns>
        IList<RedisDept> GetRedisDepts(Expression<Func<Department, bool>> whereLambda);
        /// <summary>
        /// 存储Redis
        /// </summary>
        void SetRedis();
        /// <summary>
        /// 获取部门树结构
        /// </summary>
        /// <returns></returns>
        IList<TreeSelectInfo> GetGetTreeselectListDept();
    }
}
