using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZamki : MonoBehaviour
{
    public static DestroyZamki Instance;
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
