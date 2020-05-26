/*
 * Copyright (c) Borja Fernández
 *
 */

using System.Collections;
using System.Collections.Generic;
using Vuforia;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeFromShooting : MonoBehaviour, ITrackableEventHandler
{
    public GameObject saveInformationObject;
    private TrackableBehaviour mTrackableBehaviour;
    bool loadingScene;


    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        loadingScene = false;

    }


    void Update()
    {

        mTrackableBehaviour.RegisterTrackableEventHandler(this);


    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (!loadingScene)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED)
            {
                saveInformationObject.GetComponent<ShooterGameDirector>().saveInformation();
                loadingScene = true;
                SceneManager.LoadScene(mTrackableBehaviour.TrackableName);

            }
        }
    }

}