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

    /// <summary>
    /// 合同操作接口
    /// </summary>
    public partial interface IContractInfoService
    {
        /// <summary>
        /// 微信查看页面
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns>合同对象</returns>
        ContractViewWxModel ShowWxViewMode(int Id);
        LayPageInfo<ContractInfoListViewDTO> WXCountGetList<s>(PageInfo<ContractInfo> pageInfo, Expression<Func<ContractInfo, bool>> whereLambda, Expression<Func<ContractInfo, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 根据微信账号查询用户信息
        /// </summary>
        /// <param name="Wxzh"></param>
        /// <returns></returns>
        UserInfor Yhinfo(string Wxzh);
        /// <summary>
        /// 到期合同到redis
        /// </summary>
        /// <returns></returns>
        int DaoQqhtToRedisList();
        /// <summary>
        /// 到期计划到redis
        /// </summary>
        /// <returns></returns>
        int DaoQqJhToRedisList();

    }
}
