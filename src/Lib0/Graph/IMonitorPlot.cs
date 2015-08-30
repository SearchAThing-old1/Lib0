using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib0.Graph
{

    /// <summary>
    /// Manage the insertion and retrival of monitoring plot data.
    /// </summary>    
    public interface IMonitorPlot
    {

        /// <summary>
        /// Add the given sampling data to the monitor plot.
        /// </summary>        
        /// <param name="currentTime">If not null allow to override current time (used primarly by unit tests).</param>
        void Add(IMonitorData data, DateTime? currentTime = null);

        /// <summary>
        /// Gets the width of the plot area.
        /// </summary>
        int PlotWidth { get; }

        /// <summary>
        /// Retrieve the list of available datasets.
        /// Foreach dataset is a [PlotWidth] dataset recorded as a result of the mean data.
        /// </summary>
        IEnumerable<IMonitorDataSet> DataSets { get; }

        /// <summary>
        /// Add a dataset window to keep track of gathered data in the timespan back to now.
        /// </summary>        
        IMonitorDataSet AddDataSet(string name, TimeSpan timespan);        

    }

}
