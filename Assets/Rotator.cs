using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	public Vector3 rotate;
	public bool rotate_global = true;

	private Space relativeTo = Space.Self;

	// Use this for initialization
	void Start () {
		if (rotate_global) {
			relativeTo = Space.World;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (rotate*Time.deltaTime, relativeTo);
	}
}
