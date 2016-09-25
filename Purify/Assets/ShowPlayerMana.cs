using UnityEngine;
using System.Collections;

public class ShowPlayerMana : MonoBehaviour
{
    public GameObject player;
    Mana getMana;
    RectTransform manaBar;
    float manaBarMaxWidth;
    float manaBarHeight;
    // Use this for initialization
    void Start()
    {
        manaBar = this.GetComponent<RectTransform>();
        getMana = player.GetComponent<Mana>();
        manaBarMaxWidth = manaBar.rect.width;
        manaBarHeight = manaBar.rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        float percentMana = (float)getMana.getMana() / getMana.getMaxMana();
        manaBar.sizeDelta = new Vector2(manaBarMaxWidth * percentMana, manaBarHeight);

    }
}
