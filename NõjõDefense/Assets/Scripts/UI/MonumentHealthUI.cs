using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonumentHealthUI : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private GameObject monument; // referenciar
    private Coroutine regen;
    void Start()
    {
        slider = this.gameObject.GetComponent<Slider>();
        slider.maxValue = monument.GetComponent<Health>().maxHealth;
        slider.value = slider.maxValue;
    }
    public void UpdateHealth(float value)
    {
        slider.value -= value;
        if (slider.value <= 0)
            this.enabled = false;
        if (regen != null)
            StopCoroutine(regen);
        regen = StartCoroutine(RegenHealth());
    }

    public void SetMaxHealth()
    {
        slider.value = slider.maxValue;
    }

    private IEnumerator RegenHealth()
    {
        yield return new WaitForSeconds(3f);

        while (slider.value < slider.maxValue)
        {
            slider.value += slider.maxValue / 100;
            monument.GetComponent<Health>().currentHealth = slider.value;
            yield return new WaitForSeconds(.1f);
        }

        regen = null;
        yield return null;

    }

}
