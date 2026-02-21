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
    /// 省
    /// </summary>
   public partial class ProvinceService
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public IList<Province> GetAll()
        {
            IList<Province> list = RedisHelper.StringGetToList<Province>("Nf-ProvinceListAll");
            if (list == null)
            {
                var tempquery = GetQueryable(a => true).AsNoTracking();
                list = tempquery.ToList();
                RedisHelper.ListObjToJsonStringSetAsync("Nf-ProvinceListAll", list);
            }
            return list;
        }
        /// <summary>
        /// 获取Redis需要的数据
        /// </summary>
        /// <returns></returns>
        public IList<RedisProvince> GetRedisData(Expression<Func<Province, bool>> whereLambda)
        {
            var query = from a in GetQueryable(whereLambda)
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            DisplayName = a.DisplayName

                        };
            var loacl = from a in query.AsEnumerable()
                        select new RedisProvince
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
                SysIniInfoUtility.SetRedisHash(item, StaticData.RedisProvinceKey, (a, c) =>
                {
                    return $"{a}:{c}";
                });


            }

        }
    }
}
