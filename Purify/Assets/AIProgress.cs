using UnityEngine;
using System.Collections;

public class AIProgress : MonoBehaviour {
    public int[] attackGainLevel;
    public int[] healthGainLevel;
    Health health;
    Attack attack;
    PlayerProgress progress;
    int level;
    int oldLevel;
    // Use this for initialization
    void Start () {
        health = this.GetComponent<Health>();
        attack = this.GetComponent<Attack>();
        progress = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerProgress>();
        oldLevel = progress.getExpLevel();
        level = oldLevel;
        if (oldLevel > 0)
            buffAI();
    }
	
	// Update is called once per frame
	void Update () {
        level = progress.getExpLevel();
        if(level>oldLevel)
        {
            buffAI();
            oldLevel = level;
        }
	}
    void buffAI()
    {
        health.addHealth(healthGainLevel[level]);
        attack.gainAttack(attackGainLevel[level]);
    }
}
