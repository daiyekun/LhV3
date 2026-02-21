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
    /// <summary>
    /// 单品管理
    /// </summary>
    public partial interface IBcInstanceService
    {
        /// <summary>
        /// 单品管理列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">Where条件表达式</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns>单品管理列表</returns>
        LayPageInfo<BcInstanceViewDTO> GetList<s>(PageInfo<BcInstance> pageInfo, Expression<Func<BcInstance, bool>> whereLambda, Expression<Func<BcInstance, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 校验某一字段值是否已经存在
        /// </summary>
        /// <param name="fieldInfo">字段相关信息</param>
        /// <returns>True:存在/False不存在</returns>
        bool CheckInputValExist(UniqueFieldInfo fieldInfo);
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="bcInstance">当前需要保存的实体对象</param>
        /// <returns></returns>
        BcInstance Save(BcInstance bcInstance);
        /// <summary>
        /// 查看信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        BcInstanceViewDTO ShowView(int Id);
        /// <summary>
        /// 清除标签垃圾数据
        /// </summary>
        /// <param name="currUserId">当前用户ID</param>
        /// <returns></returns>
        int ClearJunkItemData(int currUserId);
        /// <summary>
        /// 修改当前对应标签下的-UserId数据
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <param name="currUserId">当前用户ID</param>
        int UpdateItems(int Id, int currUserId);

    }
}
