using UnityEngine;
using System.Collections;

public class endScene : MonoBehaviour {
	float creditTimer = 0;
	public float timeToQuit= 12f;
	// Use this for initialization
	void Start () {
		creditTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		creditTimer += Time.deltaTime;
		if(creditTimer> timeToQuit){
			Application.Quit();
		}
	}
	void OnTriggerEnter(Collider other){
	//	if (Collider.tag == "Player"){
	//	}
}
}
