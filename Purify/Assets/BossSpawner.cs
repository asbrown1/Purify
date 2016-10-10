using UnityEngine;
using System.Collections;

public class BossSpawner : MonoBehaviour {
    public GameObject skeleton;
    public float spawnTime=3f;
    float timeSinceSpawn = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 spawnPosition = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
        timeSinceSpawn = timeSinceSpawn + Time.deltaTime;
        if(timeSinceSpawn>=spawnTime&&this.GetComponent<AIPhase>().getPhase().Equals("Attack"))
        {
            GameObject bossSkeleton = (GameObject)Instantiate(skeleton,spawnPosition,Quaternion.identity);
            timeSinceSpawn = 0;
        }
	}
}
