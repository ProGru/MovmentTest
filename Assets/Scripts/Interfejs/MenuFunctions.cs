using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Funkcje dla Canvasow (UI) oraz (GUI)
/// </summary>
public class MenuFunctions : MonoBehaviour {

    public Canvas sojuszCanvas;
    public Canvas economyCanvas;
    public Canvas armyCanvas;
    public Canvas recruitmentCanvas;
    public Canvas nextRound;
    public Canvas popup;
    public Canvas cancelCanvas;
    public Canvas prefabCanvas;
    public GameObject infoWindow;
    public GameObject infoView;
    public Canvas gameOver;
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

    public Text numberInCastle0;
    public Text numberInCastle1;
    public Text numberInCastle2;
    public Text numberInCastle3;
    public Text numberInCastle4;

    public Text numberEconomy1;
    public Text numberEconomy2;
    public Text numberEconomy3;
    public Text numberEconomy4;
    public Text numberEconomy5;
    public Text numberEconomy6;

    public Text numberEconomyLvl0;
    public Text numberEconomyLvl1;
    public Text numberEconomyLvl2;
    public Text numberEconomyLvl3;
    public Text numberEconomyLvl4;
    public Text numberEconomyLvl5;

    public Button cancelArmy5;
    public Button cancelArmy4;
    public Button cancelArmy3;
    public Button cancelArmy2;
    public Button cancelArmy1;

    public Button cancelInfWindow;

    private void Start()
    {
        gameOver = gameOver.GetComponent<Canvas>();
        sojuszCanvas = sojuszCanvas.GetComponent<Canvas>();
        popup = popup.GetComponent<Canvas>();
        popup.enabled = false;
        textPopup = textPopup.GetComponent<Text>();
        mainManager = FindObjectOfType<MainManager>();
        economyCanvas = economyCanvas.GetComponent<Canvas>();
        armyCanvas = armyCanvas.GetComponent<Canvas>();
        recruitmentCanvas = recruitmentCanvas.GetComponent<Canvas>();
        nextRound = nextRound.GetComponent<Canvas>();
        cancelCanvas = cancelCanvas.GetComponent<Canvas>();
        prefabCanvas = prefabCanvas.GetComponent<Canvas>();


        text = text.GetComponent<Text>();
        gold = gold.GetComponent<Text>();
        Time.timeScale = 1;
        cancelCanvas.enabled = false;
        economyCanvas.enabled = false;
        armyCanvas.enabled = false;
        recruitmentCanvas.enabled = false;
        gameOver.enabled = false;
        infoWindow.SetActive(true);
        cancelInfWindow = cancelInfWindow.GetComponent<Button>();

        GoldButton();

        numberArmy = numberArmy.GetComponent<Text>();
        numberArmy2 = numberArmy2.GetComponent<Text>();
        numberArmy3 = numberArmy3.GetComponent<Text>();
        numberArmy4 = numberArmy4.GetComponent<Text>();
        numberArmy5 = numberArmy5.GetComponent<Text>();

        numberInCastle0 = numberInCastle0.GetComponent<Text>();
        numberInCastle1 = numberInCastle1.GetComponent<Text>();
        numberInCastle2 = numberInCastle2.GetComponent<Text>();
        numberInCastle3 = numberInCastle3.GetComponent<Text>();
        numberInCastle4 = numberInCastle4.GetComponent<Text>();

        numberEconomy1 = numberEconomy1.GetComponent<Text>();
        numberEconomy2 = numberEconomy2.GetComponent<Text>();
        numberEconomy3 = numberEconomy3.GetComponent<Text>();
        numberEconomy4 = numberEconomy4.GetComponent<Text>();
        numberEconomy5 = numberEconomy5.GetComponent<Text>();
        numberEconomy6 = numberEconomy6.GetComponent<Text>();

        numberEconomyLvl0 = numberEconomyLvl0.GetComponent<Text>();
        numberEconomyLvl1 = numberEconomyLvl1.GetComponent<Text>();
        numberEconomyLvl2 = numberEconomyLvl2.GetComponent<Text>();
        numberEconomyLvl3 = numberEconomyLvl3.GetComponent<Text>();
        numberEconomyLvl4 = numberEconomyLvl4.GetComponent<Text>();
        numberEconomyLvl5 = numberEconomyLvl5.GetComponent<Text>();

        cancelArmy5 = cancelArmy5.GetComponent<Button>();
        cancelArmy4 = cancelArmy4.GetComponent<Button>();
        cancelArmy3 = cancelArmy3.GetComponent<Button>();
        cancelArmy2 = cancelArmy2.GetComponent<Button>();
        cancelArmy1 = cancelArmy1.GetComponent<Button>();
        displayAnulacjaBouldings();
    }

    public void PopupWindow(string text) {
        popup.enabled = true;
        textPopup.text = text;
    }

    public void PopupWindowFalse()
    {
        popup.enabled = false;
    }

    public void showMassage(string title,string text)
    {
        CanvasCloser[] canvasClosers = FindObjectsOfType<CanvasCloser>();
        if (canvasClosers.Length < 6)
        {
            GameObject infoCanvas = Instantiate(infoView, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), prefabCanvas.transform.parent) as GameObject;
            infoCanvas.transform.position =
                new Vector3(infoCanvas.transform.position.x + 50 * canvasClosers.Length,
                infoCanvas.transform.position.y - 50 * canvasClosers.Length,
                infoCanvas.transform.position.z);
            mainManager.setParentPrefabCavas(infoCanvas);
            infoCanvas.GetComponent<CanvasCloser>().setMainText(text);
            infoCanvas.GetComponent<CanvasCloser>().setTitle(title);
        }

    }

    public void showMassageGameOver(string title, string text)
    {
        gameOver.enabled = true;
        gameOver.GetComponent<CanvasCloser>().setTitle(title);
        gameOver.GetComponent<CanvasCloser>().setMainText(text);
    }

    public void showJednostkaInfo(int index)
    {
        PopupWindow(mainManager.getJednostkaName(index) + "\n Rekrutacja tury: "
            + mainManager.getRekrutacjaTime(index) + "\n" + mainManager.getSojdierInfo(index));
    }

    public void showBouldingInfo(int index)
    {
        PopupWindow(mainManager.getBouldingInfo(index));
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
                currentObject.GetComponent<ObjectTransform>().WojskaName = castle.GetComponent<CastleEntry>().castleName;
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
            cancelCanvas.enabled = true;
            recruitmentCanvas.enabled = true;
            economyCanvas.enabled = false;
            armyCanvas.enabled = false;
        }
        displayAnulacjaWojska();
    }

    public void LoadEconomyCnavas()
    {
        armyCanvas.enabled = false;
        economyCanvas.enabled = true;
        recruitmentCanvas.enabled = false;
        displayBouldingLvl();
    }

    public void LoadSojuszCanvas()
    {
        sojuszCanvas.enabled = true;
    }

    public void CloseSojuszCanvas()
    {
        sojuszCanvas.enabled = false;

    }

    public void CloseCanvas()
    {
        cancelCanvas.enabled = false;
        armyCanvas.enabled = false;
        economyCanvas.enabled = false;
        recruitmentCanvas.enabled = false;
    }

    public void LoadArmyCanvas()
    {
        armyCanvas.enabled = true;
        economyCanvas.enabled = false;
        recruitmentCanvas.enabled = false;
        displayMilitaryInCastle();
    }

    public void LoadRecruitmentCanvas()
    {
        armyCanvas.enabled = false;
        economyCanvas.enabled = false;
        recruitmentCanvas.enabled = true;
    }

    public void displayAnulacjaWojska()
    {
        if (castle != null)
        {
            int[] rekrutowaneWojska = mainManager.getMilitaryInRekrutacja(castle.GetComponent<CastleEntry>());
            numberArmy.text = rekrutowaneWojska[0].ToString();
            numberArmy2.text = rekrutowaneWojska[1].ToString();
            numberArmy3.text = rekrutowaneWojska[2].ToString();
            numberArmy4.text = rekrutowaneWojska[3].ToString();
            numberArmy5.text = rekrutowaneWojska[4].ToString();
        }
    }

    public void displayAnulacjaBouldings()
    {
        int[] bouldingProgres = mainManager.getBouldingProgres();
        numberEconomy1.text = bouldingProgres[1].ToString();
        numberEconomy2.text = bouldingProgres[2].ToString();
        numberEconomy3.text = bouldingProgres[3].ToString();
        numberEconomy4.text = bouldingProgres[4].ToString();
        numberEconomy5.text = bouldingProgres[0].ToString();
        numberEconomy6.text = bouldingProgres[5].ToString();
    }

    public void displayMilitaryInCastle()
    {
        if (castle != null)
        {
            int[] wojskaZamku = castle.GetComponent<CastleEntry>().quantityMilitary;

            numberInCastle0.text = wojskaZamku[0].ToString();
            numberInCastle1.text = wojskaZamku[1].ToString();
            numberInCastle2.text = wojskaZamku[2].ToString();
            numberInCastle3.text = wojskaZamku[3].ToString();
            numberInCastle4.text = wojskaZamku[4].ToString();
        }
    }

    public void displayBouldingLvl()
    {
        int[] bouldingLvl = mainManager.getBouldingLvl();
        numberEconomyLvl0.text = bouldingLvl[1].ToString();
        numberEconomyLvl1.text = bouldingLvl[2].ToString();
        numberEconomyLvl2.text = bouldingLvl[3].ToString();
        numberEconomyLvl3.text = bouldingLvl[4].ToString();
        numberEconomyLvl4.text = bouldingLvl[0].ToString();
        numberEconomyLvl5.text = bouldingLvl[5].ToString();
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
        displayAnulacjaBouldings();
    }

    public void TaskOnClick2()
    {
         displayAnulacjaBouldings();
    }

    public void TaskOnClick3()
    {
        displayAnulacjaBouldings();
    }

    public void TaskOnClick4()
    {
        displayAnulacjaBouldings();
    }

    public void TaskOnClick5()
    {
        displayAnulacjaBouldings();
    }

    public void TaskOnClick6()
    {
        displayAnulacjaBouldings();
    }

    public void CancelOnClick1()
    {
        mainManager.deleteRekrutowaneJednostki(castle.GetComponent<CastleEntry>(), 4);
        displayAnulacjaWojska();
    }

    public void CancelOnClick2()
    {
        mainManager.deleteRekrutowaneJednostki(castle.GetComponent<CastleEntry>(), 3);
        displayAnulacjaWojska();
    }

    public void CancelOnClick3()
    {
        mainManager.deleteRekrutowaneJednostki(castle.GetComponent<CastleEntry>(), 2);
        displayAnulacjaWojska();
    }

    public void CancelOnClick4()
    {
        mainManager.deleteRekrutowaneJednostki(castle.GetComponent<CastleEntry>(), 1);
        displayAnulacjaWojska();
    }

    public void CancelOnClick5()
    {
        mainManager.deleteRekrutowaneJednostki(castle.GetComponent<CastleEntry>(), 0);
        displayAnulacjaWojska();
    }

    public void CancelOnClick11()
    {
        mainManager.deleteBoulding(1);
        displayAnulacjaBouldings();
    }

    public void CancelOnClick12()
    {
        mainManager.deleteBoulding(2);
        displayAnulacjaBouldings();
    }

    public void CancelOnClick13()
    {
        mainManager.deleteBoulding(3);
        displayAnulacjaBouldings();
    }

    public void CancelOnClick14()
    {
        mainManager.deleteBoulding(4);
        displayAnulacjaBouldings();
    }

    public void CancelOnClick15()
    {
        mainManager.deleteBoulding(0);
        displayAnulacjaBouldings();

    }

    public void CancelOnClick16()
    {
        mainManager.deleteBoulding(5);
        displayAnulacjaBouldings();
    }

    public void rekrutujJednostke(int typeOfWarior)    {
        mainManager.rekrutujJednostke(castle.GetComponent<CastleEntry>(), typeOfWarior);
        displayAnulacjaWojska();
    }

    public void CancelInfWindow()
    {
        infoWindow.SetActive(false);
    }

}
