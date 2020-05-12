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
    public float speed;
    #endregion


    #region UnityMethods

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 movementVector = new Vector3(Input.acceleration.x,
                                            Input.acceleration.y,
                                            0f);
        if (movementVector.sqrMagnitude > 1)
        {
            movementVector.Normalize();
        }
        this.transform.Translate(movementVector * speed * Time.deltaTime);
        Debug.Log(Input.acceleration);
    }

    #endregion
}
