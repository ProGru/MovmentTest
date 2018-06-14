using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextInfoShow : MonoBehaviour {
    MenuFunctions menuFunctions;
    public string noMoney = "Nie masz wystarczająco złota. Zlikwiduj jednostki lub podbij nowe tereny.";
    public string noMoneyTitle = "Skarbiec jest pusty!";
    public string maxBouldingLvl = "Ta osiągnęła już maksymalny level.";
    public string maxBouldingLvlTitle = "Maksymalny level osiągnięty.";
    public string arleadyBuilding = "Obiekt w budowli. Poczekaj na ukończenie budynku aby zacząć nową budowę.";
    public string arleadyBouldingTitle = "Obiekt aktualnie w budowli.";
    public string bonus = "Twój bonus za budowę wzrósł do: ";
    public string bonusTitle = "Ukończono budowę!";
    public string waste = "Bankructwo! Jeśli nadal będziesz na minusie - gra zakończy się porażką.";
    public string wasteTitle = "Warning: Game Over!";
    public string captureCastle = "Przejąłeś wrogi zamek. " +
        "\n Teraz możesz rekrutować w nim jednostki i rozbudowywać swoje budynki.";
    public string captureCastleTitle = "Przejęcie Zamku";
    public string lostCastle = "Straciłeś zamek i armię. Rekrutowane jednostki zostały stracone.";
    public string lostCastleTitle = "Straciłeś zamek!";
    public string canNotAttackFriend = "Nie możesz atakować sojuszników";
    public string canNotAttackFriendTitle = "Sojusznik";
    public string defeatEnemy = "Za pokonanie wroga zdobywasz skarb";
    public string defeatEnemyTitle = "Bonus";
    public string gameOverTitle = "Game Over!";
    public string gameOverLessCastle = "Przekroczyłeś 20 tur lub przejeli ci wszystkie zamki";
    public string gameOverMoney = "GameOver jesteś zadłuzony";
    public string win = "Gratulacje zdołałeś podbić cały kontynent !!";
    public string winTitle = "Win !!!";

    void Start () {
        menuFunctions = FindObjectOfType<MenuFunctions>();
	}

    public void showMassageWindow(string massage, string title)
    {
        menuFunctions.showMassage(title, massage);
    }

    public void showGameOverWindow(string massage, string title)
    {
        menuFunctions.showMassageGameOver(title, massage);
    }

}
