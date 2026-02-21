using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Word = Microsoft.Office.Interop.Word;

namespace Common.Utility
{
    /// <summary>
    /// Word操作类
    /// </summary>
   public class WordUtility
    {
        /// <summary>
        /// 打开Word
        /// </summary>
        /// <param name="wordFullName">word全路径</param>
       public static Word.Document OpenWord(string wordFullName, bool appVisible = true, string caption = "")
       {
           //创建word应用
           Word.Application wordApp = null;
           Word.Document wordDoc = null;
           try
           {
               object missing = System.Reflection.Missing.Value;
               object sFileName = wordFullName;
              //wordApp=(Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
                wordApp = new Word.Application();
               if (!string.IsNullOrEmpty(caption))
               {
                   wordApp.Caption = caption;
               }
               else
               {
                   wordApp.Caption = "WordAddin";
               }
               wordApp.Visible = appVisible;
                wordDoc = wordApp.Documents.Open(ref sFileName, ref missing, ref missing, ref missing, ref missing, ref missing,
                   ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                   ref missing, ref missing);
                  

                   return wordDoc;

           }
           catch (Exception ex)
           {
               if (wordApp != null)
               {
                   wordApp.Quit();
               }
               LogUtility.WriteLog(typeof(WordUtility), ex);
               return null;

           }

       }
       /// <summary>
       /// 新建建Word应用程序实例
       /// </summary>
       /// <param name="appVisible">是否显示窗体</param>
       /// <param name="caption">标题</param>
       public static Word.Application CreateWordApp(bool appVisible = true, string caption = "")
       {
           Word.Application wordApp = null;
           try
           {
              
               wordApp = new Word.Application();
             
               if (!string.IsNullOrEmpty(caption))
               {
                   wordApp.Caption = caption;
                   
               }
               else
               {
                   wordApp.Caption = "WordAddin";
               }
               wordApp.Visible = appVisible;
           }
           catch (Exception ex)
           {
               
               if (wordApp != null)
               {
                   wordApp.Quit();
               }
               LogUtility.WriteLog(typeof(WordUtility), ex);
              
           }

            return wordApp;
       }

        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        public static Word.Application GetWordApp()
        {
            return (Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
        }

       

         /// <summary>
        /// 打开Word
        /// </summary>
        /// <param name="wordPath">word全路径</param>
       public static void OpenWord2(string wordPath)
       {
           //创建word应用
           Word.Application app = new Word.Application();
           Word.Document doc = null;
           
          

               object unknow = System.Reflection.Missing.Value;
               object file = wordPath;
               doc = app.Documents.Open(ref file,
                    ref unknow, ref unknow, ref unknow, ref unknow,
                    ref unknow, ref unknow, ref unknow, ref unknow,
                    ref unknow, ref unknow, ref unknow, ref unknow,
                    ref unknow, ref unknow, ref unknow);
               
               app.Visible = true;

           

       }

       /// <summary>
       /// word详细比较，比较结果生成Word并打开
       /// </summary>
       /// <param name="sourceFile">源文件（修改前文件）</param>
       /// <param name="targetFile">目标文件（修改后文件）</param>
       /// <param name="compareReult">比较的结果路径</param>
       public static void CompareWordFile(string sourceFile, string targetFile, string compareReult)
       {
           object missing = System.Reflection.Missing.Value;
           var tFileName = targetFile;
          
           var wordDoc = OpenWord(sourceFile,appVisible:false,caption:"");
         
           wordDoc.TrackRevisions = true;
           wordDoc.ShowRevisions = true;
           wordDoc.PrintRevisions = true;

           object comparetarget = Word.WdCompareTarget.wdCompareTargetNew;// 比较结果重新创建
           object addToRecentFiles = false;
           wordDoc.Compare(tFileName, ref missing, ref comparetarget, ref missing, ref missing, ref addToRecentFiles,
               ref missing, ref missing);

           // int changeCount = wordApp.ActiveDocument.Revisions.Count;
           object SaveToFormat = Word.WdSaveFormat.wdFormatDocumentDefault;//保存文档方式
           object sFileName2 = compareReult;// @"E:\result.docx"; //wordApp.ActiveDocument.FullName;
           wordDoc.Application.ActiveDocument.SaveAs(sFileName2, ref SaveToFormat, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
           var wordDoc2 = wordDoc.Application.Documents.Open(ref sFileName2, ref missing, ref missing, ref missing, ref missing, ref missing,
             ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
             ref missing, ref missing);
           //Object saveChanges = WdSaveOptions.wdDoNotSaveChanges;  //.wdSaveChanges;
           //wordDoc.Close(ref saveChanges, ref missing, ref missing);
           //wordApp.Quit(ref saveChanges, ref missing, ref missing);

           // KillWordProcess("CompareWordApp");
       }
        

      
    }
}
