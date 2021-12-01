using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GFX : MonoBehaviour
{
    private Enemy enemy;
    private Camera cam;
    private GameObject player;
    public Transform firePoint;
    [HideInInspector]public Animator anim;
    private Coroutine cr = null;

    void Start()
    {
        enemy = transform.parent.GetComponent<Enemy>();
        cam = Camera.main;
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
    }

    void LateUpdate()
    {
        transform.LookAt(enemy._target);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        return;    
    }

    public void Attack()
    {
        anim.SetBool("Attack", true);
       
        //StartCoroutine(Cooldown(enemy.attackCooldown));
    }

    public void Die()
    {
        anim.SetTrigger("Die");
    }

    public void Turn()
    {
        anim.SetBool("Turn", true);
        int rng = Random.Range(0, 2);
        if (rng == 0)
            anim.SetTrigger("Turn1");
        else
            anim.SetTrigger("Turn2");

        
        StartCoroutine(TurnAnim());
    }

    IEnumerator Cooldown(float x)
    {

        yield return new WaitForSeconds(x);

        anim.SetBool("Attack", false);

        cr = null;
        yield return null;
    }
    IEnumerator TurnAnim()
    {

        yield return new WaitForSeconds(0.35f);

        anim.SetBool("Turn", false);

        cr = null;
        yield return null;
    }

} 
