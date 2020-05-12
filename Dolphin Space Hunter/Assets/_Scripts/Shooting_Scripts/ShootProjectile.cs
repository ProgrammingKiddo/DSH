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
        //projectileShot.transform.LookAt(target.transform);
        Debug.Log("Target:" + target.transform.position + ", projectileForward: " + this.transform.forward);
        projectileShot.GetComponent<Rigidbody>().velocity = projectile.transform.forward * projectile.GetComponent<ProjectileScript>().projectileSpeed;
    }
}
