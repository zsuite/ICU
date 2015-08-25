using UnityEngine;
using System.Collections;

public class WarningContinue : MonoBehaviour {
	public MovieTexture movieContinue;
	// Use this for initialization
	//timeSet = Time.time + timeTransitions;
	public float timeTransition;
	public float continueSound;
	float timeSet = 0;
	float timeSound = 0;
	bool timeGo;

	void Start () {
	GetComponent<Renderer>().material.mainTexture = movieContinue as MovieTexture;
		//movieContinue.Play ();
		movieContinue.loop = true;
		timeSet = Time.time + timeTransition;
		timeGo = false;

	}
	
	// Update is called once per frame
	void Update () {
		//timeSet = Time.time + timeTransition;
		if(timeTransition != 0 && Time.time > timeSet){
			movieContinue.Play();
			movieContinue.loop = true;
			if (Input.anyKey && timeGo == false){
				timeSound = Time.time + continueSound;
				//timeSound = Time.time + continueSound;
				ContinueEvent("play_sx_menumain_warning_accept");
				Debug.Log("any Key");
				timeGo = true;

			}
			if (continueSound !=0 && Time.time > timeSound && timeGo == true){
				Application.LoadLevel("Calibration_1");
			}
		}
	//	if (!movieContinue.isPlaying) {
			//movieContinue.Play ();
	//	}
	}
	public void ContinueEvent (string play_sx_menumain_warning_accept){
		AkSoundEngine.PostEvent (play_sx_menumain_warning_accept, gameObject);
	}
}
