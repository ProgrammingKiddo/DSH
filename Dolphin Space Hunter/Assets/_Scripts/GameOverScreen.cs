/*
 * Copyright (c) Borja Fernández
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{

    #region Variables
    public Text blinkingText;

    private bool turnVisible = false;
    #endregion


    #region UnityMethods

    void Update()
    {
        if (Input.touchCount > 0)
        {
            SceneManager.LoadScene("Scoreboard");
        }
        Color flashingColor = blinkingText.color;

        if (flashingColor.a <= 0f)
        {
            turnVisible = true;
        }
        else if (flashingColor.a >= 1f){
            turnVisible = false;
        }

        if (turnVisible == true)
        {
            flashingColor.a += 0.025f;
        }
        else
        {
            flashingColor.a -= 0.025f;
        }

        
        blinkingText.color = flashingColor;
    }

    #endregion
}
