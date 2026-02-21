using AutoMapper;
using NF.Model.Models;
using NF.ViewModel;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Schedule;

namespace NF.AutoMapper
{
    /// <summary>
    /// AutoMapper映射配置文件
    /// </summary>
    public partial class NfProfile : Profile, IProfile
    {
        public NfProfile()
        {
            #region 流程模板
            //合同标的
            CreateMap<FlowTempDTO, FlowTemp>()

           .ForMember(a => a.CreateUserId, opt => opt.Ignore())
           .ForMember(a => a.CreateDateTime, opt => opt.Ignore())

             .ForMember(a => a.IsDelete, opt => opt.Ignore());
            //流程模板->流程模板历史
            CreateMap<FlowTemp, FlowTempHist>()
                    .ForMember(a => a.Id, opt => opt.Ignore());

            #endregion
            #region 系统及基础数据

            CreateMap<UserInfor, UserInfoDTO>();
            CreateMap<UserInfoDTO, UserInfor>();
            CreateMap<UserInfor, SessionUserInfo>();
            //存储Redis
            CreateMap<UserInfor, RedisUser>();

            CreateMap<LoginLog, LoginLogDTO>().ForMember(dest => dest.LoginUserName, mo => mo.MapFrom(src => src.LoginUser.Name)).
                ForMember(dest => dest.ResultDic, mo => mo.Ignore());
            //部门
            CreateMap<Department, RedisDept>();
            //数据字典
            CreateMap<DataDictionary, DataDictionaryDTO>()
                     .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
                     .ForMember(a => a.CreateUserId, opt => opt.Ignore())
                     .ForMember(a => a.CreateDatetime, opt => opt.Ignore())
                     .ForMember(a => a.ModifyDatetime, opt => opt.Ignore());
            CreateMap<DataDictionaryDTO, DataDictionary>()
                      .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
                     .ForMember(a => a.CreateUserId, opt => opt.Ignore())
                     .ForMember(a => a.CreateDatetime, opt => opt.Ignore())
                     .ForMember(a => a.ModifyDatetime, opt => opt.Ignore());
            //币种
            CreateMap<CurrencyManagerDTO, CurrencyManager>()
                 .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
                 .ForMember(a => a.IsDelete, opt => opt.Ignore())
                 .ForMember(a => a.CreateUserId, opt => opt.Ignore())
                 .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
                 .ForMember(a => a.ModifyDateTime, opt => opt.Ignore());
            CreateMap<CurrencyManagerDTO, RedisCurrency>();
            //印章
            CreateMap<SealManagerDTO, SealManager>()
                 .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
                 .ForMember(a => a.IsDelete, opt => opt.Ignore())
                 .ForMember(a => a.CreateUserId, opt => opt.Ignore())
                 .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
                 .ForMember(a => a.EnabledDate, opt => opt.Ignore())
                 .ForMember(a => a.SealState, opt => opt.Ignore())
                 .ForMember(a => a.ModifyDateTime, opt => opt.Ignore());
            #endregion

            #region 合同对方
            //合同对方
            CreateMap<CompanyDTO, Company>()
                     .ForMember(a => a.Caredit, opt => opt.Ignore())
                     .ForMember(a => a.CompType, opt => opt.Ignore())
                     .ForMember(a => a.CompClass, opt => opt.Ignore())
                     .ForMember(a => a.CreateUser, opt => opt.Ignore())
                     .ForMember(a => a.Level, opt => opt.Ignore())
                     .ForMember(a => a.ModifyUser, opt => opt.Ignore())
                     .ForMember(a => a.CreateUserId, opt => opt.Ignore())
                     .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
                     .ForMember(a => a.Cstate, opt => opt.Ignore())
                     .ForMember(a => a.IsDelete, opt => opt.Ignore())
                     .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
                     .ForMember(a => a.ModifyDateTime, opt => opt.Ignore())
                     .ForMember(a => a.WfState, opt => opt.Ignore())
                     .ForMember(a => a.WfItem, opt => opt.Ignore())
                     .ForMember(a => a.WfCurrNodeName, opt => opt.Ignore())
                     .ForMember(a => a.Caredit, opt => opt.Ignore())
                     .ForMember(a => a.CompClass, opt => opt.Ignore())
                     .ForMember(a => a.CompType, opt => opt.Ignore())
                     .ForMember(a => a.CreateUser, opt => opt.Ignore())
                     .ForMember(a => a.ModifyUser, opt => opt.Ignore())
                     .ForMember(a => a.PrincipalUser, opt => opt.Ignore())
                     .ForMember(a => a.ContractInfoComps, opt => opt.Ignore())
                     .ForMember(a => a.ContractInfoCompId3Navigations, opt => opt.Ignore())
                     .ForMember(a => a.ContractInfoCompId4Navigations, opt => opt.Ignore())
                     .ForMember(a => a.ContractInfoHistoryComps, opt => opt.Ignore())
                     .ForMember(a => a.ContractInfoHistoryCompId3Navigations, opt => opt.Ignore())
                     .ForMember(a => a.ContractInfoHistoryCompId4Navigations, opt => opt.Ignore())
                     .ForMember(a => a.PrincipalUser, opt => opt.Ignore()
                   );
            //验证config
            //Mapper.AssertConfigurationIsValid();
            //合同对方-联系人
            CreateMap<CompContactDTO, CompContact>()
                .ForMember(a => a.CompanyId, opt => opt.Ignore())
                .ForMember(a => a.ModifyUser, opt => opt.Ignore())
                .ForMember(a => a.CreateUser, opt => opt.Ignore())
                .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
                .ForMember(a => a.IsDelete, opt => opt.Ignore())
                .ForMember(a => a.CreateUserId, opt => opt.Ignore())
                .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
                .ForMember(a => a.ModifyDateTime, opt => opt.Ignore());
            //合同对方-备忘录
            CreateMap<CompDescriptionDTO, CompDescription>()
               .ForMember(a => a.CompanyId, opt => opt.Ignore())
               .ForMember(a => a.ModifyUser, opt => opt.Ignore())
               .ForMember(a => a.CreateUser, opt => opt.Ignore())
               .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
               .ForMember(a => a.IsDelete, opt => opt.Ignore())
               .ForMember(a => a.CreateUserId, opt => opt.Ignore())
               .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
               .ForMember(a => a.ModifyDateTime, opt => opt.Ignore());
            //合同对方-附件
            CreateMap<CompAttachmentDTO, CompAttachment>()
                .ForMember(a => a.Category, opt => opt.Ignore())
                .ForMember(a => a.Path, opt => opt.Ignore())
                 .ForMember(a => a.CompanyId, opt => opt.Ignore())
                 .ForMember(a => a.CreateUser, opt => opt.Ignore())
                 .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
                 .ForMember(a => a.IsDelete, opt => opt.Ignore())
                 .ForMember(a => a.CreateUserId, opt => opt.Ignore())
                 .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
                 .ForMember(a => a.ModifyDateTime, opt => opt.Ignore());
            #endregion

            #region 项目
            //项目
            CreateMap<ProjectManagerDTO, ProjectManager>()
                .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
                .ForMember(a => a.IsDelete, opt => opt.Ignore())
                .ForMember(a => a.CreateUserId, opt => opt.Ignore())
                .ForMember(a => a.CreateUser, opt => opt.Ignore())
                .ForMember(a => a.ModifyUser, opt => opt.Ignore())
                .ForMember(a => a.Category, opt => opt.Ignore())
                .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
                .ForMember(a => a.Pstate, opt => opt.Ignore())
                .ForMember(a => a.PrincipalUser, opt => opt.Ignore())
                .ForMember(a => a.BudgetCollectCurrency, opt => opt.Ignore())
                .ForMember(a => a.BudgetPayCurrency, opt => opt.Ignore())

                .ForMember(a => a.ModifyDateTime, opt => opt.Ignore());
            //项目附件
            CreateMap<ProjAttachmentDTO, ProjAttachment>()
            .ForMember(a => a.Category, opt => opt.Ignore())
            .ForMember(a => a.Path, opt => opt.Ignore())
             .ForMember(a => a.ProjectId, opt => opt.Ignore())
             .ForMember(a => a.CreateUser, opt => opt.Ignore())
             .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
             .ForMember(a => a.IsDelete, opt => opt.Ignore())
             .ForMember(a => a.CreateUserId, opt => opt.Ignore())
             .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
             .ForMember(a => a.ModifyDateTime, opt => opt.Ignore());
            //项目说明
            CreateMap<ProjDescriptionDTO, ProjDescription>()
            .ForMember(a => a.ProjectId, opt => opt.Ignore())
            .ForMember(a => a.CreateUser, opt => opt.Ignore())
            .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
            .ForMember(a => a.IsDelete, opt => opt.Ignore())
            .ForMember(a => a.CreateUserId, opt => opt.Ignore())
            .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
            .ForMember(a => a.ModifyDateTime, opt => opt.Ignore());
            //项目时间列表
            CreateMap<ProjScheduleDTO, ProjSchedule>()
            .ForMember(a => a.ProjectId, opt => opt.Ignore())
            .ForMember(a => a.CreateUser, opt => opt.Ignore())
            .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
            .ForMember(a => a.IsDelete, opt => opt.Ignore())
            .ForMember(a => a.CreateUserId, opt => opt.Ignore())
            .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
            .ForMember(a => a.ModifyDateTime, opt => opt.Ignore());
            #endregion

            #region 合同

            //合同
            CreateMap<ContractInfoDTO, ContractInfo>()
            .ForMember(a => a.CreateUser, opt => opt.Ignore())
            .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
            .ForMember(a => a.IsDelete, opt => opt.Ignore())
            .ForMember(a => a.ContState, opt => opt.Ignore())
            .ForMember(a => a.CreateUserId, opt => opt.Ignore())
            .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
            .ForMember(a => a.ContHid, opt => opt.Ignore())
            .ForMember(a => a.ModifyDateTime, opt => opt.Ignore());
            //合同->合同历史
            CreateMap<ContractInfo, ContractInfoHistory>()
                .ForMember(a => a.Id, opt => opt.Ignore());

            //合同备忘
            CreateMap<ContDescriptionDTO, ContDescription>()
            .ForMember(a => a.ContId, opt => opt.Ignore())
            .ForMember(a => a.CreateUserId, opt => opt.Ignore())
            .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
            .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
            .ForMember(a => a.ModifyDateTime, opt => opt.Ignore());
            //合同附件
            CreateMap<ContAttachmentDTO, ContAttachment>()
             .ForMember(a => a.Path, opt => opt.Ignore())
             .ForMember(a => a.ContId, opt => opt.Ignore())
             .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
             .ForMember(a => a.IsDelete, opt => opt.Ignore())
             .ForMember(a => a.CreateUserId, opt => opt.Ignore())
             .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
             .ForMember(a => a.ModifyDateTime, opt => opt.Ignore());
            //合同标的
            CreateMap<ContSubjectMatterDTO, ContSubjectMatter>()
           .ForMember(a => a.ContId, opt => opt.Ignore())
           .ForMember(a => a.CreateUserId, opt => opt.Ignore())
           .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
           .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
             .ForMember(a => a.IsDelete, opt => opt.Ignore())
           .ForMember(a => a.SubState, opt => opt.Ignore())
           .ForMember(a => a.ModifyDateTime, opt => opt.Ignore());
            //合同标的-》合同标的历史
            CreateMap<ContSubjectMatter, ContSubjectMatterHistory>()
                .ForMember(a => a.Id, opt => opt.Ignore());
            #endregion
            #region 招标
            CreateMap<TenderInforDTO, TenderInfor>()
        .ForMember(a => a.CreateUserId, opt => opt.Ignore());
            //开标情况
            CreateMap<OpeningSituationInforDTO, OpeningSituationInfor>();
            //招标人
            CreateMap<TendererNameLabelDTO, TendererNameLabel>();
            //中标单位
            CreateMap<SuccessfulBidderLableDTO, SuccessfulBidderLable>();
            //招标附件
            CreateMap<TenderAttachmentDTO, TenderAttachment>()
            .ForMember(a => a.Path, opt => opt.Ignore())
             .ForMember(a => a.ContId, opt => opt.Ignore())
             .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
             .ForMember(a => a.IsDelete, opt => opt.Ignore())
             .ForMember(a => a.CreateUserId, opt => opt.Ignore())
             .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
             .ForMember(a => a.ModifyDateTime, opt => opt.Ignore());
            #endregion
            #region 资金
            //计划资金
            CreateMap<ContPlanFinanceDTO, ContPlanFinance>()
           .ForMember(a => a.ContId, opt => opt.Ignore())
           .ForMember(a => a.CreateUserId, opt => opt.Ignore())
           .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
           .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
           .ForMember(a => a.ModifyDateTime, opt => opt.Ignore());
            //计划资金-》计划资金历史
            CreateMap<ContPlanFinance, ContPlanFinanceHistory>()
                .ForMember(a => a.Id, opt => opt.Ignore());
            //发票
            CreateMap<ContInvoiceDTO, ContInvoice>()
          .ForMember(a => a.CreateUserId, opt => opt.Ignore())
          .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
          .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
          .ForMember(a => a.InState, opt => opt.Ignore())
          .ForMember(a => a.ModifyDateTime, opt => opt.Ignore());
            //实际资金
            CreateMap<ContActualFinanceDTO, ContActualFinance>()
          .ForMember(a => a.CreateUserId, opt => opt.Ignore())
          .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
          .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
          .ForMember(a => a.Astate, opt => opt.Ignore())
          .ForMember(a => a.IsDelete, opt => opt.Ignore())
          .ForMember(a => a.ModifyDateTime, opt => opt.Ignore());
            //资金附件
            CreateMap<ActFinceFileDTO, ActFinceFile>()
            //.ForMember(a => a.Category, opt => opt.Ignore())
            .ForMember(a => a.Path, opt => opt.Ignore())
             .ForMember(a => a.ActId, opt => opt.Ignore())
             //.ForMember(a => a.CreateUser, opt => opt.Ignore())
             .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
             .ForMember(a => a.IsDelete, opt => opt.Ignore())
             .ForMember(a => a.CreateUserId, opt => opt.Ignore())
             .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
             .ForMember(a => a.ModifyDateTime, opt => opt.Ignore());

            //发票附件
            CreateMap<InvoFileDTO, InvoFile>()
            //.ForMember(a => a.Category, opt => opt.Ignore())
            .ForMember(a => a.Path, opt => opt.Ignore())
             .ForMember(a => a.InvoId, opt => opt.Ignore())
             //.ForMember(a => a.CreateUser, opt => opt.Ignore())
             .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
             .ForMember(a => a.IsDelete, opt => opt.Ignore())
             .ForMember(a => a.CreateUserId, opt => opt.Ignore())
             .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
             .ForMember(a => a.ModifyDateTime, opt => opt.Ignore());

            #endregion

            #region 合同文本
            CreateMap<ContTextDTO, ContText>()
         .ForMember(a => a.ContId, opt => opt.Ignore())
         .ForMember(a => a.CreateUserId, opt => opt.Ignore())
         .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
         .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
         .ForMember(a => a.ModifyDateTime, opt => opt.Ignore())
         .ForMember(a => a.IsDelete, opt => opt.Ignore())
         .ForMember(a => a.Stage, opt => opt.Ignore())
         .ForMember(a => a.Versions, opt => opt.Ignore())
          .ForMember(a => a.ContHisId, opt => opt.Ignore());

            //合同文本->合同文本历史
            CreateMap<ContText, ContTextHistory>()
                .ForMember(a => a.Id, opt => opt.Ignore());
            //盖章
            CreateMap<ContTextSealDTO, ContTextSeal>()
        //.ForMember(a => a.ContTextId, opt => opt.Ignore())
        .ForMember(a => a.CreateUserId, opt => opt.Ignore())
        .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
        .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
        .ForMember(a => a.ModifyDateTime, opt => opt.Ignore())
        .ForMember(a => a.IsDelete, opt => opt.Ignore());
            #endregion

            #region 业务品类
            //业务品类设置
            CreateMap<BusinessCategoryDTO, BusinessCategory>()
          .ForMember(a => a.IsDelete, opt => opt.Ignore())
          .ForMember(a => a.SubCompId, opt => opt.Ignore());
            //单品管理
            CreateMap<BcInstanceDTO, BcInstance>()
         .ForMember(a => a.IsDelete, opt => opt.Ignore())
         .ForMember(a => a.CreateUserId, opt => opt.Ignore())
         .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
         .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
         .ForMember(a => a.ModifyDateTime, opt => opt.Ignore());
            //单品管理
            CreateMap<BcInstance, BcInstanceDTO>()
             ;
            //单品-附件
            CreateMap<BcAttachmentDTO, BcAttachment>()
                .ForMember(a => a.Category, opt => opt.Ignore())
                .ForMember(a => a.Path, opt => opt.Ignore())
                 .ForMember(a => a.BcId, opt => opt.Ignore())
                 .ForMember(a => a.CreateUser, opt => opt.Ignore())
                 .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
                 .ForMember(a => a.IsDelete, opt => opt.Ignore())
                 .ForMember(a => a.CreateUserId, opt => opt.Ignore())
                 .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
                 .ForMember(a => a.ModifyUser, opt => opt.Ignore())

                 .ForMember(a => a.ModifyDateTime, opt => opt.Ignore());

            #endregion

            #region 标的交付
            //交付描述
            CreateMap<ContSubDesDTO, ContSubDe>()
           .ForMember(a => a.SubDeliverId, opt => opt.Ignore())
            //.ForMember(a => a.CreateUserId, opt => opt.Ignore())
            //.ForMember(a => a.ModifyUserId, opt => opt.Ignore())
            //.ForMember(a => a.ModifyDateTime, opt => opt.Ignore())
            .ForMember(a => a.DeliverTypeNavigation, opt => opt.Ignore())
           .ForMember(a => a.Sub, opt => opt.Ignore())
           //.ForMember(a => a.CreateUser, opt => opt.Ignore())
           .ForMember(a => a.DeliverUser, opt => opt.Ignore())
           ;

            #endregion

            #region 合同模板
            CreateMap<ContTxtTemplateDTO, ContTxtTemplate>()
                .ForMember(a => a.Code, opt => opt.Ignore())
                .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
                 .ForMember(a => a.CreateUser, opt => opt.Ignore())
                    .ForMember(a => a.CreateUserId, opt => opt.Ignore())
                     .ForMember(a => a.ModifyDateTime, opt => opt.Ignore())
                     .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
                     .ForMember(a => a.Path, opt => opt.Ignore())
                      .ForMember(a => a.Vesion, opt => opt.Ignore())
                       .ForMember(a => a.IsDelete, opt => opt.Ignore())
                       .ForMember(a => a.WordEdit, opt => opt.Ignore())
                      .ForMember(a => a.ShowType, opt => opt.Ignore())
                       .ForMember(a => a.ShowTypeNumber, opt => opt.Ignore())
                       .ForMember(a => a.SubcompId, opt => opt.Ignore())
                      .ForMember(a => a.TepTypeNavigation, opt => opt.Ignore())
                       .ForMember(a => a.TextTypeNavigation, opt => opt.Ignore())
                ;
            //模板到模板历史

            CreateMap<ContTxtTemplate, ContTxtTemplateHist>()
                .ForMember(a => a.TempId, opt => opt.Ignore())
                .ForMember(a => a.Id, opt => opt.Ignore())
                ;

            #endregion


            #region 询价

            CreateMap<InquiryDTO, Inquiry>();
            CreateMap<OpenTenderConditionDTO, OpenTenderCondition>();
            CreateMap<InquirerDTO, Inquirer>();
            CreateMap<TheWinningUnitDTO, TheWinningUnit>();
            CreateMap<WinningInqDTO, WinningInq>();
            CreateMap<InquiryAttachmentDTO, InquiryAttachment>()
             .ForMember(a => a.Path, opt => opt.Ignore())
             .ForMember(a => a.ContId, opt => opt.Ignore())
             .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
             .ForMember(a => a.IsDelete, opt => opt.Ignore())
             .ForMember(a => a.CreateUserId, opt => opt.Ignore())
             .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
             .ForMember(a => a.ModifyDateTime, opt => opt.Ignore());
            #endregion
            #region 约谈

            CreateMap<QuestioningDTO, Questioning>();
            CreateMap<OpenBidDTO, OpenBid>();
            CreateMap<InterviewpeopleDTO, Interviewpeople>();
            CreateMap<BidlabelDTO, Bidlabel>();
            CreateMap<WinningQueDTO, WinningQue>();
            CreateMap<QuestioningAttachmentDTO, QuestioningAttachment>()
             .ForMember(a => a.Path, opt => opt.Ignore())
             .ForMember(a => a.ContId, opt => opt.Ignore())
             .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
             .ForMember(a => a.IsDelete, opt => opt.Ignore())
             .ForMember(a => a.CreateUserId, opt => opt.Ignore())
             .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
             .ForMember(a => a.ModifyDateTime, opt => opt.Ignore());

            #endregion

            #region 进度管理
            //进度管理
            CreateMap<ScheduleManagementDTO, ScheduleManagement>()
        .ForMember(a => a.State, opt => opt.Ignore())
        .ForMember(a => a.CreateUserId, opt => opt.Ignore())
        .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
        .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
        .ForMember(a => a.ModifyDateTime, opt => opt.Ignore())
        .ForMember(a => a.IsDelete, opt => opt.Ignore());

            //进度明细
            CreateMap<ScheduleListDTO, ScheduleList>()
            .ForMember(a => a.CreateUserId, opt => opt.Ignore())
        .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
         .ForMember(a => a.IsDelete, opt => opt.Ignore())
          .ForMember(a => a.ScheduleId, opt => opt.Ignore());
            //进度管理附件
            CreateMap<ScheduleManagementAttachmentDTO, ScheduleManagementAttachment>()
             .ForMember(a => a.Path, opt => opt.Ignore())
             .ForMember(a => a.SchedulemId, opt => opt.Ignore())
             .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
             .ForMember(a => a.IsDelete, opt => opt.Ignore())
             .ForMember(a => a.CreateUserId, opt => opt.Ignore())
             .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
             .ForMember(a => a.ModifyDateTime, opt => opt.Ignore());
            //进度明细
            CreateMap<ScheduleDetailDTO, ScheduleDetail>()
       .ForMember(a => a.State, opt => opt.Ignore())
       .ForMember(a => a.CreateUserId, opt => opt.Ignore())
       .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
       .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
       .ForMember(a => a.ModifyDateTime, opt => opt.Ignore())
       .ForMember(a => a.IsDelete, opt => opt.Ignore());
            //进度明细附件
            CreateMap<ScheduleDetailAttachmentDTO, ScheduleDetailAttachment>()
             .ForMember(a => a.Path, opt => opt.Ignore())
             .ForMember(a => a.ScheduledId, opt => opt.Ignore())
             .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
             .ForMember(a => a.IsDelete, opt => opt.Ignore())
             .ForMember(a => a.CreateUserId, opt => opt.Ignore())
             .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
             .ForMember(a => a.ModifyDateTime, opt => opt.Ignore());
            #endregion

            #region 纠纷案件管理
            CreateMap<jFsbDTO, DisputeManager>()
      .ForMember(a => a.Dstate, opt => opt.Ignore())
      .ForMember(a => a.CreateUserId, opt => opt.Ignore())
      .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
      .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
      .ForMember(a => a.ModifyDateTime, opt => opt.Ignore())
      .ForMember(a => a.IsDelete, opt => opt.Ignore());
            CreateMap<AjjfDTO, CaseManager>()
.ForMember(a => a.Dstate, opt => opt.Ignore())
.ForMember(a => a.CreateUserId, opt => opt.Ignore())
.ForMember(a => a.CreateDateTime, opt => opt.Ignore())
.ForMember(a => a.ModifyUserId, opt => opt.Ignore())
.ForMember(a => a.ModifyDateTime, opt => opt.Ignore())
.ForMember(a => a.IsDelete, opt => opt.Ignore());
            #endregion
            //案件附件
            CreateMap<AjfjDTO, AjglAttachment>()
             .ForMember(a => a.Path, opt => opt.Ignore())
             .ForMember(a => a.Ajglid, opt => opt.Ignore())
             .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
             .ForMember(a => a.IsDelete, opt => opt.Ignore())
             .ForMember(a => a.CreateUserId, opt => opt.Ignore())
             .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
             .ForMember(a => a.ModifyDateTime, opt => opt.Ignore());

            //案件管理审判文书
            CreateMap<AjglSpwsDTO, AjglSpwsAttachment>()
             .ForMember(a => a.Path, opt => opt.Ignore())
             .ForMember(a => a.AjglSpwsid, opt => opt.Ignore())
             .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
             .ForMember(a => a.IsDelete, opt => opt.Ignore())
             .ForMember(a => a.CreateUserId, opt => opt.Ignore())
             .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
             .ForMember(a => a.ModifyDateTime, opt => opt.Ignore());

            //案件管理诉讼保全附件
            CreateMap<AjglSsbqDTO, AjglSsBqAttachment>()
             .ForMember(a => a.Path, opt => opt.Ignore())
             .ForMember(a => a.AjglSsbqwjid, opt => opt.Ignore())
             .ForMember(a => a.CreateDateTime, opt => opt.Ignore())
             .ForMember(a => a.IsDelete, opt => opt.Ignore())
             .ForMember(a => a.CreateUserId, opt => opt.Ignore())
             .ForMember(a => a.ModifyUserId, opt => opt.Ignore())
             .ForMember(a => a.ModifyDateTime, opt => opt.Ignore());

        }



    }
}
