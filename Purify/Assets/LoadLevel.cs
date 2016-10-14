using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {
    public string nextLevel;
    public int expForLevelBeat = 20;
    public bool hasBoss = false;
    GameObject boss;
	// Use this for initialization
	void Start () {
        if(hasBoss)
        {
            boss = GameObject.FindGameObjectWithTag("Boss");
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if(hasBoss)
        {
            if(boss==null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                PlayerProgress exp = player.GetComponent<PlayerProgress>();
                exp.gainExperience(expForLevelBeat);
                PlayerPrefs.SetInt("PlayerExperience", exp.getExperience());
                SceneManager.LoadScene(nextLevel);
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.transform.tag.Equals("Player"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerProgress exp = player.GetComponent<PlayerProgress>();
            Timer time = player.GetComponent<Timer>();
            time.recordTime();
            exp.gainExperience(expForLevelBeat);
            PlayerPrefs.SetInt("PlayerExperience", exp.getExperience());
            savePickupGain();
            SceneManager.LoadScene(nextLevel);
        }
    }
    void savePickupGain()
    {
        if (PlayerPrefs.HasKey("PickupHealthGain"))
            PlayerPrefs.SetInt("PickupHealthGain", PlayerPrefs.GetInt("PickupHealthGainTemp") + PlayerPrefs.GetInt("PickupHealthGain"));
        else
            PlayerPrefs.SetInt("PickupHealthGain", PlayerPrefs.GetInt("PickupHealthGainTemp"));
        if (PlayerPrefs.HasKey("PickupManaGain"))
            PlayerPrefs.SetInt("PickupManaGain", PlayerPrefs.GetInt("PickupManaGainTemp") + PlayerPrefs.GetInt("PickupManaGain"));
        else
            PlayerPrefs.SetInt("PickupManaGain", PlayerPrefs.GetInt("PickupManaGainTemp"));
    }
}
