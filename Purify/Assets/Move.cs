using UnityEngine;
using System.Collections;
using System;

public class Move : MonoBehaviour
{
    public float force;
    public Camera mainCam;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /*Orientation of camera*/
        Vector3 forwardCam = mainCam.transform.forward;
        Vector3 rightCam = mainCam.transform.right;
        forwardCam = forwardCam / forwardCam.magnitude;
        rightCam = rightCam / rightCam.magnitude;


        /*Moves relative to camera, but not in vertical axis*/
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector3(forwardCam.x,0, forwardCam.z) * force * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(forwardCam.x, 0, forwardCam.z) * -force * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(rightCam.x, 0, rightCam.z) * force * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(rightCam.x, 0, rightCam.z) * -force * Time.deltaTime);
        }
    }
}
