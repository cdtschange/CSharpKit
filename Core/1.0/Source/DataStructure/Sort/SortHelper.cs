using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.DataStructure.Sort
{
    public class SortHelper<T> : ISortHelper<T>
    {
        // Fields
        private static ISortHelper<T> defaultArraySortHelper;

        // Methods
        public int BinarySearch(T[] array, int index, int length, T value, IComparer<T> comparer)
        {
            int num = 0;
            try
            {
                if (comparer == null)
                {
                    comparer = Comparer<T>.Default;
                }
                num = SortHelper<T>.InternalBinarySearch(array, index, length, value, comparer);
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("InvalidOperation_IComparerFailed", exception);
            }
            return num;
        }

        private static ISortHelper<T> CreateArraySortHelper()
        {
            SortHelper<T>.defaultArraySortHelper = new SortHelper<T>();
            return SortHelper<T>.defaultArraySortHelper;
        }

        internal static int InternalBinarySearch(T[] array, int index, int length, T value, IComparer<T> comparer)
        {
            int num = index;
            int num2 = (index + length) - 1;
            while (num <= num2)
            {
                int num3 = num + ((num2 - num) >> 1);
                int num4 = comparer.Compare(array[num3], value);
                if (num4 == 0)
                {
                    return num3;
                }
                if (num4 < 0)
                {
                    num = num3 + 1;
                }
                else
                {
                    num2 = num3 - 1;
                }
            }
            return ~num;
        }

        public static void QuickSort(T[] keys, int left, int right, IComparer<T> comparer)
        {
            if (comparer == null)
            {
                comparer = Comparer<T>.Default;
            }
            do
            {
                int a = left;
                int b = right;
                int num3 = a + ((b - a) >> 1);
                SortHelper<T>.SwapIfGreaterWithItems(keys, comparer, a, num3);
                SortHelper<T>.SwapIfGreaterWithItems(keys, comparer, a, b);
                SortHelper<T>.SwapIfGreaterWithItems(keys, comparer, num3, b);
                T y = keys[num3];
                do
                {
                    while (comparer.Compare(keys[a], y) < 0)
                    {
                        a++;
                    }
                    while (comparer.Compare(y, keys[b]) < 0)
                    {
                        b--;
                    }
                    if (a > b)
                    {
                        break;
                    }
                    if (a < b)
                    {
                        T local2 = keys[a];
                        keys[a] = keys[b];
                        keys[b] = local2;
                    }
                    a++;
                    b--;
                }
                while (a <= b);
                if ((b - left) <= (right - a))
                {
                    if (left < b)
                    {
                        SortHelper<T>.QuickSort(keys, left, b, comparer);
                    }
                    left = a;
                }
                else
                {
                    if (a < right)
                    {
                        SortHelper<T>.QuickSort(keys, a, right, comparer);
                    }
                    right = b;
                }
            }
            while (left < right);
        }

        public void Sort(T[] keys, int index, int length, IComparer<T> comparer)
        {
            try
            {
                if (comparer == null)
                {
                    comparer = Comparer<T>.Default;
                }
                SortHelper<T>.QuickSort(keys, index, index + (length - 1), comparer);
            }
            catch (IndexOutOfRangeException)
            {
                object[] values = new object[3];
                values[1] = typeof(T).Name;
                values[2] = comparer;
                throw new ArgumentException(string.Format("Arg_BogusIComparer {0}", values));
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("InvalidOperation_IComparerFailed", exception);
            }
        }

        private static void SwapIfGreaterWithItems(T[] keys, IComparer<T> comparer, int a, int b)
        {
            if ((a != b) && (comparer.Compare(keys[a], keys[b]) > 0))
            {
                T local = keys[a];
                keys[a] = keys[b];
                keys[b] = local;
            }
        }

        // Properties
        public static ISortHelper<T> Default
        {
            get
            {
                ISortHelper<T> defaultArraySortHelper = SortHelper<T>.defaultArraySortHelper;
                if (defaultArraySortHelper == null)
                {
                    defaultArraySortHelper = SortHelper<T>.CreateArraySortHelper();
                }
                return defaultArraySortHelper;
            }
        }

    }
}
