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

    public TextMeshProUGUI ammunitionText;
    private int maxAmmunition;
    #endregion


    #region UnityMethods

    void Start()
    {
        updateScore(0);
        updateBonusModifier(1);
        maxAmmunition = PlayerPrefs.GetInt("MaxAmmo", 0);
        ammunitionText.text = PlayerPrefs.GetInt("Ammo", 0).ToString() +  "/ " + maxAmmunition.ToString();
        updateAmmunition(ShooterGameDirector.Instance().ammunition);
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
        ammunitionText.text = newAmmunition.ToString() + "/" + maxAmmunition.ToString();
    }
}
