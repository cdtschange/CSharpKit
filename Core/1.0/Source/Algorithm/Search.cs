using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Algorithm
{
    public class Search<T> where T : IComparable
    {

        /// <summary>
        /// Binary search
        /// </summary>
        /// <remarks>  
        /// 时间复杂度：
        /// 最好情况：O(1)
        /// 平均情况：O(log(n))
        /// 最坏情况：O(log(n))
        /// log(n) means log2(n)
        /// </remarks>
        /// <param name="arr">Sorted array by asc</param>      
        /// <param name="x">Element need to find</param>
        /// <returns>Index of the Element in the sort(start from 1)</returns>
        public static int BinarySearch(T[] arr, T x)
        {
            if (arr == null) return 0;

            int n = arr.Length;
            int i = 1, m = 0, compare = 0;
            while (i <= n)
            {
                m = (i + n) / 2;
                compare = x.CompareTo(arr[m - 1]);
                if (compare == 0)
                {
                    return m;
                }
                else if (compare < 0)
                {
                    n = m - 1;
                }
                else
                {
                    i = m + 1;
                }
            }
            m = 0;
            return m;
        }
    }
}
