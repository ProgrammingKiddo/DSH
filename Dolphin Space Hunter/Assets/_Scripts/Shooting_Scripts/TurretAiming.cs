/*
 * Copyright (c) Borja Fernández
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAiming : MonoBehaviour
{

    #region Variables
    public float rotationSpeed;
    
    #endregion


    #region UnityMethods

    void Update()
    {
        float yRotation = 0f;
        float xRotation = 0f;

        // z positivo = x negativo
        // z negativo = x positivo
        // x positivo = y positivo
        // x negativo = y negativo

        if (Mathf.Abs(Input.acceleration.x) > 0.2f)
        {
            yRotation = Input.acceleration.x * rotationSpeed;
        }
        if (Mathf.Abs(Input.acceleration.z) > 0.2f)
        {
            xRotation = -Input.acceleration.z * rotationSpeed;
        }
        // Sumamos la rotación a aplicar al ángulo actual
        yRotation += this.transform.rotation.y;
        xRotation += this.transform.rotation.x;
        // Y nos aseguramos de mantenerlo en un ángulo de visión de 90 grados
        yRotation = Mathf.Clamp(yRotation, -45f, 45f);
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);

        this.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            ShooterGameDirector.Instance().playerHit(collision.gameObject.GetComponent<ProjectileScript>());
            Debug.Log("I'm hit!");
        }
    }

    #endregion

}
