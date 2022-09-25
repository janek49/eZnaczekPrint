using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eZnaczekPrint.Render
{
    public class StampFormatDomesticRegistered : StampFormat
    {
         int STAMP_UPPER_PART_X = 110;
         int STAMP_UPPER_PART_Y = 40;
         int STAMP_UPPER_PART_WIDTH = 1360;
         int STAMP_UPPER_PART_HEIGHT = 510;

        const int STAMP_TRACKING_CODE_X = 15;
        const int STAMP_TRACKING_CODE_Y = 550;
        const int STAMP_TRACKING_CODE_WIDTH = 1535;
        const int STAMP_TRACKING_CODE_HEIGHT = 550;

        public StampFormatDomesticRegistered(Image input)
        {
            if (input == null)
                return;

            ImageWholeStamp = input;
            Bitmap upperPart = new Bitmap(STAMP_UPPER_PART_WIDTH, STAMP_UPPER_PART_HEIGHT);
            using (Graphics g = Graphics.FromImage(upperPart))
            {
                g.FillRectangle(Brushes.White, 0, 0, upperPart.Width, upperPart.Height);
                g.DrawImage(input, -STAMP_UPPER_PART_X, -STAMP_UPPER_PART_Y, input.Width, input.Height);
            }
            ImagePartUpperPart = upperPart;
            Bitmap trackingCode = new Bitmap(STAMP_TRACKING_CODE_WIDTH, STAMP_TRACKING_CODE_HEIGHT);
            using (Graphics g = Graphics.FromImage(trackingCode))
            {
                g.FillRectangle(Brushes.White, 0, 0, trackingCode.Width, trackingCode.Height);
                g.DrawImage(input, -STAMP_TRACKING_CODE_X, -STAMP_TRACKING_CODE_Y, input.Width, input.Height);
            }
            ImagePartTrackingCode = trackingCode;
        }

        public static new string GetDisplayName()
        {
            return "List polecony krajowy";
        }

    }
}
