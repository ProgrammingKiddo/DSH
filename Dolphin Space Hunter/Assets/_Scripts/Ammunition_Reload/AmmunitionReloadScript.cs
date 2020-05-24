using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AmmunitionReloadScript : MonoBehaviour
{
    public int indicatorSpeed;
    public Text ammunitionCounter, scorePanel;
    public ProgressBar shieldBar;

    private int absMax = 49;
    private int currentAmmunition, maxAmmunition, currentScore;
    private float currentShield;
    //Color Ranges:
    //-49[RED]-45[YELLOW]-6[GREEN]5[YELLOW]41[RED]49
    private int[] colorBarRanges = {-45, -6, 5, 41};
    // Start is called before the first frame update
    void Start()
    {
        currentAmmunition = PlayerPrefs.GetInt("Ammo", 0);
        maxAmmunition = PlayerPrefs.GetInt("MaxAmmo", 50);
        currentShield = PlayerPrefs.GetFloat("Shield", 100f);
        currentScore = PlayerPrefs.GetInt("PlayerScore", 0);
        indicatorSpeed = PlayerPrefs.GetInt("reloadBarSpeed", 250);

        shieldBar.BarValue = currentShield;
        ammunitionCounter.text = currentAmmunition.ToString() + "/" + maxAmmunition.ToString();
        setAmmounitionCounterColor();
        scorePanel.text = "Score: " + currentScore.ToString();

        StartCoroutine(reloadAction());
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown("space") && currentAmmunition<maxAmmunition){
        if(Input.touchCount==1 && Input.GetTouch(0).phase == TouchPhase.Began && currentAmmunition<maxAmmunition){
            if(transform.localPosition.x<=colorBarRanges[0] || transform.localPosition.x>=colorBarRanges[3]){
                currentAmmunition += (maxAmmunition/10);    //Red reload => +10% of ammo
            }else if(transform.localPosition.x <=colorBarRanges[1] || transform.localPosition.x>=colorBarRanges[2]){
                currentAmmunition += (maxAmmunition/4);     //Yellow reload => +25% of ammo
            }else{
                currentAmmunition += (maxAmmunition/2);     //Green reload => +50% of ammo
            }
            if(currentAmmunition > maxAmmunition){
                currentAmmunition = maxAmmunition;
            }
            ammunitionCounter.text = currentAmmunition.ToString() + "/" + maxAmmunition.ToString();
            setAmmounitionCounterColor();
        }
    }

    IEnumerator reloadAction(){
        bool derecha = true;
        while(true){
            if(transform.localPosition.x <= -absMax){
                derecha = true;
            }else if(transform.localPosition.x >= absMax){
                derecha = false;
            }

            if(derecha){
                transform.Translate(Vector3.right * indicatorSpeed * Time.deltaTime);
            }else{
                transform.Translate(Vector3.left * indicatorSpeed * Time.deltaTime);
            }
            
            yield return new WaitForSecondsRealtime(0.01f);
        }
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
