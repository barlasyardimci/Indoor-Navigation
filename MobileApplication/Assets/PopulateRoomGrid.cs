using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DatabaseFunctions;
using System.Threading;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using System.Linq;
using Proyecto26;
using SimpleJSON;
using Microsoft.Win32.SafeHandles;

public class PopulateRoomGrid : MonoBehaviour
{
	public GameObject prefab; // This is our prefab object that will be exposed in the inspector

	void Start()
	{

		string mapName = CurrentMap.currentMapName;

		List<string> roomList = new List<string>();
		RestClient.Get("https://indoor-navigation-bf70e.firebaseio.com/Maps/"+mapName+".json").Then(response => {
			Debug.Log("Response" + response.Text + "Ok");
			var N = JSON.Parse(response.Text);
			foreach(string room in N["GlobalRoomList"].Keys)
			{
				roomList.Add(room);
			}
			Populate(roomList);
		});


	}


	void Populate(List<string> roomList)
	{
		GameObject newObj; // Create GameObject instance
		Debug.Log("roomList 0" + roomList[0]);
		Debug.Log("I am in popp");

		foreach (string room in roomList)
		{
			// Create new instances of our prefab until we've created as many as we specified
			newObj = (GameObject)Instantiate(prefab, transform);

			// Button List
			newObj.GetComponent<Button>().GetComponentInChildren<Text>().text = room;
			changeColor(newObj.GetComponent<Button>());
			Debug.Log(room);

		}
	}
	public void changeColor(Button button)
	{
		Debug.Log("Changing highlighed color");
		float r = 0;
		float g = 0;
		float b = 255;
		ColorBlock colorVar = button.colors;
		colorVar.highlightedColor = new Color(r, g, b);
		button.colors = colorVar;
	}


}