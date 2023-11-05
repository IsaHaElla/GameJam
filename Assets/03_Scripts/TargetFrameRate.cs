using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFrameRate : MonoBehaviour
{

    public enum limits
    {
        noLimit = 0,
        limit15 = 15,
        limit30 = 30, 
        limit60 = 60, 
        limit120 = 120,
        limit240 = 240,
    }

    public limits limit;

    void Start()
    {
        //QualitySettings.vSyncCount = 0;

    }

    private void Update()
    {
        Application.targetFrameRate = (int)limit;
    }

}
