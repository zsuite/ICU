﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class Monster : MonoBehaviour {
	SightObject mon;
	public float soundEffectPitchRandomness = 0.5f;
	public int stage;
	public GameObject player;
	Vector3 differenceBetween;
	bool blinkOnce;
	// Use this for initialization
	void Start () {
		blinkOnce = true;
		stage =1;
		mon = new SightObject();
		differenceBetween = player.transform.position - this.gameObject.transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		if(renderer.isVisible && EyeValues.playerBlinked && blinkOnce){
			Teleport(stage);
			audio.pitch = Random.Range(1.0f - soundEffectPitchRandomness, 1.0f + soundEffectPitchRandomness);

			blinkOnce =false;

		}
		if (!EyeValues.playerBlinked){
			blinkOnce = true;
		}

	}

	void Teleport(int _stage){

		switch(_stage){
		case 1:
			this.gameObject.transform.position = transform.position + (player.transform.position - transform.position) * 0.5f;
			transform.LookAt(player.transform.position);

			audio.Play();
			if(Vector3.Distance(this.gameObject.transform.position, player.transform.position) <= 5){
				stage = 2;
			}
			break;
		case 2:
			this.gameObject.transform.position = transform.position + (player.transform.position - transform.position) * 0.75f;
			transform.LookAt(player.transform.position);
			audio.Play();

			stage = 3;
			break;
		case 3:
			this.gameObject.transform.position = transform.position + (player.transform.position - transform.position) * 0.85f;
			transform.LookAt(player.transform.position);
			audio.Play();

			break;
		}
		
	}
}
