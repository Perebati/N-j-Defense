using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Monument : MonoBehaviour
{
    private GameObject monumentsHolder;

    private Coroutine cr = null;

    [SerializeField] private int index;

    void Start()
    {
        monumentsHolder = transform.parent.gameObject;
    }


    public void TakingDamage()
    {
        transform.parent.GetComponent<MonumentsHolder>().ShowMessage(gameObject.name, index);
    }

    public void DestroyMonument()
    {
        //animacao
        StartCoroutine(AnimationTriggered());
    }

    IEnumerator AnimationTriggered()
    {
        yield return null;
        float timer = 0;
        while (timer < 3)
        { 
            timer += 3f;
            yield return new WaitForSeconds(3f);
        }

        gameObject.SetActive(false);

        CheckEndGame();

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
        Debug.Log("Todos os monumentos destruidos");
    }

}
