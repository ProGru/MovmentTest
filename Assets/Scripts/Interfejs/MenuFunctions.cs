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
    public Canvas armyCanvas;
    public Canvas recruitmentCanvas;
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

    public Text numberEconomy1;
    public Text numberEconomy2;
    public Text numberEconomy3;
    public Text numberEconomy4;
    public Text numberEconomy5;
    public Text numberEconomy6;

    public Button cancelArmy5;
    public Button cancelArmy4;
    public Button cancelArmy3;
    public Button cancelArmy2;
    public Button cancelArmy1;


    private void Start()
    {
        popup = popup.GetComponent<Canvas>();
        popup.enabled = false;
        textPopup = textPopup.GetComponent<Text>();
        mainManager = FindObjectOfType<MainManager>();
        economyCanvas = economyCanvas.GetComponent<Canvas>();
        armyCanvas = armyCanvas.GetComponent<Canvas>();
        recruitmentCanvas = recruitmentCanvas.GetComponent<Canvas>();
        nextRound = nextRound.GetComponent<Canvas>();
        text = text.GetComponent<Text>();
        gold = gold.GetComponent<Text>();
        Time.timeScale = 1;
        economyCanvas.enabled = false;
        armyCanvas.enabled = false;
        recruitmentCanvas.enabled = false;
        infoWindow.SetActive(false);
        GoldButton();

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

        cancelArmy5 = cancelArmy5.GetComponent<Button>();
        cancelArmy5.onClick.AddListener(CancelOnClick1);

        cancelArmy4 = cancelArmy4.GetComponent<Button>();
        cancelArmy4.onClick.AddListener(CancelOnClick2);

        cancelArmy3 = cancelArmy3.GetComponent<Button>();
        cancelArmy3.onClick.AddListener(CancelOnClick3);

        cancelArmy2 = cancelArmy2.GetComponent<Button>();
        cancelArmy2.onClick.AddListener(CancelOnClick4);

        cancelArmy1 = cancelArmy1.GetComponent<Button>();
        cancelArmy1.onClick.AddListener(CancelOnClick5);
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
                armyCanvas.enabled = false;
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
            armyCanvas.enabled = true;
            economyCanvas.enabled = false;
        }
    }

    public void LoadEconomyCnavas()
    {
        armyCanvas.enabled = false;
        economyCanvas.enabled = true;
        recruitmentCanvas.enabled = false;
    }

    public void CloseCanvas()
    {
        armyCanvas.enabled = false;
        economyCanvas.enabled = false;
        recruitmentCanvas.enabled = false;
    }

    public void LoadArmyCanvas()
    {
        armyCanvas.enabled = true;
        economyCanvas.enabled = false;
        recruitmentCanvas.enabled = false;
    }

    public void LoadRecruitmentCanvas()
    {
        armyCanvas.enabled = false;
        economyCanvas.enabled = false;
        recruitmentCanvas.enabled = true;
    }


    //just trying

    public void TaskOnClick()
    {
        numberArmy.text = "1";
    }

    public void TaskOnClick_2()
    {
        numberArmy2.text = "1";
    }

    public void TaskOnClick_3()
    {
        numberArmy3.text = "1";
    }

    public void TaskOnClick_4()
    {
        numberArmy4.text = "1";
    }

    public void TaskOnClick_5()
    {
        numberArmy5.text = "1";
    }

    public void TaskOnClick1()
    {
        numberEconomy1.text = "1";
    }

    public void TaskOnClick2()
    {
        numberEconomy2.text = "1";
    }

    public void TaskOnClick3()
    {
        numberEconomy3.text = "1";
    }

    public void TaskOnClick4()
    {
        numberEconomy4.text = "1";
    }

    public void TaskOnClick5()
    {
        numberEconomy5.text = "1";
    }

    public void TaskOnClick6()
    {
        numberEconomy6.text = "1";
    }

    public void CancelOnClick1()
    {
        numberArmy5.text = "0";
    }

    public void CancelOnClick2()
    {
        numberArmy4.text = "0";
    }

    public void CancelOnClick3()
    {
        numberArmy3.text = "0";
    }

    public void CancelOnClick4()
    {
        numberArmy2.text = "0";
    }

    public void CancelOnClick5()
    {
        numberArmy.text = "0";
    }

    public void CancelOnClick11()
    {
        numberEconomy1.text = "0";
    }

    public void CancelOnClick12()
    {
        numberEconomy2.text = "0";
    }

    public void CancelOnClick13()
    {
        numberEconomy3.text = "0";
    }

    public void CancelOnClick14()
    {
        numberEconomy4.text = "0";
    }

    public void CancelOnClick15()
    {
        numberEconomy5.text = "0";
    }

    public void CancelOnClick16()
    {
        numberEconomy6.text = "0";
    }


}
