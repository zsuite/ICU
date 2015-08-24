using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public Transform monsterHead;
	public Transform cameraHold;
	public FaceTrackingManager faceCam;
	public float maxLeftLook = -30;
	public float maxRightLook = 30;
	private float yVelocity = 0.0F;
	MouseLook[] disableMouse;
	Vector3 directionOfInterest;
	bool turnBegan = false;
	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		StateManager.Dead = false;
		disableMouse = GetComponentsInChildren<MouseLook>();
		GetComponent<CharacterController>().enabled = true;
		Cursor.visible = false;
		foreach (MouseLook mouse in disableMouse) {	
			mouse.enabled = true;
		}
	}

	// Update is called once per frametar
	void Update () {

		directionOfInterest = monsterHead.position - cameraHold.position;
		directionOfInterest = directionOfInterest.normalized;
		if (turnBegan){
			Quaternion oldRot = Camera.main.transform.rotation;
			Camera.main.transform.LookAt(monsterHead.position) ;
			Quaternion newRot = Camera.main.transform.rotation ;
			Camera.main.transform.rotation = Quaternion.Lerp(oldRot , newRot, 3f * Time.deltaTime) ;

			//cameraHold.rotation = Quaternion.Slerp(rotationC, rotationT, -0.1f);
/*			Camera.main.transform.rotation = Quaternion.Slerp(cameraHold.forward, directionOfInterest, 1f * Time.deltaTime);
*/			//Quaternion.Lerp()
			//cameraHold.up = monsterHead.up;
		}



		if(StateManager.Dead == true){
			BeginTurn();
		}
		if(StateManager.Pause == true){
			PausePlayer();
		}
		else if (StateManager.Dead == false){
			UnPausePlayer();
		}
		/*if(faceCam.EyesMoveLeftRight <=-15 || faceCam.EyesMoveLeftRight >=15){
			float smoothMoveLR =  Mathf.Round(faceCam.EyesMoveLeftRight);
			float newRotation =  Mathf.SmoothDampAngle(cameraHold.localEulerAngles.y, smoothMoveLR, ref yVelocity, .7f);

			cameraHold.localEulerAngles = new Vector3(cameraHold.localEulerAngles.x, newRotation,0);
		}
		else if(faceCam.EyesMoveLeftRight >-15 || faceCam.EyesMoveLeftRight <15){
			float smoothMoveLR =  Mathf.Round(faceCam.EyesMoveLeftRight);
			float newRotation =  Mathf.SmoothDampAngle(cameraHold.localEulerAngles.y, 0, ref yVelocity, .4f);
			if(StateManager.Dead == false){
				cameraHold.localEulerAngles = new Vector3(cameraHold.localEulerAngles.x, newRotation,0);
			}
		}*/
	}

	void BeginTurn(){

/*		Quaternion rotationQ = Quaternion.Inverse(rotationT) ;
*/		//transform.LookAt();
		//GetComponent<MouseLook>().enabled = false;
		GetComponent<CharacterController>().enabled = false;
	foreach (MouseLook mouse in disableMouse) {	
			mouse.enabled = false;
		}
		turnBegan = true;
/*/*			cameraHold.rotation = Quaternion.Slerp (cameraHold.rotation ,rotationT, 0.1f);
*/	}
	void PausePlayer(){
		GetComponent<CharacterController>().enabled = false;
		foreach (MouseLook mouse in disableMouse) {	
			mouse.enabled = false;
		}
	}
	void UnPausePlayer(){
		GetComponent<CharacterController>().enabled = true;
		foreach (MouseLook mouse in disableMouse) {	
			mouse.enabled = true;
		}
	}
}
