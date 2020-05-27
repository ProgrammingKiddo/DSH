/*
 * Copyright (c) Borja Fernández
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    #region Variables
    public float projectileSpeed;
    public int damage = 5;

    private Transform bulletTransform;
    private Vector3 originalScale;

    private float distanceToCamera;
    #endregion


    #region UnityMethods

    void Start()
    {
        //GetComponent<Rigidbody>().velocity = this.transform.forward * projectileSpeed;
        bulletTransform = GetComponent<Transform>();
        originalScale = bulletTransform.localScale;
        GetComponentInChildren<ParticleSystem>().Play();
    }

    void Update()
    {
        /*if (this.gameObject.CompareTag("PlayerProjectile") == true)
        {
            distanceToCamera = Vector3.Distance(Camera.main.transform.position, bulletTransform.position);
            bulletTransform.localScale = originalScale * Mathf.Clamp((distanceToCamera / Camera.main.farClipPlane), 0.15f, 0.4f);

        }*/
            // Si el proyectil sale del rango de visión del eje Z, lo eliminamos
            if (bulletTransform.position.z > Camera.main.farClipPlane
                || bulletTransform.position.z < Camera.main.nearClipPlane)
            {
                // Si el proyectil perdido es del jugador, le restamos puntos
                if (this.gameObject.CompareTag("PlayerProjectile") == true)
                {
                    ShooterGameDirector.Instance().missedShot();
                }
                Destroy(this.gameObject);
            }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (this.gameObject.CompareTag("EnemyProjectile") == true
            && collision.gameObject.CompareTag("Enemy") == true)
        {
            // No hacer nada
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    #endregion
}
