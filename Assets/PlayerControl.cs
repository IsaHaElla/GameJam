using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody physicsBody;

    [SerializeField] Vector3 moveDirection;
    [SerializeField] float speed;
    [SerializeField] float jump = 2f;
    float moveVelocity;
    bool isGrounded;

    public SpriteRenderer spriteRenderer;

    public GameObject BulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
      
        //physicsBody.AddForce(moveDirection*speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        }

        if (isGrounded == true)
        {
            //jumping
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("ItJumps!");
                GetComponent<Rigidbody>().velocity = new Vector2(GetComponent<Rigidbody>().velocity.y, jump);
            }
        }
        //physicsBody.MovePosition(transform.position + moveDirection * Time.deltaTime * speed);
        moveVelocity = 0;

        //Left Right Movement
        if ( Input.GetKey(KeyCode.A))
        {
            moveVelocity = -speed;
            if (spriteRenderer.flipX == false)
            { 
                spriteRenderer.flipX = true;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveVelocity = speed;
            if (spriteRenderer.flipX)
            {
                spriteRenderer.flipX = false;
            }
        }

        GetComponent<Rigidbody>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody>().velocity.y);
        
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");
        isGrounded = true;
        if (collision.gameObject.tag == "Enemy")
        {
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.LoseLife(1);
            }
            Destroy(this.gameObject);
        }
    }
    void OnCollisionExit(Collision collision)
    {
        Debug.Log("OnCollisionExit");
        isGrounded = false;
    }
    
}
