using UnityEngine;
using System.Collections;

public class PlayerController : Controller {
	public Transform target;
	// Use this for initialization
	void Start () {
	
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
		GetComponentInChildren<MouseLook>().enabled = false;
		transform.rotation = Quaternion.Slerp (transform.rotation ,rotationT, 0.1f);
	}
}
