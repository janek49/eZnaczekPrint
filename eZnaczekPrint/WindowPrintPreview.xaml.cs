using eZnaczekPrint.Model;
using eZnaczekPrint.Render;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
    /// Logika interakcji dla klasy WindowPrintPreview.xaml
    /// </summary>
    public partial class WindowPrintPreview : Window
    {
        public WindowPrintPreview()
        {
            InitializeComponent();
        }

        public LabelData LabelData;
        public StampFormat StampFormat;
        public System.Drawing.Image CurrenImageLoaded;
        public bool AutoSave = false;
        public RenderLabelFormat LabelRender;

        public WindowPrintPreview(LabelData data, StampFormat stamp, RenderLabelFormat labelformat) : this()
        {
            LabelRender = labelformat;
            LabelData = data;
            StampFormat = stamp;
        }

        private void TbBtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CurrenImageLoaded == null)
                return;

            var dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.Filter = "Plik graficzny PNG|*.png";

            if (dlg.ShowDialog() != true)
                return;

            try
            {
                CurrenImageLoaded.Save(dlg.FileName, ImageFormat.Png);
                if(MessageBox.Show("Zapisano plik:\n\n" + dlg.FileName + "\n\nChcesz go teraz otworzyć?", "Informacja", MessageBoxButton.YesNo, MessageBoxImage.Information) 
                    == MessageBoxResult.Yes)
                {
                    System.Diagnostics.Process.Start(dlg.FileName);
                }

                if (AutoSave)
                    Close();
            }
            catch (Exception ex)
            {
                new WindowError(ex, this).ShowDialog();
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            try
            {
                CurrenImageLoaded = LabelRender.DrawLabel(LabelData, StampFormat);
                ImageSource wpfimage = Util.ImageToImageSource(CurrenImageLoaded);
                imgPreview.Source = wpfimage;
            }
            catch (Exception ex)
            {
                new WindowError(ex, this).ShowDialog();
                Close();
                return;
            }

            try
            {
                CurrenImageLoaded.Save("output.png");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Uwaga! Wystąpił błąd podczas zapisywania pliku tymczasowego.\n\n" +
                    "Sprawdź uprawnienia aplikacji. Funkcja drukowania jest wciąż dostępna.\n\n\n" +
                    ex.ToString(), "Uwaga!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            if (AutoSave)
                TbBtnSave_Click(null, null);
        }
    }
}
