using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NF.IBLL
{
  public partial  interface IAjglSpwsAttachmentService
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<SpwsViewDTO> GetList<s>(PageInfo<AjglSpwsAttachment> pageInfo, Expression<Func<AjglSpwsAttachment, bool>> whereLambda, Expression<Func<AjglSpwsAttachment, s>> orderbyLambda, bool isAsc);
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
        SpwsViewDTO ShowView(int Id);
        /// <summary>
        /// 根据合同id 获取图片集合
        /// </summary>
        /// <param name="contId">图片ID</param>
        /// <returns></returns>
        IList<PicViewDTO> GetPicViews(int contId);
    }
}
