using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class Monster : MonoBehaviour {
	SightObject mon;
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
			if(Vector3.Distance(this.gameObject.transform.position, player.transform.position) <= 5){
				stage = 2;
			}
			break;
		case 2:
			this.gameObject.transform.position = transform.position + (player.transform.position - transform.position) * 0.75f;
			stage = 3;
			break;
		case 3:
			this.gameObject.transform.position = transform.position + (player.transform.position - transform.position) * 0.85f;
			break;
		}
		
	}
}
