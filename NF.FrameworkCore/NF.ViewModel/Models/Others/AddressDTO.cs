using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 地址对象、国家/省/市
    /// </summary>
    public class AddressDTO
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 子类
        /// </summary>
        public IList<AddressDTO> Childs { get; set; }



    }
}
