using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PointsUpdate : MonoBehaviour
{

    private static TextMeshProUGUI pointsTMP;

    public static int maxPoints = 0;

    void Start()
    {
        pointsTMP = this.gameObject.GetComponent<TextMeshProUGUI>();
        pointsTMP.text = "0";
    }

    public static void UpdatePoints(int value)
    {
        int totalPoints = Int32.Parse(pointsTMP.text);
        totalPoints += value;
        pointsTMP.text = "" + totalPoints;
        if (totalPoints > maxPoints)
        {
            HighScoreSC.points = totalPoints;
            maxPoints = totalPoints;
        }
    }

}
