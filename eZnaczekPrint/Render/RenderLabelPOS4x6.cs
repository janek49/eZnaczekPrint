using eZnaczekPrint.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eZnaczekPrint.Render
{
    public class RenderLabelPOS4x6 : RenderLabelFormat
    {
        public static string GetDisplayName()
        {
            return "Potwierdzenie nadania - Etykieta 4x6 (10x15cm)";
        }

        private string fileName = "POS_4x6.jpg";

        public override Bitmap DrawLabel(LabelData ld, StampFormat stamp)
        {
            int LBL_W = 2300;
            int LBL_H = 3210 / 2;

            Bitmap BITMAP = new Bitmap(Image.FromFile(fileName));
            Graphics G = Graphics.FromImage(BITMAP);

            int OUTER_MARGIN_SIZE_LEFTRIGHT = 20;
            int OUTER_MARGIN_SIZE_UPDOWN = 20;
            int OUTER_FRAME_THICCNES = 5;
            int INNER_BOX_WIDTH = LBL_W - (OUTER_MARGIN_SIZE_LEFTRIGHT * 2);
            int INNER_BOX_HEIGHT = LBL_H - (OUTER_MARGIN_SIZE_UPDOWN * 2);
            int ADRES_BOX_WIDTH = 1700;
            int ADRES_BOX_HEIGHT = 500;

            Pen PEN_FRAME = new Pen(Brushes.Black, OUTER_FRAME_THICCNES);

            //G.DrawRectangle(PEN_FRAME, new Rectangle(OUTER_MARGIN_SIZE_LEFTRIGHT, OUTER_MARGIN_SIZE_UPDOWN, INNER_BOX_WIDTH, INNER_BOX_HEIGHT));

            int SPLIT_LINE_UPDOWN_X = 500;
            int SPLIT_LINE_LEFTRIGHT_Y = 950;

            // G.DrawLine(PEN_FRAME, new Point(SPLIT_LINE_UPDOWN_X, OUTER_MARGIN_SIZE_UPDOWN), new Point(SPLIT_LINE_UPDOWN_X, LBL_H - OUTER_MARGIN_SIZE_UPDOWN));
            G.DrawLine(PEN_FRAME, new Point(OUTER_MARGIN_SIZE_LEFTRIGHT, SPLIT_LINE_LEFTRIGHT_Y), new Point(LBL_W - OUTER_MARGIN_SIZE_LEFTRIGHT, SPLIT_LINE_LEFTRIGHT_Y));

            G.DrawLine(PEN_FRAME, new Point(OUTER_MARGIN_SIZE_LEFTRIGHT, 190), new Point(LBL_W - OUTER_MARGIN_SIZE_LEFTRIGHT, 190));
            G.DrawLine(PEN_FRAME, new Point(OUTER_MARGIN_SIZE_LEFTRIGHT, 330), new Point(LBL_W - OUTER_MARGIN_SIZE_LEFTRIGHT, 330));

            int SPLIT_CROSSING_X = SPLIT_LINE_UPDOWN_X;
            int SPLIT_CROSSING_Y = SPLIT_LINE_LEFTRIGHT_Y;

            string COMMON_FONT_NAME = "Arial";
            string EMOJI_FONT_NAME = "Segoe UI Emoji";

            Font FONT_ADRESAT_TITLE = new Font(COMMON_FONT_NAME, 36, FontStyle.Underline | FontStyle.Bold);
            Font FONT_ADRESAT_CONTENT = new Font(COMMON_FONT_NAME, 48, FontStyle.Regular);
            Font FONT_SENDER_TITLE = new Font(COMMON_FONT_NAME, 30, FontStyle.Underline);
            Font FONT_SENDER_CONTENT = new Font(COMMON_FONT_NAME, 48, FontStyle.Regular);
            Font FONT_PRIORITY = new Font(COMMON_FONT_NAME, 72, FontStyle.Bold);
            Font FONT_ADRESAT_TELEFON = new Font(EMOJI_FONT_NAME, 48, FontStyle.Regular);
            Font FONT_SENDER_TELEFON = new Font(EMOJI_FONT_NAME, 48, FontStyle.Regular);

            int MARGIN_ADR_TIT_LEFT = 20;
            int MARGIN_ADR_TIT_TOP = 10;
            int MARGIN_ADR_CNT_LEFT = 20;

            int ADRESAT_CONTENT_X = SPLIT_CROSSING_X + MARGIN_ADR_CNT_LEFT;
            int ADRESAT_CONTENT_Y = SPLIT_CROSSING_Y + 10;

             G.DrawString(DateTime.Now.ToString("yyyy-MM-dd"), FONT_ADRESAT_CONTENT, Brushes.Black, new Point(SPLIT_CROSSING_X + MARGIN_ADR_TIT_LEFT, 200));

            int ADRESAT_MAX_CONTENT_LINES = 6;

            Font CHOSEN_FONT_ADRESAT = FONT_ADRESAT_CONTENT;

            if (ld.ReceiverAddress.Length > ADRESAT_MAX_CONTENT_LINES)
            {
                int FONT_SIZE = (int)((double)(ADRES_BOX_HEIGHT - 35) / 1.5d / (double)ld.SenderAddress.Length);
                if (FONT_SIZE <= 18)
                {
                    throw new Exception(string.Format("\n\nBłąd: Adres (adresat) zawiera za dużo wierszy, aby poprawnie wyświetlić etykietę.\n\n"));
                }
                CHOSEN_FONT_ADRESAT = new Font(COMMON_FONT_NAME, FONT_SIZE, FontStyle.Regular);
            }


            int TMP_COUNTER = 0;
            foreach (string line in ld.ReceiverAddress)
            {
                Font TMP_FONT = CHOSEN_FONT_ADRESAT;
                int TMP_W = (int)G.MeasureString(line, TMP_FONT).Width;
                while (TMP_W > ADRES_BOX_WIDTH / 2)
                {
                    if (TMP_FONT.Size <= 18)
                    {
                        throw new Exception(string.Format("\n\nBłąd: Wiersz za długi (adresat):\n'{0}'\n\n", line));
                    }
                    TMP_FONT = new Font(COMMON_FONT_NAME, TMP_FONT.Size - 1, TMP_FONT.Style);
                    TMP_W = (int)G.MeasureString(line, TMP_FONT).Width;
                }

                G.DrawString(line, TMP_FONT, Brushes.Black, new Point(ADRESAT_CONTENT_X, ADRESAT_CONTENT_Y + TMP_COUNTER));
                TMP_COUNTER += TMP_FONT.Height;
            }


            if (ld.ReceiverPhone != null && ld.ReceiverPhone.Trim().Length > 0)
            {
                int ADRESAT_TELEFON_Y = LBL_H - OUTER_MARGIN_SIZE_UPDOWN - MARGIN_ADR_TIT_TOP - FONT_ADRESAT_CONTENT.Height - 15;

                G.DrawString("📞 " + ld.ReceiverPhone, FONT_ADRESAT_TELEFON, Brushes.Black, ADRESAT_CONTENT_X, ADRESAT_TELEFON_Y);
            }


            




            Font CHOSEN_FONT_SENDER = FONT_SENDER_CONTENT;
             
            int SENDER_CONTENT_X = SPLIT_CROSSING_X + MARGIN_ADR_TIT_LEFT;
            int SENDER_CONTENT_Y = 335;
            int SENDER_MAX_CONTENT_LINES = 6;

            if (ld.SenderAddress.Length > SENDER_MAX_CONTENT_LINES)
            {
                int FONT_SIZE = (int)((double)(ADRES_BOX_HEIGHT - 35) / 1.5d / (double)ld.SenderAddress.Length);
                if (FONT_SIZE <= 18)
                {
                    throw new Exception(string.Format("\n\nBłąd: Adres (nadawca) zawiera za dużo wierszy, aby poprawnie wyświetlić etykietę.\n\n"));
                }
                CHOSEN_FONT_SENDER = new Font(COMMON_FONT_NAME, FONT_SIZE, FontStyle.Regular);
            }

            TMP_COUNTER = 0;
            foreach (string line in ld.SenderAddress)
            {
                Font TMP_FONT = CHOSEN_FONT_SENDER;
                int TMP_W = (int)G.MeasureString(line, TMP_FONT).Width;
                while (TMP_W > ADRES_BOX_WIDTH)
                {
                    if (TMP_FONT.Size <= 18)
                    {
                        throw new Exception(string.Format("\n\nBłąd: Wiersz za długi (nadawca):\n'{0}'\n\n", line));
                    }
                    TMP_FONT = new Font(COMMON_FONT_NAME, TMP_FONT.Size - 1, TMP_FONT.Style);
                    TMP_W = (int)G.MeasureString(line, TMP_FONT).Width;
                }

                G.DrawString(line, TMP_FONT, Brushes.Black, new Point(SENDER_CONTENT_X, SENDER_CONTENT_Y + TMP_COUNTER));
                TMP_COUNTER += TMP_FONT.Height;
            }

            if (ld.SenderPhone != null && ld.SenderPhone.Trim().Length > 0)
            {
                int SENDER_TELEFON_Y = SPLIT_CROSSING_Y - MARGIN_ADR_TIT_TOP - FONT_SENDER_CONTENT.Height - 20;

                G.DrawString("📞 " + ld.SenderPhone, FONT_SENDER_TELEFON, Brushes.Black, SENDER_CONTENT_X, SENDER_TELEFON_Y);
            }

            bool DRAW_PRIORITY = ld.IsPriority;

            if (DRAW_PRIORITY)
            {
                string PRIORITY_TEXT = "X";
                int LOCATION_PRIORITY_X = 1028;
                int LOCATION_PRIORITY_Y = 2065;
                G.DrawString(PRIORITY_TEXT, FONT_PRIORITY, Brushes.Black, new Point(LOCATION_PRIORITY_X, LOCATION_PRIORITY_Y));
            }

 

            if (stamp?.ImagePartTrackingCode != null)
            { 
                double TRACKING_SCALE_FACTOR = 1.4d;
                Image IMG_TRACKING_SCALED = Util.ResizeImage(stamp.ImagePartTrackingCode, (int)(stamp.ImagePartTrackingCode.Width / TRACKING_SCALE_FACTOR), (int)(stamp.ImagePartTrackingCode.Height / TRACKING_SCALE_FACTOR));
                G.DrawImage(IMG_TRACKING_SCALED, new Point(1130, 2770));
            }


            return BITMAP;
        }

    }
}
