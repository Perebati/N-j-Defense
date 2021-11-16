using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] public float maxHealth = 100f;


    private float currentHealth;
    
    private void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(float value)
    {
        currentHealth -= value;

        if (CompareTag("Player"))
            HealthBar.instance.UpdateSlider(-value);

        if (currentHealth <= 0)
        {
            Die();
        }

    }
    private void Die()
    {
        if (!gameObject.CompareTag("Player"))
        {
            Enemy enemy = this.gameObject.GetComponent<Enemy>();
            //animacao
            GameManager.instance.UpdatePoints(enemy.enemyDamage);
            SpawnManager.currentActiveTroops--;
            PointsUpdate.UpdatePoints((int)enemy.enemyDamage * 10);

            Destroy(this.gameObject, 0f); //tempo de animacao
        } else
        {
            // game over, menu
            Debug.Log("Morri, me erra");
        }
    }

}
