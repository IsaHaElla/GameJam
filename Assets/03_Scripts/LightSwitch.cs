using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] Light[] lights = new Light[2];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
