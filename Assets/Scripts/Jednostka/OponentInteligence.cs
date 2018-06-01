using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OponentInteligence : MonoBehaviour {
    public int mode = 0;
    private Vector3 target;
    private Vector3 startPosition;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        activeMode();
	}

    public void activeMode()
    {
        if (mode == 0)
        {
            lookingForOponentMode();
        }else if(mode == 1)
        {
            attackMode();
        }else if (mode == 2)
        {
            backMode();
        }
    }

    private void backMode()
    {

        attack(startPosition);
        mode = 0;
        //Debug.Log("Back !!");

    }

    public void stayMode()
    {
        if (target== GetComponent<ObjectTransform>().getAttackTarget())
        {
            mode = 0;
            //Debug.Log("mode 0 !");
        }
  
    }

    public void attackMode()
    {
        bool isAttack = false;
        Soldier[] soldiers = FindObjectsOfType<Soldier>();
        for (int i = 0; i < soldiers.Length; i++)
        {
            if (soldiers[i].wrogosc == 0 && soldiers[i].GetComponent<Millitary>().multi==false && soldiers[i].typeOfWarior == 4)
            {
                if (disctance(this.transform.position, soldiers[i].gameObject) < 100)
                {
                    target = soldiers[i].transform.position;
                    attack(target);
                    isAttack = true;
                    //Debug.Log("this is sparta, attack!!!");
                }
            }
        }
        if (!isAttack)
        {
            mode = 2;
        }
    }

    public void lookingForOponentMode()
    {
        Soldier[] soldiers = FindObjectsOfType<Soldier>();
        for (int i = 0; i < soldiers.Length; i++)
        {
            if (soldiers[i].wrogosc == 0)
            {
                if (disctance(this.transform.position, soldiers[i].gameObject) < 100 && soldiers[i].typeOfWarior == 4)
                {
                    mode = 1;
                    startPosition = this.transform.position;
                    //Debug.Log("this is sparta");
                }
            }
        }
    }

    public void attack(Vector3 position)
    {

        GetComponent<ObjectTransform>().setAttackTarget(position);
    }

    float disctance(Vector3 firstPos, GameObject second)
    {
        Vector3 secondPos = second.transform.position;
        float dx = firstPos.x - secondPos.x, dy = firstPos.y - secondPos.y, dz = firstPos.z - secondPos.z;
        return Mathf.Sqrt(dx * dx + dy * dy + dz * dz);
    }
}
