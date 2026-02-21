using NF.Model.Models;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.ViewModel.Models.LayUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.IBLL
{
    /// <summary>
    /// 业务品类
    /// </summary>
   public partial  interface IBusinessCategoryService
    {
        /// <summary>
        /// 查询所有节点树
        /// </summary>
        /// <returns></returns>
        IList<BusinessCategoryDTO> GetListAll();
        /// <summary>
        /// 获取layui Tree数据
        /// </summary>
        /// <returns></returns>
        IList<LayTree> GetLayUITreeData();
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="business">业务品类对象</param>
        /// <returns></returns>
        BusinessCategory AddSave(BusinessCategory business);
        /// <summary>
        /// 根据ID查询对象
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        BusinessCategoryViewDTO GetTreeDataById(int Id);
        /// <summary>
        /// 删除当前对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        int DeleteInfo(int Id);
        /// <summary>
        /// 获取类别下拉菜单树
        /// </summary>
        /// <returns></returns>
        IList<TreeSelectInfo> GetTreeselect();
        /// <summary>
        /// 校验某一字段值是否已经存在
        /// </summary>
        /// <param name="fieldInfo">字段相关信息</param>
        /// <returns>True:存在/False不存在</returns>
        bool CheckInputValExist(UniqueFieldInfo fieldInfo);
        /// <summary>
        /// 查询子类
        /// </summary>
        /// <returns></returns>
        IList<int> GetCateIds(int Id);
    }
}
