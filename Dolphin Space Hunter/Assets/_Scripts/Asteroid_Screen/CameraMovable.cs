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
using TMPro;



public class CameraMovable : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera ArCamera;
    float IntervaloAc = 1.0f / 30; //valor actualizacion;
    float LowPassKernelWidthInSeconds = 1.0f;
    float factorFiltro = 0;
    public GUISteroid canvasVida;
    private bool sinEscudo, isRunning; //Global para comprobar si tenemois escudo
    private int escudoActual,currentScore, currentAmmunition, maxAmmunition;//Escudo actual
    public TextMeshProUGUI scorePanel, ammunitionCounter;
    public float startDuration,shakeDuration, startAmount, shakeAmount, smoothAmount; //shake
    AudioSource sonidoChoque;
    Vector3 valorCrudo = Vector3.zero;
    public GameObject explosion;



    void Start()
    {
        escudoActual = 50;// = PlayerPrefs.GetInt("Shield", 50);
        currentScore = PlayerPrefs.GetInt("PlayerScore", 0);
        int aux = PlayerPrefs.GetInt("sinEscudo", 0);
        currentAmmunition =10;// = PlayerPrefs.GetInt("Ammo", 0);
        maxAmmunition = 50;// PlayerPrefs.GetInt("MaxAmmo", 50);
        if (aux == 1) { sinEscudo = true; } else { sinEscudo = false; }
        scorePanel.text = "Score: " + currentScore.ToString();
        isRunning = false;
        canvasVida.golpeado(escudoActual);
        ammunitionCounter.text = currentAmmunition.ToString() + "/" + maxAmmunition.ToString();
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
        float speed = 350.0f;
        Vector3 prueba  = Vector3.zero;

        prueba.x = -Input.acceleration.y;
        prueba.z = Input.acceleration.x;

        // clamp acceleration vector to the unit sphere
        if (prueba.sqrMagnitude > 1)
            prueba.Normalize();

        prueba *= Time.deltaTime;
        prueba *= speed;
        Vector3 newPosition = new Vector3(ArCamera.transform.position.x + prueba.z, ArCamera.transform.position.y + prueba.x, 0);

        // ArCamera.gameObject.Translate(prueba * speed);
        ArCamera.transform.position= newPosition;

      /*  Vector3 acelerometroValor = FiltradoAccelValor();
        Vector3 newPosition = new Vector3(ArCamera.transform.position.x + acelerometroValor.x,ArCamera.transform.position.y+acelerometroValor.y, ArCamera.transform.position.z);
        ArCamera.transform.position = newPosition;
        Debug.Log(ArCamera.transform.position);*/
    }

   /* void OnGUI()
    {
        GUILayout.Label("NO FILTRADO " + Input.acceleration + " Fil " + Vector3.Lerp(valorCrudo, Input.acceleration, factorFiltro)+"Camera "+ArCamera.transform.position);

    }*/

    void ShakeCamera()
    {

        startAmount = 40;//Set default (start) values
        startDuration = 40;//Set default (start) values
        shakeDuration = 20;
        if (!isRunning) { StartCoroutine(Shake()); Debug.Log("eNTRO"); }//Only call the coroutine if it isn't currently running. Otherwise, just set the variables.
    }

	IEnumerator Shake() {
		isRunning = true;
        float shakePercentage;
        bool smooth = true;
		while (shakeDuration > 0.01f) {
			Vector3 rotationAmount = Random.insideUnitSphere * shakeAmount;//A Vector3 to add to the Local Rotation
			rotationAmount.z = 0;//Don't change the Z; it looks funny.
 
			shakePercentage = shakeDuration / startDuration;//Used to set the amount of shake (% * startAmount).
 
			shakeAmount = startAmount * shakePercentage;//Set the amount of shake (% * startAmount).
			shakeDuration = Mathf.Lerp(shakeDuration, 0, Time.deltaTime);//Lerp the time, so it is less and tapers off towards the end.
 
 
			if(smooth)
				 transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rotationAmount), Time.deltaTime * smoothAmount);
			else
				transform.localRotation = Quaternion.Euler (rotationAmount);//Set the local rotation the be the rotation amount.
 
			yield return null;
		}
        transform.localRotation = Quaternion.identity;//Set the local rotation to 0 when done, just to get rid of any fudging stuff.
		isRunning = false;
	}
 


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            sonidoChoque.Play();
            escudoActual -= 10; //facil
            PlayerPrefs.SetInt("Shield", escudoActual);
            canvasVida.golpeado(escudoActual);
            ShakeCamera();
            if (escudoActual <= 0)
            {
                if (sinEscudo)
                {
                    Instantiate(explosion, new Vector3(transform.position.x, transform.position.y-2f, transform.position.z+5f), Quaternion.identity);
                    
                    SceneManager.LoadScene("GameOverScene");//cargar GAME OVER
                }
                else
                {
                    sinEscudo = true;
                    PlayerPrefs.SetInt("sinEscudo", 1);
                }
            }
        }

    }

}

