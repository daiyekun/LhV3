using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
    /// <summary>
    /// 省
    /// </summary>
   public partial interface IProvinceService
    {
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        IList<Province> GetAll();
        /// <summary>
        /// 存储Redis
        /// </summary>
        void SetRedis();
        /// <summary>
        /// 获取Redis需要的数据
        /// </summary>
        /// <returns>List</returns>
        IList<RedisProvince> GetRedisData(Expression<Func<Province, bool>> whereLambda);
    }
}
