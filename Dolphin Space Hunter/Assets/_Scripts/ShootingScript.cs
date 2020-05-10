/*
 * Copyright (c) Borja Fernández
 *
 * Esta clase gestiona las pulsaciones en la pantalla de disparo,
 * y genera un objeto proyectil, al que le otorga una velocidad fijada por el mismo
 * en la dirección frontal a la que apunta desde la pantalla.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{

    #region Variables
    public Camera gameCamera;
    public GameObject Bullet;
    public float bulletVelocity;


    Touch shootTouch;
    bool shooting = false;
    Vector3 shootTarget;
    Vector3 screenCenter;
    private GameObject bulletShot;
    
    #endregion


    #region UnityMethods

    void Start()
    {
        screenCenter = new Vector3(gameCamera.pixelWidth / 2, gameCamera.pixelHeight / 2, 0f);
    }

    void Update()
    {
        if (Input.touchCount > 0 && !shooting)
        {
            shootTouch = Input.GetTouch(0);
            shooting = true;
            shootTarget = gameCamera.ScreenToWorldPoint(screenCenter);
        }
        
    }

    private void FixedUpdate()
    {
        if (shooting)
        {
            Debug.Log("Shooting!");
            bulletShot = Instantiate(Bullet, this.transform.position, this.transform.rotation);
            bulletShot.GetComponent<Rigidbody>().velocity = this.transform.forward * bulletVelocity;
            shooting = false;
        }
    }

    #endregion
}
