using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    public GameObject player;
    public float offsetX=0.0f; 
    public float offsetZ=0.0f;
    public float force=0.2f;
    NavMeshAgent agent;
    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 playerPosition = player.transform.position;
        Vector3 playerOffset = new Vector3(playerPosition.x + offsetX, playerPosition.y, playerPosition.z + offsetZ); //Target position from player
        agent.destination = playerOffset;
	}
}
