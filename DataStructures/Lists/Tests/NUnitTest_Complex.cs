using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Lists;

namespace Tests
{
    [TestFixture(typeof(AList0))]
    [TestFixture(typeof(SLList))]
    [TestFixture(typeof(SLList2))]
    [TestFixture(typeof(DLList))]
    public class ComplexTestsNUnit<T> where T : IList, new()
    {
        IList lst = new T();

        [SetUp]
        public void SetUp()
        {
            lst.Clear();
        }

        [Test]
        [TestCase(new int[] { 1 }, new int[] { 1, 1, 0, 0 })]
        [TestCase(new int[] { 1, 2 }, new int[] { 1, 2, 0, 1 })]
        [TestCase(new int[] { 1, 2, 3 }, new int[] { 1, 3, 0, 2 })]
        [TestCase(new int[] { 1, -2, 3, 0, 5 }, new int[] { -2, 5, 1, 4 })]
        [TestCase(new int[] { 5, -5, 17, 21, 86, -153, 390 }, new int[] { -153, 390, 5, 6 })]
        public void TestMinMaxMinPosMaxPos(int[] ini, int[] expArr)
        {
            lst.Init(ini);

            int[] outL = new int[4];
            outL[0] = (int)lst.Min();
            outL[1] = (int)lst.Max();
            outL[2] = (int)lst.MinPos();
            outL[3] = (int)lst.MaxPos();

            CollectionAssert.AreEqual(expArr, outL);
        }


        [Test]
        [TestCase(new int[] { 1 }, 0, 0, new int[] { 1 })]
        [TestCase(new int[] { 1, 2 }, 0, 1, new int[] { 1, 1 })]
        [TestCase(new int[] { 1, 2, 3 }, 2, 0, new int[] { 3, 2, 3 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5 }, 1, 4, new int[] { 1, 2, 3, 4, 2 })]
        [TestCase(new int[] { 5, -5, 17, 21, 86, -153, 390 }, 1, 6, new int[] { 5, -5, 17, 21, 86, -153, -5 })]
        public void TestGetSet(int[] ini, int posGet, int posSet, int[] exp)
        {
            lst.Init(ini);
            lst.Set(posSet, lst.Get(posGet));

            CollectionAssert.AreEqual(exp, lst.ToArray());
        }

        //c) AddPos, Sort

        [Test]
        [TestCase(new int[] { 1 }, 0, 5, new int[] { 1, 5 })]
        [TestCase(new int[] { 1, 2 }, 1, -3, new int[] { -3, 1, 2 })]
        [TestCase(new int[] { 1, 2, 3 }, 2, 0, new int[] { 0, 1, 2, 3 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5 }, 1, 4, new int[] { 1, 2, 3, 4, 4, 5 })]
        [TestCase(new int[] { 5, -5, 17, 21, 86, -153, 390 }, 6, -3, new int[] { -153, -5, -3, 5, 17, 21, 86, 390 })]
        public void TestAddPosSort(int[] ini, int pos, int val, int[] exp)
        {
            lst.Init(ini);

            lst.AddPos(pos, val);
            lst.Sort();

            CollectionAssert.AreEqual(exp, lst.ToArray());
        }

        //d) AddBegin, Reverse

        [Test]
        [TestCase(new int[] { 1 }, 5, new int[] { 1, 5 })]
        [TestCase(new int[] { 1, 2 }, -3, new int[] { 2, 1, -3 })]
        [TestCase(new int[] { 1, 2, 3 }, 0, new int[] { 3, 2, 1, 0 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5 }, 4, new int[] { 5, 4, 3, 2, 1, 4 })]
        [TestCase(new int[] { 5, -5, 17, 21, 86, -153, 390 }, -3, new int[] { 390, -153, 86, 21, 17, -5, 5, -3 })]
        public void TestAddStartReverse(int[] ini, int val, int[] exp)
        {
            lst.Init(ini);

            lst.AddStart(val);
            lst.Reverse();

            CollectionAssert.AreEqual(exp, lst.ToArray());
        }

        //e) AddEnd, HalfReverse

        [Test]
        [TestCase(new int[] { 1 }, 5, new int[] { 5, 1 })]
        [TestCase(new int[] { 1, 2 }, -3, new int[] { -3, 2, 1 })]
        [TestCase(new int[] { 1, 2, 3 }, 0, new int[] { 3, 0, 1, 2 })]
        [TestCase(new int[] { 1, -2, 3, 0 }, 1, new int[] { 0, 1, 3, 1, -2 })]
        [TestCase(new int[] { 5, -5, 17, 21, 86, -153, 390 }, -3, new int[] { 86, -153, 390, -3, 5, -5, 17, 21 })]
        public void TestAddEndHalfReverse(int[] ini, int val, int[] exp)
        {
            lst.Init(ini);

            lst.AddEnd(val);
            lst.HalfReverse();
            CollectionAssert.AreEqual(exp, lst.ToArray());
        }

        //f) DelPos, Sort

        [Test]
        [TestCase(new int[] { 1, 5 }, 0, new int[] { 5 })]
        [TestCase(new int[] { 1, 2, 3 }, 2, new int[] { 1, 2 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5 }, 4, new int[] { 1, 2, 3, 4 })]
        [TestCase(new int[] { 5, -5, 17, 21, 86, -153, 390 }, 6, new int[] { -153, -5, 5, 17, 21, 86 })]
        public void TestDelPosSort(int[] ini, int pos, int[] exp)
        {
            lst.Init(ini);

            lst.DelPos(pos);
            lst.Sort();

            CollectionAssert.AreEqual(exp, lst.ToArray());
        }

        //g) DelBegin, Reverse

        [Test]
        [TestCase(new int[] { 1, 5 }, new int[] { 5 })]
        [TestCase(new int[] { 1, 2, 3 }, new int[] { 3, 2 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5 }, new int[] { 5, 4, 3, 2 })]
        [TestCase(new int[] { 5, -5, 17, 21, 86, -153, 390 }, new int[] { 390, -153, 86, 21, 17, -5 })]
        public void TestDelStratReverse(int[] ini, int[] exp)
        {
            lst.Init(ini);

            lst.DelStart();
            lst.Reverse();

            CollectionAssert.AreEqual(exp, lst.ToArray());
        }

        //h) DelEnd, HalfReverse

        [Test]
        [TestCase(new int[] { 1, 5 }, new int[] { 1 })]
        [TestCase(new int[] { 1, 2, 3 }, new int[] { 2, 1 })]
        [TestCase(new int[] { 1, 2, 3, 4 }, new int[] { 3, 2, 1 })]
        [TestCase(new int[] { 5, -5, 17, 21, 86, -153 }, new int[] { 21, 86, 17, 5, -5 })]
        public void TestDelEndHalfReverse(int[] ini, int[] exp)
        {
            lst.Init(ini);

            lst.DelEnd();
            lst.HalfReverse();

            CollectionAssert.AreEqual(exp, lst.ToArray());
        }
    }
}
