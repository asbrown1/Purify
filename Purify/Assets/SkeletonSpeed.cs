using UnityEngine;
using System.Collections;
using System;

public class SkeletonSpeed : MonoBehaviour {
    Animator anim;
    float speed;
    Rigidbody body;
    public bool detailedLog;
    Vector3 lastPosition;
    // Use this for initialization
    void Start () {
        body = this.GetComponent<Rigidbody>();
        anim = this.GetComponent<Animator>();
        lastPosition=transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 position = transform.position;
        speed = Vector3.Magnitude((position-lastPosition)/Time.deltaTime);
        if(detailedLog)
            Debug.Log(speed);
        anim.SetFloat("Speed", speed);
        lastPosition = position;
	}
}
