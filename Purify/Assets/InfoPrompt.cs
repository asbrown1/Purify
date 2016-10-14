using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoPrompt : MonoBehaviour {
    Text infoText;
    public float textShowTime = 5;
    bool promptShowing = false;
    float promptTime = 0;
    // Use this for initialization
    void Start () {
        infoText = this.transform.FindChild("Info").GetComponent<Text>();
        this.GetComponent<Image>().enabled = false;
        infoText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(promptShowing)
        {
            promptTime = promptTime + Time.deltaTime;
        }
        if(promptTime>textShowTime)
        {
            hideText();
        }
	}

    public void showText(string text)
    {
        this.GetComponent<Image>().enabled = true;
        infoText.enabled = true;
        infoText.text = text;
        promptShowing = true;
    }
    public void hideText()
    {
        this.GetComponent<Image>().enabled = false;
        infoText.enabled = false;
        promptTime = 0;
        promptShowing = false;
    }
}
