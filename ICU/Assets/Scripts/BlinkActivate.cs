using UnityEngine;
using System.Collections;

public class BlinkActivate : MonoBehaviour {


	// Use this for initialization
	bool blinkOnce;
	// Use this for initialization
	void Start () {
		blinkOnce = true;
		//blinkQuad.SetActive(false);

	}
	
	// Update is called once per frame
	void OnTriggerStay(Collider other) {
		if(EyeValues.playerBlinked && blinkOnce){
			//blinkQuad.SetActive(true);

			GetComponent<ActivateTrigger>().enabled = true;
			blinkOnce =false;
			
		}
		if (!EyeValues.playerBlinked){

			blinkOnce = true;
		}
		
	}
}
