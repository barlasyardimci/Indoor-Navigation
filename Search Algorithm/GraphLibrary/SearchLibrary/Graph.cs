using System;
using System.Collections.Generic;
using System.Text;

namespace SearchLibrary
{
    public class Graph
    {
        private List<Node> VertexList = new List<Node>();
        private List<Edge> EdgeList = new List<Edge>();
        private Dictionary<string, List<Node>> globalRoomList = new Dictionary<string, List<Node>>();
        private int[,] graphMatrix;
        private Dictionary<int, Node> reverseUniqueNodeId = new Dictionary<int, Node>();
        private Dictionary<Node, int> uniqueNodeId = new Dictionary<Node, int>();


        public Graph(List<Node> vertexList, List<Edge> edgeList, Dictionary<string, List<Node>> globalRoomList)
        {
            this.VertexList = vertexList;
            this.EdgeList = edgeList;
            this.globalRoomList = globalRoomList;
            this.graphMatrix = graphToMatrix();
        }
        public List<Node> getVertexList()
        {
            return this.VertexList;
        }
        public List<Edge> getEdgeList()
        {
            return this.EdgeList;
        }
        public Dictionary<string,List<Node>> getGlobalRoomList()
        {
            return this.globalRoomList;
        }
        public Dictionary<Node, int> getUniqueNodeId()
        {
            return this.uniqueNodeId;
        }
        public Dictionary<int, Node> getReverseUniqueNodeId()
        {
            return this.reverseUniqueNodeId;
        }
        public int[,] getGraphMatrix()
        {
            return this.graphMatrix;
        }
        /// <summary>
        /// An Integer Matrix Representing the Graph in Nodes and Weights 
        /// </summary>
        /// <returns>2D Integer Array</returns>
        public int[,] graphToMatrix()
        {
            int [,] computeGraphMatrix = new int[VertexList.Count, VertexList.Count];
            int val = 0;

            /* Assign Unique Integer Values to the Node IDs */
            foreach(Node vertex in VertexList)
            {
                if (!this.uniqueNodeId.ContainsKey(vertex))
                {
                    this.uniqueNodeId.Add(vertex,val);
                    this.reverseUniqueNodeId.Add(val, vertex);
                }
                val++;
            }

            foreach (Node node in VertexList)
            {
                Node currentNode = node;
                int currentNodeIdToInt = uniqueNodeId[currentNode];

                foreach (Edge edge in currentNode.getEdgeList())
                {
                    Node targetNode = edge.getTarget();
                    int targetNodeIdToInt = uniqueNodeId[targetNode];

                    computeGraphMatrix[currentNodeIdToInt, targetNodeIdToInt] = edge.getWeigth();
                    // A ---- B ---- C
                }
            }
            /*
             a b c d e f
          a  0 0 0 0 0 0 
          b  2 0 0 0 0 0 
          c  5 0 0 0 0 0 
          d  7 0 0 0 0 0 
          e  0 0 0 0 0 0 
          f  0 0 0 0 0 0 

          */
            return computeGraphMatrix;
        }
         
        public string getEdgeCommand(Node nodeStart, Node nodeEnd)
        {
            string command = "";
            foreach(Edge edge in this.EdgeList)
            {
                if (edge.getSource() == nodeStart && edge.getTarget() == nodeEnd)
                {
                    command = edge.getNavCommand();
                    break;
                }
            }


            return command;
        }
    }
}
