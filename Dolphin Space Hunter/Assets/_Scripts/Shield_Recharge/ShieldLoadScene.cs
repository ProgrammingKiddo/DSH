

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Vuforia;
using UnityEngine.SceneManagement;


public class ShieldLoadScene : MonoBehaviour, ITrackableEventHandler
{
    bool imagenReconocida;

    private TrackableBehaviour mTrackableBehaviour;


    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        imagenReconocida = false;


    }


    void Update()
    {

        mTrackableBehaviour.RegisterTrackableEventHandler(this);


    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (!imagenReconocida)
        {

            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED)
            {
                PlayerPrefs.SetInt("Shield", (int)ShieldScript.Shield);
                imagenReconocida = true;
                SceneManager.LoadScene(mTrackableBehaviour.TrackableName);
            }
        }
    }

}