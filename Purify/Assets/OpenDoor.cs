using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {

    GameObject player;
    public float maxDistance = 2;
    Keys keys;
    public string keyNeeded="Key";
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        keys = player.GetComponent<Keys>();
    }
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Magnitude(this.transform.position - player.transform.position);
        //Debug.Log(distance);
        if (distance < maxDistance && keys.hasKey(keyNeeded))
            {
                Destroy(this.gameObject);
            }
	}
}
