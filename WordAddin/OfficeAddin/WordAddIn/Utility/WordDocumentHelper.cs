using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using WordTool=Microsoft.Office.Tools.Word;
using Common.Model;
using Tool = Microsoft.Office.Tools;
using System.Threading;

namespace WordAddIn.Utility
{
    /// <summary>
    /// word文档帮助类
    /// </summary>
    public class WordDocumentHelper
    {

        #region 工具方法
        /// <summary>
        /// 获取宿主文档对象
        /// </summary>
        /// <returns>宿主文档对象</returns>
        public static WordTool.Document GetVstoObjectDocm() 
        {
            Word.Document currentDocument = Globals.ThisAddIn.Application.ActiveDocument;
            return Globals.Factory.GetVstoObject(currentDocument);
        }
        /// <summary>
        /// 当前文档保存
        /// </summary>
        public static void CurrentDocSave()
        {
            Globals.ThisAddIn.Application.ActiveDocument.Save();
        }
        /// <summary>
        /// 提交文件保存
        /// </summary>
        public static void SaveSubmit() 
        { 
        
        }

        #endregion

        #region 创建内容控件

        /// <summary>
        /// 创建控件内容
        /// </summary>
        /// <param name="controlName">控件名称</param>
        /// <param name="tag">标示控件</param>
        /// <returns>控件ID</returns>
        public static string CreateRichTextContent(string controlName, string tag)
        {
            //获取当前文档对象
            //Word.Document currentDocument = Globals.ThisAddIn.Application.ActiveDocument;
            //currentDocument.Save();
            //获取该文档的宿主文档对象
            WordTool.Document document = GetVstoObjectDocm();
            //document.Save();
            //获取光标位置
            var cursorPosition = Globals.ThisAddIn.Application.Selection.Start;
            //获取插入区域
            var range = document.Range(cursorPosition, cursorPosition);
            //获取word页面的控件数量
            //var ctcount= document.ContentControls.Count+1;
            var contName = $"{controlName}_{DateTime.Now.Ticks}";
            //添加内容控件
            WordTool.RichTextContentControl d = document.Controls.AddRichTextContentControl(range,contName);//使用count数是防止添加同名控件报错。
            //设置控件空值时显示内容
            d.PlaceholderText = controlName;
            //设置控件标题
            d.Title = controlName;
            //赋值时通过此查找
            d.Tag = tag;
            //锁定控件
            d.LockContentControl = true;

            return d.ID;


        }
        
        #endregion

        ///// <summary>
        ///// 设置内容控件值
        ///// </summary>
        ///// <param name="dicValue">值列表</param>
        //public static void SetRichTextContentValue(Dictionary<string, Dictionary<string, string>> dicValue)
        //{
        //    Word.Document currentDocument = Globals.ThisAddIn.Application.ActiveDocument;
        //    WordTool.Document document = Globals.Factory.GetVstoObject(currentDocument);
        //    var dic0 = dicValue["1"];//系统字段
        //    var dic1 = dicValue["2"];//自定义字段
        //    foreach (Word.ContentControl nativeControl in document.ContentControls)
        //    {
        //        if (nativeControl.Type == Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText)
        //        {


        //            if (nativeControl.Tag == "1")
        //            {//系统字段
        //                nativeControl.Range.Text = dic0[nativeControl.ID];
        //                nativeControl.Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorViolet;//紫色
        //                //锁定内容
        //                nativeControl.LockContents = true;

        //            }
        //            else
        //            {//自定义字段
        //                nativeControl.Range.Text = dic1[nativeControl.ID];
        //                nativeControl.Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorDarkYellow;

        //            }

        //        }

        //    }

        //}

         /// <summary>
        /// 根据系统变量列表设置值
        /// </summary>
        /// <param name="ListData">系统变量列表</param>
        /// <param name="IsSP">审批及审阅时</param>
        public static void SetRichTextContentValue(IList<ContractVariable> ListData, bool IsSP = false,bool IsEditWord=false)
        {
            //AutoResetEvent[] watchers = new AutoResetEvent[1];
            //watchers[0] = new AutoResetEvent(false);
            DocProtectOrUnProtect(1);//取消文档保护
            var dict = ListData.ToDictionary(a => a.VarName, a => a.VarValue);       
            WordTool.Document document = GetVstoObjectDocm(); 
            foreach (Word.ContentControl nativeControl in document.ContentControls)
            {
                if (nativeControl.Type == Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText)
                {
                    if (dict.ContainsKey(nativeControl.Tag))
                    {
                        if (nativeControl.Tag != "27" && nativeControl.Tag != "38")
                        {
                            nativeControl.LockContentControl = false;
                            nativeControl.LockContents = false;
                            nativeControl.Range.Text = dict[nativeControl.Tag];
                            nativeControl.Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorViolet;
                            //锁定内容
                            nativeControl.LockContents = true;
                            nativeControl.LockContentControl = true;
                        }
                        else
                        {//标的明细和标的概要都执行
                         //启动一个线程去画表格
                         //ContTextDraftUntitly DraftUit = new ContTextDraftUntitly();
                         //DraftUit.ShowSubjectMatter(nativeControl);
                         //if (IsSP)
                         //{
                         //    nativeControl.LockContentControl = true;
                         //    nativeControl.LockContents = true;
                         //}
                         //if (IsSP && IsEditWord)
                         //{
                         //    DocProtectOrUnProtect(1);
                         //}
                         //else
                         //{
                         //    DocProtectOrUnProtect(0);
                         //}
                         //System.Threading.Thread t = new System.Threading.Thread(a =>
                         //{
                         //    ContTextDraftUntitly DraftUit = new ContTextDraftUntitly();
                         //    DraftUit.ShowSubjectMatter(nativeControl);
                         //    if (IsSP)
                         //    {
                         //        nativeControl.LockContentControl = true;
                         //        nativeControl.LockContents = true;
                         //    }
                         //    if (IsSP && IsEditWord)
                         //    {
                         //        DocProtectOrUnProtect(1);
                         //    }
                         //    else
                         //    {
                         //        DocProtectOrUnProtect(0);
                         //    }
                         //    // watchers[0].Set();

                            //});
                            //t.SetApartmentState(System.Threading.ApartmentState.STA);
                            //t.Start();
                            ContTextDraftUntitly DraftUit = new ContTextDraftUntitly();
                            DraftUit.ShowSubjectMatter(nativeControl);
                            if (IsSP)
                            {
                                nativeControl.LockContentControl = true;
                                nativeControl.LockContents = true;
                            }
                            if (IsSP && IsEditWord)
                            {
                                DocProtectOrUnProtect(1);
                            }
                            else
                            {
                                DocProtectOrUnProtect(0);
                            }









                        }
                    }
                    else
                    {
                        nativeControl.Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorYellow;
                        nativeControl.LockContentControl = false;
                        nativeControl.LockContents = false; 

                    }

                }

            }
           
            CurrentDocSave();
            if(!dict.ContainsKey("27")&& !dict.ContainsKey("38"))
            {
                //锁定文本
                DocProtectOrUnProtect(0);
            }
           

        }

        /// <summary>
        /// 去掉控件所有样式
        /// </summary>
        /// <param name="IsReView">true:预览</param>
        public static void SetRichTextContentValue(bool IsReView)
        {

            DocProtectOrUnProtect(1);//取消文档保护

            Word.Document currentDocument = Globals.ThisAddIn.Application.ActiveDocument;
            WordTool.Document document = Globals.Factory.GetVstoObject(currentDocument);
            foreach (Word.ContentControl nativeControl in document.ContentControls)
            {
                if (nativeControl.Type == Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText)
                {

                    nativeControl.LockContents = false;
                    nativeControl.LockContentControl = false;
                    nativeControl.Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite;
                    nativeControl.Range.Shading.ForegroundPatternColor = Word.WdColor.wdColorWhite;
                    nativeControl.LockContentControl = true;
                    nativeControl.LockContents = true;


                }

            }

            //if (!IsReView)
            //{
            //    WordBarsUtility.ExcuteCommbandBarMso("ReviewTrackChanges");
            //}


        }

        /// <summary>
        /// 文档锁定或者解锁
        /// </summary>
        /// <param name="isLock">true:锁定False:解锁</param>
        public static void LockDocumentControl(bool isLock=true)
        {

            DocProtectOrUnProtect(1);//取消文档保护

            Word.Document currentDocument = Globals.ThisAddIn.Application.ActiveDocument;
            WordTool.Document document = Globals.Factory.GetVstoObject(currentDocument);
            foreach (Word.ContentControl nativeControl in document.ContentControls)
            {
                if (nativeControl.Type == Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText)
                {

                    nativeControl.LockContents = isLock;
                    nativeControl.LockContentControl = isLock;
                   


                }

            }

        

        }

        /// <summary>
        /// 设置背景色
        /// </summary>
        /// <param name="IsCustomerVar">1:系统变量</param>
        private static Word.WdColor GetControlBak(int IsCustomerVar)
        {

            switch (WordShare.requestWordType)
            {
                case requestAddinWordType.TextDraft:
                    {
                        if (IsCustomerVar == 1)
                        { //系统变量
                            return Word.WdColor.wdColorViolet;
                        }
                        else
                        { //自定义变量
                            return Word.WdColor.wdColorYellow;
                        }


                    }
                default:
                    return Word.WdColor.wdColorWhite;



            }


        }


        #region 转化word成PDF

        public static void SaveWordToPdf(string FileName, string SaveType)
        {
            switch (SaveType)
            {
                case "PDF":
                case "pdf":
                    {
                        //导出为PDF格式 
                        Globals.ThisAddIn.Application.ActiveDocument.ExportAsFixedFormat(
                          FileName,
                          Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF);
                    }
                    break;
                case "XPS":
                case "xps":
                    {
                        // 导出为XPS格式 
                        Globals.ThisAddIn.Application.ActiveDocument.ExportAsFixedFormat(
                        FileName,
                        Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatXPS);

                    }
                    break;

            };


        }

        #endregion

        #region 加密解密
        /// <summary>
        /// 设置文档状态
        /// </summary>
        /// <param name="State">0：标识锁定，其他标识解锁</param>
        /// <param name="protType">
        /// 锁定时的状态：
        /// 比如：Word.WdProtectionType.wdAllowOnlyFormFields 标示文档锁定了，但是文档内容控件是可以编辑的
        /// </param>
        public static void DocProtectOrUnProtect(int State = 0, Word.WdProtectionType protType = Word.WdProtectionType.wdAllowOnlyComments)
        {      
                Object missing = Type.Missing;
                Object Password = WordShare.WordPassword;
                if (State == 0)
                {
                    DocProtect(protType, ref missing, ref Password);
                }
                else
                {

                    Password = DocUnprotect(Password);
                }
                WordShare.WordApp.ActiveDocument.Save();
            
           
        }

        /// <summary>
        /// 设置文档状态
        /// </summary>
        /// <param name="State">0：标识锁定，其他标识解锁</param>
        /// <param name="protType">
        /// 锁定时的状态：
        /// 比如：Word.WdProtectionType.wdAllowOnlyFormFields 标示文档锁定了，但是文档内容控件是可以编辑的
        /// </param>
        public  void DocProtectOrUnProtect2(int State = 0, Word.WdProtectionType protType = Word.WdProtectionType.wdAllowOnlyComments)
        {

            WordShare.WordApp.ActiveDocument.Save();

        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="protType">文档类型</param>
        /// <param name="missing">System.Type</param>
        /// <param name="Password">密码</param>
        private static void DocProtect(Word.WdProtectionType protType, ref Object missing, ref Object Password)
        {
            object objFalse = true;

            if (WordShare.WordApp.ActiveDocument.ProtectionType != Word.WdProtectionType.wdNoProtection)
            {
                WordShare.WordApp.ActiveDocument.Unprotect(ref Password);
            }
            //在加密
            WordShare.WordApp.ActiveDocument.Protect(protType, ref objFalse, ref Password, ref missing, ref missing);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Password"></param>
        /// <returns></returns>
        private static object DocUnprotect(Object Password)
        {
            if (WordShare.WordApp.ActiveDocument.ProtectionType != Word.WdProtectionType.wdNoProtection)
            {
                WordShare.WordApp.ActiveDocument.Unprotect(ref Password);

            }
            return Password;
        }

        #endregion
    }
}
