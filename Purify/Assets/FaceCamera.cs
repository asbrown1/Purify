using UnityEngine;
using System.Collections;

//Script from http://wiki.unity3d.com/index.php?title=CameraFacingBillboard. Makes objects directly face the camera

public class FaceCamera : MonoBehaviour
{

    void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
            Camera.main.transform.rotation * Vector3.up);
    }
}