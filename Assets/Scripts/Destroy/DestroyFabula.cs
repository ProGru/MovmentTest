using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFabula : MonoBehaviour
{
    public static DestroyFabula Instance;
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
