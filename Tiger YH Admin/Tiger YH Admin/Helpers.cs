using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger_YH_Admin
{
    public static class Helpers
    {
        public static string Truncate(this string text, int maxLength, string characters = "...")
        {
            if (text.Length <= maxLength)
            {
                return text;
            }

            return text.Substring(0, maxLength - characters.Length) + characters;
        }
    }
}
