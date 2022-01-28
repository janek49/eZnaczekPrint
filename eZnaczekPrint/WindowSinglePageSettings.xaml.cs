using System;
using System.Collections.Generic;
using System.IO;
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
        public static string FILE_SENDER_ADR = "sp_default_sender_address";
        public static string FILE_SENDER_TEL = "sp_default_sender_phone";
        public static string FILE_RECV_ADR = "sp_default_receiver_address";
        public static string FILE_RECV_TEL = "sp_default_receiver_phone";

        public WindowSinglePageSettings()
        {
            InitializeComponent();
            txtSender.Text = Settings.ReadSetting(FILE_SENDER_ADR);
            txtSenderPhone.Text = Settings.ReadSetting(FILE_SENDER_TEL);
            txtReceiver.Text = Settings.ReadSetting(FILE_RECV_ADR);
            txtReceiverPhone.Text = Settings.ReadSetting(FILE_RECV_TEL);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Settings.SaveSetting(FILE_SENDER_ADR, txtSender.Text);
            Settings.SaveSetting(FILE_SENDER_TEL, txtSenderPhone.Text);
            Settings.SaveSetting(FILE_RECV_ADR, txtReceiver.Text);
            Settings.SaveSetting(FILE_RECV_TEL, txtReceiverPhone.Text);
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnGroupEventAddressTools(object sender, RoutedEventArgs e)
        {
            if (sender == btnNadawcaClear)
            {
                txtSender.Clear();
                txtSenderPhone.Clear();
            }
            else if (sender == btnAdresatClear)
            {
                txtReceiver.Clear();
                txtReceiverPhone.Clear();
            }
        }


    }
}
