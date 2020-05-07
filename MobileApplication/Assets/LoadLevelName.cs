using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevelName : MonoBehaviour
{

    public string lname;

    void OnEnable()
    {
        SceneManager.LoadScene(lname);
    }
}

