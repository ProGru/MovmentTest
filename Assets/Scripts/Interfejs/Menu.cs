using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public Texture GameLogo;
	public float buttonWidth = 250;
	public float buttonHeight = 40;
	private float buttonMargin = 20;

	void OnGUI()
	{
		GUI.DrawTexture(new Rect (0, 0, 800, 300), GameLogo);

		if(GUI.Button(new Rect (200, 100, buttonWidth, buttonHeight), "New Game")) {
            Application.LoadLevel("Scene");
        }
        if (GUI.Button(new Rect (200, 100 + buttonHeight + buttonMargin, buttonWidth, buttonHeight), "Save")) {

		}
		if(GUI.Button(new Rect (200, 100 + (buttonHeight + buttonMargin) * 2, buttonWidth, buttonHeight), "Load")) {

		}

		if(GUI.Button(new Rect (200, 100 + (buttonHeight + buttonMargin) * 3, buttonWidth, buttonHeight), "Exit")) {
			Application.Quit();
		}
	}
}
