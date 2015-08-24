using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	bool toPause = true;
	float pauseDelay = 0f;
	public GameObject myCanvas;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame 
	void Update () {
		if(Input.GetKey(KeyCode.P)){
			Application.Quit();
		}
		if(Input.GetKeyDown(KeyCode.Escape)){
			Pause(toPause);

		}
		if(Input.GetKey(KeyCode.R)){
			Application.LoadLevel(Application.loadedLevelName);
		}

	}
	public void Pause(bool pause){
		Cursor.visible = !Cursor.visible;
		StateManager.Pause = !StateManager.Pause;
		Camera.main.GetComponent<BlurEffect>().enabled = !Camera.main.GetComponent<BlurEffect>().enabled;
		if (pause){
			Time.timeScale = 0;
			myCanvas.SetActive(true);
			toPause = false;
		}
		else{
			Time.timeScale = 1;
			myCanvas.SetActive(false);
			toPause = true;
		}
	}
	public void Restart(){
		Application.LoadLevel(Application.loadedLevelName);

	}
	public void Quit(){
		Application.Quit();

	}

}
