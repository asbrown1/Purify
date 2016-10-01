using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {

    public int maxHealth = 100;
    public float timeBeforeRegen = 10.0f;   //Time character must wait before regenerating after being attacked
    float regenTimeLeft;                    //Time left before healing 1 HP
    public float regenRatePerSecond=5.0f;   //Health healed per second while regenerating
    float regenTimePerHP;                   //Time taken to heal 1HP (calculated from variable above)
    int health;                             //Tracks health
    float particleTime = 0f;
    ParticleSystem particles;
    // Use this for initialization
    void Start () {
        health = maxHealth;
        regenTimeLeft = 0;
        regenTimePerHP = 1 / regenRatePerSecond;
        if (GetComponent<ParticleSystem>())
            particles = GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
        {
            if (!(this.gameObject.tag.Equals("Player")))
                Destroy(this.gameObject);
            else
                SceneManager.LoadScene("GameOver");
        }
        if (health < maxHealth && regenTimeLeft < 0)
        {
            health++;
            regenTimeLeft = regenTimePerHP;
        }
        else
            regenTimeLeft = regenTimeLeft - Time.deltaTime;
        if (particles)
        {
            if (particleTime > 0)
            {
                particles.Play();
                particleTime = particleTime - Time.deltaTime;
            }
            else if(particles.startColor == Color.green)
            {
                particles.Stop();
            }
        }
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
        //Knockback animation goes here (might need to script position too)
        health = health - amount;
        regenTimeLeft = timeBeforeRegen;
        Debug.Log(gameObject.name + " now has " + health + " health");
    }

    public void getHealth(int amount)
    {
        particles.startColor = Color.green;
        particleTime = 0.5f;
        health = health + amount;
        if(health>maxHealth)
        {
            health = maxHealth;
        }
    }
}
