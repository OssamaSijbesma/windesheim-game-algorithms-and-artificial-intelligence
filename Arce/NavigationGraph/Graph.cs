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
                    AddEdge(coordinate, ru, 2);
                if (vertexMap.ContainsKey(rd))
                    AddEdge(coordinate, rd, 2);
                if (vertexMap.ContainsKey(d))
                    AddEdge(coordinate, d, 1);
            }

            Dijkstra(new Vector2(136, 136), new Vector2(136, 280));
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

        // Dijkstra algorithm
        public LinkedList<Vertex> Dijkstra(Vector2 Start, Vector2 Target)
        {
            ClearAll();

            // Register the startpoint of the algorithm
            Vertex start;
            if (!vertexMap.TryGetValue(Start, out start))
                throw new System.Exception();

            // Create a priority queue
            PriorityQueue<Edge> priorityQueue = new PriorityQueue<Edge>();
            priorityQueue.Add(new Edge(start, 0));
            start.dist = 0;

            // Amount of nodes seen
            int nodesSeen = 0;

            // Continue while the priority queue still has items and if not all vertexes are seen.
            while (priorityQueue.Size() > 0 && nodesSeen < vertexMap.Count)
            {
                // Get the vertex with the shortest path
                Edge path = priorityQueue.Remove();
                Vertex vertex = path.Dest;

                if (vertex.scratch != 0)
                    continue;

                // Scratch vertex
                vertex.scratch = 1;
                nodesSeen++;

                if (vertex.coordinate == Target)
                {
                    LinkedList<Vertex> vertices = new LinkedList<Vertex>();
                    Vertex verti = vertex;
                    vertices.AddLast(verti);
                    while (verti.prev != null)
                    {
                        verti.red = true;
                        vertices.AddLast(verti.prev);
                        verti = verti.prev;
                    }

                    return vertices;
                }

                // Foreach all edges en set the distance of those nodes
                foreach (Edge edge in vertex.edges)
                {
                    Vertex destVertex = edge.Dest;
                    double edgeCost = edge.Cost;



                    if (edgeCost < 0)
                        throw new System.Exception();

                    // Check if the distance is shorter
                    if (destVertex.dist > vertex.dist + edgeCost)
                    {
                        // Set the distance and the vertex where it came from
                        destVertex.dist = vertex.dist + edgeCost;
                        destVertex.prev = vertex;
                        // Add vertex to the priority queue
                        priorityQueue.Add(new Edge(destVertex, destVertex.dist));
                    }
                }
            }
            return default;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            // Draw the edges of the vertex
            foreach (Vertex vertex in vertexMap.Values)
                foreach (Edge edge in vertex.edges)
                    if (vertex.red == true && edge.Dest.red == true)
                        spriteBatch.DrawLine(vertex.coordinate, edge.Dest.coordinate, Color.Red);
                    else if (edge.Dest.scratch != 0)
                        spriteBatch.DrawLine(vertex.coordinate, edge.Dest.coordinate, Color.Yellow);
                    else
                        spriteBatch.DrawLine(vertex.coordinate, edge.Dest.coordinate, Color.ForestGreen);

            // Draw vertex
            foreach (Vertex vertex in vertexMap.Values)
                if (vertex.red == true)
                    spriteBatch.DrawCircle(vertex.coordinate, 3F, 12, Color.Red, 3F);
                else if(vertex.scratch != 0)
                    spriteBatch.DrawCircle(vertex.coordinate, 2F, 12, Color.Yellow, 3F);
                else
                    spriteBatch.DrawCircle(vertex.coordinate, 2F, 12, Color.ForestGreen, 3F);

            spriteBatch.End();
        }
    }
}
