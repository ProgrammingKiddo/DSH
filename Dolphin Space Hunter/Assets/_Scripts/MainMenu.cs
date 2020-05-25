using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public TextAsset defaultScoreboard;

    public void Start()
    {
        // Al inicio del juego, bloqueamos la orientación del dispositivo
        // a horizontal clásico (botón "home" a la derecha)
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        //Escudo al comienzo
        PlayerPrefs.SetInt("Shield", 50);
        PlayerPrefs.SetInt("sinEscudo", 0);
        // Si no existe la tabla de puntuación, creamos una por defecto
        if (!PlayerPrefs.HasKey("Scoreboard"))
        {
            PlayerPrefs.SetString("Scoreboard", defaultScoreboard.text);
        }
            Debug.Log("b: " + PlayerPrefs.GetString("Scoreboard"));
            Debug.Log("c: " + defaultScoreboard.text);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("IntroVideoScene");

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
