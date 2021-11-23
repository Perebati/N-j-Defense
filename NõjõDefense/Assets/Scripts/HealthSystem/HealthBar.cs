using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar instance;
    private Slider slider;
    private GameObject player;
    private Coroutine regen;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
        DontDestroyOnLoad(instance);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        slider = this.gameObject.GetComponent<Slider>();
        slider.maxValue = player.GetComponent<Health>().maxHealth;
        slider.value = slider.maxValue;
    }

    public void UpdateSlider(float value)
    {
        slider.value += value;
        if (regen != null)
            StopCoroutine(regen);
        regen = StartCoroutine(RegenHealth());
    }

    private IEnumerator RegenHealth()
    {
        yield return new WaitForSeconds(3f);

        while (slider.value < slider.maxValue)
        {
            slider.value += slider.maxValue / 100;
            player.GetComponent<Health>().currentHealth = slider.value;
            yield return new WaitForSeconds(.1f);
        }

        regen = null;
        yield return null;

    }

}
