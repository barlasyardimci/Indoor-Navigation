using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary;

namespace FakeGraphTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            
            SimpleDirectedGraph<string, string> graph = new SimpleDirectedGraph<string, string>();
            graph.AddVertex(new[] { "SOSQR-1", "SOSQR-2", "SOSQR-3", "SOSQR-4", "SOSQR-5" });

            graph.AddEdge("SOSQR-1", "SOSQR-2", "Path1");
            graph.AddEdge("SOSQR-1", "SOSQR-3", "Path2");

            graph.AddEdge("SOSQR-2", "SOSQR-5", "Path4");

            graph.AddEdge("SOSQR-3", "SOSQR-4", "Path3");

            graph.AddEdge("SOSQR-5", "SOSQR-4", "Path5");

            foreach (string vertex in graph.GetVertexSet())
            {
                Console.WriteLine(vertex);
            }
            foreach (var edge in graph.GetEdgeSet())
            {
                //Edge second method gets the end of the edge as the final path on the edge
                //Edge first gets the starting point of Edge
                Console.WriteLine(edge.GetFirst() + " -> " + edge.GetSecond());
               
            }
            Console.ReadLine();
        }
    }
}