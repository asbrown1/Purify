using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowSkull : MonoBehaviour {
    Image skull;
    AIPhase phase;
	// Use this for initialization
	void Start () {
        phase = this.transform.root.GetComponent<AIPhase>();
        skull = this.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(phase.getPhase().Equals("Dead"))
        {
            skull.enabled = true;
        }
        else
        {
            skull.enabled = false;
        }
	}
}
