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
	
	//	private const float NEUTRAL_RELATIVE_FACE_WIDTH = 203f;
	//	private const float NEUTRAL_RELATIVE_FACE_HEIGHT = 55.5f;
	
	//	private const float OPTIMAL_RELATIVE_FACE_WIDTH = 133f;
	private float NEUTRAL_RELATIVE_FACE_WIDTH = 0f;
	
	//	private const float OPTIMAL_RELATIVE_FACE_HEIGHT = 143f;
	private float NEUTRAL_RELATIVE_FACE_HEIGHT = 0f;
	
	//	private const float OPTIMAL_EYEBROW_UP_MAX_DISTANCE = 28f;
	//	private const float EYEBROW_UP_INITIAL_DISTANCE = 23f;
	
	//	private const float TARGET_LBROW_UP_DISTANCE = 31f;
	//	private const float NEUTRAL_LBROW_UP_DISTANCE = 21f;
	private float NEUTRAL_LBROW_UP_DISTANCE = 0f;
	private float TARGET_LBROW_UP_DISTANCE = 0f;
	
	
	//	private const float TARGET_RBROW_UP_DISTANCE = 31f;
	//	private const float NEUTRAL_RBROW_UP_DISTANCE = 21f;
	private float NEUTRAL_RBROW_UP_DISTANCE = 0f;
	private float TARGET_RBROW_UP_DISTANCE = 0f;
	
	//	private const float TARGET_LBROW_INNER_DISTANCE = 25f;  25f
	//	private const float NEUTRAL_LBROW_INNER_DISTANCE = 34f; or 41f
	private float NEUTRAL_LBROW_INNER_DISTANCE = 0f;
	private float TARGET_LBROW_INNER_DISTANCE = 0f;
	
	//	private const float TARGET_RBROW_INNER_DISTANCE = 25f;
	//	private const float NEUTRAL_RBROW_INNER_DISTANCE = 34.3f;
	private float NEUTRAL_RBROW_INNER_DISTANCE = 0f;
	private float TARGET_RBROW_INNER_DISTANCE = 0f;
	
	//	private const float OPTIMAL_EYE_CLOSE_MAX_DISTANCE = 4.5f;
	//	private const float EYE_CLOSE_INITIAL_DISTANCE = 10f;
	
	//	private const float TARGET_LBLINK_DISTANCE = 7f;
	//	private const float NEUTRAL_LBLINK_DISTANCE = 11f;
	private float NEUTRAL_LBLINK_DISTANCE = 0f;
	private float TARGET_LBLINK_DISTANCE = 0f;
	
	//	private const float TARGET_RBLINK_DISTANCE = 7f;
	//	private const float NEUTRAL_RBLINK_DISTANCE = 11f;
	private float NEUTRAL_RBLINK_DISTANCE = 0f;
	private float TARGET_RBLINK_DISTANCE = 0f;
	
	//	private const float TARGET_L_LOWERLID_UP_DISTANCE = 4.5f;
	//	private const float NEUTRAL_L_LOWERLID_UP_DISTANCE = 6.4f;
	private float NEUTRAL_L_LOWERLID_UP_DISTANCE = 0f;
	private float TARGET_L_LOWERLID_UP_DISTANCE = 0f;
	
	//	private const float TARGET_R_LOWERLID_UP_DISTANCE = 4.5f;
	//	private const float NEUTRAL_R_LOWERLID_UP_DISTANCE = 6.4f;
	private float NEUTRAL_R_LOWERLID_UP_DISTANCE = 0f;
	private float TARGET_R_LOWERLID_UP_DISTANCE = 0f;
	
	
	//Diaz changed brows to distance driven
	//	private const float OPTIMAL_EYEBROWS_DOWN_MAX_DISTANCE = 20f;
	//	private const float EYEBROWS_DOWN_INITIAL_DISTANCE = 23f;
	
	//	private const float TARGET_LBROW_DOWN_DISTANCE = 11.3f;
	//	private const float NEUTRAL_LBROW_DOWN_DISTANCE = 19f;
	private float NEUTRAL_LBROW_DOWN_DISTANCE = 0f;
	private float TARGET_LBROW_DOWN_DISTANCE = 0f;
	
	//	private const float TARGET_RBROW_DOWN_DISTANCE = 11.3f;
	//	private const float NEUTRAL_RBROW_DOWN_DISTANCE = 17f;
	private float NEUTRAL_RBROW_DOWN_DISTANCE = 0f;
	private float TARGET_RBROW_DOWN_DISTANCE = 0f;
	
	
	
	//	private const float TARGET_MOUTH_OPEN_DISTANCE = 53.5f;
	//	private const float NEUTRAL_MOUTH_OPEN_DISTANCE = 13.6f;
	//neutral = 57  target = 89 (for jawlandmarks)
	private float NEUTRAL_MOUTH_OPEN_DISTANCE = 0f;
	private float TARGET_MOUTH_OPEN_DISTANCE = 0f;
	
	//	private const float TARGET_L_SMILE_DISTANCE = 17f;
	//	private const float NEUTRAL_L_SMILE_DISTANCE = 29f;
	private float NEUTRAL_L_SMILE_DISTANCE = 0f;
	private float TARGET_L_SMILE_DISTANCE = 0f;
	
	//	private const float TARGET_R_SMILE_DISTANCE = 15f;
	//	private const float NEUTRAL_R_SMILE_DISTANCE = 28f;
	private float NEUTRAL_R_SMILE_DISTANCE = 0f;
	private float TARGET_R_SMILE_DISTANCE = 0f;
	
	//	private const float TARGET_L_SMILE_CORRECTIVE_DISTANCE = 17f;
	//	private const float NEUTRAL_L_SMILE_CORRECTIVE_DISTANCE = 29f;
	private float NEUTRAL_L_SMILE_CORRECTIVE_DISTANCE = 0f;
	private float TARGET_L_SMILE_CORRECTIVE_DISTANCE = 0f;
	
	//	private const float TARGET_R_SMILE_CORRECTIVE_DISTANCE = 15f;
	//	private const float NEUTRAL_R_SMILE_CORRECTIVE_DISTANCE = 28f;
	private float NEUTRAL_R_SMILE_CORRECTIVE_DISTANCE = 0f;
	private float TARGET_R_SMILE_CORRECTIVE_DISTANCE = 0f;
	
	// neutral distance = 57, target = 74
	private float NEUTRAL_L_MOUTH_SQUEEZE_DISTANCE = 0f;
	private float TARGET_L_MOUTH_SQUEEZE_DISTANCE = 0f;
	
	private float NEUTRAL_R_MOUTH_SQUEEZE_DISTANCE = 0f;
	private float TARGET_R_MOUTH_SQUEEZE_DISTANCE = 0f;
	
	//	private const float TARGET_UPPERLIP_PRESS_DISTANCE = 7.7f;
	//	private const float NEUTRAL_UPPERLIP_PRESS_DISTANCE = 12.5f;
	private float NEUTRAL_UPPERLIP_PRESS_DISTANCE = 0f;
	private float TARGET_UPPERLIP_PRESS_DISTANCE = 0f;
	
	//	private const float TARGET_LOWERLIP_PRESS_DISTANCE = 7.7f;
	//	private const float NEUTRAL_LOWERLIP_PRESS_DISTANCE = 12.5f;
	private float NEUTRAL_LOWERLIP_PRESS_DISTANCE = 0f;
	private float TARGET_LOWERLIP_PRESS_DISTANCE = 0f;
	
	//	private const float TARGET_UPPERLIP_UP_DISTANCE = 11.6f;
	//	private const float NEUTRAL_UPPERLIP_UP_DISTANCE = .66f;
	private float NEUTRAL_UPPERLIP_UP_DISTANCE = 0f;
	private float TARGET_UPPERLIP_UP_DISTANCE = 0f;
	
	//	private const float TARGET_LOWERLIP_DOWN_DISTANCE = 11.6f;
	//	private const float NEUTRAL_LOWERLIP_DOWN_DISTANCE = .66f;
	private float NEUTRAL_LOWERLIP_DOWN_DISTANCE = 0f;
	private float TARGET_LOWERLIP_DOWN_DISTANCE = 0f;
	
	//	private const float TARGET_FROWN_DISTANCE = 35f;
	//	private const float NEUTRAL_FROWN_DISTANCE = 27f;
	private float NEUTRAL_FROWN_DISTANCE = 0f;
	private float TARGET_FROWN_DISTANCE = 0f;
	
	//	private const float TARGET_PUCKER_DISTANCE = 35f;
	//	private const float NEUTRAL_PUCKER_DISTANCE = 53f;
	private float NEUTRAL_PUCKER_DISTANCE = 0f;
	private float TARGET_PUCKER_DISTANCE = 0f;
	
	
	
	//Create an optimal distance and an initial distance variable for each blendshape
	//	private const float OPTIMAL_MOUTH_E_MAX_DISTANCE = 10f;
	//	private const float MOUTH_E_INITIAL_DISTANCE = 63f;
	//
	//	private const float OPTIMAL_MOUTH_U_MAX_DISTANCE = 10;
	//	private const float MOUTH_U_INITIAL_DISTANCE = 30;
	//
	//	private const float OPTIMAL_MOUTH_MBP_MAX_DISTANCE = 7f;
	//	private const float MOUTH_MBP_INITIAL_DISTANCE = 10f;
	
	
	
	private const float OPTIMAL_JAW_MOVE_LEFT_RIGHT_MAX_DISTANCE = 6f;
	private const float JAW_MOVE_LEFT_RIGHT_INITIAL_DISTANCE = 3f;
	
	//	private const float MOUTH_SMILE_MULTIPLIER = 2f;
	//	//Diaz changed Kiss to distance driven
	////	private const float MOUTH_KISS_MULTIPLIER = 1.5f;
	//	private const float OPTIMAL_MOUTH_KISS_MAX_DISTANCE = 35f;
	//	private const float MOUTH_KISS_INITIAL_DISTANCE = 45f;
	
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
	public GameObject _operatorVideoScreen = null;
	
	#endregion
	
	#region Private Members

	private CmdLine _commandLine;
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
	
	private float _lBrowUp = 0f;
	private float _rBrowUp = 0f;
	private float _lBrowDown = 0f;
	private float _rBrowDown = 0f;
	private float _lBrowIn = 0f;
	private float _rBrowIn = 0f;
	
	private float _lBlink = 0f;
	private float _rBlink = 0f;
	private float _eyesMoveLeftRight = 0f;
	private float _lLowerLidUp = 0f;
	private float _rLowerLidUp = 0f;
	
	private float _mouthOpen = 0f;
	private float _lMouthSmile = 0f;
	private float _rMouthSmile = 0f;
	private float _lMouthSmileCorrect = 0f;
	private float _rMouthSmileCorrect = 0f;
	private float _lMouthSqueeze = 0f;
	private float _rMouthSqueeze = 0f;
	private float _lowerLipPress = 0f;
	private float _upperLipPress = 0f;
	private float _lowerLipDown = 0f;
	private float _upperLipUp = 0f;
	private float _mouthFrown = 0f;
	private float _mouthPucker = 0f;
	
	private float _mouthLeft = 0f;
	private float _mouthRight = 0f;
	private float _jawMoveLeftRight = 0f;
	private float _headYaw = 0f;
	private float _headPitch = 0f;
	private float _headRoll = 0f;
	
	//Make a variable for each blendshape weight
	//	private float _mouthE = 0f;
	//	private float _mouthU = 0f;
	//	private float _mouthMBP = 0f;
	
	private float _backPitch = 0f;
	
	bool Initialize = true;
	
	#endregion
	
	#region Properties
	
	public float RBrowUp 
	{
		get 
		{
			return _rBrowUp;
		}
	}
	
	public float LBrowUp 
	{
		get 
		{
			return _lBrowUp;
		}
	}
	
	public float RBrowDown 
	{
		get 
		{
			return _rBrowDown;
		}
	}
	
	public float LBrowDown 
	{
		get 
		{
			return _lBrowDown;
		}
	}
	
	public float RBrowIn 
	{
		get 
		{
			return _rBrowIn;
		}
	}
	public float LBrowIn 
	{
		get 
		{
			return _lBrowIn;
		}
	}
	public float RBlink 
	{
		get 
		{
			return _rBlink;
		}
	}
	public float LBlink 
	{
		get 
		{
			return _lBlink;
		}
	}
	public float RLowLidUp 
	{
		get 
		{
			return _rLowerLidUp;
		}
	}
	public float LLowLidUp 
	{
		get 
		{
			return _lLowerLidUp;
		}
	}
	public float LMouthSmile 
	{
		get 
		{
			return _lMouthSmile;
		}
	}
	public float RMouthSmile 
	{
		get 
		{
			return _rMouthSmile;
		}
	}
	public float LMouthSmileCorrect 
	{
		get 
		{
			return _lMouthSmileCorrect;
		}
	}
	public float RMouthSmileCorrect 
	{
		get 
		{
			return _rMouthSmileCorrect;
		}
	}
	public float UpperLipPress 
	{
		get 
		{
			return _upperLipPress;
		}
	}
	public float LowerLipPress 
	{
		get 
		{
			return _lowerLipPress;
		}
	}
	public float UpperLipUp 
	{
		get 
		{
			return _upperLipUp;
		}
	}
	public float LowerLipDown 
	{
		get 
		{
			return _lowerLipDown;
		}
	}
	public float MouthFrown
	{
		get 
		{
			return _mouthFrown;
		}
	}
	public float MouthPucker 
	{
		get 
		{
			return _mouthPucker;
		}
	}
	public float LMouthSqueeze
	{
		get
		{
			return _lMouthSqueeze;
		}
	}
	public float RMouthSqueeze
	{
		get
		{
			return _rMouthSqueeze;
		}
	}
	
	//	public float LeftEyebrowUp
	//	{
	//		get
	//		{
	//			return _leftEyebrowUp;
	//		}
	//	}
	//
	//	public float RightEyebrowUp
	//	{
	//		get
	//		{
	//			return _rightEyebrowUp;
	//		}
	//	}
	//
	//	public float EyebrowsDown
	//	{
	//		get
	//		{
	//			return _eyebrowsDown;
	//		}
	//	}
	//	
	//	public float LeftEyeClose
	//	{
	//		get
	//		{
	//			return _leftEyeClose;
	//		}
	//	}
	//	
	//	public float RightEyeClose
	//	{
	//		get
	//		{
	//			return _rightEyeClose;
	//		}
	//	}
	
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
	
	//Make function to access variable
	//	public float MouthE
	//	{
	//		get
	//		{
	//			return _mouthE;
	//		}
	//	}
	//	public float MouthU
	//	{
	//		get
	//		{
	//			return _mouthU;
	//		}
	//	}
	//	public float MouthMBP
	//	{
	//		get
	//		{
	//			return _mouthMBP;
	//		}
	//	}
	//	public float MouthKiss
	//	{
	//		get
	//		{
	//			return _mouthKiss;
	//		}
	//	}
	//
	//	public float MouthSmile
	//	{
	//		get
	//		{
	//			return _mouthSmile;
	//		}
	//	}
	
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
				_operatorVideoScreen.renderer.enabled = _showOperatorVideo;
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
		_commandLine = new CmdLine();
		_commandLine.Parse();

		//	Debug.Log("not created " + _pxcmSenseManager.instance);
		var session = PXCMSession.CreateInstance();
		_pxcmSenseManager = PXCMSenseManager.CreateInstance();
		Debug.Log("created " + _pxcmSenseManager.instance);
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
		
		if (_commandLine.DataExists)
		{
			if (pxcmCaptureManager.SetFileName(_commandLine.FileName, _commandLine.Record) < pxcmStatus.PXCM_STATUS_NO_ERROR)
			{
				Debug.Log("PXCMCaptureManager.SetFileName() failed.");
				return;
			}
			if (_pxcmSenseManager.EnableStream(PXCMCapture.StreamType.STREAM_TYPE_IR, 0, 0) < pxcmStatus.PXCM_STATUS_NO_ERROR)
			{
				Debug.Log("Enable IR stream failed.");
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
		
		
		pxcmCaptureManager.FilterByStreamProfiles(PXCMCapture.StreamType.STREAM_TYPE_COLOR, 640, 480, 0);
		
		status = _pxcmSenseManager.Init();
		if (status < pxcmStatus.PXCM_STATUS_NO_ERROR) 
		{
			pxcmCaptureManager.FilterByStreamProfiles(null);
			status = _pxcmSenseManager.Init();
			if (status < pxcmStatus.PXCM_STATUS_NO_ERROR) 
			{
				Debug.Log("PXCMCaptureManager.FilterByStreamProfiles() failed with status: " + status + ".");
				OnDisable();
				return;
			}
		}
		var size = _pxcmSenseManager.QueryCaptureManager().QueryImageSize(PXCMCapture.StreamType.STREAM_TYPE_COLOR);
		_operatorVideoTexture = new Texture2D(size.width, size.height, TextureFormat.ARGB32, false);		
		_operatorVideoScreen.renderer.material.mainTexture = _operatorVideoTexture;
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
		
		
		if (Initialize) 
		{
			NEUTRAL_RELATIVE_FACE_WIDTH = GetLandmarksDistance (pxcmLandmarkPoints [69].image.x, pxcmLandmarkPoints [53].image.x);
			NEUTRAL_RELATIVE_FACE_HEIGHT = GetLandmarksDistance (pxcmLandmarkPoints [7].image.y, pxcmLandmarkPoints [61].image.y);
			
			NEUTRAL_LBROW_UP_DISTANCE = GetLandmarksDistance (pxcmLandmarkPoints [7].image.y, pxcmLandmarkPoints [77].image.y);
			NEUTRAL_RBROW_UP_DISTANCE = GetLandmarksDistance (pxcmLandmarkPoints [2].image.y, pxcmLandmarkPoints [76].image.y);
			NEUTRAL_LBROW_DOWN_DISTANCE = GetLandmarksDistance (pxcmLandmarkPoints [7].image.y, pxcmLandmarkPoints [77].image.y);
			NEUTRAL_RBROW_DOWN_DISTANCE = GetLandmarksDistance (pxcmLandmarkPoints [2].image.y, pxcmLandmarkPoints [76].image.y);
			NEUTRAL_LBROW_INNER_DISTANCE = GetLandmarksDistance (pxcmLandmarkPoints [0].image.x, pxcmLandmarkPoints [5].image.x);
			NEUTRAL_RBROW_INNER_DISTANCE = GetLandmarksDistance (pxcmLandmarkPoints [0].image.x, pxcmLandmarkPoints [5].image.x);
			NEUTRAL_LBLINK_DISTANCE = GetLandmarksDistance (pxcmLandmarkPoints [20].image.y, pxcmLandmarkPoints [24].image.y);
			NEUTRAL_RBLINK_DISTANCE = GetLandmarksDistance (pxcmLandmarkPoints [12].image.y, pxcmLandmarkPoints [16].image.y);
			NEUTRAL_L_LOWERLID_UP_DISTANCE = GetLandmarksDistance (pxcmLandmarkPoints [24].image.y, pxcmLandmarkPoints [77].image.y);
			NEUTRAL_R_LOWERLID_UP_DISTANCE = GetLandmarksDistance (pxcmLandmarkPoints [16].image.y, pxcmLandmarkPoints [76].image.y);
			NEUTRAL_L_SMILE_DISTANCE = GetLandmarksDistance (pxcmLandmarkPoints [39].image.y, pxcmLandmarkPoints [32].image.y);
			NEUTRAL_R_SMILE_DISTANCE = GetLandmarksDistance (pxcmLandmarkPoints [33].image.y, pxcmLandmarkPoints [30].image.y);
			NEUTRAL_L_SMILE_CORRECTIVE_DISTANCE = GetLandmarksDistance (pxcmLandmarkPoints [39].image.y, pxcmLandmarkPoints [32].image.y);
			NEUTRAL_R_SMILE_CORRECTIVE_DISTANCE = GetLandmarksDistance (pxcmLandmarkPoints [33].image.y, pxcmLandmarkPoints [30].image.y);
			NEUTRAL_L_MOUTH_SQUEEZE_DISTANCE = GetLandmarksDistance (pxcmLandmarkPoints [33].image.x, pxcmLandmarkPoints [39].image.x);
			NEUTRAL_R_MOUTH_SQUEEZE_DISTANCE = GetLandmarksDistance (pxcmLandmarkPoints [33].image.x, pxcmLandmarkPoints [39].image.x);
			NEUTRAL_UPPERLIP_PRESS_DISTANCE = GetLandmarksDistance (pxcmLandmarkPoints [36].image.y, pxcmLandmarkPoints [42].image.y);
			NEUTRAL_LOWERLIP_PRESS_DISTANCE = GetLandmarksDistance (pxcmLandmarkPoints [42].image.y, pxcmLandmarkPoints [36].image.y);
			NEUTRAL_UPPERLIP_UP_DISTANCE = GetLandmarksDistance (pxcmLandmarkPoints [36].image.y, pxcmLandmarkPoints [31].image.y);
			NEUTRAL_LOWERLIP_DOWN_DISTANCE = GetLandmarksDistance (pxcmLandmarkPoints [51].image.y, pxcmLandmarkPoints [47].image.y);
			NEUTRAL_FROWN_DISTANCE = GetLandmarksDistance (pxcmLandmarkPoints [39].image.y, pxcmLandmarkPoints [32].image.y);
			NEUTRAL_PUCKER_DISTANCE = GetLandmarksDistance (pxcmLandmarkPoints [33].image.x, pxcmLandmarkPoints [39].image.x);
			NEUTRAL_MOUTH_OPEN_DISTANCE = GetLandmarksDistance (pxcmLandmarkPoints [36].image.y, pxcmLandmarkPoints [42].image.y);
			Debug.Log ("neutral mouth open " +NEUTRAL_MOUTH_OPEN_DISTANCE);
			
			TARGET_LBROW_UP_DISTANCE = NEUTRAL_LBROW_UP_DISTANCE * 1.48f;
			TARGET_RBROW_UP_DISTANCE = NEUTRAL_RBROW_UP_DISTANCE * 1.48f;
			TARGET_LBROW_DOWN_DISTANCE = NEUTRAL_LBROW_DOWN_DISTANCE * .6f;
			TARGET_RBROW_DOWN_DISTANCE = NEUTRAL_RBROW_DOWN_DISTANCE * .6f;
			TARGET_LBROW_INNER_DISTANCE = NEUTRAL_LBROW_INNER_DISTANCE * .73f;
			TARGET_RBROW_INNER_DISTANCE = NEUTRAL_RBROW_INNER_DISTANCE * .73f;
			TARGET_LBLINK_DISTANCE = NEUTRAL_LBLINK_DISTANCE * .65f;
			TARGET_RBLINK_DISTANCE = NEUTRAL_RBLINK_DISTANCE * .65f;
			TARGET_L_LOWERLID_UP_DISTANCE = NEUTRAL_L_LOWERLID_UP_DISTANCE * .7f;
			TARGET_R_LOWERLID_UP_DISTANCE = NEUTRAL_R_LOWERLID_UP_DISTANCE * .7f;
			TARGET_L_SMILE_DISTANCE = NEUTRAL_L_SMILE_DISTANCE * .57f;
			TARGET_R_SMILE_DISTANCE = NEUTRAL_R_SMILE_DISTANCE * .57f;
			TARGET_L_SMILE_CORRECTIVE_DISTANCE = NEUTRAL_L_SMILE_CORRECTIVE_DISTANCE * .57f;
			TARGET_R_SMILE_CORRECTIVE_DISTANCE = NEUTRAL_R_SMILE_CORRECTIVE_DISTANCE * .57f;
			TARGET_L_MOUTH_SQUEEZE_DISTANCE = NEUTRAL_L_MOUTH_SQUEEZE_DISTANCE * 1.3f;
			TARGET_R_MOUTH_SQUEEZE_DISTANCE = NEUTRAL_R_MOUTH_SQUEEZE_DISTANCE * 1.3f;
			TARGET_UPPERLIP_PRESS_DISTANCE = NEUTRAL_UPPERLIP_PRESS_DISTANCE * .62f;
			TARGET_LOWERLIP_PRESS_DISTANCE = NEUTRAL_LOWERLIP_PRESS_DISTANCE * .62f;
			TARGET_UPPERLIP_UP_DISTANCE = NEUTRAL_UPPERLIP_UP_DISTANCE * .17f;
			TARGET_LOWERLIP_DOWN_DISTANCE = NEUTRAL_LOWERLIP_DOWN_DISTANCE * 17.58f;
			TARGET_FROWN_DISTANCE = NEUTRAL_FROWN_DISTANCE * 1.1f;
			TARGET_PUCKER_DISTANCE = NEUTRAL_PUCKER_DISTANCE * .66f;
			TARGET_MOUTH_OPEN_DISTANCE = NEUTRAL_MOUTH_OPEN_DISTANCE * 3f;
			Debug.Log ("target mouth open " + TARGET_MOUTH_OPEN_DISTANCE);
			
			
			
			
			
			
			
			Initialize = false;
			//Debug.Log (NEUTRAL_RELATIVE_FACE_WIDTH);
			//Debug.Log (NEUTRAL_RELATIVE_FACE_HEIGHT);
		}
		
		
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
		//ALYSSA ON MONDAY START HERE GIRLFRIEND AW YEAH
		// Update face
		// DO ALL WORK HERE
		float faceWidth = pxcmLandmarkPoints[53].image.x - pxcmLandmarkPoints[69].image.x;
		//		float faceHeight = pxcmLandmarkPoints[29].image.y - pxcmLandmarkPoints[26].image.y;
		float faceHeight = pxcmLandmarkPoints[61].image.y - pxcmLandmarkPoints[7].image.y;
		
		float eyesMoveLeftRight = (pxcmLandmarkPoints[10].image.x - pxcmLandmarkPoints[76].image.x) / (pxcmLandmarkPoints[10].image.x - pxcmLandmarkPoints[14].image.x) * NORMALIZE_MAX_EYES_MOVE_LEFT_RIGHT_VALUE - NORMALIZE_MAX_EYES_MOVE_LEFT_RIGHT_VALUE / 2;
		_eyesMoveLeftRight = Mathf.Clamp(eyesMoveLeftRight, -NORMALIZE_MAX_EYES_MOVE_LEFT_RIGHT_VALUE, NORMALIZE_MAX_EYES_MOVE_LEFT_RIGHT_VALUE);
		
		float normalizedJawLandmarksDistance = GetNormalizedJawLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[61].image.x, pxcmLandmarkPoints[31].image.x, pxcmPoseEulerAngles.yaw), NEUTRAL_RELATIVE_FACE_WIDTH, faceWidth, JAW_MOVE_LEFT_RIGHT_INITIAL_DISTANCE, OPTIMAL_JAW_MOVE_LEFT_RIGHT_MAX_DISTANCE, NORMALIZE_MAX_JAW_MOVE_LEFT_RIGHT_VALUE, pxcmPoseEulerAngles.roll);
		float jawMoveLeftRight = Mathf.Lerp(_jawMoveLeftRight, normalizedJawLandmarksDistance, SMOOTH_FACTOR);
		
		//		if (Input.GetKeyDown (KeyCode.Space))
		//			Debug.Log ("jawmove = " + GetLandmarksDistance (pxcmLandmarkPoints [61].image.x, pxcmLandmarkPoints [31].image.x, pxcmPoseEulerAngles.pitch));
		
		
		if (_cameraMirrorMode == PXCMCapture.Device.MirrorMode.MIRROR_MODE_HORIZONTAL)
		{
			_lBrowUp = RBrowUp;
			_rBrowUp = LBrowUp;
			
			_lBlink = RBlink;
			_rBlink = LBlink;
			
			_jawMoveLeftRight = -jawMoveLeftRight;
		}
		else
		{
			_lBrowUp = LBrowUp;
			_rBrowUp = RBrowUp;
			
			_lBlink = LBlink;
			_rBlink = RBlink;
			
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
		
		//		if (Input.GetKeyDown (KeyCode.Space))
		//			Debug.Log ("faceWidth = " + GetLandmarksDistance (pxcmLandmarkPoints [69].image.x, pxcmLandmarkPoints [53].image.x, pxcmPoseEulerAngles.pitch));
		//
		//		if (Input.GetKeyDown (KeyCode.Space))
		//			Debug.Log ("faceHeight = " + GetLandmarksDistance (pxcmLandmarkPoints [61].image.y, pxcmLandmarkPoints [7].image.y, pxcmPoseEulerAngles.pitch));
		
		_mouthOpen = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[36].image.y, pxcmLandmarkPoints[42].image.y, pxcmPoseEulerAngles.pitch), 
		                                            NEUTRAL_RELATIVE_FACE_HEIGHT, faceHeight, NEUTRAL_MOUTH_OPEN_DISTANCE, TARGET_MOUTH_OPEN_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true);
		
		if (Input.GetKeyDown (KeyCode.Space))		
			Debug.Log ("mouthopen = " + GetLandmarksDistance (pxcmLandmarkPoints [36].image.y, pxcmLandmarkPoints [42].image.y, pxcmPoseEulerAngles.pitch));
		
		_lBrowUp = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[7].image.y, pxcmLandmarkPoints[77].image.y, pxcmPoseEulerAngles.pitch), 
		                                          NEUTRAL_RELATIVE_FACE_HEIGHT, faceHeight, NEUTRAL_LBROW_UP_DISTANCE, TARGET_LBROW_UP_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true);
		//		if (Input.GetKeyDown (KeyCode.Space))
		//			Debug.Log ("_lBrowUp = " + GetLandmarksDistance (pxcmLandmarkPoints [7].image.y, pxcmLandmarkPoints [77].image.y, pxcmPoseEulerAngles.pitch));
		
		_rBrowUp = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[2].image.y, pxcmLandmarkPoints[76].image.y, pxcmPoseEulerAngles.pitch), 
		                                          NEUTRAL_RELATIVE_FACE_HEIGHT, faceHeight, NEUTRAL_RBROW_UP_DISTANCE, TARGET_RBROW_UP_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true);
		_lBrowDown = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[7].image.y, pxcmLandmarkPoints[77].image.y, pxcmPoseEulerAngles.pitch), 
		                                            NEUTRAL_RELATIVE_FACE_HEIGHT, faceHeight, NEUTRAL_LBROW_DOWN_DISTANCE, TARGET_LBROW_DOWN_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true);
		//		if (Input.GetKeyDown (KeyCode.Space))
		//			Debug.Log ("lbrowDown = " + GetLandmarksDistance (pxcmLandmarkPoints [7].image.y, pxcmLandmarkPoints [77].image.y, pxcmPoseEulerAngles.pitch));
		
		_rBrowDown = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[2].image.y, pxcmLandmarkPoints[76].image.y, pxcmPoseEulerAngles.pitch), 
		                                            NEUTRAL_RELATIVE_FACE_HEIGHT, faceHeight, NEUTRAL_RBROW_DOWN_DISTANCE, TARGET_RBROW_DOWN_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true);
		//		if (Input.GetKeyDown (KeyCode.Space))
		//			Debug.Log ("rbrownDown = " + GetLandmarksDistance (pxcmLandmarkPoints [2].image.y, pxcmLandmarkPoints [76].image.y, pxcmPoseEulerAngles.pitch));
		
		_lBrowIn = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[0].image.x, pxcmLandmarkPoints[5].image.x, pxcmPoseEulerAngles.pitch), 
		                                          NEUTRAL_RELATIVE_FACE_HEIGHT, faceHeight, NEUTRAL_LBROW_INNER_DISTANCE, TARGET_LBROW_INNER_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true);
		//		if (Input.GetKeyDown (KeyCode.Space))
		//			Debug.Log ("lbrowIn = " + GetLandmarksDistance (pxcmLandmarkPoints [0].image.x, pxcmLandmarkPoints [5].image.x, pxcmPoseEulerAngles.pitch));
		
		_rBrowIn = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[5].image.x, pxcmLandmarkPoints[0].image.x, pxcmPoseEulerAngles.pitch), 
		                                          NEUTRAL_RELATIVE_FACE_HEIGHT, faceHeight, NEUTRAL_RBROW_INNER_DISTANCE, TARGET_RBROW_INNER_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true);
		//		if (Input.GetKeyDown (KeyCode.Space))
		//			Debug.Log ("rbrowIn = " + GetLandmarksDistance (pxcmLandmarkPoints [5].image.x, pxcmLandmarkPoints [26].image.x, pxcmPoseEulerAngles.pitch));
		
		_lBlink = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[20].image.y, pxcmLandmarkPoints[24].image.y, pxcmPoseEulerAngles.pitch), 
		                                         NEUTRAL_RELATIVE_FACE_HEIGHT, faceHeight, NEUTRAL_LBLINK_DISTANCE, TARGET_LBLINK_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true);
		//		if (Input.GetKeyDown (KeyCode.Space))
		//			Debug.Log ("_lblink = " + GetLandmarksDistance (pxcmLandmarkPoints [20].image.y, pxcmLandmarkPoints [24].image.y, pxcmPoseEulerAngles.pitch));
		
		_rBlink = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[12].image.y, pxcmLandmarkPoints[16].image.y, pxcmPoseEulerAngles.pitch), 
		                                         NEUTRAL_RELATIVE_FACE_HEIGHT, faceHeight, NEUTRAL_RBLINK_DISTANCE, TARGET_RBLINK_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true);
		//		if (Input.GetKeyDown (KeyCode.Space))
		//			Debug.Log ("_rblink = " + GetLandmarksDistance (pxcmLandmarkPoints [12].image.y, pxcmLandmarkPoints [16].image.y, pxcmPoseEulerAngles.pitch));
		
		_lLowerLidUp = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[24].image.y, pxcmLandmarkPoints[77].image.y, pxcmPoseEulerAngles.pitch), 
		                                              NEUTRAL_RELATIVE_FACE_HEIGHT, faceHeight, NEUTRAL_L_LOWERLID_UP_DISTANCE, TARGET_L_LOWERLID_UP_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true);
		//		if (Input.GetKeyDown (KeyCode.Space))
		//			Debug.Log ("lLowLidUp = " + GetLandmarksDistance (pxcmLandmarkPoints [24].image.y, pxcmLandmarkPoints [77].image.y, pxcmPoseEulerAngles.pitch));
		
		_rLowerLidUp = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[16].image.y, pxcmLandmarkPoints[76].image.y, pxcmPoseEulerAngles.pitch), 
		                                              NEUTRAL_RELATIVE_FACE_HEIGHT, faceHeight, NEUTRAL_R_LOWERLID_UP_DISTANCE, TARGET_R_LOWERLID_UP_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true);
		//		if (Input.GetKeyDown (KeyCode.Space))
		//			Debug.Log ("rLowLidUp = " + GetLandmarksDistance (pxcmLandmarkPoints [16].image.y, pxcmLandmarkPoints [76].image.y, pxcmPoseEulerAngles.pitch));
		
		_rMouthSmile = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[33].image.y, pxcmLandmarkPoints[30].image.y, pxcmPoseEulerAngles.pitch), 
		                                              NEUTRAL_RELATIVE_FACE_HEIGHT, faceHeight, NEUTRAL_R_SMILE_DISTANCE, TARGET_R_SMILE_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true);
		//		if (Input.GetKeyDown (KeyCode.Space))
		//			Debug.Log ("rSmile = " + GetLandmarksDistance (pxcmLandmarkPoints [33].image.y, pxcmLandmarkPoints [30].image.y, pxcmPoseEulerAngles.pitch));
		
		_lMouthSmile = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[39].image.y, pxcmLandmarkPoints[32].image.y, pxcmPoseEulerAngles.pitch), 
		                                              NEUTRAL_RELATIVE_FACE_HEIGHT, faceHeight, NEUTRAL_L_SMILE_DISTANCE, TARGET_L_SMILE_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true);
		//		if (Input.GetKeyDown (KeyCode.Space))
		//			Debug.Log ("lSmile = " + GetLandmarksDistance (pxcmLandmarkPoints [39].image.y, pxcmLandmarkPoints [32].image.y, pxcmPoseEulerAngles.pitch));
		
		_rMouthSmileCorrect = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[33].image.y, pxcmLandmarkPoints[30].image.y, pxcmPoseEulerAngles.pitch), 
		                                                     NEUTRAL_RELATIVE_FACE_HEIGHT, faceHeight, NEUTRAL_R_SMILE_CORRECTIVE_DISTANCE, TARGET_R_SMILE_CORRECTIVE_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true);
		
		_lMouthSmileCorrect = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[39].image.y, pxcmLandmarkPoints[32].image.y, pxcmPoseEulerAngles.pitch), 
		                                                     NEUTRAL_RELATIVE_FACE_HEIGHT, faceHeight, NEUTRAL_L_SMILE_CORRECTIVE_DISTANCE, TARGET_L_SMILE_CORRECTIVE_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true);
		
		_lMouthSqueeze = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[33].image.x, pxcmLandmarkPoints[39].image.x, pxcmPoseEulerAngles.pitch), 
		                                                NEUTRAL_RELATIVE_FACE_HEIGHT, faceHeight, NEUTRAL_L_MOUTH_SQUEEZE_DISTANCE, TARGET_L_MOUTH_SQUEEZE_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true);
		//		if (Input.GetKeyDown (KeyCode.Space))
		//						Debug.Log ("lSqueeze = " + GetLandmarksDistance (pxcmLandmarkPoints [33].image.x, pxcmLandmarkPoints [39].image.x, pxcmPoseEulerAngles.pitch));
		
		_rMouthSqueeze = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[33].image.x, pxcmLandmarkPoints[39].image.x, pxcmPoseEulerAngles.pitch), 
		                                                NEUTRAL_RELATIVE_FACE_HEIGHT, faceHeight, NEUTRAL_R_MOUTH_SQUEEZE_DISTANCE, TARGET_R_MOUTH_SQUEEZE_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true);
		
		
		_upperLipPress = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[36].image.y, pxcmLandmarkPoints[42].image.y, pxcmPoseEulerAngles.pitch), 
		                                                NEUTRAL_RELATIVE_FACE_HEIGHT, faceHeight, NEUTRAL_UPPERLIP_PRESS_DISTANCE, TARGET_UPPERLIP_PRESS_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true);
		//		if (Input.GetKeyDown (KeyCode.Space))
		//			Debug.Log ("upLipPress = " + GetLandmarksDistance (pxcmLandmarkPoints [36].image.y, pxcmLandmarkPoints [42].image.y, pxcmPoseEulerAngles.pitch));
		
		_lowerLipPress = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[42].image.y, pxcmLandmarkPoints[36].image.y, pxcmPoseEulerAngles.pitch), 
		                                                NEUTRAL_RELATIVE_FACE_HEIGHT, faceHeight, NEUTRAL_LOWERLIP_PRESS_DISTANCE, TARGET_LOWERLIP_PRESS_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true);
		//		if (Input.GetKeyDown (KeyCode.Space))
		//			Debug.Log ("lowLipPress = " + GetLandmarksDistance (pxcmLandmarkPoints [42].image.y, pxcmLandmarkPoints [36].image.y, pxcmPoseEulerAngles.pitch));
		
		_lowerLipDown = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[51].image.y, pxcmLandmarkPoints[47].image.y, pxcmPoseEulerAngles.pitch), 
		                                               NEUTRAL_RELATIVE_FACE_HEIGHT, faceHeight, NEUTRAL_LOWERLIP_DOWN_DISTANCE, TARGET_LOWERLIP_DOWN_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true);
		//		if (Input.GetKeyDown (KeyCode.Space))
		//			Debug.Log ("lowlipdown = " + GetLandmarksDistance (pxcmLandmarkPoints [51].image.y, pxcmLandmarkPoints [47].image.y, pxcmPoseEulerAngles.pitch));
		
		_upperLipUp = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[36].image.y, pxcmLandmarkPoints[31].image.y, pxcmPoseEulerAngles.pitch), 
		                                             NEUTRAL_RELATIVE_FACE_HEIGHT, faceHeight, NEUTRAL_UPPERLIP_UP_DISTANCE, TARGET_UPPERLIP_UP_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true);
		//		if (Input.GetKeyDown (KeyCode.Space))
		//			Debug.Log ("uplipup = " + GetLandmarksDistance (pxcmLandmarkPoints [36].image.y, pxcmLandmarkPoints [31].image.y, pxcmPoseEulerAngles.pitch));
		
		_mouthFrown = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[39].image.y, pxcmLandmarkPoints[32].image.y, pxcmPoseEulerAngles.pitch), 
		                                             NEUTRAL_RELATIVE_FACE_HEIGHT, faceHeight, NEUTRAL_FROWN_DISTANCE, TARGET_FROWN_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true);
		//		if (Input.GetKeyDown (KeyCode.Space))
		//			Debug.Log ("mouthFrown = " + GetLandmarksDistance (pxcmLandmarkPoints [39].image.y, pxcmLandmarkPoints [32].image.y, pxcmPoseEulerAngles.pitch));
		
		_mouthPucker = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[39].image.x, pxcmLandmarkPoints[33].image.x, pxcmPoseEulerAngles.pitch), 
		                                              NEUTRAL_RELATIVE_FACE_HEIGHT, faceHeight, NEUTRAL_PUCKER_DISTANCE, TARGET_PUCKER_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true);
		//		if (Input.GetKeyDown (KeyCode.Space))
		//			Debug.Log ("mouthPucker = " + GetLandmarksDistance (pxcmLandmarkPoints [39].image.x, pxcmLandmarkPoints [33].image.x, pxcmPoseEulerAngles.pitch));
		
		
		_rBrowDown = Mathf.Clamp(_rBrowDown, 0, (100 - _rBrowUp));
		_lBrowDown = Mathf.Clamp (_lBrowDown, 0, (100 - _lBrowUp));
		_rLowerLidUp = Mathf.Clamp (_rLowerLidUp, 0, (100 - _rBlink));
		_lLowerLidUp = Mathf.Clamp (_lLowerLidUp, 0, (100 - _lBlink));
		_rLowerLidUp = Mathf.Clamp (_rLowerLidUp, 0, (100 - _rMouthSmile));
		_lLowerLidUp = Mathf.Clamp (_lLowerLidUp, 0, (100 - _lMouthSmile));
		_lMouthSmileCorrect = Mathf.Clamp (_lMouthSmileCorrect, 0, (100 - _rMouthSmile));
		_rMouthSmileCorrect = Mathf.Clamp (_rMouthSmileCorrect, 0, (100 - _lMouthSmile));
		
		//		if (_rMouthSmile > 50)
		//			_mouthFrown = 0f;
		//		else
		//		if (_mouthOpen > 25)
		//						_mouthFrown = 0f;
		//		else
		_mouthFrown = Mathf.Clamp (_mouthFrown, 0, (100 - _lowerLipPress));
		
		
		if (_mouthOpen > 10)
			_lowerLipDown = 0f;
		else
			_lowerLipDown = Mathf.Clamp (_lowerLipDown, 0, (100 - _lowerLipPress));
		
		_upperLipUp = Mathf.Clamp (_upperLipUp, 0, (100 - _mouthOpen));
		_lowerLipPress = Mathf.Clamp (_lowerLipPress, 0, (100 - _mouthOpen));
		_upperLipPress = Mathf.Clamp (_upperLipPress, 0, (100 - _mouthOpen));
		//		_mouthPucker = Mathf.Clamp (_mouthPucker, 0, (100 - _lMouthSmile));
		//		_mouthPucker = Mathf.Clamp (_mouthPucker, 0, (100 - _rMouthSmile));
		//		_lowerLipDown = Mathf.Clamp (_lowerLipDown, 0, (100 - MouthPucker));
		//		_upperLipUp = Mathf.Clamp (_upperLipUp, 0, (100 - MouthPucker));
		//		_lowerLipDown = Mathf.Clamp (_lowerLipDown, 0, (100 - LMouthSmile));
		//		_lowerLipDown = Mathf.Clamp (_lowerLipDown, 0, (100 - RMouthSmile));
		//		_upperLipUp = Mathf.Clamp (_upperLipUp, 0, (100 - LMouthSmile));
		//		_upperLipUp = Mathf.Clamp (_upperLipUp, 0, (100 - RMouthSmile));
		_mouthPucker = Mathf.Clamp (_mouthPucker, 0, (100 - _mouthOpen));
		_lMouthSqueeze = Mathf.Clamp (_lMouthSqueeze, 0, (100 - _lMouthSmile));
		_rMouthSqueeze = Mathf.Clamp (_rMouthSqueeze, 0, (100 - _rMouthSmile));
		
		
		
		//		PXCMFaceData.ExpressionsData.FaceExpressionResult pxcmFaceExpressionResult = new PXCMFaceData.ExpressionsData.FaceExpressionResult();	
		//		result = pxcmExpressionsData.QueryExpression(PXCMFaceData.ExpressionsData.FaceExpression.EXPRESSION_SMILE, out pxcmFaceExpressionResult);
		//		if (result == true)
		//		{
		//			_mouthSmile = (float)pxcmFaceExpressionResult.intensity;
		//		}
		//		else
		//		{
		//			Debug.Log("Error querying expression: EXPRESSION_SMILE.");
		//		}
		//
		//		_mouthE = Mathf.Clamp(_mouthE, 0, (100 - _mouthSmile));
		////		result = pxcmExpressionsData.QueryExpression(PXCMFaceData.ExpressionsData.FaceExpression.EXPRESSION_KISS, out pxcmFaceExpressionResult);	
		////		if (result == true)
		////		{
		////			_mouthKiss = (float)pxcmFaceExpressionResult.intensity;
		////		}
		////		else
		////		{
		////			Debug.Log("Error querying expression: EXPRESSION_KISS.");
		////		}
		//		_mouthKiss = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[45].image.x, pxcmLandmarkPoints[49].image.x, pxcmPoseEulerAngles.pitch), 
		//		                                           NEUTRAL_RELATIVE_FACE_HEIGHT, faceHeight, MOUTH_KISS_INITIAL_DISTANCE, OPTIMAL_MOUTH_KISS_MAX_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true); 
		//
		////		float lBrowDown = 0f;
		////		float rBrowDown = 0f;
		////		result = pxcmExpressionsData.QueryExpression(PXCMFaceData.ExpressionsData.FaceExpression.EXPRESSION_BROW_LOWERER_LEFT, out pxcmFaceExpressionResult);
		////		if (result == true)
		////		{
		////			lBrowDown = (float)pxcmFaceExpressionResult.intensity;
		////		}
		////		else
		////		{
		////			Debug.Log("Error querying expression: EXPRESSION_BROW_LOWERER_LEFT.");
		////		}
		////		result = pxcmExpressionsData.QueryExpression(PXCMFaceData.ExpressionsData.FaceExpression.EXPRESSION_BROW_LOWERER_RIGHT, out pxcmFaceExpressionResult);
		////		if (result == true)
		////		{
		////			rBrowDown = (float)pxcmFaceExpressionResult.intensity;
		////		}
		////		else
		////		{
		////			Debug.Log("Error querying expression: EXPRESSION_BROW_LOWERER_RIGHT.");
		////		}
		////
		////		_eyebrowsDown = Mathf.Clamp((rBrowDown + lBrowDown), 0, 100) ;
		//		_eyebrowsDown = GetNormalizedLandmarksDistance(GetLandmarksDistance(pxcmLandmarkPoints[77].image.y, pxcmLandmarkPoints[7].image.y, pxcmPoseEulerAngles.pitch), 
		//		                                            NEUTRAL_RELATIVE_FACE_HEIGHT, faceHeight, EYEBROWS_DOWN_INITIAL_DISTANCE, OPTIMAL_EYEBROWS_DOWN_MAX_DISTANCE, NORMALIZE_MAX_FACIAL_EXPRESSION_VALUE, true); 
		//		_leftEyebrowUp = Mathf.Clamp(_leftEyebrowUp, 0, (100 - _eyebrowsDown));
		//		_rightEyebrowUp = Mathf.Clamp (_rightEyebrowUp, 0, (100 - _eyebrowsDown));
		
		
		
		// Update head
		pxcmPoseEulerAngles.pitch *= -1;
		pxcmPoseEulerAngles.roll *= -1;
		
		if (_cameraMirrorMode == PXCMCapture.Device.MirrorMode.MIRROR_MODE_HORIZONTAL)
		{
			currentHeadPose = new Vector3((float)pxcmPoseEulerAngles.yaw, -(float)pxcmPoseEulerAngles.pitch, (float)pxcmPoseEulerAngles.roll);
		}
		else
		{
			currentHeadPose = new Vector3((float)pxcmPoseEulerAngles.yaw, -(float)pxcmPoseEulerAngles.pitch, (float)pxcmPoseEulerAngles.roll);
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
		return Mathf.Abs(x1 - x2);
	}
	
	private float GetLandmarksDistance(float x1, float x2, float rotationAngle)
	{
		float distance = Mathf.Abs(x1 - x2) * Mathf.Sin((90 - rotationAngle) * Mathf.PI / 180);
		
		return distance;
	}
	
	private float GetNormalizedLandmarksDistance(float distance, // distance between two landmarks 
	                                             float optimalFaceDistance, 
	                                             float faceDistance, 
	                                             float initialDistance, 
	                                             float optimalMaxDistance, 
	                                             float maxNormalizedValue, 
	                                             bool clampMinMax)
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
			int x = Mathf.Min(Mathf.Max((int)pxcmLandmarkPoints[i].image.x, 0), _operatorVideoTexture.width);
			int y = Mathf.Min(Mathf.Max(_operatorVideoTexture.height -(int)pxcmLandmarkPoints[i].image.y, 0), _operatorVideoTexture.height);
			
			Color32 color = new Color32(0, 0, 0, 100);;
			
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
