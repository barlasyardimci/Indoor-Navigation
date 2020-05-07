using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectButton : MonoBehaviour
{
    public Button button;
    public Canvas PopupRoomSelect;
    public Canvas CameraCanvas;

    // Start is called before the first frame update
    public void OnClick()
    {
        Debug.Log(CurrentMap.targetRoomName);
        if(CurrentMap.targetRoomName != " ") { 

            PopupRoomSelect.enabled = false ;
            CameraCanvas.enabled = true;
        }

    }
}
