using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MonumentsHolder : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] monumentText;
    private Coroutine cr = null;

    [SerializeField] private GameObject[] monumentUI; 

    public void ResetMonument()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).gameObject.activeSelf)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                transform.GetChild(i).gameObject.GetComponent<Health>().currentHealth = transform.GetChild(i).gameObject.GetComponent<Health>().maxHealth;
            }
            monumentUI[i].GetComponent<MonumentHealthUI>().SetMaxHealth();
            monumentUI[i].SetActive(true);
        }

    }

    public void ShowMessage(string name, int index)
    {
        if (cr != null)
            StopCoroutine(cr);
        cr = StartCoroutine(TakeDamage(name, index));
        
        
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

    public void CheckEndGame()
    {
        bool endgame = true;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                endgame = false;
            }
        }

        if (endgame)
            EndGame();
    }

    private void EndGame()
    {
        SceneManager.LoadScene("EndScene");
        Debug.Log("Todos os monumentos destruidos");
    }

}
