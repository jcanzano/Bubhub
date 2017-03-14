using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour {

	public roombehavior room;

//	void Start()
//	{
//		room = this.transform.parent.GetComponent<roombehavior> ();
//	}

	void OnTriggerEnter(Collider other)
	{
		room.CmdCreateRoom();
		room.RpcRemoveTriggerPlane();
	}

}
