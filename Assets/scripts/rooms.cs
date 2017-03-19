using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

//Not yet generalized to junctionrooms! Junctionrooms (just the special part, child of "10 junction with small" etc) presently contain no room script. Need to be added. 
public class rooms : MonoBehaviour {

	GameObject player;
	Transform exit;
	Transform entrance;
	public JsonData mynormposts = new JsonData();
	public JsonData mysmallposts = new JsonData();
	public JsonData mylargeposts = new JsonData();
	public int normalrenderindex, smallrenderindex, largerenderindex;


	void shittyadd (int index, int size) //no elegant way of specifying ranges within JsonData object - have to iteratively add all posts to this room's post list. Can't loop in case, has to be in fxn.
	{
		int count; //used for console output only, ignore
		for (int j = index; j < index + size; j++) { 
			mynormposts.Add (transform.GetComponentInParent<floorbehavior> ().normalposts[j]);
			count = mynormposts.Count;
			//Debug.Log ("Adding post by " + mynormposts [count-1] ["blog_name"] + " to normposts");
		}
		transform.GetComponentInParent<floorbehavior> ().normpostindex = index + size;
	}

	void shittyaddsmall (int index)
	{
		mysmallposts.Add(transform.GetComponentInParent<floorbehavior>().smallspecialposts[index]);
		//Debug.Log ("Adding smallspecial by " + mysmallposts [0]["blog_name"] + " to smallposts");
		transform.GetComponentInParent<floorbehavior> ().smallpostindex = index + 1;
	}

	void shittyaddlarge (int index)
	{
		mylargeposts.Add(transform.GetComponentInParent<floorbehavior>().largespecialposts[index]);
		//Debug.Log ("Adding largespecial by " + mylargeposts [0]["blog_name"] + " to largeposts");
		transform.GetComponentInParent<floorbehavior> ().largepostindex = index + 1;
	}


	void Start () {
		entrance = transform.Find ("entrance");
		exit = transform.Find ("exit");
		player = GameObject.Find ("FirstPersonEthan");
		player.GetComponent<globalvars> ().newspawnpoint = exit.position;
		player.GetComponent<globalvars> ().newspawnrot = exit.rotation;
		normalrenderindex = 0;
		smallrenderindex = 0;
		largerenderindex = 0;
		int i = transform.GetComponentInParent<floorbehavior> ().normpostindex;
		int j = transform.GetComponentInParent<floorbehavior> ().largepostindex;
		int k = transform.GetComponentInParent<floorbehavior> ().smallpostindex;

		switch (this.name) {
		case "30(Clone)":
			shittyadd (i, 30);
			break;
		case "20(Clone)":
			shittyadd (i, 20);
			break;
		case "10(Clone)":
			shittyadd (i, 10);
			break;
		case "5(Clone)":
			shittyadd (i, 5);
			break;
		case "small 30 feature(Clone)":
			shittyadd (i, 30);
			shittyaddsmall (k);
			break;
		case "small 20 feature(Clone)":
			shittyadd (i, 20);
			shittyaddsmall (k);
			break;
		case "10 junction with small(Clone)":
			shittyadd (i, 10);
			shittyaddsmall (k);
			//Debug.Log ("I'm a junction room!");
			break;
		case "10 junction with large(Clone)":
			shittyadd (i, 10);
			shittyaddlarge (j);
			//Debug.Log ("I'm a junction room!");
			break;
		case "large(Clone)":
			shittyaddlarge (j);
			break;
		case "small(Clone)":
			shittyaddsmall (k);
			break;
		default:
			Debug.Log ("No case match!");
			break;
		}

	}

}