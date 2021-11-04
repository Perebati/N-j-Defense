using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    
    private void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(float value)
    {
        currentHealth -= value;

        if (currentHealth <= 0)
        {
            Die();
        }

    }
    private void Die()
    {
        if (!gameObject.CompareTag("Player")) // enemy, monument
        {
            //animacao
            Destroy(this.gameObject, 0f); //tempo de animacao
        } else
        {
            // game over, menu
            Debug.Log("Morri, me erra");
        }
    }

}
