using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordAddIn.Utility;
using Tool=Microsoft.Office.Tools;
using System.Threading.Tasks;
using Common.Utility;

namespace WordAddIn.ClassLib.WordAddinClass
{
    /// <summary>
    /// 插件处理类型父类
    /// </summary>
    public abstract class WordAddinBase
    {
        /// <summary>
        /// 合同模板变量
        /// </summary>
        protected VariableData VarData { get; set; }

        
        /// <summary>
        /// 设置文档状态
        /// </summary>
        public void SetWordDocumentState() 
        {
            WordDocumentHelper.DocProtectOrUnProtect();
        
        }

        /// <summary>
        /// 添加TaskPane
        /// </summary>
        /// <param name="taskPaneCollection">任务窗口集合CustomTaskPaneCollection</param>
        public void AddTaskPane(Microsoft.Office.Tools.CustomTaskPaneCollection taskPaneCollection, bool visible = true)
        {

               
                CreateTaskPane(taskPaneCollection, visible); 
            
        }

        private void CreateTaskPane(Microsoft.Office.Tools.CustomTaskPaneCollection taskPaneCollection, bool visible)
        {
            LogUtility.WriteLog(typeof(WordHandleFactory), "执行CreateWordExeInstance--CreateTaskPane");
            TaskPaneCustomUserControl control1 = new TaskPaneCustomUserControl();
            var taskPane = taskPaneCollection.Add(control1, "模板变量控制面板");
            Share.customTaskPane = taskPane;
            taskPane.Width = 320;
            taskPane.Visible = visible;
           this.VarData = control1.VarData;
           // return true;
        }
        /// <summary>
        /// 初始化Word
        /// </summary>
        /// <param name="addInDataInfo">插件传输数据实体</param>
        public abstract void InitWordStyle(AddInDataInfo addInDataInfo);
      
        /// <summary>
        /// 使用插件打开Word
        /// </summary>
        public  void WordAddinOpenDocument(bool visible=true) 
        {
            try
            {
                //MessageUtility.ShowMsg("WordAddinOpenDocument");
                WordDownloadUtility wordUtility = new WordDownloadUtility();
                string wordPath = "";
                string loadurl = "";
                loadurl = WordShare.BaseUrl+ wordUtility.GetCurrentWordUrl(WordShare.TempId, WordShare.UserId, WordShare.contTextOption);
                wordPath = WordDownloadUtility.CreateWordTempPathDefault();
               
                HttpWebRequestOptionFile.Download(loadurl, wordPath);
                object unknow = System.Reflection.Missing.Value;
                Globals.ThisAddIn.Application.Visible = visible;
                Globals.ThisAddIn.Application.Documents.Open(wordPath,
                        ref unknow, ref unknow, ref unknow, ref unknow, ref unknow,
                        ref unknow, ref unknow, ref unknow, ref unknow, ref unknow,
                        ref unknow, ref unknow, ref unknow, ref unknow, ref unknow
                        );
            }
            catch (Exception ex)
            {
                MessageUtility.ShowMsg("插件打开Word文件时失败，请查看文件是否存在！");
                LogUtility.WriteLog(typeof(WordHandleFactory), ex.ToString());
            }
        }


    }
}
