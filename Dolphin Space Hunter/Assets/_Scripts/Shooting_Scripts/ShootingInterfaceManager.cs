/*
 * Copyright (c) Borja Fernández
 *
 */

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
    #endregion


    #region UnityMethods

    void Start()
    {
        updateScore(0);
        updateBonusModifier(1);
    }

    void Update()
    {
        
    }

    #endregion

    public void updateScore(int newScore)
    {
        scoreText.text = newScore.ToString();
        //Debug.Log("Score updated to " + newScore);
    }

    public void updateBonusModifier(int newBonus)
    {
        bonusText.text = "x" + newBonus.ToString();
        Debug.Log("Multiplier updated to " + newBonus);
    }
}
