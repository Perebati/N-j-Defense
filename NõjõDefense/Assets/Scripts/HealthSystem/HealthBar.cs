using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar instance;
    private Slider slider;
    private GameObject player;

    private void Awake()
    {
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
    }

}
