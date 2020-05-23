﻿/*
 * Copyright (c) Borja Fernández
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreboardDisplayer : MonoBehaviour
{

    #region Variables
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI playersText;
    public TextMeshProUGUI scoresText;
    public TextMeshProUGUI inputText;
    public TMP_InputField input;
    public TextAsset scoreboardFile;
    public Button restartButton;

    private ScoreboardContainer scoreboard = new ScoreboardContainer();
    private int playerScore;
    private string playerName;
    #endregion


    #region UnityMethods

    void Start()
    {
        JsonManager.loadFromJson(scoreboardFile, scoreboard);
        difficultyText.text = PlayerPrefs.GetString("Difficulty", "Fácil");
        playerScore = PlayerPrefs.GetInt("PlayerScore", 750);
        playerScore = 800;
        checkIfScoreIsHighEnough();
    }

    #endregion

    // Programador anónimo del futuro, si estás leyendo esto, lo siento por la mierda de implementación,
    // pero no había tiempo para nada mejor xd
    public void endTextEdit(string name)
    {
        playerName = name;
        int lastScore = 0;
        // Cogemos el índice de la lista que apunta a la última puntuación
        // de cada dificultad
        switch(difficultyText.text)
        {
            case "Fácil":
                lastScore = 0;
                break;
            case "Medio":
                lastScore = 3;
                break;
            case "Difícil":
                lastScore = 6;
                break;
        }
        // Si la puntuación del jugador es lo suficientemente alta como para
        // aparecer en la tabla, será al menos más alta que la del último jugador
        scoreboard.scores[lastScore] = playerScore;
        scoreboard.players[lastScore] = playerName;
        // Si la puntuación es más alta que la del segundo jugador, asignamos éste
        // a la última puntuación, y escribimos la del jugador actual en su lugar
        if (playerScore > scoreboard.scores[lastScore+1])
        {
            scoreboard.scores[lastScore] = scoreboard.scores[lastScore+1];
            scoreboard.players[lastScore] = scoreboard.players[lastScore + 1];
            scoreboard.scores[lastScore + 1] = playerScore;
            scoreboard.players[lastScore + 1] = playerName;

            // Repetimos el proceso con el primer jugador de la lista
            if (playerScore > scoreboard.scores[lastScore+2])
            {
                scoreboard.scores[lastScore + 1] = scoreboard.scores[lastScore + 2];
                scoreboard.players[lastScore + 1] = scoreboard.players[lastScore + 2];
                scoreboard.scores[lastScore + 2] = playerScore;
                scoreboard.players[lastScore + 2] = playerName;
            }
        }
        // Guardamos la nueva tabla de puntuación en el fichero persistente
        JsonManager.storeToJson(scoreboardFile, scoreboard);
        // Cargamos la correspondiente tabla de puntuación en la UI
        loadScoreboardToUI();
        // Desactivamos los campos de entrada de datos
        inputText.gameObject.SetActive(false);
        input.gameObject.SetActive(false);
        // Por último activamos los campos de display de información
        activateScoreboardFields();
    }

    // Comprobamos si la puntuación es lo suficientemente alta como para entrar en
    // la tabla correspondiente a su dificultad. Si lo es, iniciamos el proceso de entrada
    // de datos para que el jugador escriba su nombre. Si no, simplemente cargamos la información
    // almacenada y la mostramos.
    private void checkIfScoreIsHighEnough()
    {
        int minimumScoreToBeat = 0;

        switch(difficultyText.text)
        {
            case "Fácil":
                minimumScoreToBeat = scoreboard.scores[0];
                break;
            case "Medio":
                minimumScoreToBeat = scoreboard.scores[3];
                break;
            case "Difícil":
                minimumScoreToBeat = scoreboard.scores[6];
                break;
        }
        if (playerScore > minimumScoreToBeat)
        {
            Debug.Log("Score " + playerScore + " is high enough (" + scoreboard.scores[0] + ")");
            activateInputFields();
        }
        else
        {
            loadScoreboardToUI();
            activateScoreboardFields();
        }
    }

    private void activateInputFields()
    {
        inputText.gameObject.SetActive(true);
        input.gameObject.SetActive(true);
    }

    private void activateScoreboardFields()
    {
        difficultyText.gameObject.SetActive(true);
        playersText.gameObject.SetActive(true);
        scoresText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    private void loadScoreboardToUI()
    {
        int lastScore = 0;
        switch(difficultyText.text)
        {
            case "Fácil":
                lastScore = 0;
                break;
            case "Medio":
                lastScore = 3;
                break;
            case "Difícil":
                lastScore = 6;
                break;
        }
        // Cargamos por orden descendente la información de la tabla de puntuación
        playersText.text = scoreboard.players[lastScore+2] + "\n" +
                            scoreboard.players[lastScore+1] + "\n" +
                            scoreboard.players[lastScore];
        scoresText.text = scoreboard.scores[lastScore+2] + "\n" +
                            scoreboard.scores[lastScore+1] + "\n" +
                            scoreboard.scores[lastScore];
    }

    public void backToMenu()
    {
        // Reiniciamos la puntuación del jugador
        PlayerPrefs.SetInt("PlayerScore", 0);
        SceneManager.LoadScene("MainMenuScene");
    }
}
