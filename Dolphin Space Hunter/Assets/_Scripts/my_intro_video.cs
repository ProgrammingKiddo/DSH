using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class my_intro_video : MonoBehaviour
{
    public Text skipIntro;
    private VideoPlayer video;
    // Start is called before the first frame update
   void Start() 
     {
         video=this.GetComponent<VideoPlayer>();
          video.loopPointReached += LoadScene;
     }

     void Update()
     {
        if(Input.touchCount==1 && Input.GetTouch(0).phase == TouchPhase.Began){
            if(!skipIntro.IsActive()){
                skipIntro.gameObject.SetActive(true);
            }else{
                SceneManager.LoadScene( "ShootingScene" );
            }
        }
     }
     void LoadScene(VideoPlayer vp)
     {
          SceneManager.LoadScene( "ShootingScene" );
     }
}

