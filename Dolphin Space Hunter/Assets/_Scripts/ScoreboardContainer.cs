/*
 * Copyright (c) Borja Fernández
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardContainer
{

    #region Variables

    // Por simplicidad, vamos a usar dos listas, en las que nosotros mismos tendremos
    // que mantener la relación de orden. Las posiciones 0-2 contienen, por orden ascendente,
    // la información referente a los 3 mejores jugadores del modo fácil.
    // Del mismo modo, los mejores jugadores del modo medio se almacenan en las posiciones
    // 3-5, y los del modo difícil en las posiciones 6-8.

    // Inicializamos las listas con una capacidad inicial por eficiencia.
    // De este modo, como sabemos exactamente cuántos elementos queremos almacenar,
    // solo tenemos que reservar memoria una vez, en lugar de cada vez que añadamos un elemento.
    public List<string> players;
    public List<int> scores;

    #endregion

    private void Awake()
    {
        scores = new List<int>(9);
        players = new List<string>(9);
    }
}
