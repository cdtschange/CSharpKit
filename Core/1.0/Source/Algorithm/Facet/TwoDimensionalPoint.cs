using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Algorithm.Facet
{
    /// <summary>
    /// 二维点
    /// </summary>
    public class TwoDimensionalPoint
    {
        /// <summary>
        /// 查找所有二维极大值点
        /// （分治法）
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static List<Vector2> FindMaximumPoints(List<Vector2> points)
        {
            List<Vector2> list = points.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
            return FindMaximumPointsCore(list, 0, list.Count - 1);
        }

        private static List<Vector2> FindMaximumPointsCore(List<Vector2> points, int start, int end)
        {
            if (start > end)
            {
                return new List<Vector2>();
            }
            if (start == end)
            {
                return new List<Vector2>() { points[start] };
            }
            int mid = (start + end) / 2;
            List<Vector2> left = FindMaximumPointsCore(points, start, mid);
            List<Vector2> right = FindMaximumPointsCore(points, mid + 1, end);
            if (right.Count == 0)
            {
                return left;
            }
            return left.Where(p => p.Y > right.First().Y).Union(right).ToList();
        }

        /// <summary>
        /// 查找二维最近点对
        /// （分治法）
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static List<Vector2> FindClosestPoints(List<Vector2> points)
        {
            List<Vector2> list = points.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
            return FindClosestPointsCore(list, 0, list.Count - 1);
        }

        private static List<Vector2> FindClosestPointsCore(List<Vector2> points, int start, int end)
        {
            if (start == end)
            {
                return new List<Vector2>() { points[start] };
            }
            if (start + 1 == end)
            {
                return new List<Vector2>() { points[start], points[end] };
            }
            int mid = (start + end) / 2;
            List<Vector2> left = FindClosestPointsCore(points, start, mid);
            List<Vector2> right = FindClosestPointsCore(points, mid + 1, end);

            List<Vector2> result = new List<Vector2>();

            double distance = double.MaxValue;
            if (left.Count == 2)
            {
                distance = left[0].Distance(left[1]);
                result = left;
            }
            if (right.Count == 2)
            {
                double rDistance = right[0].Distance(right[1]);
                if (rDistance < distance)
                {
                    distance = rDistance;
                    result = right;
                }

            }

            double midX = points[mid].X;

            for (int i = start; i < mid + 1; i++)
            {
                var tl = points[i];
                if (midX - tl.X >= distance)
                {
                    continue;
                }
                for (int j = mid + 1; j < end + 1; j++)
                {
                    var tr = points[j];
                    if ((tr.Y - tl.Y) >= distance || (tr.X - tl.X) >= distance)
                    {
                        continue;
                    }
                    double tdistance = tl.Distance(tr);
                    if (tdistance < distance)
                    {
                        result = new List<Vector2>() { tl, tr };
                        distance = tdistance;
                    }
                }
            }

            return result;
        }
    }
}
