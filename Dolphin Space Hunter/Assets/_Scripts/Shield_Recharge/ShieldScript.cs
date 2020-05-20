using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform rectTransform;
    public static float Shield{get; set;}

    // Start is called before the first frame update
    void Start()
    {
        //Shield = PlayerPrefs.GetFloat("Shield");
        Shield = 0f;
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        float UpdateShield = Mathf.MoveTowards(rectTransform.rect.height, Shield, 5.0f);
        rectTransform.sizeDelta = new Vector2(100f, Mathf.Clamp(UpdateShield, 0.0f, 100f));
    }
}