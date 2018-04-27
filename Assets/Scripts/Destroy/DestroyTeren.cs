using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTeren : MonoBehaviour
{
    public static DestroyTeren Instance;
    public static bool destroy = false;

    void Awake()
    {
        if (Instance == null & !destroy)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

