using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordAddIn.Utility;
using WordAddIn2010.Unttitily;
using Word = Microsoft.Office.Interop.Word;
using WordTool = Microsoft.Office.Tools.Word;
using Office = Microsoft.Office.Core;
using Common.Utility;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;

namespace WordAddIn.ClassLib.WordAddinClass
{
    /// <summary>
    /// 合同文本审阅
    /// </summary>
    public class ContractTextReview : WordAddinBase
    {
        /// <summary>
        /// 写入异常
        /// </summary>
        /// <param name="ex"></param>
        private static void WriteLog(Exception ex)
        {
            LogUtility.WriteLog(typeof(ContractTextReview), ex);

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
        /// <summary>
        /// 文档样式
        /// </summary>
        /// <param name="addInDataInfo">数据</param>
        public override void InitWordStyle(AddInDataInfo addInDataInfo)
        {
            try
            {
                base.WordAddinOpenDocument();
                WordReviewSetStyle();
            }
            catch (Exception ex)
            {
                WriteLog(ex);
                throw;
            }


        }
        /// <summary>
        /// 审阅时设置状态
        /// </summary>
        public void WordReviewSetStyle()
        {
            //MessageUtility.ShowMsg(WordShare.requestWordType.ToString());
            if (WordShare.requestWordType == requestAddinWordType.editable)
            {
               
                WordReviewEditable();
               
            }
            else
            {
                
                WordReviewReadOnly();
            }
        }
        /// <summary>
        /// 审阅时自读
        /// </summary>
        private void WordReviewReadOnly()
        {
            try
            {
                //MessageBox.Show("WordReviewReadOnly");
                // WordBarsUtility.ExcuteCommbandBarMso("ReviewReviewingPaneMenu", true);
                //审阅窗格左侧显示，wdPaneRevisionsHoriz底部显示
                WordShare.WordApp.ActiveDocument.ActiveWindow.View.SplitSpecial = WdSpecialPane.wdPaneRevisionsVert;
                WordReviewUtility.SetDocCommentEditState();
                WordDocumentHelper.DocProtectOrUnProtect(0);
                WordDocumentHelper.CurrentDocSave();
            }
            catch (Exception ex)
            {

                WriteLog(ex);
            }
        }
        /// <summary>
        /// 审阅时编辑
        /// </summary>
        private void WordReviewEditable()
        {
            try
            {
                var listdata = VariableHelper.GetListSystemVarData(false, true);//系统变量
                WordDocumentHelper.SetRichTextContentValue(listdata, true,true);//绑定系统变量
                ReviewReviewSetWordState();
                WordReviewUtility.SetDocCommentEditState();
                WordDocumentHelper.DocProtectOrUnProtect(1);
            }
            catch (Exception ex)
            {
                WordDocumentHelper.DocProtectOrUnProtect(1);
                WriteLog(ex);
            }
        }





        /// <summary>
        /// 设置审阅时的文本状态，ReviewTrackChangesMenu也是其中一个按钮
        /// </summary>
        public void ReviewReviewSetWordState()
        {
            WordDocumentHelper.DocProtectOrUnProtect(1);
            //WordBarsUtility.ExcuteCommbandBarMso("ReviewReviewingPane", true);
            WordBarsUtility.ExcuteCommbandBarMso("ReviewReviewingPaneMenu", true);
            //审阅窗格左侧显示，wdPaneRevisionsHoriz底部显示
            WordShare.WordApp.ActiveDocument.ActiveWindow.View.SplitSpecial = WdSpecialPane.wdPaneRevisionsVert;
            WordBarsUtility.ExcuteCommbandBarMso("ReviewTrackChanges", false);
        }
    }
}
