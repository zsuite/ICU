using UnityEngine;
using System.Collections;

public class ProximityRender : MonoBehaviour {
/*	public bool fadeIn = false;
*/
	public float minRange = 0f;
	public float maxRange = 50f;

	private Material materialToChange;
	private float distance;
	private float newAlpha;
	// Use this for initialization
	void Start () {
		materialToChange = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerStay(Collider other){
		if(other.tag == "Player"){
			distance = Vector3.Distance(Camera.main.transform.position, this.transform.position);
			if(distance <= minRange){
				newAlpha = 0;
			}
			else if (distance - minRange >= maxRange){
				newAlpha = 1;
			}
			else{
				newAlpha = (distance  - minRange)/maxRange;
			}
			materialToChange.color = new Color(materialToChange.color.r,
			                                   materialToChange.color.g,
			                                   materialToChange.color.b,
			                                   newAlpha);
		}
	}
}
