using UnityEngine;
using System.Collections;

public class WarningPlay : MonoBehaviour {
	public MovieTexture movieStart;
	// Use this for initialization		timeSet = Time.time + afterSeconds;
	//public float timeTransition;
//	float timeSet = 0;
	void Start () {
		if(GetComponent<Renderer>() != null){
			GetComponent<Renderer>().material.mainTexture = movieStart as MovieTexture;
		}
		else{
			GetComponent<Projector>().material.mainTexture = movieStart as MovieTexture;

		}
		//((MovieTexture)GetComponent<Renderer> ().material.mainTexture).Play ();
		movieStart.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		//timeSet = Time.time + timeTransition;
		if (!movieStart.isPlaying){
			Destroy (gameObject, 0f);
	}
		//if(timeTransition != 0 && Time.time > timeSet){
			//activateReady = true;
			//DoActivateTrigger();
		//}
}
}
