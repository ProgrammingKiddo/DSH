using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using TMPro;

public class my_intro_video : MonoBehaviour
{
    public TextMeshProUGUI loadingText, skipText;
    public TextAsset easySettingsFile, normalSettingsFile, hardSettingsFile;
    private VideoPlayer video;
    private bool difficultyLoad;
    private ShootingLevels_Container difficultySettings;
    // Start is called before the first frame update
    void Start() 
    {
        difficultyLoad = false;
        difficultySettings = new ShootingLevels_Container();
        video=this.GetComponent<VideoPlayer>();
        video.loopPointReached += LoadScene;

        switch(PlayerPrefs.GetString("Difficulty", "Easy")){
            case "Easy":   JsonUtility.FromJsonOverwrite(easySettingsFile.text,   difficultySettings);break;
            case "Normal": JsonUtility.FromJsonOverwrite(normalSettingsFile.text, difficultySettings);break;
            case "Hard":   JsonUtility.FromJsonOverwrite(hardSettingsFile.text,   difficultySettings);break;
        }

        PlayerPrefs.SetInt("MaxAmmo", difficultySettings.maxAmmunition);
        PlayerPrefs.SetInt("Ammo", difficultySettings.maxAmmunition);
        PlayerPrefs.SetInt("Shield", 100);
        PlayerPrefs.SetInt("ShielRechargeRate", difficultySettings.shieldRechargeRate);
        PlayerPrefs.SetInt("PlayerScore", 0);

        loadingText.gameObject.SetActive(false);
        skipText.gameObject.SetActive(true);
        difficultyLoad = true;
    }

    void Update()
    {
        if(Input.touchCount==1 && Input.GetTouch(0).phase == TouchPhase.Began && difficultyLoad){
            SceneManager.LoadScene( "ShootingScene" );
        }
    }

    void LoadScene(VideoPlayer vp)
    {
        SceneManager.LoadScene( "ShootingScene" );
    }
}

