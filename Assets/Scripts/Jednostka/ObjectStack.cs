using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStack : MonoBehaviour {

    public int counter = 0;
    private MainManager mainManager;

    private void Start()
    {
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
                    if (other.GetComponent<ObjectTransform>().typeOfWarior == this.GetComponent<ObjectTransform>().typeOfWarior)
                    {
                        counter += 1;
                        if (other.GetComponent<ObjectTransform>().wrogosc == this.GetComponent<ObjectTransform>().wrogosc)
                        {
                            this.GetComponent<ObjectTransform>().quantityMilitary +=
                            other.GetComponent<ObjectTransform>().quantityMilitary;
                            Destroy(other.gameObject);
                        }
                        else if (other.GetComponent<ObjectTransform>().wrogosc > 1 || this.GetComponent<ObjectTransform>().wrogosc > 1)
                        {
                            //this.GetComponent<ObjectTransform>().quantityMilitary +=
                            //                        other.GetComponent<ObjectTransform>().quantityMilitary;
                            //Destroy(other.gameObject);
                        }
                        else
                        {
                            this.GetComponent<ObjectTransform>().quantityMilitary -=
                            other.GetComponent<ObjectTransform>().quantityMilitary;
                            Destroy(other.gameObject);
                        }
                    }
                }
            }
        }
    }

}
