using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(FaceTrackingManager))]
public class EyeValues : MonoBehaviour {
	[SerializeField]
	private float smoothLeftEye;		//0 = EYE IS OPEN
	[SerializeField]
	private float smoothRightEye;		//100 = EYE IS CLOSED

	public static bool playerBlinked;
	public static int numberOfBlinks = 0;

	public static bool playerSmiled;

	public float blinkTriggerIndexMax = 100;	//The value when playerBlinked becomes true // Eyes are efffectively closed


	private FaceTrackingManager realCam;
	bool blinkOnce = true;

	Vector3 facePos;
	/*List<int> positionsListL;
	List<int> positionsListR;

	int positionsIndexL;
	int positionsIndexR;
*/
	void Start () {
		//Application.targetFrameRate = 30;
		/*positionsIndexL = 0;
		positionsIndexR = 0;
		positionsListL = new List<int>();
		positionsListR = new List<int>();*/
		realCam = gameObject.GetComponent<FaceTrackingManager>();
	}
	
	void Update () {
		/*//LastEyeValueL();
		//LastEyeValueR();
		deltaLeftEye = smoothLeftEye - lastEyePositionL;
		deltaRightEye = smoothRightEye - lastEyePositionR;
*/
		smoothLeftEye = realCam.LBlink; // Rounds float values to integers
		smoothRightEye = realCam.RBlink;
//		facePos = realCam.currentHeadPose;
//		leftBlinkZero = realCam.LLowLidUp;
//		rightBlinkZero = realCam.RLowLidUp;
		if(realCam.LMouthSmile > 50f || realCam.RMouthSmile > 50f || realCam.LMouthSmileCorrect > 50 || realCam.RMouthSmileCorrect > 50){
			playerSmiled = true;
		}
		else if(Input.GetKeyDown(KeyCode.H))
		{
			playerSmiled = true;
		}
		else{
			playerSmiled = false;
		}
		CheckEyeClosure();
	}

	void LateUpdate(){

	}

	void CheckEyeClosure(){
		if (blinkOnce && (smoothLeftEye >= blinkTriggerIndexMax || smoothRightEye >= blinkTriggerIndexMax) ) {	
			numberOfBlinks++;
			MetricManagerScript.AddPosition(Camera.main.transform.position);
			MetricManagerScript.AddDirection(Camera.main.transform.rotation);
			if(MetricManagerScript.somethingHappened.Count < MetricManagerScript.locationMetrics.Count){
				MetricManagerScript.AddBlinkNumber(1);
				MetricManagerScript.DidSomethingHappen(false);
			}
			blinkOnce = false;
		} 
		if (smoothLeftEye >= blinkTriggerIndexMax || smoothRightEye >= blinkTriggerIndexMax ) {	
			playerBlinked = true;
		} 

		else if (Input.GetKeyDown(KeyCode.B))
		{
			numberOfBlinks++;
			playerBlinked = true;
		}
		else {
			blinkOnce = true;
			playerBlinked = false;
		}
	}


}
