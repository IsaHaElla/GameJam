using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    [SerializeField] int currentHealth = 1;
    int maxHealth = 1;
    bool isDead = false;
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
        Destroy(this.gameObject);
    }
    private void Start()
    {
       
        /* while (currentHealth < maxHealth)
        {
        currentHealth++;
        }*/

        //currentHealth = maxHealth;
    }
    private void Update()
    {
   
    }
}
