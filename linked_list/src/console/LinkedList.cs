using System;
using System.Collections;
using System.Collections.Generic;

namespace console
{
    public class LinkedList<T> : IList<T>
    {
        Node<T> head;

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            if (head == null)
                head = new Node<T>(item);
            else
                GetTail().Next = new Node<T>(item);

            Count++;
        }

        public void Clear()
        {
            head = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (var item in array)
                Insert(arrayIndex++, item);
        }

        public bool Remove(T item)
        {
            if (!Contains(item))
                return false;

            RemoveAt(IndexOf(item));

            return true;
        }

        public int Count { get; private set; }
        public bool IsReadOnly { get; private set; }

        public int IndexOf(T item)
        {
            var currentNode = head;
            var currentIndex = 0;
            
            while (currentNode != null)
            {
                if (currentNode.Value.Equals(item))
                    return currentIndex;

                currentNode = currentNode.Next;
                currentIndex++;
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            var nodeAtPreviousIndex = ElementAt(index - 1);
            var newNodeAtIndex = new Node<T>(item, nodeAtPreviousIndex.Next);
            nodeAtPreviousIndex.Next = newNodeAtIndex;
            Count++;
        }

        public void RemoveAt(int index)
        {
            if (index == 0)
                head = head.Next;
            else
            {
                var nodeOfPreviousItem = ElementAt(index - 1);
                nodeOfPreviousItem.Next = nodeOfPreviousItem.Next.Next;
            }

            Count--;
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

        Node<T> GetTail()
        {
            return ElementAt(Count - 1);
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

        public class Enumerator : IEnumerator<T>
        {
            LinkedList<T> list;
            Node<T> currentNode;

            public Enumerator(LinkedList<T> list)
            {
                this.list = list;
            }

            public void Dispose() { }

            public bool MoveNext()
            {
                if (currentNode == null)
                    currentNode = list.head;
                else
                    currentNode = currentNode.Next;

                return currentNode != null;
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }

            public T Current { get { return currentNode.Value; } }
            object IEnumerator.Current
            {
                get { return Current; }
            }
        }
    }
}