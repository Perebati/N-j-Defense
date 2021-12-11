using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Monument : MonoBehaviour
{
    private GameObject monumentsHolder;

    private Coroutine cr = null;

    [SerializeField] private int index;

    [SerializeField] private MonumentHealthUI monumentUI;

    void Start()
    {
        monumentsHolder = transform.parent.gameObject;
    }


    public void TakingDamage(float value)
    {
        transform.parent.GetComponent<MonumentsHolder>().ShowMessage(gameObject.name, index);
        monumentUI.UpdateHealth(value);
    }

    public void DestroyMonument()
    {
        //animacao
        AnimationTriggered();
    }

    private void AnimationTriggered()
    {
       
        monumentUI.gameObject.SetActive(false);
        gameObject.SetActive(false);
        monumentsHolder.GetComponent<MonumentsHolder>().CheckEndGame();
    }

    private void CheckEndGame()
    {
       bool endgame = true;
       for (int i = 0; i < monumentsHolder.transform.childCount; i++)
       {
            if (monumentsHolder.transform.GetChild(i).gameObject.activeSelf)
            {
                endgame = false;
            }
       }

        if (endgame)
            EndGame();
    }

    private void EndGame()
    {
        //SceneManager.LoadScene("EndScene");
        Debug.Log("Todos os monumentos destruidos");
    }

}
