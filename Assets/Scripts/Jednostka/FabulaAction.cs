using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabulaAction : MonoBehaviour
{
    public bool doFabula = false;
    private MainManager mainManager;
    public string scriptName;
    public GameObject warior;
    public GameObject[] attackTargetPerRound;
    public Vector3[] movePerRound;
    public string[] recruitPerRound;
    public CastleEntry[] whithCastleRecruitPerRound;
    int round_make = -1;
    

    private void Start()
    {
        mainManager = FindObjectOfType<MainManager>();
        //warior = warior.GetComponent<GameObject>();
    }

    private void Update()
    {
        if (doFabula)
        {
            if (round_make != mainManager.getTura())
            {
                if (attackTargetPerRound.Length > mainManager.getTura() && warior != null)
                {
                    if (attackTargetPerRound[mainManager.getTura()] != null)
                    {
                        if (warior.GetComponent<Soldier>().wrogosc == 1)
                        {
                            Debug.Log("i make attack");
                            warior.SetActive(true);
                            attackTargetPerRound[mainManager.getTura()].SetActive(true);
                            warior.GetComponent<ObjectTransform>().setAttackTarget(attackTargetPerRound[mainManager.getTura()].transform.position);
                        }
                    }
                    else if (movePerRound.Length> mainManager.getTura())
                    {
                        if (movePerRound[mainManager.getTura()] != new Vector3())
                        {
                            Debug.Log("i make move");
                            warior.SetActive(true);
                            warior.GetComponent<ObjectTransform>().setAttackTarget(movePerRound[mainManager.getTura()]);
                        }
                    }

                }
                if (recruitPerRound.Length>mainManager.getTura() 
                    && whithCastleRecruitPerRound.Length > mainManager.getTura())
                {
                    if (recruitPerRound[mainManager.getTura()] != null 
                        && whithCastleRecruitPerRound[mainManager.getTura()]!=null)
                    {
                        if (whithCastleRecruitPerRound[mainManager.getTura()].wrogosc > 0)
                        {
                            string[] recruit = recruitPerRound[mainManager.getTura()].Split(',');
                            int[] quantitymilitary = whithCastleRecruitPerRound[mainManager.getTura()].quantityMilitary;
                            for (int i = 0; i < quantitymilitary.Length; i++)
                            {
                                quantitymilitary[i] += int.Parse(recruit[i]);
                            }
                            whithCastleRecruitPerRound[mainManager.getTura()].quantityMilitary = quantitymilitary;
                        }
                    }

                }
                    round_make = mainManager.getTura();
            }
        }

    }

}
