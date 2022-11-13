using System.Collections;

namespace LinkedList
{
    public class MyLinkedList<T> : ICollection<T>
    {
        public MyLinkedListNode<T>? First { get; private set; }
        public MyLinkedListNode<T>? Last { get; private set; }
        public int Count { get; private set; } = 0;
        private int State { get; set; } = 0;
        bool ICollection<T>.IsReadOnly => false;

        public delegate void LinkedListDelegate(string message);
        public event LinkedListDelegate? Notify;

        public MyLinkedList() { }

        public MyLinkedList(IEnumerable<T> collection)
        {
            foreach (T item in collection)
            {
                AddLast(item);
            }
        }

        public MyLinkedListNode<T> AddLast(T item)
        {
            MyLinkedListNode<T> newNode = new(this, item);
            if (Count == 0)
            {
                InsertNodeToEmptyList(newNode);
            }
            else
            {
                InsertNodeAfter(Last!, newNode);
            }
            return newNode;
        }

        public void AddLast(MyLinkedListNode<T> newNode)
        {
            VerifyIsTheNodeUnowned(newNode);
            if (Count == 0)
            {
                InsertNodeToEmptyList(newNode);
            }
            else
            {
                InsertNodeAfter(Last!, newNode);
            }
            newNode.List = this;
        }

        public MyLinkedListNode<T> AddFirst(T item)
        {
            MyLinkedListNode<T> newNode = new(this, item);
            if (Count == 0)
            {
                InsertNodeToEmptyList(newNode);
            }
            else
            {
                InsertNodeBefore(First!, newNode);
            }
            return newNode;
        }

        public void AddFirst(MyLinkedListNode<T> newNode)
        {
            VerifyIsTheNodeUnowned(newNode);
            if (Count == 0)
            {
                InsertNodeToEmptyList(newNode);
            }
            else
            {
                InsertNodeBefore(First!, newNode);
            }
            newNode.List = this;
        }

        public MyLinkedListNode<T> AddAfter(MyLinkedListNode<T> node, T item)
        {
            VerifyIsTheNodeAttached(node);
            MyLinkedListNode<T> newNode = new(this, item);
            InsertNodeAfter(node, newNode);
            return newNode;
        }

        public void AddAfter(MyLinkedListNode<T> node, MyLinkedListNode<T> newNode)
        {
            VerifyIsTheNodeAttached(node);
            VerifyIsTheNodeUnowned(newNode);
            InsertNodeAfter(node, newNode);
            newNode.List = this;
        }

        public MyLinkedListNode<T> AddBefore(MyLinkedListNode<T> node, T item)
        {
            VerifyIsTheNodeAttached(node);
            MyLinkedListNode<T> newNode = new(this, item);
            InsertNodeBefore(node, newNode);
            return newNode;
        }

        public void AddBefore(MyLinkedListNode<T> node, MyLinkedListNode<T> newNode)
        {
            VerifyIsTheNodeAttached(node);
            VerifyIsTheNodeUnowned(newNode);
            InsertNodeBefore(node, newNode);
            newNode.List = this;
        }

        void ICollection<T>.Add(T item)
        {
            AddLast(item);
        }

        public MyLinkedListNode<T>? Find(T item)
        {
            MyLinkedListNode<T>? current = First;
            while (current != null && !current.Value!.Equals(item))
            {
                current = current!.Next;
            }
            return current;
        }

        public bool Remove(T item)
        {
            MyLinkedListNode<T>? node = Find(item);
            if (node != null)
            {
                RemoveNode(node);
                return true;
            }
            return false;
        }

        public void Remove(MyLinkedListNode<T> node)
        {
            VerifyIsTheNodeAttached(node);
            RemoveNode(node);
        }

        public void RemoveFirst()
        {
            VerifyIsTheListEmpty();
            RemoveNode(First!);
        }

        public void RemoveLast()
        {
            VerifyIsTheListEmpty();
            RemoveNode(Last!);
        }

        public MyLinkedListNode<T>? FindLast(T item)
        {
            MyLinkedListNode<T>? current = Last;
            while (current != null && !current.Value!.Equals(item))
            {
                current = current!.Previous;
            }
            return current;
        }

        public bool Contains(T item)
        {
            return Find(item) != null;
        }

        public void Clear()
        {
            MyLinkedListNode<T>? current = First;
            while (current != null)
            {
                MyLinkedListNode<T>? temp = current;
                current = current!.Next;
                temp.Next = null;
                temp.Previous = null;
                temp.List = null;
            }
            Count = 0;
            First = null;
            Last = null;
            State++;
            Notify?.Invoke("The list was emptied.");
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex < 0 || arrayIndex >= array.Length)
            {
                throw new ArgumentOutOfRangeException("Such an index does not exist.");
            }
            
            if (Count > array.Length - arrayIndex)
            {
                throw new ArgumentException("The data cannot fit into the array.");
            }

            MyLinkedListNode<T> temp = First!;
            while (temp != null)
            {
                array.SetValue(temp.Value, arrayIndex++);
                temp = temp.Next!;
            }
        }

        private void InsertNodeToEmptyList(MyLinkedListNode<T> node)
        {
            First = node;
            Last = node;
            Count++;
            State++;
            Notify?.Invoke($"Node with the value of '{node.Value}' was added to an empty list.");
        }

        private void InsertNodeBefore(MyLinkedListNode<T> node, MyLinkedListNode<T> newNode)
        {
            if (node != First)
            {
                newNode.Previous = node.Previous;
                node.Previous!.Next = newNode;
            }
            else
            {
                First = newNode;
            }

            newNode.Next = node;
            node.Previous = newNode;
            Count++;
            State++;
            Notify?.Invoke($"New node with the value of '{newNode.Value}' was " +
                $"inserted before the node with the value of '{node.Value}'.");
        }

        private void InsertNodeAfter(MyLinkedListNode<T> node, MyLinkedListNode<T> newNode)
        {
            if (node != Last)
            {
                newNode.Next = node.Next;
                node.Next!.Previous = newNode;
            }
            else
            {
                Last = newNode;
            }

            node.Next = newNode;
            newNode.Previous = node;
            Count++;
            State++;
            Notify?.Invoke($"New node with the value of '{newNode.Value}' was " +
                $"inserted after the node with the value of '{node.Value}'.");
        }

        private void RemoveNode(MyLinkedListNode<T> node)
        {
            if (node == First)
            {
                First = First.Next;

                if (Count - 1 == 0)
                {
                    Last = null;
                }
                else
                {
                    First!.Previous = null;
                }
            }
            else if (node == Last)
            {
                if (Count == 1)
                {
                    First = null;
                    Last = null;
                }
                else
                {
                    Last.Previous!.Next = null; 
                    Last = Last.Previous;
                }
            }
            else
            {
                node.Next!.Previous = node.Previous;
                node.Previous!.Next = node.Next;
            }
            
            node.Next = null;
            node.Previous = null;
            node.List = null;
            Count--;
            State++;
            Notify?.Invoke($"Node with the value of '{node.Value}' was removed from the list.");
        }

        private static void VerifyIsTheNodeUnowned(MyLinkedListNode<T> node)
        {
            if (node.List != null)
            {
                throw new InvalidOperationException
                    ("The node must not be attached to any list to proceed with this operation.");
            }
        }

        private void VerifyIsTheNodeAttached(MyLinkedListNode<T> node)
        {
            if (node.List != this)
            {
                throw new InvalidOperationException
                    ("The node is either unattached or attached to another list.");
            }
        }

        private void VerifyIsTheListEmpty()
        {
            if (Count == 0 || First is null)
                throw new InvalidOperationException("The list is empty.");
        }

        public IEnumerator<T> GetEnumerator()
        {
            MyLinkedListNode<T> temp = First!;
            int rememberedState = State;

            while (temp != null)
            {
                yield return temp.Value;

                if (rememberedState != State)
                {
                    throw new InvalidOperationException
                        ("Collection was modified after the enumerator was instantiated.");
                }

                temp = temp.Next!;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}