using Microsoft.EntityFrameworkCore;
using NF.IBLL;
using NF.Model.Models;
///****************************************************
///代码自动生成,需要修改builder里构造函数数据库连接字符串即可
///如果有个性业务在建立一个public partial interface 
///如有报错，添加引用NuGet  PetaPoco.NetCore (1.0.1)、T4 (2.0.1)
///****************************************************
namespace NF.BLL
{

    public partial class FlowTempService : BaseService<FlowTemp>, IFlowTempService
    {
        private DbSet<FlowTemp> _FlowTempSet = null;
        public FlowTempService(DbContext dbContext)
           : base(dbContext)
        {
            _FlowTempSet = base.Db.Set<FlowTemp>();
        }

        public FlowTempService() { }
    }



    public partial class FlowTempHistService : BaseService<FlowTempHist>, IFlowTempHistService
    {
        private DbSet<FlowTempHist> _FlowTempHistSet = null;
        public FlowTempHistService(DbContext dbContext)
           : base(dbContext)
        {
            _FlowTempHistSet = base.Db.Set<FlowTempHist>();
        }

        public FlowTempHistService() { }
    }



    public partial class FlowTempNodeService : BaseService<FlowTempNode>, IFlowTempNodeService
    {
        private DbSet<FlowTempNode> _FlowTempNodeSet = null;
        public FlowTempNodeService(DbContext dbContext)
           : base(dbContext)
        {
            _FlowTempNodeSet = base.Db.Set<FlowTempNode>();
        }

        public FlowTempNodeService() { }
    }



    public partial class FlowTempNodeHistService : BaseService<FlowTempNodeHist>, IFlowTempNodeHistService
    {
        private DbSet<FlowTempNodeHist> _FlowTempNodeHistSet = null;
        public FlowTempNodeHistService(DbContext dbContext)
           : base(dbContext)
        {
            _FlowTempNodeHistSet = base.Db.Set<FlowTempNodeHist>();
        }

        public FlowTempNodeHistService() { }
    }



    public partial class FlowTempNodeInfoService : BaseService<FlowTempNodeInfo>, IFlowTempNodeInfoService
    {
        private DbSet<FlowTempNodeInfo> _FlowTempNodeInfoSet = null;
        public FlowTempNodeInfoService(DbContext dbContext)
           : base(dbContext)
        {
            _FlowTempNodeInfoSet = base.Db.Set<FlowTempNodeInfo>();
        }

        public FlowTempNodeInfoService() { }
    }



    public partial class FlowTempNodeInfoHistService : BaseService<FlowTempNodeInfoHist>, IFlowTempNodeInfoHistService
    {
        private DbSet<FlowTempNodeInfoHist> _FlowTempNodeInfoHistSet = null;
        public FlowTempNodeInfoHistService(DbContext dbContext)
           : base(dbContext)
        {
            _FlowTempNodeInfoHistSet = base.Db.Set<FlowTempNodeInfoHist>();
        }

        public FlowTempNodeInfoHistService() { }
    }



    public partial class GroupInfoService : BaseService<GroupInfo>, IGroupInfoService
    {
        private DbSet<GroupInfo> _GroupInfoSet = null;
        public GroupInfoService(DbContext dbContext)
           : base(dbContext)
        {
            _GroupInfoSet = base.Db.Set<GroupInfo>();
        }

        public GroupInfoService() { }
    }



    public partial class GroupUserService : BaseService<GroupUser>, IGroupUserService
    {
        private DbSet<GroupUser> _GroupUserSet = null;
        public GroupUserService(DbContext dbContext)
           : base(dbContext)
        {
            _GroupUserSet = base.Db.Set<GroupUser>();
        }

        public GroupUserService() { }
    }



    public partial class ActFinceFileService : BaseService<ActFinceFile>, IActFinceFileService
    {
        private DbSet<ActFinceFile> _ActFinceFileSet = null;
        public ActFinceFileService(DbContext dbContext)
           : base(dbContext)
        {
            _ActFinceFileSet = base.Db.Set<ActFinceFile>();
        }

        public ActFinceFileService() { }
    }



    public partial class InquirerService : BaseService<Inquirer>, IInquirerService
    {
        private DbSet<Inquirer> _InquirerSet = null;
        public InquirerService(DbContext dbContext)
           : base(dbContext)
        {
            _InquirerSet = base.Db.Set<Inquirer>();
        }

        public InquirerService() { }
    }



    public partial class AjglAttachmentService : BaseService<AjglAttachment>, IAjglAttachmentService
    {
        private DbSet<AjglAttachment> _AjglAttachmentSet = null;
        public AjglAttachmentService(DbContext dbContext)
           : base(dbContext)
        {
            _AjglAttachmentSet = base.Db.Set<AjglAttachment>();
        }

        public AjglAttachmentService() { }
    }



    public partial class InquiryService : BaseService<Inquiry>, IInquiryService
    {
        private DbSet<Inquiry> _InquirySet = null;
        public InquiryService(DbContext dbContext)
           : base(dbContext)
        {
            _InquirySet = base.Db.Set<Inquiry>();
        }

        public InquiryService() { }
    }



    public partial class AjglSpwsAttachmentService : BaseService<AjglSpwsAttachment>, IAjglSpwsAttachmentService
    {
        private DbSet<AjglSpwsAttachment> _AjglSpwsAttachmentSet = null;
        public AjglSpwsAttachmentService(DbContext dbContext)
           : base(dbContext)
        {
            _AjglSpwsAttachmentSet = base.Db.Set<AjglSpwsAttachment>();
        }

        public AjglSpwsAttachmentService() { }
    }



    public partial class InquiryAttachmentService : BaseService<InquiryAttachment>, IInquiryAttachmentService
    {
        private DbSet<InquiryAttachment> _InquiryAttachmentSet = null;
        public InquiryAttachmentService(DbContext dbContext)
           : base(dbContext)
        {
            _InquiryAttachmentSet = base.Db.Set<InquiryAttachment>();
        }

        public InquiryAttachmentService() { }
    }



    public partial class AjglSsBqAttachmentService : BaseService<AjglSsBqAttachment>, IAjglSsBqAttachmentService
    {
        private DbSet<AjglSsBqAttachment> _AjglSsBqAttachmentSet = null;
        public AjglSsBqAttachmentService(DbContext dbContext)
           : base(dbContext)
        {
            _AjglSsBqAttachmentSet = base.Db.Set<AjglSsBqAttachment>();
        }

        public AjglSsBqAttachmentService() { }
    }



    public partial class InterviewpeopleService : BaseService<Interviewpeople>, IInterviewpeopleService
    {
        private DbSet<Interviewpeople> _InterviewpeopleSet = null;
        public InterviewpeopleService(DbContext dbContext)
           : base(dbContext)
        {
            _InterviewpeopleSet = base.Db.Set<Interviewpeople>();
        }

        public InterviewpeopleService() { }


    }



    public partial class AjglXgLxService : BaseService<AjglXgLx>, IAjglXgLxService
    {
        private DbSet<AjglXgLx> _AjglXgLxSet = null;
        public AjglXgLxService(DbContext dbContext)
           : base(dbContext)
        {
            _AjglXgLxSet = base.Db.Set<AjglXgLx>();
        }

        public AjglXgLxService() { }
    }



    public partial class InvoDescriptionService : BaseService<InvoDescription>, IInvoDescriptionService
    {
        private DbSet<InvoDescription> _InvoDescriptionSet = null;
        public InvoDescriptionService(DbContext dbContext)
           : base(dbContext)
        {
            _InvoDescriptionSet = base.Db.Set<InvoDescription>();
        }

        public InvoDescriptionService() { }
    }



    public partial class AppGroupUserService : BaseService<AppGroupUser>, IAppGroupUserService
    {
        private DbSet<AppGroupUser> _AppGroupUserSet = null;
        public AppGroupUserService(DbContext dbContext)
           : base(dbContext)
        {
            _AppGroupUserSet = base.Db.Set<AppGroupUser>();
        }

        public AppGroupUserService() { }
    }



    public partial class InvoFileService : BaseService<InvoFile>, IInvoFileService
    {
        private DbSet<InvoFile> _InvoFileSet = null;
        public InvoFileService(DbContext dbContext)
           : base(dbContext)
        {
            _InvoFileSet = base.Db.Set<InvoFile>();
        }

        public InvoFileService() { }
    }



    public partial class AppInstService : BaseService<AppInst>, IAppInstService
    {
        private DbSet<AppInst> _AppInstSet = null;
        public AppInstService(DbContext dbContext)
           : base(dbContext)
        {
            _AppInstSet = base.Db.Set<AppInst>();
        }

        public AppInstService() { }
    }



    public partial class InvoiceCheckService : BaseService<InvoiceCheck>, IInvoiceCheckService
    {
        private DbSet<InvoiceCheck> _InvoiceCheckSet = null;
        public InvoiceCheckService(DbContext dbContext)
           : base(dbContext)
        {
            _InvoiceCheckSet = base.Db.Set<InvoiceCheck>();
        }

        public InvoiceCheckService() { }
    }



    public partial class AppInstHistService : BaseService<AppInstHist>, IAppInstHistService
    {
        private DbSet<AppInstHist> _AppInstHistSet = null;
        public AppInstHistService(DbContext dbContext)
           : base(dbContext)
        {
            _AppInstHistSet = base.Db.Set<AppInstHist>();
        }

        public AppInstHistService() { }
    }



    public partial class LoginLogService : BaseService<LoginLog>, ILoginLogService
    {
        private DbSet<LoginLog> _LoginLogSet = null;
        public LoginLogService(DbContext dbContext)
           : base(dbContext)
        {
            _LoginLogSet = base.Db.Set<LoginLog>();
        }

        public LoginLogService() { }
    }



    public partial class AppInstNodeService : BaseService<AppInstNode>, IAppInstNodeService
    {
        private DbSet<AppInstNode> _AppInstNodeSet = null;
        public AppInstNodeService(DbContext dbContext)
           : base(dbContext)
        {
            _AppInstNodeSet = base.Db.Set<AppInstNode>();
        }

        public AppInstNodeService() { }
    }



    public partial class ContPlanFinanceHistoryService : BaseService<ContPlanFinanceHistory>, IContPlanFinanceHistoryService
    {
        private DbSet<ContPlanFinanceHistory> _ContPlanFinanceHistorySet = null;
        public ContPlanFinanceHistoryService(DbContext dbContext)
           : base(dbContext)
        {
            _ContPlanFinanceHistorySet = base.Db.Set<ContPlanFinanceHistory>();
        }

        public ContPlanFinanceHistoryService() { }
    }



    public partial class OpenBidService : BaseService<OpenBid>, IOpenBidService
    {
        private DbSet<OpenBid> _OpenBidSet = null;
        public OpenBidService(DbContext dbContext)
           : base(dbContext)
        {
            _OpenBidSet = base.Db.Set<OpenBid>();
        }

        public OpenBidService() { }
    }



    public partial class AppInstNodeAreaService : BaseService<AppInstNodeArea>, IAppInstNodeAreaService
    {
        private DbSet<AppInstNodeArea> _AppInstNodeAreaSet = null;
        public AppInstNodeAreaService(DbContext dbContext)
           : base(dbContext)
        {
            _AppInstNodeAreaSet = base.Db.Set<AppInstNodeArea>();
        }

        public AppInstNodeAreaService() { }
    }



    public partial class OpeningSituationInforService : BaseService<OpeningSituationInfor>, IOpeningSituationInforService
    {
        private DbSet<OpeningSituationInfor> _OpeningSituationInforSet = null;
        public OpeningSituationInforService(DbContext dbContext)
           : base(dbContext)
        {
            _OpeningSituationInforSet = base.Db.Set<OpeningSituationInfor>();
        }

        public OpeningSituationInforService() { }
    }



    public partial class AppInstNodeAreaHistService : BaseService<AppInstNodeAreaHist>, IAppInstNodeAreaHistService
    {
        private DbSet<AppInstNodeAreaHist> _AppInstNodeAreaHistSet = null;
        public AppInstNodeAreaHistService(DbContext dbContext)
           : base(dbContext)
        {
            _AppInstNodeAreaHistSet = base.Db.Set<AppInstNodeAreaHist>();
        }

        public AppInstNodeAreaHistService() { }
    }



    public partial class OpenTenderConditionService : BaseService<OpenTenderCondition>, IOpenTenderConditionService
    {
        private DbSet<OpenTenderCondition> _OpenTenderConditionSet = null;
        public OpenTenderConditionService(DbContext dbContext)
           : base(dbContext)
        {
            _OpenTenderConditionSet = base.Db.Set<OpenTenderCondition>();
        }

        public OpenTenderConditionService() { }
    }



    public partial class AppInstNodeHistService : BaseService<AppInstNodeHist>, IAppInstNodeHistService
    {
        private DbSet<AppInstNodeHist> _AppInstNodeHistSet = null;
        public AppInstNodeHistService(DbContext dbContext)
           : base(dbContext)
        {
            _AppInstNodeHistSet = base.Db.Set<AppInstNodeHist>();
        }

        public AppInstNodeHistService() { }
    }



    public partial class OptionLogService : BaseService<OptionLog>, IOptionLogService
    {
        private DbSet<OptionLog> _OptionLogSet = null;
        public OptionLogService(DbContext dbContext)
           : base(dbContext)
        {
            _OptionLogSet = base.Db.Set<OptionLog>();
        }

        public OptionLogService() { }
    }



    public partial class AppInstNodeInfoService : BaseService<AppInstNodeInfo>, IAppInstNodeInfoService
    {
        private DbSet<AppInstNodeInfo> _AppInstNodeInfoSet = null;
        public AppInstNodeInfoService(DbContext dbContext)
           : base(dbContext)
        {
            _AppInstNodeInfoSet = base.Db.Set<AppInstNodeInfo>();
        }

        public AppInstNodeInfoService() { }
    }



    public partial class PlanFinnCheckService : BaseService<PlanFinnCheck>, IPlanFinnCheckService
    {
        private DbSet<PlanFinnCheck> _PlanFinnCheckSet = null;
        public PlanFinnCheckService(DbContext dbContext)
           : base(dbContext)
        {
            _PlanFinnCheckSet = base.Db.Set<PlanFinnCheck>();
        }

        public PlanFinnCheckService() { }
    }



    public partial class AppInstNodeInfoHistService : BaseService<AppInstNodeInfoHist>, IAppInstNodeInfoHistService
    {
        private DbSet<AppInstNodeInfoHist> _AppInstNodeInfoHistSet = null;
        public AppInstNodeInfoHistService(DbContext dbContext)
           : base(dbContext)
        {
            _AppInstNodeInfoHistSet = base.Db.Set<AppInstNodeInfoHist>();
        }

        public AppInstNodeInfoHistService() { }
    }



    public partial class ProjAttachmentService : BaseService<ProjAttachment>, IProjAttachmentService
    {
        private DbSet<ProjAttachment> _ProjAttachmentSet = null;
        public ProjAttachmentService(DbContext dbContext)
           : base(dbContext)
        {
            _ProjAttachmentSet = base.Db.Set<ProjAttachment>();
        }

        public ProjAttachmentService() { }
    }



    public partial class AppInstNodeLineService : BaseService<AppInstNodeLine>, IAppInstNodeLineService
    {
        private DbSet<AppInstNodeLine> _AppInstNodeLineSet = null;
        public AppInstNodeLineService(DbContext dbContext)
           : base(dbContext)
        {
            _AppInstNodeLineSet = base.Db.Set<AppInstNodeLine>();
        }

        public AppInstNodeLineService() { }
    }



    public partial class ProjDescriptionService : BaseService<ProjDescription>, IProjDescriptionService
    {
        private DbSet<ProjDescription> _ProjDescriptionSet = null;
        public ProjDescriptionService(DbContext dbContext)
           : base(dbContext)
        {
            _ProjDescriptionSet = base.Db.Set<ProjDescription>();
        }

        public ProjDescriptionService() { }
    }



    public partial class AppInstNodeLineHistService : BaseService<AppInstNodeLineHist>, IAppInstNodeLineHistService
    {
        private DbSet<AppInstNodeLineHist> _AppInstNodeLineHistSet = null;
        public AppInstNodeLineHistService(DbContext dbContext)
           : base(dbContext)
        {
            _AppInstNodeLineHistSet = base.Db.Set<AppInstNodeLineHist>();
        }

        public AppInstNodeLineHistService() { }
    }



    public partial class ProjectManagerService : BaseService<ProjectManager>, IProjectManagerService
    {
        private DbSet<ProjectManager> _ProjectManagerSet = null;
        public ProjectManagerService(DbContext dbContext)
           : base(dbContext)
        {
            _ProjectManagerSet = base.Db.Set<ProjectManager>();
        }

        public ProjectManagerService() { }
    }



    public partial class AppInstOpinService : BaseService<AppInstOpin>, IAppInstOpinService
    {
        private DbSet<AppInstOpin> _AppInstOpinSet = null;
        public AppInstOpinService(DbContext dbContext)
           : base(dbContext)
        {
            _AppInstOpinSet = base.Db.Set<AppInstOpin>();
        }

        public AppInstOpinService() { }
    }



    public partial class ProjScheduleService : BaseService<ProjSchedule>, IProjScheduleService
    {
        private DbSet<ProjSchedule> _ProjScheduleSet = null;
        public ProjScheduleService(DbContext dbContext)
           : base(dbContext)
        {
            _ProjScheduleSet = base.Db.Set<ProjSchedule>();
        }

        public ProjScheduleService() { }
    }



    public partial class AppInstOpinHistService : BaseService<AppInstOpinHist>, IAppInstOpinHistService
    {
        private DbSet<AppInstOpinHist> _AppInstOpinHistSet = null;
        public AppInstOpinHistService(DbContext dbContext)
           : base(dbContext)
        {
            _AppInstOpinHistSet = base.Db.Set<AppInstOpinHist>();
        }

        public AppInstOpinHistService() { }
    }



    public partial class ProvinceService : BaseService<Province>, IProvinceService
    {
        private DbSet<Province> _ProvinceSet = null;
        public ProvinceService(DbContext dbContext)
           : base(dbContext)
        {
            _ProvinceSet = base.Db.Set<Province>();
        }

        public ProvinceService() { }
    }



    public partial class BcAttachmentService : BaseService<BcAttachment>, IBcAttachmentService
    {
        private DbSet<BcAttachment> _BcAttachmentSet = null;
        public BcAttachmentService(DbContext dbContext)
           : base(dbContext)
        {
            _BcAttachmentSet = base.Db.Set<BcAttachment>();
        }

        public BcAttachmentService() { }
    }



    public partial class QuestioningService : BaseService<Questioning>, IQuestioningService
    {
        private DbSet<Questioning> _QuestioningSet = null;
        public QuestioningService(DbContext dbContext)
           : base(dbContext)
        {
            _QuestioningSet = base.Db.Set<Questioning>();
        }

        public QuestioningService() { }
    }



    public partial class BcInstanceService : BaseService<BcInstance>, IBcInstanceService
    {
        private DbSet<BcInstance> _BcInstanceSet = null;
        public BcInstanceService(DbContext dbContext)
           : base(dbContext)
        {
            _BcInstanceSet = base.Db.Set<BcInstance>();
        }

        public BcInstanceService() { }
    }



    public partial class QuestioningAttachmentService : BaseService<QuestioningAttachment>, IQuestioningAttachmentService
    {
        private DbSet<QuestioningAttachment> _QuestioningAttachmentSet = null;
        public QuestioningAttachmentService(DbContext dbContext)
           : base(dbContext)
        {
            _QuestioningAttachmentSet = base.Db.Set<QuestioningAttachment>();
        }

        public QuestioningAttachmentService() { }
    }



    public partial class BidlabelService : BaseService<Bidlabel>, IBidlabelService
    {
        private DbSet<Bidlabel> _BidlabelSet = null;
        public BidlabelService(DbContext dbContext)
           : base(dbContext)
        {
            _BidlabelSet = base.Db.Set<Bidlabel>();
        }

        public BidlabelService() { }
    }



    public partial class RemindService : BaseService<Remind>, IRemindService
    {
        private DbSet<Remind> _RemindSet = null;
        public RemindService(DbContext dbContext)
           : base(dbContext)
        {
            _RemindSet = base.Db.Set<Remind>();
        }

        public RemindService() { }
    }



    public partial class BusinessCategoryService : BaseService<BusinessCategory>, IBusinessCategoryService
    {
        private DbSet<BusinessCategory> _BusinessCategorySet = null;
        public BusinessCategoryService(DbContext dbContext)
           : base(dbContext)
        {
            _BusinessCategorySet = base.Db.Set<BusinessCategory>();
        }

        public BusinessCategoryService() { }
    }



    public partial class RoleService : BaseService<Role>, IRoleService
    {
        private DbSet<Role> _RoleSet = null;
        public RoleService(DbContext dbContext)
           : base(dbContext)
        {
            _RoleSet = base.Db.Set<Role>();
        }

        public RoleService() { }
    }



    public partial class CaseManagerService : BaseService<CaseManager>, ICaseManagerService
    {
        private DbSet<CaseManager> _CaseManagerSet = null;
        public CaseManagerService(DbContext dbContext)
           : base(dbContext)
        {
            _CaseManagerSet = base.Db.Set<CaseManager>();
        }

        public CaseManagerService() { }
    }



    public partial class RoleModuleService : BaseService<RoleModule>, IRoleModuleService
    {
        private DbSet<RoleModule> _RoleModuleSet = null;
        public RoleModuleService(DbContext dbContext)
           : base(dbContext)
        {
            _RoleModuleSet = base.Db.Set<RoleModule>();
        }

        public RoleModuleService() { }
    }



    public partial class CityService : BaseService<City>, ICityService
    {
        private DbSet<City> _CitySet = null;
        public CityService(DbContext dbContext)
           : base(dbContext)
        {
            _CitySet = base.Db.Set<City>();
        }

        public CityService() { }
    }



    public partial class RolePermissionService : BaseService<RolePermission>, IRolePermissionService
    {
        private DbSet<RolePermission> _RolePermissionSet = null;
        public RolePermissionService(DbContext dbContext)
           : base(dbContext)
        {
            _RolePermissionSet = base.Db.Set<RolePermission>();
        }

        public RolePermissionService() { }
    }



    public partial class CompanyService : BaseService<Company>, ICompanyService
    {
        private DbSet<Company> _CompanySet = null;
        public CompanyService(DbContext dbContext)
           : base(dbContext)
        {
            _CompanySet = base.Db.Set<Company>();
        }

        public CompanyService() { }
    }



    public partial class ScheduleDetailService : BaseService<ScheduleDetail>, IScheduleDetailService
    {
        private DbSet<ScheduleDetail> _ScheduleDetailSet = null;
        public ScheduleDetailService(DbContext dbContext)
           : base(dbContext)
        {
            _ScheduleDetailSet = base.Db.Set<ScheduleDetail>();
        }

        public ScheduleDetailService() { }
    }



    public partial class CompAttachmentService : BaseService<CompAttachment>, ICompAttachmentService
    {
        private DbSet<CompAttachment> _CompAttachmentSet = null;
        public CompAttachmentService(DbContext dbContext)
           : base(dbContext)
        {
            _CompAttachmentSet = base.Db.Set<CompAttachment>();
        }

        public CompAttachmentService() { }
    }



    public partial class ScheduleDetailAttachmentService : BaseService<ScheduleDetailAttachment>, IScheduleDetailAttachmentService
    {
        private DbSet<ScheduleDetailAttachment> _ScheduleDetailAttachmentSet = null;
        public ScheduleDetailAttachmentService(DbContext dbContext)
           : base(dbContext)
        {
            _ScheduleDetailAttachmentSet = base.Db.Set<ScheduleDetailAttachment>();
        }

        public ScheduleDetailAttachmentService() { }
    }



    public partial class CompContactService : BaseService<CompContact>, ICompContactService
    {
        private DbSet<CompContact> _CompContactSet = null;
        public CompContactService(DbContext dbContext)
           : base(dbContext)
        {
            _CompContactSet = base.Db.Set<CompContact>();
        }

        public CompContactService() { }
    }



    public partial class ScheduleListService : BaseService<ScheduleList>, IScheduleListService
    {
        private DbSet<ScheduleList> _ScheduleListSet = null;
        public ScheduleListService(DbContext dbContext)
           : base(dbContext)
        {
            _ScheduleListSet = base.Db.Set<ScheduleList>();
        }

        public ScheduleListService() { }
    }



    public partial class CompDescriptionService : BaseService<CompDescription>, ICompDescriptionService
    {
        private DbSet<CompDescription> _CompDescriptionSet = null;
        public CompDescriptionService(DbContext dbContext)
           : base(dbContext)
        {
            _CompDescriptionSet = base.Db.Set<CompDescription>();
        }

        public CompDescriptionService() { }
    }



    public partial class ScheduleManagementService : BaseService<ScheduleManagement>, IScheduleManagementService
    {
        private DbSet<ScheduleManagement> _ScheduleManagementSet = null;
        public ScheduleManagementService(DbContext dbContext)
           : base(dbContext)
        {
            _ScheduleManagementSet = base.Db.Set<ScheduleManagement>();
        }

        public ScheduleManagementService() { }
    }



    public partial class ContActualFinanceService : BaseService<ContActualFinance>, IContActualFinanceService
    {
        private DbSet<ContActualFinance> _ContActualFinanceSet = null;
        public ContActualFinanceService(DbContext dbContext)
           : base(dbContext)
        {
            _ContActualFinanceSet = base.Db.Set<ContActualFinance>();
        }

        public ContActualFinanceService() { }
    }



    public partial class ScheduleManagementAttachmentService : BaseService<ScheduleManagementAttachment>, IScheduleManagementAttachmentService
    {
        private DbSet<ScheduleManagementAttachment> _ScheduleManagementAttachmentSet = null;
        public ScheduleManagementAttachmentService(DbContext dbContext)
           : base(dbContext)
        {
            _ScheduleManagementAttachmentSet = base.Db.Set<ScheduleManagementAttachment>();
        }

        public ScheduleManagementAttachmentService() { }
    }



    public partial class ContAttachmentService : BaseService<ContAttachment>, IContAttachmentService
    {
        private DbSet<ContAttachment> _ContAttachmentSet = null;
        public ContAttachmentService(DbContext dbContext)
           : base(dbContext)
        {
            _ContAttachmentSet = base.Db.Set<ContAttachment>();
        }

        public ContAttachmentService() { }
    }



    public partial class SealManagerService : BaseService<SealManager>, ISealManagerService
    {
        private DbSet<SealManager> _SealManagerSet = null;
        public SealManagerService(DbContext dbContext)
           : base(dbContext)
        {
            _SealManagerSet = base.Db.Set<SealManager>();
        }

        public SealManagerService() { }
    }



    public partial class ContConsultService : BaseService<ContConsult>, IContConsultService
    {
        private DbSet<ContConsult> _ContConsultSet = null;
        public ContConsultService(DbContext dbContext)
           : base(dbContext)
        {
            _ContConsultSet = base.Db.Set<ContConsult>();
        }

        public ContConsultService() { }
    }



    public partial class SuccessfulBidderLableService : BaseService<SuccessfulBidderLable>, ISuccessfulBidderLableService
    {
        private DbSet<SuccessfulBidderLable> _SuccessfulBidderLableSet = null;
        public SuccessfulBidderLableService(DbContext dbContext)
           : base(dbContext)
        {
            _SuccessfulBidderLableSet = base.Db.Set<SuccessfulBidderLable>();
        }

        public SuccessfulBidderLableService() { }
    }



    public partial class ContCustomFieldService : BaseService<ContCustomField>, IContCustomFieldService
    {
        private DbSet<ContCustomField> _ContCustomFieldSet = null;
        public ContCustomFieldService(DbContext dbContext)
           : base(dbContext)
        {
            _ContCustomFieldSet = base.Db.Set<ContCustomField>();
        }

        public ContCustomFieldService() { }
    }



    public partial class SysEmailService : BaseService<SysEmail>, ISysEmailService
    {
        private DbSet<SysEmail> _SysEmailSet = null;
        public SysEmailService(DbContext dbContext)
           : base(dbContext)
        {
            _SysEmailSet = base.Db.Set<SysEmail>();
        }

        public SysEmailService() { }
    }



    public partial class ContDescriptionService : BaseService<ContDescription>, IContDescriptionService
    {
        private DbSet<ContDescription> _ContDescriptionSet = null;
        public ContDescriptionService(DbContext dbContext)
           : base(dbContext)
        {
            _ContDescriptionSet = base.Db.Set<ContDescription>();
        }

        public ContDescriptionService() { }
    }



    public partial class SysFieldService : BaseService<SysField>, ISysFieldService
    {
        private DbSet<SysField> _SysFieldSet = null;
        public SysFieldService(DbContext dbContext)
           : base(dbContext)
        {
            _SysFieldSet = base.Db.Set<SysField>();
        }

        public SysFieldService() { }
    }



    public partial class ContInvoiceService : BaseService<ContInvoice>, IContInvoiceService
    {
        private DbSet<ContInvoice> _ContInvoiceSet = null;
        public ContInvoiceService(DbContext dbContext)
           : base(dbContext)
        {
            _ContInvoiceSet = base.Db.Set<ContInvoice>();
        }

        public ContInvoiceService() { }
    }



    public partial class SysFunctionService : BaseService<SysFunction>, ISysFunctionService
    {
        private DbSet<SysFunction> _SysFunctionSet = null;
        public SysFunctionService(DbContext dbContext)
           : base(dbContext)
        {
            _SysFunctionSet = base.Db.Set<SysFunction>();
        }

        public SysFunctionService() { }
    }



    public partial class ContPlanFinanceService : BaseService<ContPlanFinance>, IContPlanFinanceService
    {
        private DbSet<ContPlanFinance> _ContPlanFinanceSet = null;
        public ContPlanFinanceService(DbContext dbContext)
           : base(dbContext)
        {
            _ContPlanFinanceSet = base.Db.Set<ContPlanFinance>();
        }

        public ContPlanFinanceService() { }
    }



    public partial class SysModelService : BaseService<SysModel>, ISysModelService
    {
        private DbSet<SysModel> _SysModelSet = null;
        public SysModelService(DbContext dbContext)
           : base(dbContext)
        {
            _SysModelSet = base.Db.Set<SysModel>();
        }

        public SysModelService() { }
    }



    public partial class TempNodeAreaService : BaseService<TempNodeArea>, ITempNodeAreaService
    {
        private DbSet<TempNodeArea> _TempNodeAreaSet = null;
        public TempNodeAreaService(DbContext dbContext)
           : base(dbContext)
        {
            _TempNodeAreaSet = base.Db.Set<TempNodeArea>();
        }

        public TempNodeAreaService() { }
    }



    public partial class ContractInfoService : BaseService<ContractInfo>, IContractInfoService
    {
        private DbSet<ContractInfo> _ContractInfoSet = null;
        public ContractInfoService(DbContext dbContext)
           : base(dbContext)
        {
            _ContractInfoSet = base.Db.Set<ContractInfo>();
        }

        public ContractInfoService() { }
    }



    public partial class TempNodeAreaHistService : BaseService<TempNodeAreaHist>, ITempNodeAreaHistService
    {
        private DbSet<TempNodeAreaHist> _TempNodeAreaHistSet = null;
        public TempNodeAreaHistService(DbContext dbContext)
           : base(dbContext)
        {
            _TempNodeAreaHistSet = base.Db.Set<TempNodeAreaHist>();
        }

        public TempNodeAreaHistService() { }
    }



    public partial class ContractInfoHistoryService : BaseService<ContractInfoHistory>, IContractInfoHistoryService
    {
        private DbSet<ContractInfoHistory> _ContractInfoHistorySet = null;
        public ContractInfoHistoryService(DbContext dbContext)
           : base(dbContext)
        {
            _ContractInfoHistorySet = base.Db.Set<ContractInfoHistory>();
        }

        public ContractInfoHistoryService() { }
    }



    public partial class TempNodeLineService : BaseService<TempNodeLine>, ITempNodeLineService
    {
        private DbSet<TempNodeLine> _TempNodeLineSet = null;
        public TempNodeLineService(DbContext dbContext)
           : base(dbContext)
        {
            _TempNodeLineSet = base.Db.Set<TempNodeLine>();
        }

        public TempNodeLineService() { }
    }



    public partial class ContStatisticService : BaseService<ContStatistic>, IContStatisticService
    {
        private DbSet<ContStatistic> _ContStatisticSet = null;
        public ContStatisticService(DbContext dbContext)
           : base(dbContext)
        {
            _ContStatisticSet = base.Db.Set<ContStatistic>();
        }

        public ContStatisticService() { }
    }



    public partial class TempNodeLineHistService : BaseService<TempNodeLineHist>, ITempNodeLineHistService
    {
        private DbSet<TempNodeLineHist> _TempNodeLineHistSet = null;
        public TempNodeLineHistService(DbContext dbContext)
           : base(dbContext)
        {
            _TempNodeLineHistSet = base.Db.Set<TempNodeLineHist>();
        }

        public TempNodeLineHistService() { }
    }



    public partial class ContSubDeService : BaseService<ContSubDe>, IContSubDeService
    {
        private DbSet<ContSubDe> _ContSubDeSet = null;
        public ContSubDeService(DbContext dbContext)
           : base(dbContext)
        {
            _ContSubDeSet = base.Db.Set<ContSubDe>();
        }

        public ContSubDeService() { }
    }



    public partial class TenderAttachmentService : BaseService<TenderAttachment>, ITenderAttachmentService
    {
        private DbSet<TenderAttachment> _TenderAttachmentSet = null;
        public TenderAttachmentService(DbContext dbContext)
           : base(dbContext)
        {
            _TenderAttachmentSet = base.Db.Set<TenderAttachment>();
        }

        public TenderAttachmentService() { }
    }



    public partial class ContSubDeliveryService : BaseService<ContSubDelivery>, IContSubDeliveryService
    {
        private DbSet<ContSubDelivery> _ContSubDeliverySet = null;
        public ContSubDeliveryService(DbContext dbContext)
           : base(dbContext)
        {
            _ContSubDeliverySet = base.Db.Set<ContSubDelivery>();
        }

        public ContSubDeliveryService() { }
    }



    public partial class TendererNameLabelService : BaseService<TendererNameLabel>, ITendererNameLabelService
    {
        private DbSet<TendererNameLabel> _TendererNameLabelSet = null;
        public TendererNameLabelService(DbContext dbContext)
           : base(dbContext)
        {
            _TendererNameLabelSet = base.Db.Set<TendererNameLabel>();
        }

        public TendererNameLabelService() { }
    }



    public partial class ContSubjectMatterService : BaseService<ContSubjectMatter>, IContSubjectMatterService
    {
        private DbSet<ContSubjectMatter> _ContSubjectMatterSet = null;
        public ContSubjectMatterService(DbContext dbContext)
           : base(dbContext)
        {
            _ContSubjectMatterSet = base.Db.Set<ContSubjectMatter>();
        }

        public ContSubjectMatterService() { }
    }



    public partial class TenderInforService : BaseService<TenderInfor>, ITenderInforService
    {
        private DbSet<TenderInfor> _TenderInforSet = null;
        public TenderInforService(DbContext dbContext)
           : base(dbContext)
        {
            _TenderInforSet = base.Db.Set<TenderInfor>();
        }

        public TenderInforService() { }
    }



    public partial class ContSubjectMatterHistoryService : BaseService<ContSubjectMatterHistory>, IContSubjectMatterHistoryService
    {
        private DbSet<ContSubjectMatterHistory> _ContSubjectMatterHistorySet = null;
        public ContSubjectMatterHistoryService(DbContext dbContext)
           : base(dbContext)
        {
            _ContSubjectMatterHistorySet = base.Db.Set<ContSubjectMatterHistory>();
        }

        public ContSubjectMatterHistoryService() { }
    }



    public partial class TheWinningUnitService : BaseService<TheWinningUnit>, ITheWinningUnitService
    {
        private DbSet<TheWinningUnit> _TheWinningUnitSet = null;
        public TheWinningUnitService(DbContext dbContext)
           : base(dbContext)
        {
            _TheWinningUnitSet = base.Db.Set<TheWinningUnit>();
        }

        public TheWinningUnitService() { }
    }



    public partial class ContTextService : BaseService<ContText>, IContTextService
    {
        private DbSet<ContText> _ContTextSet = null;
        public ContTextService(DbContext dbContext)
           : base(dbContext)
        {
            _ContTextSet = base.Db.Set<ContText>();
        }

        public ContTextService() { }
    }



    public partial class UserInforService : BaseService<UserInfor>, IUserInforService
    {
        private DbSet<UserInfor> _UserInforSet = null;
        public UserInforService(DbContext dbContext)
           : base(dbContext)
        {
            _UserInforSet = base.Db.Set<UserInfor>();
        }

        public UserInforService() { }
    }



    public partial class ContTextArchiveService : BaseService<ContTextArchive>, IContTextArchiveService
    {
        private DbSet<ContTextArchive> _ContTextArchiveSet = null;
        public ContTextArchiveService(DbContext dbContext)
           : base(dbContext)
        {
            _ContTextArchiveSet = base.Db.Set<ContTextArchive>();
        }

        public ContTextArchiveService() { }
    }



    public partial class UserModuleService : BaseService<UserModule>, IUserModuleService
    {
        private DbSet<UserModule> _UserModuleSet = null;
        public UserModuleService(DbContext dbContext)
           : base(dbContext)
        {
            _UserModuleSet = base.Db.Set<UserModule>();
        }

        public UserModuleService() { }
    }



    public partial class ContTextArchiveItemService : BaseService<ContTextArchiveItem>, IContTextArchiveItemService
    {
        private DbSet<ContTextArchiveItem> _ContTextArchiveItemSet = null;
        public ContTextArchiveItemService(DbContext dbContext)
           : base(dbContext)
        {
            _ContTextArchiveItemSet = base.Db.Set<ContTextArchiveItem>();
        }

        public ContTextArchiveItemService() { }
    }



    public partial class UserPermissionService : BaseService<UserPermission>, IUserPermissionService
    {
        private DbSet<UserPermission> _UserPermissionSet = null;
        public UserPermissionService(DbContext dbContext)
           : base(dbContext)
        {
            _UserPermissionSet = base.Db.Set<UserPermission>();
        }

        public UserPermissionService() { }
    }



    public partial class ContTextBorrowService : BaseService<ContTextBorrow>, IContTextBorrowService
    {
        private DbSet<ContTextBorrow> _ContTextBorrowSet = null;
        public ContTextBorrowService(DbContext dbContext)
           : base(dbContext)
        {
            _ContTextBorrowSet = base.Db.Set<ContTextBorrow>();
        }

        public ContTextBorrowService() { }
    }



    public partial class UserRoleService : BaseService<UserRole>, IUserRoleService
    {
        private DbSet<UserRole> _UserRoleSet = null;
        public UserRoleService(DbContext dbContext)
           : base(dbContext)
        {
            _UserRoleSet = base.Db.Set<UserRole>();
        }

        public UserRoleService() { }
    }



    public partial class ContTextHistoryService : BaseService<ContTextHistory>, IContTextHistoryService
    {
        private DbSet<ContTextHistory> _ContTextHistorySet = null;
        public ContTextHistoryService(DbContext dbContext)
           : base(dbContext)
        {
            _ContTextHistorySet = base.Db.Set<ContTextHistory>();
        }

        public ContTextHistoryService() { }
    }



    public partial class WinningInqService : BaseService<WinningInq>, IWinningInqService
    {
        private DbSet<WinningInq> _WinningInqSet = null;
        public WinningInqService(DbContext dbContext)
           : base(dbContext)
        {
            _WinningInqSet = base.Db.Set<WinningInq>();
        }

        public WinningInqService() { }
    }



    public partial class ContTextSealService : BaseService<ContTextSeal>, IContTextSealService
    {
        private DbSet<ContTextSeal> _ContTextSealSet = null;
        public ContTextSealService(DbContext dbContext)
           : base(dbContext)
        {
            _ContTextSealSet = base.Db.Set<ContTextSeal>();
        }

        public ContTextSealService() { }
    }



    public partial class WinningItemService : BaseService<WinningItem>, IWinningItemService
    {
        private DbSet<WinningItem> _WinningItemSet = null;
        public WinningItemService(DbContext dbContext)
           : base(dbContext)
        {
            _WinningItemSet = base.Db.Set<WinningItem>();
        }

        public WinningItemService() { }
    }



    public partial class ContTxtTempAndSubFieldService : BaseService<ContTxtTempAndSubField>, IContTxtTempAndSubFieldService
    {
        private DbSet<ContTxtTempAndSubField> _ContTxtTempAndSubFieldSet = null;
        public ContTxtTempAndSubFieldService(DbContext dbContext)
           : base(dbContext)
        {
            _ContTxtTempAndSubFieldSet = base.Db.Set<ContTxtTempAndSubField>();
        }

        public ContTxtTempAndSubFieldService() { }
    }



    public partial class WinningQueService : BaseService<WinningQue>, IWinningQueService
    {
        private DbSet<WinningQue> _WinningQueSet = null;
        public WinningQueService(DbContext dbContext)
           : base(dbContext)
        {
            _WinningQueSet = base.Db.Set<WinningQue>();
        }

        public WinningQueService() { }
    }



    public partial class ContTxtTempAndVarStoreRelaService : BaseService<ContTxtTempAndVarStoreRela>, IContTxtTempAndVarStoreRelaService
    {
        private DbSet<ContTxtTempAndVarStoreRela> _ContTxtTempAndVarStoreRelaSet = null;
        public ContTxtTempAndVarStoreRelaService(DbContext dbContext)
           : base(dbContext)
        {
            _ContTxtTempAndVarStoreRelaSet = base.Db.Set<ContTxtTempAndVarStoreRela>();
        }

        public ContTxtTempAndVarStoreRelaService() { }
    }



    public partial class ContTxtTemplateService : BaseService<ContTxtTemplate>, IContTxtTemplateService
    {
        private DbSet<ContTxtTemplate> _ContTxtTemplateSet = null;
        public ContTxtTemplateService(DbContext dbContext)
           : base(dbContext)
        {
            _ContTxtTemplateSet = base.Db.Set<ContTxtTemplate>();
        }

        public ContTxtTemplateService() { }
    }



    public partial class ContTxtTemplateHistService : BaseService<ContTxtTemplateHist>, IContTxtTemplateHistService
    {
        private DbSet<ContTxtTemplateHist> _ContTxtTemplateHistSet = null;
        public ContTxtTemplateHistService(DbContext dbContext)
           : base(dbContext)
        {
            _ContTxtTemplateHistSet = base.Db.Set<ContTxtTemplateHist>();
        }

        public ContTxtTemplateHistService() { }
    }



    public partial class ContTxtTempVarStoreService : BaseService<ContTxtTempVarStore>, IContTxtTempVarStoreService
    {
        private DbSet<ContTxtTempVarStore> _ContTxtTempVarStoreSet = null;
        public ContTxtTempVarStoreService(DbContext dbContext)
           : base(dbContext)
        {
            _ContTxtTempVarStoreSet = base.Db.Set<ContTxtTempVarStore>();
        }

        public ContTxtTempVarStoreService() { }
    }



    public partial class CountryService : BaseService<Country>, ICountryService
    {
        private DbSet<Country> _CountrySet = null;
        public CountryService(DbContext dbContext)
           : base(dbContext)
        {
            _CountrySet = base.Db.Set<Country>();
        }

        public CountryService() { }
    }



    public partial class CurrencyManagerService : BaseService<CurrencyManager>, ICurrencyManagerService
    {
        private DbSet<CurrencyManager> _CurrencyManagerSet = null;
        public CurrencyManagerService(DbContext dbContext)
           : base(dbContext)
        {
            _CurrencyManagerSet = base.Db.Set<CurrencyManager>();
        }

        public CurrencyManagerService() { }
    }



    public partial class DataDictionaryService : BaseService<DataDictionary>, IDataDictionaryService
    {
        private DbSet<DataDictionary> _DataDictionarySet = null;
        public DataDictionaryService(DbContext dbContext)
           : base(dbContext)
        {
            _DataDictionarySet = base.Db.Set<DataDictionary>();
        }

        public DataDictionaryService() { }
    }



    public partial class DepartmentService : BaseService<Department>, IDepartmentService
    {
        private DbSet<Department> _DepartmentSet = null;
        public DepartmentService(DbContext dbContext)
           : base(dbContext)
        {
            _DepartmentSet = base.Db.Set<Department>();
        }

        public DepartmentService() { }
    }



    public partial class DeptMainService : BaseService<DeptMain>, IDeptMainService
    {
        private DbSet<DeptMain> _DeptMainSet = null;
        public DeptMainService(DbContext dbContext)
           : base(dbContext)
        {
            _DeptMainSet = base.Db.Set<DeptMain>();
        }

        public DeptMainService() { }
    }



    public partial class DisputeManagerService : BaseService<DisputeManager>, IDisputeManagerService
    {
        private DbSet<DisputeManager> _DisputeManagerSet = null;
        public DisputeManagerService(DbContext dbContext)
           : base(dbContext)
        {
            _DisputeManagerSet = base.Db.Set<DisputeManager>();
        }

        public DisputeManagerService() { }
    }



}

