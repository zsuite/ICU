using UnityEngine;
using System.Collections;

public class CalibrationWait : MonoBehaviour {
	float timeSet = 0;
	float timeSound = 0;
	public float timeWait;
	public GameObject blinkCounter;
	public GameObject blinkProjecter;
	public GameObject audioPlay;
	public GameObject blinkingTrack;

	// Use this for initialization
	void Start () {
		timeSet = Time.time + timeWait;
	}
	
	// Update is called once per frame
	void Update () {
		if(timeWait != 0 && Time.time > timeSet){
			blinkingTrack.SetActive (true);
			blinkCounter.SetActive (true);
			blinkProjecter.SetActive (true);
			audioPlay.SetActive (true);
		
				
			}
	}
}
