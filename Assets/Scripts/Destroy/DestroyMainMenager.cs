using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMainMenager : MonoBehaviour
{
    public static DestroyMainMenager Instance;
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
