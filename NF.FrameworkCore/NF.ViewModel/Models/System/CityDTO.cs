using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 市
    /// </summary>
   public  class CityDTO
    {
       
    }
    /// <summary>
    /// 市
    /// </summary>
    public class RedisCity : IEntityDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 省ID
        /// </summary>
        public int? ProvinceId { get; set; }
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
