#region Lib0.Test, Copyright(C) 2015 Lorenzo Delana, License under MIT
/*
 * The MIT License(MIT)
 *
 * Copyright(c) 2015 Lorenzo Delana <lorenzo.delana@gmail.com>
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
*/
#endregion

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
