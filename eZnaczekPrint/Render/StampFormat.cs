using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eZnaczekPrint.Render
{
    public class StampFormat
    {
        public Image ImageWholeStamp;
        public Image ImagePartUpperPart;
        public Image ImagePartTrackingCode;

        public static string GetDisplayName()
        {
            return "#NO_NAME#";
        }
    }
}
