using UnityEngine;
using System.Collections;

public class blinkUp : MonoBehaviour {
	public float upMin = .5f;
	public float upMax = 1f;
	public float downMin = -.5f;
	public float downMax = -1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (gameObject.name == "startQuad1") {
			transform.localPosition = new Vector3(0, Mathf.Lerp(transform.localPosition.y, upMax, .01f), .33f);
				}
		if (gameObject.name == "startQuad2") {
			transform.localPosition = new Vector3(0, Mathf.Lerp(transform.localPosition.y, downMax, .01f), .33f);
				}

	
	}
	void OnTriggerEnter(Collider other){
		if (other.name == "EndScene"){
			Debug.Log("EndSceneNow");
			if (gameObject.name == "startQuad1") {
				transform.localPosition = new Vector3(0, Mathf.Lerp(transform.localPosition.y, upMin, .01f), .33f);
			}
			if (gameObject.name == "startQuad2") {
				transform.localPosition = new Vector3(0, Mathf.Lerp(transform.localPosition.y, downMin, .01f), .33f);
			}
}
	}
}
