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
		GUI.DrawTexture(new Rect (0, 0, 1200, 600), GameLogo);
        GUI.DrawTexture(new Rect(900, 400, 1200, 600), GameLogo);


        if (GUI.Button(new Rect (900, 400, buttonWidth, buttonHeight), "Game")) {
            Application.LoadLevel("Scene");
        }
        if (GUI.Button(new Rect (900, 400 + buttonHeight + buttonMargin, buttonWidth, buttonHeight), "Save")) {

		}
		if(GUI.Button(new Rect (900, 400 + (buttonHeight + buttonMargin) * 2, buttonWidth, buttonHeight), "Load")) {

		}

		if(GUI.Button(new Rect (900, 400 + (buttonHeight + buttonMargin) * 3, buttonWidth, buttonHeight), "Exit")) {
			Application.Quit();
		}
	}
}
