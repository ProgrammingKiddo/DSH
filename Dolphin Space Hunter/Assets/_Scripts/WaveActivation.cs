/*
 * Copyright (c) Borja Fernández
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveActivation : MonoBehaviour
{

    #region Variables

    public AudioClip newWaveSound;
    public AudioClip playerHitSound;
    public ProgressBar shieldBar;
    public CameraMovable asteroidScript;
    public TextMeshProUGUI hitText;
    public TextMeshProUGUI newWaveText;

    private int iterations;
    private int activeWave;
    private float aggressivenessMultiplier;
    #endregion


    #region UnityMethods

    void Start()
    {
        activeWave = PlayerPrefs.GetInt("ActiveWave", 0);

        switch (PlayerPrefs.GetString("DifficultyMode", "Easy"))
        {
            case "Easy":
                aggressivenessMultiplier = 0.5f;
                break;
            case "Normal":
                aggressivenessMultiplier = 1f;
                break;
            case "Hard":
                aggressivenessMultiplier = 2f;
                break;
        }
    }

    private void FixedUpdate()
    {
        if (Random.Range(0, 1000000 / aggressivenessMultiplier) <= iterations)
        {
            Debug.Log(activeWave);
            if (activeWave == 0)
            {
                GetComponent<AudioSource>().PlayOneShot(newWaveSound, 1f);
                StartCoroutine(showWaveText());
                activeWave = 1;
                PlayerPrefs.SetInt("ActiveWave", activeWave);
            }
            else
            {
                GetComponent<AudioSource>().PlayOneShot(playerHitSound, 1f);
                StartCoroutine(showHitText());
                updateShield();
            }
            iterations = 0;
        }
        iterations++;
    }

    private void updateShield()
    {
        switch (this.gameObject.scene.name)
        {
            case "AmmunitionReloadScene":
                int remainingShield = PlayerPrefs.GetInt("Shield") - (int)(10f * aggressivenessMultiplier);
                PlayerPrefs.SetInt("Shield", remainingShield);
                shieldBar.BarValue = remainingShield;
                break;
            case "AsteroidScene":
                asteroidScript.remainingShield -= (int)(10f * aggressivenessMultiplier);
                shieldBar.BarValue = asteroidScript.remainingShield;
                break;
            case "ShieldRechargeScene":
                ShieldScript.Shield -= (int)(10f * aggressivenessMultiplier);
                break;
        }
    }

    IEnumerator showHitText()
    {
        hitText.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(2f);
        hitText.gameObject.SetActive(false);
    }

    IEnumerator showWaveText()
    {
        newWaveText.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(2f);
        newWaveText.gameObject.SetActive(false);
    }

    #endregion
}
