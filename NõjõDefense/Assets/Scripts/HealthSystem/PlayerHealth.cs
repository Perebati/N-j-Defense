using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float maxHealth = 0;
    private float currentHealth = 0;

    //[SerializeField] private Slider slider;


    void Start()
    { 
        maxHealth = GameManager.instance.playerMaxHealth;
        currentHealth = maxHealth;
        //slider.highValue = maxHealth;
    }

    public void TakeDamage(float value)
    {
        currentHealth -= value;
        //slider.value -= value;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Heal(float value)
    {
        currentHealth += value;
        //slider.value += value;
    }

    private void Die()
    {
        Debug.Log("Died");
    }
}
