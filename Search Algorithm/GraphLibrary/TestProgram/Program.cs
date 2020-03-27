using System;
using GraphLibrary;
using SearchLibrary;
using System.Collections.Generic;
using Search;


namespace TestProgram
{
    class Program
    {
        static void Main(string[] args)
        {



            List<string> roomList1 = new List<string>()
            {
                "Caseb10",
                "Caseb20",
                "Sosb10",
                "Sosb20",
                "scib10"
            };
            List<string> roomList2 = new List<string>()
            {
                "Caseb10",
                "Sosb10",
                "Sosb20",
                "engb20",
                "Caseb20"

            };
            List<string> roomList3 = new List<string>()
            {
                "engb10",
                "engb20",
                "engz50",
                "scib20"
                
            };
            List<string> roomList4 = new List<string>()
            {
                "scib10",
                "scib20",
                "engz50",
                "engb10"

            };

            Node node1 = new Node("QR1", roomList1);
            Node node2 = new Node("QR2", roomList2);
            Node node3 = new Node("QR3", roomList3);
            Node node4 = new Node("QR4", roomList4);
           
            List<Node> sosb10NodeList = new List<Node>();
            sosb10NodeList.Add(node1);
            sosb10NodeList.Add(node2);
            
            List<Node> sosb20NodeList = new List<Node>();
            sosb20NodeList.Add(node1);
            sosb20NodeList.Add(node2);

            List<Node> caseb10NodeList = new List<Node>();
            caseb10NodeList.Add(node1);
            caseb10NodeList.Add(node2);

            List<Node> caseb20NodeList = new List<Node>();
            caseb20NodeList.Add(node1);
            caseb20NodeList.Add(node2);

            List<Node> engz50NodeList = new List<Node>();
            engz50NodeList.Add(node3);
            engz50NodeList.Add(node4);

            List<Node> engb20NodeList = new List<Node>();
            engb20NodeList.Add(node2);
            engb20NodeList.Add(node3);

            List<Node> engb10NodeList = new List<Node>();
            engb10NodeList.Add(node3);
            engb10NodeList.Add(node4);

            List<Node> scib10NodeList = new List<Node>();
            scib10NodeList.Add(node1);
            scib10NodeList.Add(node4);

            List<Node> scib20NodeList = new List<Node>();
            scib20NodeList.Add(node1);
            scib20NodeList.Add(node4);


            Dictionary<string, List<Node>> globalRoomList = new Dictionary<string, List<Node>>()
            {
                {"Sosb10", sosb10NodeList},
                {"Sosb20", sosb20NodeList},

                {"caseb10", caseb10NodeList},
                {"caseb20", caseb20NodeList},

                {"engz50", engz50NodeList},
                {"engb20", engb20NodeList},
                {"engb10", engb10NodeList},

                {"scib10", scib10NodeList},
                {"scib20", scib20NodeList},

            };

            Edge edge1_2 = new Edge(node1, node2, 5, "Walk Straight");
            Edge edge1_3 = new Edge(node1, node3, 7, "Walk Left");
            Edge edge1_4 = new Edge(node1, node4, 10, "Walk Right");

            Edge edge2_1 = new Edge(node2, node1, 5, "Walk Straight");
            Edge edge2_3 = new Edge(node2, node3, 3, "Walk Left");

            Edge edge3_1 = new Edge(node3, node1, 7, "Walk Right");
            Edge edge3_2 = new Edge(node3, node2, 3, "Walk Straight");
            Edge edge3_4 = new Edge(node3, node4, 15, "Walk Right");

            Edge edge4_1 = new Edge(node4, node1, 10, "Walk Straight");
            Edge edge4_3 = new Edge(node4, node3, 15, "Walk Right");

            List<Edge> edgeList1 = new List<Edge>()
            {
                edge1_2,
                edge1_3,
                edge1_4
            };
            List<Edge> edgeList2 = new List<Edge>()
            {
                 edge2_1,
                 edge2_3
            };
            List<Edge> edgeList3 = new List<Edge>()
            {
                 edge3_1,
                 edge3_2,
                 edge3_4
            };
            List<Edge> edgeList4 = new List<Edge>()
            {
                 edge4_1,
                 edge4_3
            };

            node1.setEdgeList(edgeList1);
            node2.setEdgeList(edgeList2);
            node3.setEdgeList(edgeList3);
            node4.setEdgeList(edgeList4);

            List<Node> vertexList = new List<Node>();
            vertexList.Add(node1);
            vertexList.Add(node2);
            vertexList.Add(node3);
            vertexList.Add(node4);

            List<Edge> allEdgesList = new List<Edge>()
            {
                edge1_2,
                edge1_3,
                edge1_4,
                edge2_1,
                edge2_3,
                edge3_1,
                edge3_2,
                edge3_4,
                edge4_1,
                edge4_3
            };


            Graph graph = new Graph(vertexList, allEdgesList,globalRoomList);
            SearchAlgorithm search = new SearchAlgorithm();

            List<Node> shortest = search.dijkstra(graph, node1, graph.getGlobalRoomList()["engb20"]);

            Console.WriteLine(search.pathToString(shortest,graph)[0]);
           

        }
    }
}
