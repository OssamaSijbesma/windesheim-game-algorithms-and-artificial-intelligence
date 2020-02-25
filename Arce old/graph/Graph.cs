using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.graph
{
    class Graph
    {
        public static readonly double INFINITY = System.Double.MaxValue;

        private Dictionary<string, Node> nodeMap;

        public Graph()
        {
            nodeMap = new Dictionary<string, Node>();
        }

        public Node GetVertex(string name)
        {
            Node v;
            if (!nodeMap.ContainsKey(name))
            {
                v = new Node(name);
                nodeMap.Add(name, v);
            }
            else
                v = nodeMap[name];
            return v;
        }

        public void AddEdge(string source, string dest, double cost)
        {
            Node a = nodeMap[source];
            Node b = nodeMap[dest];
            a.adj.Add(new Edge(b, cost));
        }

        public void ClearAll()
        {
            foreach (Node v in nodeMap.Values)
            {
                v.Reset();
            }
        }

        public override string ToString()
        {
            string graphString = "";
            foreach (Node v in nodeMap.Values)
            {
                graphString += v.ToString() + "\n";
            }
            return graphString;
        }

        public void Unweighted(string name)
        {
            ClearAll();

            Node startNode = GetVertex(name);

            Queue<Node> q = new Queue<Node>();
            q.Enqueue(startNode);
            startNode.dist = 0;

            while (q.Count != 0)
            {
                Node v = q.Dequeue();

                foreach (Edge edge in v.adj)
                {
                    Node w = edge.dest;

                    if (w.dist == INFINITY)
                    {
                        w.dist = v.dist + 1;
                        w.prev = v;
                        q.Enqueue(w);
                    }
                }
            }
        }

        //public void Dijkstra(string name)
        //{
        //    ClearAll();

        //    PriorityQueue<Path> pq = new PriorityQueue<Path>();
        //    Vertex startVertex = GetVertex(name);

        //    pq.Add(new Path(startVertex, 0));
        //    startVertex.dist = 0;

        //    int verticesSeen = 0;
        //    while (pq.Size() != 0 && verticesSeen < vertexMap.Count)
        //    {
        //        Path path = pq.Remove();
        //        Vertex v = path.vertex;
        //        if (v.scratch != 0)     // already processed v
        //            continue;

        //        v.scratch = 1;
        //        verticesSeen++;

        //        foreach (Edge edge in v.adj)
        //        {
        //            Vertex w = edge.dest;
        //            double costvw = edge.cost;

        //            if (w.dist > v.dist + costvw)
        //            {
        //                w.dist = v.dist + costvw;
        //                w.prev = v;
        //                pq.Add(new Path(w, w.dist));
        //            }
        //        }
        //    }
        //}
    }
}
