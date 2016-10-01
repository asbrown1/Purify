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
        GameObject colliderTarget = other.gameObject.transform.root.gameObject;
        string colliderName = colliderTarget.name;
        string colliderTag = colliderTarget.tag;
        Debug.Log(colliderName);
        Debug.Log("Time alive is " + timeAlive);
        if (timeAlive > 0.01)
        {
            Debug.Log("Time alive is " + timeAlive);
            Debug.Log("Collider is of type " + colliderTag);
            if (!(colliderTag.Equals("Environment")))
            {
                Debug.Log("Bullet collided with" + colliderName);
                colliderHealth = colliderTarget.GetComponent<Health>();
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
