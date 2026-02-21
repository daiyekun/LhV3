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
    /// 合同文本
    /// </summary>
    public partial  interface IContTextService
    {

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<ContTextViewDTO> GetList<s>(PageInfo<ContText> pageInfo, Expression<Func<ContText, bool>> whereLambda, Expression<Func<ContText, s>> orderbyLambda, bool isAsc);
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
        ContTextViewDTO ShowView(int Id);
      
        /// <summary>
        /// 保存合同文本
        /// </summary>
        /// <param name="contText">合同文本对象</param>
        /// <returns></returns>
        Dictionary<string, int> AddSave(ContText contText);
        /// <summary>
        /// 修改合同文本
        /// </summary>
        /// <param name="contText">合同文本对象</param>
        /// <returns></returns>
        Dictionary<string, int> UpdateSave(ContText contText);

        /// <summary>
        /// 查询列表大列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<ContTextListViewDTO> GetMainList<s>(PageInfo<ContText> pageInfo, Expression<Func<ContText, bool>> whereLambda, Expression<Func<ContText, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 获取当前文本状态
        /// </summary>
        /// <param name="contTxtId">文本ID</param>
        /// <param name="IsHistory">是否起草</param>
        /// <param name="IsReview">是否审阅</param>
        /// <param name="userId">当前登录人</param>
        /// <returns></returns>
       Dictionary<string,string> GetWordState(int contTxtId, bool IsHistory, bool IsReview,int userId);
        /// <summary>
        /// 起草相关修改
        /// </summary>
        /// <param name="info">当前对象</param>
        /// <param name="AddHistory">是否添加历史</param>
        /// <returns></returns>
        Dictionary<string, int> UpdateQiCaoSave(ContText info, bool AddHistory);
        /// <summary>
        /// 根据合同id 获取图片集合
        /// </summary>
        /// <param name="contId">图片ID</param>
        /// <returns></returns>
        IList<PicViewDTO> GetPicViews(int contId);

    }
}
