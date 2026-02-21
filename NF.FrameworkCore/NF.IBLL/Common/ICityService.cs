using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NF.IBLL
{
  /// <summary>
  /// 市
  /// </summary>
   public partial interface ICityService
    {
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        IList<City> GetAll();
        /// <summary>
        /// 存储Redis
        /// </summary>
        void SetRedis();
        /// <summary>
        /// 获取Redis需要的数据
        /// </summary>
        /// <returns>List</returns>
        IList<RedisCity> GetRedisData(Expression<Func<City, bool>> whereLambda);

    }
}
