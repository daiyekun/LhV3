using Common.Model;
using Word=Microsoft.Office.Interop.Word;
using WordTool = Microsoft.Office.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordAddIn.Utility;
using Common.Utility;
using System.IO;

namespace WordAddIn.ClassLib.WordAddinClass
{
    /// <summary>
    /// 合同文本下载
    /// </summary>
    public class ContractFinalProcess:WordAddinBase
    {
        /// <summary>
        /// 写入异常
        /// </summary>
        /// <param name="ex"></param>
        private static void WriteLog(Exception ex)
        {
            LogUtility.WriteLog(typeof(ContractFinalProcess), ex);

        }
        /// <summary>
        /// 写入异常在返回
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private static string LogToResult(Exception ex, string msg = "")
        {
            WriteLog(ex);
            return string.IsNullOrEmpty(msg) ? "ERR" : msg;
        }
        public override void InitWordStyle(AddInDataInfo addInDataInfo)
        {
            try
            {
                WordShare.WordApp.Visible = false;
                //MessageUtility.ShowMsg("ContractFinalProcess");
                base.WordAddinOpenDocument(false);
                if (WordShare.requestWordType == requestAddinWordType.savePdf)
                {
                    CreateDownloadPdf();

                }
                else
                {
                    //requestAddinWordType.saveWord
                    CreateDownloadWord();
                    WordShare.WordApp.ActiveDocument.Close();
                    //WordAddinComHelper.WordAddinUnload(WordShare.AddInCom_ProgId);
                    WordShare.WordApp.Quit();

                }
            }
            catch (Exception ex)
            {

                WriteLog(ex);
            }
        }


       /// <summary>
       /// 创建下载的Word文件
       /// </summary>
        private string  CreateDownloadWord()
        {
            //解锁
            WordDocumentHelper.DocProtectOrUnProtect(1);
            //去掉修改定状态文档
            string  filename = WordShare.WordApp.ActiveDocument.FullName;
            var document = WordDocumentHelper.GetVstoObjectDocm();
            WordShare.WordApp.ActiveDocument.ShowRevisions = false;
            WordShare.WordApp.ActiveDocument.ActiveWindow.View.ShowRevisionsAndComments = false;
            HandlerDocControls(document);
            DeleteDocStateBehavior(document);
            WordDocumentHelper.CurrentDocSave();
            ContTextDraftUntitly.ContTextRawSave(filename);//保存到服务器
           
            return filename;

        }
        /// <summary>
        /// 处理当前文档的控件
        /// </summary>
        /// <param name="document">当前文档</param>
        private static void HandlerDocControls(WordTool.Word.Document document)
        {
            foreach (Word.ContentControl nativeControl in document.ContentControls)
            {
                if (nativeControl.Range.Text == nativeControl.Title)
                {
                    nativeControl.LockContentControl = false;
                    nativeControl.LockContents = false;
                    nativeControl.Delete(true);

                }
                else if (nativeControl.Type == Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText)
                {
                    nativeControl.LockContentControl = false;
                    nativeControl.LockContents = false;
                    nativeControl.Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite;

                }
            }
        }
        /// <summary>
        /// 删除Word文档的一些状态行为
        /// </summary>
        /// <param name="document">当前word对象</param>
        private static void DeleteDocStateBehavior(WordTool.Word.Document document)
        {
            try
            {
                if (document.Comments.Count > 0)
                {
                    document.DeleteAllComments();//删除注释
                }
                if (document.Revisions.Count > 0)
                {
                    document.AcceptAllRevisions();//接受所有修订
                }

                document.DeleteAllInkAnnotations();//删除

                document.DeleteAllCommentsShown();


            }
            catch (Exception ex)
            {

                WriteLog(ex);
            }
        }


        /// <summary>
        /// 获取合同水印文件
        /// </summary>
        /// <param name="cttextid">合同文本ID</param>
        /// <param name="UserId">用户ID</param>
        /// <returns>合同文本路径</returns>
        public string GetContractWordWaterMark()
        {
            try
            {
                //var requestUrl = WordShare.BaseUrl + "/BLL/ModuleBusiness/Draft/Operation.aspx?cmd=/ContractAuthoring/getWordWaterMark";
                var requestUrl = WordShare.BaseUrl + "ContractDraft/ContTextDraft/GetWordWaterMark?cmd=/ContractAuthoring/getWordWaterMark";
                StringBuilder strb = new StringBuilder();
                strb.Append("&Vsto=true");
                //strb.Append("&cttextid=" + cttextid);
                var postdata = strb.ToString();
                var strfile = HttpRequestUtility.HttpPost(requestUrl, postdata);
                return strfile;
            }
            catch (Exception ex)
            {
                return LogToResult(ex);
                throw;
            }

        }

        private void CreateDownloadPdf() 
        {
            // Word.Document currentdocm;
            try
            {

                string filename = CreateDownloadWord();

                var writemark = GetContractWordWaterMark();
                var loadurl = WordShare.BaseUrl + writemark;//水印下载路径
                var tnk = System.DateTime.Now.Ticks.ToString();
                string tempPath = System.Environment.GetEnvironmentVariable("TEMP");
                string tempfile = string.Format("WriteMark_{0}.dotx", tnk);
                var markTemppath = Path.Combine(tempPath, tempfile);

                //下载水印文件到本地
                HttpWebRequestOptionFile.Download(loadurl, markTemppath);
                //pdf路径
                string pdfTempfile = string.Format("pdf_{0}.docx", tnk);
                var PdfTemppath = Path.Combine(tempPath, pdfTempfile);
                //当前水印合并的word
                string tempwordpath = string.Format("wordPath_{0}.docx", tnk);
                var wordPath = Path.Combine(tempPath, tempwordpath);
                var pdfresult = ConvertPDFToVsto(markTemppath, PdfTemppath, filename);
                if (pdfresult == "ok")
                {
                    ContTextDraftUntitly.SaveWordPdf(PdfTemppath);
                }
            }
            catch (Exception ex)
            {

                WriteLog(ex);
            }
        
        }

        /// <summary>
        /// 生成水印PDF
        /// </summary>
        /// <param name="templatePath">水印模板</param>
        /// <param name="savePath">保存PDF的路径</param>
        /// <param name="contextPath">合同文本</param>
        public string ConvertPDFToVsto(string templatePath, string savePath, string contextPath)
        {
            object missing = Type.Missing;
            // ApplicationClass wordApp = null;
            Microsoft.Office.Interop.Word.Application wordApp = null;
            Word.Document docNewWord = null;
            try
            {
                wordApp = WordShare.WordApp;

                //操作新合同文档
                docNewWord = wordApp.ActiveDocument;
                Word.Document docWaterMark = wordApp.Documents.Add(templatePath);

                //使用word模板添加构建基块
                Word.Template template = (Word.Template)docNewWord.get_AttachedTemplate();
                Word.Range templateHeaderRange = docNewWord.Sections[1].Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                Word.WdParagraphAlignment headerAlignment = templateHeaderRange.ParagraphFormat.Alignment;
                Word.Range waterHeaderRange = docWaterMark.Sections[1].Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                Word.BuildingBlock waterMark = template.BuildingBlockEntries.Add("newWaterMark", Word.WdBuildingBlockTypes.wdTypeWatermarks, "General", waterHeaderRange);

                //插入已添加到模板的水印生成块
                templateHeaderRange.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
                waterMark.Insert(templateHeaderRange, true);



                //页眉格式调整
                Word.Range rg = docNewWord.Sections[1].Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                var rText = rg.Text.Replace("\r", "").Replace("\n", "");
                var leng = rText.Length;
                if (leng > 0)
                {
                    docNewWord.Sections[1].Borders[Word.WdBorderType.wdBorderBottom].LineStyle = Word.WdLineStyle.wdLineStyleNone;
                    rg.ParagraphFormat.Alignment = headerAlignment;
                    rg.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
                    rg.MoveStart(Word.WdUnits.wdCharacter, 1);
                    rg.Delete(Word.WdUnits.wdCharacter, 1);
                }
               
                docNewWord.Save();
                docNewWord.ActiveWindow.View.Type = Word.WdViewType.wdNormalView;
                docNewWord.ActiveWindow.View.Type = Word.WdViewType.wdPrintView;
                docNewWord.ActiveWindow.View.ShowRevisionsAndComments = false;
                docNewWord.ExportAsFixedFormat(savePath, Word.WdExportFormat.wdExportFormatPDF);
                return "ok";
            }
            catch (Exception e)
            {
                LogUtility.WriteLog(typeof(WordAddinUtility), e);
                return "";
            }
            finally
            {
                if (docNewWord != null)
                {
                    object notsavechange = Word.WdSaveOptions.wdDoNotSaveChanges;
                    docNewWord.Close(ref notsavechange, ref missing, ref missing);
                }
                if (wordApp != null)
                {
                    wordApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
                }
            }
        }


    }
}
