using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models.Utility;
using NF.ViewModel;
using NF.Model.Models;
using NF.BLL.Extend;

namespace NF.BLL
{/// <summary>
 /// 合同文本
 /// </summary>
    public partial class ContTextService
    {  /// <summary>
       /// 查看或者修改
       /// </summary>
       /// <param name="Id">当前ID</param>
       /// <returns></returns>
        public IList<WxCountText> WxShowViews(int Id)
        {
            var query = from a in _ContTextSet.AsNoTracking()
                        where a.ContId == Id && a.IsDelete==0
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            CategoryId=a.CategoryId,
                            ExtenName=a.ExtenName
                        };
            var local = from a in query.AsEnumerable()
                        select new WxCountText
                        {
                            Id = a.Id,
                            Name = a.Name,
                            ContTxtType = this.Db.GetRedisDataDictionaryValue(a.CategoryId ?? 0, DataDictionaryEnum.ContTxtType),
                            //DataDicUtility.GetDicValueToRedis(a.CategoryId, DataDictionaryEnum.ContTxtType),//文本类别
                            ExtenName = a.ExtenName
                        };
             return local.ToList();
        }

    }

}
