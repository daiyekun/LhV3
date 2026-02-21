using Microsoft.Office.Interop.Word;
using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace NF.OfficeComm
{
    /// <summary>
    /// word操作的一些工具类
    /// </summary>
   public class WordDocHiper
    {
        /// <summary>
        /// word加密密码
        /// </summary>
        public static string WordPassword = "95B13B93E52C7C1FD2D2A1F341844C71QAZ";

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="protType">文档类型</param>
        /// <param name="missing">System.Type</param>
        /// <param name="Password">密码</param>
        public static void DocProtect(Document wordDocm, WdProtectionType protType, ref Object missing, ref Object Password)
        {
            object objFalse = true;

            if (wordDocm.ProtectionType != WdProtectionType.wdNoProtection)
            {
                wordDocm.Unprotect(ref Password);
            }
            //在加密
            wordDocm.Protect(protType, ref objFalse, ref Password, ref missing, ref missing);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static object DocUnprotect(Document wordDocm, Object Password)
        {
            if (wordDocm.ProtectionType != WdProtectionType.wdNoProtection)
            {
                wordDocm.Unprotect(ref Password);

            }
            return Password;
        }
        /// <summary>
        /// 加密解密Word
        /// </summary>
        /// <param name="wordApp">word对象</param>
        /// <param name="State">状态</param>
        /// <param name="protType">类型</param>
        public static void DocProtectOrUnProtect(Document wordDocm, int State = 0,
            
            WdProtectionType protType = WdProtectionType.wdAllowOnlyComments
           )
        {
            Object missing = Type.Missing;
            Object Password = WordPassword;
            if (State == 0)
            {
                DocProtect(wordDocm, protType, ref missing, ref Password);
            }
            else
            {

                Password = DocUnprotect(wordDocm, Password);
            }
            wordDocm.Save();


        }

        /// <summary>
        /// 处理当前文档的控件
        /// </summary>
        /// <param name="document">当前文档</param>
        private static void HandlerDocControls(Document document)
        {
            foreach (ContentControl nativeControl in document.ContentControls)
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
                    nativeControl.Range.Shading.BackgroundPatternColor = WdColor.wdColorWhite;

                }
            }
        }
        /// <summary>
        /// 删除Word文档的一些状态行为
        /// </summary>
        /// <param name="document">当前word对象</param>
        private static void DeleteDocStateBehavior(Document document)
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

                Log4netHelper.Error(ex.ToString());
            }
        }
        /// <summary>
        /// 清理Word
        /// </summary>
        public static bool ClearWord(Document wordDocm )
        {

            try
            {
                //解密word
                DocProtectOrUnProtect(wordDocm, 1);
                //最终审阅版本
                wordDocm.ShowRevisions = false;
                wordDocm.ActiveWindow.View.ShowRevisionsAndComments = false;
                //处理控件
                HandlerDocControls(wordDocm);
                //删除批注内容
                DeleteDocStateBehavior(wordDocm);
                wordDocm.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
               
            }
            



        }

    }
}
