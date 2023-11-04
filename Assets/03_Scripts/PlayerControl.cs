using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody physicsBody;

    [Header("Animations")]
    private Animator playerAnims;

    [Header("Movement Settings")]
    [SerializeField] Vector3 moveDirection;
    [SerializeField] float speed;
    [SerializeField] float jump = 2f;
    float moveVelocity;
    bool isGrounded;

    [Header("Shooting")]
    //public SpriteRenderer spriteRenderer;
    public GameObject BulletPrefab;
    private GameObject bulletOrigin;


    // Start is called before the first frame update
    void Start()
    {
        physicsBody = GetComponent<Rigidbody>();
        playerAnims = GetComponentInChildren<Animator>();
        //physicsBody.AddForce(moveDirection*speed);
    }
     // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            /*ABDULLAHS COMMENT: set currentBullet to null when bullet hits an objects or if its lifetime is over
            *currentBullet = Instantiate(BulletPrefab, new Vector3(transform.position.x, 1.6f, transform.position.z), Quaternion.identity);
            *if (currentBullet)
            *{
            *    currentBullet.GetComponentInChildren<BulletBehavior>().playerRotation = this.spriteRenderer;
            *}
            **/
            Instantiate(BulletPrefab, bulletOrigin.transform.position, Quaternion.identity);
        }

        if (isGrounded == true)
        {
            //jumping
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("ItJumps!");
                physicsBody.velocity = new Vector2(GetComponent<Rigidbody>().velocity.y, jump);
                playerAnims.SetTrigger("Jump");
            }
        }
        //physicsBody.MovePosition(transform.position + moveDirection * Time.deltaTime * speed);
        moveVelocity = 0;

        //Left Right Movement
        if ( Input.GetKey(KeyCode.A))
        {
            moveVelocity = -speed;
            /*if (spriteRenderer.flipX == false)
            { 
                spriteRenderer.flipX = true;
            }*/

            //Abdullahs code
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveVelocity = speed;
            /*if (spriteRenderer.flipX)
            {
                spriteRenderer.flipX = false;
            }*/
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }

        physicsBody.velocity = new Vector2(moveVelocity, GetComponent<Rigidbody>().velocity.y);

        //play animation
        playerAnims.SetFloat("moveX", Mathf.Abs(physicsBody.velocity.x));
        playerAnims.SetBool("IsGrounded", isGrounded);
        
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
