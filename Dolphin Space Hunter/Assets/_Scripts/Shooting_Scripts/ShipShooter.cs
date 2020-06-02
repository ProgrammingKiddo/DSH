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
    public GameObject shootingPoint;
    public int aggressiveness;
    public float spread;
    #endregion


    private int iterations = 0;
    #region UnityMethods



    void FixedUpdate()
    {
        // Cada nave tiene una probabilidad entre 'aggressiveness' de disparar en cada paso del Update
        // Con cada paso en el que NO dispara, se incrementa en 1 la posibilidad de que dispare
        // Cuando dispara, ese contador se reinicia, y la probabilidad vuelve a ser de 1/aggressiveness
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
        Vector3 attackVector = (calculateSpread() - shootingPoint.transform.position).normalized;

        GameObject projectileShot = Instantiate(projectile,
                                        shootingPoint.transform.position,
                                        Quaternion.LookRotation(attackVector));
        projectileShot.GetComponent<Rigidbody>().velocity = projectileShot.transform.forward *
                                        projectile.GetComponent<ProjectileScript>().projectileSpeed;
    }

    private Vector3 calculateSpread()
    {
        Vector3 targetPosition = Camera.main.transform.position;
        float xOffset = Random.Range(-spread, spread);
        float yOffset = Random.Range(-spread, spread);

        targetPosition.x += xOffset;
        targetPosition.y += yOffset;

        return targetPosition;
    }
}
