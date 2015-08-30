using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib0.Graph
{

    public class MonitorPlot : IMonitorPlot
    {
        Dictionary<string, IMonitorDataSet> dataSetsDict;

        IMonitorDataSet AddDataSetInternal(string name, TimeSpan? timespan)
        {
            var monitorTimespan = new MonitorDataSet(PlotWidth, name, timespan);
            dataSetsDict.Add(name, monitorTimespan);

            return monitorTimespan;
        }

        /// <summary>
        /// Name of default dataset.
        /// </summary>
        public const string DefaultDataSetName = "default";

        public IEnumerable<IMonitorDataSet> DataSets { get { return dataSetsDict.Values; } }

        public int PlotWidth { get; private set; }

        public void Add(IMonitorData data, DateTime? currentTime)
        {
            foreach (var ds in dataSetsDict.Values)
            {
                ds.Add(data, currentTime);
            }
        }

        public IMonitorDataSet AddDataSet(string name, TimeSpan timespan)
        {
            return AddDataSetInternal(name, timespan);
        }

        public MonitorPlot(int plotWidth)
        {
            dataSetsDict = new Dictionary<string, IMonitorDataSet>();
            PlotWidth = plotWidth;
            AddDataSetInternal(DefaultDataSetName, null);            
        }

    }

}
