/*
 * Copyright (c) Borja Fernández
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    #region Variables
    public float projectileSpeed;
    public int damage = 5;

    private Transform bulletTransform;
    private Vector3 originalScale;

    private float distanceToCamera;
    #endregion


    #region UnityMethods

    void Start()
    {
        bulletTransform = GetComponent<Transform>();
        originalScale = bulletTransform.localScale;
    }

    void Update()
    {
        distanceToCamera = Vector3.Distance(Camera.main.transform.position, bulletTransform.position);
        bulletTransform.localScale = originalScale * Mathf.Clamp((distanceToCamera / Camera.main.farClipPlane), 0.15f, 0.4f);


        if (bulletTransform.position.z > Camera.main.farClipPlane)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Debug.Log("Enemy hit!");
        }
    }

    #endregion
}
