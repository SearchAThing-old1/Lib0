using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lib0.Collections
{

    public class CircularList<D> : ICircularList<D>
    {
        D[] items;
        int beginIdx = 0;
        int curIdx = 0;

        public IEnumerable<D> Items
        {
            get
            {
                return new CircularListEnumerable(this);
            }
        }

        public int SizeMax { get; private set; }

        public int Count { get; private set; }

        public void Add(D data)
        {
            if (Count < SizeMax)
            {
                items[curIdx++] = data;
                ++Count;
            }
            else // use of circular logic
            {
                ++beginIdx;
                if (beginIdx >= SizeMax) beginIdx = 0;

                if (curIdx >= SizeMax) curIdx = 0;

                items[curIdx++] = data;
            }
        }

        public D GetItem(int index)
        {
            if (index >= Count)
                throw new IndexOutOfRangeException();

            var i = beginIdx + index;
            if (i >= SizeMax) i = i % SizeMax;

            return items[i];
        }

        public CircularList(int size)
        {
            SizeMax = size;
            items = new D[SizeMax];
        }

        #region CircularListEnumerable
        class CircularListEnumerable : IEnumerable<D>
        {
            CircularList<D> circularList;

            public IEnumerator<D> GetEnumerator()
            {
                return new CircularListEnumerator(circularList);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public CircularListEnumerable(CircularList<D> cl)
            {
                circularList = cl;
            }
        }
        #endregion

        #region CircularListEnumerator
        class CircularListEnumerator : IEnumerator<D>
        {
            int pos = -1;
            CircularList<D> circularList;

            public D Current
            {
                get
                {
                    return circularList.GetItem(pos);
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public CircularListEnumerator(CircularList<D> cl)
            {
                circularList = cl;
            }

            public void Dispose()
            {                                
            }

            public bool MoveNext()
            {               
                ++pos;
                return (pos < circularList.Count);
            }

            public void Reset()
            {
                pos = -1;
            }

        }
        #endregion

    }

}
