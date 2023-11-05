using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int currentHealth = 3;
    int maxHealth = 3;
    bool isDead = false;

    [SerializeField] GameObject deathVFX;

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
        GameObject newDeathEffect = Instantiate(deathVFX, transform.position, Quaternion.identity);
        newDeathEffect.transform.SetParent(this.transform);
        Destroy(this.gameObject);
    }
}
