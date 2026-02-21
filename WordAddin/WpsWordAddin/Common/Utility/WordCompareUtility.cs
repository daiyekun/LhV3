using Common.Model;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Word = Microsoft.Office.Interop.Word;


namespace Common.Utility
{
     /// <summary>
     /// Word文本比较
     /// </summary>
    public class WordCompareUtility
    {
       
        /// <summary>
        /// word详细比较，比较结果生成Word并打开
        /// </summary>
        /// <param name="sourceFile">源文件（修改前文件）</param>
        /// <param name="targetFile">目标文件（修改后文件）</param>
        /// <param name="compareReult">比较的结果路径</param>
        public static void CompareWordFile(string sourceFile, string targetFile, string compareReult)
        {
            object missing = System.Reflection.Missing.Value;
            object sFileName = sourceFile;
            var tFileName = targetFile;
            var wordApp = new Application();
            wordApp.Caption = "CompareWordApp";

            wordApp.Visible = false;

            var wordDoc = wordApp.Documents.Open(ref sFileName, ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing);

            wordDoc.TrackRevisions = true;
            wordDoc.ShowRevisions = true;
            wordDoc.PrintRevisions = true;

            object comparetarget = WdCompareTarget.wdCompareTargetNew;//比较结果新生成文件
            object addToRecentFiles = false;
            wordDoc.Compare(tFileName, ref missing, ref comparetarget, ref missing, ref missing, ref addToRecentFiles,
                ref missing, ref missing);

            // int changeCount = wordApp.ActiveDocument.Revisions.Count;
            object SaveToFormat = WdSaveFormat.wdFormatDocumentDefault;//保存文档方式
            object sFileName2 = compareReult;// @"E:\result.docx"; //wordApp.ActiveDocument.FullName;
            var wordApprest = wordApp;//new Application();
            
            wordApprest.Caption = "CompareWordApprest";
            wordApprest.Visible = true;
            wordApprest.ActiveDocument.SaveAs(sFileName2, ref SaveToFormat, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
            //加载插件
           WordAddinComHelper.WordAddinLoad(WordShare.AddInCom_ProgId);
           wordApprest.Documents.Close();
           //IOUtility.OpenWord(Convert.ToString(sFileName2));
           var wordDoc2 = wordApprest.Documents.Open(ref sFileName2, ref missing, ref missing, ref missing, ref missing, ref missing,
             ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
             ref missing, ref missing);
            //Object saveChanges = WdSaveOptions.wdDoNotSaveChanges;  //.wdSaveChanges;
            //wordDoc.Close(ref saveChanges, ref missing, ref missing);
            //wordApp.Quit(ref saveChanges, ref missing, ref missing);

            //KillWordProcess("CompareWordApp");
        }

        ///// <summary>
        ///// 重新加载插件
        ///// </summary>
        //private void AddinComReadLoad(Application app)
        //{
        //    foreach (Office.COMAddIn cAddin in app.COMAddIns)
        //    {

        //        if (cAddin.ProgId == WordShare.AddInCom_ProgId && !cAddin.Connect)
        //        {
        //            WordAddinComHelper.WordAddinLoad(WordShare.AddInCom_ProgId);
        //            //LogUtility.WriteLog(typeof(ThisAddIn), "cAddin.ProgId :" + cAddin.ProgId + ",状态：" + cAddin.Connect);
                  
                
        //        }

        //    }
        //}

        public static void KillWordProcess(string WinTitle)
        {
            string processName = "WINWORD";//"WINWORD";
            Process[] process = Process.GetProcessesByName(processName);
            try
            {
                foreach (Process p in process)
                {
                    if (p.MainWindowTitle.Contains(WinTitle))
                    {
                        p.Kill();

                    }
                }
            }
            catch (Exception)
            {

                //MessageBox.Show("请先关闭系统中的WINWORD.EXE进程!", "文件对比失败", MessageBoxButtons.OK);
                return;
            }
        }

        /// <summary>
        /// 插件开始比对文件
        /// </summary>
        public static void AddinCompareWordFile(QueryStringInfo Info)
        {

            WordDownloadUtility opword = new WordDownloadUtility();
            var firstFileurl = Info.baseAddr + opword.GetCurrentWordUrl(Info.fId, Info.uId, ContTextOption.contractTextCmp);
            var secondFileurl = Info.baseAddr + opword.GetCurrentWordUrl(Info.sId, Info.uId, ContTextOption.contractTextCmp);
            var firstFilePath = IOUtility.GetFilePath("firstword");
            var scondFilePath = IOUtility.GetFilePath("scondword");
            var compareReultWord = IOUtility.GetFilePath("comparereultword");
            //下载两文件到本地
            HttpWebRequestOptionFile.Download(firstFileurl, firstFilePath);
            HttpWebRequestOptionFile.Download(secondFileurl, scondFilePath);
            CompareWordFile(firstFilePath, scondFilePath, compareReultWord);



        }
    }
}
