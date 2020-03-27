using System;
using System.Collections.Generic;
using System.Text;

namespace SearchLibrary
{
    public class Edge
    {
        private Node source;
        private Node target;
        private int weight;
        private string navCommand;
        public Edge(Node source, Node target, int weight, string navCommand)
        {
            this.target = target;
            this.source = source;
            this.weight = weight;
            this.navCommand = navCommand;
        }

        public Node getSource()
        {
            return this.source;
        }
        public Node getTarget()
        {
            return this.target;
        }
        public int getWeigth()
        {
            return this.weight;
        }
        public string getNavCommand()
        {
            return this.navCommand;
        }
    }
}
 
