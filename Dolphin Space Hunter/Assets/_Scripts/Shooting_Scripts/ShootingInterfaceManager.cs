/*
 * Copyright (c) Borja Fernández
 *
 */

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShootingInterfaceManager : MonoBehaviour
{

    #region Variables
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bonusText;

    public TextMeshProUGUI currentAmmunitionText;
    public TextMeshProUGUI maxAmmunitionText;
    #endregion


    #region UnityMethods

    void Start()
    {
        updateScore(0);
        updateBonusModifier(1);
        //maxAmmunitionText.text = "/ " + PlayerPrefs.GetInt("MaxAmmo", 0).ToString();
        maxAmmunitionText.text = "/ 120";
        updateAmmunition(ShooterGameDirector.Instance().ammunition);
    }

    void Update()
    {
        
    }

    #endregion

    public void updateScore(int newScore)
    {
        scoreText.text = newScore.ToString();
    }

    public void updateBonusModifier(int newBonus)
    {
        bonusText.text = "x" + newBonus.ToString();
    }

    public void updateAmmunition(int newAmmunition)
    {
        currentAmmunitionText.text = newAmmunition.ToString();
    }
}
