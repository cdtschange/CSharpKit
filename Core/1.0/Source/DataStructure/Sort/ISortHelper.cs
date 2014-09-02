using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.DataStructure.Sort
{
    public interface ISortHelper<T>
    {
        /// <summary>
        /// 二分法查找
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="value"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        int BinarySearch(T[] keys, int index, int length, T value, IComparer<T> comparer);
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="comparer"></param>
        void Sort(T[] keys, int index, int length, IComparer<T> comparer);
    }
}
