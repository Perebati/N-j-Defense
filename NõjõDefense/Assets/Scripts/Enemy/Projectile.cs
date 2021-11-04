using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float bulletSpeed = 10f;
    [SerializeField] private float projectileDamage = 10f;


    public void Start()
    {
        Destroy(this.gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Health>().TakeDamage(projectileDamage);
            // efeito
            Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
            //efeito
            Destroy(this.gameObject);
        }

    }

}
