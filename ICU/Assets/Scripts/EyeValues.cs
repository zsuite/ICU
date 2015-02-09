using UnityEngine;
using System.Collections;

[RequireComponent(typeof(FaceTrackingManager))]
public class EyeValues : MonoBehaviour {
	[SerializeField]
	private int smoothLeftEye;		//0 = EYE IS OPEN
	[SerializeField]
	private int smoothRightEye;		//100 = EYE IS CLOSED

	
	public static bool playerBlinked;

	public int blinkTriggerIndex = 50;	//The value when playerBlinked becomes true
	
	private FaceTrackingManager realCam;

	void Start () {
		realCam = gameObject.GetComponent<FaceTrackingManager>();
	}
	
	void Update () {

		smoothLeftEye = Mathf.RoundToInt(realCam.LeftEyeClose); // Rounds float values to integers
		smoothRightEye = Mathf.RoundToInt(realCam.RightEyeClose);

		playerBlinked = CheckEyeClosure();

	}

	bool CheckEyeClosure(){
		if ((smoothLeftEye >= blinkTriggerIndex || smoothRightEye >= blinkTriggerIndex)) {	
			return true;
		} 
		else {
			return false;
		}
	}
}
