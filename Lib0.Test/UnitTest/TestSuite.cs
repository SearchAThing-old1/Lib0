#region Lib0.Test, Copyright(C) 2015 Lorenzo Delana, License under MIT
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

using Lib0.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Lib0.Test.UnitTest
{

    [TestClass]
    public class TestSuite
    {

        internal static void RunAll()
        {
            var t = typeof(TestSuite);

            var methods = t.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly);

            var obj = new TestSuite();

            foreach (var method in methods)
            {
                method.Invoke(obj, new object[] { });
            }
        }

        /// <summary>
        /// Event opertion with custom args.
        /// </summary>
        [TestMethod]
        public void Test1()
        {
            IEventOperation<MyEventArgs> op = new EventOperation<MyEventArgs>();

            op.Event += (a, b) =>
            {
                Assert.IsTrue(b.Type == EventTypes.EventA);
            };

            op.Fire(null, new MyEventArgs(EventTypes.EventA));

            Assert.IsTrue(op.FireCount == 1);
            Assert.IsTrue(op.HandledCount == 1);
        }

        /// <summary>
        /// FireCount, HandledCount.
        /// </summary>
        [TestMethod]
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

            Assert.IsTrue(op.FireCount == 1);
            Assert.IsTrue(op.HandledCount == 2);

            op.Fire();

            Assert.IsTrue(op.FireCount == 2);
            Assert.IsTrue(op.HandledCount == 2 + 3);
        }

        /// <summary>
        /// Event fired, but not handler yet attached.
        /// </summary>
        [TestMethod]
        public void Test3()
        {
            IEventOperation op = new EventOperation();

            op.Fire();

            op.Event += (a, b) =>
            {
            };

            Assert.IsTrue(op.FireCount == 1);
            Assert.IsTrue(op.HandledCount == 0);
        }

        /// <summary>
        /// Be notified after the event fired.
        /// </summary>
        [TestMethod]
        public void Test4()
        {
            IEventOperation op = new EventOperation(EventOperationBehaviorTypes.RemindPastEvents);

            op.Fire();

            Assert.IsTrue(op.FireCount == 1);
            Assert.IsTrue(op.HandledCount == 0);

            op.Event += (a, b) =>
            {
            };

            Assert.IsTrue(op.FireCount == 1);
            Assert.IsTrue(op.HandledCount == 1);
        }

        /// <summary>
        /// Be notified after the event fired (multiple listeners).
        /// </summary>
        [TestMethod]
        public void Test5()
        {
            IEventOperation op = new EventOperation(EventOperationBehaviorTypes.RemindPastEvents);

            op.Fire();

            Assert.IsTrue(op.FireCount == 1);
            Assert.IsTrue(op.HandledCount == 0);

            op.Event += (a, b) =>
            {
            };

            Assert.IsTrue(op.FireCount == 1);
            Assert.IsTrue(op.HandledCount == 1);
        }
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

}
