/*
 * Copyright (c) Borja Fernández
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{

    #region Variables
    public float radius;
    public float speed;
    public FleetController.ShipMovementPattern dolphinPattern;
    public int initialHealth = 10;
    

    private Transform dolphinTransform;
    private Transform fleetTransform;
    private Vector3 movementVector = Vector3.zero;
    private float circleStep;
    private float angle = 0f;
    private int remainingHealth;
    #endregion


    #region UnityMethods

    void Start()
    {
        dolphinTransform = GetComponent<Transform>();
        fleetTransform = GetComponentInParent<Transform>();

        // How many seconds to perform a whole circular movement
        circleStep = 360f * Time.fixedDeltaTime;
        remainingHealth = initialHealth;

    }

    void Update()
    {
        //Debug.Log("xAxis Accel: " + Input.acceleration.x + ", yAxis Accel:" + Input.acceleration.y);
    }

    private void FixedUpdate()
    {
        // Usamos las funciones de seno y coseno para calcular la siguiente posición de la nave
        movementVector.x = Mathf.Cos((angle +45f) * Mathf.Deg2Rad);
        movementVector.y = Mathf.Sin((angle +45f) * Mathf.Deg2Rad);

        // "Reseteamos" la variable para evitar desbordamientos
        angle += circleStep;
        if (angle > 360f)
        {
            angle -= 360f;
        }

        dolphinTransform.Translate(movementVector * radius, fleetTransform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Usamos una etiqueta específica para los proyectiles del jugador para evitar
        // que las naves se dañen con sus propios proyectiles
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            ShooterGameDirector.Instance().shipHit();
            remainingHealth -= collision.gameObject.GetComponent<ProjectileScript>().damage;

            if (remainingHealth <= 0)
            {
                // Al game director le enviamos solo la información relevante: la posición de la nave
                // (a la que podemos acceder mediante el transform asociado al script) y las características
                // de la misma (que están definidas en la clase DolphinScript
                ShooterGameDirector.Instance().shipDestroyed(this);
                // A la flota le enviamos la referencia de la nave (el GO) para que lo destruya y lleve
                // la cuenta de cuántas naves de la flota quedan
                GetComponentInParent<FleetController>().shipDestroyed(this.gameObject);
            }
        }
    }

    #endregion

}
