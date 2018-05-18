using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Millitary : MonoBehaviour {
    public bool multi = false;
    public int quantityMilitarySolo = 0;
    public int[] quantityMilitaryMulti = new int[] { 0, 0, 0, 0, 0 };


    private void Start()
    {
        if (quantityMilitaryMulti.Length == 0)
        {
            quantityMilitaryMulti = new int[] { 0, 0, 0, 0, 0 };
        }
    }
}
