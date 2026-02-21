using Common.Model;
using Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WordAddIn.Utility;

namespace WordAddIn.ClassLib
{
    /// <summary>
    /// 自定义任务窗口帮助类
    /// </summary>
    public class TaskPaneCustomUserControlHelper
    {
        /// <summary>
        /// 新增自定义变量
        /// </summary>
        /// <param name="listBox">自定义变量ListBox</param>
        public void CreateCustomVar(ListBox listBox) 
        {
            try
            {
                CustomVarHandleForm cwinform = new CustomVarHandleForm();
                cwinform.Text = "新增自定义变量";
                cwinform.IsUpdate = 0;//标识新增
                cwinform.lisCustomBox = listBox;//自定义变量ListBox
                cwinform.ShowDialog();
            }
            catch (Exception ex)
            {

                MsgException(ex);
            }
        
        
        }
        /// <summary>
        /// 重命名自定义变量
        /// </summary>
        /// <param name="listBox">自定义变量ListBox</param>
        public void ReNameCustomVar(ListBox listBox) 
        {
            try
            {
                //var selectIndex = listBox.SelectedIndex;
                var selectItem = (ContractVariable)listBox.SelectedItem;//(listBox.Items[selectIndex]);
                if (selectItem != null)
                {
                    CustomVarHandleForm cwinform = new CustomVarHandleForm();
                    cwinform.Text = "重命名自定义变量";
                    cwinform.IsUpdate = 1;//标识新增
                    cwinform.lisCustomBox = listBox;//自定义变量ListBox
                    cwinform.lisCustomBox.ValueMember = "VarName";
                    cwinform.lisCustomBox.DisplayMember = "VarLabel";
                    cwinform.TempValue = (ContractVariable)selectItem;
                    cwinform.ShowDialog();
                }
                else
                {
                    MessageBox.Show("请选择自定义变量！");

                }
            }
            catch (Exception ex)
            {

                MsgException(ex);
            }
        
        }
        /// <summary>
        /// 删除自定义变量
        /// </summary>
        /// <param name="listBox"></param>
        public void DelCustomVar(ListBox listBox)
        {

            try
            {
                if (listBox.Items.Count == 0)
                {
                    MessageBox.Show("没有数据可以被删除！");
                    return;
                }
                //var selectItem = listBox.SelectedItem;
                var selectIndex = listBox.SelectedIndex;
                var selectItem = (ContractVariable)(listBox.Items[selectIndex]);
                if (selectItem != null)
                {
                    var info = (ContractVariable)selectItem;
                    var _index = listBox.SelectedIndex;
                    var result = VariableHelper.DelCustomVar(Convert.ToInt32(info.VarName));
                    if (result == "SUC")
                    {
                        listBox.Items.RemoveAt(_index);
                       
                        MessageBox.Show("删除成功！");
                        
                    }
                    else
                    {
                        MessageBox.Show("删除失败！");
                    }

                }
                else
                {
                    MessageBox.Show("请选择您要删除的自定义变量！");

                }
            }
            catch (Exception ex)
            {
                MsgException(ex);

            }
        
        
        }

        /// <summary>
        /// 绑定当前系统变量或自定义变量
        /// </summary>
        /// <param name="lisbox">当前ListBox</param>
        /// <param name="getAll">是否获取所有</param>
        /// <param name="isCustom">是否是自定义变量绑定</param>
        /// <param name="isDraft">是不是生成文本，比如模板起草时</param>
        /// <returns>返回当前数据源</returns>
        public IList<ContractVariable> BindListBoxData(ListBox lisbox, bool isCustom, bool getAll=false,bool isDraft=false)
        {
           
            lisbox.DisplayMember = "VarLabel";
            lisbox.ValueMember = "VarName";
            IList<ContractVariable> listdata = null;
            if (!isDraft)
            {
                listdata = isCustom ? VariableHelper.GetListCustomVarData(getAll) : VariableHelper.GetListSystemVarData(getAll);
            }
            else
            {
                listdata = isCustom ? VariableHelper.GetListCustomVarData(false, true) : VariableHelper.GetListSystemVarData(false, true);
            }
            if (listdata != null && listdata.Count > 0)
            {
                foreach (var info in listdata)
                {

                    lisbox.Items.Add(info);

                }
                

            }
            return listdata;
        }
       

      

      
        /// <summary>
        /// 异常消息提示
        /// </summary>
        /// <param name="ex"></param>
        private static void MsgException(Exception ex)
        {
            LogUtility.WriteLog(typeof(TaskPaneCustomUserControlHelper), ex);
            MessageBox.Show("操作异常");
        }
        /// <summary>
        /// 系统变量双击创建
        /// </summary>
        /// <param name="listboxSysVar">系统变量ListBox</param>
        /// <param name="e">当前事件对象</param>
        public void LitBoxSysVarMouseDoubleClick(ListBox listboxSysVar,MouseEventArgs e) 
        {
            try
            {   
                //设计模式
                WordBarsUtility.ExcuteCommbandBarMso("DesignMode", false);
                int _index = listboxSysVar.IndexFromPoint(e.Location);
                if (_index != System.Windows.Forms.ListBox.NoMatches)
                {
                    var selectItem = (ContractVariable)(listboxSysVar.Items[_index]);

                    var richTextId = WordDocumentHelper.CreateRichTextContent(selectItem.VarLabel, selectItem.VarName.ToString());
                    //添加系统变量和模板的关系
                    VariableHelper.RecordCtVarUsage(Convert.ToInt32(selectItem.VarName));

                }
            }
            catch (Exception ex)
            {
                MsgException(ex);
               
            }
        }
        /// <summary>
        /// 自定义变量双击事件
        /// </summary>
        /// <param name="listboxSysVar">自定义变量ListBox</param>
        /// <param name="e">事件源</param>
        public void LitBoxCustomVarMouseDoubleClick(ListBox listboxSysVar, MouseEventArgs e)
        {
            try
            {
                WordBarsUtility.ExcuteCommbandBarMso("DesignMode", false);
                int _index = listboxSysVar.IndexFromPoint(e.Location);
                if (_index != System.Windows.Forms.ListBox.NoMatches)
                {
                    var selectItem = (ContractVariable)(listboxSysVar.Items[_index]);

                    var richTextId = WordDocumentHelper.CreateRichTextContent(selectItem.VarLabel, selectItem.VarName.ToString());

                }
            }
            catch (Exception ex)
            {

                MsgException(ex);
            }
        
        }


    }
}
