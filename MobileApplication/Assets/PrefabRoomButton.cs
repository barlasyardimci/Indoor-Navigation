using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PrefabRoomButton : MonoBehaviour
{

    public void OnClick(Button button)
    {
        CurrentMap.targetRoomName = button.GetComponentInChildren<Text>().text;
        changeColor(button);
    }
    public void changeColor(Button button)
    {
        float r = 0;
        float g = 0;
        float b = 255;
        ColorBlock colorVar = button.colors;
        colorVar.highlightedColor = new Color(r, g, b);
        button.colors = colorVar;
    }
}


