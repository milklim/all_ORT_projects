using System;
using System.Collections;

namespace Lists
{
    public class DLList : IList
    {
        Node head;

        public void AddEnd(int val)
        {
            Node node = new Node(val) { Next = null };

            if (head == null)
            {
                node.Prev = null;
                head = node;
                return;
            }

            Node last = head;
            while (last.Next != null)
                last = last.Next;

            last.Next = node;
            node.Prev = last;
        }

        public void AddPos(int pos, int val)
        {
            int count = Size();

            if (pos < 0 || pos > count)
                throw new IndexOutOfRangeException();

            if (head == null || pos == 0)
            {
                AddStart(val);
            }
            else
            {
                Node node = new Node(val);
                Node curr = head;
                for (int i = 1; i <= pos; i++)
                {
                    if (i != pos)
                    {
                        curr = curr.Next;
                    }
                    else
                    {
                        Node nxtNode = curr.Next;
                        if (nxtNode != null)
                        {
                            nxtNode.Prev = node;
                        }
                        curr.Next = node;
                        node.Next = nxtNode;
                        node.Prev = curr;
                    }
                }
            }


        }

        public void AddStart(int val)
        {
            Node node = new Node(val);

            node.Next = head;
            node.Prev = null;

            if (head != null)
                head.Prev = node;

            head = node;
        }

        public void Clear()
        {
            head = null;
        }

        public int? DelEnd()
        {
            if (head == null)
                return null;
            if (head.Next == null)
            {
                int ret = head.Data;
                Clear();
                return ret;
            }

            Node last = head;
            while (last.Next != null)
            {
                last = last.Next;
            }

            Node prev = last.Prev;
            prev.Next = null;

            return last.Data;
        }

        public int DelPos(int pos)
        {
            int count = Size();

            if (pos < 0 || pos >= count)
                throw new IndexOutOfRangeException();

            if (pos == 0)
                return (int)DelStart();
            if (pos == count - 1)
                return (int)DelEnd();

            Node curr = head;
            int res = head.Data;
            for (int i = 1; curr != null && i < pos; i++)
            {
                curr = curr.Next;
            }
            res = curr.Next.Data;

            Node next = curr.Next.Next;
            next.Prev = curr;
            curr.Next = next;

            return res;
        }

        public int? DelStart()
        {
            if (head == null)
                return null;

            int res = head.Data;
            if (head.Next == null)
            {
                res = head.Data;
                head = null;
                return res;
            }

            head = head.Next;
            head.Prev = null;

            return res;
        }

        public int Get(int pos)
        {
            int count = Size();

            if (pos < 0 || pos >= count)
                throw new IndexOutOfRangeException();

            Node curr = head;
            int ret = head.Data;
            for (int i = 1; i < count; i++)
            {
                if (i != pos)
                {
                    curr = curr.Next;
                }
                else
                {
                    ret = curr.Next.Data;
                    break;
                }
            }

            return ret;
        }

        public IEnumerator GetEnumerator()
        {
            return ToArray().GetEnumerator();
        }

        public void HalfReverse()
        {
            int count = Size();

            if (count == 1 || count == 0)
                return;

            Node last = GetNode(count - 1);
            Node mid = GetNode(count / 2 - 1);
            if (count % 2 != 0)
            {
                mid.Next.Prev = last;
                last.Next = mid.Next;
                mid.Next.Next.Prev = mid;
                mid.Next = mid.Next.Next;
                last = last.Next;
            }
            head.Prev = last;
            last.Next = head;
            head = mid.Next;
            last = mid;
            last.Next.Prev = null;
            last.Next = null;
        }

        public void Init(int[] ini)
        {
            if (ini == null || ini.Length == 0)
            {
                Clear();
                return;
            }

            head = new Node(ini[0]) { Prev = null };
            Node curr = head;
            for (int i = 1; i < ini.Length; i++)
            {
                curr.Next = new Node(ini[i]);
                curr.Next.Prev = curr;
                curr = curr.Next;
            }
        }

        public int? Max()
        {
            if (Size() == 0)
                return null;

            Node curr = head;
            int max = head.Data;
            while (curr.Next != null)
            {
                if (curr.Next.Data > max)
                    max = curr.Next.Data;

                curr = curr.Next;
            }

            return max;
        }

        public int? MaxPos()
        {
            int count = Size();

            if (count == 0)
                return null;

            int? max = Max();

            Node curr = head;
            int? pos = null;
            for (int i = 0; i < count; i++)
            {
                if (curr.Data == max)
                {
                    pos = i;
                    break;
                }
                curr = curr.Next;
            }

            return pos;
        }

        public int? Min()
        {
            if (Size() == 0)
                return null;

            Node curr = head;
            int min = head.Data;
            while (curr.Next != null)
            {
                if (curr.Next.Data < min)
                    min = curr.Next.Data;

                curr = curr.Next;
            }

            return min;
        }

        public int? MinPos()
        {
            int count = Size();

            if (count == 0)
                return null;

            int? min = Min();

            Node curr = head;
            int? pos = null;
            for (int i = 0; i < count; i++)
            {
                if (curr.Data == min)
                {
                    pos = i;
                    break;
                }
                curr = curr.Next;
            }

            return pos;
        }

        public void Reverse()
        {
            Node temp = null;
            Node current = head;

            while (current != null)
            {
                temp = current.Prev;
                current.Prev = current.Next;
                current.Next = temp;
                current = current.Prev;
            }

            if (temp != null)
            {
                head = temp.Prev;
            }
        }

        public void Set(int pos, int val)
        {
            if (pos < 0 || pos >= Size())
                throw new IndexOutOfRangeException();

            Node curr = head;
            for (int i = 0; i < pos; i++)
            {
                curr = curr.Next;
            }
            curr.Data = val;
        }

        public int Size()
        {
            int count = 0;
            if (head == null)
            {
                return count;
            }
            Node curr = head;
            while (curr.Next != null)
            {
                curr = curr.Next;
                count++;
            }

            return ++count;
        }

        public void Sort()
        {
            head = MergeSort(head);
        }

        public int[] ToArray()
        {
            int[] array = new int[Size()];

            Node node = head;
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = node.Data;
                node = node.Next;
            }

            return array;
        }

        public override string ToString()
        {
            if (Size() == 0)
                return string.Empty;

            Node node = head;
            string res = node.Data.ToString();

            while (node.Next != null)
            {
                node = node.Next;
                res += $", {node.Data}";
            }

            return res;
        }

        public class Node
        {
            public Node Next { get; set; }
            public Node Prev { get; set; }
            public int Data { get; set; }

            public Node(int data)
            {
                Data = data;
            }
        }




        private Node GetNode(int pos)
        {
            Node p = head;
            for (int i = 0; i < pos; i++)
            {
                p = p.Next;
            }
            return p;
        }

        private Node Split(Node head)
        {
            Node fast = head, slow = head;
            while (fast.Next != null && fast.Next.Next != null)
            {
                fast = fast.Next.Next;
                slow = slow.Next;
            }
            Node temp = slow.Next;
            slow.Next = null;
            return temp;
        }

        private Node MergeSort(Node node)
        {
            if (node == null || node.Next == null)
            {
                return node;
            }
            Node second = Split(node);

            // Recur for left and right halves
            node = MergeSort(node);
            second = MergeSort(second);

            // Merge the two sorted halves
            return Merge(node, second);
        }

        private Node Merge(Node first, Node second)
        {
            // If first linked list is empty
            if (first == null)
            {
                return second;
            }

            // If second linked list is empty
            if (second == null)
            {
                return first;
            }

            // Pick the smaller value
            if (first.Data < second.Data)
            {
                first.Next = Merge(first.Next, second);
                first.Next.Prev = first;
                first.Prev = null;
                return first;
            }
            else
            {
                second.Next = Merge(first, second.Next);
                second.Next.Prev = second;
                second.Prev = null;
                return second;
            }
        }


    }
}
