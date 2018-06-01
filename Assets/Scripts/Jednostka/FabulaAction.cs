using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabulaAction : MonoBehaviour
{
    private MainManager mainManager;
    public string scriptName;
    public GameObject warior;
    public GameObject[] attackTargetPerRound;
    public Vector3[] movePerRound;
    public string[] recruitPerRound;
    int round_make = -1;

    private void Start()
    {
        mainManager = FindObjectOfType<MainManager>();
        //warior = warior.GetComponent<GameObject>();
    }

    private void Update()
    {
        if (round_make!=mainManager.getTura())
        {
            if (attackTargetPerRound.Length > mainManager.getTura())
            {
                if (attackTargetPerRound[mainManager.getTura()] != null)
                {
                    Debug.Log("i make attack");
                    warior.SetActive(true);
                    warior.GetComponent<ObjectTransform>().setAttackTarget(attackTargetPerRound[mainManager.getTura()].transform.position);
                }
                else if (movePerRound[mainManager.getTura()] != new Vector3())
                {
                    Debug.Log("i make move");
                    warior.SetActive(true);
                    warior.GetComponent<ObjectTransform>().setAttackTarget(movePerRound[mainManager.getTura()]);
                }
            }
            round_make = mainManager.getTura();

        }
    }

}
