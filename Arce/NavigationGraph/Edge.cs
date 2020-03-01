using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.NavigationGraph
{
    class Edge
    {
        public Vertex Dest;     // Second node in edge
        public double Cost;     // Edge cost

        public Edge(Vertex destination, double cost)
        {
            Dest = destination;
            Cost = cost;
        }
    }
}
