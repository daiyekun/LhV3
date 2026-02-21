using NF.ViewModel;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.IBLL
{
  public partial  interface ICompAttachmentService
    {
       IList<WxkhFile> GetcompViwe(int Id);
    }
}
