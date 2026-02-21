using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
   public  class ProvinceDTO
    {
    }
    /// <summary>
    /// 省
    /// </summary>
    public class RedisProvince: IEntityDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 国家ID
        /// </summary>
        public int? CountryId { get; set; }
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
