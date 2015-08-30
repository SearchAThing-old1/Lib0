using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib0.Graph
{
    
    /// <summary>
    /// Define a timerange past from now.
    /// </summary>
    public interface IMonitorDataSet
    {

        /// <summary>
        /// Timespan range name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Timespan value.
        /// </summary>
        TimeSpan? TimespanTotal { get; }

        /// <summary>
        /// Max number of sample points.
        /// </summary>
        int SizeMax { get; }

        /// <summary>
        /// Gets the timespan interval foreach sample average.
        /// </summary>
        TimeSpan? TimespanInterval { get; }

        /// <summary>
        /// Plot points gathered from now back to timespan.
        /// </summary>
        IEnumerable<IMonitorData> Points { get; }

        /// <summary>
        /// Add the given data to the dataset.        
        /// </summary>        
        /// <param name="timestamp">If not null let simulate an arbitrary current time (used primarly by unit tests).</param>
        void Add(IMonitorData data, DateTime? currentTime = null);

    }

}
