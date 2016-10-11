using UnityEngine;
using System.Collections;

public class EnemyDeath : MonoBehaviour {
    public float animationTime = 2f;
    Collider body;
    AIPhase phase;
    Animator anim;
    bool triggerSent = false;
	// Use this for initialization
	void Start () {
	    phase=this.GetComponent<AIPhase>();
        anim=this.GetComponent<Animator>();
        body = this.GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(phase.getPhase().Equals("Dead"))
        {
            if (!triggerSent)
            {
                body.enabled = false;
                anim.SetTrigger("Death");
                triggerSent = true;
            }
            animationTime = animationTime - Time.deltaTime;
        }
        if(animationTime<=0)
        {
            Destroy(this.gameObject);
        }
	}
}
