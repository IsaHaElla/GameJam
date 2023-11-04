using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float offset;
    public float offsetSmoothing;
    private Vector3 playerPosition;
    [SerializeField] private float yOffset = 2; 


    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    private void Update()
    {
       
    }

    void FixedUpdate()
    {
        //playerPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        //if (player.transform.localScale.x > 0f)
        if (player.transform.localScale.x > 0)
        {
            playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y + yOffset , playerPosition.z);
        }
        else
        {
            playerPosition = new Vector3(playerPosition.x - offset, playerPosition.y + yOffset , playerPosition.z);
        }

        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);


    }
}
