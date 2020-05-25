using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenu : MonoBehaviour
{
    public TextAsset defaultScoreboard;
    public TextMeshProUGUI difficultyText; 
    public Image difficultyBackground;
    private string dificultyString;

    public void Start()
    {
        // Al inicio del juego, bloqueamos la orientación del dispositivo
        // a horizontal clásico (botón "home" a la derecha)
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        //Escudo al comienzo
        PlayerPrefs.SetInt("Shield", 50);
        // Si no existe la tabla de puntuación, creamos una por defecto
        if (!PlayerPrefs.HasKey("Scoreboard"))
        {
            PlayerPrefs.SetString("Scoreboard", defaultScoreboard.text);
        }
            Debug.Log("b: " + PlayerPrefs.GetString("Scoreboard"));
            Debug.Log("c: " + defaultScoreboard.text);
        dificultyString = "Easy";
    }

    public void PlayGame()
    {
        PlayerPrefs.SetString("Difficulty", dificultyString);
        SceneManager.LoadScene("IntroVideoScene");
    }

    public void changeDifficulty(){
        switch(difficultyText.text){   
            case "facil":   {
                dificultyString = "Normal";
                difficultyText.text = "normal";
                difficultyBackground.color = new Color32(255, 255, 0, 100);
            } break;
            case "normal":  {
                dificultyString = "Hard";
                difficultyText.text = "dificil";
                difficultyBackground.color = new Color32(255, 0, 0, 100);
            }break;
            case "dificil": {
                dificultyString = "Easy";
                difficultyText.text = "facil";
                difficultyBackground.color = new Color32(0, 255, 0, 100);
            }break;
        }
    }

    
    public void ExitGame()
    {
        Application.Quit();
        
    }

    
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
        
    }
}
