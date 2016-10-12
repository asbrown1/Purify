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
                if (this.gameObject.tag.Equals("Enemy"))
                {
                    body.enabled = false;
                    anim.SetTrigger("Death");
                }
                if (this.gameObject.tag.Equals("Boss"))
                {
                    Animation anim = this.transform.GetChild(1).GetComponent<Animation>();
                    anim.Play("dead");
                }
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
