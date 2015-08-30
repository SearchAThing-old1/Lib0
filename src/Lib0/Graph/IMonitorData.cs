using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib0.Graph
{

    /// <summary>
    /// Monitor plot point with its timestamp and value.
    /// </summary>    
    public interface IMonitorData
    {

        /// <summary>
        /// Time of sampling.
        /// </summary>
        DateTime Timestamp { get; }

        /// <summary>
        /// Value sampled ad the timestamp.
        /// </summary>
        double Value { get; }

        /// <summary>
        /// Retrieve the count of mean items that formed Value and Timestamp.
        /// </summary>
        int MeanValueCount { get; }

        /// <summary>
        /// Retrieve the timespan from the [Timestamp] over which the Value is computed as mean.
        /// </summary>
        TimeSpan MeanTimespan { get; }

        /// <summary>
        /// Add the given data to mean at this one.
        /// Pre: Timestamp of given data must greather than this.
        /// </summary>        
        void MeanAdd(IMonitorData data);

        /// <summary>
        /// Clone this object.
        /// </summary>        
        IMonitorData Clone();

    }

}
