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
    private float xMovingDistance;
    #endregion


    #region UnityMethods

    void Start()
    {
        xMovingDistance = 10f;
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        if (this.transform.position.x >= 350 || this.transform.position.x <= -350)
        {
            xMovingDistance *= -1;
        }
        this.transform.position += new Vector3(xMovingDistance, 0f, 0f);
    }

    #endregion
}
