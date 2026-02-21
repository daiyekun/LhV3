using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using NF.Common.Models;
using NF.Common.Utility;
using NF.IBLL;
using NF.Model.Models;
using NF.OfficeComm;
using NF.ViewModel;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using NF.ViewModel.Models.Common;
using NF.Web.Controllers;
using NF.Web.Utility;
using NF.Web.Utility.Common;
using NF.Web.Utility.Filters;
using Rotativa.AspNetCore;

namespace NF.Web.Areas.ContractDraft.Controllers
{

    /// <summary>
    /// 合同文本
    /// </summary>
    [Area("ContractDraft")]
    [Route("ContractDraft/[controller]/[action]")]
    public class ContTextController : NfBaseController
    {
        /// <summary>
        /// 合同文本
        /// </summary>
        private IContTextService _IContTextService;
        /// <summary>
        /// 映射
        /// </summary>
        private IMapper _IMapper;
        /// <summary>
        /// 权限
        /// </summary>
        private ISysPermissionModelService _ISysPermissionModelService;
        /// <summary>
        /// 盖章
        /// </summary>
        private IContTextSealService _IContTextSealService;
        /// <summary>
        /// 归档主体
        /// </summary>
        private IContTextArchiveService _IContTextArchiveService;
        /// <summary>
        /// 借阅
        /// </summary>
        private IContTextBorrowService _ContTextBorrowService;
        /// <summary>
        /// 合同文本历史
        /// </summary>
        private IContTextHistoryService _IContTextHistoryService;
        /// <summary>
        /// 合同
        /// </summary>
        private IContractInfoService _IContractInfoService;
        /// <summary>
        /// 合同附件
        /// </summary>
        private IContAttachmentService _IContAttachmentService;

        /// <summary>
        ///日志
        /// </summary>
        private readonly ILogger _logger;


        public ContTextController(IContTextService IContTextService, IMapper IMapper
            , ISysPermissionModelService ISysPermissionModelService
            , IContTextSealService IContTextSealService
            , IContTextArchiveService IContTextArchiveService
            , IContTextBorrowService ContTextBorrowService
            , IContTextHistoryService IContTextHistoryService
            , ILogger<ContTextController> logger
            , IContractInfoService IContractInfoService
            ,IContAttachmentService iContAttachmentService)
        {
            _IContTextService = IContTextService;
            _IMapper = IMapper;
            _ISysPermissionModelService = ISysPermissionModelService;
            _IContTextSealService = IContTextSealService;
            _IContTextArchiveService = IContTextArchiveService;
            _ContTextBorrowService = ContTextBorrowService;
            _IContTextHistoryService = IContTextHistoryService;
            _logger = logger;
            _IContractInfoService = IContractInfoService;
            _IContAttachmentService = iContAttachmentService;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetList(PageparamInfo pageParam, int contId)
        {
            var pageInfo = new NoPageInfo<ContText>();//(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<ContText>();
            var predicateOr = PredicateBuilder.False<ContText>();
            predicateOr = predicateOr.Or(a => a.ContId == -this.SessionCurrUserId && a.IsDelete == 0);
            if (contId != 0)
            {
                predicateOr = predicateOr.Or(a => a.ContId == contId && a.IsDelete == 0);
            }
            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _IContTextService.GetList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 新建
        /// </summary>
        /// <returns></returns>
        public IActionResult Build()
        {
            return View();

        }

        /// <summary>
        /// 新增合同文本
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("新增合同文本", OptionLogTypeEnum.Add, "新增合同文本", true)]
        public IActionResult Save(ContTextDTO ContTextDTO)
        {

            var saveInfo = _IMapper.Map<ContText>(ContTextDTO);
            saveInfo.CreateDateTime = DateTime.Now;
            saveInfo.ModifyDateTime = DateTime.Now;
            saveInfo.CreateUserId = this.SessionCurrUserId;
            saveInfo.ModifyUserId = this.SessionCurrUserId;
            saveInfo.IsDelete = 0;
            saveInfo.FolderName = ContTextDTO.FolderName;
            saveInfo.Path = "Uploads/" + ContTextDTO.FolderName + "/" + ContTextDTO.GuidFileName;
            saveInfo.ContId = (ContTextDTO.ContId ?? 0) <= 0 ? -this.SessionCurrUserId : ContTextDTO.ContId;
            saveInfo.Versions = 1;
            //saveInfo.IsFromTemp = 0;//本地上传
            saveInfo.Stage = 0;//原始
            var dic = _IContTextService.AddSave(saveInfo);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,
                Data = dic


            });

        }
        /// <summary>
        /// 新建附件
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("修改合同文本", OptionLogTypeEnum.Update, "修改合同文本", true)]
        public IActionResult UpdateSave(ContTextDTO ContTextDTO)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            if (ContTextDTO.Id > 0)
            {
                var updateinfo = _IContTextService.Find(ContTextDTO.Id);

                if (ContTextDTO.IsFromTemp == (int)SourceTxtEnum.Upload)
                {
                    updateinfo.Path = "Uploads/" + ContTextDTO.FolderName + "/" + ContTextDTO.GuidFileName;
                    updateinfo.FolderName = ContTextDTO.FolderName;
                    var updatedata = _IMapper.Map(ContTextDTO, updateinfo);
                    updatedata.ModifyUserId = this.SessionCurrUserId;
                    updatedata.ModifyDateTime = DateTime.Now;
                    updatedata.Versions = (updateinfo.Versions ?? 1) + 1;
                    //updateinfo.IsFromTemp = 0;//本地上传
                    updateinfo.Stage = 0;//原始
                    dic = _IContTextService.UpdateSave(updatedata);
                }
                else
                {
                    updateinfo.ModifyUserId = this.SessionCurrUserId;
                    updateinfo.ModifyDateTime = DateTime.Now;
                    updateinfo.Versions = (updateinfo.Versions ?? 1) + 1;
                    updateinfo.Stage = 0;//原始
                    dic = _IContTextService.UpdateSave(updateinfo);
                }



            }

            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,
                Data = dic


            });

        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("删除合同文本", OptionLogTypeEnum.Del, "删除合同文本", false)]
        public IActionResult Delete(string Ids)
        {
            var resinfo = new RequstResult()
            {
                Msg = "删除成功！",
                Code = 0,
            };
            var usname = this.SessionCurrUserId;
            if (usname == -10000)
            {
                _IContTextService.Delete(Ids);
            }
            else
            {
                _IContTextService.Delete(Ids);
            }
            _IContTextService.Delete(Ids);
            return new CustomResultJson(resinfo);
        }
        /// <summary>
        /// 查看
        /// </summary>
        /// <returns></returns>
        public IActionResult ShowView(int Id)
        {
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = _IContTextService.ShowView(Id)


            });
        }
        /// <summary>
        /// 收款合同文本大列表
        /// </summary>
        /// <returns></returns>
        public IActionResult ContTextCollIndex()
        {
            return View();
        }
        /// <summary>
        /// 付款合同文本大列表
        /// </summary>
        /// <returns></returns>
        public IActionResult ContTextPayIndex()
        {
            return View();
        }
        /// <summary>
        /// 获取查询条件表达式
        /// </summary>
        /// <param name="pageInfo">查询分页器，传NoPageInfo对象不分页</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        private Expression<Func<ContText, bool>> GetQueryExpression(PageInfo<ContText> pageInfo, string keyWord, int financeType)
        {
            var predicateAnd = PredicateBuilder.True<ContText>();
            var predicateOr = PredicateBuilder.False<ContText>();
            predicateAnd = predicateAnd.And(a => (a.ContId ?? 0) > 0);
            if (!string.IsNullOrEmpty(keyWord))
            {
                predicateOr = predicateOr.Or(a => a.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Cont!=null&& a.Cont.Name.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Cont!=null&& a.Cont.Code.Contains(keyWord));
                predicateOr = predicateOr.Or(a => a.Cont != null &&a.Cont.Comp!=null&& a.Cont.Comp.Name.Contains(keyWord));
                predicateAnd = predicateAnd.And(predicateOr);
            }
            predicateAnd = predicateAnd.And(a => (a.Cont!=null&& a.Cont.FinanceType == financeType) && a.IsDelete == 0);
            predicateAnd = predicateAnd.And(_ISysPermissionModelService.GetContractTextListPermissionExpression((financeType == 0 ? "querycollcontview" : "querypaycontview"), this.SessionCurrUserId, this.SessionCurrUserDeptId));

            return predicateAnd;
        }
        /// <summary>
        /// 合同文本大列表
        /// </summary>
        /// <param name="companyId">对方ID</param>
        /// <returns></returns>
        public IActionResult GetMainList(PageparamInfo pageParam, int contId)
        {
            var pageInfo = new PageInfo<ContText>(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<ContText>();
            predicateAnd = predicateAnd.And(GetQueryExpression(pageInfo, pageParam.keyWord, pageParam.requestType));
            var layPage = _IContTextService.GetMainList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }
        #region 合同文本盖章

        /// <summary>
        /// 合同盖章弹框页面
        /// </summary>
        /// <returns></returns>
        public IActionResult ContTextSeal()
        {
            return View();

        }
        /// <summary>
        /// 合同盖章显示页面
        /// </summary>
        /// <returns></returns>
        public IActionResult ContTextViewSeal()
        {
            return View();

        }
        /// <summary>
        /// 保存盖章
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("合同文本盖章", OptionLogTypeEnum.Add, "合同文本盖章", true)]
        public IActionResult SaveSeal(ContTextSealDTO textSealDTO)
        {
            if (textSealDTO.Id > 0)
            {//修改
                var updateinfo = _IContTextSealService.Find(textSealDTO.Id);
                var updatedata = _IMapper.Map(textSealDTO, updateinfo);
                updateinfo.ModifyUserId = this.SessionCurrUserId;
                updateinfo.IsDelete = 0;
                updateinfo.ModifyDateTime = DateTime.Now;
                _IContTextSealService.Update(updateinfo);
            }
            else
            {
                var saveInfo = _IMapper.Map<ContTextSeal>(textSealDTO);
                saveInfo.CreateDateTime = DateTime.Now;
                saveInfo.ModifyDateTime = DateTime.Now;
                saveInfo.CreateUserId = this.SessionCurrUserId;
                saveInfo.ModifyUserId = this.SessionCurrUserId;
                saveInfo.IsDelete = 0;
                _IContTextSealService.Add(saveInfo);
            }
            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,


            });
        }
        /// <summary>
        /// 盖章显示
        /// </summary>
        /// <returns></returns>
        public IActionResult SealShowView(int Id)
        {
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = _IContTextSealService.ShowView(SessionCurrUserId, Id)


            });

        }

        #endregion

        #region 合同归档
        /// <summary>
        /// 合同盖章弹框页面
        /// </summary>
        /// <returns></returns>
        public IActionResult ContTextArchive()
        {
            return View();

        }
        /// <summary>
        /// 合同文本归档
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("合同文本归档", OptionLogTypeEnum.Add, "合同文本归档", true)]
        public IActionResult SaveArchive(ContTextArchive textArchive, ContTextArchiveItem textArchiveItem)
        {
            textArchive.CreateDateTime = DateTime.Now;
            textArchive.CreateUserId = SessionCurrUserId;
            textArchive.ModifyDateTime = DateTime.Now;
            textArchive.ModifyUserId = SessionCurrUserId;
            textArchiveItem.CreateDateTime = DateTime.Now;
            textArchiveItem.CreateUserId = SessionCurrUserId;
            textArchiveItem.ModifyDateTime = DateTime.Now;
            textArchiveItem.ModifyUserId = SessionCurrUserId;
            _IContTextArchiveService.AddSave(textArchive, textArchiveItem);
            return new CustomResultJson(new RequstResult()
            {
                Msg = "保存成功",
                Code = 0,


            });
        }
        /// <summary>
        /// 归档明细
        /// </summary>
        /// <param name="txtId">文档ID</param>
        /// <returns></returns>
        public IActionResult GetTextArchiveItem(int txtId)
        {
            var pageInfo = new NoPageInfo<ContTextArchiveItem>();
            var predicateAnd = PredicateBuilder.True<ContTextArchiveItem>();
            predicateAnd = predicateAnd.And(a => a.ContTextId == txtId && a.IsDelete == 0);
            var layPage = _IContTextArchiveService.GetListArchiveItem(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }
        /// <summary>
        /// 归档信息
        /// </summary>
        /// <param name="Id">合同文本ID（注意不是归档信息ID）</param>
        /// <returns></returns>
        public IActionResult ArchiveShowView(int Id)
        {
            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = _IContTextArchiveService.ArchiveShowView(Id)


            });
        }

        /// <summary>
        /// 修改归档字段
        /// </summary>
        /// <param name="Id">修改对象ID</param>
        /// <param name="fieldName">修改字段名称</param>
        /// <param name="fieldVal">修改值，如果不是String后台人为判断</param>
        /// <returns></returns>
        [NfCustomActionFilter("修改归档信息", OptionLogTypeEnum.Update, "修改归档信息", false)]
        public IActionResult UpdateAchField(int Id, string fieldName, string fieldVal)
        {
            var res = _IContTextArchiveService.UpdateField(new UpdateFieldInfo()
            {
                Id = Id,
                FieldName = fieldName,
                FieldValue = fieldVal


            });
            RequstResult reqInfo = null;
            if (res > 0)
            {
                reqInfo = new RequstResult()
                {
                    Msg = "修改成功",
                    Code = 0,


                };
            }
            else
            {
                reqInfo = new RequstResult()
                {
                    Msg = "修改失败",
                    Code = 0,


                };
            }
            return new CustomResultJson(reqInfo);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [NfCustomActionFilter("删除归档明细", OptionLogTypeEnum.Del, "删除归档明细", false)]
        public IActionResult DeleteArchItem(string Ids, int conttextId)
        {
            _IContTextArchiveService.DeleteArchItem(Ids, conttextId);
            return new CustomResultJson(new RequstResult { Code = 0, Msg = "删除成功" });
        }

        #endregion

        #region 借阅
        /// <summary>
        /// 合同文本-借阅
        /// </summary>
        /// <returns></returns>
        public IActionResult ContTextBorrow()
        {
            return View();

        }
        /// <summary>
        /// 保存借阅
        /// </summary>
        /// <param name="textBorrow">借阅对象</param>
        /// <returns></returns>
        public IActionResult SaveBorrow(ContTextBorrow textBorrow)
        {
            textBorrow.CreateDateTime = DateTime.Now;
            textBorrow.CreateUserId = SessionCurrUserId;
            textBorrow.ModifyDateTime = DateTime.Now;
            textBorrow.ModifyUserId = SessionCurrUserId;
            textBorrow.BorrHandlerUser = SessionCurrUserId;
            _ContTextBorrowService.AddSave(textBorrow);
            return new CustomResultJson(new RequstResult
            {
                Code = 0,
                Msg = "保存成功"
            });
        }

        public IActionResult GetListBorrow(int txtId)
        {
            var pageInfo = new NoPageInfo<ContTextBorrow>();
            var predicateAnd = PredicateBuilder.True<ContTextBorrow>();
            predicateAnd = predicateAnd.And(a => a.ContTextId == txtId && a.IsDelete == 0);
            var layPage = _ContTextBorrowService.GetListBorrow(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);

        }
        /// <summary>
        /// 归还
        /// </summary>
        /// <param name="textBorrow">归还对象</param>
        /// <returns></returns>
        public IActionResult SaveRepay(ContTextBorrow textBorrow)
        {



            textBorrow.ModifyUserId = SessionCurrUserId;
            textBorrow.BorrHandlerUser = SessionCurrUserId;
            textBorrow.RepayHandlerUser = SessionCurrUserId;
            textBorrow.RepayNumber = textBorrow.RepayNumber;
            textBorrow.RepayUser = textBorrow.RepayUser;
            textBorrow.RepayDateTime = textBorrow.RepayDateTime;
            _ContTextBorrowService.SaveRepay(textBorrow);

            return new CustomResultJson(new RequstResult
            {
                Code = 0,
                Msg = "保存成功"
            });
        }


        #endregion

        #region 模板起草修改基本信息
        public IActionResult DraftTxtInfo()
        {
            return View();
        }
        #endregion
        /// <summary>
        /// 获取当前文本状态
        /// </summary>
        /// <returns></returns>
        public IActionResult GetWordState(int ContTextId, bool IsHistory, bool IsReview)
        {

            var dic = _IContTextService.GetWordState(ContTextId, IsHistory, IsReview, this.SessionCurrUserId);

            return new CustomResultJson(new RequstResult()
            {
                Msg = "",
                Code = 0,
                Data = dic


            });
        }

        #region 合同文本历史
        /// <summary>
        /// 合同文本列表
        /// </summary>
        /// <param name="contId">合同ID</param>
        /// <param name="pageParam">分页对象</param>
        /// <param name="txtId">当前合同文本ID</param>
        /// <returns></returns>
        public IActionResult GetHistList(PageparamInfo pageParam, int contId, int txtId)
        {
            var pageInfo = new NoPageInfo<ContTextHistory>();//(pageIndex: pageParam.page, pageSize: pageParam.limit);
            var predicateAnd = PredicateBuilder.True<ContTextHistory>();
            var predicateOr = PredicateBuilder.False<ContTextHistory>();
            predicateAnd = predicateAnd.And(p => p.ContTxtId == txtId);
            predicateOr = predicateOr.Or(a => a.ContId == -this.SessionCurrUserId && a.IsDelete == 0);
            if (contId != 0)
            {
                predicateOr = predicateOr.Or(a => a.ContId == contId && a.IsDelete == 0);
            }

            predicateAnd = predicateAnd.And(predicateOr);
            var layPage = _IContTextHistoryService.GetList(pageInfo, predicateAnd, a => a.Id, false);
            return new CustomResultJson(layPage);
        }

        #endregion
        /// <summary>
        /// 获取文本对象
        /// </summary>
        /// <returns></returns>
        public IActionResult GetByID(int Id)
        {
            try
            {
                var info = _IContTextService.Find(Id);
                return new CustomResultJson(new RequstResult()
                {
                    Msg = "",
                    Code = 0,
                    Data = info


                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new CustomResultJson(new RequstResult()
                {
                    Msg = "Err",
                    Code = 0,



                });
            }
        }
        /// <summary>
        /// PDF
        /// </summary>
        /// <returns></returns>
        public IActionResult ShowViewPdf()
        {
            return View();
        }

        ///// <summary>
        ///// 文件预览
        ///// </summary>
        ///// <param name="filepath">文件路径</param>
        ///// <returns></returns>
        //public IActionResult GetFileBytes(int Id)
        //{
        //    if (string.IsNullOrEmpty(filepath)) 
        //        filepath = "D:\\ABC.log";
        //    var provider = new FileExtensionContentTypeProvider();
        //    FileInfo fileInfo = new FileInfo(filepath);
        //    var ext = fileInfo.Extension;
        //    new FileExtensionContentTypeProvider().Mappings.TryGetValue(ext, out var contenttype);
        //    return File(FileUtility.ReadAllBytes(filepath), contenttype ?? "application/octet-stream", fileInfo.Name);
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult GetFileBytes(int Id)
        {
            string guidFileName = string.Empty;
            var contText = _IContTextService.Find(Id);
            if (contText != null)
            {
                guidFileName = contText.GuidFileName;
            }
            if (guidFileName.StartsWith('~'))
            {
                var filearr = StringHelper.Strint2ArrayString(guidFileName, "/");

                guidFileName = filearr.LastOrDefault();
            }
            var pathf = Path.Combine(
                            Directory.GetCurrentDirectory(),  "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), 6),
                            guidFileName);
            if (!FileUtility.Exists(pathf))
            {
                 pathf = Path.Combine(
                            Directory.GetCurrentDirectory(), contText.Path.TrimStart('~').TrimStart('/')
                            );
            }

            var downInfo = FileStreamingHelper.Download(pathf);
            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);
        }
        /// <summary>
        /// 图片
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult GetFileBytesTuPian(int Id)
        {
           

            /**{
                         "title": "", //相册标题
           "id": 123, //相册id
           "start": 0, //初始显示的图片序号，默认0
           "data": [   //相册包含的图片，数组格式
             {
               "alt": "图片名",
               "pid": 666, //图片id
               "src": "", //原图地址
               "thumb": "" //缩略图地址
             }
           ]
         }**/

            var pcinfo = new PicViewInfo();
            pcinfo.title = "test";
            pcinfo.id = 1;
            pcinfo.start = 0;
            PicData picData = new PicData();
            picData.alt = "";
            picData.pid = 1;
            picData.thumb = "";
            var baseurl = $"/ContractDraft/ContText/GetPic?Id={Id}";
            picData.src = baseurl; 
            var listdata = new List<PicData>();
            listdata.Add(picData);
            pcinfo.data = listdata;

            return new CustomResultJson(new RequstResult()
            {
                Msg = "success",
                Code = 0,
                Data= pcinfo

            });
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult GetPic(int Id)
        {
            string guidFileName = string.Empty;
            var contText = _IContTextService.Find(Id);
            if (contText != null)
            {
                guidFileName = contText.GuidFileName;
            }
            if (guidFileName.StartsWith('~'))
            {
                var filearr = StringHelper.Strint2ArrayString(guidFileName, "/");

                guidFileName = filearr.LastOrDefault();
            }
            var pathf = Path.Combine(
                            Directory.GetCurrentDirectory(),"Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), 6),
                            guidFileName);
            var downInfo = FileStreamingHelper.Download(pathf);
            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);

        }
        /// <summary>
        /// pdf 文件Word文件预览
        /// </summary>
        /// <returns></returns>
        public IActionResult WordView(int Id)
        {
            //Log4netHelper.Info("开始生成文件WordView。");
            string guidFileName = string.Empty;
            var contText = _IContTextService.Find(Id);
            var Islaseword = false;
            if (contText != null)
            {
                var continfo = _IContractInfoService.Find(contText.ContId??0);
                if(continfo!=null&&(continfo.ContState== (byte)ContractState.Execution|| continfo.ContState== (byte)ContractState.Approve))
                {//执行中，审批通过状态才允许直接生成最终pdf
                    Islaseword = true;
                }
                if (contText.IsFromTemp == 1&& !Islaseword)
                {
                    guidFileName = contText.FileName;
                }
                else {
                    if (!contText.GuidFileName.EndsWith(".docx"))
                    {
                        guidFileName = $"{contText.GuidFileName}.docx";
                    }
                    else
                    {
                        guidFileName = contText.GuidFileName;
                    }
                       
                    
                
                }
            }
            if (guidFileName.StartsWith('~'))
            {
                var filearr = StringHelper.Strint2ArrayString(guidFileName, "/");

                guidFileName = filearr.LastOrDefault();
            }
            var wordname = guidFileName;
            if (contText.IsFromTemp == 1)
            {
                wordname = contText.FileName;
            }
            var pathf = Path.Combine(
                            Directory.GetCurrentDirectory(),  "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), 6),
                            wordname);
            var pdfpath=Path.Combine(
                             Directory.GetCurrentDirectory(),  "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), 6),
                             //guidFileName.Replace(".docx",".pdf")
                             wordname.Replace(".docx", ".pdf")
                             );
            var markpath = Path.Combine(
                            Directory.GetCurrentDirectory(),  "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), 11),
                            "ContractTextWordWaterMark.dotx");
            //WpsWordToPdfHelper.WordToPdf(pathf, pdfpath);
            MsWordToPdfHelper wpfh = new MsWordToPdfHelper();
            //wpfh.ConvertWordToPdf(pathf, pdfpath);
            if (Islaseword)
            {//甚至最终pdf
                //Log4netHelper.Info("最终文件生成");
                if(pathf.EndsWith(".docx")|| pathf.EndsWith(".doc")|| pathf.EndsWith(".DOCX") || pathf.EndsWith(".DOCX"))
                {
                   // Log4netHelper.Info("开始调用生成");
                    wpfh.ConvertWordToWrkPdf(pathf, pdfpath, markpath);
                }
               
                
                DowloadLastPdf(contText);
            }
            else
            {//零时生成预览而已
                //Log4netHelper.Info("零时生成...");
                if (pathf.EndsWith(".docx") || pathf.EndsWith(".doc") || pathf.EndsWith(".DOCX") || pathf.EndsWith(".DOCX"))
                {
                   // Log4netHelper.Info("零时生成..调用");
                    wpfh.ConvertWordToPdf(pathf, pdfpath);
                }
            }
            
             var downInfo = FileStreamingHelper.Download(pdfpath);
            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);
        }

        /// <summary>
        /// Word文件预览
        /// </summary>
        /// <returns></returns>
        public IActionResult WordNewView(int Id)
        {
            string guidFileName = string.Empty;
            var contText = _IContTextService.Find(Id);
            var Islaseword = false;
            var newwordpath = "";
            if (contText != null)
            {
                var continfo = _IContractInfoService.Find(contText.ContId ?? 0);
                if (continfo != null && (continfo.ContState == (byte)ContractState.Execution || continfo.ContState == (byte)ContractState.Approve))
                {
                    Islaseword = true;


                }
                if (Islaseword)
                {//审批通过或者执行者

                    if (!string.IsNullOrEmpty(contText.Path) &&
                        contText.Path.ToLower().EndsWith(".pdf")
                        )
                    {//已经生成pdf
                        newwordpath = Path.Combine(Directory.GetCurrentDirectory(),
                            contText.Path.Replace(".pdf", ".docx").TrimStart('~').TrimStart('/'));
                    }
                    else
                    {
                        var pathf  = Path.Combine(
                                         Directory.GetCurrentDirectory(), contText.Path.TrimStart('~').TrimStart('/')
                                         );
                        var markpath = Path.Combine(
                                        Directory.GetCurrentDirectory(), "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), 11),
                                        "ContractTextWordWaterMark.dotx");
                        //最新生成word带水印
                         newwordpath = Path.Combine(
                                         Directory.GetCurrentDirectory(), contText.Path.TrimStart('~').TrimStart('/')
                                         );
                        MsWordToPdfHelper wpfh = new MsWordToPdfHelper();
                        wpfh.ConvertNewWordToWrkWord(pathf, newwordpath, markpath);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(contText.Path) &&
                        contText.Path.ToLower().EndsWith(".pdf")
                        )
                    {//已经生成pdf---考虑变更过程种有word
                        newwordpath = Path.Combine(Directory.GetCurrentDirectory(),
                            contText.Path.Replace(".pdf", ".docx").TrimStart('~').TrimStart('/'));
                    }
                    else
                    {
                       newwordpath = Path.Combine(
                                         Directory.GetCurrentDirectory(), contText.Path.TrimStart('~').TrimStart('/')
                                         );
                    }

                }

            }

                

                var downInfo = FileStreamingHelper.Download(newwordpath);
                return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);
            }

        public IActionResult PicView()
        {
            return View();
        }
        /// <summary>
        /// 下载最终pdf的时候
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string DowloadLastPdf(ContText info)
        {
            try
            {
                //var info = _IContTextService.Find(cttextid ?? 0);
                if (info == null)
                {
                    return "ERR";

                }
                var padfpath = info.Path;
                if (padfpath.EndsWith(".docx"))
                {
                    padfpath = padfpath.Replace(".docx", ".pdf");
                }
               

                if (info.Path.EndsWith(".docx"))
                {
                    info.Path = info.Path.Replace(".docx", ".pdf");
                    info.ExtenName = ".pdf";
                    if (info.GuidFileName.EndsWith(".docx"))
                    {
                        info.FileName = info.GuidFileName.Replace(".docx", ".pdf");
                    }
                    else
                    {
                        info.FileName = $"{info.GuidFileName}.pdf";
                    }
                    //info.FileName = info.GuidFileNameinfo.Path.Replace(".docx", ".pdf");
                }
                _IContTextService.UpdateQiCaoSave(info, AddHistory: false);

                if (RedisHelper.KeyExists($"pdf:{info.Id}:{this.SessionCurrUserId}"))
                {
                    RedisHelper.KeyDelete($"pdf:{info.Id}:{this.SessionCurrUserId}");
                }
                RedisHelper.StringSet($"pdf:{info.Id}:{this.SessionCurrUserId}", info.Path);

                return "SUC";
            }
            catch (Exception ex)
            {

                
                return "ERR";
            }
        }

        /// <summary>
        /// 图片预览
        /// </summary>
        /// <returns></returns>
        public IActionResult PictureView(int contId,int? viewType)
        {
            if ((viewType ?? 0) == 3)
            {//合同附件
                var list = _IContAttachmentService.GetPicViews(contId);
                ViewBag.ListPics = list;
            }
            else { 
            var list = _IContTextService.GetPicViews(contId);
            ViewBag.ListPics = list;
            }
            return View();

        }



    }
}