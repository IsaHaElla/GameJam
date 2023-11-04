using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    Rigidbody physicsBody;

    [SerializeField] Vector2 moveDirection;
    [SerializeField] float speed;
    [SerializeField] float lifeTime = 8.0f;

    public Transform playerPosition;
    public SpriteRenderer playerRotation;

    // Start is called before the first frame update
    void Start()
    {
        Initialized();
        StartCoroutine("DisableAfterLifetime");
        if (playerRotation.flipX == true)
        {
            float posX = 0 - playerPosition.position.x;
            moveDirection = new Vector2(posX, 0);
        } else
        {
            float posX = playerPosition.position.x;
            moveDirection = new Vector2(posX, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = moveDirection * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Collision with Enemy");
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Collidable")
        {
            Debug.Log("Collision with Object");
            Destroy(this.gameObject);
        }
    }
    void Initialized()
    {
        physicsBody = GetComponent<Rigidbody>();
    }

    IEnumerator DisableAfterLifetime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }
}
