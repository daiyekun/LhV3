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
  public partial  interface ICaseManagerService
    {
        /// <summary>
        /// 保存合同
        /// </summary>
        /// <param name="contractInfo">合同信息</param>
        /// <param name="contractInfoHistory">合同历史表信息（拷贝信息）</param>
        /// <returns>Id:\Hid:字典</returns>
        CaseManager AddSave(CaseManager CaseManager);
        /// <summary>
        /// 查询信息列表
        /// </summary>
        /// <param name="pageInfo">分页对象</param>
        /// <param name="whereLambda">查询条件表达式</param>
        /// <returns>返回layui所需对象</returns>
        LayPageInfo<AjjfDTO> GetList<s>(PageInfo<CaseManager> pageInfo, Expression<Func<CaseManager, bool>> whereLambda, Expression<Func<CaseManager, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 显示查看基本信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        AjjfDTO ShowView(int Id);
        /// <summary>
        /// 修改合同信息
        /// </summary>
        /// <param name="contractInfo">合同修改信息对象</param>
        /// <param name="contractInfoHistory">合同修改信息拷贝对象（历史）</param>
        /// <returns>Id:\Hid:字典</returns>
        bool UpdateSave(CaseManager CaseManager);

        /// <summary>
        /// 删除信息-软删除
        /// </summary>
        /// <param name="Ids">删除数据Ids</param>
        /// <returns>受影响行数</returns>
        int Delete(string Ids, int i);

        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="fields">修改字段集合</param>
        /// <returns>受影响行数</returns>
        int UpdateField(IList<UpdateFieldInfo> fields);
        int ClearJunkItemData(int currUserId);
    }
}
