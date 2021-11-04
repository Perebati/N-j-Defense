using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;

    public float playerMaxHealth;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(instance);      
    }
}
