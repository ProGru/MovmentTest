using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Glowny zarzadca gry:
/// -tury
/// -zamki
/// -jednostki
/// -eventy
/// </summary>
public class MainManager : MonoBehaviour {
    public DestroyJednostki destroyJednostki;
    public DestroyKamera destroyKamera;
    public DestroyTeren destroyTeren;
    public DestroyZamki destroyZamki;
    public DestroyInterfejs destroyInterfejs;
    public ArrayList army= new ArrayList();
    public MenuFunctions menuFunctions;
    // drogi/zamek/targ/plac cwiczen/koszary/fortyfikacja
    public int gold = 1000;
    private ArrayList rekrutacjaQueue = new ArrayList();
    private int[] rekrutacjaTime = new int[] { 2, 1, 2, 3, 2 };
    private int[] bouldingLevel = new int[] { 0, 0, 0, 0, 0, 0 };
    public int[] bouldingbonus = new int[] { 0, 0, 1000, 0, 0, 0 };
    static private int[] bouldingAddBonus = new int[] {100,100,100,100,100,100};
    private int[] bouldingProgres = new int[] { 0, 0, 0, 0, 0, 0 };
    private int[] bouldingTime = new int[] { 2, 2, 3, 2, 3, 2 };
    private int tura = 0;
    private CastleEntry[] castles;
    private ObjectTransform[] wojska;
    private int[] drogiGoldForLvl = new int[] { 400, 400, 400, 400, 400 };
    private int[] zamekGoldForLvl = new int[] { 600, 900 };
    private int[] targGoldForLvl = new int[] { 700, 1200 };
    private int[] placGoldForLvl = new int[] { 500, 700 };
    private int[] koszaryGoldForLvl = new int[] { 800, 1600 };
    private int[] fortyfikacjaGoldForLvl = new int[] { 300, 500 };
    private int[] armyGoldForEach = new int[] { 100, 100, 100, 100, 100 };
    private int paymentPerSingleWarrior = 5;


    public void Start()
    {
        //menuFunctions = GetComponent<MenuFunctions>();
        castles = Component.FindObjectsOfType<CastleEntry>();
        Soldier[] objectTransform = FindObjectsOfType<Soldier>();
        for (int i = 0; i < objectTransform.Length; i++)
        {
            army.Add(objectTransform[i]);
        }

        for (int i = 0; i < objectTransform.Length; i++)
        {
            objectTransform[i].GetComponent<ObjectTransform>().SetCastelsUnvisibility();
        }
    }

    public void setParentJednostki(GameObject obj)
    {
        obj.transform.parent = destroyJednostki.transform;
    }

    public CastleEntry[] getCastles()
    {
        return castles;
    }

    public void nextRound()
    {
        ObjectTransform[] wojska = FindObjectsOfType<ObjectTransform>();
        for (int i = 0; i < wojska.Length; i++)
        {
            wojska[i].makeDistance = 0;
        }
        tura += 1;
        addBouldingBonus();
        addRekrutowaneJednostki();
        wyplata();
        poborZaJednostki();
    }

    public int getTura()
    {
        return tura;
    }

    public string[] getWojskaName()
    {
        wojska = FindObjectsOfType<ObjectTransform>();
        string[] name = new string[wojska.Length];
        for (int i = 0; i < wojska.Length; i++)
        {
            name[i] = wojska[i].transform.name;
        }
        return name;
    }

    public void CleanCanEntryAndCounter()
    {
        ObjectTransform[] toClean = FindObjectsOfType<ObjectTransform>();
        for (int i = 0; i < toClean.Length; i++)
        {
            toClean[i].canEntry = true;

            if (toClean[i].GetComponent<ObjectStack>() != null)
            {
                toClean[i].GetComponent<ObjectStack>().counter = 0;
            }

        }
    }

    public void rozbudowaDrogi()
    {
        if (bouldingProgres[0] >= 0)
        {
            if (bouldingLevel[0] < drogiGoldForLvl.Length)
            {
                if (gold >= drogiGoldForLvl[bouldingLevel[0]])
                {
                    gold -= drogiGoldForLvl[bouldingLevel[0]];
                    menuFunctions.GoldButton();
                    bouldingProgres[0] -= bouldingTime[0];
                    bouldingTime[0] += 1;
                }
                else
                {
                    Debug.Log("not enought money!");
                }
            }
            else
            {
                Debug.Log("Max boulding level!");
            }
        }
        else
        {
            Debug.Log("arleady in progres wait for finish to bouild more!");
        }
    }

    public void rozbudowaZamku()
    {
        if (bouldingProgres[1] >= 0)
        {
            if (bouldingLevel[1] < zamekGoldForLvl.Length)
            {
                if (gold >= zamekGoldForLvl[bouldingLevel[1]])
                {
                    gold -= zamekGoldForLvl[bouldingLevel[1]];
                    menuFunctions.GoldButton();
                    bouldingProgres[1] -= bouldingTime[1];
                    bouldingTime[1] += 1;
                }else
                {
                    Debug.Log("not enought money!");
                }
            }else
            {
                Debug.Log("Max boulding level!");
            }
        }
        else
        {
            Debug.Log("arleady in progres wait for finish to bouild more!");
        }
    }

    public void rozbudowaTargu()
    {
        if (bouldingProgres[2] >= 0)
        {
            if (bouldingLevel[2] < targGoldForLvl.Length)
            {
                if (gold >= targGoldForLvl[bouldingLevel[2]])
                {
                    gold -= targGoldForLvl[bouldingLevel[2]];
                    menuFunctions.GoldButton();
                    bouldingProgres[2] -= bouldingTime[2];
                    bouldingTime[2] += 1;
                }
                else
                {
                    Debug.Log("not enought money!");
                }
            }
            else
            {
                Debug.Log("Max boulding level!");
            }
        }
        else
        {
            Debug.Log("arleady in progres wait for finish to bouild more!");
        }
    }

    public void rozbudowaPlacu()
    {
        if (bouldingProgres[3] >= 0)
        {
            if (bouldingLevel[3] < placGoldForLvl.Length)
            {
                if (gold >= placGoldForLvl[bouldingLevel[3]])
                {
                    gold -= placGoldForLvl[bouldingLevel[3]];
                    menuFunctions.GoldButton();
                    bouldingProgres[3] -= bouldingTime[3];
                    bouldingTime[3] += 1;
                }
                else
                {
                    Debug.Log("not enought money!");
                }
            }
            else
            {
                Debug.Log("Max boulding level!");
            }
        }
        else
        {
            Debug.Log("arleady in progres wait for finish to bouild more!");
        }
    }

    public void rozbudowaKoszar()
    {
        if (bouldingProgres[4] >= 0)
        {
            if (bouldingLevel[4] < koszaryGoldForLvl.Length)
            {
                if (gold >= koszaryGoldForLvl[bouldingLevel[4]])
                {
                    gold -= koszaryGoldForLvl[bouldingLevel[4]];
                    menuFunctions.GoldButton();
                    bouldingProgres[4] -= bouldingTime[4];
                    bouldingTime[4] += 1;
                }
                else
                {
                    Debug.Log("not enought money!");
                }
            }
            else
            {
                Debug.Log("Max boulding level!");
            }
        }
        else
        {
            Debug.Log("arleady in progres wait for finish to bouild more!");
        }
    }

    public void rozbudowaFortyfikacji()
    {
        if (bouldingProgres[5] >= 0)
        {
            if (bouldingLevel[5] < fortyfikacjaGoldForLvl.Length)
            {
                if (gold >= fortyfikacjaGoldForLvl[bouldingLevel[5]])
                {
                    gold -= fortyfikacjaGoldForLvl[bouldingLevel[5]];
                    menuFunctions.GoldButton();
                    bouldingProgres[5] -= bouldingTime[5];
                    bouldingTime[5] += 1;
                }
                else
                {
                    Debug.Log("not enought money!");
                }
            }
            else
            {
                Debug.Log("Max boulding level!");
            }
        }
        else
        {
            Debug.Log("arleady in progres wait for finish to bouild more!");
        }
    }

    public void addBouldingBonus()
    {
        for (int i=0; i < bouldingProgres.Length; i++)
        {
            if (bouldingProgres[i] != 0)
            {
                bouldingProgres[i] += 1;
                if (bouldingProgres[i] == 0)
                {
                    bouldingbonus[i] += bouldingAddBonus[i];
                    bouldingLevel[i] += 1;
                    Debug.Log("Bonus is now:" + bouldingbonus[i].ToString());
                }
            }
        }
    }

    public void rekrutujJednostke(CastleEntry castle, int typeOfWarior)
    {
        RekrutacjaWojska rekrutacjaWojska = new RekrutacjaWojska();
        rekrutacjaWojska.castle = castle;
        rekrutacjaWojska.typeOfWarior = typeOfWarior;
        rekrutacjaWojska.rouldLeft = -rekrutacjaTime[typeOfWarior];
        rekrutacjaQueue.Add(rekrutacjaWojska);
        gold -= armyGoldForEach[typeOfWarior];
        menuFunctions.GoldButton();
    }

    public void addRekrutowaneJednostki()
    {
        for (int i = 0; i < rekrutacjaQueue.Count; i++)
        {
            RekrutacjaWojska rekrutacjaWojska = (RekrutacjaWojska) rekrutacjaQueue[i];
            Debug.Log(rekrutacjaWojska.rouldLeft);
            if (rekrutacjaWojska.rouldLeft == -1)
            {
                rekrutacjaWojska.castle.quantityMilitary[rekrutacjaWojska.typeOfWarior] += 50;
                rekrutacjaWojska.rouldLeft += 1;
            }
            else
            {
                rekrutacjaWojska.rouldLeft += 1;
            }
        }
        for (int i = 0; i < rekrutacjaQueue.Count; i++)
        {
            RekrutacjaWojska rekrutacjaWojska = (RekrutacjaWojska)rekrutacjaQueue[i];
            if (rekrutacjaWojska.rouldLeft == 0)
            {
                rekrutacjaQueue.RemoveAt(i);
            }
        }


    }

    public void poborZaJednostki()
    {
        int payment = 0;
        for (int i = 0; i < castles.Length; i++)
        {
            if (castles[i].wrogosc == 0)
            {
                for (int j = 0; j < castles[i].quantityMilitary.Length; j++)
                {
                    payment += castles[i].quantityMilitary[j] * paymentPerSingleWarrior;
                }
            }
        }
        Soldier[] soldiers = FindObjectsOfType<Soldier>();
        for (int i = 0; i < soldiers.Length; i++)
        {
            if (soldiers[i].wrogosc == 0)
            {
                Millitary millitary = soldiers[i].GetComponent<Millitary>();
                if (millitary.multi)
                {
                    for (int j = 0; j < millitary.quantityMilitaryMulti.Length; j++)
                    {
                        payment += millitary.quantityMilitaryMulti[j] * paymentPerSingleWarrior;
                    }
                }
                else
                {
                    payment += millitary.quantityMilitarySolo * paymentPerSingleWarrior;
                }
            }
        }
        gold -= payment;
    }

    public void wyplata()
    {
        gold += bouldingbonus[2];
    }
}
