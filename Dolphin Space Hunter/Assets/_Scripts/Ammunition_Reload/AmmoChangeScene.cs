

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Vuforia;
using UnityEngine.SceneManagement;


public class AmmoChangeScene : MonoBehaviour, ITrackableEventHandler
{

    private TrackableBehaviour mTrackableBehaviour;


    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();

    }


    void Update()
    {

        mTrackableBehaviour.RegisterTrackableEventHandler(this);


    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED)
        {
            switch (mTrackableBehaviour.TrackableName)
            {
                case "AmmunitionReloadScene":
                    SceneManager.LoadScene("AmmunitionReloadScene");
                    break;

            }
        }
    }

}