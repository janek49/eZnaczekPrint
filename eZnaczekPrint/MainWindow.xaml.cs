using eZnaczekPrint.Common;
using eZnaczekPrint.Model;
using eZnaczekPrint.Render;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace eZnaczekPrint
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow _instance;

        public MainWindow()
        {
            _instance = this;
            InitializeComponent();
        }

        public static MainWindow GetInstance()
        {
            return _instance;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            LocalServer ls = new LocalServer(this);
            ls.Start();
        }


        public void webHook(string address, string phone)
        {
            Dispatcher.Invoke(() =>
            {
                this.pageSingle.txtReceiver.Text = address;
                this.pageSingle.txtReceiverPhone.Text = phone;
                this.WindowState = WindowState.Normal;
                this.Hide();
                this.Show();
                this.Activate();
            });
        }

      
    }
}
