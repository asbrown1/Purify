using UnityEngine;
using System.Collections;

public class ShowPlayerHealth : MonoBehaviour {
    public GameObject player;
    Health getHealth;
    RectTransform healthBar;
    float healthBarMaxWidth;
    float healthBarHeight;
    // Use this for initialization
    void Start () {
        healthBar = this.GetComponent<RectTransform>();
        getHealth = player.GetComponent<Health>();
        healthBarMaxWidth = healthBar.rect.width;
        healthBarHeight = healthBar.rect.height;
	}
	
	// Update is called once per frame
	void Update () {
        float percentHealth = (float)getHealth.getHealth()/getHealth.getMaxHealth();
        healthBar.sizeDelta = new Vector2(healthBarMaxWidth * percentHealth, healthBarHeight);

    }
}
