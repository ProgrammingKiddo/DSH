/*
 * Copyright (c) Borja Fernández
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingLevels_Container : MonoBehaviour
{

    #region Variables
    public string difficultyLevel;
    public int fleetsPerWave;
    public int wavesPerBoss;
    
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
