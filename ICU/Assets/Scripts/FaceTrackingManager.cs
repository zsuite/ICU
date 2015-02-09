/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2014 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using UnityEngine;
using System.Runtime.InteropServices;

public class FaceTrackingManager : MonoBehaviour
{
	#region Constants
	
	private const int MAX_CALIBRATION_FRAMES = 5;

	private const float OPTIMAL_RELATIVE_FACE_WIDTH = 203f;
	private const float OPTIMAL_RELATIVE_FACE_HEIGHT = 55.5f;

	private const float OPTIMAL_EYEBROW_UP_MAX_DISTANCE = 40f;
	private const float EYEBROW_UP_INITIAL_DISTANCE = 23f;

	private const float OPTIMAL_EYE_CLOSE_MAX_DISTANCE = 4.5f;
	private const float EYE_CLOSE_INITIAL_DISTANCE = 10f;
	
	private const float OPTIMAL_MOUTH_OPEN_MAX_DISTANCE = 41f;
	private const float MOUTH_OPEN_INITIAL_DISTANCE = 8f;
	
	private const float OPTIMAL_JAW_MOVE_LEFT_RIGHT_MAX_DISTANCE = 3.5f;
	private const float JAW_MOVE_LEFT_RIGHT_INITIAL_DISTANCE = -0.9f;
	
	private const float MOUTH_SMILE_MULTIPLIER = 2f;

	private const float MAX_BACK_PITCH = 400f;

	private const float NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE = 100f;
	private const float NORMALIZE_MAX_EYES_MOVE_LEFT_RIGHT_VALUE = 120f;
	private const float NORMALIZE_MAX_BACK_PITCH_VALUE = 50f;
	private const float NORMALIZE_MAX_JAW_MOVE_LEFT_RIGHT_VALUE = 0.016f;

	private const float SMOOTH_FACTOR = 0.5f;

	#endregion
	
	#region Public Members
	
	public bool _manualMode = false;
	public bool _recordToExternalFile;
	//public GameObject _operatorVideoScreen = null;
	
	#endregion
	
	#region Private Members
	
	private PXCMSenseManager _pxcmSenseManager = null;
	private PXCMFaceData _pxcmFaceData = null;
	private int _maxLandmarks = 0;
	private	int _currentLandmark = 0;
	private Texture2D _operatorVideoTexture = null;
	private bool _showOperatorVideo = false;
	private Vector3 _oldHeadPose = Vector3.zero;
	
	private bool _isPoseValid = false;

	private int _currentCalibrationFrame = 0;
	private bool _isCalibrating = false;
	private bool _isPoseInitialized = false;

	private Vector3 _neutralHeadPose = Vector3.zero;
	
	private PXCMCapture.Device.MirrorMode _cameraMirrorMode;
	
	private PXCMFaceData.DetectionData _pxcmFaceDetectionData = null;
	private float _baseFaceAverageDepth = -1f;

	private float _leftEyebrowUp = 0f;
	private float _rightEyebrowUp = 0f;

	private float _leftEyeClose = 0f;
	private float _rightEyeClose = 0f;
	private float _eyesMoveLeftRight = 0f;

	private float _mouthOpen = 0f;
	private float _mouthSmile = 0f;
	private float _mouthLeft = 0f;
	private float _mouthRight = 0f;
	private float _jawMoveLeftRight = 0f;
	private float _headYaw = 0f;
	private float _headPitch = 0f;
	private float _headRoll = 0f;

	private float _backPitch = 0f;

	#endregion

	#region Properties

	public float LeftEyebrowUp
	{
		get
		{
			return _leftEyebrowUp;
		}
	}

	public float RightEyebrowUp
	{
		get
		{
			return _rightEyebrowUp;
		}
	}
	
	public float LeftEyeClose
	{
		get
		{
			return _leftEyeClose;
		}
	}
	
	public float RightEyeClose
	{
		get
		{
			return _rightEyeClose;
		}
	}
	
	public float EyesMoveLeftRight
	{
		get
		{
			return _eyesMoveLeftRight;
		}
	}

	public float MouthOpen
	{
		get
		{
			return _mouthOpen;
		}
	}
	
	public float MouthSmile
	{
		get
		{
			return _mouthSmile;
		}
	}
	
	public float MouthLeft
	{
		get
		{
			return _mouthLeft;
		}
	}
	
	public float MouthRight
	{
		get
		{
			return _mouthRight;
		}
	}
	
	public float JawMoveLeftRight
	{
		get
		{
			return _jawMoveLeftRight;
		}
	}

	public float HeadYaw
	{
		get
		{
			return _headYaw;
		}
	}
	
	public float HeadPitch
	{
		get
		{
			return _headPitch;
		}
	}
	
	public float HeadRoll
	{
		get
		{
			return _headRoll;
		}
	}
	
	public float BackPitch
	{
		get
		{
			return _backPitch;
		}
	}
	
	public bool IsPoseValid
	{
		get
		{
			return _isPoseValid;
		}
	}
	
	public bool IsMirrorMode
	{
		get
		{
			return (_cameraMirrorMode == PXCMCapture.Device.MirrorMode.MIRROR_MODE_HORIZONTAL);
		}
	}

	#endregion

	#region Handlers
	
	void Awake()
	{
	}
	
	void Start()
	{
		InitializeRealSense();
	}
	
	void Update()
	{



		if (_pxcmSenseManager == null)
		{
			return;
		}
		
		// Wait until any frame data is available
		if (_pxcmSenseManager.AcquireFrame(true) < pxcmStatus.PXCM_STATUS_NO_ERROR)
		{
			return;
		}
		
		if (_showOperatorVideo)
		{
			// Retrieve the new color image from the camera and draw it on the screen
			UpdateColorImage();
		}
		
		UpdateFace();
		
		// Update calibration if in calibration step
		UpdateCalibration();
		
		// Process the next frame
		_pxcmSenseManager.ReleaseFrame();
	}

	public void OnGUI()
	{
		if (Event.current.type == EventType.KeyDown)
		{
			if ((Event.current.keyCode == KeyCode.DownArrow) || (Event.current.keyCode == KeyCode.UpArrow))
			{
				if (Event.current.keyCode == KeyCode.DownArrow)
				{
					_currentLandmark--;
					
					if (_currentLandmark < 0)
					{
						_currentLandmark = 77;
					}
				}
				else if (Event.current.keyCode == KeyCode.UpArrow)
				{
					_currentLandmark++;
					
					if (_currentLandmark > 77)
					{
						_currentLandmark = 0;
					}
				}
				
				Debug.Log("Landmark: " + _currentLandmark.ToString());
			}
			else if (Event.current.keyCode == KeyCode.R)
			{
				// Remembers the current location of landmarks and pose and sets it as the neutral pose
				StartCalibration();
			}
			else if (Event.current.keyCode == KeyCode.I)
			{
				_showOperatorVideo = !_showOperatorVideo;
			//	_operatorVideoScreen.renderer.enabled = _showOperatorVideo;
			}
		}
	}
	
	void OnDisable()
	{
		if (_pxcmSenseManager == null)
		{
			return;
		}
		
		if (_pxcmFaceData != null) 
		{
			_pxcmFaceData.Dispose();
		}
		
		_pxcmSenseManager.Dispose();
		_pxcmSenseManager = null;
	}
	
	#endregion
	
	#region Private Methods
	
	private void InitializeRealSense()
	{
		_pxcmSenseManager = PXCMSenseManager.CreateInstance();
		if (_pxcmSenseManager == null) 
		{
			Debug.Log("PXCMSenseManager.CreateInstance() failed.");
			return;
		}
		
		pxcmStatus status = _pxcmSenseManager.EnableFace();
		if (status < pxcmStatus.PXCM_STATUS_NO_ERROR)
		{
			Debug.Log("PXCMSenseManager.EnableFace() failed.");
			return;
		}
		
		PXCMCaptureManager pxcmCaptureManager = _pxcmSenseManager.QueryCaptureManager();
		if (pxcmCaptureManager == null)
		{
			Debug.Log("PXCMSenseManager.QueryCaptureManager() failed.");
			return;
		}
		
		if (_recordToExternalFile)
		{
			var fileName = "Record_" + System.DateTime.Now.Ticks + ".rssdk";
			if (pxcmCaptureManager.SetFileName(fileName, true) < pxcmStatus.PXCM_STATUS_NO_ERROR)
			{
				Debug.Log("PXCMCaptureManager.SetFileName() failed.");
				return;
			}
		}
		
		PXCMFaceModule pxcmFaceModule = _pxcmSenseManager.QueryFace();
		if (pxcmFaceModule == null)
		{
			Debug.Log("PXCMSenseManager.QueryFace() failed.");
			return;
		}
		
		PXCMFaceConfiguration pxcmFaceConfiguration = pxcmFaceModule.CreateActiveConfiguration();		
		if (pxcmFaceConfiguration == null)
		{
			Debug.Log("PXCMFaceModule.CreateActiveConfiguration() failed.");
			return;
		}
		
		_pxcmFaceData = pxcmFaceModule.CreateOutput();
		if (_pxcmFaceData == null)
		{
			Debug.Log("PXCMFaceModule.CreateOutput() failed.");
			return;
		}
		
		_maxLandmarks = pxcmFaceConfiguration.landmarks.numLandmarks;
		
		PXCMFaceConfiguration.ExpressionsConfiguration pxcmExpressionsConfiguration = pxcmFaceConfiguration.QueryExpressions();
		if (pxcmExpressionsConfiguration == null)
		{
			Debug.Log("PXCMFaceConfiguration.QueryExpressions() failed.");
			return;
		}
		
		pxcmExpressionsConfiguration.Enable();
		pxcmExpressionsConfiguration.EnableAllExpressions();
		
		pxcmFaceConfiguration.detection.isEnabled = true;
		pxcmFaceConfiguration.landmarks.isEnabled = true;
		pxcmFaceConfiguration.pose.isEnabled =true;
		pxcmFaceConfiguration.ApplyChanges();
		pxcmFaceConfiguration.Dispose();
		

		pxcmCaptureManager.FilterByStreamProfiles(PXCMCapture.StreamType.STREAM_TYPE_COLOR, 960, 540, 0);
		status = _pxcmSenseManager.Init();
		if (status < pxcmStatus.PXCM_STATUS_NO_ERROR) 
		{
			pxcmCaptureManager.FilterByStreamProfiles(PXCMCapture.StreamType.STREAM_TYPE_COLOR, 640, 480, 0);
			status = _pxcmSenseManager.Init();
			if (status < pxcmStatus.PXCM_STATUS_NO_ERROR) 
			{
				Debug.Log("PXCMCaptureManager.FilterByStreamProfiles() failed with status: " + status + ".");
				OnDisable();
				return;
			}
			else
			{
				_operatorVideoTexture = new Texture2D(640, 480, TextureFormat.ARGB32, false);
			}
		}
		else
		{
			_operatorVideoTexture = new Texture2D(960, 540, TextureFormat.ARGB32, false);
		}
		
	//	_operatorVideoScreen.renderer.material.mainTexture = _operatorVideoTexture;
	}
	
	private void UpdateFace()
	{
		_pxcmFaceData.Update();

		_isPoseValid = false;

		// Get the closest face
		PXCMFaceData.Face pxcmFace = _pxcmFaceData.QueryFaceByIndex(0);
		if (_pxcmFaceData.QueryNumberOfDetectedFaces() <= 0)
		{
			Debug.Log("PXCMFaceData.QueryFaceByIndex(0) failed.");
			return;
		}
		
		PXCMFaceData.LandmarksData pxcmLandmarksData = pxcmFace.QueryLandmarks();
		if (pxcmLandmarksData == null)
		{
			Debug.Log("PXCMFaceData.Face.QueryLandmarks() failed.");
			return;
		}
		
		PXCMFaceData.PoseData pxcmPoseData = pxcmFace.QueryPose();
		if (pxcmPoseData == null)
		{
			Debug.Log("PXCMFaceData.Face.QueryPose() failed.");
			return;
		}
		
		PXCMFaceData.LandmarkPoint[] pxcmLandmarkPoints = new PXCMFaceData.LandmarkPoint[_maxLandmarks];
		for (int i = 0; i < _maxLandmarks; ++i)
		{
			pxcmLandmarkPoints[i] = new PXCMFaceData.LandmarkPoint();
		}
		
		bool result = pxcmLandmarksData.QueryPoints(out pxcmLandmarkPoints);
		if (!result)
		{
			Debug.Log("PXCMFaceData.LandmarksData.QueryPoints() failed.");
			return;
		}
		
		PXCMFaceData.ExpressionsData pxcmExpressionsData = pxcmFace.QueryExpressions();
		if (pxcmExpressionsData == null)
		{
			Debug.Log("PXCMFaceData.Face.QueryExpressions() failed.");
			return;
		}
		
		if (_showOperatorVideo)
		{
			DrawLandmarks(pxcmLandmarkPoints);
		}
		
		PXCMFaceData.PoseEulerAngles pxcmPoseEulerAngles;
		result = pxcmPoseData.QueryPoseAngles(out pxcmPoseEulerAngles);
		if (!result)
		{
			Debug.Log("PXCMFaceData.PoseData.QueryPoseAngles() failed.");
			return;
		}

		_isPoseValid = true;

		Vector3 currentHeadPose = Vector3.zero;
		
		_cameraMirrorMode = PXCMCapture.Device.MirrorMode.MIRROR_MODE_DISABLED;

		PXCMCaptureManager pxcmCaptureManager = _pxcmSenseManager.QueryCaptureManager();
		if (pxcmCaptureManager != null)
		{
			PXCMCapture.Device pxcmCaptureDevice = pxcmCaptureManager.QueryDevice();
			if (pxcmCaptureDevice != null)
			{
				_cameraMirrorMode = pxcmCaptureDevice.QueryMirrorMode();
			}
		}

		// Update face

		float faceWidth = pxcmLandmarkPoints[53].image.x - pxcmLandmarkPoints[69].image.x;
		float faceHeight = pxcmLandmarkPoints[29].image.y - pxcmLandmarkPoints[26].image.y;

		float leftEyebrowUp = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[77].image.y, pxcmLandmarkPoints[7].image.y, pxcmPoseEulerAngles.pitch), OPTIMAL_RELATIVE_FACE_HEIGHT, faceHeight, EYEBROW_UP_INITIAL_DISTANCE, OPTIMAL_EYEBROW_UP_MAX_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true);
		float rightEyebrowUp = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[76].image.y, pxcmLandmarkPoints[2].image.y, pxcmPoseEulerAngles.pitch), OPTIMAL_RELATIVE_FACE_HEIGHT, faceHeight, EYEBROW_UP_INITIAL_DISTANCE, OPTIMAL_EYEBROW_UP_MAX_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true);

		float leftEyeClose = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[24].image.y, pxcmLandmarkPoints[20].image.y, pxcmPoseEulerAngles.pitch), OPTIMAL_RELATIVE_FACE_HEIGHT, faceHeight, EYE_CLOSE_INITIAL_DISTANCE, OPTIMAL_EYE_CLOSE_MAX_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true);
		float rightEyeClose = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[16].image.y, pxcmLandmarkPoints[12].image.y, pxcmPoseEulerAngles.pitch), OPTIMAL_RELATIVE_FACE_HEIGHT, faceHeight, EYE_CLOSE_INITIAL_DISTANCE, OPTIMAL_EYE_CLOSE_MAX_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true);

		float eyesMoveLeftRight = (pxcmLandmarkPoints[10].image.x - pxcmLandmarkPoints[76].image.x) / (pxcmLandmarkPoints[10].image.x - pxcmLandmarkPoints[14].image.x) * NORMALIZE_MAX_EYES_MOVE_LEFT_RIGHT_VALUE - NORMALIZE_MAX_EYES_MOVE_LEFT_RIGHT_VALUE / 2;
		_eyesMoveLeftRight = Mathf.Clamp(eyesMoveLeftRight, -NORMALIZE_MAX_EYES_MOVE_LEFT_RIGHT_VALUE, NORMALIZE_MAX_EYES_MOVE_LEFT_RIGHT_VALUE);

		float normalizedJawLandmarksDistance = GetNormalizedJawLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[51].image.x, pxcmLandmarkPoints[47].image.x, pxcmPoseEulerAngles.yaw), OPTIMAL_RELATIVE_FACE_WIDTH, faceWidth, JAW_MOVE_LEFT_RIGHT_INITIAL_DISTANCE, OPTIMAL_JAW_MOVE_LEFT_RIGHT_MAX_DISTANCE, NORMALIZE_MAX_JAW_MOVE_LEFT_RIGHT_VALUE, pxcmPoseEulerAngles.roll);
		float jawMoveLeftRight = Mathf.Lerp(_jawMoveLeftRight, normalizedJawLandmarksDistance, SMOOTH_FACTOR);

		if (_cameraMirrorMode == PXCMCapture.Device.MirrorMode.MIRROR_MODE_HORIZONTAL)
		{
			_leftEyebrowUp = rightEyebrowUp;
			_rightEyebrowUp = leftEyebrowUp;

			_leftEyeClose = rightEyeClose;
			_rightEyeClose = leftEyeClose;

			_jawMoveLeftRight = -jawMoveLeftRight;
		}
		else
		{
			_leftEyebrowUp = leftEyebrowUp;
			_rightEyebrowUp = rightEyebrowUp;
			
			_leftEyeClose = leftEyeClose;
			_rightEyeClose = rightEyeClose;
			
			_jawMoveLeftRight = jawMoveLeftRight;
		}

		float mouthLeftRight = normalizedJawLandmarksDistance / NORMALIZE_MAX_JAW_MOVE_LEFT_RIGHT_VALUE * NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE;

		if (mouthLeftRight > 0) 
		{
			_mouthRight = Mathf.Clamp(mouthLeftRight, 0, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE);
			_mouthLeft = 0;
		}
		else
		{
			_mouthLeft = Mathf.Clamp(-mouthLeftRight, 0, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE);
			_mouthRight = 0;
		}

		_mouthOpen = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[51].image.y, pxcmLandmarkPoints[47].image.y, pxcmPoseEulerAngles.pitch), OPTIMAL_RELATIVE_FACE_HEIGHT, faceHeight, MOUTH_OPEN_INITIAL_DISTANCE, OPTIMAL_MOUTH_OPEN_MAX_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true);

		PXCMFaceData.ExpressionsData.FaceExpressionResult pxcmFaceExpressionResult = new PXCMFaceData.ExpressionsData.FaceExpressionResult();
	
		result = pxcmExpressionsData.QueryExpression(PXCMFaceData.ExpressionsData.FaceExpression.EXPRESSION_SMILE, out pxcmFaceExpressionResult);
		if (result == true)
		{
			_mouthSmile = (float)pxcmFaceExpressionResult.intensity * MOUTH_SMILE_MULTIPLIER;
		}
		else
		{
			Debug.Log("Error querying expression: EXPRESSION_SMILE.");
		}
	
		// Update head

		pxcmPoseEulerAngles.pitch *= -1;
		pxcmPoseEulerAngles.roll *= -1;
		
		if (_cameraMirrorMode == PXCMCapture.Device.MirrorMode.MIRROR_MODE_HORIZONTAL)
		{
			currentHeadPose = new Vector3((float)pxcmPoseEulerAngles.yaw, -(float)pxcmPoseEulerAngles.pitch, (float)pxcmPoseEulerAngles.roll);
		}
		else
		{
			currentHeadPose = new Vector3(-(float)pxcmPoseEulerAngles.yaw, -(float)pxcmPoseEulerAngles.pitch, -(float)pxcmPoseEulerAngles.roll);
		}
		
		// Smooth the raw headpose data
		currentHeadPose = Vector3.Lerp(_oldHeadPose, currentHeadPose, SMOOTH_FACTOR);
		_oldHeadPose = currentHeadPose;
		
		// Account for calibration offset
		currentHeadPose -= _neutralHeadPose;
		
		_headYaw = currentHeadPose.x;
		_headPitch = currentHeadPose.y;
		_headRoll = currentHeadPose.z;

		// Update back
		_pxcmFaceDetectionData = pxcmFace.QueryDetection();
		float faceAverageDepth = 0f;
		result = _pxcmFaceDetectionData.QueryFaceAverageDepth(out faceAverageDepth);
		if (result == true)
		{
			if (_baseFaceAverageDepth == -1)
			{
				_baseFaceAverageDepth = faceAverageDepth;
				_backPitch = 0f;
			}
			else
			{
				_backPitch = Mathf.Lerp(_backPitch, (faceAverageDepth - _baseFaceAverageDepth) / MAX_BACK_PITCH * NORMALIZE_MAX_BACK_PITCH_VALUE, SMOOTH_FACTOR);
			}
		}
		else
		{
			_baseFaceAverageDepth = -1;
			_backPitch = 0f;

			Debug.Log("PXCMFaceData.Face.QueryDetection() failed.");
		}

		if (!_isPoseInitialized)
		{
			_isPoseInitialized = true;

			StartCalibration();
		}
	}

	private float GetLandmarksDistance(float x1, float x2)
	{
		return x1 - x2;
	}
	
	private float GetLandmarksDistance(float x1, float x2, float rotationAngle)
	{
		return (x1 - x2) * Mathf.Sin((90 - rotationAngle) * Mathf.PI / 180);
	}
	
	private float GetNormalizedLandmarksDistance(float distance, float optimalFaceDistance, float faceDistance, float initialDistance, float optimalMaxDistance, float maxNormalizedValue, bool clampMinMax)
	{
		float normalizedValue = (((distance * optimalFaceDistance / faceDistance) - initialDistance) / (optimalMaxDistance - initialDistance) * maxNormalizedValue);
		if (clampMinMax) 
		{
			normalizedValue = Mathf.Clamp(normalizedValue, 0, maxNormalizedValue);
		}
		
		return normalizedValue;
	}

	private float GetNormalizedJawLandmarksDistance(float distance, float optimalFaceDistance, float faceDistance, float initialDistance, float optimalMaxDistance, float maxNormalizedValue, float poseRollAngle)
	{
		float normalizedValue = GetNormalizedLandmarksDistance(distance, optimalFaceDistance, faceDistance, initialDistance, optimalMaxDistance, maxNormalizedValue, false);
		normalizedValue = Mathf.Clamp(normalizedValue, -maxNormalizedValue, maxNormalizedValue);

		if (Mathf.Abs(poseRollAngle) > 5f)
		{
			normalizedValue = 0;
		}

		return normalizedValue;
	}

	private void StartCalibration()
	{
		if (_isCalibrating)
		{
			return;
		}
		
		_isCalibrating = true;
		_currentCalibrationFrame = 0;
	}
	
	private void UpdateCalibration()
	{
		if (!_isCalibrating)
		{
			return;
		}
		
		if (++_currentCalibrationFrame >= MAX_CALIBRATION_FRAMES)
		{
			StopCalibration();
		}
	}
	
	private void StopCalibration()
	{
		_isCalibrating = false;
		_currentCalibrationFrame = 0;
		
		_neutralHeadPose = _oldHeadPose;

		float faceAverageDepth = 0f;
		if (_pxcmFaceDetectionData.QueryFaceAverageDepth(out faceAverageDepth))
		{
			_baseFaceAverageDepth = faceAverageDepth;
			_backPitch = 0f;
		}
	}
	
	private void DrawLandmarks(PXCMFaceData.LandmarkPoint[] pxcmLandmarkPoints)
	{
		// Draw all landmarks
		
		Color32[] pixels = _operatorVideoTexture.GetPixels32();
		for (int i = 0; i < pxcmLandmarkPoints.Length; i++)
		{
			// Make it 4 pixels big
			int x = (int)pxcmLandmarkPoints[i].image.x;
			int y = _operatorVideoTexture.height -(int)pxcmLandmarkPoints[i].image.y;
			
			Color32 color = new Color32(255, 255, 255, 100);;

			if (x < _operatorVideoTexture.width - 2 && y < _operatorVideoTexture.height - 2)
			{
				pixels[(y + 2) * _operatorVideoTexture.width + x] = color;
				pixels[(y + 2) * _operatorVideoTexture.width + x + 1] = color;
				pixels[(y) * _operatorVideoTexture.width + x + 2] = color;
				pixels[(y + 1) * _operatorVideoTexture.width + x + 2] = color;
				pixels[(y + 2) * _operatorVideoTexture.width + x + 2] = color;

				pixels[y * _operatorVideoTexture.width + x] = color;
				pixels[y * _operatorVideoTexture.width + x + 1] = color;
				pixels[(y + 1) * _operatorVideoTexture.width + x] = color;
				pixels[(y + 1) * _operatorVideoTexture.width + x + 1] =color;
			}
		}
		
		_operatorVideoTexture.SetPixels32(pixels);	
		_operatorVideoTexture.Apply();
	}
	
	private void UpdateColorImage()
	{
		// Retrieve the color image if ready
		PXCMCapture.Sample pxcmSample = _pxcmSenseManager.QueryFaceSample();
		PXCMImage pxcmImage = pxcmSample.color;
		if (pxcmImage != null)
		{
			PXCMImage.ImageData pxcmImageData;
			pxcmImage.AcquireAccess(PXCMImage.Access.ACCESS_READ,PXCMImage.PixelFormat.PIXEL_FORMAT_RGB32, out pxcmImageData);
			pxcmImageData.ToTexture2D(0, _operatorVideoTexture);
			pxcmImage.ReleaseAccess(pxcmImageData);
			
			_operatorVideoTexture.Apply();
		}
	}

	#endregion
}
