using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalTimeTracker
{
    /// <summary>
    /// A stack that can only hold a limited number of items.
    /// </summary>
    public class LimitedStack<T>
    {
        private const int DEFAULT_CAPACITY = 10;

        private List<T> m_items;
        private int m_capacity;

        public LimitedStack()
        {
            m_items = new List<T>();
            m_capacity = DEFAULT_CAPACITY;
        }

        public LimitedStack(int capacity) : this()
        {
            Capacity = capacity;
        }

        /// <summary>
        /// Gets or sets the number of elements that can be in the stack.
        /// </summary>
        public int Capacity
        {
            get { return m_capacity; }
            set
            {
                System.Diagnostics.Debug.Assert(value > 0, "Capacity must be greater than 0");
                if (value > 0)
                {
                    m_capacity = value;
                    // Make stack meet capacity restriction
                    while (m_items.Count > m_capacity)
                    {
                        m_items.RemoveAt(m_items.Count - 1);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the number of elements in the stack.
        /// </summary>
        public int Count
        {
            get { return m_items.Count; }
        }

        /// <summary>
        /// Push an item onto the stack.
        /// </summary>
        /// <param name="item">The item</param>
        /// <remarks>Will not let the stack grow beyond the set capacity.</remarks>
        public void Push(T item)
        {
            m_items.Insert(0, item);
            if (m_items.Count > m_capacity)
            {
                m_items.RemoveAt(m_items.Count - 1);
            }
        }

        /// <summary>
        /// Remove the top most item from the stack and return it.
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            T item = m_items[0];
            m_items.RemoveAt(0);
            return item;
        }

        /// <summary>
        /// Remove all items from stack.
        /// </summary>
        public void Clear()
        {
            m_items.Clear();
        }
    }
}
