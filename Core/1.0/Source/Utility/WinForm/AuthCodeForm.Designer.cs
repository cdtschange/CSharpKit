namespace Cdts.Utility.WinForm
{
    partial class AuthCodeForm
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
            this.picCheckCode = new System.Windows.Forms.PictureBox();
            this.txtCheckCode = new System.Windows.Forms.TextBox();
            this.btnCheckCode = new System.Windows.Forms.Button();
            this.txtSysCode = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.picCheckCode)).BeginInit();
            this.SuspendLayout();
            // 
            // picCheckCode
            // 
            this.picCheckCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picCheckCode.Location = new System.Drawing.Point(12, 12);
            this.picCheckCode.Name = "picCheckCode";
            this.picCheckCode.Size = new System.Drawing.Size(225, 85);
            this.picCheckCode.TabIndex = 0;
            this.picCheckCode.TabStop = false;
            // 
            // txtCheckCode
            // 
            this.txtCheckCode.Location = new System.Drawing.Point(255, 12);
            this.txtCheckCode.Name = "txtCheckCode";
            this.txtCheckCode.Size = new System.Drawing.Size(100, 21);
            this.txtCheckCode.TabIndex = 1;
            this.txtCheckCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCheckCode_KeyUp);
            // 
            // btnCheckCode
            // 
            this.btnCheckCode.Location = new System.Drawing.Point(255, 69);
            this.btnCheckCode.Name = "btnCheckCode";
            this.btnCheckCode.Size = new System.Drawing.Size(100, 28);
            this.btnCheckCode.TabIndex = 2;
            this.btnCheckCode.Text = "确定";
            this.btnCheckCode.UseVisualStyleBackColor = true;
            this.btnCheckCode.Click += new System.EventHandler(this.btnCheckCode_Click);
            // 
            // txtSysCode
            // 
            this.txtSysCode.Location = new System.Drawing.Point(343, 103);
            this.txtSysCode.Name = "txtSysCode";
            this.txtSysCode.Size = new System.Drawing.Size(28, 21);
            this.txtSysCode.TabIndex = 3;
            this.txtSysCode.Visible = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(12, 106);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(113, 12);
            this.linkLabel1.TabIndex = 4;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "看不清楚？换张图片";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // CheckCodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 123);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.txtSysCode);
            this.Controls.Add(this.btnCheckCode);
            this.Controls.Add(this.txtCheckCode);
            this.Controls.Add(this.picCheckCode);
            this.MaximizeBox = false;
            this.Name = "CheckCodeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "请输入验证码";
            this.Load += new System.EventHandler(this.CheckCodeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picCheckCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picCheckCode;
        private System.Windows.Forms.TextBox txtCheckCode;
        private System.Windows.Forms.Button btnCheckCode;
        private System.Windows.Forms.TextBox txtSysCode;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}