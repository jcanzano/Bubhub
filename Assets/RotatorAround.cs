using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorAround : MonoBehaviour {

	public Vector3 rotationAxis;
	public float speed = 1.0f;
	private Vector3 rotationPoint;

	// Use this for initialization
	void Start () {
		rotationPoint = this.transform.parent.transform.position;
	}

	// Update is called once per frame
	void Update () {
		transform.RotateAround (rotationPoint, rotationAxis, speed*Time.deltaTime);
	}
}
