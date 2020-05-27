/*
 * Copyright (c) Borja Fernández
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShooterGameDirector : MonoBehaviour
{

    #region Variables
    // Vamos a utilizar este gameDirector como lo que se conoce como "Singleton"
    // Para ello, tenemos una instancia de una clase, que apunta como referencia estática a sí misma.
    // De esta forma, podemos acceder a todos los atributos y métodos a través 
    private static ShooterGameDirector instance  = null;

    public GameObject player;
    public ShootingInterfaceManager shootingInterfaceManager;
    public AudioClip explosionSound;
    public AudioClip playerHitSound;
    public int ammunition;
    public ProgressBar shieldBar;
    public TextAsset easyDifficultyFile, mediumDifficultyFile, hardDifficultyFile;
    public FleetSpawner fleetSpawner;

    private GameObject activeFleets;
    private int numberOfCurrentShips = 0;
    private int activeWave;

    private int bonusModifier = 1;
    private int score = 0;
    private int remainingShield;
    private int spawnedWaves;
    private DifficultySettingsContainer shootingLevel = new DifficultySettingsContainer();

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

        score = PlayerPrefs.GetInt("PlayerScore", 0);
        ammunition = PlayerPrefs.GetInt("Ammo", 50);
        loadDifficulty();

        remainingShield = PlayerPrefs.GetInt("Shield", 0);
        spawnedWaves = PlayerPrefs.GetInt("SpawnedWaves", 0);

        shieldBar.BarValue = remainingShield;
        activeWave = PlayerPrefs.GetInt("ActiveWave", 0);
    }

    #endregion

    public void ammoConsumption()
    {
        ammunition--;
        shootingInterfaceManager.updateAmmunition(ammunition);
    }

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

        // Desactivamos la flag de oleada activa
        activeWave = 0;
    
    }

    public void playerHit(ProjectileScript projectile)
    {
        GetComponent<AudioSource>().PlayOneShot(playerHitSound, 0.8f);
        //addBonusModifier(-2);
        // Si el escudo YA estaba a 0, el jugador muere
        if (remainingShield == 0)
        {
            StartCoroutine(gameOver());
        }
        remainingShield -= projectile.damage;
        // Si, con el impacto, hemos perdido todos los escudos,
        // nos quedamos a 0
        if (remainingShield < 0)
        {
            remainingShield = 0;
        }
        shieldBar.BarValue = remainingShield;

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
    }

    private void addBonusModifier(int bonusToAdd)
    {
        bonusModifier += bonusToAdd;
        // El multiplicador nunca podrá ser negativo ni cero, ni estar por encima de 99
        bonusModifier = Mathf.Clamp(bonusModifier, 1, 100);
        shootingInterfaceManager.updateBonusModifier(bonusModifier);
    }


    IEnumerator gameOver()
    {
        // Dejamos de mostrar nada por pantalla
        Camera.main.enabled = false;
        PlayerPrefs.SetInt("PlayerScore", score);
        shieldBar.gameObject.SetActive(false);
        // Paramos la música de la escena
        GetComponent<AudioSource>().Stop();
        // Reproducimos el sonido de explosión de la nave
        playSound(explosionSound, 1f);
        yield return new WaitForSecondsRealtime(4f);
        SceneManager.LoadScene("GameOverScene");
    }

    public void saveInformation()
    {
        // Penalizador por cambiar a menudo entre escenas mientras haya una oleada activa, para evitar
        // que el jugador esté continuamente recargando munición y/o escudo
        if (activeWave == 1)
        {
            addBonusModifier(-5);
        }
        // Guardamos la información relevante
        PlayerPrefs.SetInt("PlayerScore", score);
        PlayerPrefs.SetInt("Ammo", ammunition);
        PlayerPrefs.SetInt("Shield", remainingShield);
        PlayerPrefs.SetInt("SpawnedWaves", spawnedWaves);
        PlayerPrefs.SetInt("ActiveWave", activeWave);
        PlayerPrefs.SetInt("SpawnedWaves", spawnedWaves);
    }

    private void loadDifficulty()
    {
        switch(PlayerPrefs.GetString("DifficultyMode", "Easy"))
        {
            case "Easy":
                JsonUtility.FromJsonOverwrite(easyDifficultyFile.text, shootingLevel);
                break;
            case "Normal":
                JsonUtility.FromJsonOverwrite(mediumDifficultyFile.text, shootingLevel);
                break;
            case "Hard":
                JsonUtility.FromJsonOverwrite(hardDifficultyFile.text, shootingLevel);
                break;

        }
    }

    public void playSound(AudioClip sound, float intensity)
    {
        GetComponent<AudioSource>().PlayOneShot(sound, intensity);
    }
}
