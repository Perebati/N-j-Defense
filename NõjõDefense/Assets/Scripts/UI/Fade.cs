using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    private Image img;
    void Start()
    {
        img = GetComponent<Image>();
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(.3f);

        float alpha = 1;
        while(img.color.a > 0)
        {
            alpha -= .01f;
            img.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        yield return new WaitForSeconds(3f);

        if (SceneManager.GetActiveScene().name == "EndScene")
            SceneManager.LoadScene("MainMenu");

        yield return null;
    }

}
