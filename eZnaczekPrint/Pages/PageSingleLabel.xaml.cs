using eZnaczekPrint.Model;
using eZnaczekPrint.Render;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace eZnaczekPrint.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy PageSingleLabel.xaml
    /// </summary>
    public partial class PageSingleLabel : UserControl
    {
        private System.Drawing.Image CurrentStampSelected;

        private List<Type> StampFormatsAvailable = new List<Type>();
        private List<Type> LabelFormatsAvailable = new List<Type>();

        public PageSingleLabel()
        {
            InitializeComponent();

            if (Util.IsInWpfDesigner())
                return;

            MainWindow.GetInstance().Drop += PageSingleLabel_Drop;

            var stampFormats = Assembly.GetAssembly(typeof(StampFormat)).GetTypes().Where(t => t.IsSubclassOf(typeof(StampFormat)));

            foreach (Type type in stampFormats)
            {
                MethodBase method = type.GetMethod("GetDisplayName");

                CmbStampFormat.Items.Add(method.Invoke(null, null) as string);
                StampFormatsAvailable.Add(type);
            }

            if (CmbStampFormat.Items.Count > 0)
                CmbStampFormat.SelectedIndex = 0;

            var labelFormats = Assembly.GetAssembly(typeof(RenderLabelFormat)).GetTypes().Where(t => t.IsSubclassOf(typeof(RenderLabelFormat)));

            foreach (Type type in labelFormats)
            {
                MethodBase method = type.GetMethod("GetDisplayName");

                CmbLabelRender.Items.Add(method.Invoke(null, null) as string);
                LabelFormatsAvailable.Add(type);
            }

            if (CmbLabelRender.Items.Count > 0)
                CmbLabelRender.SelectedIndex = 0;
        }

        private void PageSingleLabel_Drop(object sender, DragEventArgs e)
        {
            if (!Util.IsMouseOverElementInDropEvent(gbZnaczek, e.GetPosition(gbZnaczek)))
                return;

            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                return;

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length == 1)
            {
                var img = PocztaPolskaReader.ReadPdfToImage(files[0]);
                var stamp = PocztaPolskaReader.CutOutStamp(img);

                CurrentStampSelected = stamp;

                var wpfimg = Util.ImageToImageSource(stamp);
                imgZnaczek.Source = wpfimg;
            }
        }

        private LabelData GetLabelData()
        {
            LabelData ld = new LabelData
            {
                SenderAddress = txtSender.Text.Replace("\r", "").Split('\n'),
                ReceiverAddress = txtReceiver.Text.Replace("\r", "").Split('\n'),
                IsPriority = (bool)CkbIsPriority.IsChecked,
                SenderPhone = txtSenderPhone.Text.Trim(),
                ReceiverPhone = txtReceiverPhone.Text.Trim()
            };
            return ld;
        }

        private StampFormat CreateCurrentStampFormatType(System.Drawing.Image param)
        {
            if (CmbStampFormat.SelectedIndex < 0)
                return null;

            Type tp = StampFormatsAvailable[CmbStampFormat.SelectedIndex];
            return (StampFormat)Activator.CreateInstance(tp, new object[] { param });
        }

        private void OpenPreviewWindow(bool save = false)
        {
            WindowPrintPreview window = new WindowPrintPreview(GetLabelData(), CreateCurrentStampFormatType(CurrentStampSelected), CreateCurrentLabelRender());
            window.Owner = MainWindow.GetInstance();
            window.AutoSave = save;
            window.ShowDialog();
        }

        private RenderLabelFormat CreateCurrentLabelRender()
        {
            if (CmbStampFormat.SelectedIndex < 0)
                return null;

            Type tp = LabelFormatsAvailable[CmbLabelRender.SelectedIndex];
            return (RenderLabelFormat)Activator.CreateInstance(tp, new object[] { });
        }

        private void rbSinglePageBtnPreview_Click(object sender, RoutedEventArgs e)
        {
            OpenPreviewWindow();
        }

        private void tbBtnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            OpenPreviewWindow(true);
        }

        private void rbSinglePageBtnSettings_Click(object sender, RoutedEventArgs e)
        {
            WindowSinglePageSettings window = new WindowSinglePageSettings();
            window.Owner = MainWindow.GetInstance();
            window.ShowDialog();
        }


    }
}
