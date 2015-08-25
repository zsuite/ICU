using UnityEngine;
using System.Collections;

public class monsterKill : MonoBehaviour {
	//public GameObject killZone;
	// Use this for initialization
	float deathClock = 0.0f;
	public float secondsTillSceneChange = 3.0f;
	public string scene = "HallwayStart_1";
	void Start () {
		//LoadBank;
	}
	
	// Update is called once per frame
	void Update () {
		if (StateManager.Dead == true) {
			deathClock += Time.deltaTime;
		}
		if (deathClock>= secondsTillSceneChange){
			Application.LoadLevel(scene);
		}
	}
	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "Enemy"){
			Debug.Log ("player is dead");
			StateManager.Dead = true;
			DeathEvent("play_sx_forest_monster_killplayer");

	}
	}
	public void DeathEvent (string eventName){
		AkSoundEngine.PostEvent (eventName, gameObject);
	}
}
