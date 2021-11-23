using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private Camera mainCam;
    private GameObject player;

    private float area = 10f;

    private float timer;
    private void Start()
    {
        mainCam = Camera.main;
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
            float volume = 1 - Vector3.Distance(col.transform.position, mainCam.transform.position) / 10;
            col.gameObject.GetComponent<AudioSource>().volume = volume/2;
            col.gameObject.GetComponent<AudioSource>().Play();
        }
    }

}
