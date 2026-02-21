using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Model;
using Common.Utility;
using WordAddIn.ClassLib;
using WordAddIn.ClassLib.WordAddinClass;
using WordAddIn.Utility;

namespace WordAddIn
{
    public partial class TaskPaneCustomUserControl : UserControl
    {
        /// <summary>
        /// 任务窗口帮助类
        /// </summary>
        private TaskPaneCustomUserControlHelper TaskHelper = new TaskPaneCustomUserControlHelper();
        ///// <summary>
        ///// 合同模板变量
        ///// </summary>
       public VariableData VarData { get; set; }
        /// <summary>
        /// 插件帮助类
        /// </summary>
       private WordAddinUtility addinUtility = new WordAddinUtility();
       
        public TaskPaneCustomUserControl()
        {
           
            InitializeComponent();
           
           
           // Control.CheckForIllegalCrossThreadCalls = false;
        }
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskPaneCustomUserControl_Load(object sender, EventArgs e)
        {
            VariableData varData = new VariableData();
            this.lbluserName.Text = WordShare.UserName;
            WordShare.lisBoxSysVal = this.listboxSysVar;
            WordShare.lisBoxCustomVal = this.listboxCustomVar;
            var getAll = false;
            if (WordShare.requestWordType == requestAddinWordType.TplonreadOrwrite)
            {
                getAll = true;
               varData.SysVariable= TaskHelper.BindListBoxData(this.listboxSysVar, false, getAll);
               varData.CustomVariable = TaskHelper.BindListBoxData(this.listboxCustomVar, true, getAll);
            }
            else
            {
                BtnVisable(false);//隐藏自定义变量操作按钮
                varData.SysVariable= TaskHelper.BindListBoxData(this.listboxSysVar, false, false,true);
                varData.CustomVariable = TaskHelper.BindListBoxData(this.listboxCustomVar, true, false, true);

            }


              VarData = varData;
             //注册事件
           
          
        }


        #region 按钮相关

        /// <summary>
        /// 按钮保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_save_Click(object sender, EventArgs e)
        {
           
           // var rest = WordHandleFactory.CreateWordExeInstance().WordSave();
            var rest=WordHandleFactory.CreateIOptionInstance().WordSave();
            MessageUtility.SaveMsg(rest);
        }

        /// <summary>
        /// 新增自定义
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_CreateZdy_Click(object sender, EventArgs e)
        {
            TaskHelper.CreateCustomVar(this.listboxCustomVar);

        }
        /// <summary>
        /// 重命名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_cmm_Click(object sender, EventArgs e)
        {
            TaskHelper.ReNameCustomVar(this.listboxCustomVar);


        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_delxtbl_Click(object sender, EventArgs e)
        {
            TaskHelper.DelCustomVar(this.listboxCustomVar);

        }

        #endregion

        /// <summary>
        /// 任务窗口右下角(自定义操作相关)按钮隐藏
        /// </summary>
        /// <param name="visable">true:显示：False：隐藏</param>
        private void BtnVisable(bool visable)
        {
            btn_CreateZdy.Visible = visable;
            btn_cmm.Visible = visable;
            btn_delxtbl.Visible = visable;
        }

        /// <summary>
        /// 系统变量双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listboxSysVar_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            TaskHelper.LitBoxSysVarMouseDoubleClick(this.listboxSysVar, e);

        }
        /// <summary>
        /// 自定义变量双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listboxCustomVar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TaskHelper.LitBoxCustomVarMouseDoubleClick(this.listboxCustomVar, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var doc = WordShare.WordApp.ActiveDocument;
        }




    }
}
