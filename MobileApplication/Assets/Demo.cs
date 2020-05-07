using BarcodeScanner;
using BarcodeScanner.Scanner;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Wizcorp.Utils.Logger;
using Search;
using SearchLibrary;
using JetBrains.Annotations;

public class Demo : MonoBehaviour
{
    private Node currentNode = null;
    private Graph graph;
    private int currentPathIndex = 0;
    private IScanner BarcodeScanner;
    public Text TextHeader;
    public RawImage Image;
    private List<Node> shortestPath = new List<Node>();
    private string room;
    public Canvas PopupMapSelect;
    public Canvas CameraCanvas;
    public Canvas PopupRoomSelect;
    public static String arrowDirection;
    public static Boolean isFinalDirection;
    void Start()
    {
        PopupMapSelect.enabled = false;
        CameraCanvas.enabled = false;
        PopupRoomSelect.enabled = false;
        if (CurrentMap.currentMapName == " ")
        {
            PopupMapSelect.enabled = true;

        }
        else if(CurrentMap.targetRoomName == " ")
        {
            PopupRoomSelect.enabled = true;
        }else if(CurrentMap.targetRoomName != " ")
        {
            CameraCanvas.enabled = true;

        }

        BarcodeScanner = new Scanner();
        BarcodeScanner.Camera.Play();
        

       


        BarcodeScanner.OnReady += (sender, arg) => {
 
            Image.transform.localEulerAngles = BarcodeScanner.Camera.GetEulerAngles();
            Image.transform.localScale = BarcodeScanner.Camera.GetScale();
            Image.texture = BarcodeScanner.Camera.Texture;

     
            var rect = Image.GetComponent<RectTransform>();
            var newHeight = rect.sizeDelta.x * BarcodeScanner.Camera.Height / BarcodeScanner.Camera.Width;
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, newHeight);
        };


        BarcodeScanner.StatusChanged += (sender, arg) => {
            TextHeader.text = "Status: " + BarcodeScanner.Status;
        };
    }


    void Update()
    {
        if (BarcodeScanner == null)
        {
            return;
        }
        BarcodeScanner.Update();
    }

    #region UI Buttons

    public void ClickScan()
    {
        if (BarcodeScanner == null)
        {
            Log.Warning("No valid camera - Click Start");
            return;
        }

        BarcodeScanner.Scan((barCodeType, barCodeValue) => {
            BarcodeScanner.Stop();
            List<string> returnList = navigate(CurrentMap.currentMapGraph, currentNode, CurrentMap.targetRoomName, barCodeValue);
            string navCommand = returnList[0];
            string isFinished = returnList[1];
            if (isFinished == "Finished")
            {
                isFinalDirection = true;
                Debug.Log("Navigation commmand is "+navCommand+"the route is finished you can see your destination");
            }
            else
            {
                isFinalDirection = false; 
                Debug.Log("Navigation commmand is " + navCommand);

            }

            arrowDirection = navCommand;
            SceneManager.LoadScene("Arrow");

            TextHeader.text = "Output: " +barCodeValue;
            Debug.Log(barCodeValue);

        });
    }

    public void ClickPause()
    {
        if (BarcodeScanner == null)
        {
            Log.Warning("No valid camera - Click Stop");
            return;
        }


        BarcodeScanner.Stop();
    }

    public void ClickBack()
    {

        StartCoroutine(StopCamera(() => {
            SceneManager.LoadScene("Menu");
        }));
    }

    public IEnumerator StopCamera(Action callback)
    {
    
        Image = null;
        BarcodeScanner.Destroy();
        BarcodeScanner = null;

        yield return new WaitForSeconds(0.1f);

        callback.Invoke();
    }
    private List<string> navigate(Graph graph, Node currentNode,string room,string barCodeValue)
    {
        SearchAlgorithm search = new SearchAlgorithm();
        Boolean finished = false; 
        string navigationCommand = "";
        List<Node> targetNodes = graph.getGlobalRoomList()[room];
        if (currentNode == null)// first time scanning a qr 
        {
            currentNode = graph.getIdToNode()[barCodeValue];
           
         
            shortestPath = search.dijkstra(graph, currentNode, targetNodes);
            //we are already there
            if (shortestPath.Count == 1)
            {
                finished = true;
                Node endNode = targetNodes[0];
                if (targetNodes[0] == currentNode)
                {
                    endNode = targetNodes[1];
                }
                navigationCommand = graph.getEdgeCommand(currentNode, endNode);//get the direction command to the room
            }
            else
            {
                navigationCommand = graph.getEdgeCommand(shortestPath[0], shortestPath[1]);//get the direction command between current QR and the next QR in the shortest path
            }
        }
        else
        {
            shortestPath.Remove(currentNode);//we have passed the previous QR so we can pop it out the list 
            currentNode = graph.getIdToNode()[barCodeValue];// get the new Node 
            if (currentNode == shortestPath[0])// we are in the right direction
            {

                if (shortestPath.Count == 1)// we have arrived 
                {
                    finished = true;
                    Node endNode = targetNodes[0];
                    if (targetNodes[0] == currentNode)
                    {
                        endNode = targetNodes[1];
                    }
                    navigationCommand = graph.getEdgeCommand(currentNode, endNode);// get the direction command to the room
                }
                else
                {
                    navigationCommand = graph.getEdgeCommand(shortestPath[0], shortestPath[1]);//get the direction command between current QR and the next QR in the shortest path
                }
            }
            else//we got lost recalculate the path 
            {
                shortestPath = search.dijkstra(graph, currentNode, targetNodes);
                if (shortestPath.Count == 1)
                {
                    finished = true;
                    Node endNode = targetNodes[0];
                    if (targetNodes[0] == currentNode)
                    {
                        endNode = targetNodes[1];
                    }
                    navigationCommand = graph.getEdgeCommand(currentNode, endNode);
                }
                else
                {
                    navigationCommand = graph.getEdgeCommand(shortestPath[0], shortestPath[1]);
                }
            }

        }
        List<string> returnList = new List<string>();

        if (finished)
        {
            returnList.Add(navigationCommand);
            returnList.Add("Finished");
            return returnList;
        }
        returnList.Add(navigationCommand);
        returnList.Add("notFinished");
        return returnList;
    }

    #endregion
}
