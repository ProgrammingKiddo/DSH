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
    #region UnityMethods

    void Start()
    {
        ShooterGameDirector.Instance().changeScene(GetComponent<ImageTargetBehaviour>().ImageTarget.Name);
    }

    #endregion
}
