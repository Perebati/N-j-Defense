using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    private Collider col;
    [SerializeField] private float damage;

    private float timer = 0f;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = .1f;
        col = GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= .5f && Input.GetMouseButtonDown(0))
        {
            audioSource.Play();
            animator.SetTrigger("Katana");
            timer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.GetComponent<Health>() == null)
                return;
            other.GetComponent<Health>().TakeDamage(damage);
        }
    }



}
