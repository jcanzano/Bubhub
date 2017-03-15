using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class small30 : MonoBehaviour {

	GameObject player;
	Transform exit;
	Transform entrance;

	// Use this for initialization
	void Start () {
		entrance = transform.Find ("entrance");
		exit = transform.Find ("exit");
		player = GameObject.Find ("FirstPersonPlayer 1(Clone)");
		player.GetComponent<globalvars> ().newspawnpoint = exit.position;
		player.GetComponent<globalvars> ().newspawnrot = exit.rotation;
	}

}
