using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpDown : MonoBehaviour
{
    [SerializeField] float mMovementSpeed = 3.0f;
    [SerializeField] private Vector3[] positions;

    private int index;
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, positions[index], Time.deltaTime * mMovementSpeed);

        if (transform.position == positions[index])
        {
            if (index == positions.Length -1) 
            {
            index= 0;
            }
            else
            {
                index++;
            }
        }
    }

}
