using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Vuforia;
using UnityEngine.SceneManagement;


public class AsteroidChangeScene : MonoBehaviour, ITrackableEventHandler
{
    public GameObject saveInformationObject;
    private TrackableBehaviour mTrackableBehaviour;
    bool imagenReconocida;


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
                saveInformationObject.GetComponent<CameraMovable>().saveInformation();
                imagenReconocida = true;
                SceneManager.LoadScene(mTrackableBehaviour.TrackableName);

            }
        }
    }

}