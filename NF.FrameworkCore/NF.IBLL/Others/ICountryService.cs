using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.IBLL
{
    /// <summary>
    /// 国家
    /// </summary>
    public partial interface ICountryService
    {
        /// <summary>
        /// 获取地址集合
        /// </summary>
        /// <returns>返回地址集合</returns>
        IList<AddressDTO> GetAddress();
    }
}
