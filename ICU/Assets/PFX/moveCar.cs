using UnityEngine;
using System.Collections;

public class moveCar : MonoBehaviour {

	private float xPos;
	private float yPos;
	private float zPos;
	public float speed;

	// Use this for initialization
	void Start () {
		xPos = gameObject.transform.position.x;
		yPos = gameObject.transform.position.y;
		zPos = gameObject.transform.position.z;

	}
	
	// Update is called once per frame
	void Update () {
		xPos+= speed;

		gameObject.transform.position = new Vector3(xPos, yPos, zPos);
	}
}
