using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    Rigidbody physicsBody;

    [SerializeField] Vector3 moveDirection;
    [SerializeField] float speed;
    [SerializeField] float lifeTime = 8.0f;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Initialized();
        StartCoroutine("DisableAfterLifetime");

        if (player.transform.localScale.x > 0)
        {
            moveDirection = new Vector3(1, 0, 0);
        }
        if (player.transform.localScale.x < 0)
        {
            moveDirection = new Vector3(-1, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Collision with Enemy");
            Destroy(this.gameObject);
            EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
            enemy.LoseLife(1);
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
