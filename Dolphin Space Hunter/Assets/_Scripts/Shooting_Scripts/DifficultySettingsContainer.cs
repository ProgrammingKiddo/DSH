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
    public int fleetsPerWave;
    public int wavesPerBoss; 
    public int shieldRechargeRate; //Factor que indica cuantos frames agitar el telefono para que aumente el escudo en 1 unidad
    public int damagePerAsteroid;
    public float asteroidSpawnRate;
    public float velMinAsteroides, velMaxAsteroides;
    
    public struct fleet
    {
        public int numberOfShips;
        public int typeOfShip;
        public FleetController.FleetMovementPattern fleetPattern;
        public FleetController.ShipMovementPattern shipPattern;
    };
    

    public fleet bossFleet;
    #endregion

}
