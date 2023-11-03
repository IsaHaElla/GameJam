using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    [SerializeField] int currentHealth = 0;
    int maxHealth = 1;
    bool isDead = false;

    [SerializeField] GameObject hp_image;
    [SerializeField] GameObject[] hp_images;
    public void LoseLife(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void AddLife(int healing)
    {
        if (!isDead)
        {
            currentHealth += healing;
            if (currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
            }
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
