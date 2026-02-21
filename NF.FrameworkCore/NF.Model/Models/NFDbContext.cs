using Microsoft.EntityFrameworkCore;

namespace NF.Model.Models
{
    public partial class NFDbContext : DbContext
    {
        public NFDbContext()
        {
        }

        public NFDbContext(DbContextOptions<NFDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActFinceFile> ActFinceFiles { get; set; }
        public virtual DbSet<AjglAttachment> AjglAttachments { get; set; }
        public virtual DbSet<AjglSpwsAttachment> AjglSpwsAttachments { get; set; }
        public virtual DbSet<AjglSsBqAttachment> AjglSsBqAttachments { get; set; }
        public virtual DbSet<AjglXgLx> AjglXgLxes { get; set; }
        public virtual DbSet<AppGroupUser> AppGroupUsers { get; set; }
        public virtual DbSet<AppInst> AppInsts { get; set; }
        public virtual DbSet<AppInstHist> AppInstHists { get; set; }
        public virtual DbSet<AppInstNode> AppInstNodes { get; set; }
        public virtual DbSet<AppInstNodeArea> AppInstNodeAreas { get; set; }
        public virtual DbSet<AppInstNodeAreaHist> AppInstNodeAreaHists { get; set; }
        public virtual DbSet<AppInstNodeHist> AppInstNodeHists { get; set; }
        public virtual DbSet<AppInstNodeInfo> AppInstNodeInfos { get; set; }
        public virtual DbSet<AppInstNodeInfoHist> AppInstNodeInfoHists { get; set; }
        public virtual DbSet<AppInstNodeLine> AppInstNodeLines { get; set; }
        public virtual DbSet<AppInstNodeLineHist> AppInstNodeLineHists { get; set; }
        public virtual DbSet<AppInstOpin> AppInstOpins { get; set; }
        public virtual DbSet<AppInstOpinHist> AppInstOpinHists { get; set; }
        public virtual DbSet<BcAttachment> BcAttachments { get; set; }
        public virtual DbSet<BcInstance> BcInstances { get; set; }
        public virtual DbSet<Bidlabel> Bidlabels { get; set; }
        public virtual DbSet<BusinessCategory> BusinessCategories { get; set; }
        public virtual DbSet<CaseManager> CaseManagers { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<CompAttachment> CompAttachments { get; set; }
        public virtual DbSet<CompContact> CompContacts { get; set; }
        public virtual DbSet<CompDescription> CompDescriptions { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<ContActualFinance> ContActualFinances { get; set; }
        public virtual DbSet<ContAttachment> ContAttachments { get; set; }
        public virtual DbSet<ContConsult> ContConsults { get; set; }
        public virtual DbSet<ContCustomField> ContCustomFields { get; set; }
        public virtual DbSet<ContDescription> ContDescriptions { get; set; }
        public virtual DbSet<ContInvoice> ContInvoices { get; set; }
        public virtual DbSet<ContPlanFinance> ContPlanFinances { get; set; }
        public virtual DbSet<ContPlanFinanceHistory> ContPlanFinanceHistories { get; set; }
        public virtual DbSet<ContStatistic> ContStatistics { get; set; }
        public virtual DbSet<ContSubDe> ContSubDes { get; set; }
        public virtual DbSet<ContSubDelivery> ContSubDeliveries { get; set; }
        public virtual DbSet<ContSubjectMatter> ContSubjectMatters { get; set; }
        public virtual DbSet<ContSubjectMatterHistory> ContSubjectMatterHistories { get; set; }
        public virtual DbSet<ContText> ContTexts { get; set; }
        public virtual DbSet<ContTextArchive> ContTextArchives { get; set; }
        public virtual DbSet<ContTextArchiveItem> ContTextArchiveItems { get; set; }
        public virtual DbSet<ContTextBorrow> ContTextBorrows { get; set; }
        public virtual DbSet<ContTextHistory> ContTextHistories { get; set; }
        public virtual DbSet<ContTextSeal> ContTextSeals { get; set; }
        public virtual DbSet<ContTxtTempAndSubField> ContTxtTempAndSubFields { get; set; }
        public virtual DbSet<ContTxtTempAndVarStoreRela> ContTxtTempAndVarStoreRelas { get; set; }
        public virtual DbSet<ContTxtTempVarStore> ContTxtTempVarStores { get; set; }
        public virtual DbSet<ContTxtTemplate> ContTxtTemplates { get; set; }
        public virtual DbSet<ContTxtTemplateHist> ContTxtTemplateHists { get; set; }
        public virtual DbSet<ContractInfo> ContractInfos { get; set; }
        public virtual DbSet<ContractInfoHistory> ContractInfoHistories { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<CurrencyManager> CurrencyManagers { get; set; }
        public virtual DbSet<DataDictionary> DataDictionaries { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DeptMain> DeptMains { get; set; }
        public virtual DbSet<DisputeManager> DisputeManagers { get; set; }
        public virtual DbSet<FlowTemp> FlowTemps { get; set; }
        public virtual DbSet<FlowTempHist> FlowTempHists { get; set; }
        public virtual DbSet<FlowTempNode> FlowTempNodes { get; set; }
        public virtual DbSet<FlowTempNodeHist> FlowTempNodeHists { get; set; }
        public virtual DbSet<FlowTempNodeInfo> FlowTempNodeInfos { get; set; }
        public virtual DbSet<FlowTempNodeInfoHist> FlowTempNodeInfoHists { get; set; }
        public virtual DbSet<GroupInfo> GroupInfos { get; set; }
        public virtual DbSet<GroupUser> GroupUsers { get; set; }
        public virtual DbSet<Inquirer> Inquirers { get; set; }
        public virtual DbSet<Inquiry> Inquiries { get; set; }
        public virtual DbSet<InquiryAttachment> InquiryAttachments { get; set; }
        public virtual DbSet<Interviewpeople> Interviewpeople { get; set; }
        public virtual DbSet<InvoDescription> InvoDescriptions { get; set; }
        public virtual DbSet<InvoFile> InvoFiles { get; set; }
        public virtual DbSet<InvoiceCheck> InvoiceChecks { get; set; }
        public virtual DbSet<LoginLog> LoginLogs { get; set; }
        public virtual DbSet<OpenBid> OpenBids { get; set; }
        public virtual DbSet<OpenTenderCondition> OpenTenderConditions { get; set; }
        public virtual DbSet<OpeningSituationInfor> OpeningSituationInfors { get; set; }
        public virtual DbSet<OptionLog> OptionLogs { get; set; }
        public virtual DbSet<PlanFinnCheck> PlanFinnChecks { get; set; }
        public virtual DbSet<ProjAttachment> ProjAttachments { get; set; }
        public virtual DbSet<ProjDescription> ProjDescriptions { get; set; }
        public virtual DbSet<ProjSchedule> ProjSchedules { get; set; }
        public virtual DbSet<ProjectManager> ProjectManagers { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<Questioning> Questionings { get; set; }
        public virtual DbSet<QuestioningAttachment> QuestioningAttachments { get; set; }
        public virtual DbSet<Remind> Reminds { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleModule> RoleModules { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<ScheduleDetail> ScheduleDetails { get; set; }
        public virtual DbSet<ScheduleDetailAttachment> ScheduleDetailAttachments { get; set; }
        public virtual DbSet<ScheduleList> ScheduleLists { get; set; }
        public virtual DbSet<ScheduleManagement> ScheduleManagements { get; set; }
        public virtual DbSet<ScheduleManagementAttachment> ScheduleManagementAttachments { get; set; }
        public virtual DbSet<SealManager> SealManagers { get; set; }
        public virtual DbSet<SuccessfulBidderLable> SuccessfulBidderLables { get; set; }
        public virtual DbSet<SysEmail> SysEmails { get; set; }
        public virtual DbSet<SysField> SysFields { get; set; }
        public virtual DbSet<SysFunction> SysFunctions { get; set; }
        public virtual DbSet<SysModel> SysModels { get; set; }
        public virtual DbSet<TempNodeArea> TempNodeAreas { get; set; }
        public virtual DbSet<TempNodeAreaHist> TempNodeAreaHists { get; set; }
        public virtual DbSet<TempNodeLine> TempNodeLines { get; set; }
        public virtual DbSet<TempNodeLineHist> TempNodeLineHists { get; set; }
        public virtual DbSet<TenderAttachment> TenderAttachments { get; set; }
        public virtual DbSet<TenderInfor> TenderInfors { get; set; }
        public virtual DbSet<TendererNameLabel> TendererNameLabels { get; set; }
        public virtual DbSet<TheWinningUnit> TheWinningUnits { get; set; }
        public virtual DbSet<UserInfor> UserInfors { get; set; }
        public virtual DbSet<UserModule> UserModules { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<WinningInq> WinningInqs { get; set; }
        public virtual DbSet<WinningItem> WinningItems { get; set; }
        public virtual DbSet<WinningQue> WinningQues { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActFinceFile>(entity =>
            {
                entity.ToTable("ActFinceFile");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(500);

                entity.Property(e => e.FileSize).HasMaxLength(100);

                entity.Property(e => e.FolderName).HasMaxLength(100);

                entity.Property(e => e.GuidFileName).HasMaxLength(500);

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Path)
                    .HasMaxLength(1000)
                    .IsFixedLength();

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.HasOne(d => d.Ajfj)
                    .WithMany(p => p.ActFinceFiles)
                    .HasForeignKey(d => d.Ajfjid)
                    .HasConstraintName("FK_ActFinceFile_CaseManager");
            });

            modelBuilder.Entity<AjglAttachment>(entity =>
            {
                entity.ToTable("AjglAttachment");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(500);

                entity.Property(e => e.FolderName).HasMaxLength(50);

                entity.Property(e => e.GuidFileName).HasMaxLength(50);

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Path).HasMaxLength(500);

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.HasOne(d => d.Ajgl)
                    .WithMany(p => p.AjglAttachments)
                    .HasForeignKey(d => d.Ajglid)
                    .HasConstraintName("FK_AjglAttachment_CaseManager1");
            });

            modelBuilder.Entity<AjglSpwsAttachment>(entity =>
            {
                entity.ToTable("AjglSpwsAttachment");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(500);

                entity.Property(e => e.FolderName).HasMaxLength(50);

                entity.Property(e => e.GuidFileName).HasMaxLength(50);

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Path).HasMaxLength(500);

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.HasOne(d => d.AjglSpws)
                    .WithMany(p => p.AjglSpwsAttachments)
                    .HasForeignKey(d => d.AjglSpwsid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AjglSpwsAttachment_CaseManager");
            });

            modelBuilder.Entity<AjglSsBqAttachment>(entity =>
            {
                entity.ToTable("AjglSsBqAttachment");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(500);

                entity.Property(e => e.FolderName).HasMaxLength(50);

                entity.Property(e => e.GuidFileName).HasMaxLength(50);

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Path).HasMaxLength(500);

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.HasOne(d => d.AjglSsbqwj)
                    .WithMany(p => p.AjglSsBqAttachments)
                    .HasForeignKey(d => d.AjglSsbqwjid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AjglSsBqAttachment_CaseManager");
            });

            modelBuilder.Entity<AjglXgLx>(entity =>
            {
                entity.ToTable("AjglXgLx");

                entity.Property(e => e.Fznr).HasMaxLength(2000);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Ajgl)
                    .WithMany(p => p.AjglXgLxes)
                    .HasForeignKey(d => d.Ajglid)
                    .HasConstraintName("FK_AjglXgLx_CaseManager");
            });

            modelBuilder.Entity<AppGroupUser>(entity =>
            {
                entity.ToTable("AppGroupUser");

                entity.Property(e => e.NodeStrId).HasMaxLength(50);

                entity.HasOne(d => d.Inst)
                    .WithMany(p => p.AppGroupUsers)
                    .HasForeignKey(d => d.InstId)
                    .HasConstraintName("FK_AppGroupUser_AppInst");

                entity.HasOne(d => d.Ninfo)
                    .WithMany(p => p.AppGroupUsers)
                    .HasForeignKey(d => d.NinfoId)
                    .HasConstraintName("FK_AppGroupUser_AppInstNodeInfo");

                entity.HasOne(d => d.Node)
                    .WithMany(p => p.AppGroupUsers)
                    .HasForeignKey(d => d.NodeId)
                    .HasConstraintName("FK_AppGroupUser_AppInstNode");
            });

            modelBuilder.Entity<AppInst>(entity =>
            {
                entity.ToTable("AppInst");

                entity.Property(e => e.AppObjAmount).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.AppObjName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.AppObjNo).HasMaxLength(100);

                entity.Property(e => e.CompleteDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CurrentNodeName).HasMaxLength(150);

                entity.Property(e => e.CurrentNodeStrId).HasMaxLength(50);

                entity.Property(e => e.StartDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<AppInstHist>(entity =>
            {
                entity.ToTable("AppInstHist");

                entity.Property(e => e.AppObjAmount).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.AppObjName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.AppObjNo).HasMaxLength(100);

                entity.Property(e => e.CompleteDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CurrentNodeName).HasMaxLength(150);

                entity.Property(e => e.CurrentNodeStrId).HasMaxLength(50);

                entity.Property(e => e.StartDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<AppInstNode>(entity =>
            {
                entity.ToTable("AppInstNode");

                entity.Property(e => e.CompDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.NodeStrId).HasMaxLength(50);

                entity.Property(e => e.ReceDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<AppInstNodeArea>(entity =>
            {
                entity.ToTable("AppInstNodeArea");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.StrId).HasMaxLength(50);
            });

            modelBuilder.Entity<AppInstNodeAreaHist>(entity =>
            {
                entity.ToTable("AppInstNodeAreaHist");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.StrId).HasMaxLength(50);
            });

            modelBuilder.Entity<AppInstNodeHist>(entity =>
            {
                entity.ToTable("AppInstNodeHist");

                entity.Property(e => e.CompDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.NodeStrId).HasMaxLength(50);

                entity.Property(e => e.ReceDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<AppInstNodeInfo>(entity =>
            {
                entity.ToTable("AppInstNodeInfo");

                entity.Property(e => e.GroupName).HasMaxLength(100);

                entity.Property(e => e.Max).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.Min).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.NodeStrId).HasMaxLength(50);

                entity.Property(e => e.UsIds).HasMaxLength(50);
            });

            modelBuilder.Entity<AppInstNodeInfoHist>(entity =>
            {
                entity.ToTable("AppInstNodeInfoHist");

                entity.Property(e => e.GroupName).HasMaxLength(100);

                entity.Property(e => e.Max).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.Min).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.NodeStrId).HasMaxLength(50);
            });

            modelBuilder.Entity<AppInstNodeLine>(entity =>
            {
                entity.ToTable("AppInstNodeLine");

                entity.Property(e => e.From).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.StrId).HasMaxLength(50);

                entity.Property(e => e.To).HasMaxLength(50);
            });

            modelBuilder.Entity<AppInstNodeLineHist>(entity =>
            {
                entity.ToTable("AppInstNodeLineHist");

                entity.Property(e => e.From).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.StrId).HasMaxLength(50);

                entity.Property(e => e.To).HasMaxLength(50);
            });

            modelBuilder.Entity<AppInstOpin>(entity =>
            {
                entity.ToTable("AppInstOpin");

                entity.Property(e => e.CreateDatetime).HasColumnType("datetime");

                entity.Property(e => e.NodeStrId).HasMaxLength(50);

                entity.Property(e => e.Opinion).HasMaxLength(2000);

                entity.HasOne(d => d.Node)
                    .WithMany(p => p.AppInstOpins)
                    .HasForeignKey(d => d.NodeId)
                    .HasConstraintName("FK_AppInstOpin_AppInstNode");
            });

            modelBuilder.Entity<AppInstOpinHist>(entity =>
            {
                entity.ToTable("AppInstOpinHist");

                entity.Property(e => e.CreateDatetime).HasColumnType("datetime");

                entity.Property(e => e.NodeStrId).HasMaxLength(50);

                entity.Property(e => e.Opinion).HasMaxLength(2000);
            });

            modelBuilder.Entity<BcAttachment>(entity =>
            {
                entity.ToTable("BcAttachment");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(500);

                entity.Property(e => e.FolderName).HasMaxLength(100);

                entity.Property(e => e.GuidFileName).HasMaxLength(500);

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.BcAttachments)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_BcAttachment_DataDictionary");

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.BcAttachmentCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BcAttachment_UserInfor");

                entity.HasOne(d => d.ModifyUser)
                    .WithMany(p => p.BcAttachmentModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BcAttachment_UserInfor1");
            });

            modelBuilder.Entity<BcInstance>(entity =>
            {
                entity.ToTable("BcInstance");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Price).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.Property(e => e.Unit).HasMaxLength(20);

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.BcInstanceCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BcInstance_UserInfor");

                entity.HasOne(d => d.Lb)
                    .WithMany(p => p.BcInstances)
                    .HasForeignKey(d => d.LbId)
                    .HasConstraintName("FK_BcInstance_BusinessCategory");

                entity.HasOne(d => d.ModifyUser)
                    .WithMany(p => p.BcInstanceModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BcInstance_UserInfor1");
            });

            modelBuilder.Entity<Bidlabel>(entity =>
            {
                entity.ToTable("Bidlabel");

                entity.Property(e => e.BidPrice).HasColumnType("decimal(28, 2)");

                entity.Property(e => e.BidPrices).HasColumnType("decimal(28, 2)");

                entity.Property(e => e.Bidlabel1).HasColumnName("Bidlabel");

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.HasOne(d => d.Ques)
                    .WithMany(p => p.Bidlabels)
                    .HasForeignKey(d => d.QuesId)
                    .HasConstraintName("FK_Bidlabel_Questioning");

                entity.HasOne(d => d.WinningUnitNavigation)
                    .WithMany(p => p.Bidlabels)
                    .HasForeignKey(d => d.WinningUnit)
                    .HasConstraintName("FK_Bidlabel_OpenBid");
            });

            modelBuilder.Entity<BusinessCategory>(entity =>
            {
                entity.ToTable("BusinessCategory");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<CaseManager>(entity =>
            {
                entity.ToTable("CaseManager");

                entity.Property(e => e.Amount).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.AmoutDisc).HasMaxLength(2000);

                entity.Property(e => e.BeginDate).HasColumnType("datetime");

                entity.Property(e => e.CaseBrief).HasMaxLength(2000);

                entity.Property(e => e.CaseFileName).HasMaxLength(500);

                entity.Property(e => e.CaseFilePath).HasMaxLength(1000);

                entity.Property(e => e.CaseRemark).HasMaxLength(2000);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CourtName).HasMaxLength(500);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.DisCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DisDic).HasMaxLength(2000);

                entity.Property(e => e.DisName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Dstate).HasColumnName("DState");

                entity.Property(e => e.HandDate).HasColumnType("datetime");

                entity.Property(e => e.HandRemark).HasMaxLength(2000);

                entity.Property(e => e.LawyerName)
                    .HasMaxLength(100)
                    .HasColumnName("lawyerName");

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Pawn).HasMaxLength(500);

                entity.Property(e => e.PreFileName).HasMaxLength(500);

                entity.Property(e => e.PreFilePath).HasMaxLength(1000);

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.Property(e => e.SpFileName).HasMaxLength(500);

                entity.Property(e => e.SpFilePath).HasMaxLength(1000);

                entity.Property(e => e.SpResult).HasMaxLength(2000);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.WoDw).HasMaxLength(500);

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.CaseManagers)
                    .HasForeignKey(d => d.CreateUserId)
                    .HasConstraintName("FK_CaseManager_UserInfor");

                entity.HasOne(d => d.Dis)
                    .WithMany(p => p.CaseManagers)
                    .HasForeignKey(d => d.DisId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CaseManager_DisputeManager");

                entity.HasOne(d => d.DisputeTypeNavigation)
                    .WithMany(p => p.CaseManagerDisputeTypeNavigations)
                    .HasForeignKey(d => d.DisputeType)
                    .HasConstraintName("FK_CaseManager_DataDictionary1");

                entity.HasOne(d => d.PreTypeNavigation)
                    .WithMany(p => p.CaseManagerPreTypeNavigations)
                    .HasForeignKey(d => d.PreType)
                    .HasConstraintName("FK_CaseManager_DataDictionary2");

                entity.HasOne(d => d.UrgentNavigation)
                    .WithMany(p => p.CaseManagerUrgentNavigations)
                    .HasForeignKey(d => d.Urgent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CaseManager_DataDictionary");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.DisplayName).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<CompAttachment>(entity =>
            {
                entity.ToTable("CompAttachment");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(500);

                entity.Property(e => e.FolderName).HasMaxLength(100);

                entity.Property(e => e.GuidFileName).HasMaxLength(500);

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CompAttachments)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_CompAttachment_DataDictionary");

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.CompAttachments)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompAttachment_UserInfor1");
            });

            modelBuilder.Entity<CompContact>(entity =>
            {
                entity.ToTable("CompContact");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.DeptName).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Fax).HasMaxLength(50);

                entity.Property(e => e.Im).HasMaxLength(50);

                entity.Property(e => e.Mobile).HasMaxLength(50);

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Position).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.Property(e => e.Tel).HasMaxLength(50);

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.CompContactCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompContact_UserInfor1");

                entity.HasOne(d => d.ModifyUser)
                    .WithMany(p => p.CompContactModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompContact_UserInfor2");
            });

            modelBuilder.Entity<CompDescription>(entity =>
            {
                entity.ToTable("CompDescription");

                entity.Property(e => e.ContentText).HasMaxLength(2000);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.Item)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.CompDescriptionCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompDescription_UserInfor1");

                entity.HasOne(d => d.ModifyUser)
                    .WithMany(p => p.CompDescriptionModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompDescription_UserInfor2");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.BankAccount).HasMaxLength(50);

                entity.Property(e => e.BankName).HasMaxLength(50);

                entity.Property(e => e.BusinessScope).HasMaxLength(200);

                entity.Property(e => e.BusinessTerm).HasMaxLength(50);

                entity.Property(e => e.Code).HasMaxLength(100);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.ExpirationDateTime).HasColumnType("datetime");

                entity.Property(e => e.Fax).HasMaxLength(50);

                entity.Property(e => e.FirstContact).HasMaxLength(50);

                entity.Property(e => e.FirstContactDept).HasMaxLength(50);

                entity.Property(e => e.FirstContactEmail).HasMaxLength(50);

                entity.Property(e => e.FirstContactMobile).HasMaxLength(50);

                entity.Property(e => e.FirstContactPosition).HasMaxLength(50);

                entity.Property(e => e.FirstContactQq)
                    .HasMaxLength(50)
                    .HasColumnName("FirstContactQQ");

                entity.Property(e => e.FirstContactTel).HasMaxLength(50);

                entity.Property(e => e.FoundDateTime).HasColumnType("datetime");

                entity.Property(e => e.InvoiceAddress).HasMaxLength(100);

                entity.Property(e => e.InvoiceTel).HasMaxLength(50);

                entity.Property(e => e.InvoiceTitle).HasMaxLength(100);

                entity.Property(e => e.LegalPerson).HasMaxLength(50);

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PaidUpCapital).HasMaxLength(50);

                entity.Property(e => e.PostCode).HasMaxLength(10);

                entity.Property(e => e.RegisterAddress).HasMaxLength(200);

                entity.Property(e => e.RegisterCapital).HasMaxLength(20);

                entity.Property(e => e.Remark).HasMaxLength(4000);

                entity.Property(e => e.Reserve1).HasMaxLength(50);

                entity.Property(e => e.Reserve2).HasMaxLength(50);

                entity.Property(e => e.TaxIdentification).HasMaxLength(50);

                entity.Property(e => e.Tel).HasMaxLength(50);

                entity.Property(e => e.Trade).HasMaxLength(150);

                entity.Property(e => e.WebSite).HasMaxLength(100);

                entity.Property(e => e.WfCurrNodeName).HasMaxLength(50);

                entity.HasOne(d => d.Caredit)
                    .WithMany(p => p.CompanyCaredits)
                    .HasForeignKey(d => d.CareditId)
                    .HasConstraintName("FK_Company_DataDictionary2");

                entity.HasOne(d => d.CompClass)
                    .WithMany(p => p.CompanyCompClasses)
                    .HasForeignKey(d => d.CompClassId)
                    .HasConstraintName("FK_Company_DataDictionary3");

                entity.HasOne(d => d.CompType)
                    .WithMany(p => p.CompanyCompTypes)
                    .HasForeignKey(d => d.CompTypeId)
                    .HasConstraintName("FK_Company_DataDictionary4");

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.CompanyCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_UserInfor1");

                entity.HasOne(d => d.Level)
                    .WithMany(p => p.CompanyLevels)
                    .HasForeignKey(d => d.LevelId)
                    .HasConstraintName("FK_Company_DataDictionary1");

                entity.HasOne(d => d.ModifyUser)
                    .WithMany(p => p.CompanyModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_UserInfor2");

                entity.HasOne(d => d.PrincipalUser)
                    .WithMany(p => p.CompanyPrincipalUsers)
                    .HasForeignKey(d => d.PrincipalUserId)
                    .HasConstraintName("FK_Company_UserInfor3");
            });

            modelBuilder.Entity<ContActualFinance>(entity =>
            {
                entity.ToTable("ContActualFinance");

                entity.Property(e => e.Account).HasMaxLength(50);

                entity.Property(e => e.ActualSettlementDate).HasColumnType("datetime");

                entity.Property(e => e.AmountMoney).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.Bank).HasMaxLength(50);

                entity.Property(e => e.ConfirmDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("ConfirmDateTIme");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CurrencyRate).HasColumnType("decimal(19, 8)");

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.Property(e => e.Reserve1).HasMaxLength(100);

                entity.Property(e => e.Reserve2).HasMaxLength(100);

                entity.Property(e => e.TuisongPz).HasMaxLength(50);

                entity.Property(e => e.VoucherNo).HasMaxLength(50);

                entity.Property(e => e.WfCurrNodeName).HasMaxLength(50);

                entity.HasOne(d => d.ConfirmUser)
                    .WithMany(p => p.ContActualFinanceConfirmUsers)
                    .HasForeignKey(d => d.ConfirmUserId)
                    .HasConstraintName("FK_ContActualFinance_UserInfor1");

                entity.HasOne(d => d.Cont)
                    .WithMany(p => p.ContActualFinances)
                    .HasForeignKey(d => d.ContId)
                    .HasConstraintName("FK_ContActualFinance_ContractInfo");

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.ContActualFinanceCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContActualFinance_UserInfor");
            });

            modelBuilder.Entity<ContAttachment>(entity =>
            {
                entity.ToTable("ContAttachment");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(500);

                entity.Property(e => e.FolderName).HasMaxLength(50);

                entity.Property(e => e.GuidFileName).HasMaxLength(50);

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Path).HasMaxLength(500);

                entity.Property(e => e.Remark).HasMaxLength(2000);
            });

            modelBuilder.Entity<ContConsult>(entity =>
            {
                entity.ToTable("ContConsult");
            });

            modelBuilder.Entity<ContCustomField>(entity =>
            {
                entity.ToTable("ContCustomField");

                entity.Property(e => e.FieldVale).HasMaxLength(2000);
            });

            modelBuilder.Entity<ContDescription>(entity =>
            {
                entity.ToTable("ContDescription");

                entity.Property(e => e.Ccontent).HasMaxLength(2000);

                entity.Property(e => e.Citem).HasMaxLength(500);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<ContInvoice>(entity =>
            {
                entity.ToTable("ContInvoice");

                entity.Property(e => e.ActAmountMoney).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.AmountMoney).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.BankAccount).HasMaxLength(50);

                entity.Property(e => e.BankName).HasMaxLength(50);

                entity.Property(e => e.CheckAmount).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.ConfirmDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("ConfirmDateTIme");

                entity.Property(e => e.ConfirmedAmount).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CurrencyRate).HasColumnType("decimal(19, 8)");

                entity.Property(e => e.InAddress).HasMaxLength(150);

                entity.Property(e => e.InCode).HasMaxLength(1000);

                entity.Property(e => e.InContent).HasMaxLength(1000);

                entity.Property(e => e.InTel).HasMaxLength(50);

                entity.Property(e => e.InTitle).HasMaxLength(150);

                entity.Property(e => e.MakeOutDateTime).HasColumnType("datetime");

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.Property(e => e.Reserve1).HasMaxLength(100);

                entity.Property(e => e.Reserve2).HasMaxLength(100);

                entity.Property(e => e.SubAmount).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.SurplusAmount).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.TaxpayerIdentification).HasMaxLength(50);

                entity.Property(e => e.WfCurrNodeName).HasMaxLength(50);

                entity.HasOne(d => d.ConfirmUser)
                    .WithMany(p => p.ContInvoiceConfirmUsers)
                    .HasForeignKey(d => d.ConfirmUserId)
                    .HasConstraintName("FK_ContInvoice_UserInfor2");

                entity.HasOne(d => d.Cont)
                    .WithMany(p => p.ContInvoices)
                    .HasForeignKey(d => d.ContId)
                    .HasConstraintName("FK_ContInvoice_ContractInfo");

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.ContInvoiceCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContInvoice_UserInfor1");
            });

            modelBuilder.Entity<ContPlanFinance>(entity =>
            {
                entity.ToTable("ContPlanFinance");

                entity.Property(e => e.ActAmountMoney).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.ActSettlementDate).HasColumnType("datetime");

                entity.Property(e => e.AmountMoney).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.CheckAmount).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.ConfirmedAmount).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CurrencyRate).HasColumnType("decimal(19, 8)");

                entity.Property(e => e.IsWxmsgDate)
                    .HasColumnType("datetime")
                    .HasColumnName("IsWXMsgDate");

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PlanCompleteDateTime).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.Property(e => e.SubAmount).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.SurplusAmount).HasColumnType("decimal(28, 6)");

                entity.HasOne(d => d.Cont)
                    .WithMany(p => p.ContPlanFinances)
                    .HasForeignKey(d => d.ContId)
                    .HasConstraintName("FK_ContPlanFinance_ContractInfo1");
            });

            modelBuilder.Entity<ContPlanFinanceHistory>(entity =>
            {
                entity.ToTable("ContPlanFinanceHistory");

                entity.Property(e => e.ActAmountMoney).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.ActSettlementDate).HasColumnType("datetime");

                entity.Property(e => e.AmountMoney).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.CheckAmount).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.ConfirmedAmount).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CurrencyRate).HasColumnType("decimal(19, 8)");

                entity.Property(e => e.IsWxmsgDate)
                    .HasColumnType("datetime")
                    .HasColumnName("IsWXMsgDate");

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PlanCompleteDateTime).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.Property(e => e.SubAmount).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.SurplusAmount).HasColumnType("decimal(28, 6)");

                entity.HasOne(d => d.ContHis)
                    .WithMany(p => p.ContPlanFinanceHistories)
                    .HasForeignKey(d => d.ContHisId)
                    .HasConstraintName("FK_ContPlanFinanceHistory_ContractInfoHistory");
            });

            modelBuilder.Entity<ContStatistic>(entity =>
            {
                entity.ToTable("ContStatistic");

                entity.Property(e => e.ActualAmount).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.BalaTick).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.CompActAm).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.CompInAm).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.CompRatio).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.InvoiceAmount).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<ContSubDe>(entity =>
            {
                entity.ToTable("ContSubDe");

                entity.Property(e => e.ActualDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CurrDevNumber).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.DeliverLocation).HasMaxLength(200);

                entity.Property(e => e.Dstate).HasColumnName("DState");

                entity.Property(e => e.Field1).HasMaxLength(100);

                entity.Property(e => e.Field2).HasMaxLength(100);

                entity.Property(e => e.FileName).HasMaxLength(200);

                entity.Property(e => e.FolderName).HasMaxLength(20);

                entity.Property(e => e.GuidFileName).HasMaxLength(200);

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.NotDevNumber).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.Path).HasMaxLength(300);

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.ContSubDeCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContSubDes_UserInfor1");

                entity.HasOne(d => d.DeliverTypeNavigation)
                    .WithMany(p => p.ContSubDes)
                    .HasForeignKey(d => d.DeliverType)
                    .HasConstraintName("FK_ContSubDes_DataDictionary");

                entity.HasOne(d => d.DeliverUser)
                    .WithMany(p => p.ContSubDeDeliverUsers)
                    .HasForeignKey(d => d.DeliverUserId)
                    .HasConstraintName("FK_ContSubDes_UserInfor");

                entity.HasOne(d => d.Sub)
                    .WithMany(p => p.ContSubDes)
                    .HasForeignKey(d => d.SubId)
                    .HasConstraintName("FK_ContSubDes_ContSubjectMatter");
            });

            modelBuilder.Entity<ContSubDelivery>(entity =>
            {
                entity.ToTable("ContSubDelivery");

                entity.Property(e => e.ActualDateTime).HasColumnType("datetime");

                entity.Property(e => e.AmountMoney).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.PlanDateTime).HasColumnType("datetime");

                entity.Property(e => e.ThedeliveryAmount).HasColumnType("decimal(28, 6)");

                entity.HasOne(d => d.Sub)
                    .WithMany(p => p.ContSubDeliveries)
                    .HasForeignKey(d => d.SubId)
                    .HasConstraintName("FK_ContSubDelivery_ContSubjectMatter");
            });

            modelBuilder.Entity<ContSubjectMatter>(entity =>
            {
                entity.ToTable("ContSubjectMatter");

                entity.Property(e => e.Amount).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.AmountMoney).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.ComplateAmount).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.DelNum).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.DiscountRate).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.Field1).HasMaxLength(1000);

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.NominalQuote).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.NominalRate).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.PlanDateTime).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.Property(e => e.SalePrice).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.SjJfRq).HasColumnType("datetime");

                entity.Property(e => e.Spec).HasMaxLength(100);

                entity.Property(e => e.Stype).HasMaxLength(100);

                entity.Property(e => e.SubTotal).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.SubTotalRate).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.Unit).HasMaxLength(20);

                entity.HasOne(d => d.BcInstance)
                    .WithMany(p => p.ContSubjectMatters)
                    .HasForeignKey(d => d.BcInstanceId)
                    .HasConstraintName("FK_ContSubjectMatter_BcInstance");

                entity.HasOne(d => d.Cont)
                    .WithMany(p => p.ContSubjectMatters)
                    .HasForeignKey(d => d.ContId)
                    .HasConstraintName("FK_ContSubjectMatter_ContractInfo");
            });

            modelBuilder.Entity<ContSubjectMatterHistory>(entity =>
            {
                entity.ToTable("ContSubjectMatterHistory");

                entity.Property(e => e.Amount).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.AmountMoney).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.ComplateAmount).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.DelNum).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.DiscountRate).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.Field1).HasMaxLength(1000);

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.NominalQuote).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.NominalRate).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.PlanDateTime).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.Property(e => e.SalePrice).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.SjJfRq).HasColumnType("datetime");

                entity.Property(e => e.Spec).HasMaxLength(100);

                entity.Property(e => e.Stype).HasMaxLength(100);

                entity.Property(e => e.SubTotal).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.SubTotalRate).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.Unit).HasMaxLength(20);
            });

            modelBuilder.Entity<ContText>(entity =>
            {
                entity.ToTable("ContText");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.ElectronicVersionPath).HasMaxLength(500);

                entity.Property(e => e.ExtenName).HasMaxLength(10);

                entity.Property(e => e.FileName).HasMaxLength(500);

                entity.Property(e => e.FolderName).HasMaxLength(50);

                entity.Property(e => e.GuidFileName).HasMaxLength(50);

                entity.Property(e => e.LockTime).HasColumnType("datetime");

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Path).HasMaxLength(500);

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.Property(e => e.WordPath).HasMaxLength(500);

                entity.HasOne(d => d.Cont)
                    .WithMany(p => p.ContTexts)
                    .HasForeignKey(d => d.ContId)
                    .HasConstraintName("FK_ContText_ContractInfo");

                entity.HasOne(d => d.Template)
                    .WithMany(p => p.ContTexts)
                    .HasForeignKey(d => d.TemplateId)
                    .HasConstraintName("FK_ContText_ContTxtTemplateHist");
            });

            modelBuilder.Entity<ContTextArchive>(entity =>
            {
                entity.ToTable("ContTextArchive");

                entity.Property(e => e.ArcCabCode).HasMaxLength(15);

                entity.Property(e => e.ArcCode).HasMaxLength(15);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<ContTextArchiveItem>(entity =>
            {
                entity.ToTable("ContTextArchiveItem");

                entity.Property(e => e.ArcCabCode).HasMaxLength(15);

                entity.Property(e => e.ArcCode).HasMaxLength(15);

                entity.Property(e => e.ArcRemark).HasMaxLength(1000);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(500);

                entity.Property(e => e.FolderName).HasMaxLength(50);

                entity.Property(e => e.GuidFileName).HasMaxLength(50);

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Path).HasMaxLength(500);

                entity.Property(e => e.SubDateTime).HasColumnType("datetime");

                entity.Property(e => e.SubUser).HasMaxLength(20);
            });

            modelBuilder.Entity<ContTextBorrow>(entity =>
            {
                entity.ToTable("ContTextBorrow");

                entity.Property(e => e.BorrDateTime).HasColumnType("datetime");

                entity.Property(e => e.BorrDeptName).HasMaxLength(100);

                entity.Property(e => e.BorrRemark).HasMaxLength(1000);

                entity.Property(e => e.BorrUser).HasMaxLength(20);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.RepayDateTime).HasColumnType("datetime");

                entity.Property(e => e.RepayUser).HasMaxLength(10);
            });

            modelBuilder.Entity<ContTextHistory>(entity =>
            {
                entity.ToTable("ContTextHistory");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.ElectronicVersionPath).HasMaxLength(500);

                entity.Property(e => e.ExtenName).HasMaxLength(10);

                entity.Property(e => e.FileName).HasMaxLength(500);

                entity.Property(e => e.FolderName).HasMaxLength(50);

                entity.Property(e => e.GuidFileName).HasMaxLength(50);

                entity.Property(e => e.LockTime).HasColumnType("datetime");

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Path).HasMaxLength(500);

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.Property(e => e.WordPath).HasMaxLength(500);

                entity.HasOne(d => d.ContHis)
                    .WithMany(p => p.ContTextHistories)
                    .HasForeignKey(d => d.ContHisId)
                    .HasConstraintName("FK_ContTextHistory_ContractInfoHistory");

                entity.HasOne(d => d.Template)
                    .WithMany(p => p.ContTextHistories)
                    .HasForeignKey(d => d.TemplateId)
                    .HasConstraintName("FK_ContTextHistory_ContTxtTemplateHist");
            });

            modelBuilder.Entity<ContTextSeal>(entity =>
            {
                entity.ToTable("ContTextSeal");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.SealUser).HasMaxLength(20);

                entity.HasOne(d => d.ContText)
                    .WithMany(p => p.ContTextSeals)
                    .HasForeignKey(d => d.ContTextId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContTextSeal_ContText");

                entity.HasOne(d => d.Seal)
                    .WithMany(p => p.ContTextSeals)
                    .HasForeignKey(d => d.SealId)
                    .HasConstraintName("FK_ContTextSeal_SealManager");
            });

            modelBuilder.Entity<ContTxtTempAndSubField>(entity =>
            {
                entity.ToTable("ContTxtTempAndSubField");

                entity.Property(e => e.Sval).HasMaxLength(50);
            });

            modelBuilder.Entity<ContTxtTempAndVarStoreRela>(entity =>
            {
                entity.ToTable("ContTxtTempAndVarStoreRela");
            });

            modelBuilder.Entity<ContTxtTempVarStore>(entity =>
            {
                entity.ToTable("ContTxtTempVarStore");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.OriginalId).HasColumnName("OriginalID");
            });

            modelBuilder.Entity<ContTxtTemplate>(entity =>
            {
                entity.ToTable("ContTxtTemplate");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.DeptIds).HasMaxLength(500);

                entity.Property(e => e.MingXiTitle).HasMaxLength(200);

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Path).HasMaxLength(300);

                entity.Property(e => e.TepTypes).HasMaxLength(1000);

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.ContTxtTemplates)
                    .HasForeignKey(d => d.CreateUserId)
                    .HasConstraintName("FK_ContTxtTemplate_UserInfor");

                entity.HasOne(d => d.TepTypeNavigation)
                    .WithMany(p => p.ContTxtTemplateTepTypeNavigations)
                    .HasForeignKey(d => d.TepType)
                    .HasConstraintName("FK_ContTxtTemplate_DataDictionary");

                entity.HasOne(d => d.TextTypeNavigation)
                    .WithMany(p => p.ContTxtTemplateTextTypeNavigations)
                    .HasForeignKey(d => d.TextType)
                    .HasConstraintName("FK_ContTxtTemplate_DataDictionary1");
            });

            modelBuilder.Entity<ContTxtTemplateHist>(entity =>
            {
                entity.ToTable("ContTxtTemplateHist");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.DeptIds).HasMaxLength(500);

                entity.Property(e => e.MingXiTitle).HasMaxLength(200);

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Path).HasMaxLength(300);

                entity.Property(e => e.TepTypes).HasMaxLength(1000);

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.ContTxtTemplateHists)
                    .HasForeignKey(d => d.CreateUserId)
                    .HasConstraintName("FK_ContTxtTemplateHist_UserInfor");

                entity.HasOne(d => d.TepTypeNavigation)
                    .WithMany(p => p.ContTxtTemplateHistTepTypeNavigations)
                    .HasForeignKey(d => d.TepType)
                    .HasConstraintName("FK_ContTxtTemplateHist_DataDictionary");

                entity.HasOne(d => d.TextTypeNavigation)
                    .WithMany(p => p.ContTxtTemplateHistTextTypeNavigations)
                    .HasForeignKey(d => d.TextType)
                    .HasConstraintName("FK_ContTxtTemplateHist_DataDictionary1");
            });

            modelBuilder.Entity<ContractInfo>(entity =>
            {
                entity.ToTable("ContractInfo");

                entity.Property(e => e.ActualCompleteDateTime).HasColumnType("datetime");

                entity.Property(e => e.AdvanceAmount).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.AmountMoney).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.Code).HasMaxLength(100);

                entity.Property(e => e.ContSingNo).HasMaxLength(20);

                entity.Property(e => e.CountField).HasMaxLength(200);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CurrencyRate).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Dqjdspr).HasMaxLength(50);

                entity.Property(e => e.EffectiveDateTime).HasColumnType("datetime");

                entity.Property(e => e.EstimateAmount).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.FinanceTerms).HasMaxLength(4000);

                entity.Property(e => e.HtXmnr).HasMaxLength(4000);

                entity.Property(e => e.IsWxmsgDate)
                    .HasMaxLength(10)
                    .HasColumnName("IsWXMsgDate")
                    .IsFixedLength();

                entity.Property(e => e.ModificationRemark).HasMaxLength(4000);

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.OtherCode).HasMaxLength(100);

                entity.Property(e => e.PerformanceDateTime).HasColumnType("datetime");

                entity.Property(e => e.PlanCompleteDateTime).HasColumnType("datetime");

                entity.Property(e => e.Reserve1).HasMaxLength(100);

                entity.Property(e => e.Reserve2).HasMaxLength(100);

                entity.Property(e => e.SngnDateTime).HasColumnType("datetime");

                entity.Property(e => e.StampTax).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.WfCurrNodeName).HasMaxLength(50);

                entity.HasOne(d => d.Comp)
                    .WithMany(p => p.ContractInfoComps)
                    .HasForeignKey(d => d.CompId)
                    .HasConstraintName("FK_ContractInfo_Company");

                entity.HasOne(d => d.CompId3Navigation)
                    .WithMany(p => p.ContractInfoCompId3Navigations)
                    .HasForeignKey(d => d.CompId3)
                    .HasConstraintName("FK_ContractInfo_Company3");

                entity.HasOne(d => d.CompId4Navigation)
                    .WithMany(p => p.ContractInfoCompId4Navigations)
                    .HasForeignKey(d => d.CompId4)
                    .HasConstraintName("FK_ContractInfo_Company4");

                entity.HasOne(d => d.ContSource)
                    .WithMany(p => p.ContractInfoContSources)
                    .HasForeignKey(d => d.ContSourceId)
                    .HasConstraintName("FK_ContractInfo_ContSource");

                entity.HasOne(d => d.ContStatic)
                    .WithMany(p => p.ContractInfos)
                    .HasForeignKey(d => d.ContStaticId)
                    .HasConstraintName("FK_ContractInfo_ContStatistics");

                entity.HasOne(d => d.ContType)
                    .WithMany(p => p.ContractInfoContTypes)
                    .HasForeignKey(d => d.ContTypeId)
                    .HasConstraintName("FK_ContractInfo_ContType");

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.ContractInfoCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractInfo_CreateUser");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.ContractInfos)
                    .HasForeignKey(d => d.CurrencyId)
                    .HasConstraintName("FK_ContractInfo_CurrencyManager");

                entity.HasOne(d => d.Dept)
                    .WithMany(p => p.ContractInfoDepts)
                    .HasForeignKey(d => d.DeptId)
                    .HasConstraintName("FK_ContractInfo_Department");

                entity.HasOne(d => d.MainDept)
                    .WithMany(p => p.ContractInfoMainDepts)
                    .HasForeignKey(d => d.MainDeptId)
                    .HasConstraintName("FK_ContractInfo_MainDepartment");

                entity.HasOne(d => d.PrincipalUser)
                    .WithMany(p => p.ContractInfoPrincipalUsers)
                    .HasForeignKey(d => d.PrincipalUserId)
                    .HasConstraintName("FK_ContractInfo_PriUser");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ContractInfos)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_ContractInfo_ProjectManager");

                entity.HasOne(d => d.SumCont)
                    .WithMany(p => p.InverseSumCont)
                    .HasForeignKey(d => d.SumContId)
                    .HasConstraintName("FK_ContractInfo_SumContractInfo");

                entity.HasOne(d => d.Xj)
                    .WithMany(p => p.ContractInfos)
                    .HasForeignKey(d => d.Xjid)
                    .HasConstraintName("FK_ContractInfo_Inquiry");

                entity.HasOne(d => d.Yt)
                    .WithMany(p => p.ContractInfos)
                    .HasForeignKey(d => d.Ytid)
                    .HasConstraintName("FK_ContractInfo_Questioning");

                entity.HasOne(d => d.Zb)
                    .WithMany(p => p.ContractInfos)
                    .HasForeignKey(d => d.Zbid)
                    .HasConstraintName("FK_ContractInfo_TenderInfor");
            });

            modelBuilder.Entity<ContractInfoHistory>(entity =>
            {
                entity.ToTable("ContractInfoHistory");

                entity.Property(e => e.ActualCompleteDateTime).HasColumnType("datetime");

                entity.Property(e => e.AdvanceAmount).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.AmountMoney).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.Code).HasMaxLength(100);

                entity.Property(e => e.ContSingNo).HasMaxLength(20);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CurrencyRate).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.EffectiveDateTime).HasColumnType("datetime");

                entity.Property(e => e.EstimateAmount).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.FinanceTerms).HasMaxLength(4000);

                entity.Property(e => e.ModificationRemark).HasMaxLength(4000);

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.OtherCode).HasMaxLength(100);

                entity.Property(e => e.PerformanceDateTime).HasColumnType("datetime");

                entity.Property(e => e.PlanCompleteDateTime).HasColumnType("datetime");

                entity.Property(e => e.Reserve1).HasMaxLength(100);

                entity.Property(e => e.Reserve2).HasMaxLength(100);

                entity.Property(e => e.SngnDateTime).HasColumnType("datetime");

                entity.Property(e => e.StampTax).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.WfCurrNodeName).HasMaxLength(50);

                entity.HasOne(d => d.Comp)
                    .WithMany(p => p.ContractInfoHistoryComps)
                    .HasForeignKey(d => d.CompId)
                    .HasConstraintName("FK_ContractInfoHistory_Company");

                entity.HasOne(d => d.CompId3Navigation)
                    .WithMany(p => p.ContractInfoHistoryCompId3Navigations)
                    .HasForeignKey(d => d.CompId3)
                    .HasConstraintName("FK_ContractInfoHistory_Company3");

                entity.HasOne(d => d.CompId4Navigation)
                    .WithMany(p => p.ContractInfoHistoryCompId4Navigations)
                    .HasForeignKey(d => d.CompId4)
                    .HasConstraintName("FK_ContractInfoHistory_Company4");

                entity.HasOne(d => d.ContSource)
                    .WithMany(p => p.ContractInfoHistoryContSources)
                    .HasForeignKey(d => d.ContSourceId)
                    .HasConstraintName("FK_ContractInfoHistory_ContSource");

                entity.HasOne(d => d.ContType)
                    .WithMany(p => p.ContractInfoHistoryContTypes)
                    .HasForeignKey(d => d.ContTypeId)
                    .HasConstraintName("FK_ContractInfoHistory_ContType");

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.ContractInfoHistoryCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractInfoHistory_CreateUser");

                entity.HasOne(d => d.Dept)
                    .WithMany(p => p.ContractInfoHistoryDepts)
                    .HasForeignKey(d => d.DeptId)
                    .HasConstraintName("FK_ContractInfoHistory_Department");

                entity.HasOne(d => d.MainDept)
                    .WithMany(p => p.ContractInfoHistoryMainDepts)
                    .HasForeignKey(d => d.MainDeptId)
                    .HasConstraintName("FK_ContractInfoHistory_MainDepartment");

                entity.HasOne(d => d.PrincipalUser)
                    .WithMany(p => p.ContractInfoHistoryPrincipalUsers)
                    .HasForeignKey(d => d.PrincipalUserId)
                    .HasConstraintName("FK_ContractInfoHistory_PriUser");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ContractInfoHistories)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_ContractInfoHistory_ProjectManager");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.Property(e => e.DisplayName).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<CurrencyManager>(entity =>
            {
                entity.ToTable("CurrencyManager");

                entity.Property(e => e.Abbreviation).HasMaxLength(20);

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Rate).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.Property(e => e.ShortName).HasMaxLength(50);
            });

            modelBuilder.Entity<DataDictionary>(entity =>
            {
                entity.ToTable("DataDictionary");

                entity.Property(e => e.CreateDatetime).HasColumnType("datetime");

                entity.Property(e => e.Dtype).HasMaxLength(50);

                entity.Property(e => e.ModifyDatetime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.Property(e => e.ShortName).HasMaxLength(50);

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.DataDictionaryCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .HasConstraintName("FK_Dic_User1");

                entity.HasOne(d => d.ModifyUser)
                    .WithMany(p => p.DataDictionaryModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    .HasConstraintName("FK_Dic_User2");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.Dpath)
                    .HasMaxLength(3000)
                    .HasColumnName("DPath");

                entity.Property(e => e.Dsort).HasColumnName("DSort");

                entity.Property(e => e.Dstatus).HasColumnName("DStatus");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.No).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(1000);

                entity.Property(e => e.ShortName).HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Department_DataDictionary1");
            });

            modelBuilder.Entity<DeptMain>(entity =>
            {
                entity.ToTable("DeptMain");

                entity.Property(e => e.Account).HasMaxLength(50);

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.BankName).HasMaxLength(100);

                entity.Property(e => e.Fax).HasMaxLength(20);

                entity.Property(e => e.InvoiceName).HasMaxLength(100);

                entity.Property(e => e.LawPerson).HasMaxLength(100);

                entity.Property(e => e.TaxId).HasMaxLength(50);

                entity.Property(e => e.TelePhone).HasMaxLength(50);

                entity.Property(e => e.ZipCode).HasMaxLength(16);

                entity.HasOne(d => d.Dept)
                    .WithMany(p => p.DeptMains)
                    .HasForeignKey(d => d.DeptId)
                    .HasConstraintName("FK_DeptMain_Department");
            });

            modelBuilder.Entity<DisputeManager>(entity =>
            {
                entity.ToTable("DisputeManager");

                entity.Property(e => e.Amount).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CompInfo)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.DisputeType).HasColumnName("disputeType");

                entity.Property(e => e.Dstate).HasColumnName("DState");

                entity.Property(e => e.HandDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Comp)
                    .WithMany(p => p.DisputeManagers)
                    .HasForeignKey(d => d.CompId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DisputeManager_Company");

                entity.HasOne(d => d.Cont)
                    .WithMany(p => p.DisputeManagers)
                    .HasForeignKey(d => d.ContId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DisputeManager_ContractInfo");

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.DisputeManagerCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .HasConstraintName("FK_DisputeManager_UserInfor1");

                entity.HasOne(d => d.DisputeTypeNavigation)
                    .WithMany(p => p.DisputeManagerDisputeTypeNavigations)
                    .HasForeignKey(d => d.DisputeType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DisputeManager_DataDictionary1");

                entity.HasOne(d => d.Handler)
                    .WithMany(p => p.DisputeManagerHandlers)
                    .HasForeignKey(d => d.HandlerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DisputeManager_UserInfor");

                entity.HasOne(d => d.UrgentNavigation)
                    .WithMany(p => p.DisputeManagerUrgentNavigations)
                    .HasForeignKey(d => d.Urgent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DisputeManager_DataDictionary");
            });

            modelBuilder.Entity<FlowTemp>(entity =>
            {
                entity.ToTable("FlowTemp");

                entity.Property(e => e.CategoryIds).HasMaxLength(2000);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.DeptIds).HasMaxLength(2000);

                entity.Property(e => e.FlowItems).HasMaxLength(1000);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.FlowTemps)
                    .HasForeignKey(d => d.CreateUserId)
                    .HasConstraintName("FK_FlowTemp_UserInfor");
            });

            modelBuilder.Entity<FlowTempHist>(entity =>
            {
                entity.ToTable("FlowTempHist");

                entity.Property(e => e.CategoryIds).HasMaxLength(2000);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.DeptIds).HasMaxLength(2000);

                entity.Property(e => e.FlowItems).HasMaxLength(1000);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.FlowTempHists)
                    .HasForeignKey(d => d.CreateUserId)
                    .HasConstraintName("FK_FlowTempHist_UserInfor");
            });

            modelBuilder.Entity<FlowTempNode>(entity =>
            {
                entity.ToTable("FlowTempNode");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.StrId).HasMaxLength(50);
            });

            modelBuilder.Entity<FlowTempNodeHist>(entity =>
            {
                entity.ToTable("FlowTempNodeHist");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.StrId).HasMaxLength(50);
            });

            modelBuilder.Entity<FlowTempNodeInfo>(entity =>
            {
                entity.ToTable("FlowTempNodeInfo");

                entity.Property(e => e.Max).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.Min).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.NodeStrId).HasMaxLength(50);

                entity.Property(e => e.UsIds).HasMaxLength(50);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.FlowTempNodeInfos)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_FlowTempNodeInfo_GroupInfo");
            });

            modelBuilder.Entity<FlowTempNodeInfoHist>(entity =>
            {
                entity.ToTable("FlowTempNodeInfoHist");

                entity.Property(e => e.Max).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.Min).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.NodeStrId).HasMaxLength(50);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.FlowTempNodeInfoHists)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_FlowTempNodeInfoHist_GroupInfo");
            });

            modelBuilder.Entity<GroupInfo>(entity =>
            {
                entity.ToTable("GroupInfo");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Remark).HasMaxLength(1000);
            });

            modelBuilder.Entity<GroupUser>(entity =>
            {
                entity.ToTable("GroupUser");
            });

            modelBuilder.Entity<Inquirer>(entity =>
            {
                entity.ToTable("Inquirer");

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Position)
                    .HasMaxLength(20)
                    .HasColumnName("position");

                entity.HasOne(d => d.Inquiry)
                    .WithMany(p => p.Inquirers)
                    .HasForeignKey(d => d.InquiryId)
                    .HasConstraintName("FK_Inquirer_Inquiry");
            });

            modelBuilder.Entity<Inquiry>(entity =>
            {
                entity.ToTable("Inquiry");

                entity.Property(e => e.ProjectNumber).HasMaxLength(20);

                entity.Property(e => e.Recorder).HasColumnName("recorder");

                entity.Property(e => e.Sites).HasMaxLength(20);

                entity.Property(e => e.TheWinningConditions).HasMaxLength(1000);

                entity.Property(e => e.Times).HasColumnType("datetime");

                entity.Property(e => e.UsefulLife).HasColumnType("datetime");

                entity.Property(e => e.WfCurrNodeName).HasMaxLength(50);

                entity.Property(e => e.Zje).HasColumnType("decimal(28, 6)");

                entity.HasOne(d => d.ContractExecuteBranchNavigation)
                    .WithMany(p => p.Inquiries)
                    .HasForeignKey(d => d.ContractExecuteBranch)
                    .HasConstraintName("FK_Inquiry_Department");

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.Inquiries)
                    .HasForeignKey(d => d.CreateUserId)
                    .HasConstraintName("FK_Inquiry_UserInfor");

                entity.HasOne(d => d.ProjectNameNavigation)
                    .WithMany(p => p.Inquiries)
                    .HasForeignKey(d => d.ProjectName)
                    .HasConstraintName("FK_Inquiry_Inquiry");

                entity.HasOne(d => d.ZbdwNavigation)
                    .WithMany(p => p.Inquiries)
                    .HasForeignKey(d => d.Zbdw)
                    .HasConstraintName("FK_Inquiry_Company");
            });

            modelBuilder.Entity<InquiryAttachment>(entity =>
            {
                entity.ToTable("InquiryAttachment");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(500);

                entity.Property(e => e.FolderName).HasMaxLength(50);

                entity.Property(e => e.GuidFileName).HasMaxLength(50);

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Path).HasMaxLength(500);

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.HasOne(d => d.Cont)
                    .WithMany(p => p.InquiryAttachments)
                    .HasForeignKey(d => d.ContId)
                    .HasConstraintName("FK_InquiryAttachment_Inquiry");
            });

            modelBuilder.Entity<Interviewpeople>(entity =>
            {
                entity.Property(e => e.Kij)
                    .HasMaxLength(50)
                    .HasColumnName("kij");

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Position)
                    .HasMaxLength(20)
                    .HasColumnName("position");

                entity.HasOne(d => d.Ques)
                    .WithMany(p => p.Interviewpeople)
                    .HasForeignKey(d => d.QuesId)
                    .HasConstraintName("FK_Interviewpeople_Questioning");
            });

            modelBuilder.Entity<InvoDescription>(entity =>
            {
                entity.ToTable("InvoDescription");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.Total).HasColumnType("decimal(28, 6)");
            });

            modelBuilder.Entity<InvoFile>(entity =>
            {
                entity.ToTable("InvoFile");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(500);

                entity.Property(e => e.FileSize).HasMaxLength(100);

                entity.Property(e => e.FolderName).HasMaxLength(100);

                entity.Property(e => e.GuidFileName).HasMaxLength(500);

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Path)
                    .HasMaxLength(1000)
                    .IsFixedLength();

                entity.Property(e => e.Remark).HasMaxLength(2000);
            });

            modelBuilder.Entity<InvoiceCheck>(entity =>
            {
                entity.ToTable("InvoiceCheck");

                entity.Property(e => e.AmountMoney).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.ConfirmDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("ConfirmDateTIme");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.SettlementDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<LoginLog>(entity =>
            {
                entity.ToTable("LoginLog");

                entity.Property(e => e.CreateDatetime).HasColumnType("datetime");

                entity.Property(e => e.LoginIp).HasMaxLength(20);

                entity.Property(e => e.RequestNetIp).HasMaxLength(20);

                entity.HasOne(d => d.LoginUser)
                    .WithMany(p => p.LoginLogs)
                    .HasForeignKey(d => d.LoginUserId)
                    .HasConstraintName("FK_LoginLog_UserInfor");
            });

            modelBuilder.Entity<OpenBid>(entity =>
            {
                entity.ToTable("OpenBid");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Personnel).HasColumnName("personnel");

                entity.Property(e => e.TotalPrices).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.Unit).HasColumnName("unit");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(28, 6)");

                entity.HasOne(d => d.Ques)
                    .WithMany(p => p.OpenBids)
                    .HasForeignKey(d => d.QuesId)
                    .HasConstraintName("FK_OpenBid_OpenBid");

                entity.HasOne(d => d.UnitNavigation)
                    .WithMany(p => p.OpenBids)
                    .HasForeignKey(d => d.Unit)
                    .HasConstraintName("FK_OpenBid_Company");
            });

            modelBuilder.Entity<OpenTenderCondition>(entity =>
            {
                entity.ToTable("OpenTenderCondition");

                entity.Property(e => e.Lxfs).HasMaxLength(100);

                entity.Property(e => e.Lxr).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Personnel).HasColumnName("personnel");

                entity.Property(e => e.TotalPrices).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.Unit).HasColumnName("unit");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(28, 6)");

                entity.HasOne(d => d.Lnquiry)
                    .WithMany(p => p.OpenTenderConditions)
                    .HasForeignKey(d => d.LnquiryId)
                    .HasConstraintName("FK_OpenTenderCondition_Inquiry");

                entity.HasOne(d => d.UnitNavigation)
                    .WithMany(p => p.OpenTenderConditions)
                    .HasForeignKey(d => d.Unit)
                    .HasConstraintName("FK_OpenTenderCondition_OpenTenderCondition");
            });

            modelBuilder.Entity<OpeningSituationInfor>(entity =>
            {
                entity.ToTable("OpeningSituationInfor");

                entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");

                entity.Property(e => e.OpenSituationName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.Uitprice).HasColumnType("decimal(28, 6)");

                entity.HasOne(d => d.Lnquiry)
                    .WithMany(p => p.OpeningSituationInfors)
                    .HasForeignKey(d => d.LnquiryId)
                    .HasConstraintName("FK_OpeningSituationInfor_OpeningSituationInfor");

                entity.HasOne(d => d.Tender)
                    .WithMany(p => p.OpeningSituationInfors)
                    .HasForeignKey(d => d.TenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OpeningSituationInfor_TenderInfor1");

                entity.HasOne(d => d.UnitNavigation)
                    .WithMany(p => p.OpeningSituationInfors)
                    .HasForeignKey(d => d.Unit)
                    .HasConstraintName("FK_OpeningSituationInfor_TenderInfor");
            });

            modelBuilder.Entity<OptionLog>(entity =>
            {
                entity.ToTable("OptionLog");

                entity.Property(e => e.ActionName).HasMaxLength(100);

                entity.Property(e => e.ActionTitle).HasMaxLength(500);

                entity.Property(e => e.ControllerName).HasMaxLength(200);

                entity.Property(e => e.CreateDatetime).HasColumnType("datetime");

                entity.Property(e => e.ExecResult).HasMaxLength(50);

                entity.Property(e => e.RequestData).HasMaxLength(4000);

                entity.Property(e => e.RequestIp).HasMaxLength(20);

                entity.Property(e => e.RequestNetIp).HasMaxLength(20);

                entity.Property(e => e.RequestUrl).HasMaxLength(500);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OptionLogs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_OptionLog_UserInfor");
            });

            modelBuilder.Entity<PlanFinnCheck>(entity =>
            {
                entity.ToTable("PlanFinnCheck");

                entity.Property(e => e.AmountMoney).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.ConfirmDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("ConfirmDateTIme");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.SettlementDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProjAttachment>(entity =>
            {
                entity.ToTable("ProjAttachment");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(500);

                entity.Property(e => e.FileSize).HasMaxLength(100);

                entity.Property(e => e.FolderName).HasMaxLength(100);

                entity.Property(e => e.GuidFileName).HasMaxLength(500);

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Path)
                    .HasMaxLength(1000)
                    .IsFixedLength();

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProjAttachments)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_ProjAttachment_DataDictionary");

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.ProjAttachments)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjAttachment_UserInfor1");
            });

            modelBuilder.Entity<ProjDescription>(entity =>
            {
                entity.ToTable("ProjDescription");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Pitem).HasMaxLength(500);

                entity.Property(e => e.ProjContent).HasMaxLength(2000);

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.ProjDescriptions)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjDescription_UserInfor1");
            });

            modelBuilder.Entity<ProjSchedule>(entity =>
            {
                entity.ToTable("ProjSchedule");

                entity.Property(e => e.ActualBeginDateTime).HasColumnType("datetime");

                entity.Property(e => e.ActualCompleteDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Pitem).HasMaxLength(500);

                entity.Property(e => e.PlanBeginDateTime).HasColumnType("datetime");

                entity.Property(e => e.PlanCompleteDateTime).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.ProjSchedules)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjSchedule_UserInfor1");
            });

            modelBuilder.Entity<ProjectManager>(entity =>
            {
                entity.ToTable("ProjectManager");

                entity.Property(e => e.ActualBeginDateTime).HasColumnType("datetime");

                entity.Property(e => e.ActualCompleteDateTime).HasColumnType("datetime");

                entity.Property(e => e.BudgetPayAmountMoney).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.BugetCollectAmountMoney).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.Code).HasMaxLength(200);

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.PlanBeginDateTime).HasColumnType("datetime");

                entity.Property(e => e.PlanCompleteDateTime).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.Property(e => e.Reserve1).HasMaxLength(200);

                entity.Property(e => e.Reserve2).HasMaxLength(200);

                entity.Property(e => e.WfCurrNodeName).HasMaxLength(50);

                entity.HasOne(d => d.BudgetCollectCurrency)
                    .WithMany(p => p.ProjectManagerBudgetCollectCurrencies)
                    .HasForeignKey(d => d.BudgetCollectCurrencyId)
                    .HasConstraintName("FK_ProjectManager_CurrencyManager1");

                entity.HasOne(d => d.BudgetPayCurrency)
                    .WithMany(p => p.ProjectManagerBudgetPayCurrencies)
                    .HasForeignKey(d => d.BudgetPayCurrencyId)
                    .HasConstraintName("FK_ProjectManager_CurrencyManager2");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProjectManagers)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectManager_DataDictionary");

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.ProjectManagerCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectManager_UserInfor1");

                entity.HasOne(d => d.ModifyUser)
                    .WithMany(p => p.ProjectManagerModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectManager_UserInfor2");

                entity.HasOne(d => d.PrincipalUser)
                    .WithMany(p => p.ProjectManagerPrincipalUsers)
                    .HasForeignKey(d => d.PrincipalUserId)
                    .HasConstraintName("FK_ProjectManager_UserInfor3");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.ToTable("Province");

                entity.Property(e => e.DisplayName).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Questioning>(entity =>
            {
                entity.ToTable("Questioning");

                entity.Property(e => e.ProjectNumber).HasMaxLength(20);

                entity.Property(e => e.Recorder).HasColumnName("recorder");

                entity.Property(e => e.Sites).HasMaxLength(20);

                entity.Property(e => e.TheWinningConditions).HasMaxLength(1000);

                entity.Property(e => e.Times).HasColumnType("datetime");

                entity.Property(e => e.UsefulLife).HasColumnType("datetime");

                entity.Property(e => e.WfCurrNodeName).HasMaxLength(50);

                entity.Property(e => e.Zje).HasColumnType("decimal(28, 6)");

                entity.HasOne(d => d.ContractExecuteBranchNavigation)
                    .WithMany(p => p.Questionings)
                    .HasForeignKey(d => d.ContractExecuteBranch)
                    .HasConstraintName("FK_Questioning_Department");

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.Questionings)
                    .HasForeignKey(d => d.CreateUserId)
                    .HasConstraintName("FK_Questioning_UserInfor");

                entity.HasOne(d => d.ProjectNameNavigation)
                    .WithMany(p => p.Questionings)
                    .HasForeignKey(d => d.ProjectName)
                    .HasConstraintName("FK_Questioning_ProjectManager");

                entity.HasOne(d => d.ZbdwNavigation)
                    .WithMany(p => p.Questionings)
                    .HasForeignKey(d => d.Zbdw)
                    .HasConstraintName("FK_Questioning_Company");
            });

            modelBuilder.Entity<QuestioningAttachment>(entity =>
            {
                entity.ToTable("QuestioningAttachment");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(500);

                entity.Property(e => e.FolderName).HasMaxLength(50);

                entity.Property(e => e.GuidFileName).HasMaxLength(50);

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Path).HasMaxLength(500);

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.HasOne(d => d.Cont)
                    .WithMany(p => p.QuestioningAttachments)
                    .HasForeignKey(d => d.ContId)
                    .HasConstraintName("FK_questioningAttachment_Questioning");
            });

            modelBuilder.Entity<Remind>(entity =>
            {
                entity.ToTable("Remind");

                entity.Property(e => e.CustomName).HasMaxLength(150);

                entity.Property(e => e.Item).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(150);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.CreateDatetime).HasColumnType("datetime");

                entity.Property(e => e.ModifyDatetime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.No).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(1500);

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.RoleCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Role_UserInfor1");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_Role_Department");

                entity.HasOne(d => d.ModifyUser)
                    .WithMany(p => p.RoleModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Role_UserInfor2");
            });

            modelBuilder.Entity<RoleModule>(entity =>
            {
                entity.ToTable("RoleModule");
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.ToTable("RolePermission");

                entity.Property(e => e.DeptIds).HasMaxLength(1000);

                entity.Property(e => e.FuncCode).HasMaxLength(150);
            });

            modelBuilder.Entity<ScheduleDetail>(entity =>
            {
                entity.ToTable("ScheduleDetail");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.PddateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("PDDateTime");

                entity.Property(e => e.Pdescription)
                    .HasColumnType("text")
                    .HasColumnName("PDescription");

                entity.Property(e => e.ScheduleName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.ScheduleSerNavigation)
                    .WithMany(p => p.ScheduleDetails)
                    .HasForeignKey(d => d.ScheduleSer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScheduleDetail_ScheduleManagement");
            });

            modelBuilder.Entity<ScheduleDetailAttachment>(entity =>
            {
                entity.ToTable("ScheduleDetailAttachment");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(500);

                entity.Property(e => e.FolderName).HasMaxLength(50);

                entity.Property(e => e.GuidFileName).HasMaxLength(50);

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Path).HasMaxLength(500);

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.HasOne(d => d.Scheduled)
                    .WithMany(p => p.ScheduleDetailAttachments)
                    .HasForeignKey(d => d.ScheduledId)
                    .HasConstraintName("FK_ScheduleDetailAttachment_ScheduleDetail");
            });

            modelBuilder.Entity<ScheduleList>(entity =>
            {
                entity.ToTable("ScheduleList");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.Descriptionms).HasMaxLength(1000);

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Myjdtime).HasColumnType("datetime");

                entity.Property(e => e.ScheduleName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.ScheduleLists)
                    .HasForeignKey(d => d.ScheduleId)
                    .HasConstraintName("FK_ScheduleList_ScheduleManagement");
            });

            modelBuilder.Entity<ScheduleManagement>(entity =>
            {
                entity.ToTable("ScheduleManagement");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Gzdatetime).HasColumnType("datetime");

                entity.Property(e => e.JhCompleteDateTime).HasColumnType("datetime");

                entity.Property(e => e.JhCreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Myjdtime).HasColumnType("datetime");

                entity.Property(e => e.ScheduleName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ScheduleSer)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SjCompleteDateTime).HasColumnType("datetime");

                entity.Property(e => e.SjCreateDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<ScheduleManagementAttachment>(entity =>
            {
                entity.ToTable("ScheduleManagementAttachment");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(500);

                entity.Property(e => e.FolderName).HasMaxLength(50);

                entity.Property(e => e.GuidFileName).HasMaxLength(50);

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Path).HasMaxLength(500);

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.HasOne(d => d.Schedulem)
                    .WithMany(p => p.ScheduleManagementAttachments)
                    .HasForeignKey(d => d.SchedulemId)
                    .HasConstraintName("FK_ScheduleManagementAttachment_ScheduleManagement");
            });

            modelBuilder.Entity<SealManager>(entity =>
            {
                entity.ToTable("SealManager");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.EnabledDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(1000);

                entity.Property(e => e.SealCode).HasMaxLength(100);

                entity.Property(e => e.SealName).HasMaxLength(100);
            });

            modelBuilder.Entity<SuccessfulBidderLable>(entity =>
            {
                entity.ToTable("SuccessfulBidderLable");

                entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");

                entity.Property(e => e.SuccTotalPrice).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.SuccUitprice).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.SuccessName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.SuccessUnti)
                    .WithMany(p => p.SuccessfulBidderLables)
                    .HasForeignKey(d => d.SuccessUntiId)
                    .HasConstraintName("FK_SuccessfulBidderLable_TenderInfor");

                entity.HasOne(d => d.Tender)
                    .WithMany(p => p.SuccessfulBidderLables)
                    .HasForeignKey(d => d.TenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SuccessfulBidderLable_TenderInfor1");
            });

            modelBuilder.Entity<SysEmail>(entity =>
            {
                entity.ToTable("SysEmail");

                entity.Property(e => e.MailPwd).HasMaxLength(50);

                entity.Property(e => e.SendNickname).HasMaxLength(50);

                entity.Property(e => e.SenderMail).HasMaxLength(100);

                entity.Property(e => e.ServiceName).HasMaxLength(50);
            });

            modelBuilder.Entity<SysField>(entity =>
            {
                entity.ToTable("SysField");

                entity.Property(e => e.Fname).HasMaxLength(200);

                entity.Property(e => e.Isqy).HasColumnName("ISqy");

                entity.Property(e => e.Lable).HasMaxLength(50);

                entity.Property(e => e.SelData).HasMaxLength(2000);
            });

            modelBuilder.Entity<SysFunction>(entity =>
            {
                entity.ToTable("SysFunction");

                entity.Property(e => e.Fcode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.HasOne(d => d.Mode)
                    .WithMany(p => p.SysFunctions)
                    .HasForeignKey(d => d.ModeId)
                    .HasConstraintName("FK_SysFunction_SysModel");
            });

            modelBuilder.Entity<SysModel>(entity =>
            {
                entity.ToTable("SysModel");

                entity.Property(e => e.ActionName).HasMaxLength(50);

                entity.Property(e => e.AreaName).HasMaxLength(20);

                entity.Property(e => e.ControllerName).HasMaxLength(50);

                entity.Property(e => e.CreateDatetime).HasColumnType("datetime");

                entity.Property(e => e.Ico).HasMaxLength(50);

                entity.Property(e => e.ModifyDatetime).HasColumnType("datetime");

                entity.Property(e => e.Mpath)
                    .HasMaxLength(200)
                    .HasColumnName("MPath");

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.No).HasMaxLength(20);

                entity.Property(e => e.Pid).HasColumnName("PId");

                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.Property(e => e.RequestUrl).HasMaxLength(200);

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.SysModelCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .HasConstraintName("FK_SysModel_UserInfor1");

                entity.HasOne(d => d.ModifyUser)
                    .WithMany(p => p.SysModelModifyUsers)
                    .HasForeignKey(d => d.ModifyUserId)
                    .HasConstraintName("FK_SysModel_UserInfor2");
            });

            modelBuilder.Entity<TempNodeArea>(entity =>
            {
                entity.ToTable("TempNodeArea");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.StrId).HasMaxLength(50);
            });

            modelBuilder.Entity<TempNodeAreaHist>(entity =>
            {
                entity.ToTable("TempNodeAreaHist");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.StrId).HasMaxLength(50);
            });

            modelBuilder.Entity<TempNodeLine>(entity =>
            {
                entity.ToTable("TempNodeLine");

                entity.Property(e => e.From).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(2000);

                entity.Property(e => e.StrId).HasMaxLength(50);

                entity.Property(e => e.To).HasMaxLength(50);
            });

            modelBuilder.Entity<TempNodeLineHist>(entity =>
            {
                entity.ToTable("TempNodeLineHist");

                entity.Property(e => e.From).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(2000);

                entity.Property(e => e.StrId).HasMaxLength(50);

                entity.Property(e => e.To).HasMaxLength(50);
            });

            modelBuilder.Entity<TenderAttachment>(entity =>
            {
                entity.ToTable("TenderAttachment");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(500);

                entity.Property(e => e.FolderName).HasMaxLength(50);

                entity.Property(e => e.GuidFileName).HasMaxLength(50);

                entity.Property(e => e.ModifyDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Path).HasMaxLength(500);

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.HasOne(d => d.Cont)
                    .WithMany(p => p.TenderAttachments)
                    .HasForeignKey(d => d.ContId)
                    .HasConstraintName("FK_TenderAttachment_TenderInfor");
            });

            modelBuilder.Entity<TenderInfor>(entity =>
            {
                entity.ToTable("TenderInfor");

                entity.Property(e => e.Iocation)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");

                entity.Property(e => e.ProjectNo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("ProjectNO");

                entity.Property(e => e.TenderDate).HasColumnType("datetime");

                entity.Property(e => e.TenderExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.WfCurrNodeName).HasMaxLength(50);

                entity.Property(e => e.WinningConditions).HasMaxLength(1000);

                entity.Property(e => e.Zje).HasColumnType("decimal(28, 6)");

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.TenderInfors)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TenderInfor_UserInfor");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.TenderInfors)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TenderInfor_ProjectManager");

                entity.HasOne(d => d.ZbdwNavigation)
                    .WithMany(p => p.TenderInfors)
                    .HasForeignKey(d => d.Zbdw)
                    .HasConstraintName("FK_TenderInfor_Company");
            });

            modelBuilder.Entity<TendererNameLabel>(entity =>
            {
                entity.ToTable("TendererNameLabel");

                entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");

                entity.Property(e => e.Psition)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TeNameLabel)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Tender)
                    .WithMany(p => p.TendererNameLabels)
                    .HasForeignKey(d => d.TenderId)
                    .HasConstraintName("FK_TendererNameLabel_TenderInfor");
            });

            modelBuilder.Entity<TheWinningUnit>(entity =>
            {
                entity.ToTable("TheWinningUnit");

                entity.Property(e => e.BidPrice).HasColumnType("decimal(28, 2)");

                entity.Property(e => e.BidPrices).HasColumnType("decimal(28, 2)");

                entity.Property(e => e.Lxfs).HasMaxLength(50);

                entity.Property(e => e.Lxr).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.HasOne(d => d.Lnquiry)
                    .WithMany(p => p.TheWinningUnits)
                    .HasForeignKey(d => d.LnquiryId)
                    .HasConstraintName("FK_TheWinningUnit_Inquiry");

                entity.HasOne(d => d.WinningUnitNavigation)
                    .WithMany(p => p.TheWinningUnits)
                    .HasForeignKey(d => d.WinningUnit)
                    .HasConstraintName("FK_TheWinningUnit_OpenTenderCondition");
            });

            modelBuilder.Entity<UserInfor>(entity =>
            {
                entity.ToTable("UserInfor");

                entity.Property(e => e.Address).HasMaxLength(150);

                entity.Property(e => e.CreateDatetime).HasColumnType("datetime");

                entity.Property(e => e.DisplyName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.EntryDatetime).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(20);

                entity.Property(e => e.IdNo).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(20);

                entity.Property(e => e.Minfo).HasMaxLength(150);

                entity.Property(e => e.Mobile).HasMaxLength(50);

                entity.Property(e => e.ModifyDatetime).HasColumnType("datetime");

                entity.Property(e => e.Msystem).HasMaxLength(20);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.PhName).HasMaxLength(50);

                entity.Property(e => e.PhPath).HasMaxLength(500);

                entity.Property(e => e.Pwd)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(2000);

                entity.Property(e => e.Tel).HasMaxLength(50);

                entity.Property(e => e.Ustart).HasColumnName("UStart");

                entity.Property(e => e.WxCode).HasMaxLength(50);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.UserInfors)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_UserInfor_Department");
            });

            modelBuilder.Entity<UserModule>(entity =>
            {
                entity.ToTable("UserModule");
            });

            modelBuilder.Entity<UserPermission>(entity =>
            {
                entity.ToTable("UserPermission");

                entity.Property(e => e.DeptIds).HasMaxLength(1000);

                entity.Property(e => e.FuncCode).HasMaxLength(150);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");
            });

            modelBuilder.Entity<WinningInq>(entity =>
            {
                entity.ToTable("WinningInq");

                entity.Property(e => e.GuidFileName).HasMaxLength(500);

                entity.Property(e => e.SourceFileName).HasMaxLength(500);

                entity.Property(e => e.WinningModel)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.WinningName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.WinningQuantity).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.WinningTotalPrice).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.WinningUitprice).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.WinningUntiId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Inq)
                    .WithMany(p => p.WinningInqs)
                    .HasForeignKey(d => d.Inqid)
                    .HasConstraintName("FK_WinningInq_Inquiry");
            });

            modelBuilder.Entity<WinningItem>(entity =>
            {
                entity.ToTable("WinningItem");

                entity.Property(e => e.GuidFileName).HasMaxLength(500);

                entity.Property(e => e.SourceFileName).HasMaxLength(500);

                entity.Property(e => e.WinningModel)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.WinningName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.WinningQuantity).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.WinningTotalPrice).HasColumnType("decimal(28, 0)");

                entity.Property(e => e.WinningUitprice).HasColumnType("decimal(28, 0)");

                entity.Property(e => e.WinningUntiId)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<WinningQue>(entity =>
            {
                entity.ToTable("WinningQue");

                entity.Property(e => e.GuidFileName).HasMaxLength(500);

                entity.Property(e => e.SourceFileName).HasMaxLength(500);

                entity.Property(e => e.WinningModel)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.WinningName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.WinningQuantity).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.WinningTotalPrice).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.WinningUitprice).HasColumnType("decimal(28, 6)");

                entity.Property(e => e.WinningUntiId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Que)
                    .WithMany(p => p.WinningQues)
                    .HasForeignKey(d => d.QueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WinningQue_Questioning");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
