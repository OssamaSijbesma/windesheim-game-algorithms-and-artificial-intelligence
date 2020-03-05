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
        public double FCost;    // Edge cost
        public double GCost;    // Distance between start and current
        public double HCost;    // Heuristic cost (Manhattan Distance)

        public Edge(Vertex destination, double gCost, double hCost)
        {
            Dest = destination;
            GCost = gCost;
            HCost = hCost;
            FCost = gCost + hCost;
        }

        // Compare function
        public int CompareTo(Edge other)
        {
            double otherFCost = other.FCost;
            double otherHCost = other.HCost;

            if (FCost > otherFCost)
                return 1;
            else if (FCost < otherFCost)
                return -1;
            else
            {
                if (HCost < otherHCost)
                    return -1;
                else
                    return 0;
            }
        }
    }
}
