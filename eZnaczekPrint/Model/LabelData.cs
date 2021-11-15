using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eZnaczekPrint.Model
{
    public class LabelData
    {
        public string[] SenderAddress, ReceiverAddress;
        public string SenderPhone, ReceiverPhone;
        public bool IsPriority;
    }
}
