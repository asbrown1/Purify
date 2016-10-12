using UnityEngine;
using System.Collections;

public class SetBossDefault : MonoBehaviour {
    Animation anim;
	// Use this for initialization
	void Start () {
	    anim=this.GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!(anim.isPlaying))
            anim.Play("stand");
	}
}
