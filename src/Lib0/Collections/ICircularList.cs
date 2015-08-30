using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lib0.Collections
{

    public interface ICircularList<D>
    {

        /// <summary>
        /// Size of the circular list.
        /// When adding elements more than this size existing oldest elements will be overwritten.
        /// </summary>
        int SizeMax { get; }

        /// <summary>
        /// Gets count of items in the circular list.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Items in the circular list from oldest to newest.
        /// </summary>
        IEnumerable<D> Items { get; }

        /// <summary>
        /// Returns the item by given index.
        /// Note that the index 0 corrspond to the oldest inserted (max Count-th item).
        /// </summary>        
        D GetItem(int index);

        /// <summary>
        /// Add given item to the circular list.
        /// </summary>        
        void Add(D data);        

    }

}
