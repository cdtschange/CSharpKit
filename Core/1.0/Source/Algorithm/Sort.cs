using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Algorithm
{
    public class Sort<T> where T : IComparable
    {
        /// <summary>
        /// 直接插入排序
        /// </summary>
        /// <remarks>
        /// 时间复杂度：
        /// 最好情况：O(n)
        /// 平均情况：O(n^2)
        /// 最坏情况：O(n^2)
        /// 
        /// 外层循环有两次数据移动操作要执行：
        ///  T temp = arr[j];
        ///  arr[i + 1] = temp;
        ///  
        /// 用di 表示在内层循环中对xi执行的数据移动总次数
        /// 
        /// 直接插入排序的数据移动总次数为
        /// X=SUM(2+di)=2(n-1)+SUM(di)
        /// 最好情况：（输入数据已经顺序）SUM(di)=0, X=2(n-1)=O(n)
        /// 最坏情况：（输入数据已经逆序）
        /// d1=1,d2=2,d3=3...dn=n
        /// SUM(di)=n/2*(n-1)
        /// X=2(n-1)+n/2*(n-1)=1/2*(n-1)*(n+4)=O(n^2)
        /// 平均情况：
        /// xi (i从2~n)若是数组中第k小的，内层循环中将移动k个数据，加上外层2个移动将是k+2个数据
        /// xi是最小元素的概率是1/i，因此(2+di)的平均是
        /// 2/i+3/i+...+(i+1)/i=(i+3)/2
        /// X=SUM((i+3)/2)=1/4*(n-1)*(n+8)=O(n^2)
        /// </remarks>
        /// <param name="arr">Array need to be sorted</param>
        /// <returns>Time Complexity</returns>
        public static int StraightInsertionSort(T[] arr)
        {
            if (arr == null) return 0;

            int n = arr.Length;
            int on = 0;

            for (int j = 1; j < n; j++)
            {
                int i = j - 1;
                T xi = arr[j];
                on++;
                while (i >= 0 && xi.CompareTo(arr[i]) < 0)
                {
                    on++;
                    arr[i + 1] = arr[i];
                    i--;
                }
                arr[i + 1] = xi;
                on++;
            }
            return on;
        }

        /// <summary>
        /// 直接选择排序
        /// </summary>
        /// 时间复杂度：
        /// 最好情况：O(1)
        /// 平均情况：O(nlogn)
        /// 最坏情况：O(n^2)
        /// <param name="arr"></param>
        /// <returns></returns>
        public static int StraightSelectionSort(T[] arr)
        {
            if (arr == null) return 0;

            int n = arr.Length;
            int on = 0;

            for (int j = 0; j < n; j++)
            {
                int f = j;
                for (int k = j + 1; k < n; k++)
                {
                    if (arr[k].CompareTo(arr[f]) < 0)
                    {
                        f = k;
                    }
                }
                if (f != j)
                {
                    Swap(ref arr[j], ref arr[f]);
                    on++;
                }
            }
            return on;
        }

        /// <summary>
        /// 快速排序
        /// </summary>
        /// 时间复杂度：
        /// 最好情况：O(nlogn)
        /// 平均情况：O(nlogn)
        /// 最坏情况：O(n^2)
        /// <param name="arr"></param>
        /// <returns></returns>
        public static int QuickSort(T[] arr, int start, int end)
        {
            if (arr == null) return 0;
            if (start >= end)
            {
                return 0;
            }

            T xi = arr[start];
            int i = start, j = end;
            while (i < j)
            {
                while (arr[j].CompareTo(xi) >= 0 && j > i)
                {
                    j--;
                }
                if (j == i)
                {
                    continue;
                }
                Swap(ref arr[i], ref arr[j]);

                while (arr[i].CompareTo(xi) <= 0 && j > i)
                {
                    i++;
                }
                if (j == i)
                {
                    continue;
                }
                Swap(ref arr[i], ref arr[j]);
            }

            QuickSort(arr, start, j - 1);
            QuickSort(arr, j + 1, end);


            return 0;
        }

        /// <summary>
        /// 冒泡排序
        /// </summary>
        /// 时间复杂度：
        /// 最好情况：O(1)
        /// 平均情况：O(n^2)
        /// 最坏情况：O(n^2)
        /// <param name="arr"></param>
        /// <returns></returns>
        public static int BubbleSort(T[] arr)
        {
            if (arr == null) return 0;
            int on = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[i].CompareTo(arr[j]) > 0)
                    {
                        Swap(ref arr[i], ref arr[j]);
                        on++;
                    }
                }
            }
            return on;
        }

        /// <summary>
        /// 堆排序
        /// </summary>
        /// 时间复杂度：
        /// 最好情况：
        /// 平均情况：O(nlogn)
        /// 最坏情况：O(nlogn)
        /// <param name="arr"></param>
        /// <returns></returns>
        public static int HeapSort(T[] arr)
        {
            if (arr == null) return 0;
            int on = 0;

            BuildMaxHeap(arr);    //创建大顶推（初始状态看做：整体无序）
            for (int i = arr.Length - 1; i > 0; i--)
            {
                Swap(ref arr[0], ref arr[i]); //将堆顶元素依次与无序区的最后一位交换（使堆顶元素进入有序区）
                MaxHeapify(arr, 0, i); //重新将无序区调整为大顶堆
            }
            return on;
        }

        /// <summary>
        /// 创建大顶推（根节点大于左右子节点）
        /// </summary>
        /// <param name="arr">待排数组</param>
        private static void BuildMaxHeap(T[] arr)
        {
            //根据大顶堆的性质可知：数组的前半段的元素为根节点，其余元素都为叶节点
            for (int i = arr.Length / 2 - 1; i >= 0; i--) //从最底层的最后一个根节点开始进行大顶推的调整
            {
                MaxHeapify(arr, i, arr.Length); //调整大顶堆
            }
        }

        /// <summary>
        /// 大顶推的调整过程
        /// </summary>
        /// <param name="arr">待调整的数组</param>
        /// <param name="currentIndex">待调整元素在数组中的位置（即：根节点）</param>
        /// <param name="heapSize">堆中所有元素的个数</param>
        private static void MaxHeapify(T[] arr, int currentIndex, int heapSize)
        {
            int left = 2 * currentIndex + 1;    //左子节点在数组中的位置
            int right = 2 * currentIndex + 2;   //右子节点在数组中的位置
            int large = currentIndex;   //记录此根节点、左子节点、右子节点 三者中最大值的位置

            if (left < heapSize && arr[left].CompareTo(arr[large]) > 0)  //与左子节点进行比较
            {
                large = left;
            }
            if (right < heapSize && arr[right].CompareTo(arr[large]) > 0)    //与右子节点进行比较
            {
                large = right;
            }
            if (currentIndex != large)  //如果 currentIndex != large 则表明 large 发生变化（即：左右子节点中有大于根节点的情况）
            {
                Swap(ref arr[currentIndex], ref arr[large]);    //将左右节点中的大者与根节点进行交换（即：实现局部大顶堆）
                MaxHeapify(arr, large, heapSize); //以上次调整动作的large位置（为此次调整的根节点位置），进行递归调整
            }
        }

        /// 二路归并排序
        /// </summary>
        /// 时间复杂度：O(nlogn)
        /// <param name="arr"></param>
        /// <returns></returns>
        public static int TwoWayMergeSort(T[] arr, int start, int end)
        {
            int on = 0;
            if (start < end)
            {
                int mid = (start + end) / 2;
                TwoWayMergeSort(arr, start, mid);
                TwoWayMergeSort(arr, mid + 1, end);
                TwoWayMergeSortCore(arr, start, mid, end);
            }
            return on;
        }
        private static int TwoWayMergeSortCore(T[] arr, int start, int mid, int end)
        {
            T[] newArr = new T[end + 1];
            int on = 0, i = start, j = mid + 1, index = 0;

            while (i <= mid && j <= end)
            {
                if (arr[i].CompareTo(arr[j]) < 0)
                {
                    newArr[index++] = arr[i++];
                    on++;
                }
                else
                {
                    newArr[index++] = arr[j++];
                    on++;
                }
            }
            while (i <= mid)
            {
                newArr[index++] = arr[i++];
            }
            while (j <= end)
            {
                newArr[index++] = arr[j++];
            }
            index = 0;
            for (int k = start; k <= end; k++)
            {
                arr[k] = newArr[index++];
            }
            return on;
        }


        /// <summary>
        /// 交换函数
        /// </summary>
        /// <param name="a">元素a</param>
        /// <param name="b">元素b</param>
        private static void Swap(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

    }
}
