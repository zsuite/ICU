using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]
public class Monster : MonoBehaviour {
	public float soundEffectPitchRandomness = 0.5f;
	public int stage;
	public GameObject player;
	public GameObject playerCharacter;
	public GameObject myHead;
	public SkinnedMeshRenderer ICU_Monster_HR;
//	public List<Mesh> randomMeshes;
	//public AudioSource giggle;
	public float speedTowardsPlayer = 0.2f;
	public float headTurnSpeed = 3f;
	float acceleration = .007f;
	float initialSpeed;
	bool blinkOnce;
	Quaternion oldRot;
	Quaternion newRot;
	Vector3 newDirection;
	Monster_ControllerState monsterAnim;
	// Use this for initialization

	void Start () {
		blinkOnce = true;
		//blinkQuad.SetActive(false);
		initialSpeed = speedTowardsPlayer;
		stage =1;
		monsterAnim = GetComponent<Monster_ControllerState>();

	}
	
	// Update is called once per frame
	void Update () {
		 oldRot = myHead.transform.rotation;
		 newDirection = playerCharacter.transform.position - myHead.transform.position;
		//myHead.transform.LookAt(playerCharacter.transform.position) ;
		//Quaternion newRot = myHead.transform.rotation ;
		 newRot = Quaternion.LookRotation(newDirection) ;
		
//		Quaternion oldRot = myHead.transform.rotation;
//		myHead.transform.LookAt(playerCharacter.transform.position) ;
//		Quaternion newRot = myHead.transform.rotation ;
//		myHead.transform.rotation = Quaternion.Lerp(oldRot , newRot, headTurnSpeed * Time.deltaTime);
		if(GetComponent<Renderer>().isVisible && EyeValues.playerBlinked && blinkOnce){
			Teleport(stage);
			monsterAnim.RandomPose();
			//blinkQuad.SetActive(true);
			transform.LookAt(playerCharacter.transform.position);
			
			TeleportEvent("play_sx_forest_monster_teleport");
			//audio.pitch = Random.Range(1.0f - soundEffectPitchRandomness, 1.0f + soundEffectPitchRandomness);

			blinkOnce =false;

		}
		else if (GetComponent<Renderer>().isVisible == false && StateManager.Dead == false)
		{
				MoveEvent("play_sx_forest_monster_move");
			
//			Debug.Log ("ImCOMING");
			speedTowardsPlayer += acceleration *Time.deltaTime;
			//speedTowardsPlayer = speedTowardsPlayer + acceleration;
			transform.position = Vector3.Lerp(transform.position, player.transform.position, speedTowardsPlayer);
			transform.LookAt(playerCharacter.transform.position);

		}
		if (!EyeValues.playerBlinked){
			//blinkQuad.SetActive(false);

			blinkOnce = true;
		}
		if(GetComponent<Renderer>().isVisible)
		{
			speedTowardsPlayer = initialSpeed;
		}


	}
	void LateUpdate(){
		myHead.transform.rotation = Quaternion.Lerp(oldRot , newRot, headTurnSpeed * Time.deltaTime);

	
	}

	void Teleport(int _stage){

		switch(_stage){
		case 1:
			this.gameObject.transform.position = transform.position + (player.transform.position - transform.position) * 0.5f;

			if(Vector3.Distance(this.gameObject.transform.position, player.transform.position) <= 5){
				stage = 2;
			}
			break;
		case 2:
			this.gameObject.transform.position = transform.position + (player.transform.position - transform.position) * 0.75f;

			stage = 3;
			break;
		case 3:
			this.gameObject.transform.position = transform.position + (player.transform.position - transform.position) * 0.8f;

			break;
		}
		
	}
	public void TeleportEvent (string play_sx_forest_monster_teleport){
		AkSoundEngine.PostEvent (play_sx_forest_monster_teleport, gameObject);
	}
	public void MoveEvent (string soundToPlay){
		AkSoundEngine.PostEvent (soundToPlay, gameObject);
	}
}
