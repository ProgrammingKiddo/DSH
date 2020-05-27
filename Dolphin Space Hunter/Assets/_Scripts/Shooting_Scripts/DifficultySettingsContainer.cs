/*
 * Copyright (c) Borja Fernández
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySettingsContainer : MonoBehaviour
{

    #region Variables
    public int maxAmmunition;
    public int wavesPerBoss; 
    public float shieldRechargeRate; //Factor que indica cuantos frames agitar el telefono para que aumente el escudo en 1 unidad
    public int damagePerAsteroid;
    public float asteroidSpawnRate;
    public float minAsteroidSpeed, maxAsteroidSpeed;
    #endregion

}
