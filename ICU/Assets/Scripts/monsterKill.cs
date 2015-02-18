using UnityEngine;
using System.Collections;

public class monsterKill : MonoBehaviour {
	//public GameObject killZone;
	// Use this for initialization
	float deathClock = 0.0f;
	public float secondsTillSceneChange = 8.0f;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (StateManager.Dead == true) {
			deathClock += Time.deltaTime;
		}
		if (deathClock>= secondsTillSceneChange){
			Application.LoadLevel(0);
		}
	}
	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "Enemy"){
			Debug.Log ("player is dead");
			StateManager.Dead = true;
		gameObject.audio.Play ();

	}
	}
}
