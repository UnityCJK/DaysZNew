using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float gameTimeSecound;

    private bool isNight = false;
    public float fogDenCalc; 
    public float nightFog; // ¹ã
    private float dayFog; // ³·
    private float curFogDen; //ÇöÀç
    void Start()
    {
        dayFog = RenderSettings.fogDensity;
    }

    void Update()
    {
        transform.Rotate(Vector3.right, 1f * gameTimeSecound * Time.deltaTime);
        if (transform.eulerAngles.x >= 180)
            isNight = true;
        else if (transform.eulerAngles.x <= 340)
            isNight = false;

        if(isNight)
        {
            if (curFogDen <= nightFog)
            {
                curFogDen += 0.1f * fogDenCalc * Time.deltaTime;
                RenderSettings.fogDensity = curFogDen;
            }
        }
        else
        {
            if (curFogDen >= dayFog)
            {
                curFogDen -= 0.1f * fogDenCalc * Time.deltaTime;
                RenderSettings.fogDensity = curFogDen;
            }
        }
    }
}
