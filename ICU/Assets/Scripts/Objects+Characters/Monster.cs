using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class Monster : MonoBehaviour {
	public float soundEffectPitchRandomness = 0.5f;
	public int stage;
	public GameObject player;
	public GameObject lookAtMe;
	public GameObject blinkQuad;
	public float speedTowardsPlayer = 0.2f;
	float initialSpeed;
	bool blinkOnce;
	// Use this for initialization
	void Start () {
		blinkOnce = true;
		//blinkQuad.SetActive(false);
		initialSpeed = speedTowardsPlayer;
		stage =1;

	}
	
	// Update is called once per frame
	void Update () {
		if(renderer.isVisible && EyeValues.playerBlinked && blinkOnce){
			Teleport(stage);
			//blinkQuad.SetActive(true);
			transform.LookAt(lookAtMe.transform.position);
			
			audio.Play();
			audio.pitch = Random.Range(1.0f - soundEffectPitchRandomness, 1.0f + soundEffectPitchRandomness);

			blinkOnce =false;

		}
		else if (renderer.isVisible == false)
		{
			Debug.Log ("ImCOMING");
			speedTowardsPlayer = initialSpeed * Time.deltaTime;
			transform.position = Vector3.Lerp(transform.position, player.transform.position, speedTowardsPlayer);
			transform.LookAt(lookAtMe.transform.position);

		}
		if (!EyeValues.playerBlinked){
			//blinkQuad.SetActive(false);

			blinkOnce = true;
		}
		if(renderer.isVisible)
		{
			speedTowardsPlayer = initialSpeed;
		}


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
			this.gameObject.transform.position = transform.position + (player.transform.position - transform.position);

			break;
		}
		
	}
}
