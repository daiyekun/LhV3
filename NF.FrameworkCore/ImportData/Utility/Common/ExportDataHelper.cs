using NF.Common.Models;
using NF.Common.Utility;
using NF.ViewModel.Extend.Enums;
using NF.ViewModel.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NF.Web.Utility.Common
{
    /// <summary>
    /// 导出数据帮助类
    /// </summary>
    public class ExportDataHelper
    {
        /// <summary>
        /// 导出数据到Excel
        /// </summary>
        /// <typeparam name="T">当前实体对象</typeparam>
        /// <param name="exportRequestInfo">请求导出Excel对象</param>
        /// <param name="fileName">导出文件名称及Excel工作簿名称</param>
        /// <param name="listData">导出数据集合</param>
        /// <returns>返回导出文件对象</returns>
        public static DownLoadInfo ExportExcelExtend<T>(ExportRequestInfo exportRequestInfo, string fileName, IList<T> listData)
            where T : class, INfEntityHandle
        {
          
            FileInfo file = CreateExportFile(fileName);
            //EPPlus 是商业版本，需要再次指定非商业化，如果有兴趣可以手动手改组件
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(file))
            {
                // 添加worksheet
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(fileName);
                var coltitles = exportRequestInfo.GetCellsTitleList();
                var colfields = exportRequestInfo.GetCellsFieldList();
                //绑定头
                BindExcelHeader(worksheet, coltitles);
                //绑定数据
                BindExcelData(listData, worksheet, colfields);


                worksheet.Cells.AutoFitColumns();//自动列宽
                package.Save();


            }

            return FileStreamingHelper.Download(file.FullName);


        }
        /// <summary>
        /// 根据名称返回需要导出的文件对象
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns>FileInfo：文件信息对象</returns>
        private static FileInfo CreateExportFile(string fileName)
        {
           var  pathf = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot", "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), 3),
                            fileName + ".xlsx");
            var dicpathf = Path.Combine(
              Directory.GetCurrentDirectory(), "wwwroot", "Uploads", EmunUtility.GetDesc(typeof(UploadAndDownloadFoldersEnum), 3));

            if (!Directory.Exists(dicpathf))
            {
                Directory.CreateDirectory(dicpathf);
            }
            FileInfo file = new FileInfo(pathf);
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(pathf);
            }

            return file;
        }

        /// <summary>
        /// 绑定Excel数据
        /// </summary>
        /// <typeparam name="T">绑定对象</typeparam>
        /// <param name="listData">数据集合</param>
        /// <param name="worksheet">Excel操作对象</param>
        /// <param name="colfields">绑定字段</param>
        private static void BindExcelData<T>(IList<T> listData, ExcelWorksheet worksheet, IList<string> colfields) where T : class, INfEntityHandle
        {
            int rowindex = 2;
            foreach (var item in listData)
            {
                var colindex = 1;
                foreach (var col in colfields)
                {
                    
                        var info = item.GetPropValue(col);
                        if (info.FileType==typeof(DateTime)|| info.FileType == typeof(DateTime?))
                        {
                          worksheet.Cells[rowindex, colindex].Style.Numberformat.Format = "yyyy-MM-dd";
                        }
                        worksheet.Cells[rowindex, colindex].Value = info.FileValue;
                        
                    
                    
                    colindex++;
                }

                rowindex++;

            }
        }

        /// <summary>
        /// 绑定列头
        /// </summary>
        /// <param name="worksheet">Excel操作对象</param>
        /// <param name="coltitles">列头集合</param>
        private static void BindExcelHeader(ExcelWorksheet worksheet, IList<string> coltitles)
        {
            int column = 1;//列
            foreach (string ctitle in coltitles)
            {
                worksheet.Cells[1, column].Value = ctitle;
                worksheet.Cells[1, column].Style.Font.Bold = true;
                worksheet.Cells[1, column].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;//水平居中
                worksheet.Cells[1, column].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;//设置样式类型
                worksheet.Cells[1, column].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(159, 197, 232));//设置单元格背景色

                column++;
            }
        }
    }
    
}
