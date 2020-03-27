using System;
using SearchLibrary;
using System.Collections.Generic;


namespace Search
{
    public class SearchAlgorithm
    {
        public SearchAlgorithm()
        {

        }
        protected int minDistance(int[] dist, bool[] sptSet, int N)
        {
            // Initialize min value 
            int min = int.MaxValue, min_index = -1;

            for (int v = 0; v < N; v++)
                if (sptSet[v] == false && dist[v] <= min)
                {
                    min = dist[v];
                    min_index = v;
                }

            return min_index;
        }

        public List<Node> dijkstra(Graph graph, Node startNode, List<Node> nodeList)
        {
            int[] endVertex = new int[2];
            Console.WriteLine("Start Node: " + startNode.getId());
            Console.WriteLine("Node id 0 "+nodeList[0].getId());
            Console.WriteLine("Node id 1 " + nodeList[1].getId());

            endVertex[0] = graph.getUniqueNodeId()[nodeList[0]];
            endVertex[1] = graph.getUniqueNodeId()[nodeList[1]];
            int startVertex = graph.getUniqueNodeId()[startNode];
            Console.WriteLine("Start vertex: " + startVertex);

            int[,] adjacencyMatrix = graph.getGraphMatrix();
            Console.WriteLine("Adjacency");
            for(int i = 0; i< adjacencyMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < adjacencyMatrix.GetLength(0); j++)
                {
                    Console.Write(adjacencyMatrix[i,j]);
                    Console.Write(" ");


                }
                Console.WriteLine(" ");
            }

            int nVertices = adjacencyMatrix.GetLength(0);
            List<int> path = new List<int>();
            // shortestDistances[i] will hold the  
            // shortest distance from src to i  
            int[] shortestDistances = new int[nVertices];

            // added[i] will true if vertex i is  
            // included / in shortest path tree  
            // or shortest distance from src to  
            // i is finalized  
            bool[] added = new bool[nVertices];

            // Initialize all distances as  
            // INFINITE and added[] as false  
            for (int vertexIndex = 0; vertexIndex < nVertices;
                                                vertexIndex++)
            {
                shortestDistances[vertexIndex] = int.MaxValue;
                added[vertexIndex] = false;
            }

            // Distance of source vertex from  
            // itself is always 0  
            shortestDistances[startVertex] = 0;

            // Parent array to store shortest  
            // path tree  
            int[] parents = new int[nVertices];

            // The starting vertex does not  
            // have a parent  
            parents[startVertex] = -1;

            // Find shortest path for all  
            // vertices  
            for (int i = 1; i < nVertices; i++)
            {

                // Pick the minimum distance vertex  
                // from the set of vertices not yet  
                // processed. nearestVertex is  
                // always equal to startNode in  
                // first iteration.  
                int nearestVertex = -1;
                int shortestDistance = int.MaxValue;
                for (int vertexIndex = 0;
                        vertexIndex < nVertices;
                        vertexIndex++)
                {
                    if (!added[vertexIndex] &&
                        shortestDistances[vertexIndex] <
                        shortestDistance)
                    {
                        nearestVertex = vertexIndex;
                        shortestDistance = shortestDistances[vertexIndex];
                    }
                }

                // Mark the picked vertex as  
                // processed  
                added[nearestVertex] = true;

                // Update dist value of the  
                // adjacent vertices of the  
                // picked vertex.  
                for (int vertexIndex = 0;
                        vertexIndex < nVertices;
                        vertexIndex++)
                {
                    int edgeDistance = adjacencyMatrix[nearestVertex, vertexIndex];

                    if (edgeDistance > 0
                        && ((shortestDistance + edgeDistance) <
                            shortestDistances[vertexIndex]))
                    {
                        parents[vertexIndex] = nearestVertex;
                        shortestDistances[vertexIndex] = shortestDistance +
                                                        edgeDistance;
                    }
                }
            }
            Console.WriteLine("Endvertex0: " + shortestDistances[endVertex[0]]);
            Console.WriteLine("Endvertex1: " + shortestDistances[endVertex[1]]);
            if (shortestDistances[endVertex[0]] < shortestDistances[endVertex[1]])
            {
                path = constructPath(startVertex, endVertex[0], parents);

            }
            else
            {
                path = constructPath(startVertex, endVertex[1], parents);
                foreach(int i in path) {
                    Console.WriteLine("path indices" +
                        ":  " + i);
                }

            }

            List<Node> retList = shortestVertices(path, graph);

            foreach (Node node in retList)
            {
                Console.WriteLine(node.getId());

            }
            
            return shortestVertices(path,graph);
        }
    
        public List<Node> shortestVertices(List<int> path,Graph graph)
        {
            List<Node> shortestVertices = new List<Node>();
            Dictionary<int,Node> graphDict = graph.getReverseUniqueNodeId();

            foreach(int i in path)
            {
                shortestVertices.Add(graphDict[i]);
            }

            return shortestVertices;
        }
        public List<int> constructPath(int start, int end, int[] parent)
        {
            List<int> shortestPath = new List<int>();

            while (end != start)
            {
                shortestPath.Add(end);
                end = parent[end];
            }
            /* a b c d e f g h*/
            shortestPath.Add(start);
            shortestPath.Reverse();

            return shortestPath;
        }

        public string[] pathToString(List<Node> shortestPath, Graph graph)
        {

            int length = (int) Math.Ceiling( (double) shortestPath.Count /2);
            string[] pathArray = new string[length];

            for(int i = 0; i<shortestPath.Count;i++)
            {
                if (i + 1 == shortestPath.Count)
                {
                    break;
                }
                else
                {
                    Node node1 = shortestPath[i];
                    Node node2 = shortestPath[i + 1];
                    pathArray[i] = graph.getEdgeCommand(node1, node2);
                }

            }
            return pathArray;
        }
    }
}

