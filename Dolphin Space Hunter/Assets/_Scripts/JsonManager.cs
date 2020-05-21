/*
 * Copyright (c) Borja Fernández
 *
 */

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonManager : MonoBehaviour
{

    public static void loadScoreboard(TextAsset JsonFile, object container)
    {
        JsonUtility.FromJsonOverwrite(JsonFile.text, container);        
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
