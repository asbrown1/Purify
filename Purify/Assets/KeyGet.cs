using UnityEngine;
using System.Collections;

public class KeyGet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.name=="Player")
        {
            Keys keys = other.gameObject.GetComponent<Keys>();
            keys.addKey(this.gameObject.name);
            Destroy(this.gameObject);
        }
    }
}
