using Proyecto26;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleJSON;
using SearchLibrary;
using System.Collections.Generic;
using UnityEngine.Rendering;
using System.Linq;

public class Maps : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region UI Buttons

   
    public void ClickBack()
    {

        string mapName = CurrentMap.currentMapName;


        RestClient.Get("https://indoor-navigation-bf70e.firebaseio.com/Maps/" + mapName + ".json").Then(response => {
            Debug.Log("Response" + response.Text + "Ok");
            var N = JSON.Parse(response.Text);
            Dictionary<string, List<Node>> globalRoomList = new Dictionary<string, List<Node>>();
            Dictionary<string, Node> QRToNode = new Dictionary<string, Node>();
            List<Edge> allEdgeList = new List<Edge>();
            foreach (string node in N["Nodes"].Keys)
            {
                var nodeContent = N["Nodes"][node];
                string QRID = nodeContent["QRID"];
                List<string> nodeRoomList = new List<string>();
                foreach(string key in N["GlobalRoomList"].Keys)
                {
                    string QRs = N["GlobalRoomList"][key];
                    if (QRs.Split(',').Contains(QRID)){
                        nodeRoomList.Add(key);
                    }
                }
                QRToNode.Add(QRID, new Node(QRID, nodeRoomList));
            }
            foreach (string node in N["Nodes"].Keys)
            {

                var nodeContent = N["Nodes"][node];
                List<Edge> edgeList = new List<Edge>();
                foreach (string edge in nodeContent["EDGES"].Keys){
                    Debug.Log(""+edge);

                    Node source =  QRToNode[nodeContent["EDGES"][edge]["Source"]];

                    Node target=  QRToNode[nodeContent["EDGES"][edge]["Target"]];
                    int weight=(int)nodeContent["EDGES"][edge]["Weight"];
                    string navCommand =nodeContent["EDGES"][edge]["navCommand"];
                    Edge currentEdge = new Edge(source, target, weight, navCommand);
                    edgeList.Add(currentEdge);
                    allEdgeList.Add(currentEdge);
                    Debug.Log("Here1.3");
                }
                QRToNode[nodeContent["QRID"]].setEdgeList(edgeList);

            }
            List<Node> nodeList = new List<Node>();
            foreach (Node node in QRToNode.Values)
            {
                nodeList.Add(node);
            }
            foreach (string room in N["GlobalRoomList"].Keys)
            {
                List<Node> tempNodeList = new List<Node>();
                string nodes = N["GlobalRoomList"][room];
                string ids1 = nodes.Split(',')[0];
                string ids2 = nodes.Split(',')[1];
                tempNodeList.Add(QRToNode[ids1]);
                tempNodeList.Add(QRToNode[ids2]);

                globalRoomList.Add(room, tempNodeList);
            }
            Graph graph = new Graph(nodeList, allEdgeList, globalRoomList);
            CurrentMap.currentMapGraph = graph;
            SceneManager.LoadScene("Menu");

        });








    }

    public void RetrieveMap()
    {
    }


    #endregion
}
