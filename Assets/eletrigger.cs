using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eletrigger : MonoBehaviour {

	Vector3 positiondifference = new Vector3 (0, 10, 0);
	public GameObject newfloor;

	void OnTriggerEnter(Collider other)
	{
		Debug.Log ("loading new floor...");
		Vector3 oldposition = transform.parent.position;
		Vector3 newposition = new Vector3(0, 0, 0);
		newposition = oldposition + positiondifference;
		Instantiate (newfloor, transform.parent.Find("exit").position, Quaternion.identity);
		GameObject player = GameObject.Find ("FirstPersonEthan");
		player.transform.position = player.transform.position + positiondifference;
		player.GetComponent<globalvars>().newspawnpoint = newposition;
		player.GetComponent<globalvars> ().newspawnrot = Quaternion.identity;
	}

}
