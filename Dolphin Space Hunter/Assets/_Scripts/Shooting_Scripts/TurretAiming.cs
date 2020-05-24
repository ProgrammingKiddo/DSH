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

    private Gyroscope gyro;
    #endregion


    #region UnityMethods

    void Start()
    {
        gyro = Input.gyro;
        gyro.enabled = true;
    }

    void Update()
    {

        /*
        Vector3 movementVector = new Vector3(Input.acceleration.x,
                                            Input.acceleration.y,
                                            0f);
        if (movementVector.sqrMagnitude > 1)
        {
            movementVector.Normalize();
        }
        this.transform.Translate(movementVector * speed * Time.deltaTime);*/
        this.transform.rotation = GyroToUnity(Input.gyro.attitude);
        Debug.Log(transform.rotation);
    }

    #endregion

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
