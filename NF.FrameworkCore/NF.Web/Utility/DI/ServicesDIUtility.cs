using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NF.BLL;
using NF.Common.Extend;
using NF.Common.Models;
using NF.IBLL;
using NF.IBLL.MobileApp.CONTRACT;
using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NF.Web.Utility.DI
{
    /// <summary>
    /// 微软自带依赖注入
    /// </summary>
    public partial class ServicesDIUtility
    {
        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void ServicesDI(IServiceCollection services)
        {
            #region 系统管理及基础数据
            //权限处理类
            services.AddTransient<ISysPermissionModelService, SysPermissionModelService>();
            //用户
            services.AddTransient<IUserInforService, UserInforService>();
            //字典
            services.AddTransient<IDataDictionaryService,DataDictionaryService>();
            //部门
            services.AddTransient<IDepartmentService, DepartmentService>();
            //签约主体信息
            services.AddTransient<IDeptMainService, DeptMainService>();
            
            //登录日志
            services.AddTransient<ILoginLogService, LoginLogService>();
            //操作日志
            services.AddTransient<IOptionLogService, OptionLogService>();
            //角色
            services.AddTransient<IRoleService, RoleService>();
            //用户-角色
            services.AddTransient<IUserRoleService, UserRoleService>();
            //模块名称
            services.AddTransient<ISysModelService, SysModelService>();
            //角色菜单分配
            services.AddTransient<IRoleModuleService, RoleModuleService>();
            //用户菜单分配
            services.AddTransient<IUserModuleService, UserModuleService>();
            //功能点
            services.AddTransient<ISysFunctionService,SysFunctionService>();
            //角色功能权限分配
            services.AddTransient<IRolePermissionService, RolePermissionService>();
            //用户功能权限分配
            services.AddTransient<IUserPermissionService, UserPermissionService>();
            //市
            services.AddTransient<ICityService, CityService>();
            // 省
            services.AddTransient<IProvinceService, ProvinceService>();
            //国家
            services.AddTransient<ICountryService, CountryService>();
            //币种管理
            services.AddTransient<ICurrencyManagerService, CurrencyManagerService>();
            //国家
            services.AddTransient<ICountryService, CountryService>();
            //印章
            services.AddTransient<ISealManagerService, SealManagerService>();
            //邮箱
            services.AddTransient<ISysEmailService, SysEmailService>();
            //提醒
            services.AddTransient<IRemindService, RemindService>();
            
            
            #endregion

            #region 合同对方
            //合同对方
            services.AddTransient<ICompanyService, CompanyService>();
            //合同对方-其他联系人
            services.AddTransient<ICompContactService, CompContactService>();
            //合同对方-备忘录
            services.AddTransient<ICompDescriptionService, CompDescriptionService>();
            //合同对方-附件
            services.AddTransient<ICompAttachmentService, CompAttachmentService>();
            #endregion

            #region 项目模块
            //项目
            services.AddTransient<IProjectManagerService, ProjectManagerService>();
            //项目-附件
            services.AddTransient<IProjAttachmentService, ProjAttachmentService>();
            //项目-说明
            services.AddTransient<IProjDescriptionService, ProjDescriptionService>();
            //项目-时间
            services.AddTransient<IProjScheduleService, ProjScheduleService>();
            #endregion

            #region 合同相关
            //合同信息
            services.AddTransient<IContractInfoService, ContractInfoService>();
            //标的
            services.AddTransient<IContSubjectMatterService, ContSubjectMatterService>();
            //合同备忘
            services.AddTransient<IContDescriptionService, ContDescriptionService>();
            //合同附件
            services.AddTransient<IContAttachmentService, ContAttachmentService>();
            //合同查阅人
            services.AddTransient<IContConsultService, ContConsultService>();
            //合同历史
            services.AddTransient<IContractInfoHistoryService, ContractInfoHistoryService>();
            //标的历史
            services.AddTransient<IContSubjectMatterHistoryService, ContSubjectMatterHistoryService>();
            //合同统计字段
            services.AddTransient<IContStatisticService, ContStatisticService>();
            //自动生成编号
            services.AddTransient<INoHipler, NoHipler>();

            #endregion

            #region 资金
            //计划资金
            services.AddTransient<IContPlanFinanceService, ContPlanFinanceService>();
            //计划资金历史
            services.AddTransient<IContPlanFinanceHistoryService, ContPlanFinanceHistoryService>();
            //发票
            services.AddTransient<IContInvoiceService, ContInvoiceService>();
            //发票明细
            services.AddTransient<IInvoDescriptionService, InvoDescriptionService>();
            //实际资金
            services.AddTransient<IContActualFinanceService, ContActualFinanceService>();
            //实际资金附件
            services.AddTransient<IActFinceFileService, ActFinceFileService>();
            //发票附件
            services.AddTransient<IInvoFileService, InvoFileService>();



            #endregion

            #region 文本
            //合同文本
            services.AddTransient<IContTextService, ContTextService>();
            //文本历史
            services.AddTransient<IContTextHistoryService, ContTextHistoryService>();
            //盖章
            services.AddTransient<IContTextSealService, ContTextSealService>();
            //归档
            services.AddTransient<IContTextArchiveService, ContTextArchiveService>();
            //归档明细
            services.AddTransient<IContTextArchiveItemService, ContTextArchiveItemService>();
            //合同文本借阅
            services.AddTransient<IContTextBorrowService, ContTextBorrowService>();

            #endregion

            #region 合同模板
            //合同模板
            services.AddTransient<IContTxtTemplateService, ContTxtTemplateService>();
            services.AddTransient<IContTxtTemplateHistService, ContTxtTemplateHistService>();
            //模板变量
            services.AddTransient<IContTxtTempVarStoreService, ContTxtTempVarStoreService>();
            //模板变量映射关系
            services.AddTransient<IContTxtTempAndVarStoreRelaService, ContTxtTempAndVarStoreRelaService>();
            //合同模板标的字段
            services.AddTransient<IContTxtTempAndSubFieldService, ContTxtTempAndSubFieldService>();
            
            #endregion

            #region 流程
            //组
            services.AddTransient<IGroupInfoService, GroupInfoService>();
            //组-用户
            services.AddTransient<IGroupUserService, GroupUserService>();
            //流程模板
            services.AddTransient<IFlowTempService, FlowTempService>();
            //流程节点
            services.AddTransient<IFlowTempNodeService, FlowTempNodeService>();
            //流程节点信息
            services.AddTransient<IFlowTempNodeInfoService, FlowTempNodeInfoService>();
            //流程实例
            services.AddTransient<IAppInstService, AppInstService>();
            //流程实例节点
            services.AddTransient<IAppInstNodeService, AppInstNodeService>();
            //意见表
            services.AddTransient<IAppInstOpinService, AppInstOpinService>();
            //审批意见
            services.AddTransient<IFlowPdfService, FlowPdfService>();

            #endregion
            #region 单品管理
            //业务品类
            services.AddTransient<IBusinessCategoryService, BusinessCategoryService>();
            //单品管理
            services.AddTransient<IBcInstanceService, BcInstanceService>();
            //单品附件
            services.AddTransient<IBcAttachmentService, BcAttachmentService>();

            #endregion

            #region 标的交付
            //标的交付描述
            services.AddTransient<IContSubDeService, ContSubDeService>();
            #endregion

            #region 统计
            //统计图
            services.AddTransient<IChartService, ChartService>();

            #endregion

            #region APP端口
            //APP用户登录
            services.AddTransient<IAPPUserInforService, APPUserInforService>();
            //APP合同查看
            services.AddTransient<IAAPPContractList, APPContractList>();
            //客户/供应商
            services.AddTransient<IAPPSompanylist, APPSompanylist>();
            //计划资金
            services.AddTransient<IAPPfinance_plan, finance_plan>();
            //实际资金
            services.AddTransient<IFinance_actual, APPFinance_actualList>();
            //APP审批
            services.AddTransient<IAPPWOrkList, APPWOrkList>();
            //APP开票
            services.AddTransient<IAPPInvoice, APPInvoice>();
            #endregion

            #region 招标
            
            services.AddTransient<ITenderInforService, TenderInforService>();
            services.AddTransient<ITendererNameLabelService, TendererNameLabelService>();
            services.AddTransient<ITenderAttachmentService, TenderAttachmentService>();
            services.AddTransient<ISuccessfulBidderLableService, SuccessfulBidderLableService>();
            services.AddTransient<IOpeningSituationInforService, OpeningSituationInforService>();
            //招标
            services.AddTransient<IInquiryService, InquiryService>();
            services.AddTransient<IWinningItemService, WinningItemService>();
            //询价
            services.AddTransient<IWinningInqService, WinningInqService>(); 
            services.AddTransient<IInquiryAttachmentService, InquiryAttachmentService>();//询价附件
            services.AddTransient<IOpenTenderConditionService, OpenTenderConditionService>();
            services.AddTransient<IInquirerService, InquirerService>();
            services.AddTransient<ITheWinningUnitService, TheWinningUnitService>();
            //约谈
            services.AddTransient<IWinningQueService, WinningQueService>();
            services.AddTransient<IQuestioningService, QuestioningService>();
            services.AddTransient<IOpenBidService, OpenBidService>();//中标
            services.AddTransient<IInterviewpeopleService, InterviewpeopleService>();//人
            services.AddTransient<IBidlabelService, BidlabelService>();//
            services.AddTransient<IQuestioningAttachmentService, QuestioningAttachmentService>();//附件
            #endregion
            #region 进度管理
            services.AddTransient<IScheduleManagementService, ScheduleManagementService>();
            services.AddTransient<IScheduleListService, ScheduleListService>();
            #endregion

            #region 进度
            //进度管理
            services.AddTransient<IScheduleManagementService, ScheduleManagementService>();
            services.AddTransient<IScheduleListService, ScheduleListService>();
            services.AddTransient<IScheduleManagementAttachmentService, ScheduleManagementAttachmentService>();
            //进度明细
            services.AddTransient<IScheduleDetailService, ScheduleDetailService>();
            services.AddTransient<IScheduleDetailAttachmentService, ScheduleDetailAttachmentService>();
            #endregion

            #region 系统新增字段
            services.AddTransient<ISysFieldService, SysFieldService>();

            #endregion
            #region 纠纷案件管理
            services.AddTransient<IDisputeManagerService, DisputeManagerService>();
            services.AddTransient<ICaseManagerService, CaseManagerService>();
            services.AddTransient<IAjglAttachmentService, AjglAttachmentService>();

            services.AddTransient<IAjglSpwsAttachmentService, AjglSpwsAttachmentService>();
            services.AddTransient<IAjglSsBqAttachmentService, AjglSsBqAttachmentService>();

            #endregion
        }
        /// <summary>
        /// 手动获取
        /// </summary>
        /// <typeparam name="I">接口T</typeparam>
        /// <typeparam name="S">实现类服务</typeparam>
        /// <typeparam name="M">对应实体(执行表)</typeparam>
        /// <returns></returns>
        public static I GetService<I, S, M>()
           where I : class, IBaseService<M>
           where S : class, I, new()
           where M : class, new()
        {
            var services = new ServiceCollection();
            var connection = ConfigurationManager.GetAppSettings<ConnectionStrings>("ConnectionStrings");
            services.AddDbContext<NFDbContext>(options => options.UseSqlServer(connection.SqlConnection));
            services.AddScoped<NFDbContext>(p => p.GetService<NFDbContext>());
            services.AddTransient<I, S>();
            var provider = services.BuildServiceProvider();
            I optionlogService = provider.GetService<I>();
            return optionlogService;
        }
    }
}
