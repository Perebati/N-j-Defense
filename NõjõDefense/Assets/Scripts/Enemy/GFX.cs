using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GFX : MonoBehaviour
{
    private Enemy enemy;
    private Camera cam;
    public Transform firePoint;
    
    void Start()
    {
        enemy = transform.parent.GetComponent<Enemy>();
        cam = Camera.main;
    }

    void LateUpdate()
    {
        transform.LookAt(enemy._target);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }
}
