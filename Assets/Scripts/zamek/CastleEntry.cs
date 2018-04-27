using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Funkcja dla zamkow umozliwia:
/// -wkladanie jednostek do zamku
/// -ustawienie przynaleznosci (sojusz/wrog/my)
/// -wyswietlenie okna umozliwiajacego wyjecie jednostki
/// </summary>
public class CastleEntry : MonoBehaviour {
    public int[] quantityMilitary = new int[] { 0, 0, 0, 0, 0 };
    // 0-nasze, 1-wrog, >1 -sojusz
    public int wrogosc = 0;
    public MenuFunctions menuFunctions;
    private Collider lastCollision;

    private void Start()
    {
        if (quantityMilitary.Length == 0)
        {
            quantityMilitary = new int[] { 0, 0, 0, 0, 0 };
        }
        menuFunctions = menuFunctions.GetComponent<MenuFunctions>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ObjectTransform>().canEntry)
        {
            if (other.GetComponent<ObjectTransform>().wrogosc == wrogosc)
            {
                quantityMilitary[other.GetComponent<ObjectTransform>().typeOfWarior] += other.GetComponent<ObjectTransform>().quantityMilitary;
                Destroy(other.gameObject);
            }else if (other.GetComponent<ObjectTransform>().wrogosc > 1)
            {
                //quantityMilitary += other.GetComponent<ObjectTransform>().quantityMilitary;
                //Destroy(other.gameObject);
            }else
            {
                quantityMilitary[other.GetComponent<ObjectTransform>().typeOfWarior] -= other.GetComponent<ObjectTransform>().quantityMilitary;
                Destroy(other.gameObject);
            }
        }
    }

    private void OnMouseDown()
    {
        //Angela Tutaj!
        menuFunctions.ReloadCanvas(this.gameObject);
    }
}
