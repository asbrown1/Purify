using UnityEngine;
using System.Collections;
using System;

public class Move : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 5f;
    public double knockBackTime = 0.5;
    public Camera mainCam;
    bool delay = false;
    Vector3 knockBackDirection = new Vector3(0, 0, 0);
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 cameraOrientation = mainCam.transform.rotation.eulerAngles;
        Vector3 movement = new Vector3(0, 0, 0);
        Vector3 playerMovement = new Vector3(mainCam.transform.forward.x, 0, mainCam.transform.forward.z);
        Vector3 playerMovementRight = new Vector3(mainCam.transform.right.x, 0, mainCam.transform.right.z);
        /*Moves relative to camera, but not in vertical axis*/

        //player is currently being knocked back
        if (knockBackTime < 1.2)
        {
            knockBackTime = knockBackTime + Time.deltaTime;
            if (knockBackTime > 0.6||!delay)
            {
                movement = knockBackDirection * -movementSpeed * Time.deltaTime;
            }
        }
        
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            movement = playerMovement * movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            movement = playerMovement * -movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            movement = playerMovementRight * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            movement = playerMovementRight * -movementSpeed * Time.deltaTime;
        }
        if (movement.magnitude != 0)
        {
            transform.position += movement;
            faceCamera();
        }
    }
    public float getRotationSpeed()
    {
        return rotationSpeed;
    }

    public void faceCamera()
    {
        Vector3 cameraRotation = mainCam.transform.rotation.eulerAngles;
        Vector3 playerTargetRotation = new Vector3(0, cameraRotation.y, 0);
        transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(playerTargetRotation), Time.deltaTime * rotationSpeed);
    }

    //player knockback on being hit by enemies
    public void knockBack(Vector3 direction,bool hasDelay)
    {
        knockBackTime = 0;
        knockBackDirection = direction;
        delay = hasDelay;
    }
}
