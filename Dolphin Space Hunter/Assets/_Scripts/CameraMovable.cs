using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovable : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera ArCamera;
    float IntervaloAc = 1.0f / 30; //valor actualizacion;
    float LowPassKernelWidthInSeconds = 1.0f;
    float factorFiltro = 0;
    Vector3 valorCrudo = Vector3.zero;

    void Start()
    {
        factorFiltro = IntervaloAc / LowPassKernelWidthInSeconds;
        valorCrudo = Input.acceleration;

    }

    // Update is called once per frame
    void Update()
    {
        actualizaCamara();
        
    }

    private Vector3 FiltradoAccelValor()
    {
        Debug.Log("NO FILTRADO " + Input.acceleration);
        Debug.Log("fILTREADO " + Vector3.Lerp(valorCrudo, Input.acceleration, factorFiltro));
        return Vector3.Lerp(valorCrudo, Input.acceleration, factorFiltro);
     
    }
    private void actualizaCamara()
    {
        Vector3 acelerometroValor = FiltradoAccelValor();
        Vector3 newPosition = new Vector3(ArCamera.transform.position.x + acelerometroValor.x,ArCamera.transform.position.y+acelerometroValor.y, ArCamera.transform.position.z);
        ArCamera.transform.position = newPosition;
        Debug.Log(ArCamera.transform.position);
    }

    void OnGUI()
    {
        GUILayout.Label("NO FILTRADO " + Input.acceleration + " Fil " + Vector3.Lerp(valorCrudo, Input.acceleration, factorFiltro));

    }


}
