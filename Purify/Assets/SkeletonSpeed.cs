using UnityEngine;
using System.Collections;
using System;

public class SkeletonSpeed : MonoBehaviour {
    Animator anim;
    float speed;
    Rigidbody body;
    public bool detailedLog;
    Vector3 lastPosition;
    Animation bossAnim;
    // Use this for initialization
    void Start () {
        body = this.GetComponent<Rigidbody>();
        if (this.tag.Equals("Enemy")||this.tag.Equals("FriendlyAI"))
            anim = this.GetComponent<Animator>();
        if (this.tag.Equals("Boss"))
            bossAnim = this.transform.GetChild(1).GetComponent<Animation>();
        lastPosition=transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 position = transform.position;
        speed = Vector3.Magnitude((position-lastPosition)/Time.deltaTime);
        if(detailedLog)
            Debug.Log(speed);
        if(this.tag.Equals("Enemy") || this.tag.Equals("FriendlyAI"))
            anim.SetFloat("Speed", speed);
        else if (this.tag.Equals("Boss")&&!(bossAnim.IsPlaying("attack01")|| bossAnim.IsPlaying("attack02") || bossAnim.IsPlaying("dead")))
        {
            if (speed < 0.5)
                bossAnim.Play("stand");
            else if (speed < 5.2)
                bossAnim.Play("walk");
            else
                bossAnim.Play("run");     
        }
        lastPosition = position;
	}
}
