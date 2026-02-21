using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using NF.ViewModel.Models.Utility;
using NF.ViewModel.Extend.Enums;
using NF.Common.Extend;

namespace NF.BLL
{
    /// <summary>
    /// 合同统计字段
    /// </summary>
    public partial class ContStatisticService
    {
        public bool AddData(ContStatistic info)
        {
            var staInfo = _ContStatisticSet.Where(a => a.ContId == info.ContId).FirstOrDefault();
            if (staInfo == null)
            {
                
                Add(info);
                return true;
            }
            else
            {
                staInfo.InvoiceAmount = staInfo.InvoiceAmount + (info.InvoiceAmount ?? 0);
                staInfo.ActualAmount = staInfo.ActualAmount + (info.ActualAmount ?? 0);
                staInfo.CompInAm = staInfo.CompInAm + (info.CompInAm ?? 0);
                staInfo.CompActAm = staInfo.CompActAm + (info.CompActAm ?? 0);
                return Update(staInfo);
            }

        }
    }
}
