/*
 * Copyright (c) Borja Fernández
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterGameDirector : MonoBehaviour
{

    #region Variables
    public int bonusModifier = 1;
    public int score = 0;
    public GameObject player;

    private GameObject activeFleets;

    #endregion


    #region UnityMethods

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    #endregion

    public void shipHit()
    {
        addScore(1);
    }

    public void shipDestroyed(DolphinScript ship)
    {
        addScore(2 * ship.health);
        // Play animation on ship's position
    }

    public void fleetDestroyed(FleetController fleet)
    {
        addScore(50 * fleet.initialNumberOfShips);
        // Briefly activate a message notifying the player that a whole
        // fleet's been wiped out
    
    }

    private void addScore(int scoreToAdd)
    {
        score = score + (bonusModifier * (scoreToAdd));
    }
}
