using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cdts.Algorithm.Graphics;

namespace Cdts.Algorithm.Facet
{
    public class ConvexHull
    {
        /// <summary>
        /// Graham算法求凸包
        /// 时间复杂度：O(nlog2n)
        /// 点集集中在凸包上更快
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Graphic<Vector2, double> GrahamScan(List<Vector2> points)
        {
            Graphic<Vector2, double> graphic = new Graphic<Vector2, double>();
            if (points.Count < 3)
            {
                throw new InvalidOperationException("Convex hull can not be formde by less than 3 points!");
            }

            Vector2 first = null;
            double minX = double.MaxValue;
            double minY = double.MaxValue;
            for (int i = 0; i < points.Count; i++)
            {
                var pi = points[i];
                if (minY > pi.Y)
                {
                    minY = pi.Y;
                    first = pi;
                }
                else if (minY == pi.Y && minX > pi.X)
                {
                    minX = pi.X;
                    first = pi;
                }
            }
            List<Vector2> list = points.Where(p => p != first).OrderBy(p => (p - first).Angle(new Vector2(1, 0))).ThenBy(p => first.Distance(p)).ToList();
            List<Vector2> result = new List<Vector2>();

            result.Add(first);
            result.Add(list[0]);
            result.Add(list[1]);

            int top = 2;

            for (int i = 2; i < list.Count; i++)
            {
                while (Vector2.CrossProduct((result[top] - result[top - 1]).ToVector2(), (list[i] - result[top]).ToVector2()) <= 0)
                {
                    result.RemoveAt(top--);
                }
                ++top;
                result.Add(list[i]);
            }

            for (int i = 0; i < result.Count; i++)
            {
                graphic.Vertexes.Add(new Vertex<Vector2>() { Value = result[i] });
                if (i > 0)
                {
                    graphic.Edges.Add(new Edge<Vector2, double>() { LeftNode = graphic.Vertexes[i - 1], RightNode = graphic.Vertexes[i], Weight = result[i - 1].Distance(result[i]) });
                }
                if (i == result.Count - 1)
                {
                    graphic.Edges.Add(new Edge<Vector2, double>() { LeftNode = graphic.Vertexes[i], RightNode = graphic.Vertexes[0], Weight = result[i].Distance(result[0]) });
                }
            }

            return graphic;
        }

        /// <summary>
        /// Gift Wrapping算法求凸包
        /// 时间复杂度：O(nh) h 为凸包上的点
        /// 点集平均或随机分布更快
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Graphic<Vector2, double> GiftWrappingScan(List<Vector2> points)
        {
            Graphic<Vector2, double> graphic = new Graphic<Vector2, double>();
            if (points.Count < 3)
            {
                throw new InvalidOperationException("Convex hull can not be formde by less than 3 points!");
            }

            Vector2 first = null;
            double minX = double.MaxValue;
            double minY = double.MaxValue;
            for (int i = 0; i < points.Count; i++)
            {
                var pi = points[i];
                if (minY > pi.Y)
                {
                    minY = pi.Y;
                    first = pi;
                }
                else if (minY == pi.Y && minX > pi.X)
                {
                    minX = pi.X;
                    first = pi;
                }
            }
            Vector2 next = null;
            double minAngle = double.MaxValue;
            double minDistance = double.MaxValue;
            for (int i = 0; i < points.Count; i++)
            {
                var pi = points[i];
                if (pi == first)
                {
                    continue;
                }
                var pAngle = new Vector2(1, 0).RotateAngle((pi - first).ToVector2());
                var pDistance = first.Distance(pi);
                if (minAngle > pAngle)
                {
                    minAngle = pAngle;
                    next = pi;
                }
                else if (minAngle == pi.Y && minDistance > pDistance)
                {
                    minDistance = pDistance;
                    next = pi;
                }
            }
            List<Vector2> result = new List<Vector2>();

            result.Add(first);

            while (next != first)
            {
                result.Add(next);
                var temp = (next - result[result.Count - 2]).ToVector2();
                minAngle = double.MaxValue;
                minDistance = double.MaxValue;
                Vector2 tempNext = next;
                for (int i = 0; i < points.Count; i++)
                {
                    var pi = points[i];
                    if (pi == tempNext)
                    {
                        continue;
                    }

                    var pAngle = temp.RotateAngle((pi - tempNext).ToVector2());
                    var pDistance = tempNext.Distance(pi);
                    if (minAngle > pAngle)
                    {
                        minAngle = pAngle;
                        next = pi;
                    }
                    else if (minAngle == pAngle && minDistance > pDistance)
                    {
                        minDistance = pDistance;
                        next = pi;
                    }
                }
            }

            for (int i = 0; i < result.Count; i++)
            {
                graphic.Vertexes.Add(new Vertex<Vector2>() { Value = result[i] });
                if (i > 0)
                {
                    graphic.Edges.Add(new Edge<Vector2, double>() { LeftNode = graphic.Vertexes[i - 1], RightNode = graphic.Vertexes[i], Weight = result[i - 1].Distance(result[i]) });
                }
                if (i == result.Count - 1)
                {
                    graphic.Edges.Add(new Edge<Vector2, double>() { LeftNode = graphic.Vertexes[i], RightNode = graphic.Vertexes[0], Weight = result[i].Distance(result[0]) });
                }
            }

            return graphic;
        }

        /// <summary>
        /// 快速凸包算法
        /// 时间复杂度：O(nlog2n)
        /// 集Graham算法和Gift Wrapping算法优势于一身
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Graphic<Vector2, double> QuickHullScan(List<Vector2> points)
        {
            Graphic<Vector2, double> graphic = new Graphic<Vector2, double>();
            if (points.Count < 3)
            {
                throw new InvalidOperationException("Convex hull can not be formde by less than 3 points!");
            }
            List<Vector2> result = new List<Vector2>();

            Vector2 first = null;
            Vector2 last = null;
            double minX = double.MaxValue;
            double minY = double.MaxValue;
            double maxX = double.MinValue;
            double maxY = double.MinValue;
            for (int i = 0; i < points.Count; i++)
            {
                var pi = points[i];
                if (minY > pi.Y)
                {
                    minY = pi.Y;
                    first = pi;
                }
                else if (minY == pi.Y && minX > pi.X)
                {
                    minX = pi.X;
                    first = pi;
                }
                if (maxY < pi.Y)
                {
                    maxY = pi.Y;
                    last = pi;
                }
                else if (maxY == pi.Y && maxX < pi.X)
                {
                    maxX = pi.X;
                    last = pi;
                }
            }
            result.Add(first);
            result.Add(last);

            var ab = (last - first).ToVector2();

            List<Vector2> left = points.Where(p => p != first && p != last && ab.CrossProduct((p - first).ToVector2()) > 0).ToList();
            List<Vector2> right = points.Except(left).Where(p => p != first && p != last).ToList();

            QuickHullScanCore(first, last, result, right);
            QuickHullScanCore(last, first, result, left);

            for (int i = 0; i < result.Count; i++)
            {
                graphic.Vertexes.Add(new Vertex<Vector2>() { Value = result[i] });
                if (i > 0)
                {
                    graphic.Edges.Add(new Edge<Vector2, double>() { LeftNode = graphic.Vertexes[i - 1], RightNode = graphic.Vertexes[i], Weight = result[i - 1].Distance(result[i]) });
                }
                if (i == result.Count - 1)
                {
                    graphic.Edges.Add(new Edge<Vector2, double>() { LeftNode = graphic.Vertexes[i], RightNode = graphic.Vertexes[0], Weight = result[i].Distance(result[0]) });
                }
            }

            return graphic;
        }

        private static void QuickHullScanCore(Vector2 a, Vector2 b, List<Vector2> result, List<Vector2> points)
        {
            if (points.Count == 0)
            {
                return;
            }
            else if (points.Count == 1)
            {
                result.Insert(result.IndexOf(b), points[0]);
                points.Clear();
                return;
            }

            double dist = double.MinValue;
            Vector2 ab = (b - a).ToVector2();
            Vector2 next = null;
            for (int i = 0; i < points.Count; i++)
            {
                var pi = points[i];
                var vc = (a - pi).ToVector2();
                //点到直线距离
                double distance = Math.Abs(Vector2.CrossProduct(ab, vc));
                if (distance > dist)
                {
                    dist = distance;
                    next = pi;
                }
            }
            points.Remove(next);
            result.Insert(result.IndexOf(b), next);

            List<Vector2> left = points.Where(p => (next - a).ToVector2().CrossProduct((p - a).ToVector2()) < 0).ToList();
            List<Vector2> right = points.Where(p => (b - next).ToVector2().CrossProduct((p - next).ToVector2()) < 0).ToList();

            QuickHullScanCore(a, next, result, left);
            QuickHullScanCore(next, b, result, right);
        }

    }
}
