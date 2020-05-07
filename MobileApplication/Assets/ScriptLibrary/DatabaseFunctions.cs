using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using System;
using System.Linq;
using UnityEngine.UI;
using System.Runtime.InteropServices;

namespace DatabaseFunctions
{
    public class DatabaseFunctions
    {
        DatabaseReference reference;

        public DatabaseFunctions()
        {
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://indoor-navigation-bf70e.firebaseio.com/");
            reference = FirebaseDatabase.DefaultInstance.RootReference;
        }


 
        // Return an array of map names within the database
        public void getSnapshot(Action<DataSnapshot> callback)
        {
            List<string> mapList = new List<string>();

             FirebaseDatabase.DefaultInstance
               .GetReference("Maps")
               .GetValueAsync().  ContinueWith(task =>
               {
                   if (task.IsFaulted)
                   {
                   // Handle the error...
                   Debug.Log("Error");
                   }
                   else if (task.IsCompleted)
                   {
                       DataSnapshot snapshot = task.Result;
                       //Debug.Log("Snapshot "+task.Result);
                       callback(task.Result);
                       /*       foreach (DataSnapshot map in snapshot.Children)
                              {
                                  string name = (string)map.Key;
                                  mapList.Append(name);
                                  Debug.Log("DATABASE FUNC1 "+name );




                                  //IDictionary dictMap = (IDictionary)map.Value;
                                  //IDictionary GlobalRoomList = (IDictionary)dictMap["GlobalRoomList"];

                                  //Debug.Log("" + GlobalRoomList["Sosb10"] + " - " + dictMap["Nodes"]);
                              }*/
                       // Do something with snapshot...
                       // return mapList;
                       
                   }
               });
       

            //Debug.Log("DATABASE FUNC " + mapList[0]);

        }
        public List<string> getMapList(DataSnapshot snap)
        {
            List<string> mapList = new List<string>();
            foreach (DataSnapshot map in snap.Children)
            {
                string name = (string)map.Key;
                mapList.Append(name);
            }
            return mapList;
        }





    }
}