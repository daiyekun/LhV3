using Common.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using MSWord = Microsoft.Office.Interop.Word;

namespace WordAddIn.Utility
{
    public class WordCmp
    {
        //[DllImport("user32.dll")]
        //private static extern bool SetForegroundWindow(IntPtr hWnd);
        public static void CompWord(MSWord.Application msWordApp,string file1,string file2 )
        {
            
                object fullPath2 = file2;
                object fullPath1 = file1;
                msWordApp.Visible = false;
                msWordApp.Caption = "CompareWordApprest";
                object trueObj = (object)true;
                object falseObj = (object)false;
                object missingObj = Type.Missing;
                MSWord.Document msWordDocument1 = msWordApp.Documents.Open(ref fullPath1, ref missingObj, ref falseObj, ref falseObj,
                    ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj,
                    ref trueObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj);

                MSWord.Document msWordDocument2 = msWordApp.Documents.Open(ref fullPath2, ref missingObj, ref falseObj, ref falseObj,
                    ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj,
                    ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj);

                MSWord.Document msWordDocumentDiff = msWordApp.CompareDocuments(msWordDocument1, msWordDocument2,
                    MSWord.WdCompareDestination.wdCompareDestinationNew, MSWord.WdGranularity.wdGranularityWordLevel,
                    true, true, true, true, true, true, true, true, true, true, "", true);

                msWordDocument1.Close(ref missingObj, ref missingObj, ref missingObj);
                msWordDocument2.Close(ref missingObj, ref missingObj, ref missingObj);

                //
                //      Make sure Word is active and in the foreground
                //

                msWordApp.Visible = true;
                msWordApp.Activate();
                msWordApp.ActiveWindow.SetFocus();
            
            
           




            //MessageUtility.ShowMsg("比较晚");
            return;
        }
    }
}
