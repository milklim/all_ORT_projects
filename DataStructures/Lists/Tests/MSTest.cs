using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lists;

namespace Tests
{
    // [TestClass]
    public class TestAList0 : MSTests
    {
        internal override IList MakeList()
        {
            return new AList0();
        }
    }

    // [TestClass]
    public class TestSLList : MSTests
    {
        internal override IList MakeList()
        {
            return new SLList();
        }
    }

    //[TestClass]
    public class TestSLList2 : MSTests
    {
        internal override IList MakeList()
        {
            return new SLList2();
        }
    }

    //[TestClass]
    public class TestDLList : MSTests
    {
        internal override IList MakeList()
        {
            return new DLList();
        }
    }

    [TestClass]
    public abstract class MSTests
    {
        IList lst;
        internal abstract IList MakeList();
        public MSTests()
        {
            lst = MakeList();
        }

        [TestInitialize]
        public void SetUp()
        {
            lst.Clear();
        }

        [DataTestMethod]
        [DataRow(null, new int[] { })]
        [DataRow(new int[] { }, new int[] { })]
        [DataRow(new int[] { 2 }, new int[] { 2 })]
        [DataRow(new int[] { 5, 6 }, new int[] { 5, 6 })]
        [DataRow(new int[] { 3, 7, 4, 9, 1 }, new int[] { 3, 7, 4, 9, 1 })]
        public void TestToArray(int[] input, int[] res)
        {
            lst.Init(input);
            CollectionAssert.AreEqual(res, lst.ToArray());
        }

        [DataTestMethod]
        [DataRow(null, new int[] { })]
        [DataRow(new int[] { }, new int[] { })]
        [DataRow(new int[] { 2 }, new int[] { 2 })]
        [DataRow(new int[] { 5, 6 }, new int[] { 5, 6 })]
        [DataRow(new int[] { 3, 7, 4, 9, 1 }, new int[] { 3, 7, 4, 9, 1 })]
        public void TestInit(int[] input, int[] res)
        {
            lst.Init(input);
            CollectionAssert.AreEqual(res, lst.ToArray());
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow(new int[] { })]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestGetExEmpty(int[] input)
        {
            lst.Init(input);
            lst.Get(0);
        }

        [DataTestMethod]
        [DataRow(new int[] { 2 }, -1)]
        [DataRow(new int[] { 2 }, 1)]
        [DataRow(new int[] { 5, 6 }, -1)]
        [DataRow(new int[] { 5, 6 }, 2)]
        [DataRow(new int[] { 3, 7, 4, 9, 1 }, -1)]
        [DataRow(new int[] { 3, 7, 4, 9, 1 }, 5)]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestGetExOutRange(int[] input, int pos)
        {
            lst.Init(input);
            lst.Get(pos);
        }

        [DataTestMethod]
        [DataRow(new int[] { 2 }, 0, 2)]
        [DataRow(new int[] { 12, 3 }, 0, 12)]
        [DataRow(new int[] { 12, 3 }, 1, 3)]
        [DataRow(new int[] { 2, 1, 0, -10, 80 }, 0, 2)]
        [DataRow(new int[] { 2, 1, 0, -10, 80 }, 3, -10)]
        [DataRow(new int[] { 2, 1, 0, -10, 80 }, 4, 80)]
        public void TestGet(int[] input, int pos, int val)
        {
            lst.Init(input);
            Assert.AreEqual(val, lst.Get(pos));
        }

        [DataTestMethod]
        [DataRow(null, 0)]
        [DataRow(new int[] { }, 0)]
        [DataRow(new int[] { 2 }, 1)]
        [DataRow(new int[] { 5, 6 }, 2)]
        [DataRow(new int[] { 3, 7, 4, 9, 1 }, 5)]
        public void TestSize(int[] input, int res)
        {
            lst.Init(input);
            Assert.AreEqual(res, lst.Size());
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow(new int[] { })]
        [DataRow(new int[] { 2 })]
        [DataRow(new int[] { 5, 6 })]
        [DataRow(new int[] { 3, 7, 4, 9, 1 })]
        public void TestClear(int[] input)
        {
            lst.Init(input);
            lst.Clear();
            Assert.AreEqual(0, lst.Size());
        }


        [DataTestMethod]
        [DataRow(null, "")]
        [DataRow(new int[] { }, "")]
        [DataRow(new int[] { 2 }, "2")]
        [DataRow(new int[] { 5, 6 }, "5, 6")]
        [DataRow(new int[] { 3, 7, 4, 9, 1 }, "3, 7, 4, 9, 1")]
        public void TestToString(int[] input, string res)
        {
            lst.Init(input);
            Assert.AreEqual(res, lst.ToString());
        }

        [DataTestMethod]
        [DataRow(null, new int[] { 0 }, 1)]
        [DataRow(new int[] { }, new int[] { 0 }, 1)]
        [DataRow(new int[] { 2 }, new int[] { 0, 2 }, 2)]
        [DataRow(new int[] { 5, 6 }, new int[] { 0, 5, 6 }, 3)]
        [DataRow(new int[] { 3, 7, 4, 9, 1 }, new int[] { 0, 3, 7, 4, 9, 1 }, 6)]
        public void TestAddStart(int[] input, int[] res, int size)
        {
            lst.Init(input);
            lst.AddStart(0);
            Assert.AreEqual(size, lst.Size());
            Assert.AreEqual(0, lst.Get(0));
            CollectionAssert.AreEqual(res, lst.ToArray());
        }



        [DataTestMethod]
        [DataRow(null, new int[] { 0 }, 1)]
        [DataRow(new int[] { }, new int[] { 0 }, 1)]
        [DataRow(new int[] { 2 }, new int[] { 2, 0 }, 2)]
        [DataRow(new int[] { 5, 6 }, new int[] { 5, 6, 0 }, 3)]
        [DataRow(new int[] { 3, 7, 4, 9, 1 }, new int[] { 3, 7, 4, 9, 1, 0 }, 6)]
        public void TestAddEnd(int[] input, int[] res, int size)
        {
            lst.Init(input);
            lst.AddEnd(0);
            Assert.AreEqual(size, lst.Size());
            Assert.AreEqual(0, lst.Get(lst.Size() - 1));
            CollectionAssert.AreEqual(res, lst.ToArray());
        }



        [DataTestMethod]
        [DataRow(null, -1)]
        [DataRow(null, 1)]
        [DataRow(new int[] { }, -1)]
        [DataRow(new int[] { }, 1)]
        [DataRow(new int[] { 2 }, -1)]
        [DataRow(new int[] { 2 }, 2)]
        [DataRow(new int[] { 5, 6 }, -1)]
        [DataRow(new int[] { 5, 6 }, 3)]
        [DataRow(new int[] { 3, 7, 4, 9, 1 }, -1)]
        [DataRow(new int[] { 3, 7, 4, 9, 1 }, 6)]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestAddPosExEmpty(int[] input, int pos)
        {
            lst.Init(input);
            lst.AddPos(pos, 5);
        }

        [DataTestMethod]
        [DataRow(null, new int[] { 0 }, 0, 1)]
        [DataRow(new int[] { }, new int[] { 0 }, 0, 1)]
        [DataRow(new int[] { 2 }, new int[] { 0, 2 }, 0, 2)]
        [DataRow(new int[] { 2 }, new int[] { 2, 0 }, 1, 2)]
        [DataRow(new int[] { 5, 6 }, new int[] { 0, 5, 6 }, 0, 3)]
        [DataRow(new int[] { 5, 6 }, new int[] { 5, 6, 0 }, 2, 3)]
        [DataRow(new int[] { 5, 6 }, new int[] { 5, 0, 6 }, 1, 3)]
        [DataRow(new int[] { 3, 7, 4, 9, 1 }, new int[] { 0, 3, 7, 4, 9, 1 }, 0, 6)]
        [DataRow(new int[] { 3, 7, 4, 9, 1 }, new int[] { 3, 7, 4, 9, 1, 0 }, 5, 6)]
        [DataRow(new int[] { 3, 7, 4, 9, 1 }, new int[] { 3, 7, 4, 0, 9, 1 }, 3, 6)]
        public void TestAddPos(int[] input, int[] res, int pos, int size)
        {
            lst.Init(input);
            lst.AddPos(pos, 0);
            Assert.AreEqual(size, lst.Size());
            Assert.AreEqual(0, lst.Get(pos));
            CollectionAssert.AreEqual(res, lst.ToArray());
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow(new int[] { })]
        public void TestDelStartExEmpty(int[] input)
        {
            lst.Init(input);
            Assert.AreEqual(lst.DelStart(), null);
        }

        [DataTestMethod]
        [DataRow(new int[] { 2 }, new int[] { }, 2, 0)]
        [DataRow(new int[] { 5, 6 }, new int[] { 6 }, 5, 1)]
        [DataRow(new int[] { 3, 7, 4, 9, 1 }, new int[] { 7, 4, 9, 1 }, 3, 4)]
        public void TestDelStart(int[] input, int[] res, int val, int size)
        {
            lst.Init(input);
            int? delVal = lst.DelStart();
            Assert.AreEqual(size, lst.Size());
            Assert.AreEqual(val, delVal);
            CollectionAssert.AreEqual(res, lst.ToArray());
        }


        [DataTestMethod]
        [DataRow(null)]
        [DataRow(new int[] { })]
        public void TestDelEndExEmpty(int[] input)
        {
            lst.Init(input);
            Assert.AreEqual(lst.DelEnd(), null);
        }

        [DataTestMethod]
        [DataRow(new int[] { 2 }, new int[] { }, 2, 0)]
        [DataRow(new int[] { 5, 6 }, new int[] { 5 }, 6, 1)]
        [DataRow(new int[] { 3, 7, 4, 9, 1 }, new int[] { 3, 7, 4, 9 }, 1, 4)]
        public void TestDelEnd(int[] input, int[] res, int val, int size)
        {
            lst.Init(input);
            int? delVal = lst.DelEnd();
            Assert.AreEqual(size, lst.Size());
            Assert.AreEqual(val, delVal);
            CollectionAssert.AreEqual(res, lst.ToArray());
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow(new int[] { })]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestDelPosExEmpty(int[] input)
        {
            lst.Init(input);
            lst.DelPos(0);
        }

        [DataTestMethod]
        [DataRow(new int[] { 2 }, -1)]
        [DataRow(new int[] { 2 }, 1)]
        [DataRow(new int[] { 5, 6 }, -1)]
        [DataRow(new int[] { 5, 6 }, 2)]
        [DataRow(new int[] { 3, 7, 4, 9, 1 }, -1)]
        [DataRow(new int[] { 3, 7, 4, 9, 1 }, 5)]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestDelPosExOutRange(int[] input, int pos)
        {
            lst.Init(input);
            lst.DelPos(pos);
        }

        [DataTestMethod]
        [DataRow(new int[] { 2 }, new int[] { }, 0, 2, 0)]
        [DataRow(new int[] { 5, 6 }, new int[] { 6 }, 0, 5, 1)]
        [DataRow(new int[] { 5, 6 }, new int[] { 5 }, 1, 6, 1)]
        [DataRow(new int[] { 3, 7, 4, 9, 1 }, new int[] { 7, 4, 9, 1 }, 0, 3, 4)]
        [DataRow(new int[] { 3, 7, 4, 9, 1 }, new int[] { 3, 7, 4, 1 }, 3, 9, 4)]
        [DataRow(new int[] { 3, 7, 4, 9, 1 }, new int[] { 3, 7, 4, 9 }, 4, 1, 4)]
        public void TestDelPos(int[] input, int[] res, int pos, int val, int size)
        {
            lst.Init(input);
            int? delVal = lst.DelPos(pos);
            Assert.AreEqual(size, lst.Size());
            Assert.AreEqual(val, delVal);
            CollectionAssert.AreEqual(res, lst.ToArray());
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow(new int[] { })]
        public void TestMinEmptyArr(int[] input)
        {
            lst.Init(input);
            Assert.AreEqual(lst.Min(), null);
        }

        [DataTestMethod]
        [DataRow(new int[] { 2 }, 2)]
        [DataRow(new int[] { 12, 3 }, 3)]
        [DataRow(new int[] { 2, 1, 0, -10, 80, -15, 9, -15 }, -15)]
        public void TestMin(int[] arr, int res)
        {
            lst.Init(arr);
            Assert.AreEqual(res, lst.Min());
        }


        [DataTestMethod]
        [DataRow(null)]
        [DataRow(new int[] { })]
        public void TestMaxEmptyArr(int[] input)
        {
            lst.Init(input);
            Assert.AreEqual(lst.Max(), null);
        }

        [DataTestMethod]
        [DataRow(new int[] { 2 }, 2)]
        [DataRow(new int[] { 12, 3 }, 12)]
        [DataRow(new int[] { 2, 1, 0, -10, 80, -15, 9, -15 }, 80)]
        public void TestMax(int[] arr, int res)
        {
            lst.Init(arr);
            Assert.AreEqual(res, lst.Max());
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow(new int[] { })]
        public void TestMinPosEmptyArr(int[] input)
        {
            lst.Init(input);
            Assert.AreEqual(lst.MinPos(), null);
        }

        [DataTestMethod]
        [DataRow(new int[] { 2 }, 0)]
        [DataRow(new int[] { 12, 3 }, 1)]
        [DataRow(new int[] { 2, 1, 0, -10, 80, -15, 9, -15 }, 5)]
        public void TestMinPos(int[] arr, int res)
        {
            lst.Init(arr);
            Assert.AreEqual(res, lst.MinPos());
        }


        [DataTestMethod]
        [DataRow(null)]
        [DataRow(new int[] { })]
        public void TestMaxPosEmptyArr(int[] input)
        {
            lst.Init(input);
            Assert.AreEqual(lst.MaxPos(), null);
        }
        [DataTestMethod]
        [DataRow(new int[] { 2 }, 0)]
        [DataRow(new int[] { 12, 3 }, 0)]
        [DataRow(new int[] { 2, 1, 0, -10, 80, -15, 9, -15 }, 4)]
        public void TestMaxPos(int[] arr, int res)
        {

            lst.Init(arr);
            Assert.AreEqual(res, lst.MaxPos());
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow(new int[] { })]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestSetExEmpty(int[] input)
        {
            lst.Init(input);
            lst.Set(0, 0);
        }

        [DataTestMethod]
        [DataRow(new int[] { 2 }, -1)]
        [DataRow(new int[] { 2 }, 1)]
        [DataRow(new int[] { 5, 6 }, -1)]
        [DataRow(new int[] { 5, 6 }, 2)]
        [DataRow(new int[] { 3, 7, 4, 9, 1 }, -1)]
        [DataRow(new int[] { 3, 7, 4, 9, 1 }, 5)]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestSetExOutRange(int[] input, int pos)
        {
            lst.Init(input);
            lst.Set(pos, 0);
        }

        [DataTestMethod]
        [DataRow(new int[] { 2 }, new int[] { 0 }, 0)]
        [DataRow(new int[] { 12, 3 }, new int[] { 0, 3 }, 0)]
        [DataRow(new int[] { 12, 3 }, new int[] { 12, 0 }, 1)]
        [DataRow(new int[] { 2, 1, 0, -10, 80 }, new int[] { 0, 1, 0, -10, 80 }, 0)]
        [DataRow(new int[] { 2, 1, 0, -10, 80 }, new int[] { 2, 1, 0, 0, 80 }, 3)]
        [DataRow(new int[] { 2, 1, 0, -10, 80 }, new int[] { 2, 1, 0, -10, 0 }, 4)]
        public void TestSet(int[] arr, int[] res, int pos)
        {
            lst.Init(arr);
            lst.Set(pos, 0);
            CollectionAssert.AreEqual(res, lst.ToArray());
        }

        [DataTestMethod]
        [DataRow(null, new int[] { })]
        [DataRow(new int[] { }, new int[] { })]
        [DataRow(new int[] { 2 }, new int[] { 2 })]
        [DataRow(new int[] { 12, 3 }, new int[] { 3, 12 })]
        [DataRow(new int[] { 2, 1, 0, -10, 80, -15, 9, 5 }, new int[] { 5, 9, -15, 80, -10, 0, 1, 2 })]
        public void TestReverse(int[] arr, int[] res)
        {
            lst.Init(arr);
            lst.Reverse();
            CollectionAssert.AreEqual(res, lst.ToArray());
        }

        [DataTestMethod]
        [DataRow(null, new int[] { })]
        [DataRow(new int[] { }, new int[] { })]
        [DataRow(new int[] { 2 }, new int[] { 2 })]
        [DataRow(new int[] { 12, 3 }, new int[] { 3, 12 })]
        [DataRow(new int[] { 2, 1, 0, 80, -15, 9, 5 }, new int[] { -15, 9, 5, 80, 2, 1, 0 })]
        public void TestHalfReverse(int[] arr, int[] res)
        {

            lst.Init(arr);
            lst.HalfReverse();
            CollectionAssert.AreEqual(res, lst.ToArray());
        }


        [DataTestMethod]
        [DataRow(null, new int[] { })]
        [DataRow(new int[] { }, new int[] { })]
        [DataRow(new int[] { 2 }, new int[] { 2 })]
        [DataRow(new int[] { 12, 3 }, new int[] { 3, 12 })]
        [DataRow(new int[] { 2, 1, 0, 80, -15, 9, -15 }, new int[] { -15, -15, 0, 1, 2, 9, 80 })]
        public void TestSort(int[] arr, int[] res)
        {

            lst.Init(arr);
            lst.Sort();
            CollectionAssert.AreEqual(res, lst.ToArray());
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow(new int[] { })]
        [DataRow(new int[] { 1 })]
        [DataRow(new int[] { 1, 2 })]
        [DataRow(new int[] { 1, 2, 3, 4, 5 })]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6 })]
        public void TestForeach(int[] input)
        {
            lst.Init(input);
            int i = 0;
            foreach (int item in lst)
            {
                Assert.AreEqual(item, input[i++]);
            }
        }



        [DataTestMethod]
        [DataRow(30)]
        [DataRow(40)]
        [DataRow(50)]
        public void TestInitOverflow(int n)
        {
            int[] arr = new int[n];
            int[] expected = new int[n];
            for (int i = 0; i < n; ++i)
            {
                arr[i] = i;
                expected[i] = i;
            }
            lst.Init(arr);
            CollectionAssert.AreEqual(expected, lst.ToArray());
        }

        [DataTestMethod]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 })]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 11 })]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 11 })]
        public void TestAddEndOverflow(int[] ini, int[] exp)
        {

            lst.Init(ini);
            lst.AddEnd(11);

            CollectionAssert.AreEqual(exp, lst.ToArray());
        }

        [DataTestMethod]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, new int[] { 11, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, }, new int[] { 11, 1, 2, 3, 4, 5, 6, 7, 8, 9 })]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }, new int[] { 11, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 })]
        public void TestAddStartOverflow(int[] ini, int[] exp)
        {

            lst.Init(ini);
            lst.AddStart(11);

            CollectionAssert.AreEqual(exp, lst.ToArray());
        }

        [DataTestMethod]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, new int[] { 1, 2, 3, 4, 5, 11, 6, 7, 8, 9, 10 })]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, new int[] { 1, 2, 3, 4, 5, 11, 6, 7, 8, 9 })]
        [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }, new int[] { 1, 2, 3, 4, 5, 11, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 })]
        public void TestAddPosOverflow(int[] ini, int[] exp)
        {

            lst.Init(ini);
            lst.AddPos(5, 11);

            CollectionAssert.AreEqual(exp, lst.ToArray());
        }
    }
}
