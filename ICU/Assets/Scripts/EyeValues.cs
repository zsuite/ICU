using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(FaceTrackingManager))]
public class EyeValues : MonoBehaviour {
	[SerializeField]
	private float smoothLeftEye;		//0 = EYE IS OPEN
	[SerializeField]
	private float smoothRightEye;		//100 = EYE IS CLOSED

	[SerializeField]
	private float leftBlinkZero;		/* Eye changed*/ //Grabbed from LLidUp
	[SerializeField]
	private float rightBlinkZero;		//EYE changed// RLidUp, idk what it means


	[SerializeField]
	private float lastEyePositionL;

	[SerializeField]
	private float lastEyePositionR;

	[SerializeField]
	private float deltaLeftEye; // change between current eye position and previous eye position .33 seconds ago.

	[SerializeField]
	private float deltaRightEye; // change between current eye position and previous eye position .33 seconds ago.

	 float blinkDeltaIndex = 10; // this is the delta that will trigger a blink

	 float blinkDeltaTriggerMin = 40; // at this point the delta of your eye values will matter

	public static bool playerBlinked;

	public float blinkTriggerIndexMax = 80;	//The value when playerBlinked becomes true // Eyes are efffectively closed


	private FaceTrackingManager realCam;

	List<int> positionsListL;
	List<int> positionsListR;

	int positionsIndexL;
	int positionsIndexR;

	void Start () {
		positionsIndexL = 0;
		positionsIndexR = 0;
		positionsListL = new List<int>();
		positionsListR = new List<int>();
		realCam = gameObject.GetComponent<FaceTrackingManager>();
	}
	
	void Update () {
		//LastEyeValueL();
		//LastEyeValueR();
		deltaLeftEye = smoothLeftEye - lastEyePositionL;
		deltaRightEye = smoothRightEye - lastEyePositionR;

		smoothLeftEye = realCam.LBlink; // Rounds float values to integers
		smoothRightEye = realCam.RBlink;

		leftBlinkZero = realCam.LLowLidUp;
		rightBlinkZero = realCam.RLowLidUp;

		playerBlinked = CheckEyeClosure();

	}

	bool CheckEyeClosure(){
		if (smoothLeftEye >= blinkTriggerIndexMax  && leftBlinkZero <= 1|| smoothRightEye >= blinkTriggerIndexMax && rightBlinkZero <= 1) {	
			return true;
		} 
		/*else if ( (deltaLeftEye > blinkDeltaIndex) && (smoothLeftEye >= blinkDeltaTriggerMin)){
			return true;

		}
		else if ( (deltaRightEye > blinkDeltaIndex) && (smoothRightEye >= blinkDeltaTriggerMin)){
			return true;
			
		}*/
		else if (Input.GetKeyDown(KeyCode.B))
		{
			return true;
		}
		else {
			return false;
		}
	}

	/*void LastEyeValueL()
	{
		if(positionsIndexL > positionsListL.Count - 1)
		{
			positionsIndexL = positionsListL.Count;
		}
		if(positionsListL.Count > 3 && positionsIndexL > 0){
			positionsIndexL--;
			lastEyePositionL = positionsListL[0];
			positionsListL.RemoveAt(0);
		}
		else{
			positionsIndexL++;
			positionsListL.Add(smoothLeftEye);
		}

	}
	void LastEyeValueR()
	{
		if(positionsIndexR > positionsListR.Count - 1)
		{
			positionsIndexR = positionsListR.Count;
		}
		if(positionsListR.Count > 3 && positionsIndexR > 0){
			positionsIndexR--;
			lastEyePositionR = positionsListR[0];
			positionsListR.RemoveAt(0);
		}
		else{
			positionsIndexR++;
			positionsListR.Add(smoothRightEye);
		}
		
	}*/
}
