using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Utility
{
    public class Functions
    {
        /// <summary>
        /// 斐波那契数列
        /// </summary>
        /// <param name="index">项数（从1开始）</param>
        /// <returns>对应项的值</returns>
        public static int Fibonacci(int index)
        {
            if (index == 1 || index == 2) { return 1; }
            int i = 1, j = 1, count = 2, temp = j;
            while (count < index)
            {
                temp += i;
                i = j;
                j = temp;
                count++;
            }
            return j;
        }

        //函数f(i,n)，返回1到n之间出现的i的个数，例如f(1,12)=5
        public static ulong FSumcount(ulong i, ulong n)
        {
            if (0 > i || i > 9)
            {
                throw new ArgumentOutOfRangeException("i must be between 0 to 9!");
            }
            ulong iCount = 0;
            ulong iFactor = 1;
            ulong iLowerNum = 0;
            ulong iCurrentNum = 0;
            ulong iHigherNum = 0;
            while (n / iFactor != 0)
            {
                if ((n / (iFactor * 10)) == 0 && i == 0)
                {
                    break;
                }
                iLowerNum = n - (n / iFactor) * iFactor;
                iCurrentNum = (n / iFactor) % 10;
                iHigherNum = n / (iFactor * 10);
                if (iCurrentNum < i)
                {
                    iCount += iHigherNum * iFactor;
                }
                else if (iCurrentNum == i)
                {
                    if (i == 0)
                    {
                        iCount += (iHigherNum - 1) * iFactor + iLowerNum + 1;
                    }
                    else
                    {
                        iCount += iHigherNum * iFactor + iLowerNum + 1;
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        iCount += iHigherNum * iFactor;
                    }
                    else
                    {
                        iCount += (iHigherNum + 1) * iFactor;
                    }
                }
                iFactor *= 10;
            }
            return iCount;
        }
    }
}
