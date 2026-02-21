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
    public partial interface ISysFieldService
    {
        LayPageInfo<SysFieldDTO> GetList(PageInfo<SysField> pageInfo, Expression<Func<SysField, bool>> whereLambda);
        /// <summary>
        /// 保存系统用户
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns>返回主信息</returns>
        SysField SaveInfo(SysField sysField);

        /// <summary>
        /// 显示查看基本信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        SysField ShowView(int Id);
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="Ids">修改数据Ids</param>
        /// <returns>受影响行数</returns>
        int Delete(string Ids);
        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="info">修改字段信息</param>
        /// <returns>受影响行数</returns>
        int UpdateField(UpdateFieldInfo info);
        bool UpdateDesc(int Id, string field, string fdv);
    }
}
