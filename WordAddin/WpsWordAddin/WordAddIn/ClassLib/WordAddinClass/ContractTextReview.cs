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
                //CommandBarControlUtility.WirteControlLog();
                var listdata = VariableHelper.GetListSystemVarData(false, true);//系统变量
                WordDocumentHelper.SetRichTextContentValue(listdata, true, true);//绑定系统变量
                ReviewReviewSetWordStateWPS();
                WordReviewUtility.SetDocCommentEditState();

                //WordDocumentHelper.DocProtectOrUnProtect(1);
                WordDocumentHelper.DocProtectOrUnProtectSPEdit(1);

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
            WordBarsUtility.ExcuteCommbandBarMso("ReviewReviewingPane", false);
            WordBarsUtility.ExcuteCommbandBarMso("ReviewTrackChanges", false);
        }
        /// <summary>
        /// WPS执行
        /// </summary>
        public void ReviewReviewSetWordStateWPS()
        {
            //CommandBarControlUtility.WirteControlLog();
            WordDocumentHelper.DocProtectOrUnProtect(1);
            WordShare.WordApp.CommandBars["Reviewing"].Controls["审阅窗格(&P)"].Execute();
            WordShare.WordApp.ActiveDocument.TrackRevisions = true;
          

        }
    }
}
