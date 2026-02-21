using Common.Model;
using Common.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WordAddIn.Utility;
using WordAddIn2010.Unttitily;
using Office = Microsoft.Office.Core;
using Word = Microsoft.Office.Interop.Word;
using WordTool = Microsoft.Office.Tools.Word;

// TODO: 按照以下步骤启用功能区(XML)项:

// 1. 将以下代码块复制到 ThisAddin、ThisWorkbook 或 ThisDocument 类中。

//  protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
//  {
//      return new RibbonCustom();
//  }

// 2. 在此类的“功能区回调”区域中创建回调方法，以处理用户
//    操作(如单击某个按钮)。注意: 如果已经从功能区设计器中导出此功能区，
//    则将事件处理程序中的代码移动到回调方法并修改该代码以用于
//    功能区扩展性(RibbonX)编程模型。

// 3. 向功能区 XML 文件中的控制标记分配特性，以标识代码中的相应回调方法。

// 有关详细信息，请参见 Visual Studio Tools for Office 帮助中的功能区 XML 文档。


namespace WordAddIn
{
    [ComVisible(true)]
    public class RibbonCustom : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI ribbon;
        /// <summary>
        /// Ribbon设计帮助类
        /// </summary>
        //private RibbonUtility rb = new RibbonUtility();

        public RibbonCustom()
        {
        }

        #region IRibbonExtensibility 成员

        public string GetCustomUI(string ribbonID)
        {
            try
            {
                return GetResourceText("WordAddIn.RibbonCustom.xml");
            }
            catch (Exception ex)
            {

                LogUtility.WriteLog(typeof(RibbonCustom), ex);
                return "";
            }
        }

        #endregion

        #region 功能区回调
        //在此创建回调方法。有关添加回调方法的详细信息，请在解决方案资源管理器中选择功能区 XML 项，然后按 F1

        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {

            try
            {
               this.ribbon = ribbonUI;
               this.ribbon.ActivateTab("Tab_Lh_Mbqc");
            }
            catch (Exception ex)
            {

                LogUtility.WriteLog(typeof(RibbonCustom), ex);
            }
           
            
            
        }
         /// <summary>
        /// 判断控件是否显示
        /// </summary>
        /// <param name="control">当前控件</param>
        /// <returns>Tre：显示，False:不显示</returns>
        public bool RibbonControl_ReturnVisible(Office.IRibbonControl control)
        {
            try
            {
                LogUtility.WriteLog(typeof(RibbonCustom), "控件："+control.Id);
                RibbonUtility rb = new RibbonUtility();
                var reslt = rb.GetRibbonContractVisble(control);
                return reslt;
                
            }
            catch (Exception ex)
            {

                LogUtility.WriteLog(typeof(RibbonCustom), ex);
                return false;
            }

        }
        
            

        

        /// <summary>
        /// RibbonControl操作
        /// </summary>
        /// <param name="control">用户控件</param>
        public void OnOptionWord(Office.IRibbonControl control)
        {
            RibbonUtility rb = new RibbonUtility();
            try
            {
                switch (control.Id)
                {
                    //显示或者隐藏右侧pnel
                    case "but_show_hide_RightPanel":
                        rb.ShowHidePanel();
                        break;
                    //设计模式
                    case "but_design":
                        WordBarsUtility.ExcuteCommbandBarMso("DesignMode", false);
                        break;
                    default:
                        MessageBox.Show("没有找到相应的操作ID值！");
                        break;


                }
            }
            catch (Exception ex)
            {

                LogUtility.WriteLog(typeof(RibbonCustom), ex);
            }

        }

        /// <summary>
        /// 审阅保存按钮
        /// </summary>
        /// <param name="control">按钮控件</param>
        public void BtnReviewSave(Office.IRibbonControl control)
        {
            try
            {
                WordReviewUtility.BtnReviewSave(control);
            }
            catch (Exception ex)
            {

                LogUtility.WriteLog(typeof(RibbonCustom), ex);
            }

         

        }
        /// <summary>
        /// 批注新增
        /// </summary>
        /// <param name="control"></param>
        public void OnNewComment(Office.IRibbonControl control)
        {
            try
            {
                WordReviewUtility.OnNewComment(control);
            }
            catch (Exception ex)
            {

                LogUtility.WriteLog(typeof(RibbonCustom), ex);
            }
        }

        /// <summary>
        /// 审阅保存按钮
        /// </summary>
        /// <param name="control">按钮控件</param>
        public void BtnLhReviewNewComment(Office.IRibbonControl control)
        {
            try
            {
                WordReviewUtility.BtnReviewSave(control);
            }
            catch (Exception ex)
            {

                LogUtility.WriteLog(typeof(RibbonCustom), ex);
            }



        }


        #endregion

        #region 帮助器

        private static string GetResourceText(string resourceName)
        {
            try
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                string[] resourceNames = asm.GetManifestResourceNames();
                for (int i = 0; i < resourceNames.Length; ++i)
                {
                    if (string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        using (StreamReader resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
                        {
                            if (resourceReader != null)
                            {
                                return resourceReader.ReadToEnd();
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.WriteLog(typeof(RibbonCustom), ex);
                return null;
            }
        }

        #endregion
    }
}
