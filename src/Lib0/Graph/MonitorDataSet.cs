using Lib0.Collections;
using System;
using System.Collections.Generic;

namespace Lib0.Graph
{

    public class MonitorDataSet : IMonitorDataSet
    {
        ICircularList<IMonitorData> points;

        public string Name { get; private set; }
        public TimeSpan? TimespanTotal { get; private set; }
        public IEnumerable<IMonitorData> Points { get { return points.Items; } }
        public int SizeMax { get; private set; }
        public TimeSpan? TimespanInterval { get; private set; }

        /// <summary>
        /// Add the given data and store in the dataset computing the mean value if needed.
        /// </summary>        
        public void Add(IMonitorData data, DateTime? currentTime)
        {
            if (!TimespanTotal.HasValue || points.Count == 0) // add points without mean average for default set or if empty set
            {
                points.Add(data.Clone());
            }
            else
            {
                var last = points.GetItem(points.Count - 1);
                var ct = currentTime.HasValue ? currentTime.Value : DateTime.Now;

                // checks if last point timespan not yet overcome
                if (last.Timestamp + TimespanInterval.Value > ct)
                {
                    last.MeanAdd(data);
                }
                else // aggregate data
                {
                    points.Add(data.Clone());
                }         
            }
        }

        internal MonitorDataSet(int sizeMax, string name, TimeSpan? timespan)
        {
            points = new CircularList<IMonitorData>(sizeMax);
            Name = name;
            TimespanTotal = timespan;
            SizeMax = sizeMax;
            if (timespan.HasValue) TimespanInterval = new TimeSpan(timespan.Value.Ticks / sizeMax);
        }
    }

}
