namespace WordAddIn
{
    partial class CustomVarHandleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_qx = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.txt_Name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_qx
            // 
            this.btn_qx.Location = new System.Drawing.Point(431, 112);
            this.btn_qx.Name = "btn_qx";
            this.btn_qx.Size = new System.Drawing.Size(75, 23);
            this.btn_qx.TabIndex = 7;
            this.btn_qx.Text = "取消";
            this.btn_qx.UseVisualStyleBackColor = true;
            this.btn_qx.Click += new System.EventHandler(this.btn_qx_Click);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(315, 112);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 6;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // txt_Name
            // 
            this.txt_Name.Location = new System.Drawing.Point(97, 58);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.Size = new System.Drawing.Size(409, 21);
            this.txt_Name.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "名称：";
            // 
            // CustomVarHandleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 179);
            this.Controls.Add(this.btn_qx);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.txt_Name);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomVarHandleForm";
            this.Text = "自定义变量";
            this.Load += new System.EventHandler(this.CustomVarHandleForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_qx;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.TextBox txt_Name;
        private System.Windows.Forms.Label label1;
    }
}