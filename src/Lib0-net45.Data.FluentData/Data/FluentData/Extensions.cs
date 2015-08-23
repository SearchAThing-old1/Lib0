#region Lib0.Data.FluentData, Copyright(C) 2015 Lorenzo Delana, License under MIT
/*
 * The MIT License(MIT)
 *
 * Copyright(c) 2015 Lorenzo Delana, http://development-annotations.blogspot.it/
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

using FluentData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lib0.Data.FluentData
{

    public static class Extensions
    {

        /// <summary>
        /// Loads destination records referenced by the specified foreign source key.
        /// </summary>
        /// <param name="src">List of source records to expand.</param>
        /// <param name="ctx">Database context.</param>
        /// <param name="dstTableName">Destination table name.</param>
        /// <param name="srcKey">Selector for the source key.</param>
        /// <param name="dstKey">Selector for the destination key.</param>
        /// <returns>List of records with the new foreign key property named with the given destination table name.</returns>
        public static List<dynamic> LoadReferences(this List<dynamic> src,
            IDbContext ctx,
            string dstTableName,
            Func<dynamic, dynamic> srcKey,
            Func<dynamic, dynamic> dstKey)
        {
            var q = ctx.Sql($"select * from {dstTableName}").QueryMany<dynamic>()
                .ToDictionary(k => dstKey(k), v => v);

            foreach (var x in src.Cast<IDictionary<string, object>>())
            {
                var k = srcKey(x);

                dynamic v = null;
                if (q.TryGetValue(k, out v))
                {
                    x.Add(dstTableName, v);
                }
            }

            return src;
        }

    }

}
