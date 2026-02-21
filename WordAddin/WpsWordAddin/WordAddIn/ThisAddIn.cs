using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using Tool = Microsoft.Office.Tools;
using WordAddIn.ClassLib;
using Common.Utility;
using System.Windows.Forms;
using Common.Model;
using WordAddIn.Utility;
using WordAddIn.ClassLib.WordAddinClass;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;

namespace WordAddIn
{
    /// <summary>
    /// 插件入口
    /// </summary>
    public partial class ThisAddIn
    {
        private WordAddinUtility addinUtility = new WordAddinUtility();


        /// <summary>
        /// 装载时执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
           // MessageBox.Show("WpsThisAddIn_Startup！");
            WordShare.WordApp = Globals.ThisAddIn.Application;
            WordShare.ObjWordApp = this.Application;
            string appName = "word";
            if (WordShare.ObjWordApp.ToString().Contains("ComObject"))
            {
                appName = "wps";
            }
           
            WordAddinSetDocStyle();





        }
        /// <summary>
        /// 重新加载插件
        /// </summary>
        private void AddinComReadLoad()
        {
            foreach (Office.COMAddIn cAddin in this.Application.COMAddIns)
            {

                if (cAddin.ProgId == WordShare.AddInCom_ProgId && !cAddin.Connect)
                {
                    WordAddinComHelper.WordAddinLoad(WordShare.AddInCom_ProgId);

                }

            }
        }


        /// <summary>
        /// 使用插件设置文档样式
        /// </summary>
        private void WordAddinSetDocStyle()
        {
            try
            {
                //MessageUtility.ShowMsg("WordAddinSetDocStyle进入："+ WordShare.IsWordToLogin);
                if (WordShare.IsWordToLogin)
                {
                    //MessageUtility.ShowMsg("WordAddinSetDocStyle");
                    //Globals.ThisAddIn.Application.UserInitials = WordShare.UserName;
                    Globals.ThisAddIn.Application.UserName = WordShare.UserName;
                    AddInDataInfo addInDataInfo = new AddInDataInfo();
                    addInDataInfo.taskPaneCollection = Globals.ThisAddIn.CustomTaskPanes;
                    WordHandleFactory.CreateWordExeInstance().InitWordStyle(addInDataInfo);
                    addinUtility.RegEventHandler();



                }
                else
                {
                    LogUtility.WriteDebug(typeof(ThisAddIn), "Word插件登录服务器失败！");


                }


            }
            catch (Exception ex)
            {
                LogUtility.WriteLog(typeof(ThisAddIn), ex);
                MessageBox.Show("插件初始数据失败！");

            }
        }





        /// <summary>
        /// 卸载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            //MessageBox.Show("执行了ThisAddIn_Shutdown");

            WordAddinComHelper.WordAddinUnload(WordShare.AddInCom_ProgId);
            try
            {
                GC.Collect();
                Marshal.FinalReleaseComObject(Globals.ThisAddIn.Application);
                GC.Collect();
            }
            catch { }

        }



        #region VSTO 生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion





        /// <summary>
        /// 自定义Robbin初始化-它优先于ThisAddIn_Startup执行
        /// </summary>
        /// <returns></returns>
        protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            try
            {
                //MessageBox.Show("CreateRibbonExtensibilityObject");
                WordAddinUtility.InitData();
                var ribboninfo = new RibbonCustom();
                return ribboninfo;
            }
            catch (Exception ex)
            {

                LogUtility.WriteLog(typeof(ThisAddIn), ex);
                MessageBox.Show("插件初始数据失败！");
                return null;
            }
        }
    }
}
