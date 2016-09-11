using UnityEngine;
using System.Collections;

public class DestoryAndDamage : MonoBehaviour {

    public int bulletDamage = 5;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void onCollisionEnter(Collision info)
    {
        Health colliderHealth;
        if(!(info.collider.name.Contains("Wall")))
        {
            Debug.Log("Bullet collided");
            colliderHealth = info.collider.GetComponent<Health>();
            colliderHealth.reduceHealth(bulletDamage);
            Destroy(this.gameObject);
        }

    }
}
