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
    public ProgressBar vida;
    private int actual;
    private bool hit;

    void Start()
    {
        hit = true;
        actual = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (hit)
        {
            StartCoroutine("setVida"); //LLamar a setActual
            hit = false;
        }
        
    }

    private void setActual(int aux)
    {
        actual = aux;
    }

    public void golpeado(int valorNuevo)
    {
        setActual(valorNuevo);
        hit = true;
    }

    IEnumerator setVida()
    {
        vida.BarValue = actual;
        yield return new WaitForSeconds(.1f);
    }
}
