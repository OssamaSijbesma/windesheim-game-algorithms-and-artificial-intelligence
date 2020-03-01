using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
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

            // Construct the vertexes based off the map
            for (int counter = 0, y = 0; y < map.Height; y++)
                for (int x = 0; x < map.Width; x++, counter ++)
                    if(map.TileLayers[0].Tiles[counter].GlobalIdentifier == 8 && map.TileLayers[1].Tiles[counter].GlobalIdentifier == 0)
                        GetVertex(new Vector2(x * 16 + 8, y * 16 + 8));

            // Generate the edges
            foreach (Vertex vertex in vertexMap.Values)
            {
                Vector2 coordinate = vertex.coordinate;
                Vector2 r = new Vector2(coordinate.X + 16F, coordinate.Y);
                Vector2 ru = new Vector2(coordinate.X + 16F, coordinate.Y - 16F);
                Vector2 rd = new Vector2(coordinate.X + 16F, coordinate.Y + 16F);
                Vector2 d = new Vector2(coordinate.X, coordinate.Y + 16F);

                if (vertexMap.ContainsKey(r))
                    AddEdge(coordinate, r, 1);
                if (vertexMap.ContainsKey(ru))
                    AddEdge(coordinate, ru, 1);
                if (vertexMap.ContainsKey(rd))
                    AddEdge(coordinate, rd, 1);
                if (vertexMap.ContainsKey(d))
                    AddEdge(coordinate, d, 1);
            }
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
            vertex1.edges.AddLast(new Edge(vertex, cost));
        }

        public void ClearAll()
        {
            // Clear all vertexes
            foreach (Vertex vertex in vertexMap.Values)
                vertex.Reset();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Vertex vertex in vertexMap.Values)
            {
                // Draw vertex
                spriteBatch.DrawCircle(vertex.coordinate, 3F, 12, Color.LightGray, 3F);

                // Draw the edges of the vertex
                foreach (Edge edge in vertex.edges)
                    spriteBatch.DrawLine(vertex.coordinate, edge.Dest.coordinate, Color.LightGray);
            }

        }
    }
}
