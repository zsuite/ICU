using UnityEngine;
using System.Collections;

public class BlinkAnimationController : MonoBehaviour {
	bool blinkOnce = true;
	public GameObject blinkQuad;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (EyeValues.playerBlinked && blinkOnce) {
			blinkQuad.SetActive (true);
			Invoke("EndBlink", 0.05f);
			blinkOnce = false;
		}

		else if (!EyeValues.playerBlinked) {
			blinkOnce = true;
		}

	}

	void EndBlink(){
		blinkQuad.SetActive (false);
	}
}
