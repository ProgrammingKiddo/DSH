/*
 * Copyright (c) Borja Fernández
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetSpawner : MonoBehaviour
{

    #region Variables
    public List<GameObject> easyFleets = new List<GameObject>(5);
    public List<GameObject> normalFleets = new List<GameObject>(5);
    public List<GameObject> hardFleets = new List<GameObject>(5);
    public int spawnedWaves;

    private string difficultySetting;
    private int wavesPerBoss;
    private bool spawnBossWave = false;
    #endregion


    #region UnityMethods

    void Start()
    {
        difficultySetting = PlayerPrefs.GetString("DifficultyMode", "Easy");
        spawnedWaves = PlayerPrefs.GetInt("SpawnedWaves", 0);
        wavesPerBoss = PlayerPrefs.GetInt("WavesPerBoss", 4);

        if (PlayerPrefs.GetInt("ActiveWave") == 1)
        {
            Debug.Log("Spawning a wave in the fleet spawner!");
            spawnWave();
        }
    }


    public void spawnWave()
    {
        Debug.Log("Wave spawn!");
        if (spawnedWaves >= wavesPerBoss)
        {
            spawnBossWave = true;
        }

        switch (difficultySetting)
        {
            case "Easy":
                if (spawnBossWave == true)
                {
                    Instantiate(easyFleets[4], this.transform);
                    spawnBossWave = false;
                    spawnedWaves = 0;
                }
                else
                {
                    Instantiate(easyFleets[Random.Range(0, 3)], this.transform);
                }
                break;
            case "Normal":
                if (spawnBossWave == true)
                {
                    Instantiate(normalFleets[4], this.transform);
                    spawnBossWave = false;
                    spawnedWaves = 0;
                }
                else
                {
                    Instantiate(normalFleets[Random.Range(0, 3)], this.transform);
                }
                break;
            case "Hard":
                if (spawnBossWave == true)
                {
                    Instantiate(hardFleets[4], this.transform);
                    spawnBossWave = false;
                    spawnedWaves = 0;
                }
                else
                {
                    Instantiate(hardFleets[Random.Range(0, 3)], this.transform);
                }
                break;
        }
        spawnedWaves++;
    }

    #endregion
}
