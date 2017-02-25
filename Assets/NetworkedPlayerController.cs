using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class NetworkedPlayerController : NetworkBehaviour {

	private SkinnedMeshRenderer[] renderers;
	public int postoffset;
	public AudioSource audioSource;

	public override void OnStartLocalPlayer() {
		GetComponent<FirstPersonController>().enabled = true;
		GetComponentInChildren<AudioListener>().enabled = true;
		GetComponentInChildren<FlareLayer>().enabled = true;
		GetComponentInChildren<Camera>().enabled = true;
		audioSource = GetComponents<AudioSource> ()[1];
		
		renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
		renderers [0].material.color = Color.blue;
	}

	void Start() {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isLocalPlayer) {
			if (Input.GetKeyDown ("q")) {
				audioSource.Play ();
			}
		}
	}
}