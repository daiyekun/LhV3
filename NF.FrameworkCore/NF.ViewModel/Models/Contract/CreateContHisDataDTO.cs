using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 创建合同历史信息
    /// </summary>
    public class CreateContHisDataDTO
    {

    }
    /// <summary>
    /// 合同映射合同历史信息
    /// </summary>
    public class MappContToHistory
    {
        /// <summary>
        /// 合同ID
        /// </summary>
        public int ContId { get; set; }
        /// <summary>
        /// 合同历史ID
        /// </summary>
        public int ContHisId { get; set; }
    }
}
