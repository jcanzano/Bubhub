using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalvars : MonoBehaviour {

	public int postoffset;
	public Vector3 newspawnpoint;
	public Quaternion newspawnrot;

	// Use this for initialization
	void Start () {
		postoffset = 0;
		newspawnpoint.Set (0, 0, 0);
		newspawnrot.Set (0, 0, 0, 0);
	}

}
