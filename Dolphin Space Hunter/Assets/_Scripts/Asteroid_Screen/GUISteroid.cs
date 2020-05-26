/*
 * Copyright (c) Sergio Ruiz
 * 
 * 
 * 
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUISteroid : MonoBehaviour
{
    // Start is called before the first frame update
    public ProgressBar shieldBar;
    private int actualShield;
    private bool hit;

    void Start()
    {
        hit = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (hit)
        {
            StartCoroutine(setVida()); //LLamar a setActual
            hit = false;
        }
        
    }

    public void asteroidHit(int remmainingShield)
    {
        actualShield = remmainingShield;
        hit = true;
    }

    IEnumerator setVida()
    {
        shieldBar.BarValue = actualShield;
        yield return new WaitForSeconds(.1f);
    }
}
