using Microsoft.EntityFrameworkCore;
using NF.Common.Utility;
using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Utility;

namespace NF.BLL
{
    /// <summary>
    /// 国家
    /// </summary>
    public partial class CountryService
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public IList<Country> GetAll()
        {
            IList<Country> list = RedisHelper.StringGetToList<Country>("Nf-CountryListAll");
            if (list == null)
            {
                var tempquery = GetQueryable(a => true).AsNoTracking();
                list = tempquery.ToList();
                RedisHelper.ListObjToJsonStringSetAsync("Nf-CountryListAll", list);
            }
            return list;
        }
        /// <summary>
        /// 获取Redis需要的数据
        /// </summary>
        /// <returns></returns>
        public IList<RedisCountry> GetRedisData(Expression<Func<Country, bool>> whereLambda)
        {
            var query = from a in GetQueryable(whereLambda)
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            DisplayName = a.DisplayName

                        };
            var loacl = from a in query.AsEnumerable()
                        select new RedisCountry
                        {
                            Id = a.Id,
                            Name = a.Name,
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
                SysIniInfoUtility.SetRedisHash(item, StaticData.RedisCountryKey, (a, c) =>
                {
                    return $"{a}:{c}";
                });


            }

        }

    }
}
