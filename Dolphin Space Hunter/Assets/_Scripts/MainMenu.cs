using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenu : MonoBehaviour
{
    public TextAsset defaultScoreboard;
    public TextMeshProUGUI difficultyButtonText;
    public TextMeshProUGUI titleText;

    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI playersText;
    public TextMeshProUGUI scoresText;
    public Image difficultyBackground;

    private ScoreboardContainer scoreboard = new ScoreboardContainer();
    private string difficultyString;

    private float lightAngleStep = 0;
    private bool decreaseAngleStep = false;

    public void Start()
    {
        // Al inicio del juego, bloqueamos la orientación del dispositivo
        // a horizontal clásico (botón "home" a la derecha)
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        // Si no existe la tabla de puntuación, creamos una por defecto
        if (!PlayerPrefs.HasKey("Scoreboard"))
        {
            PlayerPrefs.SetString("Scoreboard", defaultScoreboard.text);
        }
        difficultyString = "Easy";
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("Scoreboard"), scoreboard);
        loadScoreboardToUI();
    }

    /*public void Update()
    {
        if (lightAngleStep >= 6)
        {
            decreaseAngleStep = true;
        }
        else if (lightAngleStep <= 0)
        {
            decreaseAngleStep = false;
        }

        if (decreaseAngleStep == true)
        {
            lightAngleStep -= 0.1f;
        }
        else
        {
            lightAngleStep += 0.1f;
        }

        titleText.material.SetFloat("_LightAngle", lightAngleStep);
    }*/

    public void PlayGame()
    {
        PlayerPrefs.SetString("DifficultyMode", difficultyString);
        SceneManager.LoadScene("IntroVideoScene");
    }

    public void changeDifficulty(){
        switch(difficultyButtonText.text){   
            case "facil":   {
                difficultyString = "Normal";
                difficultyButtonText.text = "normal";
                difficultyBackground.color = new Color32(255, 255, 0, 100);
            } break;
            case "normal":  {
                difficultyString = "Hard";
                difficultyButtonText.text = "dificil";
                difficultyBackground.color = new Color32(255, 0, 0, 100);
            }break;
            case "dificil": {
                difficultyString = "Easy";
                difficultyButtonText.text = "facil";
                difficultyBackground.color = new Color32(0, 255, 0, 100);
            }break;
        }
        loadScoreboardToUI();
    }

    
    public void ExitGame()
    {
        Application.Quit();
        
    }

    
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
        
    }

    private void loadScoreboardToUI()
    {
        int lastScore = 0;
        switch (difficultyString)
        {
            case "Easy":
                lastScore = 0;
                difficultyText.text = "Fácil";
                break;
            case "Normal":
                lastScore = 3;
                difficultyText.text = "Normal";

                break;
            case "Hard":
                lastScore = 6;
                difficultyText.text = "Difícil";
                break;
        }
        // Cargamos por orden descendente la información de la tabla de puntuación
        playersText.text = scoreboard.players[lastScore + 2] + "\n" +
                            scoreboard.players[lastScore + 1] + "\n" +
                            scoreboard.players[lastScore];
        scoresText.text = scoreboard.scores[lastScore + 2] + "\n" +
                            scoreboard.scores[lastScore + 1] + "\n" +
                            scoreboard.scores[lastScore];
    }
}
