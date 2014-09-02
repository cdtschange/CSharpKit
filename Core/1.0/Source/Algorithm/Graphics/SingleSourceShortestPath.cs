using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Algorithm.Graphics.Tree
{
    /// <summary>
    /// 单源最短路径
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="K"></typeparam>
    public class SingleSourceShortestPath<T, K>
        where T : IComparable
        where K : IConvertible, IComparable
    {
        /// <summary>
        /// Dijkstra算法
        /// </summary>
        /// 时间复杂度：O(m) m为边数 = O(n^2) n为顶点数
        /// <param name="graphic"></param>
        /// <param name="root"></param>
        /// <param name="nodeEdges"></param>
        /// <returns></returns>
        public static Graphic<T, K> Dijkstra(Graphic<T, K> graphic, Vertex<T> root, out Dictionary<Vertex<T>, List<Edge<T, K>>> paths)
        {
            Graphic<T, K> newGraphic = new Graphic<T, K>();

            Dictionary<Vertex<T>, List<Edge<T, K>>> nodeEdges = new Dictionary<Vertex<T>, List<Edge<T, K>>>();
            Dictionary<Vertex<T>, List<Edge<T, K>>> gpaths = new Dictionary<Vertex<T>, List<Edge<T, K>>>();
            paths = new Dictionary<Vertex<T>, List<Edge<T, K>>>();

            if (graphic.Vertexes.Count == 0)
            {
                return newGraphic;
            }
            if (!graphic.Vertexes.Contains(root))
            {
                throw new InvalidOperationException("Root vertex is not in graphic!");
            }
            newGraphic.Vertexes.Add(root);

            graphic.GetEdgesByNode(root).ForEach(e =>
            {
                var n = e.LeftNode != root ? e.LeftNode : e.RightNode != root ? e.RightNode : null;
                nodeEdges.Add(n, new List<Edge<T, K>>() { e });
            });

            while (newGraphic.Vertexes.Count < graphic.Vertexes.Count)
            {
                var newKV = nodeEdges.OrderBy(kv => (kv.Value as List<Edge<T, K>>).Sum(e => Convert.ToDouble(e.Weight))).FirstOrDefault();
                newGraphic.Vertexes.Add(newKV.Key);
                newGraphic.Edges.Add(newKV.Value[newKV.Value.Count - 1]);

                nodeEdges.Remove(newKV.Key);
                gpaths.Add(newKV.Key, newKV.Value);

                graphic.GetEdgesByNode(newKV.Key).ForEach(e =>
                {

                    var outNode = !newGraphic.Vertexes.Contains(e.LeftNode) ? e.LeftNode : !newGraphic.Vertexes.Contains(e.RightNode) ? e.RightNode : null;
                    if (outNode == null)
                    {
                        return;
                    }
                    var tempList = new List<Edge<T, K>>();
                    tempList.AddRange(gpaths[newKV.Key]);
                    tempList.Add(e);
                    if (nodeEdges.ContainsKey(outNode))
                    {
                        if (nodeEdges[outNode].Sum(ed => Convert.ToDouble(ed.Weight)) > tempList.Sum(ed => Convert.ToDouble(ed.Weight)))
                        {
                            nodeEdges[outNode] = tempList;
                        }
                    }
                    else
                    {                     
                        nodeEdges[outNode] = tempList;
                    }
                });
            }
            paths = gpaths;

            return newGraphic;
        }
    }
}
