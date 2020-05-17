using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Storage;
using UnityEngine.Events;
using System;
public class WebCamDisplay : MonoBehaviour
{
    public UnityEvent OnUploadStarted = new UnityEvent();
    public UploadGameDataCompleteEvent OnUploadComplete = new UploadGameDataCompleteEvent();

    public UploadGameDataFailedEvent OnUploadFailed = new UploadGameDataFailedEvent();
    WebCamTexture webCamTexture;
    public RawImage background;
    public AspectRatioFitter fit;
    private Coroutine _uploadCoroutine;
    private List<byte[]> imageList = new List<byte[]>(); 
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        Texture defaultBackground = background.texture;
        webCamTexture = new WebCamTexture(devices[0].name, Screen.width, Screen.height);
        //GetComponent<Renderer>().material.mainTexture = webCamTexture; //Add Mesh Renderer to the GameObject to which this script is attached to
        webCamTexture.Play();
        background.texture = webCamTexture;
    }
    private void Update()
    {
        float ratio = (float)webCamTexture.width / (float)webCamTexture.height;
        fit.aspectRatio = ratio;
        float scaleY = webCamTexture.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);
        int orient = -webCamTexture.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }

    public void TakePhoto()  // Start this Coroutine on some button click
    {

        // NOTE - you almost certainly have to do this here:

       // yield  new WaitForEndOfFrame();

        // it's a rare case where the Unity doco is pretty clear,
        // http://docs.unity3d.com/ScriptReference/WaitForEndOfFrame.html
        // be sure to scroll down to the SECOND long example on that doco page 

        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();

        //Encode to a PNG
        byte[] bytes = photo.EncodeToPNG();

        //Write out the PNG. Of course you have to substitute your_path for something sensible
        imageList.Add(bytes);
        Debug.Log(imageList.Count);
    }
     public void UploadData()

    {

        OnUploadStarted.Invoke();
        int i = 0;
        foreach (var image in imageList)
        {

            var storage = FirebaseStorage.DefaultInstance;
            var finalScoreReference = storage.GetReference("QR Image"+i);

            var metadata = new MetadataChange

            {

                ContentType = "image/png",

                CustomMetadata = new Dictionary<string, string>()

            {

                {"image_ID", "QRIMAGE"},


            }

            };



            var uploadTask = finalScoreReference.PutBytesAsync(image, metadata);

            //yield return new WaitUntil(() => uploadTask.IsCompleted);
            i++;
            OnUploadComplete.Invoke(finalScoreReference);
            Debug.Log($"Data Uploaded to {finalScoreReference.Path}");


        }




        _uploadCoroutine = null;




    }

    public void tp()
    {
        Debug.Log("TP");
        TakePhoto();
    }
    public void up()
    {
        UploadData();
    }
    public enum FailureReason

    {

        None,

        SignInFailed,

        UploadFailed,

        NoDeathData

    }



    [Serializable]

    public class UploadGameDataFailedEvent : UnityEvent<FailureReason, Exception>

    {

    }



    [Serializable]

    public class UploadGameDataCompleteEvent : UnityEvent<StorageReference>

    {

    }
}
