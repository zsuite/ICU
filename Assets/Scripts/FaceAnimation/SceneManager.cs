/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2014 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using UnityEngine;
using System.Runtime.InteropServices;

public class SceneManager : MonoBehaviour
{
	#region Constants
	
		private const float LEGEND_TOP_MARGIN = 10f;
		private const float LEGEND_RIGHT_MARGIN = 10f;
		private const float LEGEND_WIDTH = 150f;
		private const float LEGEND_ITEM_HEIGHT = 22;
		private const float OFFSCREEN_POSITION_Y = -1000f;

	#endregion

	#region Public Members

		public GameObject[] _characterObjects;

	#endregion
	
	#region Private Members

		private FaceTrackingManager _faceTrackingManager = null;
		private int _previousCharacterIndex = -1;
		private int _currentCharacterIndex = 0;

	#endregion

    #region Handlers

		void Awake ()
		{
				_faceTrackingManager = gameObject.GetComponent<FaceTrackingManager> ();

				SetCurrentCharacter ();
		}
	
		void Start ()
		{
		}
	
		void Update ()
		{
		}

		void OnGUI ()
		{
				DrawLegend ();

				if (Event.current.type == EventType.KeyDown) {
						if (Event.current.keyCode == KeyCode.S) {
								_previousCharacterIndex = _currentCharacterIndex;
								_currentCharacterIndex = (_currentCharacterIndex + 1) % _characterObjects.Length;

								SetCurrentCharacter ();
						}
				}
		}

	#endregion
	
	#region Private Methods

		private void SetCurrentCharacter ()
		{
				if (_faceTrackingManager) {
						if ((_currentCharacterIndex >= 0) && (_currentCharacterIndex < _characterObjects.Length)) {
								if (_characterObjects [_currentCharacterIndex]) {
										Transform currentEnvironmentTransform = _characterObjects [_currentCharacterIndex].transform.Find ("Environment");
										if (currentEnvironmentTransform) {
												// Activate current environment object
												currentEnvironmentTransform.gameObject.SetActive (true);
										}

										GameObject currentCharacter = _characterObjects [_currentCharacterIndex];
										Vector3 position = new Vector3 (currentCharacter.transform.position.x, 0, currentCharacter.transform.position.z);
										currentCharacter.transform.position = position;
								}
						}

						if ((_previousCharacterIndex >= 0) && (_previousCharacterIndex < _characterObjects.Length)) {
								if (_characterObjects [_previousCharacterIndex]) {
										Transform previousEnvironmentTransform = _characterObjects [_previousCharacterIndex].transform.Find ("Environment");
										if (previousEnvironmentTransform) {
												// Deactivate previous environment object
												previousEnvironmentTransform.gameObject.SetActive (false);
										}

										GameObject previousCharacter = _characterObjects [_previousCharacterIndex];
										Vector3 position = new Vector3 (previousCharacter.transform.position.x, OFFSCREEN_POSITION_Y, previousCharacter.transform.position.z);
										previousCharacter.transform.position = position;
								}
						}
				}
		}

		private void DrawLegend ()
		{
				Color oldColor = GUI.color;
				GUI.color = new Color ((51f / 255f), (89f / 255f), (118f / 255f));

				float y = LEGEND_TOP_MARGIN;
				GUI.Label (new Rect (Screen.width - LEGEND_WIDTH - LEGEND_RIGHT_MARGIN, y, LEGEND_WIDTH, LEGEND_ITEM_HEIGHT), "I - Toggle Operator Feed");
				y += LEGEND_ITEM_HEIGHT;
				GUI.Label (new Rect (Screen.width - LEGEND_WIDTH - LEGEND_RIGHT_MARGIN, y, LEGEND_WIDTH, LEGEND_ITEM_HEIGHT), "R - Reset Pose");
				y += LEGEND_ITEM_HEIGHT;
				GUI.Label (new Rect (Screen.width - LEGEND_WIDTH - LEGEND_RIGHT_MARGIN, y, LEGEND_WIDTH, LEGEND_ITEM_HEIGHT), "S - Switch Character");

				GUI.color = oldColor;
		}

	#endregion
}
