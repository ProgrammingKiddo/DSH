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
    
    public struct fleet
    {
        public int numberOfShips;
        public int typeOfShip;
        public FleetController.FleetMovementPattern fleetPattern;
        public FleetController.ShipMovementPattern shipPattern;
    };
    public List<fleet> fleetTypes = new List<fleet>();

    public fleet bossFleet;
    #endregion

}
