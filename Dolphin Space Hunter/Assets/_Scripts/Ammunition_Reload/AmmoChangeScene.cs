

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Vuforia;
using UnityEngine.SceneManagement;


public class AmmoChangeScene : MonoBehaviour, ITrackableEventHandler
{
    public GameObject saveInformationObject;
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
            saveInformationObject.GetComponent<AmmunitionReloadScript>().saveInformation();
            SceneManager.LoadScene(mTrackableBehaviour.TrackableName);
            /*switch (mTrackableBehaviour.TrackableName)
            {
                case "AmmunitionReloadScene":
                    SceneManager.LoadScene("AmmunitionReloadScene");
                    break;

            }*/
        }
    }

}