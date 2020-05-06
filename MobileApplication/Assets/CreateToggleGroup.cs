using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateToggleGroup : MonoBehaviour
{
    [Range(1,21)]
    public int mButtonCount = 20;
    public GameObject mButtonPrefab = null;
    public ToggleGroup mToggleGroup = null;
    // Start is called before the first frame update
    private void Awake()
    {
        mToggleGroup = GetComponent<ToggleGroup>();
        CreateButtons();

    }
    private void CreateButtons()
    {
        for (int i = 0; i < mButtonCount; i++)
        {
            GameObject newButton = Instantiate(mButtonPrefab);
            newButton.transform.SetParent(transform);
            string area = "Area" + (i + 1).ToString();
            newButton.GetComponentInChildren<Text>().text = area;
            newButton.name = area;
            Toggle toggle = newButton.GetComponent<Toggle>();
            toggle.group = mToggleGroup;
            toggle.onValueChanged.AddListener(PrintToggle);
            if(i == 0) {
                toggle.isOn=true;
            }
        }
        
    }
    private void PrintToggle(bool value)
    {
        if (!value)
            return;
        List<Toggle> activeToggles = new List<Toggle>(mToggleGroup.ActiveToggles());
        foreach(Toggle toggle in activeToggles)
        {
            print(toggle.gameObject.name);
        }
    }
}
