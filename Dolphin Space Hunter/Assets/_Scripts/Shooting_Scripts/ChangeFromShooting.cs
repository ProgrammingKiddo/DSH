/*
 * Copyright (c) Borja Fernández
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ChangeFromShooting : MonoBehaviour
{
    
    #region Variables

    #endregion


    #region UnityMethods

    void Start()
    {
        ShooterGameDirector.Instance().changeScene(GetComponent<ImageTargetBehaviour>().ImageTarget.Name);
    }

    void Update()
    {
        
    }

    #endregion
}
