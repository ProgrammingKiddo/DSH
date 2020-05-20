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
    // Vamos a utilizar este gameDirector como lo que en Unity se conoce como "Singleton"
    // Para ello, tenemos una instancia de una clase, que apunta como referencia estática a sí misma.
    // De esta forma, podemos acceder a todos los atributos y métodos a través 
    private static ShooterGameDirector instance  = null;

    public GameObject player;
    public ShootingInterfaceManager shootingInterfaceManager;
    // Atributos de solo lectura
    public int bonusModifier = 1;
    public int score = 0;

    private GameObject activeFleets;
    private int numberOfCurrentFleets = 0;
    private int numberOfCurrentShips = 0;


    #endregion

    public static ShooterGameDirector Instance() { return instance; }

    #region UnityMethods

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (numberOfCurrentFleets < 3 || numberOfCurrentShips < 6)
        {

        }
    }

    #endregion

    public void missedShot()
    {
        addScore(-1);
    }

    public void shipHit()
    {
        addScore(5);
    }

    public void shipDestroyed(ShipController ship)
    {
        addScore(5 * ship.initialHealth);
        addBonusModifier(1);
        numberOfCurrentShips--;
        // Play animation on ship's position
    }

    public void fleetDestroyed(FleetController fleet)
    {
        addScore(50 * fleet.initialNumberOfShips);
        addBonusModifier(5);
        numberOfCurrentFleets--;
        // Briefly activate a message notifying the player that a whole
        // fleet's been wiped out
    
    }

    private void addScore(int scoreToAdd)
    {
        // Para no castigar excesivamente al jugador, cuando se aplique algún penalizador
        // a su puntuación, éste no se multiplicará por el bono
        if (scoreToAdd > 0)
        {
            score = score + (bonusModifier * (scoreToAdd));
        }
        else
        {
            score = score + scoreToAdd;
        }
        shootingInterfaceManager.updateScore(score);
        Debug.Log("Update score to " + score);
    }

    private void addBonusModifier(int bonusToAdd)
    {
        bonusModifier += bonusToAdd;
        shootingInterfaceManager.updateBonusModifier(bonusModifier);
    }
}
