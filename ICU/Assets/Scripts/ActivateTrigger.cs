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
		LoadScene = 7, // loads the scene named string
		ReplaceMaterial = 8, // replaces material with the source material
		ActivateAudio = 9
	}

	///PlayerBlinked The action to accomplish
	public Mode action = Mode.Activate;

	public enum FaceAction {
		Nothing = 0,
		Blink = 1,
		Smile = 2
	}
	
	/// The action to accomplish
	public FaceAction fTrigger = FaceAction.Blink;
	/// The game object to affect. If none, the trigger work on this game object
	/// 
	/// 
	public float afterSeconds;
	public GameObject[] target; // thing to activate.. etc
	public GameObject source; // use only for REPLACE
	public Material[] sourceMaterials; //USE for replace materials only
	public bool repeatMaterials = false;
	public int triggerCount = 1;///
	public bool needsToBeInView = false;
	public bool sourceNeedsToBeInView = false;
	public string nextScene;
	private bool FaceTrigger;
	bool blinkOnce = false;
	bool onceForMetrics = true;

	private int q;
	bool activateReady = false;
	float timeSet = 0;

	void Start(){
		FaceTrigger = false;
		q = 0;
	}
	void Update(){
		switch(fTrigger){
			case FaceAction.Nothing:
				FaceTrigger = true;
				break;
			case FaceAction.Blink:
				FaceTrigger = EyeValues.playerBlinked;
				break;
			case FaceAction.Smile:
				FaceTrigger = EyeValues.playerSmiled;
				break;
		}

		if (!FaceTrigger) {
			blinkOnce = false;
		}
	}
	void DoActivateTrigger () {

		//if (triggerCount == 0 || repeatTrigger) {
//			Object[] currentTarget = target != null ? target : gameObject;
//			Behaviour targetBehaviour = currentTarget as Behaviour;
//			GameObject[] targetGameObject = currentTarget as GameObject;
//			if (targetBehaviour != null)
//				targetGameObject = targetBehaviour.gameObject;
		for(int i = 0; i < target.Length; i++){
			if(FaceTrigger && !blinkOnce){
				switch (action) {
					case Mode.Trigger:
						target[i].BroadcastMessage ("DoActivateTrigger");
						break;
					case Mode.Replace:
						if (target[i] !=null && source != null && target[i].GetComponent<Renderer>().isVisible) {
							GameObject.Instantiate (source, target[i].transform.position, target[i].transform.rotation);
							source.SetActive(true);
							Destroy(target[i]);
						}
						break;
					case Mode.Activate:
						if(activateReady){
							if(target[i].GetComponent<Collider>() != null){
								target[i].GetComponent<Collider>().enabled =true;
							}
							if(!needsToBeInView && !sourceNeedsToBeInView){
								target[i].SetActive(true);
							}
							else if (needsToBeInView && target[i].GetComponent<Renderer>() != null && target[i].GetComponent<Renderer>().isVisible){
								if(target[i].GetComponent<Light>() != null)
								{
									target[i].GetComponent<Light>().enabled = true;
								}
								if(target[i].GetComponent<Renderer>().material !=null)
								{
									float newAlpha = Mathf.Lerp(0, 1F, Time.deltaTime * 1000);
									target[i].GetComponent<Renderer>().material.color = new Color(GetComponent<Renderer>().material.color.r, GetComponent<Renderer>().material.color.g, GetComponent<Renderer>().material.color.b, newAlpha);
								}
								
							}
							else if(sourceNeedsToBeInView && source.GetComponent<Renderer>() != null && source.GetComponent<Renderer>().isVisible){
								target[i].SetActive(true);
								}
							}
						break;
					case Mode.Enable:
						if (target != null)
							target[i].GetComponent<Renderer>().enabled = true;
						break;	
					case Mode.Disable:
						if (target != null && target[i].GetComponent<Renderer>().isVisible)
							target[i].GetComponent<Renderer>().enabled = false;
						break;	
					case Mode.Animate:
						target[i].GetComponent<Animation>().Play ();
						break;	
					case Mode.Deactivate:
						if (target[i].GetComponent<Renderer>().isVisible && needsToBeInView){
							if(target[i].GetComponent<Light>() != null)
							{
								target[i].GetComponent<Light>().enabled = false;
							}
							target[i].SetActive(false);

						}
						else if (!needsToBeInView){
							if(target[i].GetComponent<Light>() != null)
							{
								target[i].GetComponent<Light>().enabled = false;
							}
							target[i].SetActive(false);
							
						}
						
						break;
					case Mode.ReplaceMaterial:
						if(target[i].activeSelf == false)
						{
							target[i].SetActive(true);
						}
						if(target[i].GetComponent<Renderer>() != null && target[i].GetComponent<Renderer>().isVisible && q < sourceMaterials.Length){
								target[i].GetComponent<Renderer>().material = sourceMaterials[q];
								
						}
						else if (target[i].GetComponent<Projector>() != null && q < sourceMaterials.Length){
							target[i].GetComponent<Projector>().material = sourceMaterials[q];
						}
						q++;

						break;
					case Mode.LoadScene:
					if(activateReady == true)
					{Application.LoadLevel(nextScene);}
					break;

					case Mode.ActivateAudio:	
						if(activateReady){
							gameObject.GetComponent<AudioTrigger>().BeginAudioExecution();
						}
						break;
				}
				blinkOnce = true;

				if(onceForMetrics){
					MetricManagerScript.DidSomethingHappen(true);
					onceForMetrics = false;
				}
				
			}
			triggerCount--;
		}
		//}
	}

	void OnTriggerStay(Collider other){
		if(afterSeconds != 0 && Time.time > timeSet && other.tag =="Player"){
			activateReady = true;
			DoActivateTrigger();
		}
		else if(afterSeconds == 0 && other.tag =="Player"){
			activateReady = true;
			DoActivateTrigger ();
		}

	}
	void OnTriggerEnter (Collider other) {

		timeSet = Time.time + afterSeconds;
		if(afterSeconds == 0 && other.tag =="Player"){
			activateReady = true;
			DoActivateTrigger ();
		}

	}
	void OnTriggerExit (Collider other){
		timeSet = 0;
	}
}