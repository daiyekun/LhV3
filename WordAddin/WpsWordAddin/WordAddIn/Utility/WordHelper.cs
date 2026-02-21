using Common.Model;
using Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MsWord = Microsoft.Office.Interop.Word;

namespace WordAddIn.Utility
{
    /// <summary>
    /// word帮助类
    /// </summary>
    public class WordHelper
    {
        //关闭文档事件
        public void CloseDocument()
        {
           // MessageUtility.ShowMsg("CloseDocument");
            
            // LogUtility.WriteLog(typeof(WordHelper), "执行了CloseDocument");
            //解除文本锁定
            ContTextDraftUntitly.UnLockContText();
            WordCompareUtility.KillWordProcess("CompareWordApprestOpen");



        }

        /// <summary>
        /// 内容控件被点击
        /// </summary>
        /// <param name="ContentControl">控件</param>
        public void ContentControlOnEnter(Microsoft.Office.Interop.Word.ContentControl ContentControl)
        {

            switch (WordShare.requestWordType)
            {
                case requestAddinWordType.TextDraft://模板起草
                    {
                        if (ContentControl != null && (ContentControl.Tag == "27" || ContentControl.Tag == "38"))
                        {
                            WordDocumentHelper.DocProtectOrUnProtect(1);
                        }
                        else
                        {
                            ControlOnEnterSetControlStyle(ContentControl);
                        }
                        SeletedVarToSelecdtListBoxItem(ContentControl);
                    }
                    break;
                default:

                    break;


            }

        }
        /// <summary>
        /// 当控件被点击的时候设置控件的样式/是否可以编辑
        /// </summary>
        /// <param name="ContentControl">当前控件</param>
        private static void ControlOnEnterSetControlStyle(Microsoft.Office.Interop.Word.ContentControl ContentControl)
        {
            WordDocumentHelper.DocProtectOrUnProtect(1);
            var listzdyitems = WordShare.lisBoxCustomVal.Items;
            IList<string> arraydata = new List<string>();
            foreach (var item in listzdyitems)
            {
                ContractVariable cat = (ContractVariable)item;
                arraydata.Add(cat.VarName);
            }

            if (arraydata.Contains(ContentControl.Tag))
            {
                ContentControl.Range.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorYellow;
                ContentControl.LockContents = false;
                ContentControl.LockContentControl = false;


            }
            WordDocumentHelper.DocProtectOrUnProtect(0, MsWord.WdProtectionType.wdAllowOnlyFormFields);
        }
        /// <summary>
        /// 当用户控件被选中的时候选中ListBox项
        /// </summary>
        /// <param name="ContentControl"></param>
        private static void SeletedVarToSelecdtListBoxItem(Microsoft.Office.Interop.Word.ContentControl ContentControl)
        {
            ListBox lisbox = WordShare.lisBoxSysVal;
            ListBox lisbox_zdy = WordShare.lisBoxCustomVal;
            if (ContentControl != null && ContentControl.Tag != "")
            {
                //系统
                foreach (ContractVariable item in lisbox.Items)
                {

                    if (item.VarName == ContentControl.Tag)
                    {
                        lisbox.SelectedItem = item;
                        break;

                    }


                }

                //自定义
                foreach (ContractVariable item in lisbox_zdy.Items)
                {

                    if (item.VarName == ContentControl.Tag)
                    {
                        lisbox_zdy.SelectedItem = item;
                        break;

                    }


                }
            }
        }
        /// <summary>
        /// 鼠标离开被点击的内容控件
        /// </summary>
        /// <param name="ContentControl"></param>
        /// <param name="Cancel"></param>
        public void ContentControlOnExit(Microsoft.Office.Interop.Word.ContentControl ContentControl, ref bool Cancel)
        {

            switch (WordShare.requestWordType)
            {
                case requestAddinWordType.TextDraft:
                    {//模板起草
                        //离开时立马锁定文档放置修改
                        WordDocumentHelper.DocProtectOrUnProtect(0);
                        //取消ListBox选中项
                        ListBox lisbox = WordShare.lisBoxSysVal;
                        lisbox.SelectedItems.Clear();
                        ListBox lisbox_zdy = WordShare.lisBoxCustomVal;
                        lisbox_zdy.SelectedItems.Clear();
                    }

                    break;
                //default:
                //    WordDocumentHelper.DocProtectOrUnProtect(0);
                //    break;

            }

        }

    }
}
