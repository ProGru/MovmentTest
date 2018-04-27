using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportacja : MonoBehaviour {


    public Vector3 F1Button = new Vector3(0, 0, 0);
    public float F1TimeBtwTeleportation = 10;
    float lastTeleportationTime = -1;


	void Update () {

		if (Input.GetKey(KeyCode.F1))
        {
            if (lastTeleportationTime < Time.time - F1TimeBtwTeleportation)
            {
                lastTeleportationTime = Time.time;
                transform.position = F1Button;
            }
        }
	}
}
