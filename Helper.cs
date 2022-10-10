using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaDiskSdk
{
    public static class Helper
    {
        public static string ReplaceCharsToUri(this string line)
        {
            return Uri.EscapeDataString(line);
        }
    }
}
