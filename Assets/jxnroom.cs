using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class jxnroom : MonoBehaviour {

	public bool rendering = false;
	public bool running = true;

	public void render ()
	{
		rendering = true;
		Debug.Log ("Telling attached junctionroom to render...");
	}

}
