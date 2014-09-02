using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace UI.Layout
{
    public partial class MsgWindow : ChildWindow
    {
        public MsgWindow()
        {
            InitializeComponent();
        }

        public MsgWindow(string title, string content, ImageSource imgSource)
        {
            InitializeComponent();
            MsgTitle = title;
            MsgContent = content;
            MsgImg = imgSource;
        }

        private string msgTitle;

        public string MsgTitle
        {
            get { return msgTitle; }
            set
            {
                msgTitle = value;
                titleTxt.Text = value;
            }
        }
        private string msgContent;

        public string MsgContent
        {
            get { return msgContent; }
            set
            {
                msgContent = value;
                contentTxt.Text = value;
            }
        }
        private ImageSource msgImg;

        public ImageSource MsgImg
        {
            get { return msgImg; }
            set
            {
                msgImg = value;
                img.Source = value;
            }
        }



        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}

