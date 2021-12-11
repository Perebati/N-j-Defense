using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] public float maxHealth = 100f;


    [HideInInspector] public float currentHealth;

    [SerializeField] private AudioClip deathSound;
    
    private void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(float value)
    {
        currentHealth -= value;

        if (CompareTag("Player"))
            HealthBar.instance.UpdateSlider(-value);
        if (CompareTag("Monument"))
            GetComponent<Monument>().TakingDamage(value);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = this.gameObject.GetComponent<Enemy>();
            //animacao
            SpawnManager.currentActiveTroops--;
            PointsUpdate.UpdatePoints((int)enemy.enemyDamage * 10);

            GFX gfx = transform.GetComponentInChildren<GFX>();
            enemy.GetComponent<AudioSource>().PlayOneShot(deathSound);

            Destroy(this.gameObject, .1f); //tempo de animacao

        } else
        if (gameObject.CompareTag("Monument"))
        {
            gameObject.GetComponent<Monument>().DestroyMonument();

        } else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("EndScene");
            Debug.Log("Morri, me erra");
        }
    }

}
