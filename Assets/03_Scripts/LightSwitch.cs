using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightSwitch : MonoBehaviour
{
    public Light2D myLight;
    void Start()
    {
        GameObject light2D = GameObject.FindWithTag("Light");
        myLight = GetComponent<Light2D>();
        myLight.enabled = false;
    } 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            myLight.enabled = !myLight.enabled;
    }
}
