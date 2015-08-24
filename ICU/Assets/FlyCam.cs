using UnityEngine;
using System.Collections;

public class FlyCam : MonoBehaviour {
	public float flySpeed = 1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Vertical") != 0)
		{
			transform.Translate(Vector3.forward * flySpeed * Input.GetAxis("Vertical"));
		}
		if (Input.GetAxis("Horizontal") != 0)
		{
			transform.Translate(Vector3.right * flySpeed * Input.GetAxis("Horizontal"));
		}
	}
}
