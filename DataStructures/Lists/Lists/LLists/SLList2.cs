using System;
using System.Collections;

namespace Lists

{
    // Singly Linked List
    public class SLList2 : IList
    {
        Node head;


        public void AddEnd(int val)
        {
            Node node = new Node(val) { Next = null };

            if (head == null)
            {
                head = node;
                return;
            }

            Node last = head;
            while (last.Next != null)
            {
                last = last.Next;
            }
            last.Next = node;
        }

        public void AddPos(int pos, int val)
        {
            if (pos < 0 || pos > Size())
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
                        curr.Next = node;
                        node.Next = nxtNode;
                    }
                }
            }
        }

        public void AddStart(int val)
        {
            Node node = new Node(val);

            node.Next = head;
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
            Node prev = null;

            while (last.Next != null)
            {
                prev = last;
                last = last.Next;
            }
            prev.Next = null;

            return last.Data;
        }

        public int DelPos(int pos)
        {
            int count = Size();

            if (pos < 0 || pos >= count)
                throw new IndexOutOfRangeException();

            Node curr = head;
            int ret = head.Data;
            if (pos == 0)
            {
                head = curr.Next;
                return ret;
            }
            if (pos == count - 1)
            {
                return (int)DelEnd();
            }

            for (int i = 1; curr != null && i < pos; i++)
            {
                curr = curr.Next;
            }

            Node next = curr.Next.Next;
            ret = curr.Next.Data;
            curr.Next = next;

            return ret;
        }

        public int? DelStart()
        {
            if (head == null)
                return null;

            Node exHead = head;
            head = exHead.Next;

            return exHead.Data;
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void HalfReverse()
        {
            int count = Size();

            if (count == 1 || count == 0)
                return;

            Node curr = GetNode(count - 1);
            Node mid = GetNode(count / 2 - 1);
            if (count % 2 != 0)
            {
                curr.Next = mid.Next;
                mid.Next = mid.Next.Next;
                curr = curr.Next;
            }
            curr.Next = head;
            head = mid.Next;
            mid.Next = null;
        }


        public void Init(int[] ini)
        {
            if (ini == null || ini.Length == 0)
            {
                Clear();
                return;
            }

            head = new Node(ini[0]);
            Node curr = head;
            for (int i = 1; i < ini.Length; i++)
            {
                curr.Next = new Node(ini[i]);
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
            Node prev = null;
            Node current = head;
            Node next = null;
            while (current != null)
            {
                next = current.Next;
                current.Next = prev;
                prev = current;
                current = next;
            }
            head = prev;
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
            int count = Size();

            if (count == 0 || count == 1)
                return;

            Node ptr = head, tmp = null, prev = null;

            bool flag = false;
            do
            {
                flag = false;
                ptr = head;
                while (ptr.Next != null)
                {
                    if (ptr.Data > ptr.Next.Data)
                    {
                        if (ptr == head)
                        {
                            tmp = ptr;
                            ptr = tmp.Next;
                            tmp.Next = ptr.Next;
                            ptr.Next = tmp;
                            head = ptr;
                            flag = true;
                        }
                        else
                        {
                            tmp = ptr;
                            ptr = tmp.Next;
                            tmp.Next = ptr.Next;
                            ptr.Next = tmp;
                            prev.Next = ptr;
                            flag = true;
                        }
                    }
                    prev = ptr;
                    ptr = ptr.Next;

                }
            } while (flag);
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


        private Node GetNode(int pos)
        {
            Node p = head;
            for (int i = 0; i < pos; i++)
            {
                p = p.Next;
            }
            return p;
        }

        class Node
        {
            public int Data { get; set; }
            public Node Next { get; set; }

            public Node(int data)
            {
                Data = data;
            }
        }

    }

}
