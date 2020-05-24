using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldScript : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform rectTransform;
    public Text scorePanel, ammunitionCounter;
    public static float Shield{get; set;}
    private int currentScore, currentAmmunition, maxAmmunition;

    // Start is called before the first frame update
    void Start()
    {
        Shield = PlayerPrefs.GetFloat("Shield", 0f);
        currentAmmunition = PlayerPrefs.GetInt("Ammo", 0);
        maxAmmunition = PlayerPrefs.GetInt("maxAmmo", 50);
        currentScore = PlayerPrefs.GetInt("PlayerScore", 0);
        
        scorePanel.text = "Score: " + currentScore.ToString();
        ammunitionCounter.text = currentAmmunition.ToString() + "/" + maxAmmunition.ToString();
        setAmmounitionCounterColor();

        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        float UpdateShield = Mathf.MoveTowards(rectTransform.rect.height, Shield, 5.0f);
        rectTransform.sizeDelta = new Vector2(100f, Mathf.Clamp(UpdateShield, 0.0f, 100f));
    }

    void setAmmounitionCounterColor(){
        if(currentAmmunition <= (maxAmmunition*0.1)){
            ammunitionCounter.color = Color.red;
        }else if(currentAmmunition <= (maxAmmunition*0.75)){
            ammunitionCounter.color = Color.yellow;
        }else{
            ammunitionCounter.color = Color.green;
        }
    }
}