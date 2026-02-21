using NF.Model.Models;
using NF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.IBLL
{/// <summary>
 /// 合同文本
 /// </summary>
    public partial interface IContTextService
    {
        IList<WxCountText> WxShowViews(int Id);
    }
}
