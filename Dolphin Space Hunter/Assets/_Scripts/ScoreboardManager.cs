/*
 * Copyright (c) Borja Fernández
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardManager : MonoBehaviour
{

    public static void loadScoreboard(object obj)
    {
        string jsonText = JsonUtility.ToJson(obj);
        Debug.Log(jsonText);
    }
   
}
