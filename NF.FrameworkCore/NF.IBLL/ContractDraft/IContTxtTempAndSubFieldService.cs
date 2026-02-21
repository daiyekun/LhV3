using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models.ContTxtTemplate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
    /// <summary>
    /// 合同文本模板标的字段操作
    /// </summary>
   public partial  interface IContTxtTempAndSubFieldService
    {
        /// <summary>
        /// 根据模板历史ID，显示格式，单品类别ID查询字段信息
        /// </summary>
        /// <param name="tempHistId"></param>
        /// <param name="fieldType">显示格式0: 合同标的统一格式; 1: 按业务品类设置格式</param>
        /// <param name="bcId">单类别ID：0：非业务</param>
        /// <returns>字段信息</returns>
        IList<SubChkField> GetSubChkFields(int tempHistId,int fieldType, int bcId);
        /// <summary>
        /// 查询字段
        /// </summary>
        /// <param name="tempHistId">模板ID</param>
        /// <param name="fieldType">显示格式0: 合同标的统一格式; 1: 按业务品类设置格式</param>
        /// <param name="bcId">单品类别ID。0：非业务</param>
        /// <returns></returns>
        IList<ContTempSubFiled> GetList(int tempHistId, int fieldType, int bcId);
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">Where条件</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <param name="bcId">单品类别ID。0：非业务</param>
        /// <param name="fieldType">显示格式0: 合同标的统一格式; 1: 按业务品类设置格式</param>
        /// <returns></returns>
        LayPageInfo<ContTempSubFiled> GetList<s>(PageInfo<ContTxtTempAndSubField> pageInfo, Expression<Func<ContTxtTempAndSubField, bool>> whereLambda, Expression<Func<ContTxtTempAndSubField, s>> orderbyLambda, bool isAsc, int fieldType = 0, int bcId = 0);
        /// <summary>
        /// 获取合同标的数据
        /// </summary>
        /// <param name="cttextid">合同文本ID</param>
        /// <param name="bc_id">业务类实例ID</param>
        /// <param name="field_type">字段类别</param>
        /// <returns>合同标的数据</returns>
        IList<String[]> GetContractObjectsDataTable(Int32 cttextid, Int32 bc_id, Int32 field_type);

    }
}
