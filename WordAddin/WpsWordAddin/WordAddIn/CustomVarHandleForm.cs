using Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WordAddIn.ClassLib;

namespace WordAddIn
{
    /// <summary>
    /// 自定义操作窗体
    /// </summary>
    public partial class CustomVarHandleForm : Form
    {

        public CustomVarHandleForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 是否修改
        /// </summary>
        public int IsUpdate { get; set; }
        /// <summary>
        /// 自定义变量ListBox
        /// </summary>
        public ListBox lisCustomBox { get; set; }
        /// <summary>
        /// 自定义变量值-修改时候用
        /// </summary>
        public ContractVariable TempValue { get; set; }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_save_Click(object sender, EventArgs e)
        {
            
            VariableHelper.AddContCustomVar(this, this.txt_Name.Text);
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_qx_Click(object sender, EventArgs e)
        {
            this.txt_Name.Text = "";
            this.Close();
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomVarHandleForm_Load(object sender, EventArgs e)
        {
            if (TempValue != null) {
                this.txt_Name.Text = TempValue.VarLabel;
            }

        }
    }
}
