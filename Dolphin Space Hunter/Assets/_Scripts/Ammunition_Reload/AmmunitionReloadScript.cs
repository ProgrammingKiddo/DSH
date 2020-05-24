using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmunitionReloadScript : MonoBehaviour
{
    public int indicatorSpeed = 250;
    public Text ammunitionCounter;

    private int absMax = 49;
    //Color Ranges:
    //-49[RED]-45[YELLOW]-6[GREEN]5[YELLOW]41[RED]49
    private int[] colorRanges = {-45, -6, 5, 41};
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(reloadAction());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space")){
            if(transform.localPosition.x <=-45 || transform.localPosition.x>=41){
                Debug.Log("RecargaRoja");
            }else if(transform.localPosition.x <=-6 || transform.localPosition.x>=5){
                Debug.Log("RecargaAmarilla");
            }else{
                Debug.Log("RecargaVerde");
            }
        }
    }

    IEnumerator reloadAction(){
        bool derecha = true;
        while(true){
            if(transform.localPosition.x <= -absMax){
                derecha = true;
            }else if(transform.localPosition.x >= absMax){
                derecha = false;
            }

            if(derecha){
                transform.Translate(Vector3.right * indicatorSpeed * Time.deltaTime);
            }else{
                transform.Translate(Vector3.left * indicatorSpeed * Time.deltaTime);
            }
            
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }
}
