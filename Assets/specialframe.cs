using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class specialframe : MonoBehaviour {

	private int index;
	private JsonData mypost = new JsonData();
	private string url;
	private double width;
	private double height;
	private string daddy;

	// Use this for initialization
	void Start () {

	}

	IEnumerator render () {
		daddy = transform.parent.name;

		if (daddy.Contains ("noexit")) { //special rooms attached to junctions have to go up two levels to find their special posts
			if (daddy.Contains ("small")) {
				index = transform.parent.transform.GetComponentInParent<rooms> ().smallrenderindex;
				transform.parent.transform.GetComponentInParent<rooms> ().smallrenderindex++;
				mypost = transform.parent.transform.GetComponentInParent<rooms> ().mysmallposts [index]; //contains all original content from one post. Can query for anything, just don't forget typecasts are necessary.
				Debug.Log ("added specialpost to small junction room from url " + mypost ["photos"] [0] ["alt_sizes"] [1] ["url"]);
			}

			if (daddy.Contains ("large")) {
				index = transform.parent.transform.GetComponentInParent<rooms> ().largerenderindex;
				transform.parent.transform.GetComponentInParent<rooms> ().largerenderindex++;
				mypost = transform.parent.transform.GetComponentInParent<rooms> ().mylargeposts [index];
				Debug.Log ("added specialpost to large junction room from url " + mypost ["photos"] [0] ["alt_sizes"] [1] ["url"]);
			}
		} else { //all other room types start here
			if (daddy.Contains ("small")) { 
				index = transform.GetComponentInParent<rooms> ().smallrenderindex;
				transform.GetComponentInParent<rooms> ().smallrenderindex++;
				mypost = transform.GetComponentInParent<rooms> ().mysmallposts [index];  //contains all original content from one post. Can query for anything, just don't forget typecasts are necessary.
			}

			if (daddy.Contains ("large")) {
				index = transform.GetComponentInParent<rooms> ().largerenderindex;
				transform.GetComponentInParent<rooms> ().largerenderindex++;
				mypost = transform.GetComponentInParent<rooms> ().mylargeposts [index];
			}
		}
			


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
