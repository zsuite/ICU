using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AudioTrigger : MonoBehaviour {
	bool audioBegan = false;
	int audioIndex = 0;
	float audioTimer = 0;
	[System.Serializable]
	public class AudioItem
	{
		public string audioName;
		public GameObject gameObjectToPlayOn;
		public float waitSecondsToPlayNextTrack;

	}
	public List<AudioItem> AudioList;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(audioBegan && audioIndex < AudioList.Count){
			audioTimer+= Time.deltaTime;
			ContinueAudioExecution();
		}
	}
	public void BeginAudioExecution(){
		audioBegan = true;

		PlayEvent(AudioList[audioIndex].audioName, AudioList[audioIndex].gameObjectToPlayOn);
	}
	public void ContinueAudioExecution(){
		if(audioTimer >= AudioList[audioIndex].waitSecondsToPlayNextTrack){
			PlayEvent(AudioList[audioIndex].audioName, AudioList[audioIndex].gameObjectToPlayOn);
			audioIndex++;
			audioTimer = 0f;
		}
	}

	public void PlayEvent(string eventName, GameObject objectToPlayOn){
		AkSoundEngine.PostEvent (eventName, objectToPlayOn);
	}
}
