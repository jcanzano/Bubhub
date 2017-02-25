using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class roombehavior : MonoBehaviour {

	public GameObject player;
	string url = "http://api.tumblr.com/v2/blog/squinners.tumblr.com/likes?api_key=x3pIKyCJKLugGNcHQp8beCB7aGMFPyLdg9fRajsU9n4hbhiLc0&limit=50&offset=";
	int postoffset;
	public int renderindex;
	public List<Img> images = new List<Img>();

	// Use this for initialization
	void Start () {
		postoffset = player.GetComponent<Postoffset>().postoffset; 
		Debug.Log ("starting from post " + postoffset);
		renderindex = 0;
		StartCoroutine(tumblrget(postoffset));
	}

	public class Img {
		public string url { get; set; }
		public int width { get; set; }
		public int height { get; set; }
	}

	IEnumerator tumblrget(int postoffset)
	{
		Debug.Log ("Sending request to tumblr...", this.gameObject);
		WWW www = new WWW (url + postoffset.ToString()); //www object contains only raw json
		yield return www; //waits for response before proceeding
		Debug.Log ("Request received", this.gameObject);
		JsonData data = JsonMapper.ToObject (www.text); //maps json to nested array object. posts are stored in ["response"]["liked_posts"][i]
		JsonData posts = data["response"]["liked_posts"]; //new nested array of only the posts, now stored in [i]. Makes iteration simpler to code. Must cast types when using.  

		//iterates through posts and adds 400x600 image url to "images" list if post is of type "photo" (should add "answer" type later)
		//use .Count for length of JsonData object or lists
		for (int i = 0; i < posts.Count; i++) 
		{
			if ((string) posts[i]["type"] == "photo") //limit to photo posts
			{
				string a = (string)posts [i] ["photos"] [0] ["alt_sizes"] [2] ["url"];
				if (! a.Contains("gif")) //no .gif support yet
					{ 
					images.Add (new Img {
						url = (string)posts [i] ["photos"] [0] ["alt_sizes"] [2] ["url"],
						width = (int)posts [i] ["photos"] [0] ["alt_sizes"] [2] ["width"],
						height = (int)posts [i] ["photos"] [0] ["alt_sizes"] [2] ["height"]
					});
				}
			}
		}
		Debug.Log (images.Count + " liked posts saved");
		BroadcastMessage ("render");
		player.GetComponent<Postoffset> ().postoffset = postoffset + images.Count;
	}
}
