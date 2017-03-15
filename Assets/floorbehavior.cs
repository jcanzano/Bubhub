using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class floorbehavior : MonoBehaviour {

	string url = "http://api.tumblr.com/v2/blog/squinners.tumblr.com/likes?api_key=x3pIKyCJKLugGNcHQp8beCB7aGMFPyLdg9fRajsU9n4hbhiLc0&limit=50&offset=";
	int postoffset;
	public GameObject player;
	int smallthreshold = 6;
	int largethreshold = 10;
	public int normpostindex = 0;
	public int largepostindex = 0;
	public int smallpostindex = 0;
	public JsonData normalposts = new JsonData();
	public JsonData smallspecialposts = new JsonData();
	public JsonData largespecialposts = new JsonData();


	void Start () {
		player = GameObject.Find ("FirstPersonEthan");
		postoffset = player.GetComponent<globalvars>().postoffset; 
		Debug.Log ("starting from post " + postoffset);
		StartCoroutine(tumblrget(postoffset));
	}
		
	public static List<string> Shuffle (List<string>aList) {
		System.Random _random = new System.Random ();
		string myGO;
		int n = aList.Count;
		for (int i = 0; i < n; i++)
		{
			// NextDouble returns a random number between 0 and 1.
			// ... It is equivalent to Math.random() in Java.
			int r = i + (int)(_random.NextDouble() * (n - i));
			myGO = aList[r];
			aList[r] = aList[i];
			aList[i] = myGO;
		}
		return aList;
	}

	IEnumerator tumblrget(int postoffset) 
	{
		//makes three consecutive requests as 50 is max posts retrievable and too small for a whole floor 
		int tempoffset = postoffset + 50;
		int tempoffset2 = postoffset + 100;
		Debug.Log ("Sending requests to tumblr...");
		WWW www = new WWW (url + postoffset.ToString()); //www object contains only raw json
		yield return www; //waits for response before proceeding
		Debug.Log ("Request 1 returned");
		WWW www1 = new WWW (url + tempoffset.ToString());
		yield return www1;
		Debug.Log ("Request 2 returned");
		WWW www2 = new WWW (url + tempoffset2.ToString ());
		yield return www2;
		Debug.Log ("Request 3 returned");

		//parses pages into one json object containing all posts (unfortunately no elegant means of concatenating this type...)
		JsonData data = JsonMapper.ToObject (www.text); //maps json to nested jsondata object. posts are stored in ["response"]["liked_posts"][i]
		JsonData data1 = JsonMapper.ToObject (www1.text);
		JsonData data2 = JsonMapper.ToObject (www2.text);
		JsonData posts = data["response"]["liked_posts"]; //new nested array of only the posts, now stored in [i]. Makes iteration simpler to code. Must cast type when using.  
		for (int i = 0; i < 50; i++) {
			posts.Add (data1 ["response"] ["liked_posts"][i]);
		}
		for (int i = 0; i < 50; i++) {
			posts.Add (data2["response"]["liked_posts"][i]);
		}
		Debug.Log ("Received " + posts.Count + " raw posts");
		player.GetComponent<globalvars> ().postoffset = postoffset + 150;
		Debug.Log ("New postoffset is " + player.GetComponent<globalvars> ().postoffset);


		//iterates through posts and parses into separate jsondata lists based on number of photos in post according to set thresholds
		//use .Count for length of JsonData object or lists
		//normalposts = new JsonData();
		//smallspecialposts = new JsonData ();
		//largespecialposts = new JsonData ();
		bool smallexists = false;
		bool largeexists = false;
		for (int i = 0; i < posts.Count; i++) 
		{
			if ((string) posts[i]["type"] == "photo") { //limit to photo posts for now
				string a = (string)posts [i] ["photos"] [0] ["alt_sizes"] [2] ["url"];

				if (! a.Contains("gif")) { //no gif support
					int photos = posts [i] ["photos"].Count;

					if (photos < smallthreshold) {
						normalposts.Add (posts [i]);
					}

					if (photos >= smallthreshold && photos < largethreshold) {
						smallspecialposts.Add (posts [i]);
						smallexists = true;
					}

					if (photos >= largethreshold) {
						largespecialposts.Add (posts [i]);
						largeexists = true;
					}

				}
			}
		}

		//builds roomlist based on post distribution
		int normcount = normalposts.Count;
		int smallcount = 0;
		int largecount = 0;
		if (smallexists) { //necessary workaround as .Count trips an error if called on empty Jsondata object
			smallcount = smallspecialposts.Count;
		} 
		if (largeexists) {
			largecount = largespecialposts.Count;
		} 
		Debug.Log (normcount + " photo posts stored in normalposts");
		Debug.Log (smallcount + " photo posts stored in smallspecialposts");
		Debug.Log (largecount + " photo posts stored in largespecialposts");

		//very primitive builder. requires tuning and kind of assumes equal specialpost distribution from all feeds. needs more testing.
		Debug.Log ("Building floor...");
		List<string> roomlist = new List<string> ();

		if (smallcount > 0 && normcount >= 30) {
			roomlist.Add ("small 30 feature");
			normcount = normcount - 30;
			smallcount--;
		}
		if (smallcount > 0 && normcount >= 20) {
			roomlist.Add ("small 20 feature");
			normcount = normcount - 20;
			smallcount--;
		}
		if (smallcount > 0 && normcount >= 10) {
			roomlist.Add ("10 junction with small");
			normcount = normcount - 10;
			smallcount--;
		}
		if (largecount > 0 && normcount >= 10) {
			roomlist.Add ("10 junction with large");
			normcount = normcount - 10;
			largecount--;
		}
		//multiple rooms of various sizes in case a bunch of normposts are left. Sums to about 100 for now.
		if (normcount > 30) {
			roomlist.Add ("30");
			normcount = normcount - 20;
		}
		if (normcount > 20) {
			roomlist.Add ("20");
			normcount = normcount - 20;
		}
		if (normcount > 20) {
			roomlist.Add ("20");
			normcount = normcount - 20;
		}
		if (normcount > 10) {
			roomlist.Add ("10");
			normcount = normcount - 10;
		}
		if (normcount >= 10) {
			roomlist.Add ("10");
			normcount = normcount - 10;
		}
		if (normcount >= 5) {
			roomlist.Add ("5");
			normcount = normcount - 5;
		}
		 //tops off with any remaining large and small - these will get their own hallways or rooms
		for (int i = 0; i < largecount; i++) {
			roomlist.Add ("large");
		}
		for (int i = 0; i < smallcount; i++) {
			roomlist.Add ("small");
		}


		//outputs contents of roomlist and post counts for debugging purposes
		string roomoutput1 = "Raw roomlist: ";
		for (int i = 0; i < roomlist.Count; i++) {
			roomoutput1 = roomoutput1 + roomlist[i] + ", ";
		}
		Debug.Log (roomoutput1);
		Debug.Log ("remaining normcount " + normcount);


		//shuffles, outputs again, then then iterates through roomlist calling rooms one at a time. Room names above must be exactly matching prefab names and prefabs must be saved in Resources.
		List<string> shuffledrooms = Shuffle(roomlist);
		string roomoutput2 = "Shuffled roomlist :";
		for (int i = 0; i < shuffledrooms.Count; i++) {
			roomoutput2 = roomoutput2 + shuffledrooms[i] + ", ";
		}
		Debug.Log (roomoutput2);

		for (int i = 0; i < shuffledrooms.Count; i++) {
			Debug.Log ("Normpost index: " + normpostindex + "  Smallpost index: " + smallpostindex + "  Largepost index: " + largepostindex);
			var newroom1 = (GameObject)Instantiate (Resources.Load (shuffledrooms [i]), player.GetComponent<globalvars> ().newspawnpoint, player.GetComponent<globalvars> ().newspawnrot, gameObject.transform);
			yield return newroom1; //waits for start() to complete in room, ensures newspawnpoint has been updated before adding more rooms or rendering anything
			newroom1.BroadcastMessage("render"); //tells all frames in newroom to make tumblr requests now
			if (shuffledrooms[i].Contains("junction")) {
				if (shuffledrooms[i].Contains("small")) {
					var newjxn = (GameObject) Instantiate (Resources.Load ("smallnoexit"), newroom1.transform.Find("exit 2").position, newroom1.transform.Find("exit 2").rotation, newroom1.transform);
					yield return newjxn; //waits for junctionroom to load
					newjxn.BroadcastMessage("render"); //previous render call doesn't include junctionrooms as they weren't made yet! Must call again for these but ONLY FOR THIS ROOM
				}
				if (shuffledrooms[i].Contains("large")) {
					var newjxn = (GameObject) Instantiate (Resources.Load ("largenoexit"), newroom1.transform.Find("exit 2").position, newroom1.transform.Find("exit 2").rotation, newroom1.transform);
					yield return newjxn;
					newjxn.BroadcastMessage ("render");
				}
			}
		}


		var newele = (GameObject) Instantiate (Resources.Load ("elevator"), player.GetComponent<globalvars> ().newspawnpoint, player.GetComponent<globalvars> ().newspawnrot, gameObject.transform);
		yield return newele;
			
		//var newroom1 = (GameObject)Instantiate(Resources.Load("small 30 feature"), player.GetComponent<globalvars>().newspawnpoint, player.GetComponent<globalvars>().newspawnrot, gameObject.transform);
		//yield return newroom1;
		//player.GetComponent<globalvars> ().newspawnpoint = newroom1.transform.Find ("exit").transform.position;  use if room is too slow updating the position... seems fine for now
		//player.GetComponent<globalvars> ().newspawnrot = newroom1.transform.Find ("exit").transform.rotation;
		//var newroom2 = (GameObject)Instantiate(Resources.Load("small 30 feature"), player.GetComponent<globalvars>().newspawnpoint, player.GetComponent<globalvars>().newspawnrot, gameObject.transform);

	}

}
