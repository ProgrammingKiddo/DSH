﻿/*
 * Copyright (c) Borja Fernández
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{

    #region Variables
    public GameObject gunMuzzle;
    #endregion


    #region UnityMethods

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    #endregion

    public void Shoot(GameObject projectile)
    {
        GameObject projectileShot = Instantiate(projectile, gunMuzzle.transform.position, this.transform.rotation);
        projectileShot.GetComponent<Rigidbody>().velocity = projectile.transform.forward * projectile.GetComponent<ProjectileScript>().projectileSpeed;
    }
}
