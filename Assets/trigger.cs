using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour {

	public GameObject room; 

	void OnTriggerEnter(Collider other)
	{
		Debug.Log ("loading new room...");
		Vector3 oldposition = room.transform.localPosition;
		Vector3 newposition = new Vector3 (oldposition.x - 60, oldposition.y, oldposition.z);
		Instantiate (room, newposition, Quaternion.identity);
	}

}
