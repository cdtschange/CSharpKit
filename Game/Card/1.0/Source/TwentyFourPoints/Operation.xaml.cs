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

namespace TwentyFourPoints
{
    public partial class Operation : UserControl
    {
        public Operation()
        {
            InitializeComponent();
            this.plus.MouseLeftButtonUp += new MouseButtonEventHandler(list_MouseLeftButtonUp);
            this.sub.MouseLeftButtonUp += new MouseButtonEventHandler(list_MouseLeftButtonUp);
            this.multi.MouseLeftButtonUp += new MouseButtonEventHandler(list_MouseLeftButtonUp);
            this.divide.MouseLeftButtonUp += new MouseButtonEventHandler(list_MouseLeftButtonUp);
        }


        void list_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var img = (sender as Image);
            operate.Source = img.Source;
            OperateValue = img.Name;
        }

        public string OperateValue { get; set; }
    }
}
