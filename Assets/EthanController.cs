using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class EthanController : MonoBehaviour {

	private SkinnedMeshRenderer[] renderers;
	public int postoffset;
	public AudioSource audioSource;

	void Start() {		
		GetComponent<FirstPersonController>().enabled = true;
		GetComponentInChildren<AudioListener>().enabled = true;
		GetComponentInChildren<FlareLayer>().enabled = true;
		GetComponentInChildren<Camera>().enabled = true;
		audioSource = GetComponents<AudioSource> ()[1];

		renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
		renderers [0].material.color = Color.blue;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("q")) {
			audioSource.Play ();
		}
	}
}