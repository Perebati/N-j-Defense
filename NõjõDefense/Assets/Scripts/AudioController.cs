using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private Transform mainCam;
    private GameObject player;

    private float area = .001f;

    private float timer;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3f)
        {
            PlayAudios();
            timer = 0;
        }
    }

    private void PlayAudios()
    {
        Collider[] colliders = Physics.OverlapSphere(player.transform.position, area);
        foreach (var col in colliders)
        {
            if (!col.CompareTag("Enemy"))
                return;
            float volume = 1 - Vector3.Distance(col.transform.position, mainCam.transform.position) / area;
            col.gameObject.GetComponent<AudioSource>().volume = volume/2.0f;
            col.gameObject.GetComponent<AudioSource>().Play();
        }
    }

}
