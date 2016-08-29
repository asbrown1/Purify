using UnityEngine;
using System.Collections;

public class FollowPlayerBackup : MonoBehaviour {

    public GameObject player;
    public float offsetX=0.0f; 
    public float offsetZ=0.0f;
    public float force=0.2f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Rigidbody body = this.GetComponent<Rigidbody>();
        Vector3 playerPosition = player.transform.position;
        Vector3 playerOffset = new Vector3(playerPosition.x + offsetX, playerPosition.y, playerPosition.z + offsetZ); //Target position from player
        Vector3 friendlyPosition = this.transform.position;
        Vector3 differences = new Vector3(playerOffset.x - friendlyPosition.x, 0, playerOffset.z - friendlyPosition.z); //Difference between target and current position
        if (differences.magnitude > 0.5)    //To stop very small corrections
        {
            differences = differences / differences.magnitude;  //Normalise vector length to make velocity constant
            this.transform.Translate(differences * force * Time.deltaTime);
        }
	}
}
