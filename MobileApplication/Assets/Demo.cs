using BarcodeScanner;
using BarcodeScanner.Scanner;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Wizcorp.Utils.Logger;

public class Demo : MonoBehaviour
{

    private IScanner BarcodeScanner;
    public Text TextHeader;
    public RawImage Image;

    void Start()
    {
        BarcodeScanner = new Scanner();
        BarcodeScanner.Camera.Play();


        BarcodeScanner.OnReady += (sender, arg) => {
 
            Image.transform.localEulerAngles = BarcodeScanner.Camera.GetEulerAngles();
            Image.transform.localScale = BarcodeScanner.Camera.GetScale();
            Image.texture = BarcodeScanner.Camera.Texture;

     
            var rect = Image.GetComponent<RectTransform>();
            var newHeight = rect.sizeDelta.x * BarcodeScanner.Camera.Height / BarcodeScanner.Camera.Width;
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, newHeight);
        };


        BarcodeScanner.StatusChanged += (sender, arg) => {
            TextHeader.text = "Status: " + BarcodeScanner.Status;
        };
    }


    void Update()
    {
        if (BarcodeScanner == null)
        {
            return;
        }
        BarcodeScanner.Update();
    }

    #region UI Buttons

    public void ClickScan()
    {
        if (BarcodeScanner == null)
        {
            Log.Warning("No valid camera - Click Start");
            return;
        }

        BarcodeScanner.Scan((barCodeType, barCodeValue) => {
            BarcodeScanner.Stop();
            TextHeader.text = "Output: " +barCodeValue;
            Debug.Log(barCodeValue);

        });
    }

    public void ClickPause()
    {
        if (BarcodeScanner == null)
        {
            Log.Warning("No valid camera - Click Stop");
            return;
        }


        BarcodeScanner.Stop();
    }

    public void ClickBack()
    {

        StartCoroutine(StopCamera(() => {
            SceneManager.LoadScene("Menu");
        }));
    }

    public IEnumerator StopCamera(Action callback)
    {
    
        Image = null;
        BarcodeScanner.Destroy();
        BarcodeScanner = null;

        yield return new WaitForSeconds(0.1f);

        callback.Invoke();
    }

    #endregion
}
