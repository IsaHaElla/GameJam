using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightSwitch : MonoBehaviour
{
    public Light2D myLight;
    public bool isOn = true;
    public GameObject Dreamdoor;
    public GameObject torchFire;

    private void Start()
    {
        if (myLight)
        {
            myLight.enabled = false;
            torchFire.SetActive(false);
        }
    }

    public void ToggleLight()
    {
        if (myLight) 
        {
            isOn = !isOn;
            myLight.enabled = isOn;
            torchFire.SetActive(isOn);
            Destroy(Dreamdoor);
        }
    
    }

    void DisableLights()
    {
        if (myLight)
        {
            isOn = false;
            myLight.enabled = isOn;
            torchFire.SetActive(isOn);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            myLight.enabled = !isOn;
            Destroy(Dreamdoor);
        }
    } 
}
