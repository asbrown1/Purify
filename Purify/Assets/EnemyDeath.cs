using UnityEngine;
using System.Collections;

public class EnemyDeath : MonoBehaviour {
    public float animationTime = 2f;
    AIPhase phase;
	// Use this for initialization
	void Start () {
	    phase=this.GetComponent<AIPhase>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(phase.getPhase().Equals("Dead"))
        {
            animationTime = animationTime - Time.deltaTime;
        }
        if(animationTime<=0)
        {
            Destroy(this.gameObject);
        }
	}
}
