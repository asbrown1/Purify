using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowPlayerManaText : MonoBehaviour
{

    public GameObject player;
    Mana getMana;
    Text manaText;
    // Use this for initialization
    void Start()
    {
        getMana = player.GetComponent<Mana>();
        manaText = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        manaText.text = "Mana (" + getMana.getMana() + "/" + getMana.getMaxMana() + ")";
    }
}
