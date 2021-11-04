using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;

    private void Awake()
    {     
        
        instance = this;

        DontDestroyOnLoad(instance);
    }
}
