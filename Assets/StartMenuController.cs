using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour {

	public Canvas exitMenu;
	public Button startButton;
	public Button exitButton;

	// Use this for initialization
	void Start () 
	{
		exitMenu = exitMenu.GetComponent<Canvas>();
		startButton = startButton.GetComponent<Button> ();
		exitButton = exitButton.GetComponent<Button> ();
		exitMenu.enabled = false;
	}

	public void ExitPress() 
	{
		exitMenu.enabled = true;
		startButton.enabled = false;
		exitButton.enabled = false;
	}
	public void NoPress()
	{
		exitMenu.enabled = false;
		startButton.enabled = true;
		exitButton.enabled = true;
	}

	public void StartLevel () 
	{
		SceneManager.LoadScene (1);
	}

	public void ExitGame () 
	{
		Application.Quit(); 
	}
}
