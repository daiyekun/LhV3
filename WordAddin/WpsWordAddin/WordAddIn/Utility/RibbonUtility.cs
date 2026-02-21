using Common.Model;
using Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordAddIn.ClassLib;
using Office = Microsoft.Office.Core;

namespace WordAddIn.Utility
{
   /// <summary>
   /// Ribbon功能设计器
   /// </summary>
    public class RibbonUtility
    {

        /// <summary>
        /// 显示或者隐藏panel
        /// </summary>
        public void ShowHidePanel()
        {
            if (Share.customTaskPane.Visible)

                Share.customTaskPane.Visible = false;
            else
                Share.customTaskPane.Visible = true;
        }

        /// <summary>
        /// 判断是否显示控件
        /// </summary>
        /// <param name="control">控件ID-idMso</param>
        /// <returns>Tru显示：false不显示</returns>
        public bool GetRibbonContractVisble(Office.IRibbonControl control)
        {
            bool result = true;//隐藏
            if (WordShare.UserId > 0 && WordShare.IsWordToLogin)
            {
                switch (WordShare.requestWordType)
                {
                    case requestAddinWordType.TplonreadOrwrite:
                        result = this.OnReadOrwriteVisible(control, result);
                        break;
                    case requestAddinWordType.Tplonreadonly:
                        result = this.OnReadonlyVisible(control, result);
                        break;
                    case requestAddinWordType.TextDraft:
                        result = this.MoBanQicaoVisible(control, result);
                        break;
                    case requestAddinWordType.conttext_readonly:
                    case requestAddinWordType.history_readonly:
                        result = this.OnReadonlyVisible(control, result);
                        break;
                    case requestAddinWordType.editable://审阅文本可以修改合同文本
                        result = this.EditableVisible(control, result);
                        break;
                    case requestAddinWordType.contractReview://审阅但不修改文本
                        result = ReviewNoEditVisible(control, result);
                        break;
                    case requestAddinWordType.wdCompare:
                        result = WdCompareVisible(control, result);
                        break;
                    default:
                        break;
                }

            }
            else
            {
                if (control.Id == "Tab_Lh_Mbqc")
                    result = false;
                else
                    result = true;//如果不是插件进入
            }

            return result;

        }


        #region 不同操作状态下对Word工具栏布局

        /// <summary>
        /// 比对合同文本
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="result">状态</param>
        /// <returns>返回Flase显示，true隐藏</returns>
        private bool WdCompareVisible(Office.IRibbonControl control, bool result)
        {
            //MessageUtility.ShowMsg("WdCompareVisible");
            switch (control.Id)
            {
                case "TabHome"://开始
                case "TabInsert"://插入
                case "TabPageLayoutWord"://页面布局
                case "TabReferences"://引用
                case "TabMailings"://邮件
                case "TabReviewWord"://审阅
                case "TabView"://视图
                case "TabOutlining"://未知
                case "TabDeveloper"://开发工具
                case "Lh_CTRevisions"://修改定模板
                case "Lh_Mbqc"://合同起草
                case "Lh_GroupComments"://审阅
                case "Lh_CTReview"://合同文本审批
                    result = false;
                    break;
                case "Tab_Lh_Mbqc"://罗合总标签
                case "RibbonControl_ReturnVisible":
                case "CG_GroupCompare"://比较
                    result = true;
                    break;



            }
            return result;

        }

        /// <summary>
        /// 读写-审阅时，审批人可以修改模板
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="result">状态</param>
        /// <returns>返回Flase显示，true隐藏</returns>
        private bool ReviewNoEditVisible(Office.IRibbonControl control, bool result)
        {
            switch (control.Id)
            {
                case "TabHome"://开始
                case "TabInsert"://插入
                case "TabPageLayoutWord"://页面布局
                case "TabReferences"://引用
                case "TabMailings"://邮件
                case "TabReviewWord"://审阅
                case "TabView"://视图
                case "TabOutlining"://未知
                case "TabDeveloper"://开发工具
                case "Lh_CTRevisions"://修改定模板
                case "RibbonControl_ReturnVisible"://比较时
                case "CG_GroupCompare":
                    {

                        result = false;//隐藏
                    }
                    break;
                case "Tab_Lh_Mbqc"://罗合总标签
                    {
                        result = true;//显示

                    }
                    break;
                case "Lh_Mbqc"://合同起草
                    result = false;
                    break;
                case "Lh_GroupComments"://审阅
                case "Lh_CTReview"://合同文本审批
                    {
                        
                        result = true;//显示
                       
                    }
                    break;



            }
            return result;
        }

        /// <summary>
        /// 读写-审阅时，审批人可以修改模板
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="result">状态</param>
        /// <returns>返回Flase显示，true隐藏</returns>
        private bool EditableVisible(Office.IRibbonControl control, bool result)
        {
            switch (control.Id)
            {
                case "TabHome"://开始
                case "TabInsert"://插入
                case "TabPageLayoutWord"://页面布局
                case "TabReferences"://引用
                case "TabMailings"://邮件
                case "TabReviewWord"://审阅
                case "TabView"://视图
                case "TabOutlining"://未知
                case "TabDeveloper"://开发工具
                case "RibbonControl_ReturnVisible"://比较文本
                case "CG_GroupCompare":
                    {

                        result = false;//隐藏
                    }
                    break;
                case "Tab_Lh_Mbqc"://罗合总标签
               
                    {
                        result = true;//显示

                    }
                    break;
                case "Lh_Mbqc"://合同起草
                    result = false;
                    break;
                case "Lh_GroupComments"://审阅
                case "Lh_CTReview"://合同审批
                case "Lh_CTRevisions"://修改定模板
                    {
                        result = true;//显示

                    }
                    break;
                



            }
            return result;
        }
        /// <summary>
        /// 读写-只读时
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="result">状态</param>
        /// <returns>返回Flase显示，true隐藏</returns>
        private bool OnReadonlyVisible(Office.IRibbonControl control, bool result)
        {
            switch (control.Id)
            {
                case "TabHome"://开始
                case "TabInsert"://插入
                case "TabPageLayoutWord"://页面布局
                case "TabReferences"://引用
                case "TabMailings"://邮件
                case "TabReviewWord"://审阅
                case "TabView"://视图
                case "TabOutlining"://未知
                case "TabDeveloper"://开发工具
                case "Tab_Lh_Mbqc"://合同起草
                case "Lh_GroupComments"://审阅
                case "Lh_CTRevisions"://修改定模板
                case "Lh_CTReview"://合同审批
                case "RibbonControl_ReturnVisible"://比较文本
                case "CG_GroupCompare":
                    {
                        result = false;//隐藏

                    }

                    break;

            }
            return result;
        }
        /// <summary>
        /// 模板起草
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="result">状态</param>
        /// <returns>返回Flase显示，true隐藏</returns>
        private bool MoBanQicaoVisible(Office.IRibbonControl control, bool result)
        {
            switch (control.Id)
            {
                case "TabHome"://开始
                case "TabInsert"://插入
                case "TabPageLayoutWord"://页面布局
                case "TabReferences"://引用
                case "TabMailings"://邮件
                case "TabReviewWord"://审阅
                case "TabView"://视图
                case "TabOutlining"://未知
                case "TabDeveloper"://开发工具
                case "Tab_Lh_Mbqc"://合同起草
                case "Lh_Mbqc"://合同起草
                case "RibbonControl_ReturnVisible"://比较文本
                    {
                        result = true;//显示
                    }
                    break;
                case "Lh_GroupComments"://审阅
                case "Lh_CTRevisions"://修改定模板
                case "Lh_CTReview"://合同审批
                case "CG_GroupCompare":
                case "but_design"://罗控件下-设计
                case "BtnSaveDocReview"://罗控件下-保存
                    {
                        result = false;//隐藏

                    }

                    break;

            }
            return result;
        }
        /// <summary>
        /// 读写-比如建立模板时
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="result">状态</param>
        /// <returns>返回Flase显示，true隐藏</returns>
        private bool OnReadOrwriteVisible(Office.IRibbonControl control, bool result)
        {
            switch (control.Id)
            {  
               
                case "TabHome"://开始
                case "TabInsert"://插入
                case "TabPageLayoutWord"://页面布局
                case "TabReferences"://引用
                case "TabMailings"://邮件
                case "TabReviewWord"://审阅
                case "TabView"://视图
                case "TabOutlining"://未知
                case "TabDeveloper"://开发工具
                case "Tab_Lh_Mbqc"://合同起草
                case "RibbonControl_ReturnVisible"://比较文本
                    {
                        result = true;//显示
                    }
                    break;
                case "Lh_GroupComments"://审阅
                case "Lh_CTRevisions"://修改定模板
                case "Lh_CTReview"://合同审批
                case "BtnSaveDocReview"://保存
                case "BuildingBlocksSaveCoverPage"://保存和BtnSaveDocReview是同一个按钮
                case "CG_GroupCompare":
                    {
                        result = false;//隐藏

                    }

                    break;

            }
            return result;
        }

        #endregion

    }
}
