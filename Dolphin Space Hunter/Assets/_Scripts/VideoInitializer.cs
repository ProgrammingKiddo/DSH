using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using TMPro;

public class VideoInitializer : MonoBehaviour
{
    public TextMeshProUGUI loadingText, skipText;
    public TextAsset easySettingsFile, normalSettingsFile, hardSettingsFile;
    private VideoPlayer video;
    private DifficultySettingsContainer difficultySettings;
    private AsyncOperation loadingScene;

    void Start() 
    {
        // Cargamos la escena de forma asíncrona, y desactivamos su carga automática
        loadingScene = SceneManager.LoadSceneAsync("AsteroidScene");
        loadingScene.allowSceneActivation = false;

        video = this.GetComponent<VideoPlayer>();
        // Si el vídeo ya ha terminado, también permitimos la carga de la escena
        video.loopPointReached += allowSceneLoad;
     
        loadDifficultySettings();
    }


    void Update()
    {
        // Si la escena ya se ha cargado, y el jugador pulsa para saltarse la intro, permitimos cargarla
        if (loadingScene.progress >= 0.9f)
        {
            loadingText.gameObject.SetActive(false);
            skipText.gameObject.SetActive(true);
            if (Input.touchCount==1 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0)){
                loadingScene.allowSceneActivation = true;
            }
        }
    }

    private void loadDifficultySettings()
    {
        difficultySettings = new DifficultySettingsContainer();

        switch (PlayerPrefs.GetString("DifficultyMode", "Easy")){
            case "Easy":
                JsonUtility.FromJsonOverwrite(easySettingsFile.text, difficultySettings);
                break;
            case "Normal":
                JsonUtility.FromJsonOverwrite(normalSettingsFile.text, difficultySettings);
                break;
            case "Hard":
                JsonUtility.FromJsonOverwrite(hardSettingsFile.text, difficultySettings);
                break;
        }

        PlayerPrefs.SetInt("MaxAmmo", difficultySettings.maxAmmunition);
        PlayerPrefs.SetInt("Ammo", difficultySettings.maxAmmunition);
        PlayerPrefs.SetInt("Shield", 100);
        PlayerPrefs.SetInt("AsteroidDamage", difficultySettings.damagePerAsteroid);
        PlayerPrefs.SetFloat("ShielRechargeRate", difficultySettings.shieldRechargeRate);
        PlayerPrefs.SetFloat("minAsteroidSpeed", difficultySettings.minAsteroidSpeed);
        PlayerPrefs.SetFloat("maxAsteroidSpeed", difficultySettings.maxAsteroidSpeed);
        PlayerPrefs.SetInt("PlayerScore", 0);
        PlayerPrefs.SetInt("WavesPerBoss", difficultySettings.wavesPerBoss);
        PlayerPrefs.SetInt("ActiveWave", 0);
    }

    private void allowSceneLoad(VideoPlayer source)
    {
        loadingScene.allowSceneActivation = true;
    }
}

