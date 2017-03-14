using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BoyColor : NetworkBehaviour {

	[SyncVar]
	public Color color;

	SkinnedMeshRenderer[] renderers;

	// Use this for initialization
	void Start () {
		renderers = GetComponentsInChildren<SkinnedMeshRenderer> ();
		for (int i = 0; i < renderers.Length; i++) {
			renderers [i].material.color = color;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
