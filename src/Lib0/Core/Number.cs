using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lib0.Core
{

    public static partial class Extensions
    {

        /// <summary>
        /// Returns true if two numbers are equals using a tolerance of 1e-6 about the smaller one.
        /// </summary>        
        public static bool EqualsAutoTol(this double x, double y, double precision = 1e-6)
        {
            return Math.Abs(x - y) < Math.Min(x, y) * precision;
        }

    }

}
