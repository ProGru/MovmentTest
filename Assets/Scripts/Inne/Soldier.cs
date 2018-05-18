using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour {

    public float speed = 2;
    public float maxDistancePerRound = 100;
    // 0-nasze, 1-wrog, >1 -sojusz 
    public int wrogosc = 0;
    //0-lucznik, 1-konnica, 2-piechota, 3-woj, 4-szpieg
    public int typeOfWarior = 0;

    public Soldier(int typeOfWarior)
    {
        this.typeOfWarior = typeOfWarior;
        if (typeOfWarior == 0)
        {
            speed = 2;
            maxDistancePerRound = 100;
        }
    }
    
}
