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
    private bool sinEscudo, isRunning; //Global para comprobar si tenemois escudo
    private int escudoActual;//Escudo actual
    public float startDuration,shakeDuration, startAmount, shakeAmount, smoothAmount; //shake
    AudioSource sonidoChoque;
    Vector3 valorCrudo = Vector3.zero;
    public GameObject explosion;


    void Start()
    {
        isRunning = false;
        canvasVida.golpeado(100);
        escudoActual = 100;//
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
            canvasVida.golpeado(escudoActual);
            ShakeCamera();
            if (escudoActual <= 0)
            {
                if (sinEscudo)
                {
                    Instantiate(explosion, new Vector3(0, -2, 5), Quaternion.identity);
                    //Insertar espera?
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

