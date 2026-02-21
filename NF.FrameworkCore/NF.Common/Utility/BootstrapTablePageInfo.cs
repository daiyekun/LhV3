using System;
using System.Collections.Generic;
using System.Text;

namespace NF.Common.Utility
{
    /// <summary>
    /// Bootstrap Table分页对象
    /// </summary>
    /// <typeparam name="T">分页实体对象</typeparam>
   public class BootstrapTablePageInfo<T> where T : class, new()
    {
        public int total { get; set; }
        public IList<T> rows { get; set; }
    }
}
