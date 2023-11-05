using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    [SerializeField] int currentHealth = 1;
    int maxHealth = 1;
    bool isDead = false;

    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject respawnPoint;

    private void Start()
    {
        respawnPoint = GameObject.Find("Spawnpoint");
    }

    public void LoseLife(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        GetComponentInChildren<Animator>().SetTrigger("Die");
        GameObject newDeathVFX = Instantiate(deathVFX, transform.position, Quaternion.identity);
        newDeathVFX.transform.SetParent(this.transform);
        Destroy(newDeathVFX, 2);
        DisablePlayerSettings();
    }

    void DisablePlayerSettings()
    {
        GetComponent<PlayerControl>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;
    }

    void EnablePlayerSettings()
    {
        GetComponent<PlayerControl>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Collider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && gameObject.tag == "Player")
        {
            LoseLife(1);
        }
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(2);
        this.transform.position = respawnPoint.transform.position;
        EnablePlayerSettings();
    }
    
}
