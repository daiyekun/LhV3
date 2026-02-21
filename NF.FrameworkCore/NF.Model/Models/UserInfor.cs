using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class UserInfor
    {
        public UserInfor()
        {
            BcAttachmentCreateUsers = new HashSet<BcAttachment>();
            BcAttachmentModifyUsers = new HashSet<BcAttachment>();
            BcInstanceCreateUsers = new HashSet<BcInstance>();
            BcInstanceModifyUsers = new HashSet<BcInstance>();
            CaseManagers = new HashSet<CaseManager>();
            CompAttachments = new HashSet<CompAttachment>();
            CompContactCreateUsers = new HashSet<CompContact>();
            CompContactModifyUsers = new HashSet<CompContact>();
            CompDescriptionCreateUsers = new HashSet<CompDescription>();
            CompDescriptionModifyUsers = new HashSet<CompDescription>();
            CompanyCreateUsers = new HashSet<Company>();
            CompanyModifyUsers = new HashSet<Company>();
            CompanyPrincipalUsers = new HashSet<Company>();
            ContActualFinanceConfirmUsers = new HashSet<ContActualFinance>();
            ContActualFinanceCreateUsers = new HashSet<ContActualFinance>();
            ContInvoiceConfirmUsers = new HashSet<ContInvoice>();
            ContInvoiceCreateUsers = new HashSet<ContInvoice>();
            ContSubDeCreateUsers = new HashSet<ContSubDe>();
            ContSubDeDeliverUsers = new HashSet<ContSubDe>();
            ContTxtTemplateHists = new HashSet<ContTxtTemplateHist>();
            ContTxtTemplates = new HashSet<ContTxtTemplate>();
            ContractInfoCreateUsers = new HashSet<ContractInfo>();
            ContractInfoHistoryCreateUsers = new HashSet<ContractInfoHistory>();
            ContractInfoHistoryPrincipalUsers = new HashSet<ContractInfoHistory>();
            ContractInfoPrincipalUsers = new HashSet<ContractInfo>();
            DataDictionaryCreateUsers = new HashSet<DataDictionary>();
            DataDictionaryModifyUsers = new HashSet<DataDictionary>();
            DisputeManagerCreateUsers = new HashSet<DisputeManager>();
            DisputeManagerHandlers = new HashSet<DisputeManager>();
            FlowTempHists = new HashSet<FlowTempHist>();
            FlowTemps = new HashSet<FlowTemp>();
            Inquiries = new HashSet<Inquiry>();
            LoginLogs = new HashSet<LoginLog>();
            OptionLogs = new HashSet<OptionLog>();
            ProjAttachments = new HashSet<ProjAttachment>();
            ProjDescriptions = new HashSet<ProjDescription>();
            ProjSchedules = new HashSet<ProjSchedule>();
            ProjectManagerCreateUsers = new HashSet<ProjectManager>();
            ProjectManagerModifyUsers = new HashSet<ProjectManager>();
            ProjectManagerPrincipalUsers = new HashSet<ProjectManager>();
            Questionings = new HashSet<Questioning>();
            RoleCreateUsers = new HashSet<Role>();
            RoleModifyUsers = new HashSet<Role>();
            SysModelCreateUsers = new HashSet<SysModel>();
            SysModelModifyUsers = new HashSet<SysModel>();
            TenderInfors = new HashSet<TenderInfor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Pwd { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string DisplyName { get; set; }
        public int Sex { get; set; }
        public int? Age { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime? EntryDatetime { get; set; }
        public string IdNo { get; set; }
        public string Address { get; set; }
        public int? DepartmentId { get; set; }
        public int? Sort { get; set; }
        public int? State { get; set; }
        public string Remark { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDatetime { get; set; }
        public int? ModifyUserId { get; set; }
        public DateTime? ModifyDatetime { get; set; }
        public int? IsDelete { get; set; }
        public int Ustart { get; set; }
        public string Msystem { get; set; }
        public string Minfo { get; set; }
        public string PhName { get; set; }
        public string PhPath { get; set; }
        public int? UserEs { get; set; }
        public int? UserEsTy { get; set; }
        public string WxCode { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<BcAttachment> BcAttachmentCreateUsers { get; set; }
        public virtual ICollection<BcAttachment> BcAttachmentModifyUsers { get; set; }
        public virtual ICollection<BcInstance> BcInstanceCreateUsers { get; set; }
        public virtual ICollection<BcInstance> BcInstanceModifyUsers { get; set; }
        public virtual ICollection<CaseManager> CaseManagers { get; set; }
        public virtual ICollection<CompAttachment> CompAttachments { get; set; }
        public virtual ICollection<CompContact> CompContactCreateUsers { get; set; }
        public virtual ICollection<CompContact> CompContactModifyUsers { get; set; }
        public virtual ICollection<CompDescription> CompDescriptionCreateUsers { get; set; }
        public virtual ICollection<CompDescription> CompDescriptionModifyUsers { get; set; }
        public virtual ICollection<Company> CompanyCreateUsers { get; set; }
        public virtual ICollection<Company> CompanyModifyUsers { get; set; }
        public virtual ICollection<Company> CompanyPrincipalUsers { get; set; }
        public virtual ICollection<ContActualFinance> ContActualFinanceConfirmUsers { get; set; }
        public virtual ICollection<ContActualFinance> ContActualFinanceCreateUsers { get; set; }
        public virtual ICollection<ContInvoice> ContInvoiceConfirmUsers { get; set; }
        public virtual ICollection<ContInvoice> ContInvoiceCreateUsers { get; set; }
        public virtual ICollection<ContSubDe> ContSubDeCreateUsers { get; set; }
        public virtual ICollection<ContSubDe> ContSubDeDeliverUsers { get; set; }
        public virtual ICollection<ContTxtTemplateHist> ContTxtTemplateHists { get; set; }
        public virtual ICollection<ContTxtTemplate> ContTxtTemplates { get; set; }
        public virtual ICollection<ContractInfo> ContractInfoCreateUsers { get; set; }
        public virtual ICollection<ContractInfoHistory> ContractInfoHistoryCreateUsers { get; set; }
        public virtual ICollection<ContractInfoHistory> ContractInfoHistoryPrincipalUsers { get; set; }
        public virtual ICollection<ContractInfo> ContractInfoPrincipalUsers { get; set; }
        public virtual ICollection<DataDictionary> DataDictionaryCreateUsers { get; set; }
        public virtual ICollection<DataDictionary> DataDictionaryModifyUsers { get; set; }
        public virtual ICollection<DisputeManager> DisputeManagerCreateUsers { get; set; }
        public virtual ICollection<DisputeManager> DisputeManagerHandlers { get; set; }
        public virtual ICollection<FlowTempHist> FlowTempHists { get; set; }
        public virtual ICollection<FlowTemp> FlowTemps { get; set; }
        public virtual ICollection<Inquiry> Inquiries { get; set; }
        public virtual ICollection<LoginLog> LoginLogs { get; set; }
        public virtual ICollection<OptionLog> OptionLogs { get; set; }
        public virtual ICollection<ProjAttachment> ProjAttachments { get; set; }
        public virtual ICollection<ProjDescription> ProjDescriptions { get; set; }
        public virtual ICollection<ProjSchedule> ProjSchedules { get; set; }
        public virtual ICollection<ProjectManager> ProjectManagerCreateUsers { get; set; }
        public virtual ICollection<ProjectManager> ProjectManagerModifyUsers { get; set; }
        public virtual ICollection<ProjectManager> ProjectManagerPrincipalUsers { get; set; }
        public virtual ICollection<Questioning> Questionings { get; set; }
        public virtual ICollection<Role> RoleCreateUsers { get; set; }
        public virtual ICollection<Role> RoleModifyUsers { get; set; }
        public virtual ICollection<SysModel> SysModelCreateUsers { get; set; }
        public virtual ICollection<SysModel> SysModelModifyUsers { get; set; }
        public virtual ICollection<TenderInfor> TenderInfors { get; set; }
    }
}
