using System;
using System.Collections.Generic;
using System.Text;

namespace NF.IBLL
{
    public partial interface INoHipler
    {
        public string ContractNo(int DeptId, int ContTypeId, string MainDeptShortName);
        public string ContractNo2(int DeptId, int ContTypeId, string MainDeptShortName);
        /// <summary>
        /// 项目编号
        /// </summary>
        /// <returns></returns>
        string ProjectNo();
        /// <summary>
        /// 客户-供应商-其他对方 编号自动生成
        /// </summary>
        /// <param name="gyslb">类别</param>
        /// <param name="compType">0:可以，1：供应商，2：其他对方</param>
        /// <returns></returns>
        string CustrNo(int gyslb = 0,int compType=1);
    }
}
