using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cdts.Algorithm;

namespace AlgorithmTest
{
    [TestClass]
    public class SortTest
    {
        [TestMethod]
        public void StraightInsertionSortTest()
        {
            int[] arr = new int[] { 7, 5, 1, 4, 3, 2, 6 };
            int n = arr.Length;
            int on = Sort<int>.StraightInsertionSort(arr);
            for (int i = 1; i < 8; i++)
            {
                Assert.AreEqual(i, arr[i - 1]);
            }
            //Assert.AreEqual(2 * (n - 1), on);
            arr = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            on = Sort<int>.StraightInsertionSort(arr);
            for (int i = 1; i < 8; i++)
            {
                Assert.AreEqual(i, arr[i - 1]);
            }
            Assert.AreEqual(2 * (n - 1), on);
            arr = new int[] { 7, 6, 5, 4, 3, 2, 1 };
            on = Sort<int>.StraightInsertionSort(arr);
            for (int i = 1; i < 8; i++)
            {
                Assert.AreEqual(i, arr[i - 1]);
            }
            Assert.AreEqual(1.0 / 2 * (n - 1) * (n + 4), on);
        }

        [TestMethod]
        public void StraightSelectionSortTest()
        {
            int[] arr = new int[] { 7, 5, 1, 4, 3, 2, 6 };
            int n = arr.Length;
            int on = Sort<int>.StraightSelectionSort(arr);
            for (int i = 1; i < 8; i++)
            {
                Assert.AreEqual(i, arr[i - 1]);
            }
            //Assert.AreEqual(2 * (n - 1), on);
            arr = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            on = Sort<int>.StraightSelectionSort(arr);
            for (int i = 1; i < 8; i++)
            {
                Assert.AreEqual(i, arr[i - 1]);
            }
            Assert.AreEqual(0, on);
            arr = new int[] { 7, 6, 5, 4, 3, 2, 1 };
            on = Sort<int>.StraightSelectionSort(arr);
            for (int i = 1; i < 8; i++)
            {
                Assert.AreEqual(i, arr[i - 1]);
            }
            Assert.AreEqual(3, on);
        }

        [TestMethod]
        public void QuickSortTest()
        {
            int[] arr = new int[] { 7, 5, 1, 4, 3, 2, 6 };
            int n = arr.Length;
            int on = Sort<int>.QuickSort(arr, 0, n - 1);
            for (int i = 1; i < 8; i++)
            {
                Assert.AreEqual(i, arr[i - 1]);
            }
            //Assert.AreEqual(2 * (n - 1), on);
            arr = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            on = Sort<int>.QuickSort(arr, 0, n - 1);
            for (int i = 1; i < 8; i++)
            {
                Assert.AreEqual(i, arr[i - 1]);
            }
            //Assert.AreEqual(0, on);
            arr = new int[] { 7, 6, 5, 4, 3, 2, 1 };
            on = Sort<int>.QuickSort(arr, 0, n - 1);
            for (int i = 1; i < 8; i++)
            {
                Assert.AreEqual(i, arr[i - 1]);
            }
            //Assert.AreEqual(3, on);
        }

        [TestMethod]
        public void BubbleSortTest()
        {
            int[] arr = new int[] { 7, 5, 1, 4, 3, 2, 6 };
            int n = arr.Length;
            int on = Sort<int>.BubbleSort(arr);
            for (int i = 1; i < 8; i++)
            {
                Assert.AreEqual(i, arr[i - 1]);
            }
            //Assert.AreEqual(2 * (n - 1), on);
            arr = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            on = Sort<int>.BubbleSort(arr);
            for (int i = 1; i < 8; i++)
            {
                Assert.AreEqual(i, arr[i - 1]);
            }
            Assert.AreEqual(0, on);
            arr = new int[] { 7, 6, 5, 4, 3, 2, 1 };
            on = Sort<int>.BubbleSort(arr);
            for (int i = 1; i < 8; i++)
            {
                Assert.AreEqual(i, arr[i - 1]);
            }
            Assert.AreEqual(n * (n - 1) / 2.0, on);
        }

        [TestMethod]
        public void HeapSortTest()
        {
            int[] arr = new int[] { 7, 5, 1, 4, 3, 2, 6 };
            int n = arr.Length;
            int on = Sort<int>.HeapSort(arr);
            for (int i = 1; i < 8; i++)
            {
                Assert.AreEqual(i, arr[i - 1]);
            }
            //Assert.AreEqual(2 * (n - 1), on);
            arr = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            on = Sort<int>.HeapSort(arr);
            for (int i = 1; i < 8; i++)
            {
                Assert.AreEqual(i, arr[i - 1]);
            }
            //Assert.AreEqual(0, on);
            arr = new int[] { 7, 6, 5, 4, 3, 2, 1 };
            on = Sort<int>.HeapSort(arr);
            for (int i = 1; i < 8; i++)
            {
                Assert.AreEqual(i, arr[i - 1]);
            }
            //Assert.AreEqual(n * (n - 1) / 2.0, on);
        }

        [TestMethod]
        public void TwoWayMergeSortTest()
        {
            int[] arr = new int[] { 7, 5, 1, 4, 3, 2, 6 };
            int n = arr.Length;
            int on = Sort<int>.TwoWayMergeSort(arr, 0, n - 1);
            for (int i = 1; i < 8; i++)
            {
                Assert.AreEqual(i, arr[i - 1]);
            }
            //Assert.AreEqual(2 * (n - 1), on);
            arr = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            on = Sort<int>.TwoWayMergeSort(arr, 0, n - 1);
            for (int i = 1; i < 8; i++)
            {
                Assert.AreEqual(i, arr[i - 1]);
            }
            //Assert.AreEqual(0, on);
            arr = new int[] { 7, 6, 5, 4, 3, 2, 1 };
            on = Sort<int>.TwoWayMergeSort(arr, 0, n - 1);
            for (int i = 1; i < 8; i++)
            {
                Assert.AreEqual(i, arr[i - 1]);
            }
            //Assert.AreEqual(3, on);
        }


    }
}
