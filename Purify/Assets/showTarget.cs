using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class showTarget : MonoBehaviour {
    Image image;
    HealBuffV2 targetting;
    Transform parentObject;
	// Use this for initialization
	void Start () {
        parentObject = this.transform.root.GetComponent<Transform>();
        image = this.GetComponent<Image>();
        targetting = GameObject.FindGameObjectWithTag("Player").GetComponent<HealBuffV2>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(targetting.isClosest(parentObject.name))
        {
            image.enabled = true;
        }
        else
        {
            image.enabled = false;
        }
	}
}
