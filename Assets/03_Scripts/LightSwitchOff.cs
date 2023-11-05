using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightSwitchOff : MonoBehaviour
{
    public Light2D myLight;
    public bool isOn = false;
    public GameObject Dreamdoor;
    private void Start()
    {
        if (myLight)
        {
            myLight.enabled = isOn;
        }

    }
    public void TorchLight()
    {
        if (myLight)
        {
            isOn = isOn;
            myLight.enabled = !isOn;
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
