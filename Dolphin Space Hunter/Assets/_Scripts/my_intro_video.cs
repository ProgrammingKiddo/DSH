using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class my_intro_video : MonoBehaviour
{
    private VideoPlayer video;
    // Start is called before the first frame update
   void Start() 
     {
         video=this.GetComponent<VideoPlayer>();
          video.loopPointReached += LoadScene;
     }
     void LoadScene(VideoPlayer vp)
     {
          SceneManager.LoadScene( "ShootingScene" );
      }
}

