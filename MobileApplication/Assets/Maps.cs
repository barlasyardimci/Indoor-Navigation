using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DatabaseFunctions;

public class Maps : MonoBehaviour
{
    DatabaseFunctions.DatabaseFunctions database;
    // Start is called before the first frame update
    void Start()
    {
        database  = new DatabaseFunctions.DatabaseFunctions();
        List<string> mapList = database.getMapList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region UI Buttons

    public void ClickBack()
    {
        SceneManager.LoadScene("Menu");
    }

    #endregion
}
