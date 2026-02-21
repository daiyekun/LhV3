using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models
{
   public class WxActualFinance
    {
        /// <summary>
        /// 实际金额
        /// </summary>
        public string Sjzj { get; set; }
       /// <summary>
       /// 合同名称
       /// </summary>
        public string HtName { get; set; }
        /// <summary>
        /// 资金状态
        /// </summary>
        public string ZjSata { get; set; }

        public int Id { get; set; }
        /// <summary>
        /// 合同id
        /// </summary>
        public int? Htid { get; set; }
        public int? WfItem { get; set; }

        public string WfItemDic { get; set; }
        public string WfStateDic { get; set; }
      

    }
}
