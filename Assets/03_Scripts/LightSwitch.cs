using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] Light[] lights = new Light[2];
    public GameObject lightSwitch;
    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (lightSwitch.activeInHierarchy == true)
            {
                lightSwitch.SetActive(false);
            }
            else
            {
                lightSwitch.SetActive(true);

            }
        }
    }
}
