/*
 * Copyright (c) Sergio Ruiz
 * 
 * 
 * 
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDestroyer : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (this.gameObject.GetComponent<Rigidbody>().position.z <-100)
        {
            Destroy(this.gameObject);

        }
    }
}
