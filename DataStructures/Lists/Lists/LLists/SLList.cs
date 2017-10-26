using System;
using System.Collections;

namespace Lists

{
    // Singly Linked List
    public class SLList : IList
    {
        Node head;
        Node tail;
        int count;


        public void AddEnd(int val)
        {
            Node node = new Node(val);

            if (head == null)
                head = node;
            else
                tail.Next = node;

            tail = node;
            count++;
        }

        public void AddPos(int pos, int val)
        {
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
                for (int i = 1; i <= count; i++)
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
                count++;
            }
        }

        public void AddStart(int val)
        {
            Node node = new Node(val);

            Node exHead = head;

            head = node;
            node.Next = exHead;
            if (tail == null)
            {
                tail = node;
            }

            count++;
        }

        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }

        public int? DelEnd()
        {
            if (tail == null)
                return null;
            if (tail == head)
            {
                int ret = tail.Data;
                Clear();
                return ret;
            }
            Node exTail = tail;

            Node curr = head;
            Node prev = null;

            while (curr.Next != null)
            {
                prev = curr;
                curr = curr.Next;
            }
            tail = prev;
            tail.Next = null;
            count--;

            return exTail.Data;
        }

        public int DelPos(int pos)
        {
            if (pos < 0 || pos >= count)
                throw new IndexOutOfRangeException();

            if (pos == 0)
            {
                return (int)DelStart();
            }
            else
            {
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
                        Node delPosNode = curr.Next;
                        ret = delPosNode.Data;
                        curr.Next = delPosNode.Next;
                        count--;
                    }
                }

                return ret;
            }
        }

        public int? DelStart()
        {
            if (head == null)
                return null;

            Node exHead = head;
            head = exHead.Next;
            count--;

            return exHead.Data;
        }

        public int Get(int pos)
        {
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
            if (count == 1 || count == 0)
                return;

            Node curr = GetNode(Size() - 1);
            Node mid = GetNode(Size() / 2 - 1);
            if (Size() % 2 != 0)
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
            tail = curr;
            count = ini.Length;
        }

        public int? Max()
        {
            if (count == 0)
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
            if (count == 0)
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
            RecursiveReverse(ref head);
        }

        public void Set(int pos, int val)
        {
            if (pos < 0 || pos >= count)
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
            return count;
        }

        public void Sort()
        {
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
            int[] array = new int[count];

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
            if (count == 0)
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

        private static void RecursiveReverse(ref Node list)
        {
            Node first;
            Node rest;
            if (list == null) return;
            first = list;
            rest = first.Next;
            if (rest == null)
            {
                return;
            }
            RecursiveReverse(ref rest);
            first.Next.Next = first;
            first.Next = null;
            list = rest;
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
            public Node(int data)
            {
                Data = data;
            }
            public int Data { get; set; }
            public Node Next { get; set; }
        }

    }

}
