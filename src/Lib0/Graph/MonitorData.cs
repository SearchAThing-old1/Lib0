using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib0.Graph
{

    public class MonitorData : IMonitorData
    {
        public DateTime Timestamp { get; private set; }
        public double Value { get; private set; }
        public int MeanValueCount { get; private set; }
        public TimeSpan MeanTimespan { get; private set; }

        public MonitorData(DateTime timestamp, double value)
        {
            Timestamp = timestamp;
            Value = value;
            MeanValueCount = 1;
        }

        public IMonitorData Clone()
        {
            return new MonitorData(Timestamp, Value);
        }

        public void MeanAdd(IMonitorData data)
        {
            if (data.Timestamp < Timestamp) throw new ArgumentException("Timestamp of data to mean must greather than existing");

            MeanTimespan = data.Timestamp - Timestamp;
            Value = (Value * MeanValueCount + data.Value) / (MeanValueCount + 1);
            ++MeanValueCount;
        }

    }

}
