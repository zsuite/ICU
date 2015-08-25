 using UnityEngine;
using System.Collections;

public class blinkNumber : MonoBehaviour {
	public GameObject projector;
	public GameObject projectorScreen;
	public GameObject wall;
	public GameObject MonsterActivate;
	public GameObject projectortrigger1;
	public GameObject LightReveal;
	public GameObject exitVO;
	// Use this for initialization
	void Start () {
		EyeValues.numberOfBlinks = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (EyeValues.numberOfBlinks == 6) {
			projector.SetActive (false);
			projectortrigger1.SetActive (false);
			projectorScreen.SetActive (false);
			wall.SetActive (false);
			PlayEvent("stop_sx_calibration_projector");
			exitVO.SetActive (true);
		}
		if (EyeValues.numberOfBlinks == 7) {
			MonsterActivate.SetActive (true);
		}
		if (EyeValues.numberOfBlinks == 8) {
			LightReveal.SetActive (true);
		}


	}
	public void PlayEvent (string stop_sx_calibration_projector){
		AkSoundEngine.PostEvent (stop_sx_calibration_projector, gameObject);
	}
}
