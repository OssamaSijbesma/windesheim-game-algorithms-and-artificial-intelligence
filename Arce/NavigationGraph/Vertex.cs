using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.NavigationGraph
{
    class Vertex
    {
        public Vector2 coordinate;      // Vertex coordinate
        public LinkedList<Edge> edges;  // Adjacent vertices
        public double dist;             // Cost to get to this vertex with the last used algorithm
        public Vertex prev;             // Previous vertex on shortest path algorithm
        public int scratch;             // Extra variable used in algorithms
        public bool red;

        public Vertex(Vector2 coordinate)
        {
            this.edges = new LinkedList<Edge>();
            this.coordinate = coordinate;
            Reset();
        }

        // Reset the vertex
        public void Reset()
        {
            dist = Graph.INFINITY;
            prev = null;
            scratch = 0;
        }
    }
}
