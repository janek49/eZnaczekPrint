using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace eZnaczekPrint
{
    /// <summary>
    /// Logika interakcji dla klasy WindowSinglePageSettings.xaml
    /// </summary>
    public partial class WindowSinglePageSettings : Window
    {
        public WindowSinglePageSettings()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
