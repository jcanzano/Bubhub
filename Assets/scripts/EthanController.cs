using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class EthanController : MonoBehaviour {

	private SkinnedMeshRenderer[] renderers;
	public int postoffset;
	public AudioSource audioSource;
	public float minFov = 10f;
	public float maxFov = 60f;
	public float defaultFov = 60f;
	public float initialZoomFov = 40f;
	public float zoomSensitivity = 10f;

	private float currentFov;

	void Start() {		
		GetComponent<FirstPersonController>().enabled = true;
		GetComponentInChildren<AudioListener>().enabled = true;
		GetComponentInChildren<FlareLayer>().enabled = true;
		GetComponentInChildren<Camera>().enabled = true;
		audioSource = GetComponents<AudioSource> ()[1];

		renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
		renderers [0].material.color = Color.blue;

		Camera.main.fieldOfView = defaultFov;
	}
	
	// Update is called once per frame
	void Update () {
		// SLAMMIN
		if (Input.GetKeyDown ("q")) {
			audioSource.Play ();
		}

		// camera zoom effect
		if (Input.GetKeyDown ("e")) {
			Camera.main.fieldOfView = initialZoomFov;
		}
		if (Input.GetKey ("e")) {
			currentFov = Camera.main.fieldOfView;
			currentFov += -Input.mouseScrollDelta.y * zoomSensitivity;
			currentFov = Mathf.Clamp (currentFov, minFov, maxFov);
			Camera.main.fieldOfView = currentFov;
		}
		if (Input.GetKeyUp ("e")) {
			Camera.main.fieldOfView = defaultFov;
		}
	}
}