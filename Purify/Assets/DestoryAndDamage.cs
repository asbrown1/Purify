using UnityEngine;
using System.Collections;

public class DestoryAndDamage : MonoBehaviour {

    public int bulletDamage = 5;
    public float timeBeforeDestroy = 2.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timeBeforeDestroy = timeBeforeDestroy-Time.deltaTime;
        if (timeBeforeDestroy <= 0)
            Destroy(this.gameObject);
	}

    void OnCollisionEnter(Collision info)
    {
        Health colliderHealth;
        string colliderName = info.collider.name;
        if (!(colliderName.Contains("Wall")||colliderName.Contains("Terrain")))
        {
            Debug.Log("Bullet collided with" +colliderName);
            colliderHealth = GameObject.Find(colliderName).GetComponent<Health>();
            colliderHealth.reduceHealth(bulletDamage);
            Destroy(this.gameObject);
        }

    }
}
