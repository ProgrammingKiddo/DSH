/*
 * Copyright (c) Borja Fernández
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolphinScript : MonoBehaviour
{

    #region Variables
    public float radius;
    public float speed;
    public FleetController.ShipMovementPattern dolphinPattern;
    

    private Transform dolphinTransform;
    private Transform fleetTransform;
    private Vector3 movementVector = Vector3.zero;
    private float circleStep;
    private float degree = 0f;
    #endregion


    #region UnityMethods

    void Start()
    {
        dolphinTransform = GetComponent<Transform>();
        fleetTransform = GetComponentInParent<Transform>();

        // How many seconds to perform a whole circular movement
        circleStep = 360f * speed * Time.fixedDeltaTime;

    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        movementVector.x = Mathf.Cos((degree +45f) * Mathf.Deg2Rad) * radius;
        movementVector.y = Mathf.Sin((degree +45f) * Mathf.Deg2Rad) * radius;

        degree += circleStep;
        if (degree > 360f)
        {
            degree -= 360f;
        }

        dolphinTransform.Translate(movementVector * radius, fleetTransform);
    }

    #endregion
}
