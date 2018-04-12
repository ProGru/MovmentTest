using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuFunctions : MonoBehaviour {

    public Canvas menuCanvas;
    public Canvas addSoldierCanvas;
    public Canvas inventoryCanvas;
    public GameObject army;
    GameObject currentObject;
    Vector3 detinationPosition;
    RaycastHit hit;
    bool addMilitaryStatus = false;

    private void Start()
    {
        inventoryCanvas = inventoryCanvas.GetComponent<Canvas>();
        menuCanvas = menuCanvas.GetComponent<Canvas>();
        Time.timeScale = 0;
        addSoldierCanvas = addSoldierCanvas.GetComponent<Canvas>();
        menuCanvas.enabled = true;
        addSoldierCanvas.enabled = false;
        inventoryCanvas.enabled = false;
    }

    public void ButtonStart()
    {
        inventoryCanvas.enabled = false;
        menuCanvas.enabled = false;
        addSoldierCanvas.enabled = false;
        Time.timeScale = 1;
    }

    public void ChangeAddMilitaryStatus()
    {
        currentObject = Instantiate(army, new Vector3(0,0,0), new Quaternion(0, transform.rotation.y, 0, transform.rotation.w)) as GameObject;

        addMilitaryStatus = true;
    }
    private void Update()
    {
        this.AddItemInMousePressPosition();
    }

    private void AddItemInMousePressPosition() {
        if (addMilitaryStatus)
        {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000))
                {
                    if (hit.transform.name == transform.name)
                    {
                        Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, 1000);
                    }
                    detinationPosition = hit.point + new Vector3(0, 1, 0);
                }
            currentObject.transform.position = detinationPosition;
                //Instantiate(army, detinationPosition, new Quaternion(0, transform.rotation.y, 0, transform.rotation.w));
            if (Input.GetMouseButtonDown(0))
            {
                addMilitaryStatus = false;
                addSoldierCanvas.enabled = false;
                inventoryCanvas.enabled = false;
            }
        }
    }

    public void ReloadCanvas()
    {
        addSoldierCanvas.enabled = false;
        inventoryCanvas.enabled = true;
        menuCanvas.enabled = false;
    }
}
