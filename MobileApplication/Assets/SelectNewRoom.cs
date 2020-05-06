using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectNewRoom : MonoBehaviour
{
    public Button button;
    public Canvas PopupRoomSelect;
    public Canvas CameraCanvas;

    // Start is called before the first frame update
    public void OnClick()
    {
            PopupRoomSelect.enabled = true;
            CameraCanvas.enabled = false;
    }
}
