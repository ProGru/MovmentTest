using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMaker : MonoBehaviour {

    public void makeAttack(GameObject attackerObject, GameObject defenderObject)
    {
        Millitary attacker = attackerObject.GetComponent<Millitary>();
        Soldier attackerSoldier = attackerObject.GetComponent<Soldier>();
        Millitary defender = defenderObject.GetComponent<Millitary>();
        Soldier defenderSoldier = defenderObject.GetComponent<Soldier>();

        if (attacker.multi == false && defender.multi == false)
        {
            if (attackerSoldier.typeOfWarior == defenderSoldier.typeOfWarior)
            {
                attacker.quantityMilitarySolo -=
                defender.quantityMilitarySolo;
                Destroy(defenderObject);
            }
            else
            {
                attacker.multi = true;
                attacker.quantityMilitaryMulti[attackerSoldier.typeOfWarior] = attacker.quantityMilitarySolo;
                attacker.quantityMilitaryMulti[defenderSoldier.typeOfWarior] -= defender.quantityMilitarySolo;
                Destroy(defenderObject);
            }
        }
        else if (attacker.multi == true && defender.multi == false)
        {
            attacker.quantityMilitaryMulti[defenderSoldier.typeOfWarior] -= defender.quantityMilitarySolo;
            Destroy(defenderObject);
        }
        else if (attacker.multi == false && defender.multi == true)
        {
            defender.quantityMilitaryMulti[attackerSoldier.typeOfWarior] -= attacker.quantityMilitarySolo;
            Destroy(attackerObject);
        }
        else
        {
            for (int i = 0; i < defender.quantityMilitaryMulti.Length; i++)
            {
                attacker.quantityMilitaryMulti[i] -= defender.quantityMilitaryMulti[i];
            }
            Destroy(defenderObject);
        }
    }

    public void makeSojusz(GameObject attackerObject, GameObject defenderObject)
    {
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
                GetComponent<Renderer>().material.color= Color.black;
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

    public void makeCastleAttack(GameObject attackerObject,GameObject castleObject)
    {
        Millitary attacker = attackerObject.GetComponent<Millitary>();
        Soldier attackerSoldier = attackerObject.GetComponent<Soldier>();
        CastleEntry castle = castleObject.GetComponent<CastleEntry>();
        if (attackerSoldier.wrogosc == castle.wrogosc)
        {
            if (attacker.multi == false)
            {
                castle.quantityMilitary[attackerSoldier.typeOfWarior] += attacker.quantityMilitarySolo;
                Destroy(attackerObject);
            }
            else
            {
                for (int i = 0; i < castle.quantityMilitary.Length; i++)
                {
                    castle.quantityMilitary[i] += attacker.quantityMilitaryMulti[i];
                }
                Destroy(attackerObject);
            }
        }
        else if (attackerSoldier.wrogosc > 1)
        {
            
        }
        else
        {
            if (attacker.multi == false)
            {
                castle.quantityMilitary[attackerSoldier.typeOfWarior] -= attacker.quantityMilitarySolo;
                Destroy(attackerObject);
            }
            else
            {
                for (int i = 0; i < castle.quantityMilitary.Length; i++)
                {
                    castle.quantityMilitary[i] -= attacker.quantityMilitaryMulti[i];
                }
                Destroy(attackerObject);
            }
        }
    }
}


