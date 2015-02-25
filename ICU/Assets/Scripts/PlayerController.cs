using UnityEngine;
using System.Collections;

public class PlayerController : Controller {
	public Transform target;
	public Transform cameraHold;
	public FaceTrackingManager faceCam;
	public float maxLeftLook = -30;
	public float maxRightLook = 30;
	private float yVelocity = 0.0F;
	MouseLook[] disableMouse;
	// Use this for initialization
	void Start () {
		StateManager.Dead = false;
		disableMouse = GetComponentsInChildren<MouseLook>();
		GetComponent<MouseLook>().enabled = true;
		GetComponent<CharacterController>().enabled = true;
		Screen.showCursor = false;
		foreach (MouseLook mouse in disableMouse) {	
			mouse.enabled = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.R)){
			Application.LoadLevel(Application.loadedLevelName);
		}
		if(Input.GetKeyDown(KeyCode.Escape)){
			Screen.showCursor = !Screen.showCursor;
		}

		if(StateManager.Dead == true){
			BeginTurn();
		}
		if(faceCam.EyesMoveLeftRight <=-20 || faceCam.EyesMoveLeftRight >=20){
			float smoothMoveLR =  Mathf.Round(faceCam.EyesMoveLeftRight);
			float newRotation =  Mathf.SmoothDampAngle(cameraHold.localEulerAngles.y, smoothMoveLR, ref yVelocity, .7f);

			cameraHold.localEulerAngles = new Vector3(cameraHold.localEulerAngles.x, newRotation,0);
		}
		else if(faceCam.EyesMoveLeftRight >-20 || faceCam.EyesMoveLeftRight <20){
			float smoothMoveLR =  Mathf.Round(faceCam.EyesMoveLeftRight);
			float newRotation =  Mathf.SmoothDampAngle(cameraHold.localEulerAngles.y, 0, ref yVelocity, .4f);

			cameraHold.localEulerAngles = new Vector3(cameraHold.localEulerAngles.x, newRotation,0);
		}
	}

	void BeginTurn(){
		Vector3 relativePos = target.position - cameraHold.position;
		Quaternion rotationT = Quaternion.LookRotation(relativePos);
		GetComponent<MouseLook>().enabled = false;
		GetComponent<CharacterController>().enabled = false;
	foreach (MouseLook mouse in disableMouse) {	
			mouse.enabled = false;
		}
			cameraHold.rotation = Quaternion.Slerp (cameraHold.rotation ,rotationT, 0.1f);
	}
}
