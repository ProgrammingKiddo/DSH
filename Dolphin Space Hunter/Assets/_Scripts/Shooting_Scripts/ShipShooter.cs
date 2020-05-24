/*
 * Copyright (c) Borja Fernández
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShooter : MonoBehaviour
{

    #region Variables
    public GameObject projectile;
    public int aggressiveness;
    public float minSpread, maxSpread;
    #endregion


    private int iterations = 0;
    #region UnityMethods

    void Start()
    {
        
    }

    void Update()
    {
        // Cada nave tiene una probabilidad entre 1000 de disparar en cada paso del Update
        // Con cada paso en el que NO dispara, se incrementa en 1 la posibilidad de que dispare
        // Cuando dispara, ese contador se reinicia, y la probabilidad vuelve a ser de 1/1000
        if (Random.Range(0, aggressiveness) <= iterations)
        {
            iterations = 0;
            shoot();
        }
        else
        {
            iterations++;
        }
    }

    #endregion

    private void shoot()
    {
        Vector3 shootingPoint = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 200);
        Vector3 attackVector = (calculateSpread() - shootingPoint).normalized;

        GameObject projectileShot = Instantiate(projectile,
                                        shootingPoint,
                                        Quaternion.LookRotation(attackVector));
        projectileShot.GetComponent<Rigidbody>().velocity = projectileShot.transform.forward * projectile.GetComponent<ProjectileScript>().projectileSpeed;
    }

    private Vector3 calculateSpread()
    {
        Vector3 targetPosition = Camera.main.transform.position;
        float xOffset = Random.Range(minSpread, maxSpread);
        float yOffset = Random.Range(minSpread, maxSpread);

        targetPosition.x += xOffset;
        targetPosition.y += yOffset;

        return targetPosition;
    }
}
