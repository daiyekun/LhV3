using Microsoft.EntityFrameworkCore;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NF.BLL
{
    /// <summary>
    /// 市
    /// </summary>
    public partial class CityService
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public IList<City> GetAll()
        {
            IList<City> list = RedisHelper.StringGetToList<City>("Nf-CityListAll");
            if (list == null)
            {
                var tempquery = GetQueryable(a => true).AsNoTracking();
                list = tempquery.ToList();
                RedisHelper.ListObjToJsonStringSetAsync("Nf-CityListAll", list);
            }
            return list;
        }
        /// <summary>
        /// 获取Redis需要的数据
        /// </summary>
        /// <returns></returns>
        public IList<RedisCity> GetRedisData(Expression<Func<City, bool>> whereLambda)
        {
            var query = from a in GetQueryable(whereLambda)
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            ProvinceId=a.ProvinceId,
                            DisplayName = a.DisplayName

                        };
            var loacl = from a in query.AsEnumerable()
                        select new RedisCity
                        {
                            Id = a.Id,
                            Name = a.Name,
                            ProvinceId = a.ProvinceId,
                            DisplayName = a.DisplayName
                        };

            return loacl.ToList();
        } 
        /// <summary>
        /// 存储Redis
        /// </summary>
        public void SetRedis()
        {
            var list = GetRedisData(a => true);
            foreach (var item in list)
            {
                SysIniInfoUtility.SetRedisHash(item, StaticData.RedisCityKey, (a, c) =>
                {
                    return $"{a}:{c}";
                });


            }

        }
          

    }
}
