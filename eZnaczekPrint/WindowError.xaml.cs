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
    /// Logika interakcji dla klasy WindowError.xaml
    /// </summary>
    public partial class WindowError : Window
    {
        public WindowError()
        {
            InitializeComponent();
        }

        private string errorText, errorMessage;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.LblErrorText.Content = errorText;
            this.TxtErrorMessage.Text = errorMessage;
        }

        public WindowError(string errorText, string errorMessage, Window owner = null) : this()
        {
            this.errorText = errorText;
            this.errorMessage = errorMessage;
            if (owner != null && owner.IsVisible) this.Owner = owner;
        }

        public WindowError(Exception ex, Window owner = null) : this(ex.GetType().FullName, ex.ToString(), owner)
        {

        }

        private void btnCloseApp_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnIgnore_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }



    }
}
