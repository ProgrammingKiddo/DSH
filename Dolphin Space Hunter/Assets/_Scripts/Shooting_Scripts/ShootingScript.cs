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
    public GameObject Projectile;
    //public float bulletVelocity;
    public GameObject Gun1;
    public GameObject Gun2;

    public List<AudioClip> shootingSFX;

    Touch shootTouch;
    bool shooting = false;
    Vector3 shootTarget;
    private GameObject bulletShot;
    private Vector3 leftProjectileSpawn;
    private Vector3 rightProjectileSpawn;

    // Iterador para intercalar el disparo entre armas
    private int i = 0;
    // Iterador para intercalar el sonido de disparo
    private int j = 0;
    #endregion


    #region UnityMethods

    void Start()
    {
    }

    void Update()
    {
        leftProjectileSpawn = gameCamera.transform.position + new Vector3(-80f, -75f, 400f);
        rightProjectileSpawn = gameCamera.transform.position + new Vector3(80f, -75f, 400f);

        // Si se detecta algún toque, y no se está disparando en este momento
        if (Input.touchCount > 0 && !shooting)
        {
            // Cogemos solo el primer toque (para evitar múltiples disparos tocando con varios dedos
            shootTouch = Input.GetTouch(0);
            // Solo hacemos un disparo por toque, evitando que un toque prolongado resulte en varios disparos
            if (shootTouch.phase == TouchPhase.Began)
            {
                shooting = true;
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (shooting)
        {
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(shootingSFX[j]);
            // Alternar disparo entre ambas armas
            if (i % 2 == 0)
            {
                Gun1.GetComponent<ShootProjectile>().Shoot(Projectile);
            }
            else
            {
                Gun2.GetComponent<ShootProjectile>().Shoot(Projectile);
            }
            i++;
            j++;
            if (i == 2) i = 0;
            if (j == 3) j = 0;
            shooting = false;
        }
    }

    #endregion
}
