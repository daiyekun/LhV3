using System;
using System.Collections.Generic;
using System.Text;
using Word;

namespace NF.OfficeUtility
{
    /// <summary>
    /// wps转换成pdf
    /// </summary>
    public class WpsWordToPdfHelper : IDisposable
    {
        public void Dispose()
        {


        }

        /// <summary>
        /// 生成PDF
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="targetPath"></param>
        /// <returns></returns>
        public static bool WordToPdf(string sourcePath,string targetPath)
        {
            ApplicationClass app = new ApplicationClass();
            Document doc = null;
            try
            {
                doc = app.Documents.Open(sourcePath,Visible:false);
                doc.ExportAsFixedFormat(targetPath,WdExportFormat.wdExportFormatPDF);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                doc.Close();
            }
            return true;
        }
    }
}
