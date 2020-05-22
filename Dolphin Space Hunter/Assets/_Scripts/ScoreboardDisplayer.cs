/*
 * Copyright (c) Borja Fernández
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreboardDisplayer : MonoBehaviour
{

    #region Variables
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI playersText;
    public TextMeshProUGUI scoresText;
    public TextAsset scoreboardFile;

    private ScoreboardContainer scoreboard = new ScoreboardContainer();
    #endregion


    #region UnityMethods

    void Start()
    {
        JsonManager.loadFromJson(scoreboardFile, scoreboard);
        difficultyText.text = "Fácil";
        playersText.text = scoreboard.players[0] + "\n" + scoreboard.players[1] + "\n" + scoreboard.players[2];
        scoresText.text = scoreboard.scores[0] + "\n" + scoreboard.scores[1] + "\n" + scoreboard.scores[2];
    }

    #endregion
}
