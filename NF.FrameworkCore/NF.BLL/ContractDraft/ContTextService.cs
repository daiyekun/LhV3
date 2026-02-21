using NF.Common.Utility;
using NF.Model.Models;
using NF.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using NF.ViewModel.Models.Utility;
using NF.ViewModel.Extend.Enums;
using Microsoft.EntityFrameworkCore;
using NF.AutoMapper;
using NF.ViewModel;
using NF.BLL.Extend;
using AutoMapper;

namespace NF.BLL
{
    /// <summary>
    /// 合同文本
    /// </summary>
    public partial class ContTextService
    {
        private IMapper _IMapper;
        /// <summary>
        /// 查询分页
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<ContTextViewDTO> GetList<s>(PageInfo<ContText> pageInfo, Expression<Func<ContText, bool>> whereLambda, Expression<Func<ContText, s>> orderbyLambda, bool isAsc)
        {
            //var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var tempquery = Db.Set<ContText>().Include(a=>a.Template).AsTracking().Where(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<ContText>)) {
                tempquery = tempquery.Skip<ContText>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<ContText>(pageInfo.PageSize);
                    }
            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            CategoryId = a.CategoryId,
                            Remark = a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            TemplateId = a.TemplateId,//合同模板ID
                            IsFromTemp = a.IsFromTemp,//文本来源
                            //CreateUserDisplyName =  //a.CreateUser.DisplyName,
                            Stage = a.Stage,//阶段
                            Path = a.Path,
                            FileName = a.FileName,
                            Versions = a.Versions,//版本
                            ModifyDateTime = a.ModifyDateTime,//变更日期
                            ExtenName = a.ExtenName,//扩展名称
                            GuidFileName = a.GuidFileName,//Guid文件名称
                            ContId = a.ContId,
                            TempName = a.Template==null?"": a.Template.Name,//模板名称
                        };
            var local = from a in query.AsEnumerable()
                        select new ContTextViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            CategoryId = a.CategoryId,
                            Remark = a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            TemplateId = a.TemplateId,//合同模板ID
                            IsFromTxt = EmunUtility.GetDesc(typeof(SourceTxtEnum), a.IsFromTemp ?? -1),//文本来源
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId),
                            Stagetxt = EmunUtility.GetDesc(typeof(StageTxtEnum), a.Stage ?? -1),//阶段
                            ContTxtType = this.Db.GetRedisDataDictionaryValue(a.CategoryId ?? 0, DataDictionaryEnum.ContTxtType),
                            //DataDicUtility.GetDicValueToRedis(a.CategoryId, DataDictionaryEnum.ContTxtType),//文本类别
                            Path = a.Path,
                            FileName = a.FileName,
                            Versions = a.Versions,//版本
                            ModifyDateTime = a.ModifyDateTime,//变更日期
                            ExtenName = a.ExtenName,//扩展名称
                            GuidFileName = a.GuidFileName,//Guid文件名称
                            ContId = a.ContId,
                            TempName = a.TempName,//模板名称
                            IsFromTemp = a.IsFromTemp ?? 0,//文本来源

                        };
            return new LayPageInfo<ContTextViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };


        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="Ids">需要删除的Ids集合</param>
        /// <returns>受影响行数</returns>
        public int Delete(string Ids)
        {
            string sqlstr = "update ContText set IsDelete=1 where Id in(" + Ids + ")";
            return ExecuteSqlCommand(sqlstr);
        }

        

        /// <summary>
        /// 查看或者修改
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public ContTextViewDTO ShowView(int Id)
        {
            var query = from a in _ContTextSet.AsNoTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            CategoryId = a.CategoryId,
                            Remark = a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            TemplateId = a.TemplateId,//合同模板ID
                            IsFromTemp = a.IsFromTemp,//文本来源 = a.IsFromTemp,//文本来源
                            //CreateUserDisplyName =  //a.CreateUser.DisplyName,
                            Stage = a.Stage,//阶段
                            Path = a.Path,
                            FileName = a.FileName,
                            Versions = a.Versions,//版本
                            ModifyDateTime = a.ModifyDateTime,//变更日期
                            ExtenName = a.ExtenName,//扩展名称
                            GuidFileName = a.GuidFileName,//Guid文件名称
                            FolderName = a.FolderName,//文件夹
                            TempName = a.Template.Name,//模板名称
                        };
            var local = from a in query.AsEnumerable()
                        select new ContTextViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            CategoryId = a.CategoryId,
                            Remark = a.Remark,
                            CreateDateTime = a.CreateDateTime,
                            CreateUserId = a.CreateUserId,
                            TemplateId = a.TemplateId,//合同模板ID
                            IsFromTxt = EmunUtility.GetDesc(typeof(SourceTxtEnum), a.IsFromTemp ?? -1),//文本来源
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId),
                            Stagetxt = EmunUtility.GetDesc(typeof(StageTxtEnum), a.Stage ?? -1),//阶段
                            ContTxtType = this.Db.GetRedisDataDictionaryValue(a.CategoryId ?? 0, DataDictionaryEnum.ContTxtType),
                           // DataDicUtility.GetDicValueToRedis(a.CategoryId, DataDictionaryEnum.ContTxtType),//文本类别
                            Path = a.Path,
                            FileName = a.FileName,
                            Versions = a.Versions,//版本
                            ModifyDateTime = a.ModifyDateTime,//变更日期
                            ExtenName = a.ExtenName,//扩展名称
                            GuidFileName = a.GuidFileName,//Guid文件名称
                            FolderName = a.FolderName,//文件夹
                            TempName = a.TempName,//模板名称
                            IsFromTemp = a.IsFromTemp,

                        };
            return local.FirstOrDefault();
        }



        /// <summary>
        /// 保存合同文本
        /// </summary>
        /// <param name="contText">合同文本对象</param>
        public Dictionary<string, int> AddSave(ContText contText)
        {
            if (contText.IsFromTemp == (byte)SourceTxtEnum.Upload)
            {
                var inof = Add(contText);
                return SaveContTextHistory(inof);
            }
            else
            {
                return AddQiCaoSave(contText);
            }


        }

        #region 起草时的文本保存-暂且和上传分开

        /// <summary>
        /// 起草时保存
        /// </summary>
        private Dictionary<string, int> AddQiCaoSave(ContText contText)
        {
            var dic = new Dictionary<string, int>();
            if (!contText.ContId.HasValue)
            {
                contText.ContId = -contText.CreateUserId; //-LoginUtility.GetCurrentUserID();
            }

            contText.Versions = 1;

            var tick = DateTime.Now.Ticks;
            string docName = tick + "-" + contText.Name;

            if (contText.IsFromTemp != (byte)SourceTxtEnum.Upload)
            {
                contText.FolderName = EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), 6);
                contText.Path = $"~/Uploads/{contText.FolderName}/{docName}.docx";
                contText.FileName = $"{docName}.docx";
                contText.GuidFileName = tick.ToString();

            }

            ContractInfo cont = null;
            if (contText.ContId > 0)
            {
                cont = Db.Set<ContractInfo>().AsNoTracking().FirstOrDefault(a => a.Id == contText.ContId);
            }
            if (cont == null)
            {
                contText.Stage = 0;//原始
            }
            else
            {
                int str = cont.ContState;

                int? px = 0;

                var contHist = Db.Set<ContractInfoHistory>()
                    .Where(a => a.ContId == cont.Id)
                    .OrderByDescending(a => a.ModificationTimes)
                    .FirstOrDefault();

                if (contHist != null)
                {
                    contText.ContHisId = contHist.Id;
                    px = contHist.ModificationTimes;
                }

                contText.Stage = (str != (int)ContractState.Execution && px == 0) ? 0 : str == (int)ContractState.Execution ? (++px) : px;
                //contText.STEP_VERSION = 1;
            }

            Add(contText);
            //存新版本
            dic = SaveContTextHistory(contText);




            //if (contText.IsFromTemp == SourceTxtEnum.TempDraft && HistStepState.IsWordEdit)
            //{
            //    //保存2个历史纪录
            //    var hist2 = new WOO_CONT_TEXT_HISTORY();
            //    info.Clone(hist2);
            //    hist2.CONT_TEXT_ID = info.ID;
            //    hist2.STEP_STATE = hist.STEP_STATE;

            //    //原始信息
            //    hist.VERSIONS = "0";
            //    var docName1 = (tick + 1) + "-" + info.NAME;
            //    hist.PATH = String.Format("~/Upload/TextField/{0}.docx", docName1);

            //    context.Insert(hist2);
            //    context.SaveChanges();
            //}
            return dic;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="contText">文本对象</param>
        /// <returns></returns>
        public Dictionary<string, int> UpdateQiCaoSave(ContText info, bool AddHistory)
        {

            HistoryStepState HistStepState = null;
            var dic = new Dictionary<string, int>();
            dic.Add("Id", info.Id);
            #region AddHistory
            if (AddHistory)
            {
                //参考《合同文本历史记录.vsd》
                var IncludeWordEdit = false;
                if (info.IsFromTemp == (int)SourceTxtEnum.TempDraft || info.IsFromTemp == (int)SourceTxtEnum.FreeDraft
                    || info.IsFromTemp== (int)SourceTxtEnum.Upload)
                {
                    IncludeWordEdit = true;
                }
                HistStepState = this.GetHistoryStepState(info, IncludeWordEdit: IncludeWordEdit);


            }

            if (AddHistory)
            {
                //if (info.IsFromTemp != (int)SourceTxtEnum.Upload)
                //{
                    var lastHist = Db.Set<ContTextHistory>()
                        .Where(a => a.ContTxtId == info.Id)
                        .OrderByDescending(a => a.Id)
                        .FirstOrDefault();

                    if (lastHist != null)
                    {
                        var LastHistStepState = new HistoryStepState(lastHist.Stage.ToString());//STEP_STATE
                        if (LastHistStepState.Equals(HistStepState))
                        {
                            AddHistory = false;
                        }
                    }
                //}
            }
            if (AddHistory)
            {
                var version = StringHelper.ConvertToIntNull(info.Versions);
                if (!version.HasValue)
                {
                    info.Versions = 1;
                }
                else
                {
                    info.Versions = (version ?? 0) + 1;
                }
            }
            #endregion

            Update(info);
            //更新合同文本历史表
            if (AddHistory)
            {
                //var hist = new ContTextHistory();
                //info.Clone(hist);
                var hist = _IMapper.Map<ContText, ContTextHistory>(info);// info.ToModel<ContText, ContTextHistory>();
                var oldPath = info.Path;
                if (info.IsFromTemp == (int)SourceTxtEnum.Upload)
                {
                    var tick = DateTime.Now.Ticks;
                    string docName = tick + "-" + info.Name;
                    hist.FolderName = EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), 6);
                    hist.Path = $"Uploads/{info.FolderName}/{docName}.docx";
                    hist.FileName = $"{docName}.docx";
                    hist.GuidFileName = tick.ToString();
                }
                else
                {
                    var tick = DateTime.Now.Ticks;
                    string docName = tick + "-" + info.Name;
                    // hist = String.Format("~/Upload/TextField/{0}.docx", docName);
                    hist.FolderName = EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), 6);
                    hist.Path = $"~/Uploads/{info.FolderName}/{docName}.docx";
                    hist.FileName = $"{docName}.docx";
                    hist.GuidFileName = tick.ToString();
                }

                FileUtility.CopyFile(oldPath, hist.Path);

                hist.ContTxtId = info.Id;
                hist.CreateUserId = info.ModifyUserId;
                hist.CreateDateTime = DateTime.Now;
                hist.ModifyUserId = hist.CreateUserId;
                hist.ModifyDateTime = DateTime.Now;
                //hist.STEP_VERSION = info.STEP_VERSION;
                //STEP_STATE
                // hist.STEP_STATE = HistStepState.ToString();
                Db.Set<ContTextHistory>().Add(hist);
                Db.SaveChanges();
                dic.Add("Hid", hist.Id);
                //context.Insert(hist);
                info.Path = hist.Path;
                info.GuidFileName = hist.GuidFileName;
                info.FileName = hist.FileName;
                Update(info);


            }
            else
            {
                var hist = Db.Set<ContTextHistory>()
                    .Where(a => a.ContId == info.Id)
                    .OrderByDescending(a => a.Id)
                    .FirstOrDefault();

                if (hist == null)
                {
                    hist = new ContTextHistory();
                    hist.ContTxtId = info.Id;
                }
                dic.Add("Hid", hist.Id);
            }

            return dic;

        }

        #endregion

        /// <summary>
        /// 保存合同文本
        /// </summary>
        /// <param name="contText">合同文本对象</param>
        public Dictionary<string, int> UpdateSave(ContText contText)
        {
            Update(contText);
            if (contText.IsFromTemp == (byte)SourceTxtEnum.Upload)
            {

                return SaveContTextHistory(contText);
            }
            else
            {
                return UpdateTempSave(contText);
            }
        }
        /// <summary>
        /// 模板起草
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, int> UpdateTempSave(ContText contText)
        {
            var dic = new Dictionary<string, int>();
            dic.Add("Id", contText.Id);
            //var txtinfo = Db.Set<ContText>().Find(contText.Id);
            //txtinfo.ModifyUserId = contText.ModifyUserId;
            //txtinfo.ModifyDateTime = contText.ModifyDateTime;
            //txtinfo.Versions = contText.Versions;

            var hist = _IMapper.Map<ContText, ContTextHistory>(contText);// contText.ToModel<ContText, ContTextHistory>();
            var oldPath = contText.Path;
            var tick = DateTime.Now.Ticks;
            string docName = tick + "-" + contText.Name;
            hist.FolderName = EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), 6);
            hist.Path = $"~/Uploads/{ hist.FolderName}/{docName}.docx";
            hist.FileName = $"{docName}.docx";
            hist.GuidFileName = tick.ToString();
            FileUtility.CopyFile(oldPath, hist.Path);
            hist.ContTxtId = contText.Id;
            hist.CreateUserId = contText.ModifyUserId;
            hist.CreateDateTime = DateTime.Now;
            hist.ModifyUserId = hist.CreateUserId;
            hist.ModifyDateTime = DateTime.Now;
            Db.Set<ContTextHistory>().Add(hist);
            Db.SaveChanges();
            dic.Add("Hid", hist.Id);

            return dic;


        }
        /// <summary>
        /// 保存历史
        /// </summary>
        /// <param name="ContText">合同文本</param>
        /// <returns></returns>
        private Dictionary<string, int> SaveContTextHistory(ContText contText)
        {
            try
            {
                var hiinfo = _IMapper.Map<ContText, ContTextHistory>(contText);// contText.ToModel<ContText, ContTextHistory>();
                hiinfo.ContTxtId = contText.Id;
                if (contText.ContId > 0)
                {
                    var contHis = Db.Set<ContractInfoHistory>().AsNoTracking().Where(a => a.ContId == contText.ContId).OrderByDescending(a => a.Id).FirstOrDefault();
                    hiinfo.ContHisId = contHis.Id;
                }

                Db.Set<ContTextHistory>().Add(hiinfo);
                Db.SaveChanges();
                var dic = new Dictionary<string, int>();
                dic.Add("Id", contText.Id);
                dic.Add("Hid", hiinfo.Id);
                return dic;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        /// <summary>
        /// 合同文本大列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public LayPageInfo<ContTextListViewDTO> GetMainList<s>(PageInfo<ContText> pageInfo, Expression<Func<ContText, bool>> whereLambda, Expression<Func<ContText, s>> orderbyLambda, bool isAsc)
        {

            //var tempquery = GetQueryToPage(pageInfo, whereLambda, orderbyLambda, isAsc);
            var tempquery = Db.Set<ContText>()
                .Include(a=>a.Cont).ThenInclude(a=>a.Comp)
                .AsTracking().Where<ContText>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<ContText>))
                tempquery = tempquery.Skip<ContText>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<ContText>(pageInfo.PageSize);
            var query = from a in tempquery.ToList()
                        join b in Db.Set<ContTextSeal>().AsNoTracking()
                        on a.Id equals b.ContTextId into seal
                        from sl in seal.DefaultIfEmpty()
                        join c in Db.Set<ContTextArchive>().AsNoTracking()
                        on a.Id equals c.ContTextId into arch
                        from ac in arch.DefaultIfEmpty()
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            CategoryId = a.CategoryId,
                            FileName = a.FileName,
                            Remark = a.Remark,
                            IsFromTemp = a.IsFromTemp,//文本来源
                            ExtenName = a.ExtenName,
                            ContId = a.ContId,
                            ContName = a.Cont!=null?a.Cont.Name:"",
                            ContCode = a.Cont != null ? a.Cont.Code:"",
                            ContState = a.Cont != null ? a.Cont.ContState:-1,
                            PrincipalUserId = a.Cont != null ? a.Cont.PrincipalUserId:-1,
                            CreateUserId = a.CreateUserId,
                            CompId = a.Cont != null ? a.Cont.CompId:-1,
                            CompName = (a.Cont != null&& a.Cont.Comp!=null) ? a.Cont.Comp.Name:"",
                            SealState = sl == null ? -1 : sl.SealState,//用章状态
                            // SealName = sl == null ? "" : sl.Seal.SealName,//用章名称
                             SealName = sl == null||sl.Seal==null ? "" : sl.Seal.SealName,//用章名称
                            SealDate = sl == null ? null : (DateTime?)sl.CreateDateTime,//用章日期
                            ArchiveCode = ac == null ? "" : ac.ArcCode,//归档号
                            ArchiveState = ac == null ? 0 : 1,//归档状态
                            ArchiveCabCode = ac == null ? "" : ac.ArcCabCode,//归档柜号
                            ArchiveNumber = ac == null ? 0 : ac.ArcSumNumber,//归档总数
                            BorrowNumber = ac == null ? 0 : ac.BorrSumNumber,//借阅总数
                        };

            var local = from a in query.AsEnumerable()
                        select new ContTextListViewDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            CategoryName = DataDicUtility.GetDicValueToRedis(a.CategoryId, DataDictionaryEnum.ContTxtType),
                            FileName = a.FileName,
                            Remark = a.Remark,
                            IsFromTxt = EmunUtility.GetDesc(typeof(SourceTxtEnum), a.IsFromTemp ?? -1),//文本来源
                            ExtenName = a.ExtenName,
                            ContId = a.ContId,
                            ContName = a.ContName,
                            ContCode = a.ContCode,
                            ContState = a.ContState,
                            IsFromTemp = a.IsFromTemp ?? 0,//文本来源
                            ContStateDic = EmunUtility.GetDesc(typeof(ContractState), a.ContState),
                            PrincipalUserName = this.Db.GetRedisUserFieldValue(a.PrincipalUserId ?? -1),
                         //   RedisValueUtility.GetUserShowName(a.PrincipalUserId ?? -1), //负责人
                            CreateUserName = this.Db.GetRedisUserFieldValue(a.CreateUserId),
                            //RedisValueUtility.GetUserShowName(a.CreateUserId), //建立人
                            CompId = a.CompId,
                            CompName = a.CompName,
                            SealState = a.SealState,//用章状态
                            SealStateDic = EmunUtility.GetDesc(typeof(SealStateEnum), a.SealState),
                            SealName = (a.SealName==null ? "" :a.SealName),//用章名称
                            SealDate = a.SealDate,//用章日期
                            ArchiveCode = a.ArchiveCode,//归档号
                            ArchiveState = a.ArchiveState,//归档状态
                            ArchiveStateDic = EmunUtility.GetDesc(typeof(ArchiveStateEnum), a.SealState),
                            ArchiveCabCode = a.ArchiveCabCode,//归档柜号
                            ArchiveNumber = a.ArchiveNumber ?? 0,//归档总数
                            BorrowNumber = a.BorrowNumber ?? 0,//借阅总数
                            Surplus = (a.ArchiveNumber ?? 0) - (a.BorrowNumber ?? 0),//剩余数量
                            IsPdf=!string.IsNullOrEmpty(a.FileName)&&a.FileName.EndsWith(".pdf")
                        };
            return new LayPageInfo<ContTextListViewDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };



        }
        /// <summary>
        /// 包含IsInWorkflow,IsWordEdit得计算
        /// </summary>
        /// <param name="ContText"></param>
        /// <param name="IncludeWordEdit"></param>
        /// <returns></returns>
        private HistoryStepState GetHistoryStepState(ContText ContText, bool IncludeWordEdit = false)
        {
            var info = new HistoryStepState();
            //info.UserID = LoginUtility.GetCurrentUserID();
            //info.UserID=
            var query = from a in Db.Set<AppInst>()
                        from b in Db.Set<AppInstNode>()
                        from c in Db.Set<ContText>()
                        where a.AppObjId == c.ContId
                        && a.ObjType == (int)FlowObjEnums.Contract
                        && a.AppState == (int)AppInstEnum.AppState0
                        && a.CurrentNodeId == b.Id
                        && c.Id == ContText.Id
                        select new
                        {
                            WfInst = a,
                            WfInstNode = b,
                        };
            var temp = query.FirstOrDefault();
            AppInst WfInst = null;
            AppInstNode WfInstNode = null;
            AppInstNodeInfo WfInstNodeInfo = null;

            if (temp != null)
            {
                WfInst = temp.WfInst;
                WfInstNode = temp.WfInstNode;
                WfInstNodeInfo = Db.Set<AppInstNodeInfo>().Where(a => a.InstNodeId == WfInstNode.Id).FirstOrDefault();
                info.WfInstID = WfInst.Id;
                info.WfInstNodeID = WfInst.CurrentNodeId;
                info.WfInstState = WfInst.AppState;
                info.WfInstVersions = WfInst.Version;

                if (IncludeWordEdit)
                {
                    if (WfInst.AppState == (int)AppInstEnum.AppState0)
                    {
                        //审批中
                        info.IsInWorkflow = true;
                        if (WfInstNodeInfo != null && WfInstNodeInfo.ReviseText == 1)
                        {
                            info.IsWordEdit = true;
                        }
                    }
                }
            }
            if (IncludeWordEdit)
            {
                if (!info.IsInWorkflow)
                {
                    var ContTextTempHist = Db.Set<ContTxtTemplateHist>().Where(a => a.Id == ContText.TemplateId).FirstOrDefault();
                    if (ContTextTempHist != null && ContTextTempHist.WordEdit == 1)
                    {
                        info.IsWordEdit = true;
                    }
                }
            }

            return info;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contTxtId"></param>
        /// <param name="IsHistory"></param>
        /// <param name="IsReview"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetWordState(int contTxtId, bool IsHistory, bool IsReview, int userId)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (IsReview)
            {
                var IsReviseText = false;


                //审阅

                var query = from a in Db.Set<AppInst>()
                            from b in Db.Set<ContText>()
                            where a.AppObjId == b.ContId
                             && a.ObjType == (int)FlowObjEnums.Contract
                             && a.AppState == (int)AppInstEnum.AppState0
                            && b.Id == contTxtId
                            select a;

                var inst = query.FirstOrDefault();
                if (inst != null)
                {
                   
                    var nodeInfos = Db.Set<AppInstNodeInfo>().Where(a => a.InstId == inst.Id).ToList();
                    var current = nodeInfos.Where(a => a.NodeStrId == inst.CurrentNodeStrId).FirstOrDefault();
                    if (current != null)
                    {
                        var listuserIds = Db.Set<AppGroupUser>()
                        .Where(a => a.GroupId == current.GroupId).Select(a => a.UserId).ToList();
                        if (listuserIds.Contains(userId))
                        {
                            IsReviseText = current.ReviseText == 1;

                        }

                    }

                }

                dic.Add("ContTempWordEdit", IsReviseText.ToString());

               

            }
            else if (IsHistory)
            {
                //起草
                var info = Db.Set<ContTextHistory>().Where(a => a.Id == contTxtId).FirstOrDefault();
                
               if(info!=null)
                {
                    if (info.IsFromTemp == (int)SourceTxtEnum.Upload
                        || info.IsFromTemp == (int)SourceTxtEnum.FreeDraft)
                    {
                       
                        dic.Add("ContTempWordEdit","false");
                    }
                    else
                    {
                        if (info.Template != null)
                        {
                            dic.Add("ContTempWordEdit", (info.Template.WordEdit??0).ToString());
                            
                        }
                    }
                }
            }
            else
            {
                var info = Db.Set<ContText>().Include(a=>a.Cont).Include(a=>a.Template).Where(a => a.Id == contTxtId).FirstOrDefault(); 
                   
                if (info != null )
                {
                    dic.Add("ContractState",info.Cont.ContState.ToString());
                    if (info.Template != null)
                    {
                        dic.Add("ContTempWordEdit", (info.Template.WordEdit??0).ToString());
                    }
                    else
                    {
                        dic.Add("ContTempWordEdit","false");
                    }
                    
                }
            }

            return dic;

        }
        /// <summary>
        /// 根据合同id 获取图片集合
        /// </summary>
        /// <param name="contId">图片ID</param>
        /// <returns></returns>
        public IList<PicViewDTO> GetPicViews(int contId)
        {
            var extens = new string[] {
                ".png",".jpg",".jpeg",".bmp",".svg",".gif",".tif",".psd",".pcx",".svg",".cdr",".raw",
                ".avif",".raw",".ai",".tga",".exif",".fpx",".eps",".webp"
            };
            var list = Db.Set<ContText>().Where(a => a.ContId == contId && extens.Contains(a.ExtenName.ToLower()))
                .Select(a => new PicViewDTO
                {
                    Id = a.Id,
                    Name = a.Name,
                    Path = a.Path,
                }).ToList();

            return list;

        }
    
    }
}
