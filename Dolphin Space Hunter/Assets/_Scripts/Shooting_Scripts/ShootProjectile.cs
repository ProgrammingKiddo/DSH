/*
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
    public GameObject target;
    public int damage = 5;
    #endregion


    #region UnityMethods

    void Start()
    {
        this.transform.LookAt(target.transform);
    }

    void Update()
    {
        
    }

    #endregion

    public void Shoot(GameObject projectile)
    {
        GameObject projectileShot = Instantiate(projectile, gunMuzzle.transform.position, this.transform.rotation);
        projectileShot.GetComponent<Rigidbody>().velocity = this.transform.forward * projectile.GetComponent<ProjectileScript>().projectileSpeed;
    }
}
