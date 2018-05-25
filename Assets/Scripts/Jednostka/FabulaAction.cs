using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabulaAction : MonoBehaviour {

    public string scriptName;
    public GameObject warior;
    public GameObject[] attackTargetPerRound;
    public GameObject[] movePerRound;
    public string[] recruitPerRound;

    private void Start()
    {
        for (int i = 0; i < movePerRound.Length; i++)
        {
            if (movePerRound[i] != null)
            {
                Debug.Log("hola");
                warior.gameObject.SetActive(true);
                warior.gameObject.GetComponent<ObjectTransform>().setAttackTarget(movePerRound[i].transform.position);

            }
            else
            {
            }
        }
    }

}
