using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PrefabMapButton : MonoBehaviour
{
    public Toggle toggle;

    public void OnClick(Button button)
    {
       CurrentMap.currentMapName= button.GetComponentInChildren<Text>().text;

        changeColor(button);
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


