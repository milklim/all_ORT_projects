using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lists;

namespace Tests
{
    [TestClass]
    public class MsTstAList0 : MSTestComplex
    {
        internal override IList MakeList()
        {
            return new AList0();
        }
    }

    [TestClass]
    public class MsTstSLList : MSTestComplex
    {
        internal override IList MakeList()
        {
            return new SLList();
        }
    }

    [TestClass]
    public class MsTstSLList2 : MSTestComplex
    {
        internal override IList MakeList()
        {
            return new SLList2();
        }
    }

    [TestClass]
    public class MsTstDLList : MSTestComplex
    {
        internal override IList MakeList()
        {
            return new DLList();
        }
    }

    [TestClass]
    public abstract class MSTestComplex
    {
        internal abstract IList MakeList();

        IList lst = null;

        public MSTestComplex()
        {
            lst = MakeList();
        }

        [TestInitialize]
        public void TestSetUp()
        {
            lst.Clear();
        }

        //а) Min, Max, MinPos, MaxPos

        [DataTestMethod]
        [DataRow(new int[] { 1 }, new int[] { 1, 1, 0, 0 })]
        [DataRow(new int[] { 1, 2 }, new int[] { 1, 2, 0, 1 })]
        [DataRow(new int[] { 1, 2, 3 }, new int[] { 1, 3, 0, 2 })]
        [DataRow(new int[] { 1, 2, 3, 4, 5 }, new int[] { 1, 5, 0, 4 })]
        [DataRow(new int[] { 5, -5, 17, 21, 86, -153, 390 }, new int[] { -153, 390, 5, 6 })]
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

        //b) Get, Set

        [DataTestMethod]
        [DataRow(new int[] { 1 }, 0, 0, new int[] { 1 })]
        [DataRow(new int[] { 1, 2 }, 0, 1, new int[] { 1, 1 })]
        [DataRow(new int[] { 1, 2, 3 }, 2, 0, new int[] { 3, 2, 3 })]
        [DataRow(new int[] { 1, -2, 3, 0, 5 }, 4, 1, new int[] { 1, 5, 3, 0, 5 })]
        [DataRow(new int[] { 5, -5, 17, 21, 86, -153, 390 }, 5, 0, new int[] { -153, -5, 17, 21, 86, -153, 390 })]
        public void TestGetSet(int[] ini, int posGet, int posSet, int[] exp)
        {
            lst.Init(ini);
            lst.Set(posSet, lst.Get(posGet));

            CollectionAssert.AreEqual(exp, lst.ToArray());
        }

        //c) AddPos, Sort

        [DataTestMethod]
        [DataRow(new int[] { 1 }, 0, 5, new int[] { 1, 5 })]
        [DataRow(new int[] { 1, 2 }, 1, -3, new int[] { -3, 1, 2 })]
        [DataRow(new int[] { 1, 2, 3 }, 2, 0, new int[] { 0, 1, 2, 3 })]
        [DataRow(new int[] { 1, 2, 3, 4, 5 }, 1, 4, new int[] { 1, 2, 3, 4, 4, 5 })]
        [DataRow(new int[] { 5, -5, 17, 21, 86, -153, 390 }, 6, -3, new int[] { -153, -5, -3, 5, 17, 21, 86, 390 })]
        public void TestAddPosSort(int[] ini, int pos, int val, int[] exp)
        {
            lst.Init(ini);

            lst.AddPos(pos, val);
            lst.Sort();

            CollectionAssert.AreEqual(exp, lst.ToArray());
        }

        //d) AddBegin, Reverse

        [DataTestMethod]
        [DataRow(new int[] { 1 }, 5, new int[] { 1, 5 })]
        [DataRow(new int[] { 1, 2 }, -3, new int[] { 2, 1, -3 })]
        [DataRow(new int[] { 1, 2, 3 }, 0, new int[] { 3, 2, 1, 0 })]
        [DataRow(new int[] { 1, 2, 3, 4, 5 }, 4, new int[] { 5, 4, 3, 2, 1, 4 })]
        [DataRow(new int[] { 5, -5, 17, 21, 86, -153, 390 }, -3, new int[] { 390, -153, 86, 21, 17, -5, 5, -3 })]
        public void TestAddStartReverse(int[] ini, int val, int[] exp)
        {
            lst.Init(ini);

            lst.AddStart(val);
            lst.Reverse();

            CollectionAssert.AreEqual(exp, lst.ToArray());
        }

        //e) AddEnd, HalfReverse

        [DataTestMethod]
        [DataRow(new int[] { 1 }, 5, new int[] { 5, 1 })]
        [DataRow(new int[] { 1, 2 }, -3, new int[] { -3, 2, 1 })]
        [DataRow(new int[] { 1, 2, 3 }, 0, new int[] { 3, 0, 1, 2 })]
        [DataRow(new int[] { 1, 2, 3, 4 }, 4, new int[] { 4, 4, 3, 1, 2 })]
        [DataRow(new int[] { 5, -5, 17, 21, 86, -153, 390 }, -3, new int[] { 86, -153, 390, -3, 5, -5, 17, 21 })]
        public void TestAddEndHalfReverse(int[] ini, int val, int[] exp)
        {
            lst.Init(ini);
            lst.AddEnd(val);
            lst.HalfReverse();

            CollectionAssert.AreEqual(exp, lst.ToArray());
        }

        //f) DelPos, Sort

        [DataTestMethod]
        [DataRow(new int[] { 1, 5 }, 0, new int[] { 5 })]
        [DataRow(new int[] { 1, 2, 3 }, 2, new int[] { 1, 2 })]
        [DataRow(new int[] { 1, -2, 3, 0, 5 }, 1, new int[] { 0, 1, 3, 5 })]
        [DataRow(new int[] { 5, -5, 17, 21, 86, -153, 390 }, 6, new int[] { -153, -5, 5, 17, 21, 86 })]
        public void TestDelPosSort(int[] ini, int pos, int[] exp)
        {
            lst.Init(ini);

            lst.DelPos(pos);
            lst.Sort();

            CollectionAssert.AreEqual(exp, lst.ToArray());
        }

        //g) DelBegin, Reverse

        [DataTestMethod]
        [DataRow(new int[] { 1, 5 }, new int[] { 5 })]
        [DataRow(new int[] { 1, 2, 3 }, new int[] { 3, 2 })]
        [DataRow(new int[] { 1, 2, 3, 4, 5 }, new int[] { 5, 4, 3, 2 })]
        [DataRow(new int[] { 5, -5, 17, 21, 86, -153, 390 }, new int[] { 390, -153, 86, 21, 17, -5 })]
        public void TestDelStratReverse(int[] ini, int[] exp)
        {
            lst.Init(ini);

            lst.DelStart();
            lst.Reverse();

            CollectionAssert.AreEqual(exp, lst.ToArray());
        }

        //h) DelEnd, HalfReverse

        [DataTestMethod]
        [DataRow(new int[] { 1, 5 }, new int[] { 1 })]
        [DataRow(new int[] { 1, 2, 3 }, new int[] { 2, 1 })]
        [DataRow(new int[] { 1, 2, 3, 4 }, new int[] { 3, 2, 1 })]
        [DataRow(new int[] { 5, -5, 17, 21, 86, -153 }, new int[] { 21, 86, 17, 5, -5 })]
        public void TestDelEndHalfReverse(int[] ini, int[] exp)
        {
            lst.Init(ini);

            lst.DelEnd();
            lst.HalfReverse();

            CollectionAssert.AreEqual(exp, lst.ToArray());
        }
    }
}
