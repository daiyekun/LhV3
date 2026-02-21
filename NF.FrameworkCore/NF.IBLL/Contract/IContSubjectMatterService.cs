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
    /// 合同标的
    /// </summary>
    public partial interface IContSubjectMatterService
    {
        /// <summary>
        /// 保存合同标的
        /// </summary>
        /// <param name="subjectMatter">合同标的</param>
        /// <returns>Id:->Hid:</returns>
        Dictionary<string, int> AddSave(ContSubjectMatter subjectMatter, IMapper _IMapper);
        /// <summary>
        /// 合同标的保存
        /// </summary>
        /// <param name="subs">集合</param>
        /// <returns></returns>
        bool AddSave(IList<ContSubjectMatterDTO> subs, IMapper _IMapper);

        /// <summary>
        /// 修改合同标的
        /// </summary>
        /// <param name="subjectMatter">修改合同标的对象</param>
        /// <param name="subjectMatterHistory">修改合同标的拷贝对象（历史）</param>
        /// <returns>Id:\Hid:字典</returns>
        Dictionary<string, int> UpdateSave(ContSubjectMatter subjectMatter, IMapper _IMapper);
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<ContSubjectMatterViewDTO> GetList<s>(PageInfo<ContSubjectMatter> pageInfo, Expression<Func<ContSubjectMatter, bool>> whereLambda, Expression<Func<ContSubjectMatter, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 合同标的大列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">where表达式</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <returns>列表JSON</returns>
        LayPageInfo<ContSubjectMatterListDTO> GetMainList<s>(PageInfo<ContSubjectMatter> pageInfo, Expression<Func<ContSubjectMatter, bool>> whereLambda, Expression<Func<ContSubjectMatter, s>> orderbyLambda, bool isAsc);
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
        ContSubjectMatterViewDTO ShowView(int Id);
        /// <summary>
        /// 交付列表
        /// </summary>
        /// <typeparam name="s">排序字段</typeparam>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="orderbyLambda">排序表达式</param>
        /// <param name="isAsc">是否正序</param>
        /// <param name="search">查询类型</param>
        /// <returns></returns>
        LayPageInfo<JiaoFuListInfo> GetJiaoFuList<s>(PageInfo<ContSubjectMatter> pageInfo, Expression<Func<ContSubjectMatter, bool>> whereLambda, Expression<Func<ContSubjectMatter, s>> orderbyLambda, bool isAsc, int? search);

        /// <summary>
        /// Word插件使用-获取 业务类/非业务类 合同标的表格的标识符列表
        /// </summary>
        /// <param name="cttextid">合同文本ID</param>
        /// <returns>合同标的表格的标识符列表</returns>
         List<ContractObjectTableIdentifier> GetContractObjectIdentifiers(int cttextid);
        /// <summary>
        /// 根据合同ID查询当前的标的总金额
        /// </summary>
        /// <param name="contId">负数时标识当前登录用户。正数合同ID</param>
        /// <returns></returns>
        ContSubTotalInfo GetSubTotal(int contId,int userId);


    }
}
