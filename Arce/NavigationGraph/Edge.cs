using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.NavigationGraph
{
    class Edge : IComparable<Edge>
    {
        public Vertex Dest;     // Second node in edge
        public double Cost;     // Edge cost

        public Edge(Vertex destination, double cost)
        {
            Dest = destination;
            Cost = cost;
        }
        // Compare function

        public int CompareTo(Edge other)
        {
            double otherCost = other.Cost;
            return Cost < otherCost ? -1 : Cost > otherCost ? 1 : 0;
        }
    }
}
