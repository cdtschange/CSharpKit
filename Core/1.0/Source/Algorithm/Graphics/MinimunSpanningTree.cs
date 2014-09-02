using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Algorithm.Graphics.Tree
{
    /// <summary>
    /// 最小生成树
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MinimunSpanningTree<T, K>
        where T : IComparable
        where K : IConvertible, IComparable
    {
        /// <summary>
        /// Kruskal算法
        /// </summary>
        /// 时间复杂度：O(mlogm) m为边数 = O(n^2logn) n为顶点数
        /// <param name="graphic"></param>
        /// <returns></returns>
        public static Graphic<T, K> Kruskal(Graphic<T, K> graphic)
        {
            Graphic<T, K> newGraphic = new Graphic<T, K>();
            Edge<T, K>[] edges = graphic.Edges.ToArray();
            Sort<Edge<T, K>>.QuickSort(edges, 0, graphic.Edges.Count - 1);

            Dictionary<Vertex<T>, int> smallTrees = new Dictionary<Vertex<T>, int>();

            for (int i = 0; i < graphic.Vertexes.Count; i++)
            {
                smallTrees.Add(graphic.Vertexes[i], i);
            }

            for (int i = 0; i < edges.Length; i++)
            {
                Edge<T, K> edge = edges[i];
                Vertex<T> leftNode = edge.LeftNode;
                Vertex<T> rightNode = edge.RightNode;

                if (smallTrees[leftNode] == smallTrees[rightNode])
                {
                    continue;
                }

                List<Vertex<T>> changedNodes = new List<Vertex<T>>();

                foreach (var kv in smallTrees)
                {
                    if (kv.Value == smallTrees[rightNode])
                    {
                        changedNodes.Add(kv.Key);
                    }
                }

                changedNodes.ForEach(n =>
                {
                    smallTrees[n] = smallTrees[leftNode];
                });

                newGraphic.Edges.Add(edge);
                if (!newGraphic.Vertexes.Contains(leftNode))
                {
                    newGraphic.Vertexes.Add(leftNode);
                }
                if (!newGraphic.Vertexes.Contains(rightNode))
                {
                    newGraphic.Vertexes.Add(rightNode);
                }

            }
            return newGraphic;
        }

        /// <summary>
        /// Prim算法
        /// </summary>
        /// 时间复杂度：O(n^2) n为顶点数
        /// <param name="graphic"></param>
        /// <returns></returns>
        public static Graphic<T, K> Prim(Graphic<T, K> graphic)
        {
            Graphic<T, K> newGraphic = new Graphic<T, K>();

            Dictionary<Vertex<T>, Edge<T, K>> nodeEdges = new Dictionary<Vertex<T>, Edge<T, K>>();

            if (graphic.Vertexes.Count == 0)
            {
                return newGraphic;
            }

            Vertex<T> node = graphic.Vertexes[0];
            newGraphic.Vertexes.Add(node);

            graphic.GetEdgesByNode(node).ForEach(e =>
            {
                var n = e.LeftNode != node ? e.LeftNode : e.RightNode != node ? e.RightNode : null;
                nodeEdges.Add(n, e);
            });

            while (newGraphic.Vertexes.Count < graphic.Vertexes.Count)
            {
                var newKV = nodeEdges.OrderBy(kv => (kv.Value as Edge<T, K>).Weight).FirstOrDefault();
                newGraphic.Vertexes.Add(newKV.Key);
                newGraphic.Edges.Add(newKV.Value);

                nodeEdges.Remove(newKV.Key);

                graphic.GetEdgesByNode(newKV.Key).ForEach(e =>
                {

                    var outNode = !newGraphic.Vertexes.Contains(e.LeftNode) ? e.LeftNode : !newGraphic.Vertexes.Contains(e.RightNode) ? e.RightNode : null;
                    if (outNode == null)
                    {
                        return;
                    }
                    if (nodeEdges.ContainsKey(outNode))
                    {
                        if (nodeEdges[outNode] > e)
                        {
                            nodeEdges[outNode] = e;
                        }
                    }
                    else
                    {
                        nodeEdges.Add(outNode, e);
                    }
                });
            }

            return newGraphic;
        }

    }
}
