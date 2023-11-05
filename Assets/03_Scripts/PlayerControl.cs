using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    Rigidbody physicsBody;

    [Header("Animations")]
    private Animator playerAnims;

    [Header("Movement Settings")]
    [SerializeField] Vector3 moveDirection;
    [SerializeField] float maxSpeed ;
    [SerializeField] float speed;
    [SerializeField] float speedDecreaseSpiritForm;
    [SerializeField] float jump = 2f;
    float moveVelocity;
    [SerializeField] bool isGrounded;

    [Header("Spawnpoint and next Scene")]
    Vector3 lastSpawnPoint;
    [SerializeField] string sceneToLoad;

    [Header("Shooting")]
    //public SpriteRenderer spriteRenderer;
    public GameObject BulletPrefab;
    [SerializeField] private GameObject bulletOrigin;

    [Header("Form Switch")]
    [SerializeField] bool inSpiritForm;
    [SerializeField] float spiritTime = 3f;
    [SerializeField] float spiritCooldown = 5f;
    [SerializeField] float switchAnimationTime = 2f;
    [SerializeField] bool inCooldown;
    [SerializeField] GameObject normalSprite;
    [SerializeField] GameObject spiritSprite;
    [SerializeField] GameObject switchVFX;

    [Header("Dreamdoor")]
    [SerializeField] GameObject[] dreamdoors;


    // Start is called before the first frame update
    void Start()
    {
        inCooldown = false;
        speed = maxSpeed;
        inSpiritForm = false;
        physicsBody = GetComponent<Rigidbody>();
        playerAnims = GetComponentInChildren<Animator>();
        //physicsBody.AddForce(moveDirection*speed);
    }
     // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inCooldown == false) { switchForm(); }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            /*ABDULLAHS COMMENT: set currentBullet to null when bullet hits an objects or if its lifetime is over
            *currentBullet = Instantiate(BulletPrefab, new Vector3(transform.position.x, 1.6f, transform.position.z), Quaternion.identity);
            *if (currentBullet)
            *{
            *    currentBullet.GetComponentInChildren<BulletBehavior>().playerRotation = this.spriteRenderer;
            *}
            **/
            ShootBullet();
            playerAnims.SetTrigger("Shoot");
        }

        if (isGrounded == true && inSpiritForm == false)
        {
            //jumping
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("ItJumps!");
                physicsBody.velocity = new Vector2(GetComponent<Rigidbody>().velocity.y, jump);
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

    void ShootBullet()
    {
        GameObject newBullet = Instantiate(BulletPrefab, bulletOrigin.transform.position, Quaternion.identity);
        newBullet.GetComponent<BulletBehavior>().player = transform.gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");

        if (collision.gameObject.tag == "Dreamdoor" && inSpiritForm== false)
        {
            Debug.Log("Dreamdoor Collision");
        }

        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeathZone")
        {
            Debug.Log("DeathZone Collision");
            teleportToSpawnPoint();
        }

        if (other.gameObject.tag == "SpawnPoint")
        {
            Debug.Log("SpawnPoint Collision");
            lastSpawnPoint = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        }

        if (other.gameObject.tag == "WinZone")
        {
            Debug.Log("WinZone Collision");
            SceneManager.LoadScene(sceneToLoad);
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("OnCollisionExit Ground");
            isGrounded = false;
        }
    }

    void switchForm()
    {
        if (inSpiritForm == false) 
        { 
            StartCoroutine("spiritTimer");
            StartCoroutine(ToggleFormTimer());
            PlaySwitchVFX();
        } 
        /*else 
        {
            inSpiritForm = false;
            speed = maxSpeed;
            GetComponent<Health>().enabled = true;
            for (int i = 0; i < dreamdoors.Length; i++)
            {
                dreamdoors[i].GetComponent<BoxCollider>().enabled = true;
            }
            isGrounded = true;
            inCooldown = true;
            StartCoroutine("spiritTimerCooldown");
            ReturnForm();
        }*/
        //Abdullah animation für spirit einfügen
    }

    IEnumerator ToggleFormTimer()
    {
        inSpiritForm = true;
        yield return new WaitForSeconds(switchAnimationTime);
        normalSprite.SetActive(false);
        spiritSprite.SetActive(true);
        speed -= speedDecreaseSpiritForm;
        GetComponent<Health>().enabled = false;
        gameObject.layer = 7;
        StartCoroutine(ResetForm());
        /*for (int i = 0; i < dreamdoors.Length; i++)
        {
            dreamdoors[i].GetComponent<BoxCollider>().enabled = false;
        }*/
    }

    IEnumerator ResetForm()
    {
        yield return new WaitForSeconds(spiritTime);
        inSpiritForm = false;
        speed = maxSpeed;
        GetComponent<Health>().enabled = true;
        /*for (int i = 0; i < dreamdoors.Length; i++)
        {
            dreamdoors[i].GetComponent<BoxCollider>().enabled = true;
        }*/
        gameObject.layer = 0;
        isGrounded = true;
        inCooldown = true;
        StartCoroutine("spiritTimerCooldown");
        ReturnForm();
    }

    void ReturnForm()
    {
        normalSprite.SetActive(true);
        spiritSprite.SetActive(false);
    }

    void PlaySwitchVFX()
    {
        GameObject newSwitchEffect = Instantiate(switchVFX, transform.position, Quaternion.identity);
        newSwitchEffect.transform.SetParent(this.transform);
        Destroy(newSwitchEffect, 3);
    }

    void teleportToSpawnPoint()
    {
        this.transform.position = lastSpawnPoint;
    }
    IEnumerator spiritTimer()
    {
        yield return new WaitForSeconds(spiritTime);
        switchForm();
    }

    IEnumerator spiritTimerCooldown()
    {
        yield return new WaitForSeconds(spiritCooldown);
        inCooldown = false;
    }
}
