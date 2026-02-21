using Excel;
using PowerPoint;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Word;

namespace NF.OfficeUtility
{
    /// <summary>
    /// Word转换成pdf
    /// </summary>
    public class WpsToPdfUtility
    {
        ///// <summary>
        ///// 是否杀死全部WPS程序
        ///// </summary>
        //public bool IsKillAllWps = false;
        ////Wps的动态对象
        //dynamic wps;
        ///// <summary>
        ///// 源文件路径
        ///// </summary>
        //public string FilePath { get; set; }
        //public void Dispose()
        //{
        //    if (wps != null)
        //    {
        //        wps.Quit();
        //        //释放掉wps对象
        //        wps = null;
        //        #region 强制关闭所有wps的功能慎用,尤其是带并发的
        //        //强制关闭所有wps进程,解决文件占用的问题
        //        if (this.IsKillAllWps)
        //        {
        //            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("wps");
        //            foreach (System.Diagnostics.Process prtemp in process)
        //            {
        //                prtemp.Kill();
        //            }
        //        }
        //        #endregion
        //    }
        //}

        ///// <summary>
        ///// 初始化类基础信息
        ///// </summary>
        ///// <param name="FilePath">文件路径</param>
        ///// <param name="IsKillAllWps">转换完成后是否杀死全部WPS应用</param>
        //public WpsToPdfUtility(string FilePath, bool IsKillAllWps = false)
        //{
        //    if (File.Exists(FilePath))
        //    {
        //        this.IsKillAllWps = IsKillAllWps;
        //        this.FilePath = FilePath;
        //        string Extension = Path.GetExtension(FilePath).ToLower();//扩展名 ".aspx"
        //        switch (Extension)
        //        {
        //            case "xls":
        //                Extension = "KET.Application";
        //                break;
        //            case "xlsx":
        //                Extension = "KET.Application";
        //                break;
        //            case "ppt":
        //                Extension = "KWPP.Application";
        //                break;
        //            case "pptx":
        //                Extension = "KWPP.Application";
        //                break;
        //            default:
        //                Extension = "KWps.Application";
        //                break;
        //        }
        //        Type type = Type.GetTypeFromProgID(Extension);
        //        if (type == null)
        //        {
        //            Extension = "wps.Application";
        //            type = Type.GetTypeFromProgID("wps.Application");
        //        }
        //        wps = Activator.CreateInstance(type);
        //        //比较完整的一些
        //        //WPS文字           KWPS.Aplication
        //        //WPS的Excel        KET.Application
        //        //WPS的演示文档     KWPP.Application
        //        //Word              Word.Application
        //        //Excel             Excel.Application
        //        //Powerpoint        Powerpoint.Application
        //    }
        //    else
        //    {
        //        throw new Exception("找不到原文件，请检查！");
        //    }
        //}


        ///// <summary>
        ///// 使用wps将Word转PDF
        ///// </summary>
        ///// <param name="TargetPath">目标文件路径，不传默认在源文件的所属目录</param>
        ///// <returns>Pdf文件路径</returns>
        //public string WordWpsToPdf(string TargetPath = "")
        //{
        //    if (string.IsNullOrEmpty(FilePath))
        //    {
        //        throw new Exception("请传入文件路径");
        //    }
        //    //如果没传入文件路径就默认使用源目录
        //    if (string.IsNullOrEmpty(TargetPath))
        //    {
        //        TargetPath = Path.ChangeExtension(FilePath, "pdf");
        //    }
        //    try
        //    {
        //        //忽略警告提示
        //        wps.DisplayAlerts = false;
        //        //用wps 打开word不显示界面
        //        dynamic doc = wps.Documents.Open(FilePath, Visible: false);
        //        //保存为Pdf
        //        doc.ExportAsFixedFormat(TargetPath, Word.WdExportFormat.wdExportFormatPDF);
        //        //设置隐藏菜单栏和工具栏
        //        //wps.setViewerPreferences(PdfWriter.HideMenubar | PdfWriter.HideToolbar);
        //        doc.Close();
        //        doc = null;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    finally
        //    {
        //        Dispose();
        //    }
        //    return TargetPath;
        //}
        ///// <summary>
        ///// 使用wps将xls转PDF
        ///// </summary>
        ///// <param name="TargetPath">目标文件路径，不传默认在源文件的所属目录</param>
        ///// <returns>Pdf文件路径</returns>
        //public string XlsWpsToPdf(string TargetPath = "")
        //{
        //    if (string.IsNullOrEmpty(FilePath))
        //    {
        //        throw new Exception("请传入文件路径");
        //    }
        //    //如果没传入文件路径就默认使用源目录
        //    if (string.IsNullOrEmpty(TargetPath))
        //    {
        //        TargetPath = Path.ChangeExtension(FilePath, "pdf");
        //    }
        //    try
        //    {
        //        XlFixedFormatType targetType = XlFixedFormatType.xlTypePDF;
        //        object missing = Type.Missing;
        //        //忽略警告提示
        //        wps.DisplayAlerts = false;
        //        //xls 转pdf
        //        dynamic doc = wps.Application.Workbooks.Open(FilePath, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
        //        //保存为Pdf
        //        doc.ExportAsFixedFormat(targetType, TargetPath, XlFixedFormatQuality.xlQualityStandard, true, false, missing, missing, missing, missing);
        //        //设置隐藏菜单栏和工具栏
        //        //wps.setViewerPreferences(PdfWriter.HideMenubar | PdfWriter.HideToolbar);
        //        doc.Close();
        //        doc = null;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    finally
        //    {
        //        Dispose();
        //    }
        //    return TargetPath;
        //}

        ///// <summary>
        ///// 使用ppt将xls转PDF
        ///// </summary>
        ///// <param name="TargetPath">目标文件路径，不传默认在源文件的所属目录</param>
        ///// <returns>Pdf文件路径</returns>
        //public string PptWpsToPdf(string TargetPath = "")
        //{
        //    if (string.IsNullOrEmpty(FilePath))
        //    {
        //        throw new Exception("请传入文件路径");
        //    }
        //    //如果没传入文件路径就默认使用源目录
        //    if (string.IsNullOrEmpty(TargetPath))
        //    {
        //        TargetPath = Path.ChangeExtension(FilePath, "pdf");
        //    }
        //    try
        //    {
        //        //忽略警告提示
        //        wps.DisplayAlerts = false;
        //        //ppt 转pdf
        //        dynamic doc = wps.Presentations.Open(FilePath, MsoTriState.msoCTrue,
        //            MsoTriState.msoCTrue, MsoTriState.msoCTrue);
        //        object missing = Type.Missing;
        //        //doc.ExportAsFixedFormat(pdfPath, PpFixedFormatType.ppFixedFormatTypePDF,
        //        //    PpFixedFormatIntent.ppFixedFormatIntentPrint,
        //        //    MsoTriState.msoCTrue, PpPrintHandoutOrder.ppPrintHandoutHorizontalFirst,
        //        //    PpPrintOutputType.ppPrintOutputBuildSlides,
        //        //      MsoTriState.msoCTrue, null, PpPrintRangeType.ppPrintAll,"",
        //        //      false, false, false, false, false, missing);
        //        //保存为Pdf
        //        doc.SaveAs(TargetPath, PowerPoint.PpSaveAsFileType.ppSaveAsPDF, MsoTriState.msoTrue);
        //        //设置隐藏菜单栏和工具栏
        //        //wps.setViewerPreferences(PdfWriter.HideMenubar | PdfWriter.HideToolbar);
        //        doc.Close();
        //        doc = null;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    finally
        //    {
        //        Dispose();
        //    }
        //    return TargetPath;
        //}




    }
}
