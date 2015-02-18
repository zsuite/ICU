using UnityEngine;
using System.Collections;
public class ActivateTrigger : MonoBehaviour {

	// A multi-purpose script which causes an action to occur when
	// a trigger collider is entered.

	public enum Mode {
		Trigger   = 0, // Just broadcast the action on to the target
		Replace   = 1, // replace target with source
		Activate  = 2, // Activate the target GameObject
		Enable    = 3, // makes Visible
		Disable   = 4, // Makes invisible
		Animate   = 5, // Start animation on target
		Deactivate= 6, // Decativate target GameObject
		LoadScene = 7 // loads the scene named string
	}

	/// The action to accomplish
	public Mode action = Mode.Activate;

	/// The game object to affect. If none, the trigger work on this game object
	/// 
	/// 
	public float afterSeconds;
	public GameObject[] target; // thing to activate.. etc
	public GameObject source; // use only for REPLACE
	public int triggerCount = 1;///
	public bool repeatTrigger = false;
	public bool activateViaBlink = true;
	public bool needsToBeVisible = false;
	public string nextScene;
	private bool PlayerBlinked;
	bool activateReady = false;
	float timeSet = 0;

	void Start(){
		PlayerBlinked = false;
	}
	void Update(){
		if (activateViaBlink){
			PlayerBlinked = EyeValues.playerBlinked;
		}
		else{
			PlayerBlinked = true;
		}
	}
	void DoActivateTrigger () {
		triggerCount--;

		if (triggerCount == 0 || repeatTrigger) {
//			Object[] currentTarget = target != null ? target : gameObject;
//			Behaviour targetBehaviour = currentTarget as Behaviour;
//			GameObject[] targetGameObject = currentTarget as GameObject;
//			if (targetBehaviour != null)
//				targetGameObject = targetBehaviour.gameObject;
			for(int i = 0; i < target.Length; i++){
				if(target[i].renderer.isVisible == needsToBeVisible && PlayerBlinked){

					switch (action) {
						case Mode.Trigger:
							target[i].BroadcastMessage ("DoActivateTrigger");
							break;
						case Mode.Replace:
							if (source != null) {
								GameObject.Instantiate (source, target[i].transform.position, target[i].transform.rotation);
								Destroy(target[i]);
							}
							break;
						case Mode.Activate:
							
								target[i].SetActive(true);
							break;
						case Mode.Enable:
							if (target != null)
								target[i].renderer.enabled = true;
							break;	
						case Mode.Disable:
							if (target != null)
								target[i].renderer.enabled = false;
							break;	
						case Mode.Animate:
							target[i].animation.Play ();
							break;	
						case Mode.Deactivate:
							target[i].SetActive(false);
							break;
					}
				}
			}
		}
	}
	void DoActivateTriggerTime () {

//		Object[] currentTarget = target != null ? target : gameObject;
//		Behaviour targetBehaviour = currentTarget as Behaviour;
//		GameObject[] targetGameObject = currentTarget as GameObject;
//		if (targetBehaviour != null)
//			targetGameObject = targetBehaviour.gameObject;
		for(int i = 0; i < target.Length; i++){
			if(target[i].renderer.isVisible == needsToBeVisible && PlayerBlinked){
				switch (action) {
				case Mode.Trigger:
					target[i].BroadcastMessage ("DoActivateTrigger");
					break;
				case Mode.Replace:
					if (source != null) {
						GameObject.Instantiate (source, target[i].transform.position, target[i].transform.rotation);
						Destroy (target[i]);
					}
					break;
				case Mode.Activate:
					if(activateReady)
						target[i].SetActive(true);
					break;
				case Mode.Enable:
					if (target != null)
						target[i].renderer.enabled = true;
					break;	
				case Mode.Disable:
					if (target != null)
						target[i].renderer.enabled = false;
					break;	
				case Mode.Animate:
					target[i].animation.Play ();
					break;	
				case Mode.Deactivate:

					target[i].SetActive(false);
					break;
				case Mode.LoadScene:
						Debug.Log("Ya");
						Application.LoadLevel(nextScene);
					break;
				}
			}
		}

	}
	void OnTriggerStay(Collider other){
		if(afterSeconds != 0 && Time.time > timeSet){
			activateReady = true;
			DoActivateTriggerTime();
		}
		else if(afterSeconds == 0){
			DoActivateTrigger ();
		}

	}
	void OnTriggerEnter (Collider other) {

		timeSet = Time.time + afterSeconds;
		if(afterSeconds == 0){
			DoActivateTrigger ();
		}

	}
	void OnTriggerExit (Collider other){
		timeSet = 0;
	}
}