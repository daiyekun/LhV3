using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models
{
  
        /// <summary>
        /// 案件管理状态
        /// </summary>
        [EnumClass(Max = 10, Min = 0, None = -1)]
        public enum AjglState
        {
            /// <summary>
            /// 已立项：0
            /// </summary>
            [EnumItem(Value = 0, Desc = "已立项")]
            NonExecution = 0,
            /// <summary>
            /// 一审中1
            /// </summary>
            [EnumItem(Value = 1, Desc = "一审中")]
            Execution = 1,
            /// <summary>
            /// 二审中2
            /// </summary>
            [EnumItem(Value = 2, Desc = "二审中")]
            Terminated = 2,
            /// <summary>
            /// 已执行3
            /// </summary>
            [EnumItem(Value = 3, Desc = "已执行")]
            Dozee = 3,
            /// <summary>
            /// 已结案4
            /// </summary>
            [EnumItem(Value = 4, Desc = "已结案")]
            Approvaling = 4,
          
        }
    
}
