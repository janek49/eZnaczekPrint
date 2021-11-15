using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eZnaczekPrint.Render
{
    public class PocztaPolskaReader
    {
        public static Image ReadPdfToImage(string file)
        {
            using (var dokument = PdfiumViewer.PdfDocument.Load(file))
            {
                int x = 4960;
                int y = 7016;
                return dokument.Render(0, x, y, 96, 96, true);
            }
        }

        public static Bitmap CutOutStamp(Image page)
        {
            int STAMP_WIDTH = 1560;
            int STAMP_HEIGHT = 1110;
            int STAMP_POS_X = 170;
            int STAMP_POS_Y = 205;
            int PAGE_WIDTH_PX = 4960;
            int PAGE_HEIGHT_PX = 7016;
            Bitmap bmp = new Bitmap(STAMP_WIDTH, STAMP_HEIGHT);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));
                g.DrawImage(page, -STAMP_POS_X, -STAMP_POS_Y, PAGE_WIDTH_PX, PAGE_HEIGHT_PX);
            }
            return bmp;
        }
    }
}
