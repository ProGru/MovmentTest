using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Funkcja dla Obiektow wojska umozliwia:
/// -ich przesuwanie po powierzchni Mesch renderera
/// -odkrywanie i ukrywanie zamkow przy zblizeniu
/// -ograniczenie ruchu na ture
/// </summary>
[RequireComponent(typeof(Renderer)),RequireComponent(typeof(NavMeshAgent))]
public class ObjectTransform : MonoBehaviour
{
    Color beforeColor;
    RaycastHit hit;
    Vector3 newPosition;
    Quaternion newRotation;
    NavMeshAgent _navMeshAgent;
    Soldier soldier;
    private bool selected = false;
    public bool canEntry = true;
    CastleEntry[] platforms;
    private MainManager mainManager;
    private MenuFunctions menuFunctions;
    public float makeDistance = 0;
    public bool canBeMoved = true;
    public bool yours = false;
    public string WojskaName;


    /// <summary>
    /// dystans od pozycji do obiektu
    /// </summary>
    /// <param name="firstPos"></param>
    /// <param name="second"></param>
    /// <returns></returns>
    float disctance(Vector3 firstPos,GameObject second)
    {
        Vector3 secondPos = second.transform.position;
        float dx = firstPos.x - secondPos.x, dy = firstPos.y - secondPos.y, dz = firstPos.z - secondPos.z;
        return Mathf.Sqrt(dx * dx + dy * dy + dz * dz);
    }

    /// <summary>
    /// dystans od pozycji do pozycji
    /// </summary>
    /// <param name="firstPos"></param>
    /// <param name="second"></param>
    /// <returns></returns>
    float disctance(Vector3 firstPos, Vector3 second)
    {
        Vector3 secondPos = second;
        float dx = firstPos.x - secondPos.x, dy = firstPos.y - secondPos.y, dz = firstPos.z - secondPos.z;
        return Mathf.Sqrt(dx * dx + dy * dy + dz * dz);
    }

    /// <summary>
    /// Ustawia widzialnosc zamkow na TRUE
    /// </summary>
    private void SetCastelsVisability()
    {
        if (canEntry)
        {
            platforms = mainManager.getCastles();
            for (int i = 0; i < platforms.Length; i++)
            {

                if (disctance(newPosition, platforms[i].gameObject) <= 100)
                {
                    if (!platforms[i].GetComponent<CastleEntry>().yours)
                    {
                        platforms[i].gameObject.SetActive(true);
                    }
                }
            }
            ArrayList army = mainManager.army;
            for (int i = 0; i < army.Count; i++)
            {
                Soldier soldier = (Soldier)army[i];
                if (soldier != null)
                {
                    if (disctance(newPosition, soldier.gameObject) <= 100)
                    {
                        soldier.gameObject.SetActive(true);
                    }
                }
                else
                {
                    army.RemoveAt(i);
                }

            }
        }
    }

    /// <summary>
    /// ustawia widzialnosc zamkow na FALSE
    /// </summary>
    public void SetCastelsUnvisibility()
    {
        platforms = mainManager.getCastles();
        for (int i = 0; i < platforms.Length; i++)
        {
            if (platforms[i].wrogosc != 0)
            {
                if (disctance(newPosition, platforms[i].gameObject) >= 100)
                {
                    platforms[i].gameObject.SetActive(false);
                }
            }

        }
        ArrayList army = mainManager.army;
        for (int i = 0; i < army.Count; i++)
        {
            Soldier soldier = (Soldier)army[i];
            if (soldier.wrogosc != 0)
            {
                if (disctance(newPosition, soldier.gameObject) >= 100)
                {
                    soldier.gameObject.SetActive(false);
                }
            }

        }
    }


    /// <summary>
    /// Ustawia pozycje do ktorej pujdzie jednostka
    /// (jesli odleglosc nie przekracza maksymalnej mozliwej)
    /// </summary>
    /// <param name="target"></param>
    public void SetTargetDestination(Vector3 target)
    {
        if (canGoMoreInRound(transform.position, target))
        {
            newPosition = target;
        }
        else
        {
            newPosition = transform.position;
            //tu obliczymy pozycje nowa (chyba ze nie zmieniac)
        }
    }

    /// <summary>
    /// Sprawdza czy obiekt moze isc dalej w danej rundzie zwieksza licznik o dystans
    /// </summary>
    /// <param name="actualyPosition"></param>
    /// <param name="destinationPosition"></param>
    /// <returns></returns>
    private bool canGoMoreInRound(Vector3 actualyPosition, Vector3 destinationPosition)
    {
        if (this.disctance(actualyPosition, destinationPosition) + makeDistance > soldier.maxDistancePerRound+mainManager.bouldingbonus[0])
        {
            return false;
        }
        else
        {
            makeDistance += this.disctance(actualyPosition, destinationPosition);
            return true;
        }
    }

    private void Start()
    {
        menuFunctions = FindObjectOfType<MenuFunctions>();
        soldier = GetComponent<Soldier>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        newPosition = transform.position;
        mainManager = FindObjectOfType<MainManager>();
    }

    void OnMouseDown()
    {
        mainManager.CleanCanEntryAndCounter();
        Renderer rend = GetComponent<Renderer>();
        rend.material.SetColor("_Color", Color.yellow);
    }

    void showQuantityMilitaryInformation()
    {
        if (GetComponent<Millitary>().multi)
        {
            Debug.Log("Multi :" + GetComponent<Millitary>().quantityMilitaryMulti[0]);
        }
        else
        {
            Debug.Log("Solo :" + GetComponent<Millitary>().quantityMilitarySolo);
        }
    }

    public void showMillitaryInPopap()
    {
        Millitary millitary = this.GetComponent<Millitary>();
        if (millitary.multi)
        {
            menuFunctions.PopupWindow(WojskaName + "\n Łucznicy: " + millitary.quantityMilitaryMulti[0].ToString()
                + "\n Konnica: " + millitary.quantityMilitaryMulti[1].ToString()
                + "\n Piechota: " + millitary.quantityMilitaryMulti[2].ToString()
                + "\n Wojownicy: " + millitary.quantityMilitaryMulti[3].ToString()
                + "\n Szpiedzy: " + millitary.quantityMilitaryMulti[4].ToString()
                );
        }
        else
        {
            int type = millitary.GetComponent<Soldier>().typeOfWarior;
            string[] wojska = { "Łucznicy", "Konnica", "Piechota", "Wojownicy", "Szpiedzy" };
            menuFunctions.PopupWindow(WojskaName + "\n "+ wojska[type]+": " + millitary.quantityMilitaryMulti[0].ToString());
        }
    }

    private void OnMouseEnter()
    {
        showMillitaryInPopap();
        
        Renderer rend = GetComponent<Renderer>();
        showQuantityMilitaryInformation();
        if (rend.material.GetColor("_Color") != Color.red && rend.material.GetColor("_Color") != Color.yellow)
        {
            beforeColor = rend.material.GetColor("_Color");
            rend.material.SetColor("_Color", Color.red);

        }
        else if (rend.material.GetColor("_Color") != Color.yellow)
        {
            rend.material.SetColor("_Color", Color.red);
        }
    }

    private void OnMouseExit()
    {
       
        Renderer rend = GetComponent<Renderer>();
        if (rend.material.GetColor("_Color") == Color.red)
        {
            rend.material.SetColor("_Color", beforeColor);
        }
    }

    private void OnMouseUp()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.SetColor("_Color", beforeColor);
        if (this.yours)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000))
            {
                if (hit.transform.name == gameObject.transform.name)
                {
                    Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, 100);
                }

                newRotation = Quaternion.LookRotation(hit.point - transform.position);
                this.SetTargetDestination(hit.point + new Vector3(0, 1, 0));

                SetCastelsUnvisibility();
            }
        }
    }

    private void Update()
    {
        if (canBeMoved)
        {
            SetCastelsVisability();
            if (!Input.GetKey(KeyCode.LeftControl))
            {
                if (Input.GetMouseButtonUp(0))
                {
                    if (selected)
                    {
                        canEntry = true;
                        this.OnMouseUp();
                        selected = false;
                    }
                }
                if (_navMeshAgent.isOnNavMesh)
                {
                    _navMeshAgent.SetDestination(newPosition);
                }

                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 10 * Time.deltaTime);
            }
            else
            {
                if (Input.GetMouseButtonUp(0))
                {
                    Vector3 canPos = Camera.main.WorldToScreenPoint(transform.position);
                    canPos.y = BoxSelection.InvertMouseY(canPos.y);
                    selected = BoxSelection.selection.Contains(canPos);
                }
                if (selected)
                {
                    this.OnMouseEnter();
                }
                else
                {
                    this.OnMouseExit();
                }
            }
        }
    }

    public void setAttackTarget(Vector3 position)
    {
        newPosition = position;
    }

    public Vector3 getAttackTarget()
    {
        return newPosition;
    }
}
