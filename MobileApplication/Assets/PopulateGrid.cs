using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DatabaseFunctions;
using System.Threading;

public class PopulateGrid : MonoBehaviour
{
	public GameObject prefab; // This is our prefab object that will be exposed in the inspector

	DatabaseFunctions.DatabaseFunctions database;
	List<string> mapList;
	int numberToCreate;

	void Start()
	{
		
		database = new DatabaseFunctions.DatabaseFunctions();
		Debug.Log("ASDIA");

		mapList = database.getMapList();


		Thread.Sleep(3000);
		Debug.Log("DATABASE FUNC2 ");
		Debug.Log("" + mapList[0]);
		//numberToCreate = mapList.Count;
		//Populate();
	}

	void Update()
	{

	}

	void Populate()
	{
		GameObject newObj; // Create GameObject instance

		for (int i = 0; i < numberToCreate; i++)
		{
			// Create new instances of our prefab until we've created as many as we specified
			newObj = (GameObject)Instantiate(prefab, transform);

			// Button List
			newObj.GetComponent<Button>().GetComponentInChildren<Text>().text = mapList[i];
		}

	}
}