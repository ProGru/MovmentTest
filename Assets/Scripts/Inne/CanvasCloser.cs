using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CanvasCloser : MonoBehaviour {

    public Canvas thisCanvas;
    public Text mainText;
    public Text title;
	void Start () {
        mainText = mainText.GetComponent<Text>();
        title = title.GetComponent<Text>();
	}

    public void closeCanvas()
    {
        Destroy(gameObject);
    }

    public void setMainText(string text)
    {
        mainText.text = text;
    }

    public void setTitle(string text)
    {
        title.text = text;
    }

    public void quitGame()
    {
        Application.Quit();
    }

}
