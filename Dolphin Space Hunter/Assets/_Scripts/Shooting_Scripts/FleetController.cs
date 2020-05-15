/*
 * Copyright (c) Borja Fernández
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetController : MonoBehaviour
{

    #region Variables
    // Declaración de enums
        public enum ShipMovementPattern
    {
        Still,
        Horizontal,
        Vertical,
        Erratic,
        Circular,
        DoubleCircular
    };
        public enum FleetMovementPattern
    {
        Still,
        Horizontal,
        Vertical,
        Erratic,
        Circular
    };

    // Atributos públicos
        public GameObject ShipModel;
        public ShipMovementPattern shipsPattern;
        public FleetMovementPattern fleetPattern;

        public int initialNumberOfShips = 0;
        public float speed;

    // Atributos privados
        private List<GameObject> ships;
        private Transform fleetTransform;
        private int createdShips = 0;
        private int numberOfShips = 0;
        private Vector3 startingPosition;
        private Vector3 movementVector;

        private Vector3 shipsErraticMovement = Vector3.zero;
        private float angle;
        private float radius = 10;
        private float circleStep;

    #endregion


    #region UnityMethods

    void Start()
    {
        fleetTransform = this.gameObject.transform;
        startingPosition = fleetTransform.position;

        setPatternAttributes();
        
        //StartCoroutine(SpawnShips());
        numberOfShips = initialNumberOfShips;
    }


    private void FixedUpdate()
    {
        switch(fleetPattern)
        {
            case FleetMovementPattern.Horizontal:
            case FleetMovementPattern.Vertical:
            case FleetMovementPattern.Erratic:
                lateralMovement();
                break;
            case FleetMovementPattern.Circular:
                circularMovement();
                break;
            default:
                break;
        }
    }

    #endregion

    IEnumerator SpawnShips()
    {
        GameObject tempShipRef;
        
        while (createdShips < initialNumberOfShips)
        {
            tempShipRef = Instantiate(ShipModel, this.gameObject.transform, false);
            tempShipRef.GetComponent<ShipController>().shipPattern = shipsPattern;
            if (shipsPattern == ShipMovementPattern.Erratic)
            {
                tempShipRef.GetComponent<ShipController>().movementVector = shipsErraticMovement;
            }
            ships.Add(tempShipRef);
            createdShips++;
            yield return new WaitForSecondsRealtime(1.3f);
        }
    }

    private void setPatternAttributes()
    {
        if (fleetPattern == FleetMovementPattern.Horizontal)
        {
            movementVector = new Vector3(1f, 0f, 0f);
        }
        if (fleetPattern == FleetMovementPattern.Vertical)
        {
            movementVector = new Vector3(0f, 1f, 0f);
        }
        // Si la flota tiene un patrón de movimiento errático,
        // creamos para ella un vector de movimiento
        if (fleetPattern == FleetMovementPattern.Erratic)
        {
            movementVector = new Vector3(Random.Range(0.2f, 1f),
                                                Random.Range(0.2f, 1f),
                                                0f);
        }
        // Si las naves de la flota tienen un patrón de movimiento errático,
        // creamos para ellas un vector de movimiento diferente
        if (shipsPattern == ShipMovementPattern.Erratic)
        {
            shipsErraticMovement = new Vector3(Random.Range(0.2f, 1f),
                                            Random.Range(0.2f, 1f),
                                            0f);
        }
        circleStep = 360f * Time.fixedDeltaTime;
        angle = 0f;
    }

    private void lateralMovement()
    {
        checkInBounds();
        // Multiplicamos el movimiento por Time.fixedDeltaTime (el tiempo de ejecución de cada paso de físicas
        // para coordinar el movimiento de la flota con el de los cuerpos físicos de la escena.
        fleetTransform.Translate(movementVector * speed);
    }

    private void circularMovement()
    {
        // Usamos las funciones de seno y coseno para calcular la siguiente posición de la nave
        movementVector.x = Mathf.Cos((angle + 45f) * Mathf.Deg2Rad);
        movementVector.y = Mathf.Sin((angle + 45f) * Mathf.Deg2Rad);

        // "Reseteamos" la variable para evitar desbordamientos
        angle += circleStep;
        if (angle > 360f)
        {
            angle -= 360f;
        }

        fleetTransform.Translate(movementVector * radius, fleetTransform);
    }

    // Método para asegurarnos de que, en los movimientos no-circulares, nos mantenemos
    // en un rango de espacio concreto alrededor de la posición inicial
    private void checkInBounds()
    {
        if (Mathf.Abs(fleetTransform.position.x - startingPosition.x) > 500)
        {
            movementVector.x *= -1;
        }
        if (Mathf.Abs(fleetTransform.position.y - startingPosition.y) > 500)
        {
            movementVector.y *= -1;
        }
    }

    public void shipDestroyed(GameObject ship)
    {
        //ships.Remove(ship);
        ship.SetActive(false);
        numberOfShips--;
        if (numberOfShips <= 0)
        {
            ShooterGameDirector.Instance().fleetDestroyed(this);
            Debug.Log("Fleet destroyed!");
        }
    }
}
