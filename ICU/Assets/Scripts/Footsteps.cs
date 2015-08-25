using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour {
	bool isWalking = false;
	bool playOnce = true;
	bool firstTime = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)){
			////if(!audio.isPlaying)
			isWalking = true;
		}
		else{
			//PauseEvent("play_sx_player_fs", 1);
			PlayEvent("stop_sx_player_fs");
			isWalking = false;
			playOnce = true;
		}

		if (isWalking && playOnce)
		{
				PlayEvent("play_sx_player_fs");
				firstTime = false;
			
			playOnce = false;
		}
		//elsec
			//audio.Pause();
	}
	public void PlayEvent (string eventName){
		AkSoundEngine.PostEvent (eventName, gameObject);
	}
	/*//public void PauseEvent (string eventName, int fadeout){
		//uint eventID;
		//eventID = AkSoundEngine.GetIDFromString (eventName);
		//AkSoundEngine.ExecuteActionOnEvent(eventID, AkActionOnEventType.AkActionOnEventType_Pause, gameObject, fadeout * 1000, AkCurveInterpolation.AkCurveInterpolation_Sine);
	//}*/
	//public void ResumeEvent (string eventName, int fadeout){
		//uint eventID;
		//eventID = AkSoundEngine.GetIDFromString (eventName);
		//AkSoundEngine.ExecuteActionOnEvent(eventID, AkActionOnEventType.AkActionOnEventType_Resume, gameObject, fadeout * 1000, AkCurveInterpolation.AkCurveInterpolation_Sine);
	//}
}
