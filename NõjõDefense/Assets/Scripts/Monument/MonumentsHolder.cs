using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonumentsHolder : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] monumentText;
    private Coroutine cr = null;

    public void ResetMonument()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).gameObject.activeSelf)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                transform.GetChild(i).gameObject.GetComponent<Health>().currentHealth = transform.GetChild(i).gameObject.GetComponent<Health>().maxHealth;
            }
        }
    }

    public void ShowMessage(string name, int index)
    {
        if (cr == null)
        {
            cr = StartCoroutine(TakeDamage(name, index));
        }
        
    }

    IEnumerator TakeDamage(string name, int index)
    {
        Color color = monumentText[index].color;
        color.a = 1f;
        monumentText[index].color = color;
        monumentText[index].text = transform.GetChild(index).gameObject.name + " is taking damage!";
        monumentText[index].gameObject.SetActive(true);
        while (monumentText[index].color.a > 0)
        {
            color.a -= 0.01f;
            monumentText[index].color = color;
            yield return new WaitForSeconds(0.01f);
        }

        monumentText[index].gameObject.SetActive(false);
        cr = null;
        yield return null;
    }

}
