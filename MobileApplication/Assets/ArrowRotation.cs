using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotation : MonoBehaviour
{

    private string direction;
    public GameObject south_arrow;
    public GameObject north_arrow;
    public GameObject east_arrow;
    public GameObject west_arrow;

    // Start is called before the first frame update
    void Start()
    {
        direction = Demo.arrowDirection;

        if(direction.Equals("south"))
        {
            south_arrow.SetActive(true);
            north_arrow.SetActive(false);
            east_arrow.SetActive(false);
            west_arrow.SetActive(false);
            //arrow.transform.Rotate(0, 90, 270);
            //arrow.transform.rotation = new Quaternion(0,90,270,0);
        }
        else if (direction.Equals("east"))
        {
            south_arrow.SetActive(false);
            north_arrow.SetActive(false);
            east_arrow.SetActive(true);
            west_arrow.SetActive(false);
            //arrow.transform.Rotate(180, 180, 0);
            //arrow.transform.rotation = new Quaternion(180, 180, 0, 0);
        }
        else if (direction.Equals("north"))
        {
            south_arrow.SetActive(false);
            north_arrow.SetActive(true);
            east_arrow.SetActive(false);
            west_arrow.SetActive(false);
            //arrow.transform.Rotate(180, 90, 0);
            //arrow.transform.rotation = new Quaternion(180, 90, 0, 0);
        }
        else if (direction.Equals("west"))
        {
            south_arrow.SetActive(false);
            north_arrow.SetActive(false);
            east_arrow.SetActive(false);
            west_arrow.SetActive(true);
            //arrow.transform.Rotate(180, 0, 0);
            //arrow.transform.rotation = new Quaternion(180, 0, 0, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
