using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleEntry : MonoBehaviour {
    public int quantityMilitary = 0;
    public MenuFunctions menuFunctions;
    private void Start()
    {
        menuFunctions = menuFunctions.GetComponent<MenuFunctions>();
    }

    private void OnTriggerEnter(Collider other)
    {
        quantityMilitary += other.GetComponent<ObjectTransform>().quantityMilitary;
        Destroy(other.gameObject);
    }

    private void OnMouseDown()
    {
        menuFunctions.ReloadCanvas();
    }
}
