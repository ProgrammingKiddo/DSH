using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject musicManager;

    public void PlayGame()
    {
        SceneManager.LoadScene("PlayGameScene");

    }

    
    public void ExitGame()
    {
        Application.Quit();
        
    }

     public void Awake () {
      var go = GameObject.Find("Music"); //Finds the game object called Game Music.
      if (go != musicManager) { 
          go = musicManager; //Replaces the old audio with the new one set in the inspector.
          go.GetComponent<AudioSource>().Stop(); //Plays the audio.
      }
    }
    
    public void OptionsGame()
    {
        DontDestroyOnLoad(musicManager);   
        SceneManager.LoadScene("OptionsScene");
       
    }
}
