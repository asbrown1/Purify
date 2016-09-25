using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowPlayerHealthText : MonoBehaviour {

    public GameObject player;
    Health getHealth;
    Text healthText;
    // Use this for initialization
    void Start () {
        getHealth = player.GetComponent<Health>();
        healthText = this.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        healthText.text = "Health (" + getHealth.getHealth() + "/" + getHealth.getMaxHealth() + ")";
	}
}
