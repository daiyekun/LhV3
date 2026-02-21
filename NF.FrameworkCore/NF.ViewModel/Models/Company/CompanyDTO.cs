using System;
using System.Collections.Generic;
using System.Text;

namespace NF.ViewModel.Models
{
    /// <summary>
    /// 合同对方实体类
    /// </summary>
    public  class CompanyDTO
    {
        public int Id { get; set; }
        public byte? Ctype { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int? LevelId { get; set; }
        public int? CareditId { get; set; }
        public int? CompClassId { get; set; }
        public int? CompTypeId { get; set; }
        public byte IsDelete { get; set; }
        public int? CountryId { get; set; }
        public int? ProvinceId { get; set; }
        public int? CityId { get; set; }
        public string Trade { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string RegisterCapital { get; set; }
        public string RegisterAddress { get; set; }
        public DateTime? FoundDateTime { get; set; }
        public string BusinessTerm { get; set; }
        public DateTime? ExpirationDateTime { get; set; }
        public string InvoiceTitle { get; set; }
        public string TaxIdentification { get; set; }
        public string InvoiceAddress { get; set; }
        public string InvoiceTel { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public string PaidUpCapital { get; set; }
        public string LegalPerson { get; set; }
        public string WebSite { get; set; }
        public string FirstContact { get; set; }
        public string FirstContactDept { get; set; }
        public string FirstContactPosition { get; set; }
        public string FirstContactTel { get; set; }
        public string FirstContactMobile { get; set; }
        public string FirstContactQq { get; set; }
        public string FirstContactEmail { get; set; }
        public string Remark { get; set; }
        public byte Cstate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int ModifyUserId { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public int? PrincipalUserId { get; set; }
        public string BusinessScope { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }



    }

     
}
