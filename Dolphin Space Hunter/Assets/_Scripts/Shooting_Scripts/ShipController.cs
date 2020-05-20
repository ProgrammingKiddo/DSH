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
    // Atributos públicos
        public float radius;
        public float speed;
        public FleetController.ShipMovementPattern shipPattern;
        public int initialHealth = 10;
    
        public Vector3 movementVector = new Vector3(1f, 1f, 0f);
    Vector3 startingPosition;

    // Atributos privados
        private Transform shipTransform;
        private Transform fleetTransform;
        private int remainingHealth;
        // Atributos de movimiento
        private float circleStep;
        private float angle;
    #endregion


    #region UnityMethods

    void Start()
    {
        shipTransform = GetComponent<Transform>();
        startingPosition = shipTransform.position;
        fleetTransform = GetComponentInParent<Transform>();

        setPatternAttributes();

        remainingHealth = initialHealth;

    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        switch (shipPattern)
        {
            case FleetController.ShipMovementPattern.Horizontal:
            case FleetController.ShipMovementPattern.Vertical:
            case FleetController.ShipMovementPattern.Erratic:
                lateralMovement();
                break;
            case FleetController.ShipMovementPattern.Circular:
                circularMovement();
                break;
            case FleetController.ShipMovementPattern.DoubleCircular:
                doubleCircularMovement();
                break;
            // Still, la nave no se mueve
            default:
                break;
        }
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

    private void setPatternAttributes()
    {
        if (shipPattern == FleetController.ShipMovementPattern.Horizontal)
        {
            movementVector = new Vector3(1f, 0f, 0f);
        }
        if (shipPattern == FleetController.ShipMovementPattern.Vertical)
        {
            movementVector = new Vector3(0f, 1f, 0f);
        }

        // Para el patrón de movimiento errático, el vector de movimiento ya
        // nos viene dado desde la flota, para que todas las naves se muevan igual

        // Si la nave tiene un patrón de movimiento circular o doble-circular,
        // calculamos el nº de grados que recorrerá en cada paso de físicas
        if (shipPattern == FleetController.ShipMovementPattern.Circular
            || shipPattern == FleetController.ShipMovementPattern.DoubleCircular)
        {
            // How many seconds to perform a whole circular movement
            circleStep = 360f * Time.fixedDeltaTime;
            angle = 0f;
        }

    }

    private void lateralMovement()
    {
        checkInBounds();
        shipTransform.Translate(movementVector * speed);
    }

    private void circularMovement()
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

        shipTransform.Translate(movementVector * radius, fleetTransform);
    }

    private void doubleCircularMovement()
    {

    }

    // Método para asegurarnos de que, en los movimientos no-circulares, nos mantenemos
    // en un rango de espacio concreto alrededor de la posición inicial
    private void checkInBounds()
    {
        if (Mathf.Abs(shipTransform.position.x - startingPosition.x) > 500)
        {
            movementVector.x *= -1;
        }
        if (Mathf.Abs(shipTransform.position.y - startingPosition.y) > 500)
        {
            movementVector.y *= -1;
        }
    }
}
