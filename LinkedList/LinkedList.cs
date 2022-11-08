namespace LinkedList
{
    public class MyLinkedList<T>
    {
        public int Count { get; private set; } = 0;
        public MyLinkedListNode<T>? First { get; private set; }
        public MyLinkedListNode<T>? Last { get; private set; }

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

        public MyLinkedListNode<T>? Find(T item)
        {
            MyLinkedListNode<T>? current = First;
            while (current != null && !current.Value!.Equals(item))
            {
                current = current!.Next;
            }
            return current;
        }

        private void InsertNodeToEmptyList(MyLinkedListNode<T> node)
        {
            First = node;
            Last = node;
            Count++;
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
                    ("The node is attached to another list or to nothing.");
            }
        }

        private void VerifyIsTheListEmpty()
        {
            if (Count == 0 || First is null)
                throw new InvalidOperationException("The list is empty.");
        }

        public void PrintList()
        {
            MyLinkedListNode<T> temp = First!;
            if (temp != null)
            {
                Console.Write("The list contains: ");
                while (temp != null)
                {
                    Console.Write(temp.Value + " ");
                    temp = temp.Next!;
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("The list is empty.");
            }
        }
    }
}