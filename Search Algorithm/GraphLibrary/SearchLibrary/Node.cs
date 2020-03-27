using System;
using System.Collections.Generic;
using System.Text;

namespace SearchLibrary
{
    public class Node
    {
        private List<string> roomList = new List<string>();
        private List<Edge> edgeList = new List<Edge>();
        private string id;
        /// <summary>
        /// Her node Komşularıyla arasında olan odaları tutsun
        /// Global bi roomList asıl programın çalıştığı; bu da bir Hashmap olacak [keyler sınıflar olacak, value'lar hangi iki Node'e ait olduğu]
        /// 2 taraf için de search çalıştırmak TMS'e mail (optional)
        /// </summary>
        /// <param name=""></param>
        /// <param name="rooms"></param
        /// 

        public Node(string id, List<string> roomList)
        {
            this.id = id;
            this.roomList = roomList;
        }
        public string getId()
        {
            return this.id;
        }
        public List<Edge> getEdgeList()
        {
            return this.edgeList;
        }
        public void setEdgeList(List<Edge> edgeList)
        {
            this.edgeList = edgeList;
        }
        public List<string> getRoomList()
        {
            return this.roomList;
        }
    }
}
