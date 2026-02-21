using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
   public class CountryDTO
    {
    }
    /// <summary>
    /// 国家Redis
    /// </summary>
    public class RedisCountry: IEntityDTO
    {
        /// <summary>
        /// ID 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
    }
}
