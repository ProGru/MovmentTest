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
    public Canvas popup;
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
    public Text textPopup;
    /// <summary>
    /// ////
    /// </summary>

    public Text numberArmy;
    public Text numberArmy2;
    public Text numberArmy3;
    public Text numberArmy4;
    public Text numberArmy5;
    public Button armySlot;
    public Button armySlot2;
    public Button armySlot3;
    public Button armySlot4;
    public Button armySlot5;

    public Text numberEconomy1;
    public Text numberEconomy2;
    public Text numberEconomy3;
    public Text numberEconomy4;
    public Text numberEconomy5;
    public Text numberEconomy6;
    public Button economySlot1;
    public Button economySlot2;
    public Button economySlot3;
    public Button economySlot4;
    public Button economySlot5;
    public Button economySlot6;

    public Button cancelArmy5;
    public Button cancelArmy4;
    public Button cancelArmy3;


    private void Start()
    {
        popup = popup.GetComponent<Canvas>();
        popup.enabled = false;
        textPopup = textPopup.GetComponent<Text>();
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
        //////

        numberArmy = numberArmy.GetComponent<Text>();
        numberArmy2 = numberArmy2.GetComponent<Text>();
        numberArmy3 = numberArmy3.GetComponent<Text>();
        numberArmy4 = numberArmy4.GetComponent<Text>();
        numberArmy5 = numberArmy5.GetComponent<Text>();

        numberEconomy1 = numberEconomy1.GetComponent<Text>();
        numberEconomy2 = numberEconomy2.GetComponent<Text>();
        numberEconomy3 = numberEconomy3.GetComponent<Text>();
        numberEconomy4 = numberEconomy4.GetComponent<Text>();
        numberEconomy5 = numberEconomy5.GetComponent<Text>();
        numberEconomy6 = numberEconomy6.GetComponent<Text>();

        armySlot = armySlot.GetComponent<Button>();
        armySlot.onClick.AddListener(TaskOnClick);

        armySlot2 = armySlot2.GetComponent<Button>();
        armySlot2.onClick.AddListener(TaskOnClick_2);

        armySlot3 = armySlot3.GetComponent<Button>();
        armySlot3.onClick.AddListener(TaskOnClick_3);

        armySlot4 = armySlot4.GetComponent<Button>();
        armySlot4.onClick.AddListener(TaskOnClick_4);

        armySlot5 = armySlot5.GetComponent<Button>();
        armySlot5.onClick.AddListener(TaskOnClick_5);

        economySlot1 = economySlot1.GetComponent<Button>();
        economySlot1.onClick.AddListener(TaskOnClick);

        economySlot2 = economySlot2.GetComponent<Button>();
        economySlot2.onClick.AddListener(TaskOnClick);

        economySlot3 = economySlot3.GetComponent<Button>();
        economySlot3.onClick.AddListener(TaskOnClick);

        economySlot4 = economySlot4.GetComponent<Button>();
        economySlot4.onClick.AddListener(TaskOnClick);

        economySlot5 = economySlot5.GetComponent<Button>();
        economySlot5.onClick.AddListener(TaskOnClick);

        economySlot6 = economySlot6.GetComponent<Button>();
        economySlot6.onClick.AddListener(TaskOnClick);

        cancelArmy5 = cancelArmy5.GetComponent<Button>();
        cancelArmy5.onClick.AddListener(CancelOnClick1);

        cancelArmy4 = cancelArmy4.GetComponent<Button>();
        cancelArmy4.onClick.AddListener(CancelOnClick2);

        cancelArmy3 = cancelArmy3.GetComponent<Button>();
        cancelArmy3.onClick.AddListener(CancelOnClick3);
        

       
    }

    public void PopupWindow(string text) {
        popup.enabled = true;
        textPopup.text = text;
    }

    public void PopupWindowFalse()
    {
        popup.enabled = false;
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


    //just trying

    public void TaskOnClick()
    {
        numberArmy.text = "1";
        Debug.Log("What happened?");
    }

    public void TaskOnClick_2()
    {
        numberArmy2.text = "1";
        Debug.Log("What happened?");
    }

    public void TaskOnClick_3()
    {
        numberArmy3.text = "1";
        Debug.Log("What happened?");
    }

    public void TaskOnClick_4()
    {
        numberArmy4.text = "1";
        Debug.Log("What happened?");
    }

    public void TaskOnClick_5()
    {
        numberArmy5.text = "1";
        Debug.Log("What happened?");
    }

    public void TaskOnClick1()
    {
        numberEconomy1.text = "1";
        Debug.Log("What happened?");
    }

    public void TaskOnClick2()
    {
        numberEconomy2.text = "1";
        Debug.Log("What happened?");
    }

    public void TaskOnClick3()
    {
        numberEconomy3.text = "1";
        Debug.Log("What happened?");
    }

    public void TaskOnClick4()
    {
        numberEconomy4.text = "1";
        Debug.Log("What happened?");
    }

    public void TaskOnClick5()
    {
        numberEconomy5.text = "1";
        Debug.Log("What happened?");
    }

    public void TaskOnClick6()
    {
        numberEconomy6.text = "1";
        Debug.Log("What happened?");
    }

    public void CancelOnClick1()
    {
        numberArmy5.text = "0";
        Debug.Log("Is it working?");
    }

    public void CancelOnClick2()
    {
        numberArmy4.text = "0";
        Debug.Log("Is it working?");
    }

    public void CancelOnClick3()
    {
        numberArmy3.text = "0";
        Debug.Log("Is it working?");
    }


}
