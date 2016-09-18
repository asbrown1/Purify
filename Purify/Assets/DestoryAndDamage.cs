using UnityEngine;
using System.Collections;

public class DestoryAndDamage : MonoBehaviour {

    public int bulletDamage = 5;
    public float timeBeforeDestroy = 2.0f;
    Vector3 velocity=new Vector3(0,0,0);
    float timeAlive=0.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timeAlive = timeAlive + Time.deltaTime;
        if (timeAlive >= 2)
        {
            Debug.Log("Bullet destroyed as alive for too long");
            Destroy(this.gameObject);
        }
        transform.Translate(velocity * Time.deltaTime);
	}

    void OnTriggerEnter(Collider other)
    {
        Health colliderHealth;
        string colliderName = other.name;
        Debug.Log(colliderName);
        Debug.Log("Time alive is " + timeAlive);
        if (timeAlive > 0.01)
        {
            Debug.Log("Time alive is " + timeAlive);
            if (!(colliderName.Contains("Wall") || colliderName.Contains("Terrain")))
            {
                Debug.Log("Bullet collided with" + colliderName);
                colliderHealth = GameObject.Find(colliderName).GetComponent<Health>();
                colliderHealth.reduceHealth(bulletDamage);
                Debug.Log("Bullet destroyed as collided with character");
                Destroy(this.gameObject);
            }
            if (colliderName.Contains("Wall"))
            {
                Debug.Log("Bullet destroyed collided with wall");
                Destroy(this.gameObject);
            }
        }

    }

    public void setSpeed(Vector3 speed)
    {
        velocity = speed;
    }
}
