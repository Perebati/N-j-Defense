using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;

    private static int totalPoints = 0;

    private void Awake()
    {            
        instance = this;

        DontDestroyOnLoad(instance);
    }

    private void Start()
    {
        totalPoints = 0;
    }

    public void UpdatePoints(float value)
    {
        totalPoints += (int) value;
    }

}
