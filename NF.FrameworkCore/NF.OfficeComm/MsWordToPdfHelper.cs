using Microsoft.Office.Interop.Word;
using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using Word = Microsoft.Office.Interop.Word;

namespace NF.OfficeComm
{
   public class MsWordToPdfHelper
    {
        public static string WordPassword = "95B13B93E52C7C1FD2D2A1F341844C71QAZ";
        /// <summary>
        /// 将word文档转换成PDF格式
        /// </summary>
        /// <param name="sourcePath">源文件路径,word路径</param>
        /// <param name="targetPath">目标文件路径，pdf路径</param> 
        /// 
        /// <returns></returns>
        public  bool WordConvertPDF(string sourcePath, string targetPath)
        {
            bool result;
            object paramMissing = Type.Missing;
            ApplicationClass wordApplication = new ApplicationClass();
            Document wordDocument = null;
            try
            {
                object paramSourceDocPath = sourcePath;
                string paramExportFilePath = targetPath;

                WdExportFormat paramExportFormat = WdExportFormat.wdExportFormatPDF;
                bool paramOpenAfterExport = false;
                WdExportOptimizeFor paramExportOptimizeFor =
                        WdExportOptimizeFor.wdExportOptimizeForPrint;
                WdExportRange paramExportRange = WdExportRange.wdExportAllDocument;
                int paramStartPage = 0;
                int paramEndPage = 0;
                WdExportItem paramExportItem = WdExportItem.wdExportDocumentContent;
                bool paramIncludeDocProps = true;
                bool paramKeepIRM = true;
                WdExportCreateBookmarks paramCreateBookmarks =
                        WdExportCreateBookmarks.wdExportCreateWordBookmarks;
                bool paramDocStructureTags = true;
                bool paramBitmapMissingFonts = true;
                bool paramUseISO19005_1 = false;

                wordDocument = wordApplication.Documents.Open(
                        ref paramSourceDocPath, ref paramMissing, ref paramMissing,
                        ref paramMissing, ref paramMissing, ref paramMissing,
                        ref paramMissing, ref paramMissing, ref paramMissing,
                        ref paramMissing, ref paramMissing, ref paramMissing,
                        ref paramMissing, ref paramMissing, ref paramMissing,
                        ref paramMissing);

                if (wordDocument != null)
                    wordDocument.ExportAsFixedFormat(paramExportFilePath,
                            paramExportFormat, paramOpenAfterExport,
                            paramExportOptimizeFor, paramExportRange, paramStartPage,
                            paramEndPage, paramExportItem, paramIncludeDocProps,
                            paramKeepIRM, paramCreateBookmarks, paramDocStructureTags,
                            paramBitmapMissingFonts, paramUseISO19005_1,
                            ref paramMissing);
                result = true;
            }
            finally
            {
                if (wordDocument != null)
                {
                    wordDocument.Close(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordDocument = null;
                }
                if (wordApplication != null)
                {
                    wordApplication.Quit(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordApplication = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            return result;
            //return true;
        }

        //将word文档转换成PDF格式
        public bool ConvertWordToPdf(string sourcePath, string targetPath)
        {
            bool result;
            object paramMissing = Type.Missing;
            Word.ApplicationClass wordApplication = new Word.ApplicationClass();
            Document wordDocument = null;
            try
            {
                object paramSourceDocPath = sourcePath;
                string paramExportFilePath = targetPath;

                Word.WdExportFormat paramExportFormat = Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF;
                bool paramOpenAfterExport = false;
                Word.WdExportOptimizeFor paramExportOptimizeFor =
                        Word.WdExportOptimizeFor.wdExportOptimizeForPrint;
                Word.WdExportRange paramExportRange = Word.WdExportRange.wdExportAllDocument;
                int paramStartPage = 0;
                int paramEndPage = 0;
                Word.WdExportItem paramExportItem = Word.WdExportItem.wdExportDocumentContent;
                bool paramIncludeDocProps = true;
                bool paramKeepIRM = true;
                Word.WdExportCreateBookmarks paramCreateBookmarks =
                        Word.WdExportCreateBookmarks.wdExportCreateWordBookmarks;
                bool paramDocStructureTags = true;
                bool paramBitmapMissingFonts = true;
                bool paramUseISO19005_1 = false;

                wordDocument = wordApplication.Documents.Open(
                        ref paramSourceDocPath, ref paramMissing, ref paramMissing,
                        ref paramMissing, ref paramMissing, ref paramMissing,
                        ref paramMissing, ref paramMissing, ref paramMissing,
                        ref paramMissing, ref paramMissing, ref paramMissing,
                        ref paramMissing, ref paramMissing, ref paramMissing,
                        ref paramMissing);

                if (wordDocument != null)
                    wordDocument.ExportAsFixedFormat(paramExportFilePath,
                            paramExportFormat, paramOpenAfterExport,
                            paramExportOptimizeFor, paramExportRange, paramStartPage,
                            paramEndPage, paramExportItem, paramIncludeDocProps,
                            paramKeepIRM, paramCreateBookmarks, paramDocStructureTags,
                            paramBitmapMissingFonts, paramUseISO19005_1,
                            ref paramMissing);
                result = true;
            }
            finally
            {
                if (wordDocument != null)
                {
                    wordDocument.Close(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordDocument = null;
                }
                if (wordApplication != null)
                {
                    wordApplication.Quit(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordApplication = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            return result;
        }

        /// <summary>
        /// 生成水印PDF
        /// </summary>
        /// <param name="marktemplatePath">水印模板</param>
        /// <param name="savePath">保存PDF的路径</param>
        /// <param name="contextPath">合同文本</param>
        private bool ConvertPDFToVsto(string marktemplatePath, string savePath, string contextPath)
        {
           // Log4netHelper.Error("开始执行....ConvertPDFToVsto");
            object missing = Type.Missing;
            // ApplicationClass wordApp = null;
            Microsoft.Office.Interop.Word.Application wordApp = null;
            Word.Document docNewWord = null;
            try
            {
                wordApp = new Word.ApplicationClass();
                object paramSourceDocPath = contextPath;
                //操作新合同文档
                //
                docNewWord = wordApp.Documents.Open(
                       ref paramSourceDocPath, ref missing, ref missing,
                       ref missing, ref missing, ref missing,
                       ref missing, ref missing, ref missing,
                       ref missing, ref missing, ref missing,
                       ref missing, ref missing, ref missing,
                       ref missing);
                //清理Word
                WordDocHiper.ClearWord(docNewWord);
               // Log4netHelper.Error("开始执行....ClearWord完成");
                //Log4netHelper.Info($"marktemplatePath路径：{marktemplatePath}");
               
                    Word.Document docWaterMark = wordApp.Documents.Add(marktemplatePath);
               
               
               // Log4netHelper.Info($"marktemplatePath添加完毕...");

                //使用word模板添加构建基块
                Word.Template template = (Word.Template)docNewWord.get_AttachedTemplate();
                Word.Range templateHeaderRange = docNewWord.Sections[1].Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                Word.WdParagraphAlignment headerAlignment = templateHeaderRange.ParagraphFormat.Alignment;
                Word.Range waterHeaderRange = docWaterMark.Sections[1].Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                Word.BuildingBlock waterMark = template.BuildingBlockEntries.Add("newWaterMark", Word.WdBuildingBlockTypes.wdTypeWatermarks, "General", waterHeaderRange);
               // Log4netHelper.Info($"插入已添加到模板的水印生成块...");
                //插入已添加到模板的水印生成块
                templateHeaderRange.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
                waterMark.Insert(templateHeaderRange, true);
                

                //Log4netHelper.Info($"页眉格式调整...");
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
               // Log4netHelper.Info($"docNewWord.Save...");
                docNewWord.Save();
                docNewWord.ActiveWindow.View.Type = Word.WdViewType.wdNormalView;
                docNewWord.ActiveWindow.View.Type = Word.WdViewType.wdPrintView;
                docNewWord.ActiveWindow.View.ShowRevisionsAndComments = false;
                docNewWord.ExportAsFixedFormat(savePath, Word.WdExportFormat.wdExportFormatPDF);
               // Log4netHelper.Info($"ExportAsFixedFormat...");
                return true;
            }
            catch (Exception e)
            {
                Log4netHelper.Error(e.ToString());
                return false;
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



        /// <summary>
        /// 生成水印word
        /// </summary>
        /// <param name="marktemplatePath">水印模板</param>
        /// <param name="savePath">保存PDF的路径</param>
        /// <param name="contextPath">合同文本</param>
        private bool ConvertNewWordToVsto(string marktemplatePath, string savePath, string contextPath)
        {
            object missing = Type.Missing;
            // ApplicationClass wordApp = null;
            Microsoft.Office.Interop.Word.Application wordApp = null;
            Word.Document docNewWord = null;
            try
            {
                wordApp = new Word.ApplicationClass();
                object paramSourceDocPath = contextPath;
                //操作新合同文档
                //
                docNewWord = wordApp.Documents.Open(
                       ref paramSourceDocPath, ref missing, ref missing,
                       ref missing, ref missing, ref missing,
                       ref missing, ref missing, ref missing,
                       ref missing, ref missing, ref missing,
                       ref missing, ref missing, ref missing,
                       ref missing);
                //清理Word
                WordDocHiper.ClearWord(docNewWord);

                Word.Document docWaterMark = wordApp.Documents.Add(marktemplatePath);

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
                return true;
            }
            catch (Exception e)
            {
                Log4netHelper.Error(e.ToString());
                return false;
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



        /// <summary>
        /// word转换成pdf
        /// </summary>
        /// <param name="sourcePath">Word原始文件</param>
        /// <param name="targetPath">目标Word文件</param>
        /// <param name="markPath">水印文件</param>
        public bool ConvertWordToWrkPdf(string sourcePath, string targetPath,string markPath)
        {
            
            try
            {
               return ConvertPDFToVsto(markPath, targetPath, sourcePath);
            }
            catch (Exception ex)
            {
                Log4netHelper.Error("生成pdf ConvertWordToWrkPdf:"+ex.ToString());
                return false;
            }

        }


        /// <summary>
        /// word转换成word
        /// </summary>
        /// <param name="sourcePath">Word原始文件</param>
        /// <param name="targetPath">目标Word文件</param>
        /// <param name="markPath">水印文件</param>
        public bool ConvertNewWordToWrkWord(string sourcePath, string targetPath, string markPath)
        {

            try
            {
                return ConvertNewWordToVsto(markPath, targetPath, sourcePath);
            }
            catch (Exception ex)
            {
                Log4netHelper.Error(ex.ToString());
                return false;
            }

        }




    }
}
