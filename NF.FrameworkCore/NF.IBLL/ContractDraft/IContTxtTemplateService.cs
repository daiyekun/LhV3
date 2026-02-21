using AutoMapper;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
    /// <summary>
    /// 合同模板
    /// </summary>
   public partial interface IContTxtTemplateService
    {
        /// <summary>
        /// 大列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">where条件表达式</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <param name="deptId">经办结构，选择模板时用</param>
        /// <param name="htLb">合同类别</param>
        /// <returns></returns>
        LayPageInfo<ContTxtTemplateListDTO> GetList<s>(PageInfo<ContTxtTemplate> pageInfo, Expression<Func<ContTxtTemplate, bool>> whereLambda,
           Expression<Func<ContTxtTemplate, s>> orderbyLambda, bool isAsc, int deptId = 0,int htLb=0);
        /// <summary>
        ///校验对象值
        /// </summary>
        /// <param name="templateDTO">模板对象</param>
        /// <returns>RequestMsg：返回消息对象</returns>
        RequestMsg CheckInputValExist(ContTxtTemplateDTO templateDTO);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="template">当前对象</param>
        /// <returns>当前模板信息</returns>
        CurrTempInfo AddSave(ContTxtTemplate template, IMapper _IMapper);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="template">当前修改对象</param>
        /// <returns>当前模板信息</returns>
        CurrTempInfo UpdateSave(ContTxtTemplate template, IMapper _IMapper);
        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="info">修改字段对象</param>
        /// <returns>受影响行数</returns>
        int UpdateField(UpdateFieldInfo info);
        /// <summary>
        /// 删除信息-软删除
        /// </summary>
        /// <param name="Ids">删除数据Ids</param>
        /// <returns>受影响行数</returns>
        int Delete(string Ids);
        /// <summary>
        /// 显示查看基本信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        ContTxtTemplateViewDTO ShowView(int Id);
        /// <summary>
        /// 自定义变量
        /// </summary>
        /// <param name="cttextid"></param>
        /// <returns></returns>
        IList<ContractVariable> GetCustomVariables(Int32 cttextid);





    }
}
