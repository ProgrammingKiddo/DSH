using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    public float sensibility;
    public float rechargeRate;
    private int cont;
    // Start is called before the first frame update
    void Start()
    {
        cont = 0;
        sensibility = 5;
        rechargeRate = 5;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Shield = " + ShieldScript.Shield + "\ncont = " + cont);
        //Debug.Log("X:" + Input.acceleration.x + " Y:" + Input.acceleration.y + " Z:" + Input.acceleration.z + " Sum:" + 
        //          (Mathf.Abs(Input.acceleration.x) + Mathf.Abs(Input.acceleration.y) + Mathf.Abs(Input.acceleration.z)));
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
