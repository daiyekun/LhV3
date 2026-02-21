using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NF.IBLL
{
    public partial interface IProjectManagerService
    {

        /// <summary>ProjectWxModel
        /// 微信查看页面
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns>合同对象</returns>
        ProjectViewWxModel ShowWxViewMode(int Id);
        LayPageInfo<ProjectManagerViewDTO> GetWxProjectList<s>(PageInfo<ProjectManager> pageInfo, Expression<Func<ProjectManager, bool>> whereLambda, Expression<Func<ProjectManager, s>> orderbyLambda, bool isAsc);
        /// <summary>
        /// 获取资金统计
        /// </summary>
        /// <param name="项目ID">合同对方ID</param>
        /// <returns>资金统计对象</returns>
        WxXmZjTj WxGetFundStatistics(int projId);

        LayPageInfo<WxXmXgSk> GetXmXgSk<s>(PageInfo<ContractInfo> pageInfo, Expression<Func<ContractInfo, bool>> whereLambda, Expression<Func<ContractInfo, s>> orderbyLambda, bool isAsc);
    }
}
