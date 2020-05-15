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

    public GameObject ShipModel;
    public enum ShipMovementPattern
    {
        Still,
        Horizontal,
        Vertical,
        Erratic,
        Circular,
        DoubleCircular
    };
    public ShipMovementPattern shipsPattern;

    public enum FleetMovementPattern
    {
        Still,
        Horizontal,
        Vertical,
        Erratic,
        Circular
    };
    public FleetMovementPattern fleetPattern;

    public int initialNumberOfShips = 0;
    public int behaviour;
    public float speed;

    private List<GameObject> ships;
    private Transform fleetTransform;
    private int createdShips = 0;
    private int numberOfShips = 0;
    private Vector3 startingPosition;
    private Vector3 fleetMovementVector;

    private Vector3 shipsErraticMovement = Vector3.zero;
    #endregion


    #region UnityMethods

    void Start()
    {
        fleetTransform = this.gameObject.transform;
        startingPosition = fleetTransform.position;

        // Si la flota tiene un patrón de movimiento errático,
        // creamos para ella un vector de movimiento
        if (fleetPattern == FleetMovementPattern.Erratic)
        {
            fleetMovementVector = new Vector3(Random.Range(0.2f, 1f),
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
        //StartCoroutine(SpawnShips());
        numberOfShips = initialNumberOfShips;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(fleetTransform.position.x - startingPosition.x) > 500)
        {
            fleetMovementVector.x *= -1;
        }
        if (Mathf.Abs(fleetTransform.position.y - startingPosition.y) > 500)
        {
            fleetMovementVector.y *= -1;
        }

        // Multiplicamos el movimiento por Time.fixedDeltaTime (el tiempo de ejecución de cada paso de físicas
        // para coordinar el movimiento de la flota con el de los cuerpos físicos de la escena.
        fleetTransform.Translate(fleetMovementVector * speed);
        
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
