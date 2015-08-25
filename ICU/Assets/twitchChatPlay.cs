using UnityEngine;
using System.Collections;

public class twitchChatPlay : MonoBehaviour {
	public MovieTexture twitchPlay;
	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().material.mainTexture = twitchPlay as MovieTexture;
		twitchPlay.Play ();
		twitchPlay.loop = true;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
