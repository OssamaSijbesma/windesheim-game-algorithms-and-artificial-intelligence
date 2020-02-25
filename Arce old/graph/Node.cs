using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.graph
{
    class Node
    {
        public string name;         // Node name
        public List<Edge> adj;      // Adjacent nodes
        public double dist;         // Cost
        public Node prev;           // Previous node on shortest path
        public int scratch;         // Extra variable used in shortest path algorithm

        public Node(string name)
        {
            this.name = name;
            adj = new List<Edge>();
            Reset();
        }

        public void Reset()
        {
            dist = Graph.INFINITY;
            prev = null;
            scratch = 0;
        }

        public override string ToString()
        {
            string nodeString = name;
            if (dist != Graph.INFINITY)
            {
                nodeString += "(" + dist + ")";
            }
            if (adj.Count != 0)
            {
                nodeString += " [ ";
                foreach (var item in adj)
                {
                    nodeString += item.dest.name;
                    nodeString += "(" + item.cost + ") ";
                }
                nodeString += "]";
            }
            return nodeString;
        }
    }
}
