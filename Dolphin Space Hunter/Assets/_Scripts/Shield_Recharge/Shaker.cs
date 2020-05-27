using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    public float sensibility;
    public int rechargeRate;
    private int cont;
    // Start is called before the first frame update
    void Start()
    {
        cont = 0;
        sensibility = 2;
        rechargeRate = PlayerPrefs.GetInt("ShielRechargeRate", 5);
    }

    // Update is called once per frame
    void Update()
    {
        if(ShieldScript.Shield < 100){
            if((Mathf.Abs(Input.acceleration.x) + Mathf.Abs(Input.acceleration.y) + Mathf.Abs(Input.acceleration.z)) > sensibility){
                cont++;
                if(cont >= rechargeRate){
                    ShieldScript.Shield++;
                    cont = 0;
                }
            }
        }
    }
}
