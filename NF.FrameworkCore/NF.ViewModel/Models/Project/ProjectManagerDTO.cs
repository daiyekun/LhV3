using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 项目DTO
    /// </summary>
   public class ProjectManagerDTO
    {
        public int Id { get; set; }
        public int? Pid { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int CategoryId { get; set; }
        public DateTime? PlanBeginDateTime { get; set; }
        public DateTime? PlanCompleteDateTime { get; set; }
        public DateTime? ActualBeginDateTime { get; set; }
        public DateTime? ActualCompleteDateTime { get; set; }
        public decimal? BugetCollectAmountMoney { get; set; }
        public int? BudgetCollectCurrencyId { get; set; }
        public decimal? BudgetPayAmountMoney { get; set; }
        public int? BudgetPayCurrencyId { get; set; }
        public int? PrincipalUserId { get; set; }
        public int? Pstate { get; set; }
        public string Remark { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
    }


}
