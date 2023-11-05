using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject playerCharacter;
    public float offset;
    public float offsetSmoothing;
    private Vector3 playerPosition;
    [SerializeField] private float yOffset = 2;


    private void Awake()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        playerCharacter = GameObject.FindGameObjectWithTag("Player");
    }

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
        playerPosition = new Vector3(playerCharacter.transform.position.x, playerCharacter.transform.position.y, playerCamera.transform.position.z);

        
        if (playerCharacter.transform.localScale.x > 0)
        {
            playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y + yOffset , playerPosition.z);
        }
        else
        {
            playerPosition = new Vector3(playerPosition.x - offset, playerPosition.y + yOffset , playerPosition.z);
        }

        playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, playerPosition, offsetSmoothing * Time.deltaTime);


    }
}
