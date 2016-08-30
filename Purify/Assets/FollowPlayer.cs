using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    public GameObject player;
    public float distance = 5.0f;
    public float angle = 30.0f;
    public float force=0.2f;
    NavMeshAgent agent;
    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 playerPosition = player.transform.position;
        Vector3 playerRotation = player.transform.rotation.eulerAngles;
        float zValue = distance * Mathf.Cos((playerRotation.y+angle)*Mathf.Deg2Rad);
        float xValue = distance * Mathf.Sin((playerRotation.y + angle)*Mathf.Deg2Rad);
        Vector3 playerOffset = new Vector3(playerPosition.x + xValue, playerPosition.y, playerPosition.z + zValue); //Target position from player
        agent.destination = playerOffset;
	}
}
