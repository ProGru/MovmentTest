using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Funkcje dla Canvasow (UI) oraz (GUI)
/// </summary>
public class MenuFunctions : MonoBehaviour {

    public Canvas economyCanvas;
    public Canvas rekrutacjaCanvas;
    public Canvas nextRound;
    public GameObject infoWindow;
    public GameObject[] army;
    public Text text;
    public Text gold;
    GameObject currentObject = null;
    Vector3 detinationPosition;
    RaycastHit hit;
    bool addMilitaryStatus = false;
    private GameObject castle;
    private MainManager mainManager;
    private int createTypeOf;

    private void Start()
    {
        mainManager = FindObjectOfType<MainManager>();
        economyCanvas = economyCanvas.GetComponent<Canvas>();
        rekrutacjaCanvas = rekrutacjaCanvas.GetComponent<Canvas>();
        nextRound = nextRound.GetComponent<Canvas>();
        text = text.GetComponent<Text>();
        gold = gold.GetComponent<Text>();
        Time.timeScale = 1;
        economyCanvas.enabled = false;
        rekrutacjaCanvas.enabled = false;
        infoWindow.SetActive(false);
        GoldButton();
    }

    /// <summary>
    /// zmiana tury
    /// </summary>
    public void NextRoundButton()
    {
        mainManager.nextRound();
        text.text = mainManager.getTura().ToString();
        GoldButton();
    }

    public void GoldButton()
    {
        gold.text = mainManager.gold.ToString();
    }

    public void ChangeAddMilitaryStatus(int i)
    {
        if (castle.GetComponent<CastleEntry>().quantityMilitary[i] > 0)
        {
            if (castle.GetComponent<CastleEntry>().wrogosc < 1)
            {
                createTypeOf = i;
                addMilitaryStatus = true;
            }
        }
    }

    private void SetObjectProperty(GameObject obj, Vector3 castlePosition)
    {
        obj.GetComponent<ObjectTransform>().canEntry = false;
        obj.GetComponent<ObjectTransform>().yours = castle.GetComponent<CastleEntry>().yours;


        Physics.Raycast(castlePosition + new Vector3(0, 0, 10), Camera.main.transform.forward, out hit, 10000);

        obj.transform.position = hit.point;

        obj.GetComponent<Millitary>().quantityMilitarySolo = castle.GetComponent<CastleEntry>()
            .quantityMilitary[createTypeOf];

        obj.GetComponent<Soldier>().wrogosc = castle.GetComponent<CastleEntry>().wrogosc;

        castle.GetComponent<CastleEntry>().quantityMilitary[createTypeOf] = 0;

        obj.GetComponent<Soldier>().typeOfWarior = createTypeOf;
        mainManager.army.Add(obj.GetComponent<Soldier>());
    }

    private void Update()
    {
        this.AddItemInMousePressPosition();
        if (Input.GetKeyDown("escape"))
        {
            Application.LoadLevel("MapMenu");
        }
    }

    /// <summary>
    /// Wykrywa punkt na ktory ma byc dodany obiekt z zamku 
    /// jesli flaga militaryStatus jest up to dodaje go do gry i przesuwa w wyznaczone miejsce
    /// </summary>
    private void AddItemInMousePressPosition() {
        if (addMilitaryStatus)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 10000))
            {
                if (hit.transform.name == transform.name)
                {
                    Physics.Raycast(hit.point, Camera.main.transform.forward, out hit, 10000);
                }
                detinationPosition = hit.point + new Vector3(0, 1, 0);
            }
            if (currentObject == null)
            {
                currentObject = Instantiate(army[createTypeOf], castle.transform.position + new Vector3(0, 0, 10), new Quaternion(0, transform.rotation.y, 0, transform.rotation.w), castle.transform.parent) as GameObject;
                currentObject.GetComponent<ObjectTransform>().canEntry = false;
                mainManager.setParentJednostki(currentObject);
            }
            currentObject.transform.position = detinationPosition;

            if (Input.GetMouseButtonDown(0))
            {
                SetObjectProperty(currentObject, castle.transform.position);
                currentObject = null;

                addMilitaryStatus = false;
                rekrutacjaCanvas.enabled = false;
            }
        }
    }

    /// <summary>
    /// wyswietla inventory dla danego zamku whithCastle
    /// </summary>
    /// <param name="whithCastle"></param>
    public void ReloadCanvas(GameObject whithCastle)
    {
        castle = whithCastle;
        if (castle.GetComponent<CastleEntry>().wrogosc == 0)
        {
            rekrutacjaCanvas.enabled = true;
            economyCanvas.enabled = false;
        }
    }

    public void LoadEconomyCnavas()
    {
        rekrutacjaCanvas.enabled = false;
        economyCanvas.enabled = true;
    }

    public void CloseCanvas()
    {
        rekrutacjaCanvas.enabled = false;
        economyCanvas.enabled = false;
    }

    public void LoadRekrutacjaCanvas()
    {
        rekrutacjaCanvas.enabled = true;
        economyCanvas.enabled = false;
    }
}
