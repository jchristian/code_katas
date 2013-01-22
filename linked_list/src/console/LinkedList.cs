using System;
using System.Collections;
using System.Collections.Generic;

namespace console
{
    public class LinkedList<T> : IList<T>
    {
        Node<T> head;
        Node<T> tail; 

        public IEnumerator<T> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            if (head == null)
            {
                head = new Node<T>(item);
                tail = head;
            }
            else
            {
                var old_tail = tail;
                tail = new Node<T>(item);
                old_tail.Next = tail;
            }
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new System.NotImplementedException();
        }

        public int Count { get; private set; }
        public bool IsReadOnly { get; private set; }

        public int IndexOf(T item)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            var nodeAtPreviousIndex = ElementAt(index - 1);
            var newNodeAtIndex = new Node<T>(item, nodeAtPreviousIndex.Next);
            nodeAtPreviousIndex.Next = newNodeAtIndex;
        }

        public void RemoveAt(int index)
        {
            throw new System.NotImplementedException();
        }

        public T this[int index]
        {
            get { return ElementAt(index).Value; }
            set { ElementAt(index).Value = value; }
        }

        Node<T> ElementAt(int index)
        {
            if (index >= Count)
                throw new ArgumentException(null, "index");

            var node = head;

            for (int i = 0; i < index; i++)
                node = node.Next;

            return node;
        }

        class Node<T>
        {
            public T Value { get; set; }
            public Node<T> Next { get; set; }

            public Node(T value) : this(value, null) { }
            public Node(T value, Node<T> next)
            {
                Value = value;
                Next = next;
            }
        }
    }
}