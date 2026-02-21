using Common.Model;
using Common.Utility;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using WordAddIn.Utility;

namespace WordAddIn.ClassLib.WordAddinClass
{
    /// <summary>
    /// 合同文本对比
    /// </summary>
    public class ContractTextCmp : WordAddinBase
    {
        
        /// <summary>
        /// 写入异常
        /// </summary>
        /// <param name="ex"></param>
        private static void WriteLog(Exception ex)
        {
            LogUtility.WriteLog(typeof(ContractTextCmp), ex);

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
                base.WordAddinOpenDocument();
                // ComWord();
                //CompareWord();

            }
            catch (Exception ex)
            {

                WriteLog(ex);
            }
            
        }

        private void ComWord()
        {
            var doc = GetFirstWord();
        }
        private Document GetFirstWord()
        {
            
            WordDownloadUtility opwordUtility = new WordDownloadUtility();
            var firstFileurl = WordShare.BaseUrl + opwordUtility.GetCurrentWordUrl(WordShare.TempId, WordShare.UserId, ContTextOption.contractTextCmp);
            var firstFilePath = IOUtility.GetFilePath("firstword");
            HttpWebRequestOptionFile.Download(firstFileurl, firstFilePath);
            object missing = System.Reflection.Missing.Value;
            var wordApp = WordShare.WordApp;
            wordApp.Caption = "CompareWordApp";
            wordApp.Visible = true;
            object sFileName = firstFilePath;
            var sdoc=wordApp.Documents.Open(ref sFileName, ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing);

            return sdoc;
        }

        #region 合同文本比较1

        private void  CompareWord() 
        {
            WordDownloadUtility opwordUtility = new WordDownloadUtility();
            var firstFileurl = WordShare.BaseUrl + opwordUtility.GetCurrentWordUrl(WordShare.TempId, WordShare.UserId, ContTextOption.contractTextCmp);
            var secondFileurl = WordShare.BaseUrl + opwordUtility.GetCurrentWordUrl(WordShare.CompareTextId, WordShare.UserId, ContTextOption.contractTextCmp);
            var firstFilePath = IOUtility.GetFilePath("firstword");
            var scondFilePath = IOUtility.GetFilePath("scondword");
            var compareReultWord = IOUtility.GetFilePath("comparereultword");
            //下载两文件到本地
            HttpWebRequestOptionFile.Download(firstFileurl, firstFilePath);
            HttpWebRequestOptionFile.Download(secondFileurl, scondFilePath);
            MessageUtility.ShowMsg("11");
            WordCmp.CompWord(WordShare.WordApp,firstFilePath, scondFilePath);
            //CompareWordFile(firstFilePath, scondFilePath, compareReultWord);
            //LogUtility.WriteDebug(typeof(ContractTextCmp),compareReultWord);
            //return compareReultWord;
            
        }
        


        /// <summary>
        /// word详细比较，比较结果生成Word并打开
        /// </summary>
        /// <param name="sourceFile">源文件（修改前文件）</param>
        /// <param name="targetFile">目标文件（修改后文件）</param>
        /// <param name="compareReult">比较的结果路径</param>
        private static void CompareWordFile(string sourceFile, string targetFile, string compareReult)
        {
            Document sourcedoc = null;
            Document CompareResult = null;
            object missing = System.Reflection.Missing.Value;
            try
            {

                object sFileName = sourceFile;
                var tFileName = targetFile;
                var wordApp = WordShare.WordApp;
                wordApp.Caption = "CompareWordApp";

                wordApp.Visible = false;

                var wordDoc = wordApp.Documents.Open(ref sFileName, ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing);

                wordDoc.TrackRevisions = true;
                wordDoc.ShowRevisions = true;
                wordDoc.PrintRevisions = true;
                WordShare.WordApp.ActiveDocument.Save();
                sourcedoc = WordShare.WordApp.ActiveDocument;
                var sourceName = WordShare.WordApp.ActiveDocument.Name;

                object comparetarget = WdCompareTarget.wdCompareTargetNew;//比较结果新生成文件
                object addToRecentFiles = false;
                wordDoc.Compare(tFileName, ref missing, ref comparetarget, ref missing, ref missing, ref addToRecentFiles,
                    ref missing, ref missing);


                object SaveToFormat = WdSaveFormat.wdFormatDocumentDefault;//保存文档方式
                object sFileName2 = compareReult;// @"E:\result.docx"; //wordApp.ActiveDocument.FullName;
                var wordApprest = wordApp;
                wordApprest.Caption = "CompareWordApprest";
                wordApprest.Visible = true;
                wordApprest.ActiveDocument.SaveAs(sFileName2, ref SaveToFormat, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
                foreach (Document doc in WordShare.WordApp.Documents)
                {
                    if (doc.Name == sourceName)
                    {
                        Object saveChanges = WdSaveOptions.wdDoNotSaveChanges;  //.wdSaveChanges;
                        doc.Close(ref saveChanges, ref missing, ref missing);
                    }

                }
                CompareResult = wordApprest.Documents.Open(ref sFileName2, ref missing, ref missing, ref missing, ref missing, ref missing,
                  ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                  ref missing, ref missing);

                
            }
            catch (Exception ex)
            {

                Object saveChanges = WdSaveOptions.wdDoNotSaveChanges;  //.wdSaveChanges;
                if (sourcedoc != null)
                {
                    sourcedoc.Close(ref saveChanges, ref missing, ref missing);
                }
                if (CompareResult != null)
                {
                    CompareResult.Close(ref saveChanges, ref missing, ref missing);
                }
                if (WordShare.WordApp != null)
                {
                    WordShare.WordApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(WordShare.WordApp);
                }

                LogUtility.WriteLog(typeof(ContractTextCmp), ex);
            }

        }

        #endregion





        /// <summary>
        /// 重新创建新的App
        /// </summary>
        public static bool CompareCreateApp(string requesturl)
        {
            WordCompareUtility.KillWordProcess("CompareWordApp");//杀死进程
           WordAddinRequestUtility addinRequestUtility = new WordAddinRequestUtility();
            return addinRequestUtility.WordAddinRequestHandle(requesturl);
        }

       
    }
}
