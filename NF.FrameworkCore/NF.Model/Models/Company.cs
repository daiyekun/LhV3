using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class Company
    {
        public Company()
        {
            ContractInfoCompId3Navigations = new HashSet<ContractInfo>();
            ContractInfoCompId4Navigations = new HashSet<ContractInfo>();
            ContractInfoComps = new HashSet<ContractInfo>();
            ContractInfoHistoryCompId3Navigations = new HashSet<ContractInfoHistory>();
            ContractInfoHistoryCompId4Navigations = new HashSet<ContractInfoHistory>();
            ContractInfoHistoryComps = new HashSet<ContractInfoHistory>();
            DisputeManagers = new HashSet<DisputeManager>();
            Inquiries = new HashSet<Inquiry>();
            OpenBids = new HashSet<OpenBid>();
            OpenTenderConditions = new HashSet<OpenTenderCondition>();
            OpeningSituationInfors = new HashSet<OpeningSituationInfor>();
            Questionings = new HashSet<Questioning>();
            TenderInfors = new HashSet<TenderInfor>();
        }

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
        public byte? WfState { get; set; }
        public int? WfItem { get; set; }
        public string WfCurrNodeName { get; set; }

        public virtual DataDictionary Caredit { get; set; }
        public virtual DataDictionary CompClass { get; set; }
        public virtual DataDictionary CompType { get; set; }
        public virtual UserInfor CreateUser { get; set; }
        public virtual DataDictionary Level { get; set; }
        public virtual UserInfor ModifyUser { get; set; }
        public virtual UserInfor PrincipalUser { get; set; }
        public virtual ICollection<ContractInfo> ContractInfoCompId3Navigations { get; set; }
        public virtual ICollection<ContractInfo> ContractInfoCompId4Navigations { get; set; }
        public virtual ICollection<ContractInfo> ContractInfoComps { get; set; }
        public virtual ICollection<ContractInfoHistory> ContractInfoHistoryCompId3Navigations { get; set; }
        public virtual ICollection<ContractInfoHistory> ContractInfoHistoryCompId4Navigations { get; set; }
        public virtual ICollection<ContractInfoHistory> ContractInfoHistoryComps { get; set; }
        public virtual ICollection<DisputeManager> DisputeManagers { get; set; }
        public virtual ICollection<Inquiry> Inquiries { get; set; }
        public virtual ICollection<OpenBid> OpenBids { get; set; }
        public virtual ICollection<OpenTenderCondition> OpenTenderConditions { get; set; }
        public virtual ICollection<OpeningSituationInfor> OpeningSituationInfors { get; set; }
        public virtual ICollection<Questioning> Questionings { get; set; }
        public virtual ICollection<TenderInfor> TenderInfors { get; set; }
    }
}
