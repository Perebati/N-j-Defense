using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreTxt : MonoBehaviour
{
    private TextMeshProUGUI pointsTMP;

    void Start()
    {
        pointsTMP = this.gameObject.GetComponent<TextMeshProUGUI>();
        pointsTMP.text = "High Score: " + HighScoreSC.points;
    }
}
