using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutItemsOnScrean : MonoBehaviour {

    public GameObject figure;
    Vector3 destinationPosition;
    RaycastHit hit;

    void Update () {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000))
        {
            if (hit.transform.name == transform.name)
            {
                Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, 1000);
            }
            destinationPosition = hit.point + new Vector3(0,1,0);

        }
		if (Input.GetKeyDown(KeyCode.F2))
        {           
            Instantiate(figure, destinationPosition,new Quaternion( 0, transform.rotation.y, 0, transform.rotation.w));
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            Platrofm[] platforms = Component.FindObjectsOfType<Platrofm>();

            if (platforms.Length > 0)
            {
                Platrofm platform = platforms[0];
                Destroy(platform.gameObject);
            }
        }
	}
}
