using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



/// <summary>
/// Glowny zarzadca gry:
/// -tury
/// -zamki
/// -jednostki
/// -eventy
/// </summary>
public class MainManager : MonoBehaviour {
    public string[] nazwyZamkow = { "Targaryen", "Tyrell", "Lannister", "Nikt" };
    public DestroyJednostki destroyJednostki;
    public DestroyKamera destroyKamera;
    public DestroyTeren destroyTeren;
    public DestroyZamki destroyZamki;
    public DestroyInterfejs destroyInterfejs;
    public ArrayList army= new ArrayList();
    public MenuFunctions menuFunctions;
    private TextInfoShow textInfoShow;
    private string[] bouldingNames = { "drogi", "zamek", "targ",
        "plac cwiczen", "koszary", "fortyfikacja" };
    private string[] jednostkaNemes = { "Łucznicy", "Konnica", "Piechota", "Wojownicy", "Szpiedzy" };
    // drogi/zamek/targ/plac cwiczen/koszary/fortyfikacja
    public int gold = 1000;
    private ArrayList rekrutacjaQueue = new ArrayList();
    private int[] rekrutacjaTime = new int[] { 2, 1, 2, 3, 2 };
    private int[] bouldingLevel = new int[] { 0, 0, 0, 0, 0, 0 };
    public int[] bouldingbonus = new int[] { 0, 0, 1000, 0, 0, 0 };
    static private int[] bouldingAddBonus = new int[] {100,100,2000,100,100,100};
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
    private int warningForNoMoney = 0;
    private int curentSojusz = 3;
    private int rundaSojusz = -1;
    private AttackMaker attackMaker = new AttackMaker();


    public void Start()
    {
        textInfoShow = FindObjectOfType<TextInfoShow>();
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

    public void addGold(int money)
    {
        gold += money;
        menuFunctions.GoldButton();
    }

    public int getRekrutacjaTime(int index)
    {
        return rekrutacjaTime[index];
    }

    public string getSojdierInfo(int index)
    {
        return attackMaker.getSoldierInfo(index);
    }

    public string getBouldingInfo(int index)
    {
        string message = getBouldingName(index) + "\n";
        if (index == 0)
        {
            if (drogiGoldForLvl.Length > (bouldingLevel[index]))
            {
                message += "cena za next Lvl: " + drogiGoldForLvl[bouldingLevel[index]] + "\n";
            }
            else
            {
                message += "max Lvl \n";
            }
        }else if (index == 1)
        {
            if (zamekGoldForLvl.Length > (bouldingLevel[index]))
            {
                message += "cena za next Lvl: " + zamekGoldForLvl[bouldingLevel[index]] + "\n";
            }
            else
            {
                message += "max Lvl \n";
            }
        }
        else if (index == 2)
        {
            if (targGoldForLvl.Length > (bouldingLevel[index]))
            {
                message += "cena za next Lvl: " + targGoldForLvl[bouldingLevel[index]] + "\n";
            }
            else
            {
                message += "max Lvl \n";
            }
        }
        else if (index == 3)
        {
            if (placGoldForLvl.Length > (bouldingLevel[index]))
            {
                message += "cena za next Lvl: " + placGoldForLvl[bouldingLevel[index]] + "\n";
            }
            else
            {
                message += "max Lvl \n";
            }
        }
        else if (index == 4)
        {
            if (koszaryGoldForLvl.Length > (bouldingLevel[index]))
            {
                message += "cena za next Lvl: " + koszaryGoldForLvl[bouldingLevel[index]] + "\n";
            }
            else
            {
                message += "max Lvl \n";
            }
        }
        else if (index == 5)
        {
            if (fortyfikacjaGoldForLvl.Length > (bouldingLevel[index]))
            {
                message += "cena za next Lvl: " + fortyfikacjaGoldForLvl[bouldingLevel[index]] + "\n";
            }
            else
            {
                message += "max Lvl \n";
            }
        }
        message += "aktualny bonus: " + bouldingbonus[index];
        message += "\npo ulepszeniu: " + (bouldingbonus[index] + bouldingAddBonus[index]);
        return message;
    }

    public void setParentJednostki(GameObject obj)
    {
        obj.transform.parent = destroyJednostki.transform;
    }

    public void setParentPrefabCavas(GameObject obj)
    {
        obj.transform.parent = menuFunctions.prefabCanvas.transform;
    }

    public CastleEntry[] getCastles()
    {
        return castles;
    }

    public string getBouldingName(int index)
    {
        return bouldingNames[index];
    }

    public string getJednostkaName(int index)
    {
        return jednostkaNemes[index];
    }

    public int[] getBouldingProgres()
    {
        return bouldingProgres;
    }

    public int[] getBouldingLvl()
    {
        return bouldingLevel;
    }

    public bool itsAGameOver()
    {
        int yourCastle = 0;
        for (int i =0; i < castles.Length; i++)
        {
            if (castles[i].wrogosc == 0)
            {
                yourCastle += 1;
            }
        }
        if (yourCastle == 0 || tura>19)
        {
            textInfoShow.showGameOverWindow(textInfoShow.gameOverLessCastle, textInfoShow.gameOverTitle);
            return true;
        }
        if (warningForNoMoney > 2)
        {
            textInfoShow.showGameOverWindow(textInfoShow.gameOverMoney, textInfoShow.gameOverTitle);
            return true;
        }
        if (yourCastle == castles.Length)
        {
            textInfoShow.showGameOverWindow(textInfoShow.win, textInfoShow.winTitle);
            return true;
        }
        return false;
    }

    public void makeSojuszWith(int who)
    {
        if (tura != rundaSojusz)
        {
            Debug.Log(who);
            curentSojusz = who;
            for (int i = 0; i < castles.Length; i++)
            {
                if (castles[i].castleName.Equals(nazwyZamkow[who]))
                {
                    castles[i].wrogosc = who + 2;
                }
                else
                {
                    if (castles[i].wrogosc != 0)
                    {
                        castles[i].wrogosc = 1;
                    }
                }
            }

            for (int i = 0; i < army.Count; i++)
            {
                Soldier soldier = (Soldier)army[i];
                if (soldier.GetComponent<ObjectTransform>().WojskaName.Equals(nazwyZamkow[who]))
                {
                    soldier.wrogosc = who + 2;
                }
                else
                {
                    if (soldier.wrogosc != 0)
                    {
                        soldier.wrogosc = 1;
                    }
                }
            }

            menuFunctions.CloseSojuszCanvas();
            if (curentSojusz != 3)
            {
                rundaSojusz = tura;
            }
        }
        else
        {
            menuFunctions.CloseSojuszCanvas();
            textInfoShow.showMassageWindow(textInfoShow.sojusz+nazwyZamkow[curentSojusz], textInfoShow.sojuszTitle);
        }
    }

    public void nextRound()
    {
        if (!itsAGameOver())
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
            menuFunctions.displayAnulacjaBouldings();
            if (tura == 1)
            {
                
            }
        }
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
                    textInfoShow.showMassageWindow(textInfoShow.noMoney, textInfoShow.noMoneyTitle);
                }
            }
            else
            {
                textInfoShow.showMassageWindow(textInfoShow.maxBouldingLvl, textInfoShow.maxBouldingLvlTitle);
            }
        }
        else
        {
            textInfoShow.showMassageWindow(textInfoShow.arleadyBuilding, textInfoShow.arleadyBouldingTitle);
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
                }
                else
                {
                    textInfoShow.showMassageWindow(textInfoShow.noMoney, textInfoShow.noMoneyTitle);
                }
            }
            else
            {
                textInfoShow.showMassageWindow(textInfoShow.maxBouldingLvl, textInfoShow.maxBouldingLvlTitle);
            }
        }
        else
        {
            textInfoShow.showMassageWindow(textInfoShow.arleadyBuilding, textInfoShow.arleadyBouldingTitle);
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
                    textInfoShow.showMassageWindow(textInfoShow.noMoney, textInfoShow.noMoneyTitle);
                }
            }
            else
            {
                textInfoShow.showMassageWindow(textInfoShow.maxBouldingLvl, textInfoShow.maxBouldingLvlTitle);
            }
        }
        else
        {
            textInfoShow.showMassageWindow(textInfoShow.arleadyBuilding, textInfoShow.arleadyBouldingTitle);
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
                    textInfoShow.showMassageWindow(textInfoShow.noMoney, textInfoShow.noMoneyTitle);
                }
            }
            else
            {
                textInfoShow.showMassageWindow(textInfoShow.maxBouldingLvl, textInfoShow.maxBouldingLvlTitle);
            }
        }
        else
        {
            textInfoShow.showMassageWindow(textInfoShow.arleadyBuilding, textInfoShow.arleadyBouldingTitle);
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
                    textInfoShow.showMassageWindow(textInfoShow.noMoney, textInfoShow.noMoneyTitle);
                }
            }
            else
            {
                textInfoShow.showMassageWindow(textInfoShow.maxBouldingLvl, textInfoShow.maxBouldingLvlTitle);
            }
        }
        else
        {
            textInfoShow.showMassageWindow(textInfoShow.arleadyBuilding, textInfoShow.arleadyBouldingTitle);
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
                    textInfoShow.showMassageWindow(textInfoShow.noMoney, textInfoShow.noMoneyTitle);
                }
            }
            else
            {
                textInfoShow.showMassageWindow(textInfoShow.maxBouldingLvl, textInfoShow.maxBouldingLvlTitle);
            }
        }
        else
        {
            textInfoShow.showMassageWindow(textInfoShow.arleadyBuilding, textInfoShow.arleadyBouldingTitle);
        }
    }

    public void addBouldingBonus()
    {
        int add = 0;
        string massage = textInfoShow.bonus;
        for (int i=0; i < bouldingProgres.Length; i++)
        {
            if (bouldingProgres[i] != 0)
            {
                bouldingProgres[i] += 1;
                if (bouldingProgres[i] == 0)
                {
                    bouldingbonus[i] += bouldingAddBonus[i];
                    bouldingLevel[i] += 1;
                    menuFunctions.displayBouldingLvl();
                    massage += "\n dla " + bouldingNames[i] + " do " + bouldingbonus[i];
                    add += 1;
                }
            }
        }
        if (add > 0)
        {
            textInfoShow.showMassageWindow(massage, textInfoShow.bonusTitle);
        }
    }

    public void deleteBoulding(int boulding)
    {
        if (bouldingProgres[boulding] != 0)
        {
            bouldingProgres[boulding] = 0;
            int zwrot = 0;
            switch (boulding)
            {
                case 0:
                    zwrot = drogiGoldForLvl[bouldingLevel[boulding]];
                    break;
                case 1:
                    zwrot = zamekGoldForLvl[bouldingLevel[boulding]];
                    break;
                case 2:
                    zwrot = targGoldForLvl[bouldingLevel[boulding]];
                    break;
                case 3:
                    zwrot = placGoldForLvl[bouldingLevel[boulding]];
                    break;
                case 4:
                    zwrot = koszaryGoldForLvl[bouldingLevel[boulding]];
                    break;
                case 5:
                    zwrot = fortyfikacjaGoldForLvl[bouldingLevel[boulding]];
                    break;
            }
            gold += zwrot;
            menuFunctions.GoldButton();
        }
    }

    public void rekrutujJednostke(CastleEntry castle, int typeOfWarior)
    {
        if (gold >= armyGoldForEach[typeOfWarior])
        {
            RekrutacjaWojska rekrutacjaWojska = new RekrutacjaWojska();
            rekrutacjaWojska.castle = castle;
            rekrutacjaWojska.typeOfWarior = typeOfWarior;
            rekrutacjaWojska.rouldLeft = -rekrutacjaTime[typeOfWarior];
            rekrutacjaQueue.Add(rekrutacjaWojska);
            gold -= armyGoldForEach[typeOfWarior];
            menuFunctions.GoldButton();
        }
        else
        {
            textInfoShow.showMassageWindow(textInfoShow.noMoney, textInfoShow.noMoneyTitle);
        }
    }

    public void addRekrutowaneJednostki()
    {
        RekrutacjaWojska rekrutacjaWojska;
        for (int i = 0; i < rekrutacjaQueue.Count; i++)
        {
            rekrutacjaWojska = (RekrutacjaWojska) rekrutacjaQueue[i];
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
            rekrutacjaWojska = (RekrutacjaWojska)rekrutacjaQueue[i];
            if (rekrutacjaWojska.rouldLeft == 0)
            {
                rekrutacjaQueue.RemoveAt(i);
                i = -1;
            }

        }

        menuFunctions.displayAnulacjaWojska();
        menuFunctions.displayMilitaryInCastle();

    }

    public void deleteRekrutowaneJednostki(CastleEntry castle, int typeOfWarior)
    {
        RekrutacjaWojska toRemove = null;
        for (int i = 0; i < rekrutacjaQueue.Count; i++)
        {
            RekrutacjaWojska rekrutacjaWojska = (RekrutacjaWojska)rekrutacjaQueue[i];
            if (rekrutacjaWojska.castle.Equals(castle))
            {
                if (rekrutacjaWojska.typeOfWarior == typeOfWarior)
                {
                    if (toRemove != null)
                    {
                        if (toRemove.rouldLeft > rekrutacjaWojska.rouldLeft)
                        {
                            toRemove = rekrutacjaWojska;
                        }
                    }
                    else
                    {
                        toRemove = rekrutacjaWojska;
                    }
                }
            }
        }
        if (toRemove != null)
        {
            rekrutacjaQueue.Remove(toRemove);
            gold += armyGoldForEach[toRemove.typeOfWarior];
            menuFunctions.GoldButton();
            Debug.Log("anulowano rekrutaacje");
        }
    }

    public int[] getMilitaryInRekrutacja(CastleEntry castle)
    {
        int[] militaryInCastleRecruit = new int[] { 0, 0, 0, 0, 0 };
        for (int i = 0; i < rekrutacjaQueue.Count; i++)
        {
            RekrutacjaWojska rekrutacjaWojska = (RekrutacjaWojska)rekrutacjaQueue[i];
            if (rekrutacjaWojska.castle.Equals(castle))
            {
                militaryInCastleRecruit[rekrutacjaWojska.typeOfWarior] += 1;
            }
        }
        return militaryInCastleRecruit;

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
        if (payment > gold)
        {
            if (warningForNoMoney < 3)
            {
                warningForNoMoney += 1;
                textInfoShow.showMassageWindow(textInfoShow.waste, textInfoShow.wasteTitle);
            }
        }
        gold -= payment;
    }

    public void wyplata()
    {
        gold += bouldingbonus[2];
    }
}
