/*
 * Copyright (c) Sergio Ruiz
 * 
 * 
 * 
 * 
 */



using System.Collections;
using System.Collections.Generic;
using Vuforia;
using UnityEngine;

public class AsteroidMovable : MonoBehaviour
{
    public GameObject Atype1, Atype2, Atype3;
    private GameObject asteroid;
    private float initX, initY, initZ = 2000;
    public Camera MyCamera;
    public float vel,radio;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("createAsteroid");
        metodoPrueba();
    }


    // Update is called once per frame
    void Update()
    {
    }


    IEnumerator createAsteroid()
    {
        while (true)
        {
            int tipoA = Random.Range(0, 3);
            initX = Random.Range(-Screen.width/2,Screen.width / 2);
            initY = Random.Range(-Screen.height/2, Screen.height/2);
            initX += MyCamera.transform.position.x;
            initY += MyCamera.transform.position.y;
            switch (tipoA)
            {
                case 0: asteroid = Atype1;break;//Instantiate(Atype1, new Vector3(initX, initY, initZ), Quaternion.identity, imagen.transform); break;  //genera asteroides
                case 1: asteroid = Atype2;break;// Instantiate(Atype2, new Vector3(initX, initY, initZ), Quaternion.identity, imagen.transform); break;
                case 2: asteroid = Atype3;break;// Instantiate(Atype3, new Vector3(initX, initY, initZ), Quaternion.identity, imagen.transform); break;
                default: break;
            }
            asteroid = Instantiate(asteroid, new Vector3(initX, initY, initZ), Quaternion.identity);
            asteroid.GetComponent<Rigidbody>().velocity = new Vector3(0f,0f,vel);
            asteroid.transform.localScale = new Vector3(75f, 75f, 75f);//tam asteroides DIFICULTA cambiar
            asteroid.GetComponent<SphereCollider>().radius = radio;
            yield return new WaitForSecondsRealtime(1.0f);
        }

    }

    private void metodoPrueba()
    {
        ScoreboardContainer difMedia = new ScoreboardContainer();
        difMedia.difficultyMode = "Medio";
        difMedia.players.Add("Borja");
        difMedia.players.Add("Antonio");
        difMedia.players.Add("Jose");
        difMedia.scores.Add(14000);
        difMedia.scores.Add(7500);
        difMedia.scores.Add(5000);

        ScoreboardManager.loadScoreboard(difMedia);
        ScoreboardManager.storeJson(difMedia);
    }
}
