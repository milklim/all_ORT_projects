using System;
using System.Collections;
using System.Linq;


namespace Lists
{
    public class AList0 : IList
    {
        int[] array;

        public AList0() => this.Init(new int[0]);

        public void Init(int[] ini)
        {
            if (ini == null)
            {
                this.array = new int[0];
                return;
            }
            this.array = ini;
        }

        public void AddEnd(int val)
        {
            int newLen = Size() + 1;
            int[] tmpArr = new int[newLen];

            for (int i = 0; i < array.Length; i++)
            {
                tmpArr[i] = array[i];
            }
            tmpArr[newLen - 1] = val;
            array = tmpArr;
        }

        public void AddPos(int pos, int val)
        {
            if (pos < 0 || pos > array.Length)
            {
                throw new IndexOutOfRangeException();
            }

            int[] tmpArr = new int[Size() + 1];
            for (int i = 0, k = 0; k < tmpArr.Length; i++, k++)
            {
                if (k != pos)
                {
                    tmpArr[k] = array[i];
                }
                else
                {
                    i--;
                    tmpArr[k] = val;
                    
                }
            }

            this.array = tmpArr;
        }

        public void AddStart(int val)
        {
            int[] tmpArr = new int[Size() + 1];
            tmpArr[0] = val;

            for (int i = 0; i < array.Length; i++)
            {
                tmpArr[i + 1] = array[i];
            }

            this.array = tmpArr;
        }

        public void Clear()
        {
            this.array = new int[0] ;
        }

        public int? DelEnd()
        {
            if (array.Length == 0)
            {
                return null;
            }
            int[] tmpArr = new int[Size() - 1];
            int delElem = array[Size() - 1];

            for (int i = 0; i < tmpArr.Length; i++)
            {
                tmpArr[i] = array[i];
            }
            this.array = tmpArr;

            return delElem;
        }

        public int DelPos(int pos)
        {
            if (pos < 0 || pos >= array.Length)
                throw new IndexOutOfRangeException();

            int[] tmpArr = new int[Size() - 1];
            int delElem = array[pos];

            for (int i = 0, k = 0; i < tmpArr.Length; i++, k++)
            {
                tmpArr[i] = (i != pos) ? array[k] : array[++k];
            }
            this.array = tmpArr;

            return delElem;
        }

        public int? DelStart()
        {
            if (array.Length == 0)
            {
                return null;
            }
            int[] tmpArr = new int[Size() - 1];
            int delElem = array[0];

            for (int i = 0; i < tmpArr.Length; i++)
            {
                tmpArr[i] = array[i + 1];
            }
            this.array = tmpArr;

            return delElem;
        }

        public int Get(int pos)
        {
            if (pos < 0 || pos >= array.Length)
                throw new IndexOutOfRangeException();
            return this.array[pos];
        }


        public void HalfReverse()
        {
            int mid = (array.Length % 2 == 0) ? array.Length / 2 : array.Length / 2 + 1;
            for (int i = 0; i < array.Length / 2; i++)
            {
                int temp = array[i];
                array[i] = array[i + mid];
                array[i + mid] = temp;
            }
        }


        public int? Max()
        {
            if (array.Length == 0)
            {
                return null;
            }
            return this.array.Max();
        }

        public int? MaxPos()
        {
            int? max = this.Max();
            if (max == null)
            {
                return null;
            }
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == max)
                {
                    return i;
                }
            }
            return null;
        }

        public int? Min()
        {
            if (array.Length == 0)
            {
                return null;
            }
            return this.array.Min();
        }

        public int? MinPos()
        {
            int? min = Min();
            if (min == null)
            {
                return null;
            }
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == min)
                {
                    return i;
                }
            }
            return null;
        }

        public void Reverse()
        {
            int[] tmpArr = new int[Size()];
            for (int i = 0, k = array.Length - 1; i < array.Length; i++, k--)
            {
                tmpArr[i] = array[k];
            }
            this.array = tmpArr;
        }

        public void Set(int pos, int val)
        {
            if (array.Length == 0)
            {
                throw new IndexOutOfRangeException();
            }
            this.array[pos] = val;
        }

        public int Size()
        {
            return this.array.Length;
        }

        public void Sort()
        {
            var sorted = from elem in array
                         orderby elem
                         select elem;
            int i = 0;
            foreach (var item in sorted)
            {
                array[i] = item;
                i++;
            }
        }

        public int[] ToArray()
        {
            return array;
        }

        public override string ToString()
        {
            string res = string.Empty;
            for (int i = 0; i < array.Length; i++)
            {
                res += $"{array[i]}, ";
            }
            return res.TrimEnd(' ', ',');
        }

        public IEnumerator GetEnumerator()
        {
            return array.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
