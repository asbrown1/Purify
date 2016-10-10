using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerProgress : MonoBehaviour {
    string currentLevel;
    int experience;
    int expLevel;
    int pickupHealthGain;
    int pickupManaGain;
    public int levelExpNeeded;
    public int attackGainLevel;
    public int manaGainLevel;
    public int healthGainLevel;
    public int healStrengthGainLevel;
    public int buffStrengthGainLevel;
    bool hasCheckedStart = false;
	// Use this for initialization
	void Start () {
        currentLevel = SceneManager.GetActiveScene().name;
        expLevel = 0;
        PlayerPrefs.SetString("LastLevel", currentLevel);
        if(PlayerPrefs.HasKey("PlayerExperience"))
        {
            experience = PlayerPrefs.GetInt("PlayerExperience");
        }
        else
        {
            PlayerPrefs.SetInt("PlayerExpereince", 0);
            experience = 0;
        }
    }
	
	// Update is called once per frame
	void Update () {
	if(!hasCheckedStart)    //Basically a late 'start' as some things need to run after other scripts start
        {
            if (PlayerPrefs.HasKey("PickupHealthGain"))
            {
                healthGainLevel = healthGainLevel = PlayerPrefs.GetInt("PickupHealthGain");
            }
            if (PlayerPrefs.HasKey("PickupManaGain"))
            {
                manaGainLevel = manaGainLevel = PlayerPrefs.GetInt("PickupManaGain");
            }
            checkExpLevel();
            hasCheckedStart = false;
        }
	}

    public int getExperience()
    {
        return experience;
    }

    public int getExpLevel()
    {
        return expLevel;
    }
    public int getExpNextLevel()
    {
        return (expLevel+1)*levelExpNeeded;
    }
    public void gainExperience(int amount)
    {
        experience = experience + amount;
        checkExpLevel();
    }

    void checkExpLevel()
    {
        Debug.Log("Entering CheckEXPLevel. Old level is "+expLevel);
        int oldExpLevel = expLevel;
        expLevel = experience / levelExpNeeded;
        Debug.Log("Current level is " + expLevel);
        if(expLevel>oldExpLevel)
        {
            Debug.Log("Changing stats");
            PlayerAttack attack = GetComponent<PlayerAttack>();
            Mana mana = GetComponent<Mana>();
            Health health = GetComponent<Health>();
            attack.addStrength(attackGainLevel*expLevel);
            mana.addMana(manaGainLevel*expLevel);
            health.addHealth(healthGainLevel*expLevel);
        }
    }
}
