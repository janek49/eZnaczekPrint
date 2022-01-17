using eZnaczekPrint.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eZnaczekPrint.Render
{
    public class RenderLabel4x6 : RenderLabelFormat
    {
        public static string GetDisplayName()
        {
            return "Etykieta 4x6 (10x15cm)";
        }

        public override Bitmap DrawLabel(LabelData ld, StampFormat stamp)
        {
            int LBL_W = 1800;
            int LBL_H = 1240;

            Bitmap BITMAP = new Bitmap(LBL_W, LBL_H);
            Graphics G = Graphics.FromImage(BITMAP);

            int OUTER_MARGIN_SIZE_LEFTRIGHT = 20;
            int OUTER_MARGIN_SIZE_UPDOWN = 20;
            int OUTER_FRAME_THICCNES = 2;
            int INNER_BOX_WIDTH = LBL_W - (OUTER_MARGIN_SIZE_LEFTRIGHT * 2);
            int INNER_BOX_HEIGHT = LBL_H - (OUTER_MARGIN_SIZE_UPDOWN * 2);

            Pen PEN_FRAME = new Pen(Brushes.Black, OUTER_FRAME_THICCNES);

            G.FillRectangle(Brushes.White, new Rectangle(0, 0, LBL_W, LBL_H));
            G.DrawRectangle(PEN_FRAME, new Rectangle(OUTER_MARGIN_SIZE_LEFTRIGHT, OUTER_MARGIN_SIZE_UPDOWN, INNER_BOX_WIDTH, INNER_BOX_HEIGHT));

            int SPLIT_LINE_UPDOWN_X = OUTER_MARGIN_SIZE_LEFTRIGHT + (INNER_BOX_WIDTH / 2);
            int SPLIT_LINE_LEFTRIGHT_Y = OUTER_MARGIN_SIZE_UPDOWN + (INNER_BOX_HEIGHT / 2);

            G.DrawLine(PEN_FRAME, new Point(SPLIT_LINE_UPDOWN_X, OUTER_MARGIN_SIZE_UPDOWN), new Point(SPLIT_LINE_UPDOWN_X, LBL_H - OUTER_MARGIN_SIZE_UPDOWN));
            G.DrawLine(PEN_FRAME, new Point(OUTER_MARGIN_SIZE_LEFTRIGHT, SPLIT_LINE_LEFTRIGHT_Y), new Point(LBL_W - OUTER_MARGIN_SIZE_LEFTRIGHT, SPLIT_LINE_LEFTRIGHT_Y));

            int SPLIT_CROSSING_X = SPLIT_LINE_UPDOWN_X;
            int SPLIT_CROSSING_Y = SPLIT_LINE_LEFTRIGHT_Y;

            string COMMON_FONT_NAME = "Arial";
            string EMOJI_FONT_NAME = "Segoe UI Emoji";

            Font FONT_ADRESAT_TITLE = new Font(COMMON_FONT_NAME, 36, FontStyle.Underline | FontStyle.Bold);
            Font FONT_ADRESAT_CONTENT = new Font(COMMON_FONT_NAME, 48, FontStyle.Regular);
            Font FONT_SENDER_TITLE = new Font(COMMON_FONT_NAME, 30, FontStyle.Underline);
            Font FONT_SENDER_CONTENT = new Font(COMMON_FONT_NAME, 34, FontStyle.Regular);
            Font FONT_PRIORITY = new Font(COMMON_FONT_NAME, 72, FontStyle.Bold);
            Font FONT_ADRESAT_TELEFON = new Font(EMOJI_FONT_NAME, 48, FontStyle.Regular);
            Font FONT_SENDER_TELEFON = new Font(EMOJI_FONT_NAME, 34, FontStyle.Regular);

            int MARGIN_ADR_TIT_LEFT = 20;
            int MARGIN_ADR_TIT_TOP = 10;
            int MARGIN_ADR_CNT_LEFT = 20;
            int MARGIN_ADR_CNT_TOP = 75;

            int ADRESAT_CONTENT_X = SPLIT_CROSSING_X + MARGIN_ADR_CNT_LEFT;
            int ADRESAT_CONTENT_Y = SPLIT_CROSSING_Y + MARGIN_ADR_CNT_TOP;

            G.DrawString("Adresat / To:", FONT_ADRESAT_TITLE, Brushes.Black, new Point(SPLIT_CROSSING_X + MARGIN_ADR_TIT_LEFT, SPLIT_CROSSING_Y + MARGIN_ADR_TIT_TOP));

            int ADRESAT_MAX_CONTENT_LINES = 6;

            Font CHOSEN_FONT_ADRESAT = FONT_ADRESAT_CONTENT;

            if (ld.ReceiverAddress.Length > ADRESAT_MAX_CONTENT_LINES)
            {
                int FONT_SIZE = INNER_BOX_HEIGHT / 4 / ld.ReceiverAddress.Length;
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
                while (TMP_W > INNER_BOX_WIDTH / 2)
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


            G.DrawString("Nadawca / From:", FONT_SENDER_TITLE, Brushes.Black,
                new Point(OUTER_MARGIN_SIZE_LEFTRIGHT + MARGIN_ADR_TIT_LEFT, OUTER_MARGIN_SIZE_UPDOWN + MARGIN_ADR_TIT_TOP));

            Font CHOSEN_FONT_SENDER = FONT_SENDER_CONTENT;

            int MARGIN_SENDER_CNT_TOP = 60;
            int SENDER_CONTENT_X = OUTER_MARGIN_SIZE_LEFTRIGHT + MARGIN_ADR_TIT_LEFT;
            int SENDER_CONTENT_Y = OUTER_MARGIN_SIZE_UPDOWN + MARGIN_SENDER_CNT_TOP;
            int SENDER_MAX_CONTENT_LINES = 8;

            if (ld.SenderAddress.Length > SENDER_MAX_CONTENT_LINES)
            {
                int FONT_SIZE = (INNER_BOX_HEIGHT - 35) / 4 / ld.SenderAddress.Length;
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
                while (TMP_W > INNER_BOX_WIDTH / 2)
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
                int SENDER_TELEFON_Y = SPLIT_CROSSING_Y - MARGIN_ADR_TIT_TOP - FONT_SENDER_CONTENT.Height - 10;

                G.DrawString("📞 " + ld.SenderPhone, FONT_SENDER_TELEFON, Brushes.Black, SENDER_CONTENT_X, SENDER_TELEFON_Y);
            }

            bool DRAW_PRIORITY = ld.IsPriority;
            int MARGIN_PRIORITY_TOP = 10;

            if (DRAW_PRIORITY)
            {
                string PRIORITY_TEXT = "PRIORYTET";
                int LOCATION_PRIORITY_X = OUTER_MARGIN_SIZE_LEFTRIGHT + (INNER_BOX_WIDTH / 4 - (int)G.MeasureString(PRIORITY_TEXT, FONT_PRIORITY).Width / 2);
                G.DrawString(PRIORITY_TEXT, FONT_PRIORITY, Brushes.Black, new Point(LOCATION_PRIORITY_X, SPLIT_CROSSING_Y + MARGIN_PRIORITY_TOP));
            }


            if (stamp?.ImageWholeStamp != null)
            {
                int STAMP_MARGIN_LEFT = 10;
                int STAMP_MARGIN_TOP = 10;
                double STAMP_SCALE_FACTOR = 1.60d;
                Image IMG_STAMP_SCALED = Util.ResizeImage(stamp.ImagePartUpperPart, (int)(stamp.ImagePartUpperPart.Width / STAMP_SCALE_FACTOR), (int)(stamp.ImagePartUpperPart.Height / STAMP_SCALE_FACTOR));
                G.DrawImage(IMG_STAMP_SCALED, new Point(SPLIT_CROSSING_X + STAMP_MARGIN_LEFT, OUTER_MARGIN_SIZE_UPDOWN + STAMP_MARGIN_TOP));
            }

            if (stamp?.ImagePartTrackingCode != null)
            {
                int TRACKING_MARGIN_LEFT = 30;
                int TRACKING_MARGIN_TOP = 150;
                double TRACKING_SCALE_FACTOR = 1.85d;
                Image IMG_TRACKING_SCALED = Util.ResizeImage(stamp.ImagePartTrackingCode, (int)(stamp.ImagePartTrackingCode.Width / TRACKING_SCALE_FACTOR), (int)(stamp.ImagePartTrackingCode.Height / TRACKING_SCALE_FACTOR));
                G.DrawImage(IMG_TRACKING_SCALED, new Point(OUTER_MARGIN_SIZE_LEFTRIGHT + TRACKING_MARGIN_LEFT, SPLIT_CROSSING_Y + TRACKING_MARGIN_TOP));
            }


            return BITMAP;
        }

    }
}
