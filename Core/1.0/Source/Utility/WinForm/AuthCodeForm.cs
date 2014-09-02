using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cdts.Utility.WinForm
{
    public partial class AuthCodeForm : Form
    {
        public AuthCodeForm()
        {
            InitializeComponent();
        }
        public AuthCodeForm(GetCheckCodeImageHandler handler)
            : this()
        {
            this.GetCheckCodeImage = handler;
        }


        private void btnCheckCode_Click(object sender, EventArgs e)
        {
            if (this.txtCheckCode.Text.Length < 2)
            {
                MessageBox.Show("输入的验证码长度不够");
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public string GetCheckCode()
        {
            return this.txtCheckCode.Text;
        }

        public string GetSysCode()
        {
            return this.txtSysCode.Text;
        }

        public delegate bool GetCheckCodeImageHandler(out byte[] imgPic, out string syscheckcode);

        public GetCheckCodeImageHandler GetCheckCodeImage;


        private void CheckCodeForm_Load(object sender, EventArgs e)
        {
            linkLabel1_LinkClicked(sender, null);
            this.Activate();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            byte[] bytes = null;
            string syscode = string.Empty;
            if (GetCheckCodeImage != null && GetCheckCodeImage(out bytes, out syscode))
            {
                this.picCheckCode.Image = new System.Drawing.Bitmap(new System.IO.MemoryStream(bytes));
                this.txtSysCode.Text = syscode;
            }
            else
            {
                MessageBox.Show("获取失败，请重新点击获取...");
            }
        }

        private void txtCheckCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnCheckCode_Click(sender, EventArgs.Empty);
        }
    }
}
