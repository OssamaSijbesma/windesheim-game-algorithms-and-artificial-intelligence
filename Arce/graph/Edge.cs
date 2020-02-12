using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.graph
{
    class Edge
    {
        public Node dest;       // Second node in edge
        public double cost;     // Edge cost

        public Edge(Node d, double c)
        {
            dest = d;
            cost = c;
        }
    }
}
