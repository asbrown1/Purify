using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
    public string type;
    public int amount;
    public bool permanent;
    public float rotationSpeed = 5;
	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("PickupHealthGainTemp", 0);
        PlayerPrefs.SetInt("PickupManaGainTemp", 0);
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(0, rotationSpeed, 0);
	}
    void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.tag.Equals("Player"))
        {
            if(type.Equals("Health"))
            {
                Health health = other.transform.root.GetComponent<Health>();
                health.addPickupHealth(amount);
                if(permanent)
                {
                    if (PlayerPrefs.HasKey("PickupHealthGainTemp"))
                    {
                        PlayerPrefs.SetInt("PickupHealthGainTemp", PlayerPrefs.GetInt("PickupHealthGainTemp") + amount);
                    }
                    else
                        PlayerPrefs.SetInt("PickupHealthGainTemp", amount);
                }
            }
            if(type.Equals("Mana"))
            {
                Mana mana= other.transform.root.GetComponent<Mana>();
                mana.addPickupMana(amount);
                if (permanent)
                {
                    if (PlayerPrefs.HasKey("PickupManaGainTemp"))
                    {
                        PlayerPrefs.SetInt("PickupManaGainTemp", PlayerPrefs.GetInt("PickupManaGainTemp") + amount);
                    }
                    else
                        PlayerPrefs.SetInt("PickupManaGainTemp", amount);
                }
            }
            Destroy(this.gameObject);
        }
    }
}
