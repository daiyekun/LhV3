using Common.Model;
using Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordAddIn.ClassLib;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using WordTool=Microsoft.Office.Tools.Word;
using WordAddIn.ClassLib.WordAddinClass;


namespace WordAddIn.Utility
{
    /// <summary>
    /// Word插件工具类
    /// </summary>
   public  class WordAddinUtility
    {
       /// <summary>
       /// 初始化数据
       /// </summary>
       public static void InitData() 
       {
           try
           {
               //读取零时文件
               InIDataHipler.ReadINIToData();
               // MessageUtility.ShowMsg("开始登陆");
               //登录
               InIDataHipler.WordLogin(WordShare.UserId);
           }
           catch (Exception ex)
           {
               LogUtility.WriteLog(typeof(WordAddinUtility),ex);
               
           }

       }
       /// <summary>
       /// 插件装载类
       /// </summary>
       public void AddInStartup() 
       {
           WordShare.WordApp = Globals.ThisAddIn.Application;
           //设置文档当前用户
           Globals.ThisAddIn.Application.UserInitials = Globals.ThisAddIn.Application.UserName = WordShare.UserName;
       }

       /// <summary>
       /// 注册事件
       /// </summary>
       public void RegEventHandler()
       {
            try
            {
                WordHelper wordHelper = new WordHelper();
                ////添加事件
                //this.Application.DocumentBeforeSave +=
                //  new Word.ApplicationEvents4_DocumentBeforeSaveEventHandler(Application_DocumentBeforeSave);
                //LogUtility.WriteLog(typeof(WordAddinUtility), "RegEventHandler注册事件");
                //注册事件
                //Word.Document currentDocument = Globals.ThisAddIn.Application.ActiveDocument;
                //WordTool.Document document = Globals.Factory.GetVstoObject(currentDocument);
                WordDocumentHelper.GetVstoObjectDocm().CloseEvent += new Word.DocumentEvents2_CloseEventHandler(wordHelper.CloseDocument);
                WordDocumentHelper.GetVstoObjectDocm().ContentControlOnEnter += new Word.DocumentEvents2_ContentControlOnEnterEventHandler(wordHelper.ContentControlOnEnter);
                WordDocumentHelper.GetVstoObjectDocm().ContentControlOnExit += new Word.DocumentEvents2_ContentControlOnExitEventHandler(wordHelper.ContentControlOnExit);
            }
            catch (Exception ex)
            {

                LogUtility.WriteLog(typeof(WordAddinUtility),ex);
            }


       }
       /// <summary>
       /// 创建TaskPane
       /// </summary>
       /// <param name="taskPaneCollection"></param>
       /// <param name="visible">是否显示</param>
       /// <returns></returns>
       public VariableData AddCustomTaskPane(Microsoft.Office.Tools.CustomTaskPaneCollection taskPaneCollection, bool visible)
       {
           TaskPaneCustomUserControl control1 = new TaskPaneCustomUserControl();
           var taskPane = taskPaneCollection.Add(control1, "模板变量控制面板");
           Share.customTaskPane = taskPane;
           taskPane.Width = 320;
           taskPane.Visible = visible;
           return null;
          // return control1.VarData;
           
       }

      



      
    }
}
