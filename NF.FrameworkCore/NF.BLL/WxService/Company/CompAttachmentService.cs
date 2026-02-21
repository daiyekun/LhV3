using Microsoft.EntityFrameworkCore;
using NF.ViewModel;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.BLL
{
   public partial class CompAttachmentService
    {

        /// <summary>
        /// 查看或者修改
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public IList<WxkhFile> GetcompViwe(int Id)
        {
            var query = from a in _CompAttachmentSet.Include(a => a.Category).AsNoTracking()
                        where a.CompanyId == Id && a.IsDelete == 0
                        select new
                        {
                            Id = a.Id,//Wx
                           
                            CategoryName = a.Category.Name, //Wx
                            
                            FileName = a.FileName,//Wx
                          

                        };
            var local = from a in query.AsEnumerable()
                        select new WxkhFile
                        {
                            Id = a.Id,
                          
                            CategoryName = a.CategoryName,
                            
                            FileName = a.FileName,
                          
                        };
            var  filelist= local.ToList();
            return filelist;
        }

    }
}
