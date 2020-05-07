using System;
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
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        direction = Demo.arrowDirection;
        Boolean isFinalDirection = Demo.isFinalDirection;



        if (direction.Equals("south"))
        {
            if (isFinalDirection == true)
            {
                south_arrow.GetComponent<Renderer>().material.color = Color.red;

            }

            south_arrow.SetActive(true);
            north_arrow.SetActive(false);
            east_arrow.SetActive(false);
            west_arrow.SetActive(false);




            //arrow.transform.Rotate(0, 90, 270);
            //arrow.transform.rotation = new Quaternion(0,90,270,0);
        }
        else if (direction.Equals("east"))
        {
            if (isFinalDirection == true)
            {
                east_arrow.GetComponent<Renderer>().material.color = Color.red;

            }
            south_arrow.SetActive(false);
            north_arrow.SetActive(false);
            east_arrow.SetActive(true);
            west_arrow.SetActive(false);


            //arrow.transform.Rotate(180, 180, 0);
            //arrow.transform.rotation = new Quaternion(180, 180, 0, 0);
        }
        else if (direction.Equals("north"))
        {
            if (isFinalDirection == true)
            {
                north_arrow.GetComponent<Renderer>().material.color = Color.red;

            }
            south_arrow.SetActive(false);
            north_arrow.SetActive(true);
            east_arrow.SetActive(false);
            west_arrow.SetActive(false);
            //arrow.transform.Rotate(180, 90, 0);
            //arrow.transform.rotation = new Quaternion(180, 90, 0, 0);
        }
        else if (direction.Equals("west"))
        {
            if (isFinalDirection == true)
            {
                west_arrow.GetComponent<Renderer>().material.color = Color.red;

            }
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
