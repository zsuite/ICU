/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2014 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using UnityEngine;
using System.Runtime.InteropServices;

public class FaceAnimationController : MonoBehaviour
{
	#region Constants
	
	private const float RETURN_FROM_FAILURE_SMOOTH_FACTOR = 0.3f;
	private const float FAILURE_SMOOTH_FACTOR = 0.1f;
	private const float MIN_SMOOTH_TRANSFORM = 0.5f;
	private const float MIN_SMOOTH_BLEND_SHAPE = 2f;

	#endregion

	#region Public Members

	public GameObject _bodyObject;
	public GameObject _mouthObject;

	public GameObject _leftEyeObject;
	public GameObject _rightEyeObject;
	public GameObject _headObject;
	public GameObject _jawObject;
	public GameObject _backObject;

	public int _leftEyebrowUpBodyBlendShapeIndex = -1;
	public int _rightEyebrowUpBodyBlendShapeIndex = -1;
	public int _leftEyeCloseBodyBlendShapeIndex = -1;
	public int _rightEyeCloseBodyBlendShapeIndex = -1;
	public int _leftEyeBottomLidUpBodyBlendShapeIndex = -1;
	public int _rightEyeBottomLidUpBodyBlendShapeIndex = -1;
	public int _mouthOpenBodyBlendShapeIndex = -1;
	public int _mouthSmileBodyBlendShapeIndex = -1;
	public int _mouthLeftBodyBlendShapeIndex = -1;
	public int _mouthRightBodyBlendShapeIndex = -1;

	public int _mouthOpenMouthBlendShapeIndex = -1;

	#endregion
	
	#region Private Members

	private SkinnedMeshRenderer _bodyRenderer = null;
    private SkinnedMeshRenderer _mouthRenderer = null;
	
	private Vector3 _leftEyeRotationAngles = Vector3.zero;
	private Vector3 _rightEyeRotationAngles = Vector3.zero;
	private Vector3 _headRotationAngles = Vector3.zero;
	private Vector3 _jawPosition = Vector3.zero;
	private Vector3 _backRotationAngles = Vector3.zero;

	private float _leftEyebrowUpInitialValue = 0f;
	private float _rightEyebrowUpInitialValue = 0f;
	private float _leftEyeCloseInitialValue = 0f;
	private float _rightEyeCloseInitialValue = 0f;
	private float _mouthOpenInitialValue = 0f;
	private float _mouthSmileInitialValue = 0f;
	private float _mouthLeftInitialValue = 0f;
	private float _mouthRightInitialValue = 0f;

	private Vector3 _leftEyeInitialRotationAngles = Vector3.zero;
	private Vector3 _rightEyeInitialRotationAngles = Vector3.zero;
	private Vector3 _headInitialRotationAngles = Vector3.zero;
	private Vector3 _jawInitialPosition = Vector3.zero;
	private Vector3 _backInitialRotationAngles = Vector3.zero;

	private bool _executeLeftEyebrowSmoothing = false;
	private bool _executeRightEyebrowSmoothing = false;
	private bool _executeLeftEyeCloseSmoothing = false;
	private bool _executeRightEyeCloseSmoothing = false;
	private bool _executeLeftEyeSmoothing = false;
	private bool _executeRightEyeSmoothing = false;
	private bool _executeMouthOpenSmoothing = false;
	private bool _executeMouthSmileSmoothing = false;
	private bool _executeMouthLeftSmoothing = false;
	private bool _executeMouthRightSmoothing = false;
	private bool _executeHeadSmoothing = false;
	private bool _executeJawSmoothing = false;
	private bool _executeBackSmoothing = false;

	private FaceTrackingManager _faceTrackingManager = null;

	#endregion

    #region Handlers

	void Awake()
	{
		_bodyRenderer = _bodyObject ? _bodyObject.GetComponent<SkinnedMeshRenderer>() : null;
		_mouthRenderer = _mouthObject ? _mouthObject.GetComponent<SkinnedMeshRenderer>() : null;

		if (_bodyRenderer)
		{
			if (_leftEyebrowUpBodyBlendShapeIndex >= 0)
			{
				_leftEyebrowUpInitialValue = _bodyRenderer.GetBlendShapeWeight(_leftEyebrowUpBodyBlendShapeIndex);
			}

			if (_rightEyebrowUpBodyBlendShapeIndex >= 0)
			{
				_rightEyebrowUpInitialValue = _bodyRenderer.GetBlendShapeWeight(_rightEyebrowUpBodyBlendShapeIndex);
			}

			if (_leftEyeCloseBodyBlendShapeIndex >= 0)
			{
				_leftEyeCloseInitialValue = _bodyRenderer.GetBlendShapeWeight(_leftEyeCloseBodyBlendShapeIndex);
			}

			if (_rightEyeCloseBodyBlendShapeIndex >= 0)
			{
				_rightEyeCloseInitialValue = _bodyRenderer.GetBlendShapeWeight(_rightEyeCloseBodyBlendShapeIndex);
			}

			if (_mouthOpenBodyBlendShapeIndex >= 0)
			{
				_mouthOpenInitialValue = _bodyRenderer.GetBlendShapeWeight(_mouthOpenBodyBlendShapeIndex);
			}
			
			if (_mouthSmileBodyBlendShapeIndex >= 0)
			{
				_mouthSmileInitialValue = _bodyRenderer.GetBlendShapeWeight(_mouthSmileBodyBlendShapeIndex);
			}
			
			if (_mouthLeftBodyBlendShapeIndex >= 0)
			{
				_mouthLeftInitialValue = _bodyRenderer.GetBlendShapeWeight(_mouthLeftBodyBlendShapeIndex);
			}
			
			if (_mouthRightBodyBlendShapeIndex >= 0)
			{
				_mouthRightInitialValue = _bodyRenderer.GetBlendShapeWeight(_mouthRightBodyBlendShapeIndex);
			}
		}

		if (_headObject)
		{
			Quaternion rotation = _headObject.transform.rotation;
			_headInitialRotationAngles = rotation.eulerAngles;
		}
		
		if (_leftEyeObject)
		{
			Quaternion rotation = _leftEyeObject.transform.rotation;
			_leftEyeInitialRotationAngles = rotation.eulerAngles;
		}
		
		if (_rightEyeObject)
		{
			Quaternion rotation = _rightEyeObject.transform.rotation;
			_rightEyeInitialRotationAngles = rotation.eulerAngles;
		}

		if (_jawObject) 
		{
			_jawInitialPosition = _jawObject.transform.position;
		}
		
		if (_backObject)
		{
			Quaternion rotation = _backObject.transform.rotation;
			_backInitialRotationAngles = rotation.eulerAngles;
		}

		GameObject sceneObject = GameObject.Find("Scene");
		_faceTrackingManager = sceneObject ? sceneObject.GetComponent<FaceTrackingManager>() : null;
	}
	
	void Start()
	{
	}
	
	void Update()
	{
		float rightEyebrowUp = 0;
		float leftEyebrowUp = 0;
		float rightEyeClose = 0;
		float leftEyeClose = 0;
		float mouthOpen = 0;
		float mouthSmile = 0;
		float mouthLeft = 0;
		float mouthRight = 0;

		if (_faceTrackingManager && !_faceTrackingManager._manualMode)
		{
			rightEyebrowUp = _faceTrackingManager.RightEyebrowUp;
			leftEyebrowUp = _faceTrackingManager.LeftEyebrowUp;
			rightEyeClose = _faceTrackingManager.RightEyeClose;
			leftEyeClose = _faceTrackingManager.LeftEyeClose;
			mouthOpen = _faceTrackingManager.MouthOpen;
			mouthSmile = _faceTrackingManager.MouthSmile;
			mouthLeft = _faceTrackingManager.MouthLeft;
			mouthRight = _faceTrackingManager.MouthRight;
		}

		float value;

		// Eyebrows Up
		SetBodyBlendShapeValue(leftEyebrowUp, _rightEyebrowUpBodyBlendShapeIndex, _rightEyebrowUpInitialValue, ref _executeRightEyebrowSmoothing, out value);
		SetBodyBlendShapeValue(rightEyebrowUp, _leftEyebrowUpBodyBlendShapeIndex, _leftEyebrowUpInitialValue, ref _executeLeftEyebrowSmoothing, out value);

		// Eyes Open
		SetEyeClose(rightEyeClose, _leftEyeCloseBodyBlendShapeIndex, _leftEyeBottomLidUpBodyBlendShapeIndex, _leftEyeCloseInitialValue, ref _executeLeftEyeCloseSmoothing);
		SetEyeClose(leftEyeClose, _rightEyeCloseBodyBlendShapeIndex, _rightEyeBottomLidUpBodyBlendShapeIndex, _rightEyeCloseInitialValue, ref _executeRightEyeCloseSmoothing);

		// Mouth Open
		SetMouthOpen(mouthOpen);
		
		// Mouth Smile
		SetBodyBlendShapeValue(mouthSmile, _mouthSmileBodyBlendShapeIndex, _mouthSmileInitialValue, ref _executeMouthSmileSmoothing, out value);
		
		// Mouth Left Right
		SetBodyBlendShapeValue(mouthLeft, _mouthLeftBodyBlendShapeIndex, _mouthLeftInitialValue, ref _executeMouthLeftSmoothing, out value);
		SetBodyBlendShapeValue(mouthRight, _mouthRightBodyBlendShapeIndex, _mouthRightInitialValue, ref _executeMouthRightSmoothing, out value);
	}

	void LateUpdate()
	{
		// These movements are done in LateUpdate to override the idle animation

		if (_faceTrackingManager && !_faceTrackingManager._manualMode)
		{
			// Head Movement
			SetHeadRotation(_faceTrackingManager.HeadRoll, _faceTrackingManager.HeadYaw, _faceTrackingManager.HeadPitch, _faceTrackingManager.BackPitch);

			// Eyes Movement
			SetEyesRotation(_faceTrackingManager.EyesMoveLeftRight, _faceTrackingManager.IsMirrorMode);
			
			// Jaw Movement
			SetJawPosition(_faceTrackingManager.JawMoveLeftRight);

			// Back Movement
			SetBackRotation(_faceTrackingManager.BackPitch);
		}
	}

	#endregion
	
	#region Private Methods

	private void SetEyeClose(float rawValue, int eyeUpperLidBlendShapeIndex, int eyeLowerLidBlendShapeIndex, float eyeInitialValue, ref bool executeEyeSmoothing)
	{
		float value;
		if (SetBodyBlendShapeValue(rawValue, eyeUpperLidBlendShapeIndex, eyeInitialValue, ref executeEyeSmoothing, out value))
		{
			if (eyeLowerLidBlendShapeIndex >= 0)
			{
				_bodyRenderer.SetBlendShapeWeight(eyeLowerLidBlendShapeIndex, value);
			}
		}
	}
	
	private void SetMouthOpen(float rawValue)
	{
		float value;
		if (SetBodyBlendShapeValue(rawValue, _mouthOpenBodyBlendShapeIndex, _mouthOpenInitialValue, ref _executeMouthOpenSmoothing, out value))
		{
			if (_mouthRenderer && _mouthOpenMouthBlendShapeIndex >= 0)
			{
				_mouthRenderer.SetBlendShapeWeight(_mouthOpenMouthBlendShapeIndex, value);
			}
		}
	}

	private bool SetBodyBlendShapeValue(float rawValue, int blendShapeIndex, float initialValue, ref bool executeSmoothing, out float value)
	{
		value = 0;

		if (_bodyRenderer && blendShapeIndex >= 0)
		{
			value = CalcBlendShapeValue(rawValue, blendShapeIndex, initialValue, ref executeSmoothing);
			_bodyRenderer.SetBlendShapeWeight(blendShapeIndex, value);

			return true;
		}

		return false;
	}
	
	private void SetHeadRotation(float headRollRawValue, float headYawRawValue, float headPitchRawValue, float backPitchRawValue)
	{
		if (_headObject)
		{
			Quaternion rotation = _headObject.transform.rotation;
			Vector3 newRotationAngles = new Vector3(rotation.eulerAngles.x + headRollRawValue, rotation.eulerAngles.y + headYawRawValue, rotation.eulerAngles.z + headPitchRawValue - backPitchRawValue);
			
			UpdateRotation(rotation, newRotationAngles, ref _headRotationAngles, _headInitialRotationAngles, ref _executeHeadSmoothing);
			
			rotation.eulerAngles = _headRotationAngles;
			_headObject.transform.rotation = rotation;
		}
	}

	private void SetEyesRotation(float eyesMoveLeftRightRawValue, bool isHorizontalMirror)
	{
		if (_leftEyeObject && _rightEyeObject)
		{
			float eyesMoveLeftRightValue = 0f;
			
			if (eyesMoveLeftRightRawValue != 0f)
			{
				eyesMoveLeftRightValue = eyesMoveLeftRightRawValue;
				
				if (!isHorizontalMirror)
				{
					eyesMoveLeftRightValue *= -1;
				}
			}
			
			Quaternion leftEyeRotation = _leftEyeObject.transform.rotation;

			Vector3 newRotationAngles = new Vector3(leftEyeRotation.eulerAngles.x, leftEyeRotation.eulerAngles.y + eyesMoveLeftRightValue, leftEyeRotation.eulerAngles.z);

			UpdateRotation(leftEyeRotation, newRotationAngles, ref _leftEyeRotationAngles, _leftEyeInitialRotationAngles, ref _executeLeftEyeSmoothing);
			
			leftEyeRotation.eulerAngles = new Vector3(leftEyeRotation.eulerAngles.x, _leftEyeRotationAngles.y, leftEyeRotation.eulerAngles.z);;
			_leftEyeObject.transform.rotation = leftEyeRotation;

			Quaternion rightEyeRotation = _rightEyeObject.transform.rotation;
			
			newRotationAngles = new Vector3(rightEyeRotation.eulerAngles.x, rightEyeRotation.eulerAngles.y + eyesMoveLeftRightValue, rightEyeRotation.eulerAngles.z);

			UpdateRotation(rightEyeRotation, newRotationAngles, ref _rightEyeRotationAngles, _rightEyeInitialRotationAngles, ref _executeRightEyeSmoothing);
			
			rightEyeRotation.eulerAngles = new Vector3(rightEyeRotation.eulerAngles.x, _rightEyeRotationAngles.y, rightEyeRotation.eulerAngles.z);
			_rightEyeObject.transform.rotation = rightEyeRotation;
		}
	}

	private void SetJawPosition(float jawMoveLeftRightRawValue)
	{
		if (_jawObject)
		{
			Vector3 position = _jawObject.transform.position;
			
			if (_faceTrackingManager.IsPoseValid)
			{
				Vector3 newPosition = new Vector3(position.x, position.y, position.z + jawMoveLeftRightRawValue);
				
				if (_executeJawSmoothing)
				{
					if (Mathf.Abs(newPosition.z - _jawPosition.z) < MIN_SMOOTH_TRANSFORM)
					{
						_executeJawSmoothing = false;
						_jawPosition = newPosition;
					}
					else
					{
						_jawPosition = Vector3.Lerp(_jawPosition, newPosition, RETURN_FROM_FAILURE_SMOOTH_FACTOR);
					}
				}
				else
				{
					_jawPosition = newPosition;
				}
			}
			else
			{
				_executeJawSmoothing = true;

				if (_jawPosition == Vector3.zero)
				{
					_jawPosition = _jawObject.transform.position;
				}

				if (Mathf.Abs(_jawInitialPosition.z - _jawPosition.z) < MIN_SMOOTH_TRANSFORM)
				{
					return;
				}

				_jawPosition = Vector3.Lerp(_jawPosition, _jawInitialPosition, FAILURE_SMOOTH_FACTOR);
			}
			
			_jawObject.transform.position = _jawPosition;
		}
	}
	
	private void SetBackRotation(float backPitchRawValue)
	{
		if (_backObject)
		{
			Quaternion rotation = _backObject.transform.rotation;
		
			Vector3 newRotationAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, rotation.eulerAngles.z + backPitchRawValue);
			
			UpdateRotation(rotation, newRotationAngles, ref _backRotationAngles, _backInitialRotationAngles, ref _executeBackSmoothing);
			
			rotation.eulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, _backRotationAngles.z);
			_backObject.transform.rotation = rotation;
		}
	}
	
	private float CalcBlendShapeValue(float rawValue, int blendShapeIndex, float initialValue, ref bool executeSmoothing)
	{
		float value = 0f;
		float currentValue = _bodyRenderer.GetBlendShapeWeight(blendShapeIndex);
		
		if (_faceTrackingManager.IsPoseValid)
		{
			if (executeSmoothing)
			{
				value = rawValue;
				if (Mathf.Abs(value - currentValue) > MIN_SMOOTH_BLEND_SHAPE)
				{
					value = Mathf.Lerp(currentValue, value, RETURN_FROM_FAILURE_SMOOTH_FACTOR);
				}
				else
				{
					executeSmoothing = false;
				}
			}
			else
			{
				value = rawValue;
			}
		}
		else
		{
			executeSmoothing = true;
		
			value = Mathf.Lerp(currentValue, initialValue, FAILURE_SMOOTH_FACTOR);
		}
		
		return value;
	}
	
	private void UpdateRotation(Quaternion currentRotation, Vector3 newRotationAngles, ref Vector3 targetRotationAngles, Vector3 rotationAnglesInitialValue, ref bool executeSmoothing)
	{
		if (_faceTrackingManager.IsPoseValid)
		{
			if (executeSmoothing)
			{
				if (Mathf.Abs(newRotationAngles.x - targetRotationAngles.x) < MIN_SMOOTH_TRANSFORM && Mathf.Abs(newRotationAngles.y - targetRotationAngles.y) < MIN_SMOOTH_TRANSFORM && Mathf.Abs(newRotationAngles.z - targetRotationAngles.z) < MIN_SMOOTH_TRANSFORM)
				{
					executeSmoothing = false;
					targetRotationAngles = newRotationAngles;
				}
				else
				{
					targetRotationAngles = Vector3.Lerp(targetRotationAngles, newRotationAngles, RETURN_FROM_FAILURE_SMOOTH_FACTOR);
				}
			}
			else
			{
				targetRotationAngles = newRotationAngles;
			}
		}
		else
		{
			executeSmoothing = true;
			
			if (targetRotationAngles == Vector3.zero)
			{
				targetRotationAngles = currentRotation.eulerAngles;
			}

			targetRotationAngles = Vector3.Lerp(targetRotationAngles, rotationAnglesInitialValue, FAILURE_SMOOTH_FACTOR);
		}	
	}

	#endregion
}
