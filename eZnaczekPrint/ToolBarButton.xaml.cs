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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace eZnaczekPrint
{
    /// <summary>
    /// Logika interakcji dla klasy ToolBarButton.xaml
    /// </summary>
    /// 


    public partial class ToolBarButton : Button
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(String), typeof(ToolBarButton), new FrameworkPropertyMetadata(null));
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(ImageSource), typeof(ToolBarButton), new FrameworkPropertyMetadata(null));
        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public ToolBarButton()
        {
            InitializeComponent();
        }

        private void Button_Loaded(object sender, RoutedEventArgs e)
        {
            if (Text != null)
                textBlock.Text = Text;
            if (Image != null)
                icon.Source = Image;
        }
    }
}
