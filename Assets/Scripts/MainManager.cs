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
    public Canvas canvasMenu;
    private int tura = 0;
    private CastleEntry[] castles;
    private ObjectTransform[] wojska; 

    public void Start()
    {
        canvasMenu = GetComponent<Canvas>();
        castles = Component.FindObjectsOfType<CastleEntry>();
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

}
