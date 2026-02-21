using Word=Microsoft.Office.Interop.Word;
using WordTool=Microsoft.Office.Tools.Word;
using Office = Microsoft.Office.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Utility;
using Common.Model;
using WordAddIn.Utility;
using WordAddIn.ClassLib.WordAddinClass;

namespace WordAddIn2010.Unttitily
{
    /// <summary>
    /// 审阅
    /// </summary>
    public class WordReviewUtility
    {
        /// <summary>
        /// 文件保存
        /// </summary>
        /// <returns></returns>
        public static string ContTextSave(string strFileToUpload)
        {
            //请求路径
            var HttprequestUrl = WordShare.BaseUrl + "ContractDraft/ContractReview/SaveReviewedContractText";
            StringBuilder strb = new StringBuilder(HttprequestUrl);
            strb.Append("?uid=" + WordShare.UserId);//登录人ID
            strb.Append("&cttextid=" + WordShare.TempId);//合同文本ID
            var requst = HttpWebRequestOptionFile.MyUploader(strFileToUpload, strb.ToString());
            return requst;

       }
        /// <summary>
        /// 获取当前文档的批注集合
        /// </summary>
        /// <returns></returns>
        public static IList<Word.Comment> GetComments(WordTool.Document wordDoc) 
        {
            IList<Word.Comment> listComment = new List<Word.Comment>();
            foreach (Word.Comment wordComment in wordDoc.Comments)
            {
                listComment.Add(wordComment);
              
            }

            return listComment;

        
        }


        /// <summary>
        /// 设置,如果不是自己的批注。保存时需要去掉修改内容
        /// </summary>
        /// <param name="document"></param>
        public static void SetCommentRange(WordTool.Document document)
        {
            var listComment = WordReviewUtility.GetComments(document);
            foreach (var comminfo in listComment)
            {
                var author = comminfo.Author;
                Word.Range rage = comminfo.Range;
                var text = rage.Text;
                var date = comminfo.Date;
                var _index = comminfo.Index;
                if (author != WordShare.UserName)//表示不是当前用户新增批注
                {

                    foreach (Word.ContentControl nativeControl in rage.ContentControls)
                    {
                        if (nativeControl.Type == Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText)
                        {



                            var controlTxt = nativeControl.Range.Text;
                            if (controlTxt != text)
                            {
                                //去掉原有控件权限，不然comminfo.Range不允许编辑
                                nativeControl.LockContentControl = false;
                                nativeControl.LockContents = false;
                                //存储一个控件对象，其实不
                                VstoRichTextContentControl vstoTextContentControl = new VstoRichTextContentControl();
                                vstoTextContentControl.Tag = nativeControl.Tag;
                                vstoTextContentControl.ID = nativeControl.ID;
                                vstoTextContentControl.Title = nativeControl.Title;
                                vstoTextContentControl.Range = nativeControl.Range;
                                vstoTextContentControl.Text = nativeControl.Range.Text;
                                //vstoTextContentControl.PlaceholderText = Txtcontrol.PlaceholderText;
                                vstoTextContentControl.Tag = nativeControl.Tag;

                                nativeControl.Delete();
                                // var tempcontrol = nativeControl;
                                comminfo.Range.Text = "";
                                //创建控件
                                Word.ContentControl newNativeControl = comminfo.Range.ContentControls.Add();
                                newNativeControl.Tag = vstoTextContentControl.Tag;
                                newNativeControl.Title = vstoTextContentControl.Title;
                                newNativeControl.Range.Text = vstoTextContentControl.Text;
                                newNativeControl.LockContentControl = true;
                                newNativeControl.LockContents = true;

                            }


                        }
                    }




                }



            }
        }

        /// <summary>
        /// 审阅保存按钮
        /// </summary>
        /// <param name="control">按钮控件</param>
        public static void BtnReviewSave(Office.IRibbonControl control)
        {
           
            try
            {

               // var currentDocument = WordShare.WordApp.ActiveDocument;
                var temppath = WordShare.WordApp.ActiveDocument.FullName;
                WordTool.Document document = WordDocumentHelper.GetVstoObjectDocm();
                WordReviewUtility.SetCommentRange(document);
                WordDocumentHelper.DocProtectOrUnProtect(1);
                WordBarsUtility.ExcuteCommbandBarMso("ReviewTrackChanges");
                WordDocumentHelper.LockDocumentControl(false);//.SetRichTextContentValue(true);
                WordDocumentHelper.CurrentDocSave();
                var result =WordReviewUtility.ContTextSave(temppath);
                ContractTextReview contractTextReview = new ContractTextReview();
                WordDocumentHelper.LockDocumentControl(true);
               // contractTextReview.WordReviewSetStyle();
                WordBarsUtility.ExcuteCommbandBarMso("ReviewTrackChanges",false);

                MessageUtility.SaveMsg(result);
                WordDocumentHelper.DocProtectOrUnProtect(0);

            }
            catch (Exception ex)
            {

                LogUtility.WriteLog(typeof(WordReviewUtility), ex);
            }

        }

        #region 删除批注集合

        /// <summary>
        /// 删除批注
        /// </summary>
        /// <param name="listComment">批注集合</param>
        public static void DeleteComments(IList<Word.Comment> listComment)
        {
            foreach (var cominfo in listComment)
            {
                var rage = cominfo.Range;
                var count = rage.ContentControls.Count;
                if (count > 0)
                {
                    object cont_Index = 0;
                    //control = rage.ContentControls[ref cont_Index] as wordTool.RichTextContentControl;

                    foreach (Word.ContentControl nativeControl in rage.ContentControls)
                    {
                        if (nativeControl.Type == Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText)
                        {
                            nativeControl.LockContentControl = false;
                            nativeControl.LockContents = false;
                        }
                    }


                }
                cominfo.Delete();//删除


            }
        }
        #endregion


        #region 审阅加载数据
        /// <summary>
        /// 设置word批注状态
        /// </summary>
        public static void SetDocCommentEditState()
        {
           
            WordTool.Document document = WordDocumentHelper.GetVstoObjectDocm();
            //获取当前文档所有批注
            var listComment = WordReviewUtility.GetComments(document);
            foreach (var cominfo in listComment)
            {
                var author = cominfo.Author;
                Word.Range rage = cominfo.Range;
                var text = rage.Text;
                var date = cominfo.Date;
                var _index = cominfo.Index;


                if (cominfo.Author != WordShare.UserName)
                {


                    Word.ContentControl control = CreateCommentControl(document, author, rage, text, _index);
                    control.LockContentControl = true;
                    control.LockContents = true;

                }
                else
                {
                    Word.ContentControl control = null;
                    var count = rage.ContentControls.Count;
                    if (count > 0)
                    {
                        object cont_Index = 0;
                        //control = rage.ContentControls[ref cont_Index] as wordTool.RichTextContentControl;

                        foreach (Word.ContentControl nativeControl in rage.ContentControls)
                        {
                            if (nativeControl.Type == Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText)
                            {
                                nativeControl.LockContentControl = false;
                                nativeControl.LockContents = false;
                            }
                        }

                    }
                    else
                    {

                        control = CreateCommentControl(document, author, rage, text, _index);
                        control.LockContentControl = false;
                        control.LockContents = false;
                    }


                }


            }
            // currentdocm.Save();//保存本地
        }

        /// <summary>
        /// 创建控件
        /// </summary>
        /// <param name="document">当前文档wordTool.Document</param>
        /// <param name="author">批注作者</param>
        /// <param name="rage">批注Range</param>
        /// <param name="text">批注内容</param>
        /// <param name="_index">批注当前索引</param>
        public static Word.ContentControl CreateCommentControl(WordTool.Document document, string author, Word.Range rage, string text, int _index)
        {
            //var contName = author + _index;
            ////添加内容控件
            //wordTool.RichTextContentControl d = document.Controls.AddRichTextContentControl(rage,
            //    contName);
            ////设置控件空值时显示内容
            //d.PlaceholderText = text;
            ////设置控件标题
            //d.Title = author;
            ////赋值时通过此查找
            //d.Tag = author;
            ////锁定控件

            //return d;

            Word.ContentControl contentControl = rage.ContentControls.Add();
            //设置控件空值时显示内容
            contentControl.Range.Text = text;
            //设置控件标题
            contentControl.Title = author;
            //赋值时通过此查找
            contentControl.Tag = author;
            return contentControl;

        }
        #endregion

    }
}
