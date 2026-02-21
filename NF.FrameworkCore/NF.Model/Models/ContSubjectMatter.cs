using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class ContSubjectMatter
    {
        public ContSubjectMatter()
        {
            ContSubDeliveries = new HashSet<ContSubDelivery>();
            ContSubDes = new HashSet<ContSubDe>();
        }

        public int Id { get; set; }
        public int? ContId { get; set; }
        public string Name { get; set; }
        public string Spec { get; set; }
        public string Stype { get; set; }
        public string Unit { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Price { get; set; }
        public string Remark { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public byte IsDelete { get; set; }
        public int? BcInstanceId { get; set; }
        public byte? IsFromCategory { get; set; }
        public decimal? DiscountRate { get; set; }
        public decimal? SubTotalRate { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? ComplateAmount { get; set; }
        public DateTime? PlanDateTime { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? AmountMoney { get; set; }
        public decimal? NominalQuote { get; set; }
        public decimal? NominalRate { get; set; }
        public decimal? DelNum { get; set; }
        public DateTime? SjJfRq { get; set; }
        public byte? SubState { get; set; }
        public string Field1 { get; set; }

        public virtual BcInstance BcInstance { get; set; }
        public virtual ContractInfo Cont { get; set; }
        public virtual ICollection<ContSubDelivery> ContSubDeliveries { get; set; }
        public virtual ICollection<ContSubDe> ContSubDes { get; set; }
    }
}
