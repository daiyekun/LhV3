namespace WordAddIn
{
    partial class TaskPaneCustomUserControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_xtbl = new System.Windows.Forms.Label();
            this.lab_msg = new System.Windows.Forms.Label();
            this.btn_save = new System.Windows.Forms.Button();
            this.listboxSysVar = new System.Windows.Forms.ListBox();
            this.btn_delxtbl = new System.Windows.Forms.Button();
            this.btn_cmm = new System.Windows.Forms.Button();
            this.btn_CreateZdy = new System.Windows.Forms.Button();
            this.lbluserName = new System.Windows.Forms.Label();
            this.listboxCustomVar = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lbl_xtbl
            // 
            this.lbl_xtbl.AutoSize = true;
            this.lbl_xtbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_xtbl.Location = new System.Drawing.Point(23, 55);
            this.lbl_xtbl.Name = "lbl_xtbl";
            this.lbl_xtbl.Size = new System.Drawing.Size(65, 12);
            this.lbl_xtbl.TabIndex = 10;
            this.lbl_xtbl.Text = "系统变量：";
            // 
            // lab_msg
            // 
            this.lab_msg.AutoSize = true;
            this.lab_msg.Location = new System.Drawing.Point(23, 20);
            this.lab_msg.Name = "lab_msg";
            this.lab_msg.Size = new System.Drawing.Size(41, 12);
            this.lab_msg.TabIndex = 9;
            this.lab_msg.Text = "您好！";
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(232, 20);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 31);
            this.btn_save.TabIndex = 8;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // listboxSysVar
            // 
            this.listboxSysVar.FormattingEnabled = true;
            this.listboxSysVar.ItemHeight = 12;
            this.listboxSysVar.Location = new System.Drawing.Point(25, 83);
            this.listboxSysVar.Name = "listboxSysVar";
            this.listboxSysVar.Size = new System.Drawing.Size(282, 292);
            this.listboxSysVar.TabIndex = 14;
            this.listboxSysVar.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listboxSysVar_MouseDoubleClick);
            // 
            // btn_delxtbl
            // 
            this.btn_delxtbl.Location = new System.Drawing.Point(232, 589);
            this.btn_delxtbl.Name = "btn_delxtbl";
            this.btn_delxtbl.Size = new System.Drawing.Size(75, 23);
            this.btn_delxtbl.TabIndex = 13;
            this.btn_delxtbl.Text = "删除";
            this.btn_delxtbl.UseVisualStyleBackColor = true;
            this.btn_delxtbl.Click += new System.EventHandler(this.btn_delxtbl_Click);
            // 
            // btn_cmm
            // 
            this.btn_cmm.Location = new System.Drawing.Point(144, 589);
            this.btn_cmm.Name = "btn_cmm";
            this.btn_cmm.Size = new System.Drawing.Size(75, 23);
            this.btn_cmm.TabIndex = 12;
            this.btn_cmm.Text = "重命名";
            this.btn_cmm.UseVisualStyleBackColor = true;
            this.btn_cmm.Click += new System.EventHandler(this.btn_cmm_Click);
            // 
            // btn_CreateZdy
            // 
            this.btn_CreateZdy.Location = new System.Drawing.Point(14, 586);
            this.btn_CreateZdy.Name = "btn_CreateZdy";
            this.btn_CreateZdy.Size = new System.Drawing.Size(110, 26);
            this.btn_CreateZdy.TabIndex = 11;
            this.btn_CreateZdy.Text = "新建自定义变量";
            this.btn_CreateZdy.UseVisualStyleBackColor = true;
            this.btn_CreateZdy.Click += new System.EventHandler(this.btn_CreateZdy_Click);
            // 
            // lbluserName
            // 
            this.lbluserName.AutoSize = true;
            this.lbluserName.ForeColor = System.Drawing.Color.Blue;
            this.lbluserName.Location = new System.Drawing.Point(70, 20);
            this.lbluserName.Name = "lbluserName";
            this.lbluserName.Size = new System.Drawing.Size(71, 12);
            this.lbluserName.TabIndex = 16;
            this.lbluserName.Text = "lbluserName";
            // 
            // listboxCustomVar
            // 
            this.listboxCustomVar.FormattingEnabled = true;
            this.listboxCustomVar.ItemHeight = 12;
            this.listboxCustomVar.Location = new System.Drawing.Point(25, 387);
            this.listboxCustomVar.Name = "listboxCustomVar";
            this.listboxCustomVar.Size = new System.Drawing.Size(282, 184);
            this.listboxCustomVar.TabIndex = 17;
            this.listboxCustomVar.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listboxCustomVar_MouseDoubleClick);
            // 
            // TaskPaneCustomUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listboxCustomVar);
            this.Controls.Add(this.lbluserName);
            this.Controls.Add(this.listboxSysVar);
            this.Controls.Add(this.btn_delxtbl);
            this.Controls.Add(this.btn_cmm);
            this.Controls.Add(this.btn_CreateZdy);
            this.Controls.Add(this.lbl_xtbl);
            this.Controls.Add(this.lab_msg);
            this.Controls.Add(this.btn_save);
            this.Name = "TaskPaneCustomUserControl";
            this.Size = new System.Drawing.Size(639, 625);
            this.Load += new System.EventHandler(this.TaskPaneCustomUserControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_xtbl;
        private System.Windows.Forms.Label lab_msg;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.ListBox listboxSysVar;
        private System.Windows.Forms.Button btn_delxtbl;
        private System.Windows.Forms.Button btn_cmm;
        private System.Windows.Forms.Button btn_CreateZdy;
        private System.Windows.Forms.Label lbluserName;
        private System.Windows.Forms.ListBox listboxCustomVar;
    }
}
