using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordAddIn.Utility;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordAddIn2010.Unttitily;
using Common.Utility;
using Word = Microsoft.Office.Interop.Word;

namespace WordAddIn.ClassLib.WordAddinClass
{
    /// <summary>
    /// 合同模板起草
    /// </summary>
    public class ContractTextDraft : WordAddinBase, IDocOption
    {

        /// <summary>
        /// 写入异常
        /// </summary>
        /// <param name="ex"></param>
        private static void WriteLog(Exception ex)
        {
            LogUtility.WriteLog(typeof(ContractTextDraft), ex);

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
        /// 初始化文档状态
        /// </summary>
        /// <param name="addInDataInfo">参数实体</param>
        public override void InitWordStyle(AddInDataInfo addInDataInfo)
        {
            try
            {

                base.WordAddinOpenDocument();
                if (WordShare.requestWordType == requestAddinWordType.TextDraft)
                {
                    base.AddTaskPane(addInDataInfo.taskPaneCollection);
                    SetCurrentDoc(base.VarData);
                }
                else if (WordShare.requestWordType == requestAddinWordType.conttext_readonly || WordShare.requestWordType == requestAddinWordType.history_readonly)
                {
                    WordShare.WordApp.ActiveDocument.ShowRevisions = true;
                    WordDocumentHelper.SetRichTextContentValue(true);
                    WordDocumentHelper.DocProtectOrUnProtect(0);
                    WordShare.WordApp.ActiveDocument.Save();
                }

            }
            catch (Exception ex)
            {

                WriteLog(ex);
            }
        }

        /// <summary>
        /// 设置当前的Word状态
        /// </summary>
        /// <param name="varData">当前变量相关数据</param>
        private void SetCurrentDoc(VariableData varData) 
        {
            try
            {
                //MessageUtility.ShowMsg("SetCurrentDoc");
                WordDocumentHelper.SetRichTextContentValue(varData.SysVariable);
                //WordDocumentHelper.DocProtectOrUnProtect(0);
            }
            catch (Exception ex)
            {

                WriteLog(ex);
            }
        }

       
        /// <summary>
        /// 保存Word
        /// </summary>
        /// <param name="wordFullName">Word文档全路径名称</param>
        /// <returns></returns>
        public  string WordSave(string wordFullName = "")
        {
            try
            {

                var document = WordDocumentHelper.GetVstoObjectDocm();
                var filename = document.FullName;
                WordDocumentHelper.DocProtectOrUnProtect(1);
                //获取当前文档所有批注
                var listComment = WordReviewUtility.GetComments(document);
                if (listComment.Count > 0)
                {
                    MessageBoxButtons messButton = MessageBoxButtons.YesNo;
                    DialogResult dr = MessageBox.Show("合同文本中有批注,是否删除原有批注?", "特别提示", messButton, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        //一定需要设置可以编辑，不然的删除异常句柄错误
                        //删除批注
                        WordReviewUtility.DeleteComments(listComment);

                    }


                }
                //保存
                // WordDocumentHelper.SetRichTextContentValue(false);
                WordDocumentHelper.CurrentDocSave();
                var result = ContTextDraftUntitly.ContTextSave(filename);
                WordDocumentHelper.DocProtectOrUnProtect(0);
                return result;
            }
            catch (Exception ex)
            {

                return LogToResult(ex);
            }
            
            
        }

    
    }
}
