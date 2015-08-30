using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lib0.Core
{

    public static partial class Extensions
    {

        /// <summary>
        /// Returns the given string stripped from the given part if exists at beginning.
        /// </summary>        
        public static string StripBegin(this string str, char c)
        {
            if (str.Length > 0 && str[0] == c)
                return str.Substring(1, str.Length - 1);
            else
                return str;
        }

        /// <summary>
        /// Returns the given string stripped from the given part if exists at beginning.
        /// </summary>        
        public static string StripBegin(this string str, string partToStrip)
        {
            if (str.StartsWith(partToStrip))
            {
                var ptsl = partToStrip.Length;
                return str.Substring(ptsl, str.Length - ptsl);
            }
            else
                return str;
        }

        /// <summary>
        /// Returns the given string stripped from the given part if exists at end.
        /// </summary>        
        public static string StripEnd(this string str, char c)
        {
            if (str.Length>0 && str[str.Length-1] == c)
                return str.Substring(0, str.Length-1);
            else
                return str;
        }

        /// <summary>
        /// Returns the given string stripped from the given part if exists at end.
        /// </summary>        
        public static string StripEnd(this string str, string partToStrip)
        {
            if (str.EndsWith(partToStrip))
                return str.Substring(0, str.Length - partToStrip.Length);
            else
                return str;
        }

        /// <summary>
        /// Smart line splitter that split a text into lines whatever unix or windows line ending style.
        /// By default its remove empty lines.
        /// </summary>        
        /// <param name="removeEmptyLines">If true remove empty lines.</param>        
        public static IEnumerable<string> Lines(this string txt, bool removeEmptyLines = true)
        {
            var q = txt.Replace("\r\n", "\n").Split('\n');

            if (removeEmptyLines)
                return q.Where(r=>r.Trim().Length>0);
            else
                return q;
        }

    }

}
