namespace LinkedList.Tests
{
    public class MyLinkedListTests
    {
        [Test]
        public void AddLast_AddingItemToAnEmptyList_ItemBecomesFirst()
        {
            MyLinkedList<int> list = new();
            int item = 5;

            list.AddLast(item);

            Assert.Multiple(() =>
            {
                Assert.That(list.First!.Value, Is.EqualTo(item));
                Assert.That(list, Has.Count.EqualTo(1));
            });
        }

        [Test]
        public void AddLast_AddingItem_ItemBecomesLast()
        {
            MyLinkedList<int> list = new(new[] { 1, 9, 3 });
            int item = 5;

            list.AddLast(item);

            Assert.Multiple(() =>
            {
                Assert.That(list.Last!.Value, Is.EqualTo(item));
                Assert.That(list, Has.Count.EqualTo(4));
            });
        }

        [Test]
        public void AddLast_AddingNodeToAnEmptyList_NodeBecomesFirst()
        {
            MyLinkedList<int> list = new();
            MyLinkedListNode<int> node = new(5);

            list.AddLast(node);

            Assert.Multiple(() =>
            {
                Assert.That(list.First, Is.EqualTo(node));
                Assert.That(list, Has.Count.EqualTo(1));
            });
        }

        [Test]
        public void AddLast_AddingNode_NodeBecomesLast()
        {
            MyLinkedList<int> list = new(new[] { 1, 9, 3 });
            MyLinkedListNode<int> node = new(5);

            list.AddLast(node);

            Assert.Multiple(() =>
            {
                Assert.That(list.Last, Is.EqualTo(node));
                Assert.That(list, Has.Count.EqualTo(4));
            });
        }

        [Test]
        public void AddLast_AddingForeignNode_Throws()
        {
            MyLinkedList<int> list = new();
            MyLinkedList<int> foreignList = new();
            MyLinkedListNode<int> node = new(5);
            foreignList.AddLast(node);

            Assert.Catch<InvalidOperationException>(() => list.AddLast(node));
        }

        [Test]
        public void AddFirst_AddingItem_ItemBecomesFirst()
        {
            MyLinkedList<int> list = new(new[] { 1, 9, 3 });
            int item = 5;

            list.AddFirst(item);

            Assert.Multiple(() =>
            {
                Assert.That(list.First!.Value, Is.EqualTo(item));
                Assert.That(list, Has.Count.EqualTo(4));
            });
        }

        [Test]
        public void AddFirst_AddingNode_NodeBecomesFirst()
        {
            MyLinkedList<int> list = new();
            MyLinkedListNode<int> node = new(5);

            list.AddFirst(node);

            Assert.Multiple(() =>
            {
                Assert.That(list.First, Is.EqualTo(node));
                Assert.That(list, Has.Count.EqualTo(1));
            });
        }

        [Test]
        public void AddFirst_AddingForeignNode_Throws()
        {
            MyLinkedList<int> list = new();

            MyLinkedList<int> foreignList = new();
            MyLinkedListNode<int> node = new(5);
            foreignList.AddLast(node);

            Assert.Catch<InvalidOperationException>(() => list.AddFirst(node));
        }

        [Test]
        public void AddAfter_AddingItemAfterNode()
        {
            MyLinkedList<int> list = new();
            MyLinkedListNode<int> node = list.AddLast(10);
            MyLinkedListNode<int> node2 = list.AddLast(5);

            MyLinkedListNode<int> testNode = list.AddAfter(node, 20);

            Assert.Multiple(() =>
            {
                Assert.That(testNode.Previous, Is.EqualTo(node));
                Assert.That(testNode.Next, Is.EqualTo(node2));
                Assert.That(list, Has.Count.EqualTo(3));
            });
        }

        [Test]
        public void AddAfter_AddingItemAfterLast_ItemBecomesLast()
        {
            MyLinkedList<int> list = new();
            MyLinkedListNode<int> node = list.AddLast(10);

            MyLinkedListNode<int> testNode = list.AddAfter(node, 20);

            Assert.Multiple(() =>
            {
                Assert.That(testNode.Previous, Is.EqualTo(node));
                Assert.That(list.Last, Is.EqualTo(testNode));
                Assert.That(list, Has.Count.EqualTo(2));
            });
        }

        [Test]
        public void AddAfter_AddingItemAfterAnUnattachedNode_Throws()
        {
            MyLinkedList<int> list = new();
            MyLinkedListNode<int> node = new(10);

            Assert.Catch<InvalidOperationException>(() => list.AddAfter(node, 20));
        }

        [Test]
        public void AddAfter_AddingNodeAfterNode()
        {
            MyLinkedList<int> list = new();
            MyLinkedListNode<int> node = list.AddLast(10);
            MyLinkedListNode<int> node2 = list.AddLast(5);
            MyLinkedListNode<int> nodeToInsert = new(15);

            list.AddAfter(node, nodeToInsert);

            Assert.Multiple(() =>
            {
                Assert.That(nodeToInsert.Previous, Is.EqualTo(node));
                Assert.That(nodeToInsert.Next, Is.EqualTo(node2));
                Assert.That(list, Has.Count.EqualTo(3));
            });
        }

        [Test]
        public void AddAfter_AddingNodeAfterLast_NewNodeBecomesLast()
        {
            MyLinkedList<int> list = new();
            MyLinkedListNode<int> node = list.AddLast(10);
            MyLinkedListNode<int> nodeToInsert = new(15);

            list.AddAfter(node, nodeToInsert);

            Assert.Multiple(() =>
            {
                Assert.That(nodeToInsert.Previous, Is.EqualTo(node));
                Assert.That(list.Last, Is.EqualTo(nodeToInsert));
                Assert.That(list, Has.Count.EqualTo(2));
            });
        }

        [Test]
        public void AddAfter_AddingForeignNode_Throws()
        {
            MyLinkedList<int> list = new();
            MyLinkedListNode<int> node = list.AddLast(20);
            MyLinkedList<int> foreignList = new();
            MyLinkedListNode<int> foreignNode = new(5);
            foreignList.AddLast(foreignNode);

            Assert.Catch<InvalidOperationException>(() => list.AddAfter(node, foreignNode));
        }

        [Test]
        public void AddAfter_AddingNodeAfterAnUnattachedNode_Throws()
        {
            MyLinkedList<int> list = new();
            MyLinkedListNode<int> node = new(10);

            Assert.Catch<InvalidOperationException>(() => list.AddAfter(node, new MyLinkedListNode<int>(13)));
        }

        [Test]
        public void AddBefore_AddingItemBeforeNode()
        {
            MyLinkedList<int> list = new();
            MyLinkedListNode<int> node = list.AddLast(10);
            MyLinkedListNode<int> node2 = list.AddLast(5);

            MyLinkedListNode<int> testNode = list.AddBefore(node2, 20);

            Assert.Multiple(() =>
            {
                Assert.That(testNode.Previous, Is.EqualTo(node));
                Assert.That(testNode.Next, Is.EqualTo(node2));
                Assert.That(list, Has.Count.EqualTo(3));
            });
        }

        [Test]
        public void AddBefore_AddingItemBeforeFirst_ItemBecomesFirst()
        {
            MyLinkedList<int> list = new();
            MyLinkedListNode<int> node = list.AddFirst(10);

            MyLinkedListNode<int> testNode = list.AddBefore(node, 20);

            Assert.Multiple(() =>
            {
                Assert.That(testNode.Next, Is.EqualTo(node));
                Assert.That(list.First, Is.EqualTo(testNode));
                Assert.That(list, Has.Count.EqualTo(2));
            });
        }

        [Test]
        public void AddBefore_AddingItemBeforeAnUnattachedNode_Throws()
        {
            MyLinkedList<int> list = new();
            MyLinkedListNode<int> node = new(10);

            Assert.Catch<InvalidOperationException>(() => list.AddBefore(node, 20));
        }

        [Test]
        public void AddBefore_AddingNodeBeforeNode()
        {
            MyLinkedList<int> list = new();
            MyLinkedListNode<int> node = list.AddLast(10);
            MyLinkedListNode<int> node2 = list.AddLast(5);
            MyLinkedListNode<int> nodeToInsert = new(15);

            list.AddBefore(node2, nodeToInsert);

            Assert.Multiple(() =>
            {
                Assert.That(nodeToInsert.Previous, Is.EqualTo(node));
                Assert.That(nodeToInsert.Next, Is.EqualTo(node2));
                Assert.That(list, Has.Count.EqualTo(3));
            });
        }

        [Test]
        public void AddBefore_AddingNodeBeforeFirst_NewNodeBecomesFirst()
        {
            MyLinkedList<int> list = new();
            MyLinkedListNode<int> node = list.AddLast(10);
            MyLinkedListNode<int> nodeToInsert = new(15);

            list.AddBefore(node, nodeToInsert);

            Assert.Multiple(() =>
            {
                Assert.That(nodeToInsert.Next, Is.EqualTo(node));
                Assert.That(list.First, Is.EqualTo(nodeToInsert));
                Assert.That(list, Has.Count.EqualTo(2));
            });
        }

        [Test]
        public void AddBefore_AddingForeignNode_Throws()
        {
            MyLinkedList<int> list = new();
            MyLinkedListNode<int> node = list.AddLast(20);

            MyLinkedList<int> foreignList = new();
            MyLinkedListNode<int> foreignNode = new(5);
            foreignList.AddLast(foreignNode);

            Assert.Catch<InvalidOperationException>(() => list.AddBefore(node, foreignNode));
        }

        [Test]
        public void AddBefore_AddingNodeBeforeAnUnattachedNode_Throws()
        {
            MyLinkedList<int> list = new();
            MyLinkedListNode<int> node = new(10);

            Assert.Catch<InvalidOperationException>(() => list.AddBefore(node, new MyLinkedListNode<int>(13)));
        }

        [Test]
        public void Find_FindingAndFailingToFind()
        {
            MyLinkedList<int> list = new(new[] { 1, 9, 3 });

            MyLinkedListNode<int>? foundNode = list.Find(9);
            MyLinkedListNode<int>? foundNode2 = list.Find(8);

            Assert.Multiple(() =>
            {
                Assert.That(foundNode!.Value, Is.EqualTo(9));
                Assert.That(foundNode2, Is.Null);
            });
        }

        [Test]
        public void Remove_RemovingExistingItem_ReturnsTrue()
        {
            MyLinkedList<int> list = new(new[] { 1, 9, 3 });

            bool result = list.Remove(9);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True);
                Assert.That(list, Has.Count.EqualTo(2));
            });
        }

        [Test]
        public void Remove_RemovingNonexistentItem_ReturnsFalse()
        {
            MyLinkedList<int> list = new(new[] { 1, 9, 3 });

            bool result = list.Remove(11);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.False);
                Assert.That(list, Has.Count.EqualTo(3));
            });
        }

        [Test]
        public void Remove_RemovingMiddleNode()
        {
            MyLinkedList<int> list = new();
            MyLinkedListNode<int> n1 = list.AddLast(5);
            MyLinkedListNode<int> n2 = list.AddLast(7);
            MyLinkedListNode<int> n3 = list.AddLast(9);

            list.Remove(n2);

            Assert.Multiple(() =>
            {
                Assert.That(n1.Next, Is.EqualTo(n3));
                Assert.That(n3.Previous, Is.EqualTo(n1));
                Assert.That(list, Has.Count.EqualTo(2));
            });
        }

        [Test]
        public void Remove_RemovingFirstNode_FirstNodeChanges()
        {
            MyLinkedList<int> list = new();
            MyLinkedListNode<int> n1 = list.AddLast(5);
            MyLinkedListNode<int> n2 = list.AddLast(7);

            list.Remove(n1);

            Assert.Multiple(() =>
            {
                Assert.That(list.First, Is.EqualTo(n2));
                Assert.That(list, Has.Count.EqualTo(1));
            });
        }

        [Test]
        public void Remove_RemovingLastNode_LastNodeChanges()
        {
            MyLinkedList<int> list = new();
            list.AddLast(5);
            MyLinkedListNode<int> n1 = list.AddLast(7);
            MyLinkedListNode<int> n2 = list.AddLast(7);

            list.Remove(n2);

            Assert.Multiple(() =>
            {
                Assert.That(list.Last, Is.EqualTo(n1));
                Assert.That(list, Has.Count.EqualTo(2));
            });
        }

        [Test]
        public void Remove_RemovingUnattachedNode_Throws()
        {
            MyLinkedList<int> list = new();
            MyLinkedListNode<int> n = new(5);

            Assert.Catch<InvalidOperationException>(() => list.Remove(n));
        }

        [Test]
        public void RemoveFirst_FromEmptyList_Throws()
        {
            MyLinkedList<int> list = new();

            Assert.Catch<InvalidOperationException>(() => list.RemoveFirst());
        }

        [Test]
        public void RemoveLast_FromEmptyList_Throws()
        {
            MyLinkedList<int> list = new();

            Assert.Catch<InvalidOperationException>(() => list.RemoveLast());
        }

        [Test]
        public void RemoveLast_LastNodeChanges()
        {
            MyLinkedList<int> list = new();
            list.AddLast(5);
            MyLinkedListNode<int> n1 = list.AddLast(7);
            list.AddLast(7);

            list.RemoveLast();

            Assert.That(list.Last, Is.EqualTo(n1));
        }

        [Test]
        public void RemoveFirst_FirstNodeChanges()
        {
            MyLinkedList<int> list = new();
            list.AddLast(5);
            MyLinkedListNode<int> n2 = list.AddLast(7);

            list.RemoveFirst();

            Assert.That(list.First, Is.EqualTo(n2));
        }

        [Test]
        public void FindLast_FindingOrFailingToFind()
        {
            MyLinkedList<int> list = new(new[] { 1, 9, 0, 3, 9, 1 });

            MyLinkedListNode<int>? foundNode = list.FindLast(9);
            MyLinkedListNode<int>? foundNode2 = list.Find(8);

            Assert.Multiple(() =>
            {
                Assert.That(foundNode!.Value, Is.EqualTo(9));
                Assert.That(foundNode2, Is.Null);
            });
        }

        [Test]
        public void Contains_ItemFound()
        {
            MyLinkedList<int> list = new(new[] { 3, 4, 9 });

            bool result = list.Contains(4);

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void Contains_ItemNotFound()
        {
            MyLinkedList<int> list = new(new[] { 3, 4, 9 });

            bool result = list.Contains(5);

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void Clear_NoNodesZeroCount()
        {
            MyLinkedList<int> list = new(new[] { 3, 4, 9 });

            list.Clear();

            Assert.Multiple(() =>
            {
                Assert.That(list.First, Is.Null);
                Assert.That(list.Last, Is.Null);
                Assert.That(list, Has.Count.EqualTo(0));
            });
        }

        [Test]
        public void ClearThenAddLast()
        {
            MyLinkedList<int> list = new(new[] { 3, 4, 9 });
            MyLinkedListNode<int> n = new(22);

            list.Clear();
            list.AddLast(n);

            Assert.Multiple(() =>
            {
                Assert.That(list.First, Is.EqualTo(n));
                Assert.That(list, Has.Count.EqualTo(1));
            });
        }

        [Test]
        public void CopyTo_NonexistentIndex_Throws()
        {
            MyLinkedList<int> list = new(new[] { 3, 4, 9 });
            int[] arr = new int[3];

            Assert.Catch<ArgumentOutOfRangeException>(() => list.CopyTo(arr, 3));
        }

        [Test]
        public void CopyTo_ItemsCannotFit_Throws()
        {
            MyLinkedList<int> list = new(new[] { 3, 4, 9 });
            int[] arr = new int[3];

            Assert.Catch<ArgumentException>(() => list.CopyTo(arr, 1));
        }

        [Test]
        public void CopyTo_FillingArray()
        {
            MyLinkedList<int> list = new(new[] { 3, 4, 9 });
            int[] arr = new int[3];

            list.CopyTo(arr, 0);

            int i = 0;
            foreach (var item in list)
            {
                Assert.That(arr[i] == item);
                i++;
            }
        }

        [Test]
        public void Enumerator_ModifyingCollection_Throws()
        {
            MyLinkedList<int> list = new(new[] { 3, 4, 9 });

            Assert.Catch<InvalidOperationException>(() =>
            {
                foreach (var x in list)
                {
                    list.AddLast(x);
                }
            });
        }

        [Test]
        public void Events_Are_Raised()
        {
            MyLinkedList<int> list = new(new[] { 3, 4, 9 });
            bool wasRaised = false;

            list.Notify += (message) =>
            {
                wasRaised = true;
            };

            list.Clear();

            Assert.That(wasRaised);
        }

    }
}