using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashLight : MonoBehaviour
{
    [SerializeField]
    private Light flashLight;
    private bool isEnabled = true;

    [SerializeField]
    private Image indecator;
    
    private float maxBatteryLife;

    private float curBatteryLife;

    [SerializeField]
    private float lightDrain = 0.1f;

    void Start(){
        maxBatteryLife = flashLight.intensity;
        curBatteryLife = maxBatteryLife;
    }

    void Update(){
        if (isEnabled)
        {
            if (curBatteryLife > 0)
            {
                curBatteryLife -= lightDrain * Time.deltaTime;

                flashLight.intensity = curBatteryLife;

                var indecatorWidth = curBatteryLife / maxBatteryLife;
                var indecatorScale = indecator.transform.localScale;

                indecator.transform.localScale = new Vector3(indecatorWidth, indecatorScale.y, indecatorScale.z);
            }
            else
            {
                curBatteryLife = 0;
                isEnabled = false;
                flashLight.enabled = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.F) && curBatteryLife > 0){            
            isEnabled = !isEnabled;
            flashLight.enabled = isEnabled;
        }
    }

    public void AddEnergy(float en)
    {
        curBatteryLife += en;
        if (curBatteryLife > maxBatteryLife)
        {
            curBatteryLife = maxBatteryLife;
        }
    }

}
