using UnityEngine;
using System.Collections;

/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -> A CharacterMotor and a CharacterController component will be automatically added.

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
[AddComponentMenu("Camera-Control/Mouse Look")]
public class DelayedMouseLook : MonoBehaviour {

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;
	private float xVelocity = 1.0f;

	public GameObject playerCharacter;
	float rotationY = 0F;
	float rotationX = 0F;
	public bool updateRotation = false;
	public float StrideInterval;
	Quaternion oldRot;
	Vector3 oldPos;
	Quaternion minRot;
	Quaternion maxRot;
	Vector3 originalPosition;
	Vector3 vectorOffset;

	public bool lockX = false;
	void Awake(){
		//originalPosition = transform.position;

		//vectorOffset = playerCharacter.transform.position - originalPosition;

	}
	void Update(){
		//transform.position = playerCharacter.transform.position - vectorOffset;

	}
	void LateUpdate ()
	{

		if (axes == RotationAxes.MouseX)
		{
			if(lockX){
				//transform.rotation = oldRot;

				rotationX += Input.GetAxis("Mouse X") * sensitivityX; 
				Quaternion newRotation = playerCharacter.transform.rotation;
				if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0){
					//transform.position = new Vector3(transform.position.x,transform.position.y * Mathf.PingPong(.1f, 1),transform.position.z);
				}
				if((playerCharacter.transform.rotation.eulerAngles.y > oldRot.eulerAngles.y + maximumX)||
				   (playerCharacter.transform.rotation.eulerAngles.y < oldRot.eulerAngles.y + minimumX)||
				   Input.GetAxis("Vertical") != 0){

					updateRotation = true;
				}
//				else if(playerCharacter.transform.rotation.eulerAngles.y > oldRot.eulerAngles.y + maximumX + maximumX||
//						playerCharacter.transform.rotation.eulerAngles.y < oldRot.eulerAngles.y + minimumX + minimumX)
//				{
//					updateRotation = false;
//					transform.rotation = oldRot;
//					transform.localPosition = oldPos;
//					//transform.rotation = newRotation;
//
//
//				}
				else{
					transform.rotation = oldRot;
					transform.localPosition = oldPos;
					updateRotation = false;
				}
				if(updateRotation){
					transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, .4f * Time.deltaTime);
					oldRot = transform.rotation;
					oldPos = transform.localPosition;
				}

				//rotationX = Mathf.Clamp (rotationX, minimumX, maximumX);
			//	if(rotationX < minimumX || rotationX > maximumX){
		//		rotationX = Mathf.SmoothDamp(rotationX, minimumX , ref xVelocity, .05f);
			//	}
//				else if (rotationX > maximumX){
//					rotationX = Mathf.SmoothDamp(rotationX, maximumX , ref xVelocity, .05f);
//
//				}
				//transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotationX, 0);
			}

		}
	
	}
	
	void Start ()
	{
		oldRot = playerCharacter.transform.rotation;
		oldPos = transform.localPosition;

	}
}