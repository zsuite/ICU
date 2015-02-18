using UnityEngine;
using System.Collections;

public class PlayerController : Controller {
	public Transform target;
	MouseLook[] disableMouse;
	// Use this for initialization
	void Start () {
		StateManager.Dead = false;
		disableMouse = GetComponentsInChildren<MouseLook>();
		GetComponent<MouseLook>().enabled = true;
		GetComponent<CharacterController>().enabled = true;
		foreach (MouseLook mouse in disableMouse) {	
			mouse.enabled = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(StateManager.Dead == true){
			BeginTurn();
		}
	}

	void BeginTurn(){
		Vector3 relativePos = target.position - transform.position;
		Quaternion rotationT = Quaternion.LookRotation(relativePos);
		GetComponent<MouseLook>().enabled = false;
		GetComponent<CharacterController>().enabled = false;
	foreach (MouseLook mouse in disableMouse) {	
			mouse.enabled = false;
		}
			transform.rotation = Quaternion.Slerp (transform.rotation ,rotationT, 0.1f);
	}
}
