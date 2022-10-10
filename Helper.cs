using System;

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
