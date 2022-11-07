namespace LinkedList
{
    public sealed class MyLinkedListNode<T>
    {
        public T Value { get; set; }
        public MyLinkedListNode<T>? Next { get; internal set; }
        public MyLinkedListNode<T>? Previous { get; internal set; }
        public MyLinkedList<T>? List { get; internal set; }

        public MyLinkedListNode(T value)
        {
            Value = value;
        }

        internal MyLinkedListNode(MyLinkedList<T> list, T value)
        {
            List = list;
            Value = value;
        }

        public override string ToString()
        {
            return Value?.ToString() ?? string.Empty;
        }
    }
}
