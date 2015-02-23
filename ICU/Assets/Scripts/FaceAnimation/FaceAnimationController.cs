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
	
	public GameObject _lensNiceObject;
	//	public int _leftEyebrowUpBodyBlendShapeIndex = -1;
	//	public int _rightEyebrowUpBodyBlendShapeIndex = -1;
	//	public int _eyebrowsDownBodyBlendShapeIndex = -1;
	//
	//	public int _leftEyeCloseBodyBlendShapeIndex = -1;
	//	public int _rightEyeCloseBodyBlendShapeIndex = -1;
	//	public int _leftEyeBottomLidUpBodyBlendShapeIndex = -1;
	//	public int _rightEyeBottomLidUpBodyBlendShapeIndex = -1;
	//
	//	public int _mouthOpenBodyBlendShapeIndex = -1;
	//	public int _mouthSmileBodyBlendShapeIndex = -1;
	//	public int _mouthKissBodyBlendShapeIndex = -1;
	//	public int _mouthLeftBodyBlendShapeIndex = -1;
	//	public int _mouthRightBodyBlendShapeIndex = -1;
	//	public int _mouthEBodyBlendShapeIndex = -1;
	//	public int _mouthEBlendShapeIndex = -1;
	//	public int _mouthOpenMouthBlendShapeIndex = -1;
	//	public int _mouthBodyUBlendShapeIndex = -1;
	//	public int _mouthUBlendShapeIndex = -1;
	//	public int _mouthMBPBlendShapeIndex = -1;
	
	public int _rBrowDownBodyBlendShapeIndex = -1;
	public int _lBrowDownBodyBlendShapeIndex = -1;
	public int _rBrowUpBodyBlendShapeIndex = -1;
	public int _lBrowUpBodyBlendShapeIndex = -1;
	public int _rBrowInBodyBlendShapeIndex = -1;
	public int _lBrowInBodyBlendShapeIndex = -1;
	public int _lBlinkBodyBlendShapeIndex = -1;
	public int _rBlinkBodyBlendShapeIndex = -1;
	public int _lLowerLidUpBodyBlendShapeIndex = -1;
	public int _rLowerLidUpBodyBlendShapeIndex = -1;
	public int _lSmileBodyBlendShapeIndex = -1;
	public int _rSmileBodyBlendShapeIndex = -1;
	public int _lSmileCorrectiveBodyBlendShapeIndex = -1;
	public int _rSmileCorrectiveBodyBlendShapeIndex = -1;
	public int _lMouthSqueezeBodyBlendShapeIndex = -1;
	public int _rMouthSqueezeBodyBlendShapeIndex = -1;
	public int _upperLipPressBodyBlendShapeIndex = -1;
	public int _lowerLipPressBodyBlendShapeIndex = -1;
	public int _upperLipUpBodyBlendShapeIndex = -1;
	public int _lowerLipDownBodyBlendShapeIndex = -1;
	public int _mouthFrownBodyBlendShapeIndex = -1;
	public int _mouthPuckerBodyBlendShapeIndex = -1;
	public int _mouthOpenBodyBlendShapeIndex = -1;
	public int _mouthOpenMouthBlendShapeIndex = -1;
	
	
	//EyeLens BlenshapeIndex
	public int _lensRightEyeSmileBlendShapeIndex = -1;
	public int _lensRightEyeBlinkBlendShapeIndex = -1;
	public int _lensRightEyeOpenBlendShapeIndex = -1;
	public int _lensRightEyeLookUpBlendShapeIndex = -1;
	public int _lensLeftEyeSmileBlendShapeIndex = -1;
	public int _lensLeftEyeBlinkBlendShapeIndex = -1;
	public int _lensLeftEyeOpenBlendShapeIndex = -1;
	public int _lensLeftEyeLookUpBlendShapeIndex = -1;
	
	
	#endregion
	
	#region Private Members
	
	private SkinnedMeshRenderer _bodyRenderer = null;
	private SkinnedMeshRenderer _mouthRenderer = null;
	private SkinnedMeshRenderer _lensEyesRenderer = null;
	
	private Vector3 _leftEyeRotationAngles = Vector3.zero;
	private Vector3 _rightEyeRotationAngles = Vector3.zero;
	private Vector3 _headRotationAngles = Vector3.zero;
	private Vector3 _jawPosition = Vector3.zero;
	private Vector3 _backRotationAngles = Vector3.zero;
	
	//	private float _leftEyebrowUpInitialValue = 0f;
	//	private float _rightEyebrowUpInitialValue = 0f;
	//	private float _eyebrowsDownInitialValue = 0f;
	//	private float _leftEyeCloseInitialValue = 0f;
	//	private float _rightEyeCloseInitialValue = 0f;
	//
	//	private float _mouthEInitialValue = 0f;
	//	private float _mouthOpenInitialValue = 0f;
	//	private float _mouthSmileInitialValue = 0f;
	//	private float _mouthKissInitialValue = 0f;
	//	private float _mouthLeftInitialValue = 0f;
	//	private float _mouthRightInitialValue = 0f;
	//	private float _mouthUInitialValue = 0f;
	//	private float _mouthMBPInitialValue = 0f;
	
	private float _lBrowDownInitialValue = 0f;
	private float _rBrowDownInitialValue = 0f;
	private float _lBrowUpInitialValue = 0f;
	private float _rBrowUpInitialValue = 0f;
	private float _lBrowInInitialValue = 0f;
	private float _rBrowInInitialValue = 0f;
	private float _lBlinkInitialValue = 0f;
	private float _rBlinkInitialValue = 0f;
	private float _lLowerLidUpInitialValue = 0f;
	private float _rLowerLidUpInitialValue = 0f;
	private float _lSmileInitialValue = 0f;
	private float _rSmileInitialValue = 0f;
	private float _lSmileCorrectiveInitialValue = 0f;
	private float _rSmileCorrectiveInitialValue = 0f;
	private float _lMouthSqueezeInitialValue = 0f;
	private float _rMouthSqueezeInitialValue = 0f;
	private float _upperLipPressInitialValue = 0f;
	private float _lowerLipPressInitialValue = 0f;
	private float _lowerLipDownInitialValue = 0f;
	private float _upperLipUpInitialValue = 0f;
	private float _mouthFrownInitialValue = 0f;
	private float _mouthPuckerInitialValue = 0f;
	private float _mouthOpenInitialValue = 0f;
	private float _mouthOpenMouthInitialValue = 0f;
	
	
	//EyeLensInitials
	private float _lensRightEyeSmileInitialValue = 0f;
	private float _lensRightEyeBlinkInitialValue = 0f;
	private float _lensRightEyeOpenInitialValue = 0f;
	private float _lensRightEyeLookUpInitialValue = 0f;
	private float _lensLeftEyeSmileInitialValue = 0f;
	private float _lensLeftEyeBlinkInitialValue = 0f;
	private float _lensLeftEyeOpenInitialValue = 0f;
	private float _lensLeftEyeLookUpInitialValue = 0f;
	
	
	private Vector3 _leftEyeInitialRotationAngles = Vector3.zero;
	private Vector3 _rightEyeInitialRotationAngles = Vector3.zero;
	private Vector3 _headInitialRotationAngles = Vector3.zero;
	private Vector3 _jawInitialPosition = Vector3.zero;
	private Vector3 _backInitialRotationAngles = Vector3.zero;
	
	//	private bool _executeLeftEyebrowSmoothing = false;
	//	private bool _executeRightEyebrowSmoothing = false;
	//	private bool _executeEyebrowsDownSmoothing = false;
	//	private bool _executeLeftEyeCloseSmoothing = false;
	//	private bool _executeRightEyeCloseSmoothing = false;
	private bool _executeLeftEyeSmoothing = false;
	private bool _executeRightEyeSmoothing = false;
	//	private bool _executeMouthOpenSmoothing = false;
	//	private bool _executeMouthESmoothing = false;
	//	private bool _executeMouthSmileSmoothing = false;
	//	private bool _executeMouthKissSmoothing = false;
	//	private bool _executeMouthLeftSmoothing = false;
	//	private bool _executeMouthRightSmoothing = false;
	private bool _executeHeadSmoothing = false;
	private bool _executeJawSmoothing = false;
	private bool _executeBackSmoothing = false;
	//	private bool _executeMouthUSmoothing = false;
	//	private bool _executeMouthMBPSmoothing = false;
	
	private bool _executeLBrowDownSmoothing = false;
	private bool _executeRBrowDownSmoothing = false;
	private bool _executeLBrowUpSmoothing = false;
	private bool _executeRBrowUpSmoothing = false;
	private bool _executeLBrowInSmoothing = false;
	private bool _executeRBrowInSmoothing = false;
	private bool _executeLBlinkSmoothing = false;
	private bool _executeRBlinkSmoothing = false;
	private bool _executeLLowerLidUpSmoothing = false;
	private bool _executeRLowerLidUpSmoothing = false;
	private bool _executeLSmileSmoothing = false;
	private bool _executeRSmileSmoothing = false;
	private bool _executeLSmileCorrectiveSmoothing = false;
	private bool _executeRSmileCorrectiveSmoothing = false;
	private bool _executeLMouthSqueezeSmoothing = false;
	private bool _executeRMouthSqueezeSmoothing = false;
	private bool _executeUpperLipPressSmoothing = false;
	private bool _executeLowerLipPressSmoothing = false;
	private bool _executeLowerLipDownSmoothing = false;
	private bool _executeUpperLipUpSmoothing = false;
	private bool _executeMouthFrownSmoothing = false;
	private bool _executeMouthPuckerSmoothing = false;
	private bool _executeMouthOpenSmoothing = false;
	private bool _executeMouthOpenMouthSmoothing = false;
	
	//EyeLens
	private bool _lensRightEyeSmileSmoothing = false;
	private bool _lensRightEyeBlinkSmoothingg = false;
	private bool _lensRightEyeOpenSmoothinge = false;
	private bool _lensRightEyeLookUpSmoothing = false;
	private bool _lensLeftEyeSmileSmoothing = false;
	private bool _lensLeftEyeBlinkSmoothing = false;
	private bool _lensLeftEyeOpenSmoothing = false;
	private bool _lensLeftEyeLookUpSmoothing = false;
	
	private FaceTrackingManager _faceTrackingManager = null;
	
	#endregion
	
	#region Handlers
	
	void Awake()
	{
		_bodyRenderer = _bodyObject ? _bodyObject.GetComponent<SkinnedMeshRenderer>() : null;
		_mouthRenderer = _mouthObject ? _mouthObject.GetComponent<SkinnedMeshRenderer>() : null;
		_lensEyesRenderer = _lensNiceObject ? _mouthObject.GetComponent<SkinnedMeshRenderer>() : null;
		
		if (_bodyRenderer) 
		{
			#region BodyCheckIndex
			//				if (_leftEyebrowUpBodyBlendShapeIndex >= 0)
			//				{
			//					_leftEyebrowUpInitialValue = _bodyRenderer.GetBlendShapeWeight(_leftEyebrowUpBodyBlendShapeIndex);
			//				}
			//	
			//				if (_rightEyebrowUpBodyBlendShapeIndex >= 0)
			//				{
			//					_rightEyebrowUpInitialValue = _bodyRenderer.GetBlendShapeWeight(_rightEyebrowUpBodyBlendShapeIndex);
			//				}
			//	
			//				if (_eyebrowsDownBodyBlendShapeIndex >= 0)
			//				{
			//					_eyebrowsDownInitialValue = _bodyRenderer.GetBlendShapeWeight(_eyebrowsDownBodyBlendShapeIndex);
			//				}
			//	
			//				if (_leftEyeCloseBodyBlendShapeIndex >= 0)
			//				{
			//					_leftEyeCloseInitialValue = _bodyRenderer.GetBlendShapeWeight(_leftEyeCloseBodyBlendShapeIndex);
			//				}
			//	
			//				if (_rightEyeCloseBodyBlendShapeIndex >= 0)
			//				{
			//					_rightEyeCloseInitialValue = _bodyRenderer.GetBlendShapeWeight(_rightEyeCloseBodyBlendShapeIndex);
			//				}
			//	
			//				if (_mouthOpenBodyBlendShapeIndex >= 0)
			//				{
			//					_mouthOpenInitialValue = _bodyRenderer.GetBlendShapeWeight(_mouthOpenBodyBlendShapeIndex);
			//				}
			//	
			//				if (_mouthEBodyBlendShapeIndex >= 0)
			//				{
			//					_mouthEInitialValue = _bodyRenderer.GetBlendShapeWeight(_mouthEBlendShapeIndex);
			//				}
			//				
			//				if (_mouthSmileBodyBlendShapeIndex >= 0)
			//				{
			//					_mouthSmileInitialValue = _bodyRenderer.GetBlendShapeWeight(_mouthSmileBodyBlendShapeIndex);
			//				}
			//	
			//				if (_mouthKissBodyBlendShapeIndex >= 0)
			//				{
			//					_mouthKissInitialValue = _bodyRenderer.GetBlendShapeWeight(_mouthKissBodyBlendShapeIndex);
			//				}
			//	
			//				if (_mouthLeftBodyBlendShapeIndex >= 0)
			//				{
			//					_mouthLeftInitialValue = _bodyRenderer.GetBlendShapeWeight(_mouthLeftBodyBlendShapeIndex);
			//				}
			//				
			//				if (_mouthRightBodyBlendShapeIndex >= 0)
			//				{
			//					_mouthRightInitialValue = _bodyRenderer.GetBlendShapeWeight(_mouthRightBodyBlendShapeIndex);
			//				}
			//				if (_mouthBodyUBlendShapeIndex >= 0)
			//				{
			//					_mouthUInitialValue = _bodyRenderer.GetBlendShapeWeight(_mouthBodyUBlendShapeIndex);
			//				}
			//	
			//				if (_mouthUBlendShapeIndex >= 0)
			//				{
			//					_mouthUInitialValue = _bodyRenderer.GetBlendShapeWeight(_mouthUBlendShapeIndex);
			//				}
			//				if (_mouthMBPBlendShapeIndex >= 0)
			//				{
			//					_mouthMBPInitialValue = _bodyRenderer.GetBlendShapeWeight(_mouthMBPBlendShapeIndex);
			//				}
			if (_rBrowDownBodyBlendShapeIndex >= 0) {
				_rBrowDownInitialValue = _bodyRenderer.GetBlendShapeWeight (_rBrowDownBodyBlendShapeIndex);
			}
			if (_lBrowDownBodyBlendShapeIndex >= 0) {
				_lBrowDownInitialValue = _bodyRenderer.GetBlendShapeWeight (_lBrowDownBodyBlendShapeIndex);
			}
			if (_rBrowUpBodyBlendShapeIndex >= 0) {
				_rBrowUpInitialValue = _bodyRenderer.GetBlendShapeWeight (_rBrowUpBodyBlendShapeIndex);
			}
			if (_lBrowUpBodyBlendShapeIndex >= 0) {
				_lBrowUpInitialValue = _bodyRenderer.GetBlendShapeWeight (_lBrowUpBodyBlendShapeIndex);
			}
			if (_rBrowInBodyBlendShapeIndex >= 0) {
				_rBrowInInitialValue = _bodyRenderer.GetBlendShapeWeight (_rBrowInBodyBlendShapeIndex);
			}
			if (_lBrowInBodyBlendShapeIndex >= 0) {
				_lBrowInInitialValue = _bodyRenderer.GetBlendShapeWeight (_lBrowInBodyBlendShapeIndex);
			}
			if (_rBlinkBodyBlendShapeIndex >= 0) {
				_rBlinkInitialValue = _bodyRenderer.GetBlendShapeWeight (_rBlinkBodyBlendShapeIndex);
			}
			if (_lBlinkBodyBlendShapeIndex >= 0) {
				_lBlinkInitialValue = _bodyRenderer.GetBlendShapeWeight (_lBlinkBodyBlendShapeIndex);
			}
			if (_rLowerLidUpBodyBlendShapeIndex >= 0) {
				_rLowerLidUpInitialValue = _bodyRenderer.GetBlendShapeWeight (_rLowerLidUpBodyBlendShapeIndex);
			}
			if (_lLowerLidUpBodyBlendShapeIndex >= 0) {
				_lLowerLidUpInitialValue = _bodyRenderer.GetBlendShapeWeight (_lLowerLidUpBodyBlendShapeIndex);
			}
			if (_rSmileBodyBlendShapeIndex >= 0) {
				_rSmileInitialValue = _bodyRenderer.GetBlendShapeWeight (_rSmileBodyBlendShapeIndex);
			}
			if (_lSmileBodyBlendShapeIndex >= 0) {
				_lSmileInitialValue = _bodyRenderer.GetBlendShapeWeight (_lSmileBodyBlendShapeIndex);
			}
			if (_rSmileCorrectiveBodyBlendShapeIndex >= 0) {
				_rSmileCorrectiveInitialValue = _bodyRenderer.GetBlendShapeWeight (_rSmileCorrectiveBodyBlendShapeIndex);
			}
			if (_lSmileCorrectiveBodyBlendShapeIndex >= 0) {
				_lSmileCorrectiveInitialValue = _bodyRenderer.GetBlendShapeWeight (_lSmileCorrectiveBodyBlendShapeIndex);
			}
			if (_lMouthSqueezeBodyBlendShapeIndex >= 0) {
				_lMouthSqueezeInitialValue = _bodyRenderer.GetBlendShapeWeight (_lMouthSqueezeBodyBlendShapeIndex);
			}
			if (_rMouthSqueezeBodyBlendShapeIndex >= 0) {
				_rMouthSqueezeInitialValue = _bodyRenderer.GetBlendShapeWeight (_rMouthSqueezeBodyBlendShapeIndex);
			}
			if (_upperLipPressBodyBlendShapeIndex >= 0) {
				_upperLipPressInitialValue = _bodyRenderer.GetBlendShapeWeight (_upperLipPressBodyBlendShapeIndex);
			}
			if (_lowerLipPressBodyBlendShapeIndex >= 0) {
				_lowerLipPressInitialValue = _bodyRenderer.GetBlendShapeWeight (_lowerLipPressBodyBlendShapeIndex);
			}
			if (_upperLipUpBodyBlendShapeIndex >= 0) {
				_upperLipUpInitialValue = _bodyRenderer.GetBlendShapeWeight (_upperLipUpBodyBlendShapeIndex);
			}
			if (_lowerLipDownBodyBlendShapeIndex >= 0) {
				_lowerLipDownInitialValue = _bodyRenderer.GetBlendShapeWeight (_lowerLipDownBodyBlendShapeIndex);
			}
			if (_mouthFrownBodyBlendShapeIndex >= 0) {
				_mouthFrownInitialValue = _bodyRenderer.GetBlendShapeWeight (_mouthFrownBodyBlendShapeIndex);
			}
			if (_mouthPuckerBodyBlendShapeIndex >= 0) {
				_mouthPuckerInitialValue = _bodyRenderer.GetBlendShapeWeight (_mouthPuckerBodyBlendShapeIndex);
			}
			if (_mouthOpenBodyBlendShapeIndex >= 0) {
				_mouthOpenInitialValue = _bodyRenderer.GetBlendShapeWeight (_mouthOpenBodyBlendShapeIndex);
			}
			#endregion
		}
		if (_mouthRenderer) 
		{
			#region MouthCheckIndex
			if (_mouthOpenMouthBlendShapeIndex >= 0) 
			{
				_mouthOpenMouthInitialValue = _mouthRenderer.GetBlendShapeWeight (_mouthOpenMouthBlendShapeIndex);
			}
			#endregion
		}
		if (_lensEyesRenderer) 
		{
			#region LensCheckIndex
			//check for lens Index
			if (_lensRightEyeSmileBlendShapeIndex >= 0)
			{
				_mouthOpenMouthInitialValue = _lensEyesRenderer.GetBlendShapeWeight(_mouthOpenMouthBlendShapeIndex);
			}
			
			if (_lensRightEyeBlinkBlendShapeIndex >= 0)
			{
				_mouthOpenMouthInitialValue = _lensEyesRenderer.GetBlendShapeWeight(_mouthOpenMouthBlendShapeIndex);
			}
			
			if (_lensRightEyeOpenBlendShapeIndex >= 0)
			{
				_mouthOpenMouthInitialValue = _lensEyesRenderer.GetBlendShapeWeight(_mouthOpenMouthBlendShapeIndex);
			}
			
			if (_lensRightEyeLookUpBlendShapeIndex >= 0)
			{
				_mouthOpenMouthInitialValue = _lensEyesRenderer.GetBlendShapeWeight(_mouthOpenMouthBlendShapeIndex);
			}
			
			if (_lensLeftEyeSmileBlendShapeIndex >= 0)
			{
				_mouthOpenMouthInitialValue = _lensEyesRenderer.GetBlendShapeWeight(_mouthOpenMouthBlendShapeIndex);
			}
			
			if (_lensLeftEyeBlinkBlendShapeIndex >= 0)
			{
				_mouthOpenMouthInitialValue = _lensEyesRenderer.GetBlendShapeWeight(_mouthOpenMouthBlendShapeIndex);
			}
			
			if (_lensLeftEyeOpenBlendShapeIndex >= 0)
			{
				_mouthOpenMouthInitialValue = _lensEyesRenderer.GetBlendShapeWeight(_mouthOpenMouthBlendShapeIndex);
			}
			
			if (_lensLeftEyeLookUpBlendShapeIndex >= 0)
			{
				_mouthOpenMouthInitialValue = _lensEyesRenderer.GetBlendShapeWeight(_mouthOpenMouthBlendShapeIndex);
			}
			#endregion
		}
		#region CheckObjects
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
		#endregion
		GameObject sceneObject = GameObject.Find("Scene");
		_faceTrackingManager = sceneObject ? sceneObject.GetComponent<FaceTrackingManager>() : null;
	}
	
	void Start()	
	{
	}
	
	void Update()
	{
		float rBrowDown = 0;
		float lBrowDown = 0;
		float rBrowUp = 0;
		float lBrowUp = 0;
		float rBrowIn = 0;
		float lBrowIn = 0;
		float rBlink = 0;
		float lBlink = 0;
		float rLowerLidUp = 0;
		float lLowerLidUp = 0;
		float rSmile = 0;
		float lSmile = 0;
		float rSmileCorrective = 0;
		float lSmileCorrective = 0;
		float lMouthSqueeze = 0;
		float rMouthSqueeze = 0;
		float upperLipPress = 0;
		float lowerLipPress = 0;
		float lowerLipDown = 0;
		float upperLipUp = 0;
		float mouthFrown = 0;
		float mouthPucker = 0;
		float mouthOpen = 0;
		float mouthOpenMouth = 0;
		
		//EyeLensValues
		float lensRightEyeSmile = 0;
		float lensRightEyeBlink = 0;
		float lensRightEyeOpen = 0;
		float lensRightEyeLookUp = 0;
		float lensLeftEyeSmile = 0;
		float lensLeftEyeBlinkx = 0;
		float lensLeftEyeOpen = 0;
		float lensLeftEyeLookUp = 0;
		
		if (_faceTrackingManager && !_faceTrackingManager._manualMode)
		{
			rBrowDown = _faceTrackingManager.RBrowDown;
			lBrowDown = _faceTrackingManager.LBrowDown;
			rBrowUp = _faceTrackingManager.RBrowUp;
			lBrowUp = _faceTrackingManager.LBrowUp;
			rBrowIn = _faceTrackingManager.RBrowIn;
			lBrowIn = _faceTrackingManager.LBrowIn;
			rBlink = _faceTrackingManager.RBlink;
			lBlink = _faceTrackingManager.LBlink;
			lLowerLidUp = _faceTrackingManager.LLowLidUp;
			rLowerLidUp = _faceTrackingManager.RLowLidUp;
			lSmile = _faceTrackingManager.LMouthSmile;
			rSmile = _faceTrackingManager.RMouthSmile;
			lSmileCorrective = _faceTrackingManager.LMouthSmileCorrect;
			rSmileCorrective = _faceTrackingManager.RMouthSmileCorrect;
			lMouthSqueeze = _faceTrackingManager.LMouthSqueeze;
			rMouthSqueeze = _faceTrackingManager.RMouthSqueeze;
			upperLipPress = _faceTrackingManager.UpperLipPress;
			lowerLipPress = _faceTrackingManager.LowerLipPress;
			lowerLipDown = _faceTrackingManager.LowerLipDown;
			upperLipUp = _faceTrackingManager.UpperLipUp;
			mouthFrown = _faceTrackingManager.MouthFrown;
			mouthPucker = _faceTrackingManager.MouthPucker;
			mouthOpen = mouthOpenMouth = _faceTrackingManager.MouthOpen;
			
			lensRightEyeSmile = 0;
			lensRightEyeBlink = 0;
			lensRightEyeOpen = 0;
			lensRightEyeLookUp = 0;
			lensLeftEyeSmile = 0;
			lensLeftEyeBlinkx = 0;
			lensLeftEyeOpen = 0;
			lensLeftEyeLookUp = 0;
		}
		
		float value;
		//Old Style
		//		SetBodyBlendShapeValue (rBrowUp, _rBrowUpBodyBlendShapeIndex, _rBrowUpInitialValue, ref _executeRBrowUpSmoothing, out value);
		//		SetBodyBlendShapeValue (lBrowUp, _lBrowUpBodyBlendShapeIndex, _lBrowUpInitialValue, ref _executeLBrowUpSmoothing, out value);
		//		SetBodyBlendShapeValue (rBrowDown, _rBrowDownBodyBlendShapeIndex, _rBrowDownInitialValue, ref _executeRBrowDownSmoothing, out value);
		//		SetBodyBlendShapeValue (lBrowDown, _lBrowDownBodyBlendShapeIndex, _lBrowDownInitialValue, ref _executeLBrowDownSmoothing, out value);
		//		SetBodyBlendShapeValue (rBrowIn, _rBrowInBodyBlendShapeIndex, _rBrowInInitialValue, ref _executeRBrowInSmoothing, out value);
		//		SetBodyBlendShapeValue (lBrowIn, _lBrowInBodyBlendShapeIndex, _lBrowInInitialValue, ref _executeLBrowInSmoothing, out value);
		//		SetBodyBlendShapeValue (rBlink, _rBlinkBodyBlendShapeIndex, _rBlinkInitialValue, ref _executeRBlinkSmoothing, out value);
		//		SetBodyBlendShapeValue (lBlink, _lBlinkBodyBlendShapeIndex, _lBlinkInitialValue, ref _executeLBlinkSmoothing, out value);
		//		SetBodyBlendShapeValue (rLowerLidUp, _rLowerLidUpBodyBlendShapeIndex, _rLowerLidUpInitialValue, ref _executeRLowerLidUpSmoothing, out value);
		//		SetBodyBlendShapeValue (lLowerLidUp, _lLowerLidUpBodyBlendShapeIndex, _lLowerLidUpInitialValue, ref _executeLLowerLidUpSmoothing, out value);
		//		SetBodyBlendShapeValue (rSmile, _rSmileBodyBlendShapeIndex, _rSmileInitialValue, ref _executeRSmileSmoothing, out value);
		//		SetBodyBlendShapeValue (lSmile, _lSmileBodyBlendShapeIndex, _lSmileInitialValue, ref _executeLSmileSmoothing, out value);
		//		SetBodyBlendShapeValue (rSmileCorrective, _rSmileCorrectiveBodyBlendShapeIndex, _rSmileCorrectiveInitialValue, ref _executeRSmileCorrectiveSmoothing, out value);
		//		SetBodyBlendShapeValue (lSmileCorrective, _lSmileCorrectiveBodyBlendShapeIndex, _lSmileCorrectiveInitialValue, ref _executeLSmileCorrectiveSmoothing, out value);
		//		SetBodyBlendShapeValue (lMouthSqueeze, _lMouthSqueezeBodyBlendShapeIndex, _lMouthSqueezeInitialValue, ref _executeLMouthSqueezeSmoothing, out value);
		//		SetBodyBlendShapeValue (rMouthSqueeze, _rMouthSqueezeBodyBlendShapeIndex, _rMouthSqueezeInitialValue, ref _executeRMouthSqueezeSmoothing, out value);
		//		SetBodyBlendShapeValue (upperLipPress, _upperLipPressBodyBlendShapeIndex, _upperLipPressInitialValue, ref _executeUpperLipPressSmoothing, out value);
		//		SetBodyBlendShapeValue (lowerLipPress, _lowerLipPressBodyBlendShapeIndex, _lowerLipPressInitialValue, ref _executeLowerLipPressSmoothing, out value);
		//		SetBodyBlendShapeValue (upperLipUp, _upperLipUpBodyBlendShapeIndex, _upperLipUpInitialValue, ref _executeUpperLipUpSmoothing, out value);
		//		SetBodyBlendShapeValue (lowerLipDown, _lowerLipDownBodyBlendShapeIndex, _lowerLipDownInitialValue, ref _executeLowerLipDownSmoothing, out value);
		//		//SetBodyBlendShapeValue (mouthFrown, _mouthFrownBodyBlendShapeIndex, _mouthFrownInitialValue, ref _executeMouthFrownSmoothing, out value);
		//		SetBodyBlendShapeValue (mouthPucker, _mouthPuckerBodyBlendShapeIndex, _mouthPuckerInitialValue, ref _executeMouthPuckerSmoothing, out value);
		//		SetBodyBlendShapeValue (mouthOpen, _mouthOpenBodyBlendShapeIndex, _mouthOpenInitialValue, ref _executeMouthOpenSmoothing, out value);
		//		SetMouthBlendShapeValue (mouthOpenMouth, _mouthOpenMouthBlendShapeIndex, _mouthOpenMouthInitialValue, ref _executeMouthOpenMouthSmoothing, out value);
		
		//BodyRenderer
		SetBlendShapeValue (rBrowUp, _rBrowUpBodyBlendShapeIndex, _rBrowUpInitialValue, ref _executeRBrowUpSmoothing, out value, _bodyRenderer);
		SetBlendShapeValue (lBrowUp, _lBrowUpBodyBlendShapeIndex, _lBrowUpInitialValue, ref _executeLBrowUpSmoothing, out value, _bodyRenderer);
		SetBlendShapeValue (rBrowDown, _rBrowDownBodyBlendShapeIndex, _rBrowDownInitialValue, ref _executeRBrowDownSmoothing, out value, _bodyRenderer);
		SetBlendShapeValue (lBrowDown, _lBrowDownBodyBlendShapeIndex, _lBrowDownInitialValue, ref _executeLBrowDownSmoothing, out value, _bodyRenderer);
		SetBlendShapeValue (rBrowIn, _rBrowInBodyBlendShapeIndex, _rBrowInInitialValue, ref _executeRBrowInSmoothing, out value, _bodyRenderer);
		SetBlendShapeValue (lBrowIn, _lBrowInBodyBlendShapeIndex, _lBrowInInitialValue, ref _executeLBrowInSmoothing, out value, _bodyRenderer);
		SetBlendShapeValue (rBlink, _rBlinkBodyBlendShapeIndex, _rBlinkInitialValue, ref _executeRBlinkSmoothing, out value, _bodyRenderer);
		SetBlendShapeValue (lBlink, _lBlinkBodyBlendShapeIndex, _lBlinkInitialValue, ref _executeLBlinkSmoothing, out value, _bodyRenderer);
		SetBlendShapeValue (rLowerLidUp, _rLowerLidUpBodyBlendShapeIndex, _rLowerLidUpInitialValue, ref _executeRLowerLidUpSmoothing, out value, _bodyRenderer);
		SetBlendShapeValue (lLowerLidUp, _lLowerLidUpBodyBlendShapeIndex, _lLowerLidUpInitialValue, ref _executeLLowerLidUpSmoothing, out value, _bodyRenderer);
		SetBlendShapeValue (rSmile, _rSmileBodyBlendShapeIndex, _rSmileInitialValue, ref _executeRSmileSmoothing, out value, _bodyRenderer);
		SetBlendShapeValue (lSmile, _lSmileBodyBlendShapeIndex, _lSmileInitialValue, ref _executeLSmileSmoothing, out value, _bodyRenderer);
		SetBlendShapeValue (rSmileCorrective, _rSmileCorrectiveBodyBlendShapeIndex, _rSmileCorrectiveInitialValue, ref _executeRSmileCorrectiveSmoothing, out value, _bodyRenderer);
		SetBlendShapeValue (lSmileCorrective, _lSmileCorrectiveBodyBlendShapeIndex, _lSmileCorrectiveInitialValue, ref _executeLSmileCorrectiveSmoothing, out value, _bodyRenderer);
		SetBlendShapeValue (lMouthSqueeze, _lMouthSqueezeBodyBlendShapeIndex, _lMouthSqueezeInitialValue, ref _executeLMouthSqueezeSmoothing, out value, _bodyRenderer);
		SetBlendShapeValue (rMouthSqueeze, _rMouthSqueezeBodyBlendShapeIndex, _rMouthSqueezeInitialValue, ref _executeRMouthSqueezeSmoothing, out value, _bodyRenderer);
		SetBlendShapeValue (upperLipPress, _upperLipPressBodyBlendShapeIndex, _upperLipPressInitialValue, ref _executeUpperLipPressSmoothing, out value, _bodyRenderer);
		SetBlendShapeValue (lowerLipPress, _lowerLipPressBodyBlendShapeIndex, _lowerLipPressInitialValue, ref _executeLowerLipPressSmoothing, out value, _bodyRenderer);
		SetBlendShapeValue (upperLipUp, _upperLipUpBodyBlendShapeIndex, _upperLipUpInitialValue, ref _executeUpperLipUpSmoothing, out value, _bodyRenderer);
		SetBlendShapeValue (lowerLipDown, _lowerLipDownBodyBlendShapeIndex, _lowerLipDownInitialValue, ref _executeLowerLipDownSmoothing, out value, _bodyRenderer);
		//SetBodyBlendShapeValue (mouthFrown, _mouthFrownBodyBlendShapeIndex, _mouthFrownInitialValue, ref _executeMouthFrownSmoothing, out value);
		SetBlendShapeValue (mouthPucker, _mouthPuckerBodyBlendShapeIndex, _mouthPuckerInitialValue, ref _executeMouthPuckerSmoothing, out value, _bodyRenderer);
		SetBlendShapeValue (mouthOpen, _mouthOpenBodyBlendShapeIndex, _mouthOpenInitialValue, ref _executeMouthOpenSmoothing, out value, _bodyRenderer);
		
		//MouthRenderer
		SetBlendShapeValue (mouthOpenMouth, _mouthOpenMouthBlendShapeIndex, _mouthOpenMouthInitialValue, ref _executeMouthOpenMouthSmoothing, out value, _mouthRenderer);
		
		//LensRenderer
		
		
		//		Debug.Log ("mouthOpen" + mouthOpen);
		//		Debug.Log ("mouthOpenMouth" + mouthOpenMouth);
		//		float rightEyebrowUp = 0;
		//		float leftEyebrowUp = 0;
		//		float eyebrowsDown = 0;
		//		float rightEyeClose = 0;
		//		float leftEyeClose = 0;
		//		float mouthOpen = 0;
		//		float mouthSmile = 0;
		//		float mouthKiss = 0;
		//		float mouthLeft = 0;
		//		float mouthRight = 0;
		//		float mouthE = 0;
		//		float mouthU = 0;
		//		float mouthMBP = 0;
		//
		//		if (_faceTrackingManager && !_faceTrackingManager._manualMode)
		//		{
		//			rightEyebrowUp = _faceTrackingManager.RightEyebrowUp;
		//			leftEyebrowUp = _faceTrackingManager.LeftEyebrowUp;
		//			eyebrowsDown = _faceTrackingManager.EyebrowsDown;
		//			rightEyeClose = _faceTrackingManager.RightEyeClose;
		//			leftEyeClose = _faceTrackingManager.LeftEyeClose;
		//			mouthOpen = _faceTrackingManager.MouthOpen;
		//			mouthSmile = _faceTrackingManager.MouthSmile;
		//			mouthKiss = _faceTrackingManager.MouthKiss;
		//			mouthLeft = _faceTrackingManager.MouthLeft;
		//			mouthRight = _faceTrackingManager.MouthRight;
		//			mouthE = _faceTrackingManager.MouthE;
		//			mouthU = _faceTrackingManager.MouthU;
		//			mouthMBP = _faceTrackingManager.MouthMBP;
		//		}
		//
		//
		//
		//		//Debug.Log (mouthE);
		//		float value;
		//
		//		// Eyebrows Up
		//		SetBodyBlendShapeValue(leftEyebrowUp, _rightEyebrowUpBodyBlendShapeIndex, _rightEyebrowUpInitialValue, ref _executeRightEyebrowSmoothing, out value);
		//		SetBodyBlendShapeValue(rightEyebrowUp, _leftEyebrowUpBodyBlendShapeIndex, _leftEyebrowUpInitialValue, ref _executeLeftEyebrowSmoothing, out value);
		//
		//		// Eyes Open
		//		SetEyeClose(rightEyeClose, _leftEyeCloseBodyBlendShapeIndex, _leftEyeBottomLidUpBodyBlendShapeIndex, _leftEyeCloseInitialValue, ref _executeLeftEyeCloseSmoothing);
		//		SetEyeClose(leftEyeClose, _rightEyeCloseBodyBlendShapeIndex, _rightEyeBottomLidUpBodyBlendShapeIndex, _rightEyeCloseInitialValue, ref _executeRightEyeCloseSmoothing);
		//
		//		// Mouth Open
		//		SetMouthOpen(mouthOpen);
		//
		//		// Mouth Smile
		//		SetBodyBlendShapeValue(mouthSmile, _mouthSmileBodyBlendShapeIndex, _mouthSmileInitialValue, ref _executeMouthSmileSmoothing, out value);
		//
		//		// Eyebrows Down
		//		SetBodyBlendShapeValue(eyebrowsDown, _eyebrowsDownBodyBlendShapeIndex, _eyebrowsDownInitialValue, ref _executeEyebrowsDownSmoothing, out value);
		//
		//		// Mouth E
		//		SetBodyBlendShapeValue(mouthE, _mouthEBodyBlendShapeIndex, _mouthEInitialValue, ref _executeMouthESmoothing, out value);
		//		SetMouthBlendShapeValue (mouthE, _mouthEBlendShapeIndex, _mouthEInitialValue, ref _executeMouthUSmoothing, out value);
		//
		//		// Mouth U
		//		SetBodyBlendShapeValue (mouthU, _mouthBodyUBlendShapeIndex, _mouthUInitialValue, ref _executeMouthUSmoothing, out value);
		//		SetMouthBlendShapeValue (mouthU, _mouthUBlendShapeIndex, _mouthUInitialValue, ref _executeMouthUSmoothing, out value);
		//
		//		// Mouth MBP
		//		SetBodyBlendShapeValue (mouthMBP, _mouthMBPBlendShapeIndex, _mouthMBPInitialValue, ref _executeMouthMBPSmoothing, out value);
		//
		//		// Mouth Kiss
		//		SetBodyBlendShapeValue(mouthKiss, _mouthKissBodyBlendShapeIndex, _mouthKissInitialValue, ref _executeMouthKissSmoothing, out value);
		//
		//		// Mouth Left Right
		//		SetBodyBlendShapeValue(mouthLeft, _mouthLeftBodyBlendShapeIndex, _mouthLeftInitialValue, ref _executeMouthLeftSmoothing, out value);
		//		SetBodyBlendShapeValue(mouthRight, _mouthRightBodyBlendShapeIndex, _mouthRightInitialValue, ref _executeMouthRightSmoothing, out value);
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
			//SetJawPosition(_faceTrackingManager.JawMoveLeftRight);
			
			// Back Movement
			SetBackRotation(_faceTrackingManager.BackPitch);
		}
	}
	
	#endregion
	
	#region Private Methods
	
	//	private void SetEyeClose(float rawValue, int eyeUpperLidBlendShapeIndex, int eyeLowerLidBlendShapeIndex, float eyeInitialValue, ref bool executeEyeSmoothing)
	//	{
	//		float value;
	//		if (SetBodyBlendShapeValue(rawValue, eyeUpperLidBlendShapeIndex, eyeInitialValue, ref executeEyeSmoothing, out value))
	//		{
	//			if (eyeLowerLidBlendShapeIndex >= 0)
	//			{
	//				_bodyRenderer.SetBlendShapeWeight(eyeLowerLidBlendShapeIndex, value);
	//			}
	//		}
	//	}
	//	
	//	private void SetMouthOpen(float rawValue)
	//	{
	//		float value;
	//		if (SetBodyBlendShapeValue(rawValue, _mouthOpenBodyBlendShapeIndex, _mouthOpenInitialValue, ref _executeMouthOpenSmoothing, out value))
	//		{
	//			if (_mouthRenderer && _mouthOpenMouthBlendShapeIndex >= 0)
	//			{
	//				_mouthRenderer.SetBlendShapeWeight(_mouthOpenMouthBlendShapeIndex, value);
	//			}
	//		}
	//	}
	private float CalcBlendShapeValueForRenderer(float rawValue, int blendShapeIndex, float initialValue, ref bool executeSmoothing, SkinnedMeshRenderer renderer)
	{
		float value = 0f;
		float currentValue = renderer.GetBlendShapeWeight(blendShapeIndex);
		
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
	
	private bool SetBlendShapeValue(float rawValue, int blendShapeIndex, float initialValue, ref bool executeSmoothing, out float value, SkinnedMeshRenderer renderer)
	{
		value = 0;
		
		if (_mouthRenderer && blendShapeIndex >= 0)
		{
			value = CalcBlendShapeValueForRenderer(rawValue, blendShapeIndex, initialValue, ref executeSmoothing, renderer );
			//value = Mathf.Clamp(value, 0, 100);
			renderer.SetBlendShapeWeight(blendShapeIndex, value);
			
			return true;
		}
		
		return false;
	}
	
	//	private bool SetMouthBlendShapeValue(float rawValue, int blendShapeIndex, float initialValue, ref bool executeSmoothing, out float value)
	//	{
	//		value = 0;
	//		
	//		if (_mouthRenderer && blendShapeIndex >= 0)
	//		{
	//			value = CalcBlendShapeValueMouth(rawValue, blendShapeIndex, initialValue, ref executeSmoothing);
	//			//value = Mathf.Clamp(value, 0, 100);
	//			_mouthRenderer.SetBlendShapeWeight(blendShapeIndex, value);
	//			
	//			return true;
	//		}
	//		
	//		return false;
	//	}
	//	
	//	private bool SetBodyBlendShapeValue(float rawValue, int blendShapeIndex, float initialValue, ref bool executeSmoothing, out float value)
	//	{
	//		value = 0;
	//		
	//		if (_bodyRenderer && blendShapeIndex >= 0)
	//		{
	//			value = CalcBlendShapeValue(rawValue, blendShapeIndex, initialValue, ref executeSmoothing);
	//			//value = Mathf.Clamp(value, 0, 100);
	//			_bodyRenderer.SetBlendShapeWeight(blendShapeIndex, value);
	//			
	//			return true;
	//		}
	//		
	//		return false;
	//	}
	
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
	
	
	
	//	private float CalcBlendShapeValueMouth(float rawValue, int blendShapeIndex, float initialValue, ref bool executeSmoothing)
	//	{
	//		float value = 0f;
	//		float currentValue = _mouthRenderer.GetBlendShapeWeight(blendShapeIndex);
	//		
	//		if (_faceTrackingManager.IsPoseValid)
	//		{
	//			if (executeSmoothing)
	//			{
	//				value = rawValue;
	//				if (Mathf.Abs(value - currentValue) > MIN_SMOOTH_BLEND_SHAPE)
	//				{
	//					value = Mathf.Lerp(currentValue, value, RETURN_FROM_FAILURE_SMOOTH_FACTOR);
	//				}
	//				else
	//				{
	//					executeSmoothing = false;
	//				}
	//			}
	//			else
	//			{
	//				value = rawValue;
	//			}
	//		}
	//		else
	//		{
	//			executeSmoothing = true;
	//			
	//			value = Mathf.Lerp(currentValue, initialValue, FAILURE_SMOOTH_FACTOR);
	//		}
	//		
	//		return value;
	//	}
	//	
	//	private float CalcBlendShapeValue(float rawValue, int blendShapeIndex, float initialValue, ref bool executeSmoothing)
	//	{
	//		float value = 0f;
	//		float currentValue = _bodyRenderer.GetBlendShapeWeight(blendShapeIndex);
	//		
	//		if (_faceTrackingManager.IsPoseValid)
	//		{
	//			if (executeSmoothing)
	//			{
	//				value = rawValue;
	//				if (Mathf.Abs(value - currentValue) > MIN_SMOOTH_BLEND_SHAPE)
	//				{
	//					value = Mathf.Lerp(currentValue, value, RETURN_FROM_FAILURE_SMOOTH_FACTOR);
	//				}
	//				else
	//				{
	//					executeSmoothing = false;
	//				}
	//			}
	//			else
	//			{
	//				value = rawValue;
	//			}
	//		}
	//		else
	//		{
	//			executeSmoothing = true;
	//			
	//			value = Mathf.Lerp(currentValue, initialValue, FAILURE_SMOOTH_FACTOR);
	//		}
	//		
	//		return value;
	//	}
	
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
