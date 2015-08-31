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

using Lib0.Core;
using System.Reflection;
using System;
using Xunit;
using Lib0.Graph;
using Lib0.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lib0.Test.UnitTest
{

    public class TestSuite
    {

        internal static void RunAll()
        {
            var t = typeof(TestSuite);

            var methods = t.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            var obj = new TestSuite();

            foreach (var method in methods)
            {
                Console.WriteLine($"running test [{method.Name}]");
                method.Invoke(obj, new object[] { });
            }
        }

        #region event operation [tests]

        /// <summary>
        /// Event opertion with custom args.
        /// </summary>
        [Fact]
        public void Test1()
        {
            IEventOperation<MyEventArgs> op = new EventOperation<MyEventArgs>();

            op.Event += (a, b) =>
            {
                Assert.True(b.Type == EventTypes.EventA);
            };

            op.Fire(null, new MyEventArgs(EventTypes.EventA));

            Assert.True(op.FireCount == 1);
            Assert.True(op.HandledCount == 1);
        }

        /// <summary>
        /// FireCount, HandledCount.
        /// </summary>
        [Fact]
        public void Test2()
        {
            IEventOperation op = new EventOperation();

            op.Event += (a, b) =>
            {
            };

            op.Event += (a, b) =>
            {
            };

            op.Fire();

            op.Event += (a, b) =>
            {
            };

            Assert.True(op.FireCount == 1);
            Assert.True(op.HandledCount == 2);

            op.Fire();

            Assert.True(op.FireCount == 2);
            Assert.True(op.HandledCount == 2 + 3);
        }

        /// <summary>
        /// Event fired, but not handler yet attached.
        /// </summary>
        [Fact]
        public void Test3()
        {
            IEventOperation op = new EventOperation();

            op.Fire();

            op.Event += (a, b) =>
            {
            };

            Assert.True(op.FireCount == 1);
            Assert.True(op.HandledCount == 0);
        }

        /// <summary>
        /// Be notified after the event fired.
        /// </summary>
        [Fact]
        public void Test4()
        {
            IEventOperation op = new EventOperation(EventOperationBehaviorTypes.RemindPastEvents);

            op.Fire();

            Assert.True(op.FireCount == 1);
            Assert.True(op.HandledCount == 0);

            op.Event += (a, b) =>
            {
            };

            Assert.True(op.FireCount == 1);
            Assert.True(op.HandledCount == 1);
        }

        /// <summary>
        /// Be notified after the event fired (multiple listeners).
        /// </summary>
        [Fact]
        public void Test5()
        {
            IEventOperation op = new EventOperation(EventOperationBehaviorTypes.RemindPastEvents);

            var listener1HitCount = 0;
            var listener2HitCount = 0;

            {
                op.Fire(); // event fired ( no handlers yet connected )
                Assert.True(op.FireCount == 1 && op.HandledCount == 0);

                op.Event += (a, b) => // listener1 connects and receive 1 event
                {
                    ++listener1HitCount;
                };
                Assert.True(op.FireCount == 1 && op.HandledCount == 1);
            }

            {
                op.Fire(); // event fired ( listener1 will receive its 2-th event )
                Assert.True(op.FireCount == 2 && op.HandledCount == 2);

                op.Event += (a, b) => // listener2 connected and receive 2 events
                {
                    ++listener2HitCount;
                };
            }

            Assert.True(listener1HitCount == 2 && listener2HitCount == 2);
            Assert.True(op.FireCount == 2 && op.HandledCount == listener1HitCount + listener2HitCount);
        }

        #endregion

        #region string [tests]

        [Fact]
        public void StringTest1()
        {
            var s1 = "Hi this is a sample";
            var s2 = " and this is another";

            Assert.True(s1.StripBegin("Hi ") == "this is a sample");
            Assert.True(s1.StripEnd("a sample") == "Hi this is ");
            Assert.True(s2.StripBegin("and this") == " and this is another"); // begin part not stripped cause not trimmed
            Assert.True(s2.Trim().StripBegin("and this") == " is another");
        }

        #endregion

        #region circular list [tests]
        [Fact]
        public void CircularListTest1()
        {
            ICircularList<int> lst = new CircularList<int>(5);

            for (int i = 0; i < 5; ++i) lst.Add(i);

            // verify assert's integrity
            Assert.True(lst.Count == 5);
            Assert.True(lst.GetItem(0) == 0);
            Assert.True(lst.GetItem(4) == 4);

            lst.Add(5); // step out from the max size

            Assert.True(lst.Count == 5);
            Assert.True(lst.GetItem(0) == 1);
            Assert.True(lst.GetItem(4) == 5);

            // complete count items
            for (int i = 0; i < 4; ++i) lst.Add(6 + i);

            // verify assert's integrity
            Assert.True(lst.Count == 5);
            Assert.True(lst.GetItem(0) == 5);
            Assert.True(lst.GetItem(4) == 9);
        }

        /// <summary>
        /// Test enumerator
        /// </summary>
        [Fact]
        public void CircularListTest2()
        {
            ICircularList<int> lst = new CircularList<int>(5);

            for (int i = 0; i < 15; ++i) lst.Add(i);

            // verify assert's integrity
            Assert.True(lst.Count == 5);
            Assert.True(lst.GetItem(0) == 10);
            Assert.True(lst.GetItem(4) == 14);

            var l = lst.Items.ToList();

            Assert.True(l.Count == 5);
            Assert.True(l[0] == 10);
            Assert.True(l[4] == 14);
        }
        #endregion

        #region monitor plot [tests]

        [Fact]
        public void MonitorPlotTest1()
        {
            IMonitorPlot plot = new MonitorPlot(3);

            var dsmean = plot.AddDataSet("6 seconds", TimeSpan.FromSeconds(6));

            var dtnow = DateTime.Now;
            var ts = TimeSpan.FromSeconds(1);

            plot.Add(new MonitorData(dtnow, 5), dtnow); dtnow += ts;
            plot.Add(new MonitorData(dtnow, 6), dtnow); dtnow += ts;
            plot.Add(new MonitorData(dtnow, 1), dtnow); dtnow += ts;
            plot.Add(new MonitorData(dtnow, 2), dtnow); dtnow += ts;
            plot.Add(new MonitorData(dtnow, 3), dtnow); dtnow += ts;
            plot.Add(new MonitorData(dtnow, 4), dtnow); dtnow += ts;

            var q1 = plot.DataSets.First(w => w.Name == "default");
            var q1lst = q1.Points.ToList();
            Assert.True(q1lst.Count == 3);
            // checks that the first "default" set window move to the last three data
            Assert.True(q1lst[0].Value.EqualsAutoTol(2));
            Assert.True(q1lst[1].Value.EqualsAutoTol(3));
            Assert.True(q1lst[2].Value.EqualsAutoTol(4));

            // now check that the "6 seconds" dataset mean 3 points over last 6 seconds
            // in other words it will get (5.5, 1.5, 3.5) that are means of ( (5,6), (1,2), (3,4) )
            var meanlst = dsmean.Points.ToList();
            Assert.True(meanlst.Count == 3);
            Assert.True(meanlst[0].Value.EqualsAutoTol(5.5));
            Assert.True(meanlst[1].Value.EqualsAutoTol(1.5));
            Assert.True(meanlst[2].Value.EqualsAutoTol(3.5));

            // proceed adding 2 more items
            plot.Add(new MonitorData(dtnow, 6), dtnow); dtnow += ts;
            plot.Add(new MonitorData(dtnow, 7), dtnow); dtnow += ts;
            
            q1lst = q1.Points.ToList();
            Assert.True(q1lst.Count==3);
            Assert.True(q1lst[0].Value.EqualsAutoTol(4));
            Assert.True(q1lst[1].Value.EqualsAutoTol(6));
            Assert.True(q1lst[2].Value.EqualsAutoTol(7));

            // check the mean, should be (1.5, 3.5, 6.5)
            meanlst = dsmean.Points.ToList();
            Assert.True(meanlst.Count == 3);
            Assert.True(meanlst[0].Value.EqualsAutoTol(1.5));
            Assert.True(meanlst[1].Value.EqualsAutoTol(3.5));
            Assert.True(meanlst[2].Value.EqualsAutoTol(6.5));
        }
        #endregion
    }

    public enum EventTypes { EventA, EventB };

    public class MyEventArgs : EventArgs
    {
        public EventTypes Type { get; private set; }

        public MyEventArgs(EventTypes type)
        {
            Type = type;
        }
    }

    public class Program
    {

        public static void Main(string[] args)
        {
            TestSuite.RunAll();

            Console.WriteLine("All test done. Hit enter to exit...");
            Console.Read();
        }

    }

}
