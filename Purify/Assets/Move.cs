using UnityEngine;
using System.Collections;
using System;

public class Move : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 5f;
    public Camera mainCam;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /*Orientation of camera*/
        Vector3 movement= new Vector3(0,0,0);
        /*Moves relative to camera, but not in vertical axis*/
        if (Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.W))
        {
            movement=this.transform.forward * movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            movement= this.transform.forward * -movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            movement = this.transform.right * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            movement = this.transform.right * -movementSpeed * Time.deltaTime;
        }
        if (movement.magnitude!=0)
        {
            transform.position += movement;
            if(!(Input.GetKey(KeyCode.DownArrow)))
            transform.rotation = Quaternion.Slerp(this.transform.rotation,Quaternion.LookRotation(movement),Time.deltaTime*rotationSpeed);
        }
    }
}
