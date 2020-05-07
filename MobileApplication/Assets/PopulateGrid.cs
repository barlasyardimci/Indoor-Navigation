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


public class PopulateGrid : MonoBehaviour
{
	public GameObject prefab; // This is our prefab object that will be exposed in the inspector


	void Start()
	{
		List<string> strL = new List<string>();
		RestClient.Get("https://indoor-navigation-bf70e.firebaseio.com/Maps.json").Then(response => {
			Debug.Log("Response" + response.Text + "Ok");
			var N = JSON.Parse(response.Text);
			foreach (var key in N)
			{
				Debug.Log(key.Key.ToString());
				strL.Add(key.Key);
			}
			Populate(strL);
		});
		

	}


	void Populate(List<string> mapList)
	{
		GameObject newObj; // Create GameObject instance
		Debug.Log("Maplist 0" + mapList[0]);
		Debug.Log("I am in popp");

		foreach(string map in mapList)
		{
			// Create new instances of our prefab until we've created as many as we specified
			newObj = (GameObject)Instantiate(prefab, transform);

			// Button List
			newObj.GetComponent<Button>().GetComponentInChildren<Text>().text = map;

			Debug.Log(map);

		}
	}


}