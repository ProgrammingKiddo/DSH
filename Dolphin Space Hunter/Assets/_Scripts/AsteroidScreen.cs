using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vuforia;
using UnityEngine.SceneManagement;
using System.Threading;

public class AsteroidScreen : MonoBehaviour
{
    public ImageTargetBehaviour imagenAsteroides;
    private bool sc, cs,sinEscudo;
    private Thread create,hit;
    public GUISteroid canvasVida;
    private int aux=0,actual=100;
    public Camera camaraAr;

    // Start is called before the first frame update
    void Start()
    {
        sc = true;
        cs = true;
        sinEscudo = false;
        hit = new Thread(isHit);
        //  change = new Thread(ChangeScreen);
        hit.Start();
        //  change.Start();
        //gol.Start();
        canvasVida = GetComponent<GUISteroid>();
        imagenAsteroides = GetComponent<ImageTargetBehaviour>();
        camaraAr = GetComponent<Camera>();


    }

    // Update is called once per frame
    void Update()
    {

    }
    private void isHit()
    {
       // while (true)
       // {
            if (sc)
            {
                actual = actual - 10; //facil
                canvasVida.setActual(actual);
                canvasVida.golpeado();
            if (actual <= 0)
            {
                if (sinEscudo)
                {
                    SceneManager.LoadScene("gameOver");//cargar GAME OVER

                }else{
                    sinEscudo = true;
                }
            }
                sc = false;
            }
      //  }

    }

   /* IEnumerator CrearAsteroides()
    {

        //Thread.Sleep(1000);


        yield return new WaitForSeconds(10.1f);


    }*/

    private void CreateSteroid()
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
            Debug.Log("co2");
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

