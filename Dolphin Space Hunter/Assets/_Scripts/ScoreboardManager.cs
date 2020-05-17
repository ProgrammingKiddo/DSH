/*
 * Copyright (c) Borja Fernández
 *
 */

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoreboardManager : MonoBehaviour
{

    public static void loadScoreboard(object obj)
    {
        string jsonText = JsonUtility.ToJson(obj);
        Debug.Log(jsonText);
    }

    public static void storeJson(object obj)
    {
        using (FileStream fileStream = new FileStream("./info.json", FileMode.Open))
        {
            byte[] info = new System.Text.UTF8Encoding(true).GetBytes(JsonUtility.ToJson(obj, true));
            fileStream.Write(info, 0, info.Length);
        }
    }
   
}
