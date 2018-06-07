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

    public void setCastleColor(int i)
    {
        Renderer rend = GetComponent<Renderer>();
        if (i == 0)
        {
            rend.material.SetColor("_Color", Color.black);
        }else
        {
            rend.material.SetColor("_Color", Color.red);
        }
    }

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
        menuFunctions.ReloadCanvas(this.gameObject);
    }

    void showQuantityMilitaryInfo()
    {
       Debug.Log("index 0:" + quantityMilitary[0]);

    }

    private void OnMouseEnter()
    {
        //showQuantityMilitaryInfo();
        menuFunctions.PopupWindow("Stark");
    }

    private void OnMouseExit()
    {
        //showQuantityMilitaryInfo();
        menuFunctions.PopupWindowFalse();
    }
}
