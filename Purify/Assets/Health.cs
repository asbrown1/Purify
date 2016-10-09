using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {
    AIPhase phase;
    public int startMaxHealth = 100;
    int maxHealth;
    public float timeBeforeRegen = 10.0f;   //Time character must wait before regenerating after being attacked
    float regenTimeLeft;                    //Time left before healing 1 HP
    public float regenRatePerSecond=5.0f;   //Health healed per second while regenerating
    float regenTimePerHP;                   //Time taken to heal 1HP (calculated from variable above)
    int health;                             //Tracks health
    float particleTime = 0f;
    public int expPerEnemyKilled=5;
    ParticleSystem particles;
    public bool detailedLog = false;
    // Use this for initialization
    void Start () {
        phase = this.GetComponent<AIPhase>();
        maxHealth = startMaxHealth;
        health = maxHealth;
        regenTimeLeft = 0;
        regenTimePerHP = 1 / regenRatePerSecond;
        if (GetComponent<ParticleSystem>())
            particles = GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        if (detailedLog)
            Debug.Log("Health is " + health);
        if (health <= 0&&!(phase.getPhase().Equals("Dead")))
        {
            if (!(this.gameObject.tag.Equals("Player")))
            {
                if (this.gameObject.tag.Equals("Enemy"))
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    PlayerProgress exp = player.GetComponent<PlayerProgress>();
                    exp.gainExperience(expPerEnemyKilled);
                }
                phase.setPhase("Dead");
            }
            else
                SceneManager.LoadScene("GameOver");
        }
        if (health < maxHealth && regenTimeLeft < 0&&!(phase.getPhase().Equals("Dead")))
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
        if(this.transform.tag.Equals("Player"))
        {
            Move move = this.GetComponent<Move>();
            move.knockBack(); //changed to a knockback function instead
        }
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
    public void addHealth(int amount)
    {
        maxHealth = startMaxHealth + amount;
    }
    public void revive()
    {
        health = maxHealth;
        phase.setPhase("Follow");
    }
}
