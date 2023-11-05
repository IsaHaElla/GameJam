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
        Destroy(this.gameObject); //nicht gut zu destroyen, vor allem wenn man von nem checkpoint wieder lädt. Lieber disablen/enablen - Abdullah
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
