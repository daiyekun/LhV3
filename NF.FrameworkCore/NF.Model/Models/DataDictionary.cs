using System;
using System.Collections.Generic;

namespace NF.Model.Models
{
    public partial class DataDictionary
    {
        public DataDictionary()
        {
            BcAttachments = new HashSet<BcAttachment>();
            CaseManagerDisputeTypeNavigations = new HashSet<CaseManager>();
            CaseManagerPreTypeNavigations = new HashSet<CaseManager>();
            CaseManagerUrgentNavigations = new HashSet<CaseManager>();
            CompAttachments = new HashSet<CompAttachment>();
            CompanyCaredits = new HashSet<Company>();
            CompanyCompClasses = new HashSet<Company>();
            CompanyCompTypes = new HashSet<Company>();
            CompanyLevels = new HashSet<Company>();
            ContSubDes = new HashSet<ContSubDe>();
            ContTxtTemplateHistTepTypeNavigations = new HashSet<ContTxtTemplateHist>();
            ContTxtTemplateHistTextTypeNavigations = new HashSet<ContTxtTemplateHist>();
            ContTxtTemplateTepTypeNavigations = new HashSet<ContTxtTemplate>();
            ContTxtTemplateTextTypeNavigations = new HashSet<ContTxtTemplate>();
            ContractInfoContSources = new HashSet<ContractInfo>();
            ContractInfoContTypes = new HashSet<ContractInfo>();
            ContractInfoHistoryContSources = new HashSet<ContractInfoHistory>();
            ContractInfoHistoryContTypes = new HashSet<ContractInfoHistory>();
            Departments = new HashSet<Department>();
            DisputeManagerDisputeTypeNavigations = new HashSet<DisputeManager>();
            DisputeManagerUrgentNavigations = new HashSet<DisputeManager>();
            ProjAttachments = new HashSet<ProjAttachment>();
            ProjectManagers = new HashSet<ProjectManager>();
        }

        public int Id { get; set; }
        public int? Pid { get; set; }
        public string Name { get; set; }
        public int? Sort { get; set; }
        public string Dtype { get; set; }
        public string Remark { get; set; }
        public byte? FundsNature { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateDatetime { get; set; }
        public int? ModifyUserId { get; set; }
        public DateTime? ModifyDatetime { get; set; }
        public int? DtypeNumber { get; set; }
        public byte? IsDelete { get; set; }
        public string ShortName { get; set; }

        public virtual UserInfor CreateUser { get; set; }
        public virtual UserInfor ModifyUser { get; set; }
        public virtual ICollection<BcAttachment> BcAttachments { get; set; }
        public virtual ICollection<CaseManager> CaseManagerDisputeTypeNavigations { get; set; }
        public virtual ICollection<CaseManager> CaseManagerPreTypeNavigations { get; set; }
        public virtual ICollection<CaseManager> CaseManagerUrgentNavigations { get; set; }
        public virtual ICollection<CompAttachment> CompAttachments { get; set; }
        public virtual ICollection<Company> CompanyCaredits { get; set; }
        public virtual ICollection<Company> CompanyCompClasses { get; set; }
        public virtual ICollection<Company> CompanyCompTypes { get; set; }
        public virtual ICollection<Company> CompanyLevels { get; set; }
        public virtual ICollection<ContSubDe> ContSubDes { get; set; }
        public virtual ICollection<ContTxtTemplateHist> ContTxtTemplateHistTepTypeNavigations { get; set; }
        public virtual ICollection<ContTxtTemplateHist> ContTxtTemplateHistTextTypeNavigations { get; set; }
        public virtual ICollection<ContTxtTemplate> ContTxtTemplateTepTypeNavigations { get; set; }
        public virtual ICollection<ContTxtTemplate> ContTxtTemplateTextTypeNavigations { get; set; }
        public virtual ICollection<ContractInfo> ContractInfoContSources { get; set; }
        public virtual ICollection<ContractInfo> ContractInfoContTypes { get; set; }
        public virtual ICollection<ContractInfoHistory> ContractInfoHistoryContSources { get; set; }
        public virtual ICollection<ContractInfoHistory> ContractInfoHistoryContTypes { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<DisputeManager> DisputeManagerDisputeTypeNavigations { get; set; }
        public virtual ICollection<DisputeManager> DisputeManagerUrgentNavigations { get; set; }
        public virtual ICollection<ProjAttachment> ProjAttachments { get; set; }
        public virtual ICollection<ProjectManager> ProjectManagers { get; set; }
    }
}
