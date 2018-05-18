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
    public bool yours = false;
    public MenuFunctions menuFunctions;
    private Collider lastCollision;
    private AttackMaker attackMaker;
    MainManager mainManager;

    private void Start()
    {
        this.gameObject.AddComponent<AttackMaker>();
        attackMaker = GetComponent<AttackMaker>();
        if (quantityMilitary.Length == 0)
        {
            quantityMilitary = new int[] { 0, 0, 0, 0, 0 };
        }
        menuFunctions = menuFunctions.GetComponent<MenuFunctions>();
        mainManager = FindObjectOfType<MainManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ObjectTransform>().canEntry)
        {
            attackMaker.makeCastleAttack(other.gameObject, this.gameObject);
        }
    }

    private void OnMouseDown()
    {
        //Angela Tutaj!
        menuFunctions.ReloadCanvas(this.gameObject);
    }
}
