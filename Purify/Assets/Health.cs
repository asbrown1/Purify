using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public int maxHealth = 100;
    public float timeBeforeRegen = 10.0f;   //Time character must wait before regenerating after being attacked
    float regenTimeLeft;                    //Time left before healing 1 HP
    public float regenRatePerSecond=5.0f;   //Health healed per second while regenerating
    float regenTimePerHP;                   //Time taken to heal 1HP (calculated from variable above)
    int health;                             //Tracks health
    // Use this for initialization
    void Start () {
        health = maxHealth;
        regenTimeLeft = 0;
        regenTimePerHP = 1 / regenRatePerSecond;
    }
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
            Destroy(this.gameObject);
        if (health < maxHealth && regenTimeLeft < 0)
        {
            health++;
            regenTimeLeft = regenTimePerHP;
        }
        else
            regenTimeLeft = regenTimeLeft - Time.deltaTime;
    }

    public int getHealth()
    {
        return health;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

    public void reduceHealth(int amount)
    {
        health = health - amount;
        regenTimeLeft = timeBeforeRegen;
        Debug.Log(gameObject.name + " now has " + health + " health");
    }
}
