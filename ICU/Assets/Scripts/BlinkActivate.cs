using UnityEngine;
using System.Collections;

public class BlinkActivate : MonoBehaviour {

	public ActivateTrigger[] activateScripts;
	// Use this for initialization
	bool blinkOnce;
	// Use this for initialization
	void Start () {
		blinkOnce = true;
		//blinkQuad.SetActive(false);
		activateScripts = GetComponents<ActivateTrigger>();
	}
	
	// Update is called once per frame
	void OnTriggerStay(Collider other) {
		if(EyeValues.playerBlinked && blinkOnce){
			//blinkQuad.SetActive(true);
			foreach(ActivateTrigger actT in activateScripts)
			{
				actT.enabled = true;
			}
			blinkOnce =false;
			
		}
		if (!EyeValues.playerBlinked){

			blinkOnce = true;
		}
		
	}
}
