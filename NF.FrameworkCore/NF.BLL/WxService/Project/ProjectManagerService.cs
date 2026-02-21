using Microsoft.EntityFrameworkCore;
using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.BLL
{
    public partial class ProjAttachmentService

    {

        public IList<WxProjAttachmen> WxShowViews(int Id)
        {
            var query = from a in _ProjAttachmentSet.AsNoTracking()
                        where a.ProjectId == Id && a.IsDelete == 0
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            CategoryId = a.CategoryId
                        };
            var local = from a in query.AsEnumerable()
                        select new WxProjAttachmen
                        {
                            Id = a.Id,
                            Name = a.Name,
                            ProjFileType = Pname(a.CategoryId??0)//RedisHelper.HashGet($"{StaticData.RedisDataKey}:{a.CategoryId}:{(int)DataDictionaryEnum.projectType}", "Name"),//项目类别

                                           //RedisHelper.HashGet($"{StaticData.RedisDataKey}:{a.CategoryId}:{(int)DataDictionaryEnum.projectType}", "Name"),//项目类别// DataDicUtility.GetDicValueToRedis(a.CategoryId, DataDictionaryEnum.projectFileType),//文本类别

                        };
            return local.ToList();
        }


        public string Pname(int t) {
            var name = "";

            try
            {
                // select DtypeNumber,*from DataDictionary where DtypeNumber = 13
              
                name = Db.Set<DataDictionary>().Where(a => a.DtypeNumber == 14 && a.Id == t).FirstOrDefault().Name;
                return name;
            }
            catch (Exception)
            {

                return name;
            }


        }
    }
}
