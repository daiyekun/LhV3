using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models
{
  public  class HtTypeTj
    {
        /// <summary>
        /// 收-重点工程类
        /// </summary>
        public int skzdgc { get; set; }
        /// <summary>
        /// 收-物资供应类
        /// </summary>
        public int skwzgy { get; set; }
         /// <summary>
        /// 收-电子信息类
        /// </summary>
        public int skdzxx { get; set; }
        /// <summary>
        ///  付-重点工程类
        /// </summary>
        public int fkzdgc { get; set; }
        /// <summary>
        /// 付-物资供应类
        /// </summary>
        public int fkwzgy { get; set; }
        /// <summary>
        ///  付-电子信息类
        /// </summary>
        public int fkdzxx { get; set; }
    }
}
