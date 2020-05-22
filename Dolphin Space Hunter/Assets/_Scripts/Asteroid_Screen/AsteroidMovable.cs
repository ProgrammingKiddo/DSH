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
    public Camera MyCamera;
    public float vel;

    private GameObject asteroid;
    private float initX, initY, initZ = 2000;

    public TextAsset JsonFile;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("createAsteroid");
        metodoPrueba();
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
                case 0: asteroid = Atype1;break;
                case 1: asteroid = Atype2;break;
                case 2: asteroid = Atype3;break;
                default: break;
            }
            asteroid = Instantiate(asteroid, new Vector3(initX, initY, initZ), Quaternion.identity);
            asteroid.GetComponent<Rigidbody>().velocity = new Vector3(0f,0f,vel);
            asteroid.transform.localScale = new Vector3(75f, 75f, 75f);//tam asteroides DIFICULTA cambiar
            yield return new WaitForSecondsRealtime(1.0f);
        }

    }

    private void metodoPrueba()
    {
        ScoreboardContainer difMedia = new ScoreboardContainer();
            difMedia.difficultyMode = "Difícil";
            difMedia.players.Add("Borja");
            difMedia.players.Add("Pablo");
            difMedia.players.Add("Pocoyo");
            difMedia.scores.Add(140000);
            difMedia.scores.Add(75);
            difMedia.scores.Add(2);
        ScoreboardContainer difficulty = new ScoreboardContainer();
        JsonManager.loadFromJson(JsonFile, difficulty);
        Debug.Log("Modo: " + difficulty.difficultyMode + ", Jugador TOP: " + difficulty.players[0] + ", Puntuación TOP:" + difficulty.scores[0]);


        //ScoreboardManager.loadScoreboard(difMedia);
        JsonManager.storeToJson(JsonFile ,difMedia);
    }
}
