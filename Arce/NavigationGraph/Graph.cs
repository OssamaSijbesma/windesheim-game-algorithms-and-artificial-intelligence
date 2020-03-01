using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.NavigationGraph
{
    class Graph
    {
        public static readonly double INFINITY = System.Double.MaxValue;
        private Dictionary<Vector2, Vertex> vertexMap;

        public Graph(TiledMap map)
        {
            vertexMap = new Dictionary<Vector2, Vertex>();
            // Construct the map with the flood fill algorithm

        }

        public Vertex GetVertex(Vector2 coordinate)
        {
            Vertex vertex;

            // Return a vertex if it's not in the dictonary add one
            if (vertexMap.TryGetValue(coordinate, out vertex))
                return vertex;
            else
            {
                vertex = new Vertex(coordinate);
                vertexMap.Add(coordinate, vertex);
                return vertex;
            }
        }

        public void AddEdge(Vector2 source, Vector2 dest, double cost)
        {
            // Add a new edge to the graph
            Vertex vertex = GetVertex(source);
            Vertex vertex1 = GetVertex(dest);
            vertex.edges.AddLast(new Edge(vertex1, cost));
        }

        public void ClearAll()
        {
            // Clear all vertexes
            foreach (Vertex vertex in vertexMap.Values)
                vertex.Reset();
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
