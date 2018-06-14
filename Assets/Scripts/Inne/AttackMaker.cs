using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMaker : MonoBehaviour
{
    private TextInfoShow textInfoShow;
    private MainManager mainManager;
    private int[] soldiersHP = new int[] { 110, 120, 130, 150, 50 };
    private int[] soldierDEF = new int[] { 50, 70, 80, 90, 20 };
    private int[] soldierATACK = new int[] { 110, 100, 95, 130, 55 };

    private bool winAttacker = false;


    private void Start()
    {
        textInfoShow = FindObjectOfType<TextInfoShow>();
        if (this.GetComponent<MeshRenderer>() == null)
        {
            this.gameObject.AddComponent<MeshRenderer>();
        }
        mainManager = FindObjectOfType<MainManager>();
    }

    public void makeAttack(GameObject attackerObject, GameObject defenderObject)
    {
        Millitary attacker = attackerObject.GetComponent<Millitary>();
        Soldier attackerSoldier = attackerObject.GetComponent<Soldier>();
        Millitary defender = defenderObject.GetComponent<Millitary>();
        Soldier defenderSoldier = defenderObject.GetComponent<Soldier>();

        int attackerType = attackerSoldier.wrogosc;
        int defenderType = defenderSoldier.wrogosc;

        ArrayList winner = atackSoldiers(attacker, defender, attackerType, defenderType);

        if (winAttacker)
        {
            if (attacker.multi)
            {
                attacker.quantityMilitaryMulti = ReparseToMultiSoldierQuantityMilitary(winner);
            }
            else
            {
                attacker.quantityMilitarySolo = ReparseToSoloSoldierQuantityMilitary(winner);
            }
            if (defenderObject.GetComponent<ObjectTransform>().goldBonus > 0)
            {
                mainManager.addGold(defenderObject.GetComponent<ObjectTransform>().goldBonus);
                textInfoShow.showMassageWindow(textInfoShow.defeatEnemy + "\n Gold: " 
                    + defenderObject.GetComponent<ObjectTransform>().goldBonus, textInfoShow.defeatEnemyTitle);
            }
            Destroy(defenderObject);
           
        }
        else
        {
            if (defender.multi)
            {
                defender.quantityMilitaryMulti = ReparseToMultiSoldierQuantityMilitary(winner);
            }
            else
            {
                defender.quantityMilitarySolo = ReparseToSoloSoldierQuantityMilitary(winner);
            }
            if (attackerObject.GetComponent<ObjectTransform>().goldBonus > 0)
            {
                mainManager.addGold(attackerObject.GetComponent<ObjectTransform>().goldBonus);
                textInfoShow.showMassageWindow(textInfoShow.defeatEnemy + "\n Gold: "
                    + attackerObject.GetComponent<ObjectTransform>().goldBonus, textInfoShow.defeatEnemyTitle);
            }
            Destroy(attackerObject);
        }

    }

    public void makeSojusz(GameObject attackerObject, GameObject defenderObject)
    {
        textInfoShow.showMassageWindow(textInfoShow.canNotAttackFriend, textInfoShow.canNotAttackFriendTitle);
        /*
        Millitary attacker = attackerObject.GetComponent<Millitary>();
        Soldier attackerSoldier = attackerObject.GetComponent<Soldier>();
        Millitary defender = defenderObject.GetComponent<Millitary>();
        Soldier defenderSoldier = defenderObject.GetComponent<Soldier>();

        if (attacker.multi == false && defender.multi == false)
        {
            if (attackerSoldier.typeOfWarior == defenderSoldier.typeOfWarior)
            {
                attacker.quantityMilitarySolo +=
                defender.quantityMilitarySolo;
                Destroy(defenderObject);
            }
            else
            {
                attacker.multi = true;
                attacker.quantityMilitaryMulti[attackerSoldier.typeOfWarior] = attacker.quantityMilitarySolo;
                attacker.quantityMilitaryMulti[defenderSoldier.typeOfWarior] = defender.quantityMilitarySolo;
                Destroy(defenderObject);
            }
        }
        else if (attacker.multi == true && defender.multi == false)
        {
            attacker.quantityMilitaryMulti[defenderSoldier.typeOfWarior] += defender.quantityMilitarySolo;
            Destroy(defenderObject);
        }
        else if (attacker.multi == false && defender.multi == true)
        {
            defender.quantityMilitaryMulti[attackerSoldier.typeOfWarior] += attacker.quantityMilitarySolo;
            Destroy(attackerObject);
        }
        else
        {
            for (int i = 0; i < defender.quantityMilitaryMulti.Length; i++)
            {
                attacker.quantityMilitaryMulti[i] += defender.quantityMilitaryMulti[i];
            }
            Destroy(defenderObject);
        }*/
    }

    public void makeStack(GameObject attackerObject, GameObject defenderObject)
    {
        Millitary attacker = attackerObject.GetComponent<Millitary>();
        Soldier attackerSoldier = attackerObject.GetComponent<Soldier>();
        Millitary defender = defenderObject.GetComponent<Millitary>();
        Soldier defenderSoldier = defenderObject.GetComponent<Soldier>();

        if (attacker.multi == false && defender.multi == false)
        {
            if (attackerSoldier.typeOfWarior == defenderSoldier.typeOfWarior)
            {
                attacker.quantityMilitarySolo +=
                defender.quantityMilitarySolo;
                Destroy(defenderObject);
            }
            else
            {
                attacker.multi = true;
                GetComponent<Renderer>().material.color = Color.black;
                attacker.quantityMilitaryMulti[attackerSoldier.typeOfWarior] = attacker.quantityMilitarySolo;
                attacker.quantityMilitaryMulti[defenderSoldier.typeOfWarior] = defender.quantityMilitarySolo;
                Destroy(defenderObject);
            }
        }
        else if (attacker.multi == true && defender.multi == false)
        {
            attacker.quantityMilitaryMulti[defenderSoldier.typeOfWarior] += defender.quantityMilitarySolo;
            Destroy(defenderObject);
        }
        else if (attacker.multi == false && defender.multi == true)
        {
            defender.quantityMilitaryMulti[attackerSoldier.typeOfWarior] += attacker.quantityMilitarySolo;
            Destroy(attackerObject);
        }
        else
        {
            for (int i = 0; i < defender.quantityMilitaryMulti.Length; i++)
            {
                attacker.quantityMilitaryMulti[i] += defender.quantityMilitaryMulti[i];
            }
            Destroy(defenderObject);
        }
    }

    public void makeCastleAttack(GameObject attackerObject, GameObject castleObject)
    {
        Millitary attacker = attackerObject.GetComponent<Millitary>();
        Soldier attackerSoldier = attackerObject.GetComponent<Soldier>();
        CastleEntry castle = castleObject.GetComponent<CastleEntry>();

        if (castle.wrogosc == attackerSoldier.wrogosc)
        {
            if (attacker.multi)
            {
                for (int i = 0; i < attacker.quantityMilitaryMulti.Length; i++)
                {
                    castle.quantityMilitary[i] += attacker.quantityMilitaryMulti[i];
                }
            }
            else
            {
                castle.quantityMilitary[attackerSoldier.typeOfWarior] += attacker.quantityMilitarySolo;
            }
            Destroy(attackerObject);
        }else if (attackerSoldier.wrogosc==0 && castle.wrogosc > 1)
        {
            makeSojusz(attackerObject,attackerObject);
        }
        else
        {

            int attackerType = attackerSoldier.wrogosc;
            int castleType = castle.wrogosc;

            ArrayList winer = atackCastle(attacker, castle, attackerType, castleType);
            if (winAttacker)
            {
                castle.quantityMilitary = ReparseToMultiSoldierQuantityMilitary(winer);
                castle.wrogosc = attackerSoldier.wrogosc;
                if (attackerSoldier.wrogosc == 0)
                {
                    castle.yours = true;
                    castle.castleName = attackerSoldier.GetComponent<ObjectTransform>().WojskaName;
                    textInfoShow.showMassageWindow(textInfoShow.captureCastle +
                        "\n Za przejecie zamku otrzymujesz: "+castle.goldBonus, textInfoShow.captureCastleTitle);
                    mainManager.addGold( castle.goldBonus);
                    
                }
                else
                {
                    castle.yours = false;
                    castle.castleName = attackerSoldier.GetComponent<ObjectTransform>().WojskaName;
                    textInfoShow.showMassageWindow(textInfoShow.lostCastle, textInfoShow.lostCastleTitle);
                }
                castle.setCastleColor(attackerSoldier.wrogosc);
            }
            else
            {
                castle.quantityMilitary = ReparseToMultiSoldierQuantityMilitary(winer);
            }
            Destroy(attackerObject);
        }
    }

    public ArrayList MultiSoldierParser(int[] millitary, int type)
    {
        ArrayList single = new ArrayList();
        for (int i = 0; i < millitary.Length; i++)
        {
            for (int j = 0; j < millitary[i]; j++)
            {
                if (type == 0)
                {
                    single.Add(new SingleSoldier(soldiersHP[i], i, soldierDEF[i], soldierATACK[i] + mainManager.bouldingbonus[4]));
                }
                else
                {
                    single.Add(new SingleSoldier(soldiersHP[i], i, soldierDEF[i], soldierATACK[i]));

                }
            }
        }
        return single;
    }
    public ArrayList SingleSoldierParser(int quantityMilitarySolo, int typeOfWarior, int type)
    {
        ArrayList single = new ArrayList();
        for (int i = 0; i < quantityMilitarySolo; i++)
        {
            if (type == 0)
            {
                single.Add(new SingleSoldier(soldiersHP[typeOfWarior], typeOfWarior, soldierDEF[typeOfWarior],
                    soldierATACK[typeOfWarior] + mainManager.bouldingbonus[4]));
            }
            else
            {
                single.Add(new SingleSoldier(soldiersHP[typeOfWarior], typeOfWarior, soldierDEF[typeOfWarior],
                    soldierATACK[typeOfWarior]));
            }
        }
        return single;
    }

    public int[] ReparseToMultiSoldierQuantityMilitary(ArrayList array)
    {
        int[] wynik = new int[] { 0, 0, 0, 0, 0 };
        for (int i = 0; i < array.Count; i++)
        {
            SingleSoldier soldier = (SingleSoldier)array[i];
            wynik[soldier.typeofWarior] += 1;
        }

        return wynik;
    }

    public int ReparseToSoloSoldierQuantityMilitary(ArrayList array)
    {
        return array.Count;
    }

    public ArrayList atackSoldiers(Millitary attacker, Millitary defender, int attackerType, int defenderType)
    {
        ArrayList attackerArray;
        ArrayList defenderArray;
        ArrayList attackerArrayDestroy;
        ArrayList defenderArrayDestroy;


        if (attacker.multi == true)
        {
            attackerArray = MultiSoldierParser(attacker.quantityMilitaryMulti, attackerType);
            attackerArrayDestroy = MultiSoldierParser(attacker.quantityMilitaryMulti, attackerType);
        }
        else
        {
            attackerArray = SingleSoldierParser(attacker.quantityMilitarySolo, attacker.GetComponent<Soldier>().typeOfWarior, attackerType);
            attackerArrayDestroy = SingleSoldierParser(attacker.quantityMilitarySolo, attacker.GetComponent<Soldier>().typeOfWarior, attackerType);

        }

        if (defender.multi == true)
        {
            defenderArray = MultiSoldierParser(defender.quantityMilitaryMulti, defenderType);
            defenderArrayDestroy = MultiSoldierParser(attacker.quantityMilitaryMulti, defenderType);
        }
        else
        {
            defenderArray = SingleSoldierParser(defender.quantityMilitarySolo, defender.GetComponent<Soldier>().typeOfWarior, defenderType);
            defenderArrayDestroy = SingleSoldierParser(defender.quantityMilitarySolo, defender.GetComponent<Soldier>().typeOfWarior, defenderType);

        }

        int ile = (attackerArray.Count + defenderArray.Count) / 5;
        for (int j = 0; j < ile; j++)
        {
            for (int i = 0; i < attackerArray.Count; i++)
            {

                if (defenderArrayDestroy.Count > 1)
                {

                    SingleSoldier soldierAtakujacy = (SingleSoldier)attackerArray[i];
                    int wybrano = Random.RandomRange(0, defenderArrayDestroy.Count);
                    SingleSoldier soldierObronca = (SingleSoldier)defenderArrayDestroy[wybrano];

                    int attack = soldierAtakujacy.attack;
                    int obroncaDEF = soldierObronca.defence;
                    soldierObronca.defence -= attack;
                    attack -= obroncaDEF;
                    Debug.Log(attack);

                    if (attack > 0)
                    {
                        soldierObronca.hp -= attack;
                    }

                    if (soldierObronca.hp <= 0)
                    {
                        defenderArrayDestroy.RemoveAt(wybrano);

                    }
                    else
                    {
                        soldierObronca.defence = obroncaDEF;
                    }

                }

            }

            for (int i = 0; i < defenderArray.Count; i++)
            {
                if (attackerArrayDestroy.Count > 1)
                {
                    SingleSoldier soldierAtakujacy = (SingleSoldier)defenderArray[i];
                    int wybrano = Random.RandomRange(0, attackerArrayDestroy.Count);
                    SingleSoldier soldierObronca = (SingleSoldier)attackerArrayDestroy[wybrano];

                    int attack = soldierAtakujacy.attack;
                    int obroncaDEF = soldierObronca.defence;
                    soldierObronca.defence -= attack;
                    attack -= obroncaDEF;
                    if (attack > 0)
                    {
                        soldierObronca.hp -= attack;
                    }

                    if (soldierObronca.hp <= 0)
                    {
                        attackerArrayDestroy.RemoveAt(wybrano);
                    }
                    else
                    {
                        soldierObronca.defence = obroncaDEF;
                    }

                }

            }

            attackerArray = attackerArrayDestroy;
            defenderArray = defenderArrayDestroy;
        }
        if (attackerArrayDestroy.Count > defenderArrayDestroy.Count)
        {
            winAttacker = true;
            return attackerArrayDestroy;
        }
        else
        {
            winAttacker = false;
            return defenderArrayDestroy;
        }

    }

    public ArrayList atackCastle(Millitary attacker, CastleEntry castle, int attackerType, int defenderType)
    {
        ArrayList defenderArray;
        ArrayList attackerArray;
        ArrayList attackerArrayDestroy;
        ArrayList defenderArrayDestroy;


        if (attacker.multi == true)
        {
            attackerArray = MultiSoldierParser(attacker.quantityMilitaryMulti, attackerType);
            attackerArrayDestroy = MultiSoldierParser(attacker.quantityMilitaryMulti, attackerType);
        }
        else
        {
            attackerArray = SingleSoldierParser(attacker.quantityMilitarySolo, attacker.GetComponent<Soldier>().typeOfWarior, attackerType);
            attackerArrayDestroy = SingleSoldierParser(attacker.quantityMilitarySolo, attacker.GetComponent<Soldier>().typeOfWarior, attackerType);

        }

        defenderArray = MultiSoldierParser(castle.quantityMilitary, defenderType);
        defenderArrayDestroy = MultiSoldierParser(castle.quantityMilitary, defenderType);

        //Dodanie bonusu za fortyfikacje
        if (castle.wrogosc == 0)
        {
            for (int i = 0; i < defenderArray.Count; i++)
            {
                SingleSoldier soldier = (SingleSoldier)defenderArray[i];
                SingleSoldier soldier1 = (SingleSoldier)defenderArray[i];
                soldier.attack += mainManager.bouldingbonus[5];
                soldier1.attack += mainManager.bouldingbonus[5];

            }
        }

        int ile = (attackerArray.Count + defenderArray.Count) / 5;
        for (int j = 0; j < ile; j++)
        {
            for (int i = 0; i < attackerArray.Count; i++)
            {

                if (defenderArrayDestroy.Count > 1)
                {

                    SingleSoldier soldierAtakujacy = (SingleSoldier)attackerArray[i];
                    int wybrano = Random.RandomRange(0, defenderArrayDestroy.Count);
                    SingleSoldier soldierObronca = (SingleSoldier)defenderArrayDestroy[wybrano];

                    int attack = soldierAtakujacy.attack;
                    int obroncaDEF = soldierObronca.defence;
                    soldierObronca.defence -= attack;
                    attack -= obroncaDEF;
                    Debug.Log(attack);

                    if (attack > 0)
                    {
                        soldierObronca.hp -= attack;
                    }

                    if (soldierObronca.hp <= 0)
                    {
                        defenderArrayDestroy.RemoveAt(wybrano);

                    }
                    else
                    {
                        soldierObronca.defence = obroncaDEF;
                    }

                }

            }

            for (int i = 0; i < defenderArray.Count; i++)
            {
                if (attackerArrayDestroy.Count > 1)
                {
                    SingleSoldier soldierAtakujacy = (SingleSoldier)defenderArray[i];
                    int wybrano = Random.RandomRange(0, attackerArrayDestroy.Count);
                    SingleSoldier soldierObronca = (SingleSoldier)attackerArrayDestroy[wybrano];

                    int attack = soldierAtakujacy.attack;
                    int obroncaDEF = soldierObronca.defence;
                    soldierObronca.defence -= attack;
                    attack -= obroncaDEF;
                    if (attack > 0)
                    {
                        soldierObronca.hp -= attack;
                    }

                    if (soldierObronca.hp <= 0)
                    {
                        attackerArrayDestroy.RemoveAt(wybrano);
                    }
                    else
                    {
                        soldierObronca.defence = obroncaDEF;
                    }

                }

            }

            attackerArray = attackerArrayDestroy;
            defenderArray = defenderArrayDestroy;
        }
        if (attackerArrayDestroy.Count > defenderArrayDestroy.Count)
        {
            winAttacker = true;
            return attackerArrayDestroy;
        }
        else
        {
            winAttacker = false;
            return defenderArrayDestroy;
        }

    }
}
public class SingleSoldier
{
    public int hp;
    public int typeofWarior;
    public int defence;
    public int attack;

    public SingleSoldier(int hp, int typeOfWarior, int defence, int attack)
    {
        this.hp = hp;
        this.typeofWarior = typeOfWarior;
        this.defence = defence;
        this.attack = attack;
    }
}



