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

        // Si no existe la tabla de puntuación, creamos una por defecto
        if (!PlayerPrefs.HasKey("Scoreboard"))
        {
            PlayerPrefs.SetString("Scoreboard", JsonUtility.ToJson(defaultScoreboard));
        }
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
