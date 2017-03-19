using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class frames : MonoBehaviour {

	private int index;
	private JsonData mypost = new JsonData();
	private string url;
	private double width;
	private double height;

	// Use this for initialization
	void Start () {
		
	}
	
	IEnumerator render () {
		index = transform.GetComponentInParent<rooms>().normalrenderindex;
		transform.GetComponentInParent<rooms> ().normalrenderindex ++;
		mypost = transform.GetComponentInParent<rooms> ().mynormposts [index]; //contains all the content from the original post. Can query for anything, just don't forget typecasts are necessary.
		url = (string)mypost ["photos"] [0] ["alt_sizes"] [1] ["url"];
		width = (int)mypost ["photos"] [0] ["alt_sizes"] [1] ["width"];
		height = (int)mypost ["photos"] [0] ["alt_sizes"] [1] ["height"];
		Debug.Log("adding " + url + " to picture of size " + width + " by " + height);
		WWW www = new WWW (url); //retrieves image from url stored in Json
		yield return www;
		Vector3 scale = transform.localScale; //roundabout but required, won't compile unless changes made in a temporary variable THEN applied to localscale as vec3
		scale.z = (float) height / 1000;
		scale.x = (float) width / 1000;
		transform.localScale = scale;
		Renderer renderer = GetComponent<Renderer> ();
		renderer.material.mainTexture = www.texture;
	}
}

