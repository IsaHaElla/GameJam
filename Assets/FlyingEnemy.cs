using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float mMovementSpeed;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player= GameObject.FindGameObjectWithTag("Player");  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Chase() 
    { }
}
