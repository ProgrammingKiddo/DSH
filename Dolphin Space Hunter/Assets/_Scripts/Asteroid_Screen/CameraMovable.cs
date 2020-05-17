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
using UnityEngine.SceneManagement;


public class CameraMovable : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera ArCamera;
    float IntervaloAc = 1.0f / 30; //valor actualizacion;
    float LowPassKernelWidthInSeconds = 1.0f;
    float factorFiltro = 0;
    public GUISteroid canvasVida;
    private bool sinEscudo;
    private int actual;
    AudioSource sonidoChoque;
    Vector3 valorCrudo = Vector3.zero;

    void Start()
    {
        canvasVida.golpeado(100);
        actual = 100;
        factorFiltro = IntervaloAc / LowPassKernelWidthInSeconds;
        valorCrudo = Input.acceleration;
        sonidoChoque = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        actualizaCamara();
        
    }

    private Vector3 FiltradoAccelValor()
    {
        Debug.Log("NO FILTRADO " + Input.acceleration);
        Debug.Log("fILTREADO " + Vector3.Lerp(valorCrudo, Input.acceleration, factorFiltro));
        return Vector3.Lerp(valorCrudo, Input.acceleration, factorFiltro);
     
    }
    private void actualizaCamara()
    {
        float speed = 200.0f;
        Vector3 prueba  = Vector3.zero;

        // we assume that the device is held parallel to the ground
        // and the Home button is in the right hand

        // remap the device acceleration axis to game coordinates:
        // 1) XY plane of the device is mapped onto XZ plane
        // 2) rotated 90 degrees around Y axis
        prueba.x = -Input.acceleration.y;
        prueba.z = Input.acceleration.x;

        // clamp acceleration vector to the unit sphere
        if (prueba.sqrMagnitude > 1)
            prueba.Normalize();

        prueba *= Time.deltaTime;
        prueba *= speed;
        Vector3 newPosition = new Vector3(ArCamera.transform.position.x + prueba.z, ArCamera.transform.position.y + prueba.x, 0);


        // Move object
        // ArCamera.gameObject.Translate(prueba * speed);
        ArCamera.transform.position= newPosition;




      /*  Vector3 acelerometroValor = FiltradoAccelValor();
        Vector3 newPosition = new Vector3(ArCamera.transform.position.x + acelerometroValor.x,ArCamera.transform.position.y+acelerometroValor.y, ArCamera.transform.position.z);
        ArCamera.transform.position = newPosition;
        Debug.Log(ArCamera.transform.position);*/
    }

    void OnGUI()
    {
        GUILayout.Label("NO FILTRADO " + Input.acceleration + " Fil " + Vector3.Lerp(valorCrudo, Input.acceleration, factorFiltro)+"Camera "+ArCamera.transform.position);

    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("cOLISSION");
        if (other.gameObject.tag == "Enemy")
        {
            sonidoChoque.Play();
            actual -= 10; //facil
            canvasVida.golpeado(actual);
            if (actual <= 0)
            {
                if (sinEscudo)
                {
                    SceneManager.LoadScene("gameOver");//cargar GAME OVER

                }
                else
                {
                    sinEscudo = true;
                }
            }
        }

    }

}

