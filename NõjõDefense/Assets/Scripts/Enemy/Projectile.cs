using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float bulletSpeed = 10f;
    [SerializeField] private float projectileDamage = 10f;

    private Collider col;


    public void Start()
    {
        col = GetComponent<Collider>();
        Destroy(this.gameObject, 4f);
    }

    private void OnCollisionEnter(Collision other)
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
