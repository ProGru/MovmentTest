using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStack : MonoBehaviour {

    public int counter = 0;
    private MainManager mainManager;
    private AttackMaker attackMaker;

    private void Start()
    { 
        this.gameObject.AddComponent<AttackMaker>();
        attackMaker = GetComponent<AttackMaker>();
        mainManager = FindObjectOfType<MainManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        ObjectStack objectStack = other.GetComponent<ObjectStack>();
        if ( objectStack != null && 
            (other.GetComponent<ObjectTransform>().canEntry && this.GetComponent<ObjectTransform>().canEntry))
        {
            if (counter == 0)
            {
                if (other.GetComponent<ObjectStack>().counter == 0)
                {
                        counter += 1;
                    if (other.GetComponent<Soldier>().wrogosc == this.GetComponent<Soldier>().wrogosc)
                    {
                        attackMaker.makeStack(this.gameObject, other.gameObject);
                    }
                    else if (other.GetComponent<Soldier>().wrogosc > 1 || this.GetComponent<Soldier>().wrogosc > 1)
                    {
                        attackMaker.makeSojusz(this.gameObject, other.gameObject);
                    }
                    else
                    {
                        attackMaker.makeAttack(this.gameObject, other.gameObject);
                    }
                }
            }
        }
    }

}
