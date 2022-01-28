using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eZnaczekPrint
{
    public static class Settings
    {

        public static string ReadSetting(string file)
        {
            if (!Directory.Exists("settings"))
                Directory.CreateDirectory("settings");
            return Util.ReadTextFileOrCreate("settings/" + file + ".txt");
        }

        public static void SaveSetting(string file, string value)
        {
            if (!Directory.Exists("settings"))
                Directory.CreateDirectory("settings");
            File.WriteAllText("settings/" + file + ".txt", value);
        }
    }
}
