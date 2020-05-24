/*
 * Copyright (c) Sergio Ruiz
 * 
 * 
 * 
 * 
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vuforia;
using UnityEngine.SceneManagement;

public class AsteroidScreen : MonoBehaviour
{
    public ImageTargetBehaviour imagenAsteroides;
    private bool sc, cs;
    // Start is called before the first frame update
    void Start()
    {
        sc = true;
        cs = true;
        imagenAsteroides = GetComponent<ImageTargetBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ChangeScreen()
    {
       // while (sc)
        /*{
            if ()
            {
                SceneManager.LoadScene("<yourscenename>");

            }

            if ()
            {
                SceneManager.LoadScene("<yourscenename>");

            }
            if ()
            {
                SceneManager.LoadScene("<yourscenename>");

            }

            if ()
            {
                SceneManager.LoadScene("<yourscenename>");

            }*/
        //}

    }
}
/*
public class TargetToChangeScene : MonoBehaviour, ITrackableEventHandler

{

    #region PRIVATE_MEMBERS
    private TrackableBehaviour mTrackableBehaviour;
    #endregion // PRIVATE_MEMBERS
    #region MONOBEHAVIOUR_METHODS

    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);

        }
    }

    #endregion

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)

    {

        if (newStatus == TrackableBehaviour.Status.DETECTED ||

            newStatus == TrackableBehaviour.Status.TRACKED ||

            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)

        {
            SceneManager.LoadScene("MainMenuScene");
        }else{

        }

    }

}*/

