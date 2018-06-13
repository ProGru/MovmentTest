using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextInfoShow : MonoBehaviour {
    MenuFunctions menuFunctions;
    public string noMoney = "Niemasz tyle pieniedzy sprobój coś sprzedać lub przejąć!";
    public string noMoneyTitle = "Brak pieniedzy";
    public string maxBouldingLvl = "Ta budowla ma już maksymalny Level";
    public string maxBouldingLvlTitle = "Maksymalny Level Osiagnięty";
    public string arleadyBuilding = "Już budujesz ten typ budowli poczekaj aż " +
        "budowlańcy zakończą pracę by zacząć nową budowę";
    public string arleadyBouldingTitle = "Już Budujesz Ten Typ";
    public string bonus = "Twój bonus za budowę wzrósł do: ";
    public string bonusTitle = "Ukończono budowę";
    public string waste = "Zarabiasz mniej niż wydajesz jeśli nadal będziesz na minusie zakończysz grę";
    public string wasteTitle = "Warning Game Over!";
    public string captureCastle = "Przejąłeś wrogi zamek. " +
        "\n Teraz możesz rekrutować w nim jednostki i rozbudowywać swoje budynki.";
    public string captureCastleTitle = "Przejecie Zamku";
    public string lostCastle = "Straciłeś zamek wszystkie jednostki w zamku zostały zabite. \n" +
        "Rekrutowane jednostki zostały stracone.";
    public string lostCastleTitle = "Straciłeś zamek!";

    void Start () {
        menuFunctions = FindObjectOfType<MenuFunctions>();
	}

    public void showMassageWindow(string massage, string title)
    {
        menuFunctions.showMassage(title, massage);
    }
}
